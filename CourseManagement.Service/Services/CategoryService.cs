using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using CourseManagement.Data.UnitOfWork;
using CourseManagement.Service.IServices;

namespace CourseManagement.Service.Services;

public class CategoryService: ICategoryService
{
    private readonly IUnitOfWork unitOfWork;
    
    public CategoryService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    
    public ResultViewModel GetAllCategory()
    {
        try
        {
            var categories = unitOfWork.Category.GetAll().Where(c => c.IsDeleted == false);
            return ResultViewModel.Success("Get All Categories Success!", categories);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel GetCategoryById(string id)
    {
        try
        {
            var category = unitOfWork.Category.BuildQuery(c => c.CategoryId == id).FirstOrDefault();
            return ResultViewModel.Success("Get Category Id: " + id + " Success", category);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel CreateCategory(Category category)
    {
        try
        {
            category.CategoryId = Guid.NewGuid().ToString();
            unitOfWork.Category.Add(category);
            unitOfWork.SaveChange();
            return ResultViewModel.Success("Create New Category Success");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
        public ResultViewModel UpdateCategory(Category category)
        {
            try
            {
                var existingCategory = unitOfWork.Category
                    .BuildQuery(c => c.CategoryId == category.CategoryId)
                    .FirstOrDefault();

                if (existingCategory == null)
                {
                    return ResultViewModel.Fail("Category not found");
                }

                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;
                existingCategory.SetUpdated();

                unitOfWork.Category.Update(existingCategory);
                unitOfWork.SaveChange();
                return ResultViewModel.Success("Update Category Success");
            }
            catch (Exception ex)
            {
                return ResultViewModel.FailException(ex);
            }
        }
    
    public ResultViewModel DeleteCategory(string id)
    {
        try
        {
            unitOfWork.Category.Delete(id);
            unitOfWork.SaveChange();
            return ResultViewModel.Success("Delete Category Success");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel GetAllCategoriesSelect2(string search)
    {
        try
        {
            var categories = unitOfWork.Category.GetAll();
            var select2Result = categories
                .Where(c => c.IsDeleted == false && (string.IsNullOrEmpty(search) || c.Name.Contains(search, StringComparison.OrdinalIgnoreCase)))
                .Select(c => new Select2ViewModel
                {
                    Id = c.CategoryId,
                    Text = c.Name
                });
            return ResultViewModel.Success("Get All Categories for Select2 Success", select2Result);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
}