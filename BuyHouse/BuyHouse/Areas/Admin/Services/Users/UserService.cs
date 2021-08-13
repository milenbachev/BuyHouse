namespace BuyHouse.Areas.Admin.Services.Users
{
    using BuyHouse.Data;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly BuyHouseDbContext data;

        public UserService(BuyHouseDbContext data) 
        {
            this.data = data;
        }


        public UserServiceQueryModel All(int usersPerPage, int curentPage)
        {
            var queryUser = this.data.Users.AsQueryable();

            var totalUsers = queryUser.Count();

            var user = this.data
                .Users
                .Select(x => new UserServiceListModel
                {
                   Id = x.Id,
                   UserName = x.UserName,
                   FullName = x.FullName,
                   IsAgent = x.IsAgent
                })
                .Skip((curentPage - 1) * usersPerPage)
                .Take(usersPerPage)
                .ToList();

            return new UserServiceQueryModel
            {
                TotalUsers = totalUsers,
                CurentPage = curentPage,
                UsersPerPage = usersPerPage,
                Users = user
            };
        }
    }
}
