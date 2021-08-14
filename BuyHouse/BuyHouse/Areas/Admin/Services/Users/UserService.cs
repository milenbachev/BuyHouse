namespace BuyHouse.Areas.Admin.Services.Users
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuyHouse.Data;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly BuyHouseDbContext data;
        private readonly IMapper mapper;

        public UserService(BuyHouseDbContext data, IMapper mapper) 
        {
            this.data = data;
            this.mapper = mapper;
        }


        public UserServiceQueryModel All(int usersPerPage, int curentPage)
        {
            var queryUser = this.data.Users.AsQueryable();

            var totalUsers = queryUser.Count();

            var user = this.data
                .Users
                .ProjectTo<UserServiceListModel>(this.mapper.ConfigurationProvider)
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
