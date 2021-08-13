namespace BuyHouse.Areas.Admin.Services.Users
{
    public interface IUserService
    {
        UserServiceQueryModel All(int usersPerPage, int curentPage);
    }
}
