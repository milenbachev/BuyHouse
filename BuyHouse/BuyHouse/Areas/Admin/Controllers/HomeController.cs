namespace BuyHouse.Areas.Admin.Controllers
{
    using BuyHouse.Models.Home;
    using BuyHouse.Services.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratolRoleName)]
    public class HomeController :Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public IActionResult Index() 
        {
            var total = this.homeService.GetCounts();

            return this.View(new IndexViewModel
            {
                TotalAgents = total.TotalAgents,
                TotalProperties = total.TotalProperties,
                TotalCategory = total.TotalCategory,
                TotalUsers = total.TotalUsers
            });
        }
    }
}
