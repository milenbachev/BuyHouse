namespace BuyHouse.Areas.Admin.Services.Users
{
    using System.Collections.Generic;

    public class UserServiceQueryModel
    {
        public int CurentPage { get; set; }

        public int TotalUsers { get; set; }

        public int UsersPerPage { get; set; }

        public IEnumerable<UserServiceListModel> Users { get; set; }
    }
}
