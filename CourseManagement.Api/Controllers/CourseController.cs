using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using CourseManagement.Service.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController(ICourseService courseService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllCourses()
    {
        var courses = courseService.GetAllCourse();
        return new JsonResult(courses);
    }

    [HttpGet("{id}")]
    public IActionResult GetCourseById(string id)
    {
        var course = courseService.GetCourseById(id);
        return new JsonResult(course);
    }

    [HttpPost]
    public ResultViewModel CreateCourse([FromBody] CourseViewModel course)
    {
        var result = courseService.CreateCourse(course);
        return result;
    }

    [HttpPut]
    public ResultViewModel UpdateCourse([FromBody] CourseViewModel course)
    {
        var result = courseService.UpdateCourse(course);
        return result;
    }

    [HttpDelete("{id}")]
    public ResultViewModel DeleteCourse(string id)
    {
        var result = courseService.DeleteCourse(id);
        return result;
    }
}