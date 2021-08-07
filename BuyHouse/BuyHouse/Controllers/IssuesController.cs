namespace BuyHouse.Controllers
{
    using BuyHouse.Infrastructure;
    using BuyHouse.Models.Issues;
    using BuyHouse.Services.Agents;
    using BuyHouse.Services.Issues;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class IssuesController : Controller
    {
        private readonly IIssueService issueService;
        private readonly IAgentService agentService;

        public IssuesController(IIssueService issueService, IAgentService agentService) 
        {
            this.issueService = issueService;
            this.agentService = agentService;
        }

        [Authorize]
        public IActionResult Create() 
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]

        public IActionResult Create(CreateIssueFormModel issue, int id) 
        {
            var userId = this.User.GetId();
            var userIsAgent = agentService.IsAgent(userId);

            var propertyId = id;

            if (userIsAgent) 
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) 
            {
                return View(issue);
            }

            this.issueService.Create(
                propertyId,
                userId,
                issue.DescriptionIssue);

            return this.RedirectToAction("All", "Properties");
        }
    }
}
