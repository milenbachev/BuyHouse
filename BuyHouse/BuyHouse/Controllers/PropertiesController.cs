namespace BuyHouse.Controllers
{
    using BuyHouse.Infrastructure;
    using BuyHouse.Models.Properties;
    using BuyHouse.Services.Agents;
    using BuyHouse.Services.City;
    using BuyHouse.Services.Properties;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PropertiesController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly IAgentService agentService;
        private readonly ICityService cityService;

        public PropertiesController(IPropertyService propertyService, IAgentService agentService, ICityService cityService) 
        {
            this.propertyService = propertyService;
            this.agentService = agentService;
            this.cityService = cityService;
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
                Transactions = this.propertyService.AllTransaction(),               
                Cities = this.cityService.AllCity(),
                Constructions = this.propertyService.AllConstruction()
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

            if (!this.propertyService.TransactionExist(property.TypeOfTransactionId)) 
            {
                this.ModelState.AddModelError(nameof(property.TypeOfTransactionId), "Type of transaction does not exist");
            }

            if (!this.cityService.CityExist(property.CityId)) 
            {
                this.ModelState.AddModelError(nameof(property.CityId), "City does not exist");
            }

            if (!this.propertyService.ConstructionExist(property.ConstructionId)) 
            {
                this.ModelState.AddModelError(nameof(property.ConstructionId), "Construction does nor exist");
            }

            if (!ModelState.IsValid) 
            {
                property.Categories = this.propertyService.AllCategory();
                property.Transactions = this.propertyService.AllTransaction();
                property.Cities = this.cityService.AllCity();
                property.Constructions = this.propertyService.AllConstruction();

                return this.View(property);
            }

            //TODO AGENTiD
            this.propertyService.Create(
                property.Area,
                property.Floor,
                property.Floors,
                property.BedRoom,
                property.Bath,
                property.Price,
                property.ImageUrl,
                property.Description,
                property.CategotyId,
                property.TypeOfTransactionId,
                property.CityId,
                property.AgentId,
                property.ConstructionId);

            return this.RedirectToAction("All", "Properties");
        }

        public IActionResult All(AllPropertyModel property) 
        {
            var queryProprties = this.propertyService.All(
                property.Category,
                property.City,
                property.Transaction,
                property.Construction,
                property.CurentPage,
                AllPropertyModel.PropertyPerPage);

            property.Properties = queryProprties.Properties;

            return this.View(property);
        }
    }
}
