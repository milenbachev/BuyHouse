namespace BuyHouse.Areas.Admin.Controllers
{
    using AutoMapper;
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
        private readonly IMapper mapper;

        public AgentsController(IAgentAdminService service, IAgentService agentService, IMapper mapper) 
        {
            this.service = service;
            this.agentService = agentService;
            this.mapper = mapper;
        }

        public IActionResult Edit(int id) 
        {
            var agent = this.agentService.Details(id);

            var agentForm = this.mapper.Map<AgentFormModel>(agent);

            return this.View(agentForm);
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

        public IActionResult Delete(int id) 
        {
            this.service.Delete(id);

            return RedirectToAction("All", "Users");
        }
    }
}
