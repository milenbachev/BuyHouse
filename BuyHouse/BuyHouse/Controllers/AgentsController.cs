namespace BuyHouse.Controllers
{
    using BuyHouse.Infrastructure;
    using BuyHouse.Models.Agents;
    using BuyHouse.Services.Agents;
    using BuyHouse.Services.City;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AgentsController : Controller
    {
        private readonly IAgentService agentService;
        private readonly ICityService cityService;

        public AgentsController(IAgentService agentService, ICityService cityService) 
        {
            this.agentService = agentService;
            this.cityService = cityService;
        }

        [Authorize]
        public IActionResult Create() 
        {
            return this.View(new AgentFormModel
            {
                Cities = this.cityService.AllCity()
            }) ;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(AgentFormModel agent) 
        {
            var userId = this.User.GetId();

            var userIsAgent = agentService.IsAgent(userId);  

            if (userIsAgent) 
            {
                return BadRequest();
            }
            if (!this.cityService.CityExist(agent.CityId))
            {
                this.ModelState.AddModelError(nameof(agent.CityId), "City does not exist");
            }

            if (!ModelState.IsValid) 
            {
                agent.Cities = this.cityService.AllCity();

                return View(agent);
            }

            this.agentService.Create(
               agent.Name,
               agent.PhoneNumber,
               agent.Description,
               agent.ImageUrl,
               agent.CityId);

            return this.RedirectToAction("All", "Properties"); 
        }
    }
}
