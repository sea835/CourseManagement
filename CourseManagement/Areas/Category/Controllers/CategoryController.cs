using CourseManagement.Models;
using CourseManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Category.Controllers;

[Area("Category")]
public class CategoryController: Controller
{
    private  readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
            _unitOfWork = unitOfWork;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult GetAllCategories()
    {
        var categories = _unitOfWork.CategoryRepository.GetAll().Data;
        return Json(new { data = categories });
    }
    
    public IActionResult GetAllCategoriesSelect2(string search)
    {
        var result = _unitOfWork.CategoryRepository.GetAll();
        var categories = result.Data as List<Models.Category>;

        if (!string.IsNullOrEmpty(search))
        {
            categories = categories
                .Where(c => c.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        var viewModels = categories
            .Select<Models.Category, Select2ViewModel>(c => new Select2ViewModel
            {
                Id = c.CategoryId,
                Text = c.Name
            })
            .ToList();

        var select2Result = new Select2ResultModel
        {
            Results = viewModels,
            More = false // nếu có phân trang thì true
        };

        return Json(select2Result);
    }
}