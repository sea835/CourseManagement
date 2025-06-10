using CourseManagement.Core.Models;
using System.Collections.Generic;
using CourseManagement.Core.RequestModels;
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
        var result = categoryService.GetAllCategory();
        var data = (result.Data as IEnumerable<Category>)?.Select(c => new CategoryResponseModel
        {
            CategoryId = c.CategoryId,
            Name = c.Name,
            Description = c.Description,
            Icon = c.Icon
        });
        result.Data = data;
        Response.StatusCode = result.Code;
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetCategoryById(string id)
    {
        var result = categoryService.GetCategoryById(id);
        if (result.Data is Category c)
        {
            result.Data = new CategoryResponseModel
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                Icon = c.Icon
            };
        }
        Response.StatusCode = result.Code;
        return Ok(result);
    }

    [HttpPost]
    public ResultViewModel CreateCategory([FromBody] CategoryRequestModel model)
    {
        var category = new Category
        {
            Name = model.Name,
            Description = model.Description,
            Icon = model.Icon
        };
        var result = categoryService.CreateCategory(category);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpPut("{id}")]
    public ResultViewModel UpdateCategory(string id, [FromBody] CategoryRequestModel model)
    {
        var category = new Category
        {
            CategoryId = id,
            Name = model.Name,
            Description = model.Description,
            Icon = model.Icon
        };
        var result = categoryService.UpdateCategory(category);
        Response.StatusCode = result.Code;
        return result;
    }

    [HttpDelete("{id}")]
    public ResultViewModel DeleteCategory(string id)
    {
        var result = categoryService.DeleteCategory(id);
        Response.StatusCode = result.Code;
        return result;
    }
}