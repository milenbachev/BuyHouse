namespace BuyHouse.Areas.Admin.Models.Users
{
    using BuyHouse.Areas.Admin.Services.Users;
    using System.Collections.Generic;

    public class AllUserModel
    {
        public int CurentPage { get; set; } = 1;

        public int UserPerPage { get; set; } = 5;

        public int TotalUsers { get; set; }

        public IEnumerable<UserServiceListModel> Users { get; set; }
    }
}
