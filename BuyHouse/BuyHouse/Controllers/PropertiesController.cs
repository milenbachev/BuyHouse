namespace BuyHouse.Controllers
{
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


        public PropertiesController(IPropertyService propertyService, IAgentService agentService) 
        {
            this.propertyService = propertyService;
            this.agentService = agentService;
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
                agentId
                );

            return this.RedirectToAction("All", "Properties");
        }

        public IActionResult All([FromQuery]AllPropertyModel property) 
        {
            var queryProperties = this.propertyService.All(
                property.Transaction,
                property.City,
                property.Construction,
                property.CurentPage,
                AllPropertyModel.PropertyPerPage);

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

            if (!this.agentService.IsAgent(userId)) 
            {
                return RedirectToAction(nameof(AgentsController.Create), "Agents");
            }

            var property = this.propertyService.Details(id);

            return this.View(new PropertyFormModel
            {
                Area = property.Area,
                Title = property.Title,
                Year = property.Year,
                Floor = property.Floor,
                Floors = property.Floors,
                BedRoom = property.BedRoom,
                Bath = property.Bath,
                Price = property.Price,
                ImageUrl = property.ImageUrl,
                Description = property.Description,
                City = property.City,
                Construction = property.Construction,
                TypeOfTransaction = property.Transaction,
                CategotyId = property.CategoryId,
                Categories = this.propertyService.AllCategory(),
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, PropertyFormModel property) 
        {
            var agentId = agentService.AgentsId(this.User.GetId());

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
            if (!this.propertyService.AgentIsCreate(id, (int)agentId)) 
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

            if (agentId == 0) 
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
    }
}
