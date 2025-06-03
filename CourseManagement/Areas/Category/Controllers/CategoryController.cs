using CourseManagement.Models;
using CourseManagement.IServices;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Category.Controllers;

[Area("Category")]
public class CategoryController: Controller
{
    private readonly ICategoryService categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }
    
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
}