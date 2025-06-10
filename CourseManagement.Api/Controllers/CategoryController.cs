using CourseManagement.Core.Models;
using System;
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
    public ResultViewModel GetAllCategory()
    {
        try
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
            return result;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpGet("{id}")]
    public ResultViewModel GetCategoryById(string id)
    {
        try
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
            return result;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPost]
    public ResultViewModel CreateCategory([FromBody] CategoryRequestModel model)
    {
        try
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
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpPut("{id}")]
    public ResultViewModel UpdateCategory(string id, [FromBody] CategoryRequestModel model)
    {
        try
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
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    [HttpDelete("{id}")]
    public ResultViewModel DeleteCategory(string id)
    {
        try
        {
            var result = categoryService.DeleteCategory(id);
            Response.StatusCode = result.Code;
            return result;
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
}