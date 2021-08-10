namespace BuyHouse.Areas.Admin.Services
{
    public interface ICategoryService
    {
        int Create(string name);

        bool CategoryExist(string name);
    }
}
