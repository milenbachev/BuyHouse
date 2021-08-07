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

            var totalProprties = total.TotalProperties;
            var totalAgent = total.TotalAgents;

            return this.View(new IndexViewModel 
            {
                Properties = property,
                TotalAgents = totalAgent,
                TotalProperties = totalProprties
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
