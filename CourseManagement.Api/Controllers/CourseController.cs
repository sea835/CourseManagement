using CourseManagement.Core.Models;
using System.Linq;
using CourseManagement.Core.RequestModels;
using CourseManagement.Core.ViewModels;
using CourseManagement.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController(ICourseService courseService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllCourses()
    {
        var courses = courseService.GetAllCourse()
            .Select(c => new CourseResponseModel
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Description = c.Description,
                Level = c.Level,
                Duration = c.Duration,
                Language = c.Language,
                ThumbnailUrl = c.ThumbnailUrl,
                Price = c.Price,
                IsFree = c.IsFree,
                AuthorName = c.AuthorName,
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            });
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public IActionResult GetCourseById(string id)
    {
        var c = courseService.GetCourseById(id);
        if (c == null) return NotFound();
        var course = new CourseResponseModel
        {
            CourseId = c.CourseId,
            Title = c.Title,
            Description = c.Description,
            Level = c.Level,
            Duration = c.Duration,
            Language = c.Language,
            ThumbnailUrl = c.ThumbnailUrl,
            Price = c.Price,
            IsFree = c.IsFree,
            AuthorName = c.AuthorName,
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName
        };
        return Ok(course);
    }

    [HttpPost]
    public ResultViewModel CreateCourse([FromBody] CourseRequestModel model)
    {
        var course = new Course
        {
            Title = model.Title,
            Description = model.Description,
            FullDescription = model.FullDescription,
            Level = model.Level,
            Duration = model.Duration,
            Language = model.Language,
            ThumbnailUrl = model.ThumbnailUrl,
            Price = model.Price,
            IsFree = model.IsFree,
            AuthorName = model.AuthorName,
            CategoryId = model.CategoryId
        };
        var result = courseService.CreateCourse(course);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpPut("{id}")]
    public ResultViewModel UpdateCourse(string id, [FromBody] CourseRequestModel model)
    {
        var course = new CourseViewModel
        {
            CourseId = id,
            Title = model.Title,
            Description = model.Description,
            FullDescription = model.FullDescription,
            Level = model.Level,
            Duration = model.Duration,
            Language = model.Language,
            ThumbnailUrl = model.ThumbnailUrl,
            Price = model.Price,
            IsFree = model.IsFree,
            AuthorName = model.AuthorName,
            CategoryId = model.CategoryId
        };
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

    [HttpGet("category/{categoryId}")]
    public IActionResult GetAllCoursesByCategoryId(string categoryId)
    {
        var courses = courseService.GetAllCoursesByCategoryId(categoryId);
        return Ok(courses);
    }

    [HttpGet("lesson/{lessonId}")]
    public IActionResult GetAllCoursesByLessonId(string lessonId)
    {
        var c = courseService.GetCourseByLessonId(lessonId);
        if (c == null) return NotFound();
        var course = new CourseResponseModel
        {
            CourseId = c.CourseId,
            Title = c.Title,
            Description = c.Description,
            Level = c.Level,
            Duration = c.Duration,
            Language = c.Language,
            ThumbnailUrl = c.ThumbnailUrl,
            Price = c.Price,
            IsFree = c.IsFree,
            AuthorName = c.AuthorName,
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName
        };
        return Ok(course);
    }
}