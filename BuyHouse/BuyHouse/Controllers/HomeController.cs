namespace BuyHouse.Controllers
{
    using BuyHouse.Models;
    using BuyHouse.Models.Home;
    using BuyHouse.Services.Home;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService) 
        {
            this.homeService = homeService;
        }

        public IActionResult Index()
        {
            var property = this.homeService
                .GetLast();

            var total = this.homeService.GetCounts();

            return this.View(new IndexViewModel 
            {
                Properties = property,
                TotalAgents = total.TotalAgents,
                TotalProperties = total.TotalProperties,
                TotalCategory = total.TotalCategory,
                TotalUsers = total.TotalUsers
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
