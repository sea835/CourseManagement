using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.AdminSite.Areas.Course.Controllers;

[Area("Course")]
public class CourseController : Controller
{
    private readonly ICourseService courseService;
    private readonly ICategoryService categoryService;

    public CourseController(ICourseService courseService)
    {
        this.courseService = courseService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetAllCourses()
    {
        var result = courseService.GetAllCourse().Data;
        return Json(new { data = result });
    }

    public IActionResult CreateCourse()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateCourse(Core.Models.Course course)
    {
        var result = courseService.CreateCourse(course);
        TempData["ToastType"] = "success";
        TempData["ToastMessage"] = result.SuccessMessage;
        return RedirectToAction("Index");
    }

    public IActionResult EditCourse(string id)
    {
        var course = courseService.GetCourseById(id);

        return View(course);
    }

    [HttpPost]
    public IActionResult EditCourse(CourseViewModel courseViewModel)
    {
        courseService.UpdateCourse(courseViewModel);

        TempData["ToastType"] = "info";
        TempData["ToastMessage"] = "Course updated successfully!";
        return RedirectToAction("Index");
    }


    [HttpPost]
    public IActionResult DeleteCourse(string id)
    {
        courseService.DeleteCourse(id);
        TempData["ToastType"] = "error";
        TempData["ToastMessage"] = "Course deleted successfully!";
        return RedirectToAction("Index");
    }
}
