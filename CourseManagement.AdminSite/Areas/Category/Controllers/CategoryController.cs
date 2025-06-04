using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.AdminSite.Areas.Category.Controllers;

[Area("Category")]
public class CategoryController(ICategoryService categoryService): Controller
{
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult GetAllCategories()
    {
        var result = categoryService.GetAllCategory();
        return Json(new { data = result.Data });
    }
    
    public IActionResult GetAllCategoriesSelect2(string search)
    {
        var result = categoryService.GetAllCategoriesSelect2(search);
        return Json(new { results = result });
    }

    public IActionResult CreateCategory()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateCategory(CategoryViewModel categoryViewModel)
    {
        categoryService.CreateCategory(categoryViewModel);
        return Ok();
    }
}