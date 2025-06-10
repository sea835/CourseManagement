using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using CourseManagement.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController (ICategoryService categoryService): ControllerBase
{
    [HttpGet]
    public IActionResult GetAllCategory()
    {
        var categories = categoryService.GetAllCategory();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public IActionResult GetCategoryById(string id)
    {
        var category = categoryService.GetCategoryById(id);
        return Ok(category);
    }

    [HttpPost]
    public ResultViewModel CreateCategory([FromBody] Category category)
    {
        var result = categoryService.GetAllCategory();
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpPut]
    public ResultViewModel UpdateCategory([FromBody] Category category)
    {
        var result = categoryService.GetAllCategory();
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpDelete("{id}")]
    public ResultViewModel DeleteCategory(string id)
    {
        var result = categoryService.GetAllCategory();
        Response.StatusCode = result.Code;
        return result;
    }
}