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
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpPut]
    public ResultViewModel UpdateCourse([FromBody] CourseViewModel course)
    {
        var result = courseService.UpdateCourse(course);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpDelete("{id}")]
    public ResultViewModel DeleteCourse(string id)
    {
        var result = courseService.DeleteCourse(id);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpGet("{categoryId}")]
    public IActionResult GetAllCoursesByCategoryId(string categoryId)
    {
        var courses = courseService.GetAllCoursesByCategoryId(categoryId);
        return Ok(courses);
    }

    [HttpGet("{lessonId}")]
    public IActionResult GetAllCoursesByLessonId(string lessonId)
    {
        var course = courseService.GetCourseByLessonId(lessonId);
        return Ok(course);
    }
}