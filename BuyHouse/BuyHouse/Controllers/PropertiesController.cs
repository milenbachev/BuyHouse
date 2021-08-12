namespace BuyHouse.Controllers
{
    using AutoMapper;
    using BuyHouse.Infrastructure;
    using BuyHouse.Models.Properties;
    using BuyHouse.Services.Agents;
    using BuyHouse.Services.Properties;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PropertiesController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly IAgentService agentService;
        private readonly IMapper mapper;

        public PropertiesController(IPropertyService propertyService, IAgentService agentService, IMapper mapper) 
        {
            this.propertyService = propertyService;
            this.agentService = agentService;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Add() 
        {
            if (!this.agentService.IsAgent(this.User.GetId())) 
            {
                return RedirectToAction(nameof(AgentsController.Create), "Agents");
            }

            return this.View(new PropertyFormModel
            {
                Categories = this.propertyService.AllCategory(),
            }); 
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(PropertyFormModel property) 
        {
            var agentId = this.agentService.AgentsId(this.User.GetId());

            if (agentId == 0) 
            {
                return RedirectToAction(nameof(AgentsController.Create), "Agents");
            }
            if (!this.propertyService.CategoryExist(property.CategotyId)) 
            {
                this.ModelState.AddModelError(nameof(property.CategotyId), "Category does not exist");
            }

            if (!ModelState.IsValid) 
            {
                property.Categories = this.propertyService.AllCategory();

                return this.View(property);
            }

            this.propertyService.Create(
                property.Area,
                property.Title,
                property.Year,
                property.Floor,
                property.Floors,
                property.BedRoom,
                property.Bath,
                property.Price,
                property.ImageUrl,
                property.Description,
                property.CategotyId,
                property.TypeOfTransaction,
                property.City,
                property.Construction,
                agentId);

            return this.RedirectToAction("All", "Properties");
        }

        public IActionResult All([FromQuery]AllPropertyModel property) 
        {
            var queryProperties = this.propertyService.All(
                property.Transaction,
                property.City,
                property.Construction,
                property.CurentPage,
                AllPropertyModel.ObjectPerPage);

            var propertiesCity = this.propertyService.AllCity();
            var propertyConstruction = this.propertyService.AllConstruction();
            var propertyTransaction = this.propertyService.AllTransaction();

            property.TotalProperties = queryProperties.TotalProperties;
            property.Cities = propertiesCity;
            property.Constructions = propertyConstruction;
            property.Transactions = propertyTransaction;
            property.Properties = queryProperties.Properties;

            return this.View(property);
        }

        [Authorize]
        public IActionResult Details(int id) 
        {
            var property = this.propertyService
                .Details(id);

            return this.View(property);
        }

        [Authorize]
        public IActionResult Edit(int id) 
        {
            var userId = this.User.GetId();

            if (!this.agentService.IsAgent(userId) && !User.IsAdmin()) 
            {
                return RedirectToAction(nameof(AgentsController.Create), "Agents");
            }

            var property = this.propertyService.Details(id);

            if (property.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var propertyForm = this.mapper.Map<PropertyFormModel>(property);

            propertyForm.Categories = this.propertyService.AllCategory();

            return View(propertyForm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, PropertyFormModel property) 
        {
            var agentId = agentService.AgentsId(this.User.GetId());

            if (agentId == 0 && !User.IsAdmin()) 
            {
                return RedirectToAction(nameof(AgentsController.Create), "Agents");
            }
            if (!this.propertyService.CategoryExist(property.CategotyId)) 
            {
                this.ModelState.AddModelError(nameof(property.CategotyId), "Category does not exist");
            }
            if (!ModelState.IsValid) 
            {
                property.Categories = this.propertyService.AllCategory();

                return this.View(property);
            }
            if (!this.propertyService.AgentIsCreate(id, (int)agentId) && !User.IsAdmin()) 
            {
                return BadRequest();
            }

            this.propertyService.Edit(
                id,
                property.Area,
                property.Title,
                property.Year,
                property.Floor,
                property.Floors,
                property.BedRoom,
                property.Bath,
                property.Price,
                property.ImageUrl,
                property.Description,
                property.CategotyId,
                property.TypeOfTransaction,
                property.City,
                property.Construction);

            return this.RedirectToAction("All", "Properties");
        }

        [Authorize]
        public IActionResult Delete(int id) 
        {
            var agentId = this.agentService.AgentsId(this.User.GetId());

            if (agentId == 0 && !User.IsAdmin()) 
            {
                return RedirectToAction(nameof(AgentsController.Create), "Agents");
            }

            this.propertyService.Delete(id);

            return this.RedirectToAction("All", "Properties");
        }

        [Authorize]
        public IActionResult AgentProperties() 
        {
            var properties = this.propertyService.AgentProperties(this.User.GetId());

            return this.View(properties);
        }

        [Authorize]
        public IActionResult CurentAgent(int id) 
        {
            var properties = this.propertyService.CurentAgentProperties(id);

            return this.View(properties);
        }
    }
}
