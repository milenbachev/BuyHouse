namespace BuyHouse.Areas.Admin.Services.Categories
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuyHouse.Data;
    using BuyHouse.Data.Models;
    using System.Linq;

    public class CategoryService : ICategoryService
    {
        private readonly BuyHouseDbContext data;
        private readonly IMapper mapper;

        public CategoryService(BuyHouseDbContext data, IMapper mapper) 
        {
            this.data = data;
            this.mapper = mapper;
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

        public CategoryServiceQueryModel All(int categoryPerPage, int curentPage)
        {
            var queryCategory = this.data.Categories.AsQueryable();
            var totalCategories = queryCategory.Count();

            var category = this.data
                .Categories
                .ProjectTo<CategoryServiceListModel>(this.mapper.ConfigurationProvider)
                .Skip((curentPage - 1) * categoryPerPage)
                .Take(categoryPerPage)
                .ToList();

            return new CategoryServiceQueryModel
            {
                TotalCategories = totalCategories,
                CurentPage = curentPage,
                CategoriesPerPage = categoryPerPage,
                Categories = category
            };
        }

        public bool Edit(int id, string name)
        {
            var category = this.data.Categories.Find(id);

            if (category == null) 
            {
                return false;
            }

            category.Name = name;

            this.data.SaveChanges();

            return true;
        }

        public CategoryServiceListModel Details(int id)
        {
            var category = this.data
                .Categories
                .Where(x => x.Id == id)
                .ProjectTo<CategoryServiceListModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

            return category;
        }

        public bool Delete(int id)
        {
            var category = this.data
                .Categories
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (category == null) 
            {
                return false;
            }

            this.data.Categories.Remove(category);
            this.data.SaveChanges();

            return true;
        } 
    }
}
