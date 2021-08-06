namespace BuyHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IssuesController : Controller
    {
        [Authorize]
        public IActionResult Create() 
        {
            return this.View();
        }
    }
}
