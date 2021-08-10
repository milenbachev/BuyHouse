namespace BuyHouse.Areas.Admin.Controllers
{
    using BuyHouse.Areas.Admin.Models;
    using BuyHouse.Areas.Admin.Services;
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

            return RedirectToAction("Index", "Home");
        }
    }
}
