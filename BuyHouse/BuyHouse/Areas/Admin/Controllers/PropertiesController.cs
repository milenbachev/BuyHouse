namespace BuyHouse.Areas.Admin.Controllers
{
    using BuyHouse.Models.Properties;
    using BuyHouse.Services.Properties;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratolRoleName)]
    public class PropertiesController : Controller
    {
        private const int propertiesPerPage = 5;

        private readonly IPropertyService service;

        public PropertiesController(IPropertyService service) 
        {
            this.service = service;
        }
        public IActionResult All([FromQuery] AllPropertyModel property) 
        {
            var queryProperties = this.service.All(
                property.Transaction,
                property.City,
                property.Construction,
                property.CurentPage,
                propertiesPerPage,
                publicOnly: false);

            var propertiesCity = this.service.AllCity();

            property.TotalProperties = queryProperties.TotalProperties;
            property.Cities = propertiesCity;
            property.Properties = queryProperties.Properties;

            return this.View(property);
        }

        public IActionResult Approve(int id) 
        {
            this.service.Approve(id);

            return RedirectToAction("All", "Properties");
        }
    }
}
