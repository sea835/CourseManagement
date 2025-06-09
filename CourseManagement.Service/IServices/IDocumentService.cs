using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;

namespace CourseManagement.Service.IServices;

public interface IDocumentService
{
    public ResultViewModel GetAllDocuments();
    public ResultViewModel GetDocumentById(string id);
    public ResultViewModel CreateDocument(Document document);
    public ResultViewModel UpdateDocument(Document document);
    public ResultViewModel DeleteDocument(string id);
}