using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;

namespace CourseManagement.Core.Interfaces.IServices;

public interface IContentService
{
    public ResultViewModel GetContentById(string id);
    public ResultViewModel UpdateContent(Content content);
    public ResultViewModel CreateContent(Content content);
    public ResultViewModel DeleteContent(string id);
    public ResultViewModel GetAllContents();
}