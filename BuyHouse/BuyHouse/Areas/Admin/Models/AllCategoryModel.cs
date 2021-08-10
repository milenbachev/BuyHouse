namespace BuyHouse.Areas.Admin.Models
{
    using BuyHouse.Areas.Admin.Services.Categories;
    using System.Collections.Generic;

    public class AllCategoryModel
    {
        public string Name { get; set; }

        public int CurentPage { get; set; } = 1;

        public int CategoryPerPage { get; set; } = 5;

        public int TotalCategories { get; set; }

        public IEnumerable<CategoryServiceListModel> Categories { get; set; }
    }
}
