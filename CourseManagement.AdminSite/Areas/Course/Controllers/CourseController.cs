using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.AdminSite.Areas.Course.Controllers;

[Area("Course")]
public class CourseController(ICourseService courseService) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetAllCourses()
    {
        var result = courseService.GetAllCourse();
        return Json(new { data = result });
    }

    public IActionResult GetCourseById(string id)
    {
        var course = courseService.GetCourseById(id);
        return Json(course);
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

    public IActionResult GetAllCoursesSelect2(string searchTerm)
    {
        var courses = courseService.GetAllCoursesSelect2(searchTerm);
        return Json(new { results = courses });
    }
    
    public IActionResult GetAllCoursesByCategoryId(string categoryId)
    {
        var courses = courseService.GetAllCoursesByCategoryId(categoryId);
        return Json(new { data = courses });
    }
    
    public IActionResult GetCourseByLessonId(string id)
    {
        var course = courseService.GetCourseByLessonId(id);
        return Json(course);
    }
    
    public IActionResult GetCourseByLessonIdSelect2(string lessonId)
    {
        var course = courseService.GetCourseByLessonId(lessonId);
        return Json(new
        {
            id = course.CourseId,
            text = course.Title
        });
    }
}
