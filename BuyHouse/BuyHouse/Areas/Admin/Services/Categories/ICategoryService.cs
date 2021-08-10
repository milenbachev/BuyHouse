namespace BuyHouse.Areas.Admin.Services.Categories
{
    public interface ICategoryService
    {
        int Create(string name);

        CategoryServiceQueryModel All(int categoryPerPage, int curentPage);

        bool Edit(int id, string name);

        CategoryServiceListModel Details(int id);

        bool Delete(int id);

        bool CategoryExist(string name);
    }
}
