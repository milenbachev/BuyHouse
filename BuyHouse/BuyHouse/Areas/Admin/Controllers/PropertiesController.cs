namespace BuyHouse.Areas.Admin.Controllers
{
    using BuyHouse.Services.Properties;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratolRoleName)]
    public class PropertiesController : Controller
    {
        private readonly IPropertyService service;

        public PropertiesController(IPropertyService service) 
        {
            this.service = service;
        }
        public IActionResult All() 
        {
            var properties = this.service.All(publicOnly: false);
            
            return this.View(properties);
        }

        public IActionResult Approve(int id) 
        {
            this.service.Approve(id);

            return RedirectToAction("All", "Properties");
        }
    }
}
