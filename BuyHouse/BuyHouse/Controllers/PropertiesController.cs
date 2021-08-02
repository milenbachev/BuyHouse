namespace BuyHouse.Controllers
{
    using BuyHouse.Models.Properties;
    using BuyHouse.Services.Properties;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PropertiesController : Controller
    {
        private readonly IPropertyService propertyService;

        public PropertiesController(IPropertyService propertyService) 
        {
            this.propertyService = propertyService;
        }

        public IActionResult Add() 
        {
            return this.View(new PropertyFormModel
            {
                Categories = this.propertyService.AllCategory(),
                Transactions = this.propertyService.AllTransaction(),               
                Cities = this.propertyService.AllCity(),
                Constructions = this.propertyService.AllConstruction()
            }); 
        }

        [HttpPost]
        public IActionResult Add(PropertyFormModel property) 
        {
            if (!this.propertyService.CategoryExist(property.CategotyId)) 
            {
                this.ModelState.AddModelError(nameof(property.CategotyId), "Category does not exist");
            }

            if (!this.propertyService.TransactionExist(property.TypeOfTransactionId)) 
            {
                this.ModelState.AddModelError(nameof(property.TypeOfTransactionId), "Type of transaction does not exist");
            }

            if (!this.propertyService.CityExist(property.CityId)) 
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
                property.Cities = this.propertyService.AllCity();
                property.Constructions = this.propertyService.AllConstruction();

                return this.View(property);
            }

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
    }
}
