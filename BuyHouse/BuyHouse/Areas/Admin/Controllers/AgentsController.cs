namespace BuyHouse.Areas.Admin.Controllers
{
    using BuyHouse.Areas.Admin.Services.Agents;
    using BuyHouse.Models.Agents;
    using BuyHouse.Services.Agents;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratolRoleName)]
    public class AgentsController : Controller
    {
        private readonly IAgentAdminService service;
        private readonly IAgentService agentService;

        public AgentsController(IAgentAdminService service, IAgentService agentService) 
        {
            this.service = service;
            this.agentService = agentService;
        }

        public IActionResult Edit(int id) 
        {
            var agent = this.agentService.Details(id);


            return this.View(new AgentFormModel 
            {
                Name = agent.Name,
                PhoneNumber = agent.PhoneNumber,
                ImageUrl = agent.ImageUrl,
                Description = agent.Description,
                City = agent.City
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, AgentFormModel agent) 
        {
            if (!ModelState.IsValid) 
            {
                return this.View(agent);
            }

            this.service.Edit(
                id,
                agent.Name,
                agent.PhoneNumber,
                agent.Description,
                agent.ImageUrl,
                agent.City);

            return this.RedirectToAction("All","Agents", new { Area =""});
        }
    }
}
