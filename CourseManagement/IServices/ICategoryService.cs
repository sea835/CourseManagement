using CourseManagement.Models;
using CourseManagement.ViewModels;

namespace CourseManagement.IServices;

public interface ICategoryService
{
    public ResultViewModel GetAllCategory();
    public ResultViewModel GetCategoryById(string id);
    public ResultViewModel CreateCategory(Category category);
    public ResultViewModel UpdateCategory(Category category);
    public ResultViewModel DeleteCategory(string id);
    public ResultViewModel GetAllCategoriesSelect2(string search);
}