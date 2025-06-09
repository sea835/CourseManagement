using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CourseManagement.Service.IServices;

public interface IDocumentService
{
    public ResultViewModel GetAllDocuments();
    public ResultViewModel GetDocumentById(string id);
    public ResultViewModel CreateDocument(Document document);
    public ResultViewModel UpdateDocument(Document document);
    public ResultViewModel DeleteDocument(string id);
    IEnumerable<DocumentViewModel> GetDocumentsByLessonId(string lessonId);
    Task UploadAndCreateDocument(DocumentViewModel model, IFormFile documentFile, string rootPath, Action<string> setPathCallback);
    Task<(Stream FileStream, string ContentType, string FileName)> GetDocumentStreamById(string id, string webRootPath);
}