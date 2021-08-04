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
    }
}
