namespace BuyHouse.Areas.Admin.Controllers
{
    using BuyHouse.Areas.Admin.Models.Categories;
    using BuyHouse.Areas.Admin.Services.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratolRoleName)]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService) 
        {
            this.categoryService = categoryService;
        } 
        public IActionResult Create() 
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CategoryFormModel category) 
        {
            if (this.categoryService.CategoryExist(category.Name))
            {
                return View(category);
            }
            if (!ModelState.IsValid) 
            {
                return View(category);
            }

            this.categoryService.Create(
                category.Name);

            return RedirectToAction("All", "Categories");
        }


        public IActionResult All([FromQuery]AllCategoryModel category) 
        {
            var queryCategory = this.categoryService.All(
                category.CategoryPerPage,
                category.CurentPage);

            category.TotalCategories = queryCategory.TotalCategories;
            category.Categories = queryCategory.Categories;

            return this.View(category);
        }

        public IActionResult Edit(int id) 
        {
            var category = this.categoryService.Details(id);

            return this.View(new CategoryFormModel 
            {
                Name = category.Name
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, CategoryFormModel category) 
        {
            if (!ModelState.IsValid) 
            {
                return View(category);
            }

            this.categoryService.Edit(
                id,
                category.Name);

            return this.RedirectToAction("All", "Categories");
        }

        public IActionResult Delete(int id) 
        {
            this.categoryService.Delete(id);

            return this.RedirectToAction("All", "Categories");
        }
    }
}
