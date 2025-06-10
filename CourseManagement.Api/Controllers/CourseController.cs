using CourseManagement.Core.Models;
using System;
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
    public ResultViewModel GetAllCourses()
    {
        try
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
            return ResultViewModel.Success("Get all courses successfully", courses);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpGet("{id}")]
    public ResultViewModel GetCourseById(string id)
    {
        try
        {
            var c = courseService.GetCourseById(id);
            if (c == null)
            {
                return ResultViewModel.Fail("Course not found");
            }
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
            return ResultViewModel.Success("Get course by id successfully", course);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPost]
    public ResultViewModel CreateCourse([FromBody] CourseRequestModel model)
    {
        try
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
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPut("{id}")]
    public ResultViewModel UpdateCourse(string id, [FromBody] CourseRequestModel model)
    {
        try
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
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpDelete("{id}")]
    public ResultViewModel DeleteCourse(string id)
    {
        try
        {
            var result = courseService.DeleteCourse(id);
            Response.StatusCode = result.Code;
            return result;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpGet("category/{categoryId}")]
    public ResultViewModel GetAllCoursesByCategoryId(string categoryId)
    {
        try
        {
            var courses = courseService.GetAllCoursesByCategoryId(categoryId);
            return courses;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpGet("lesson/{lessonId}")]
    public ResultViewModel GetAllCoursesByLessonId(string lessonId)
    {
        try
        {
            var c = courseService.GetCourseByLessonId(lessonId);
            if (c == null)
            {
                return ResultViewModel.Fail("Course not found");
            }
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
            return ResultViewModel.Success("Get course by lesson id successfully", course);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
}