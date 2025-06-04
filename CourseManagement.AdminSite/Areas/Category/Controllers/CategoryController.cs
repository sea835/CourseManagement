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
        return RedirectToAction("Index");
    }

    public IActionResult EditCategory(string id)
    {
        var category = categoryService.GetCategoryById(id).Data;
        return View(category);
    }
    
    [HttpPost]
    public IActionResult EditCategory(CategoryViewModel categoryViewModel)
    {
        categoryService.UpdateCategory(categoryViewModel);
        TempData["ToastType"] = "info";
        TempData["ToastMessage"] = "Category updated successfully!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult DeleteCategory(string id)
    {
        categoryService.DeleteCategory(id);
        TempData["ToastType"] = "error";
        TempData["ToastMessage"] = "Category delete successfully!";
        return Ok();
    }
}