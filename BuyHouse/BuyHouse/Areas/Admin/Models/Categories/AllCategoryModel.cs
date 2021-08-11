namespace BuyHouse.Areas.Admin.Models.Categories
{
    using BuyHouse.Areas.Admin.Services.Categories;
    using System.Collections.Generic;

    public class AllCategoryModel
    {
        public int CurentPage { get; set; } = 1;

        public int CategoryPerPage { get; set; } = 5;

        public int TotalCategories { get; set; }

        public IEnumerable<CategoryServiceListModel> Categories { get; set; }
    }
}
