using CourseManagement.IServices;
using CourseManagement.Models;
using CourseManagement.UnitOfWork;
using CourseManagement.ViewModels;

namespace CourseManagement.Services;

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
            var categories = unitOfWork.Category.GetAll();
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
            var category = unitOfWork.Category.Find(c => c.CategoryId == id);
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
            unitOfWork.Category.Update(category);
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
                .Where(c => string.IsNullOrEmpty(search) || c.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
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