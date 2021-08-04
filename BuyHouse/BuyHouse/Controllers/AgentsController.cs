﻿namespace BuyHouse.Controllers
{
    using BuyHouse.Infrastructure;
    using BuyHouse.Models.Agents;
    using BuyHouse.Services.Agents;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AgentsController : Controller
    {
        private readonly IAgentService agentService;

        public AgentsController(IAgentService agentService) 
        {
            this.agentService = agentService;
        }

        [Authorize]
        public IActionResult Create() 
        {
            return this.View();
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

            if (!ModelState.IsValid) 
            {
                return View(agent);
            }

            this.agentService.Create(
               agent.Name,
               agent.PhoneNumber,
               agent.Description,
               agent.ImageUrl,
               agent.City,
               userId);

            return this.RedirectToAction("All", "Properties"); 
        }
    }
}
