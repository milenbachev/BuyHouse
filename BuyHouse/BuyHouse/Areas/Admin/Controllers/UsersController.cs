namespace BuyHouse.Areas.Admin.Controllers
{
    using BuyHouse.Areas.Admin.Models.Users;
    using BuyHouse.Areas.Admin.Services.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratolRoleName)]
    public class UsersController : Controller
    {
        private readonly IUserService service;

        public UsersController(IUserService service) 
        {
            this.service = service;
        }

        public IActionResult All([FromQuery] AllUserModel user) 
        {

            var queryUser = this.service.All(
                user.UserPerPage,
                user.CurentPage);

            user.TotalUsers = queryUser.TotalUsers;
            user.Users = queryUser.Users;

            return this.View(user);
        }
    }
}
