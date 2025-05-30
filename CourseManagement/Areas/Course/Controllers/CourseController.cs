using CourseManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Course.Controllers;

[Area("Course")]
public class CourseController: Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CourseController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult GetAllCourses()
    {
        var courses = _unitOfWork.CourseRepository.GetAll();
        return Json(new { data = courses });
    }
}