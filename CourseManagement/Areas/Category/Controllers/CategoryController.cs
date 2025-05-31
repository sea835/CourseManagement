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
}