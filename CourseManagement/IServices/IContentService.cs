using CourseManagement.Models;
using CourseManagement.ViewModels;

namespace CourseManagement.IServices;

public interface IContentService
{
    public ResultViewModel GetContentById(string id);
    public ResultViewModel UpdateContent(Content content);
    public ResultViewModel CreateContent(Content content);
    public ResultViewModel DeleteContent(string id);
    public ResultViewModel GetAllContents();
}