namespace BuyHouse.Areas.Admin.Services.Categories
{
    using System.Collections.Generic;

    public class CategoryServiceQueryModel
    {
        public int CurentPage { get; set; }

        public int TotalCategories { get; set; }

        public int CategoriesPerPage { get; set; }

        public IEnumerable<CategoryServiceListModel> Categories { get; set; }
    }
}
