namespace BuyHouse.Areas.Admin.Services
{
    using BuyHouse.Data;
    using BuyHouse.Data.Models;
    using System;
    using System.Linq;

    public class CategoryService : ICategoryService
    {
        private readonly BuyHouseDbContext data;

        public CategoryService(BuyHouseDbContext data) 
        {
            this.data = data;
        }
        public int Create(string name)
        {
            var category = new Category
            {
                Name = name,
            };

            this.data.Categories.Add(category);
            this.data.SaveChanges();

            return category.Id;
        }

        public bool CategoryExist(string name)
        {
            return this.data.Categories
                .Any(x => x.Name == name);      
        }
    }
}
