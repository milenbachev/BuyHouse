namespace BuyHouse.Controllers
{
    using AutoMapper;
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
        private readonly IMapper mapper;

        public IssuesController(IIssueService issueService, IAgentService agentService, IMapper mapper) 
        {
            this.issueService = issueService;
            this.agentService = agentService;
            this.mapper = mapper;
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
            var userIsAdmin = this.User.IsAdmin();

            var propertyId = id;

            if (userIsAgent || userIsAdmin) 
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

        [Authorize]
        public IActionResult Edit(int id) 
        {
            var userId = this.User.GetId();
            var userIsAdmin = this.User.IsAdmin();

            var issue = this.issueService.Details(id);

            if (issue.UserId != userId && userIsAdmin)
            {
                return Unauthorized();
            }

            var issueForm = this.mapper.Map<CreateIssueFormModel>(issue);

            return this.View(issueForm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, CreateIssueFormModel issue)
        {
            var agentId = agentService.AgentsId(this.User.GetId());

            if (agentId == 0)
            {
                return RedirectToAction(nameof(AgentsController.Create), "Agents");
            }

            if (!ModelState.IsValid) 
            {
                return View(issue);
            }

            this.issueService.Edit(
                id,
                issue.DescriptionIssue,
                issue.CreateOn);

            return this.RedirectToAction("AgentProperties", "Properties");
        }

        [Authorize]
        public IActionResult Delete(int id) 
        {
            var agentId = this.agentService.AgentsId(this.User.GetId());

            if (agentId == 0)
            {
                return RedirectToAction(nameof(AgentsController.Create), "Agents");
            }

            this.issueService.Delete(id);

            return this.RedirectToAction("AgentProperties", "Properties");
        }

        [Authorize]
        public IActionResult IssueInfo(int id) 
        {
            var issues = this.issueService.AllIssue(id);

            return this.View(issues);
        }
    }
}
