using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using CourseManagement.Data.UnitOfWork;
using CourseManagement.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.Threading.Tasks;

namespace CourseManagement.Service.Services;

public class DocumentService: IDocumentService
{
    private readonly IUnitOfWork unitOfWork;
    
    public DocumentService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    
    public ResultViewModel GetAllDocuments()
    {
        try
        {
            var documents = unitOfWork.Document.GetAll();
            return ResultViewModel.Success("Get all documents successfully", documents);
        }
        catch (Exception ex)
        {
           return ResultViewModel.FailException(ex);
        }
    }

    public ResultViewModel GetDocumentById(string id)
    {
        try
        {
            var document = unitOfWork.Document.GetById(id);
            if (document == null)
            {
                return ResultViewModel.Fail("Document not found");
            }
            return ResultViewModel.Success("Get document successfully", document);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel CreateDocument(Document document)
    {
        try
        {
            if (document == null)
            {
                return ResultViewModel.Fail("Document cannot be null");
            }
            unitOfWork.Document.Add(document);
            // unitOfWork.SaveChange();
            return ResultViewModel.Success("Document created successfully", document);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel UpdateDocument(Document document)
    {
        try
        {
            var existingDocument = unitOfWork.Document.BuildQuery(d=> d.DocumentId == document.DocumentId).FirstOrDefault();
            existingDocument.Title = document.Title;
            existingDocument.DocumentId = document.DocumentId;
            existingDocument.FilePath = document.FilePath;
            existingDocument.FileType = document.FileType;
            existingDocument.LessonId = document.LessonId;
            existingDocument.SizeInBytes = document.SizeInBytes;
            existingDocument.SetUpdated();
            unitOfWork.Document.Update(document);
            // unitOfWork.SaveChange();
            return ResultViewModel.Success("Document updated successfully", document);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel DeleteDocument(string id)
    {
        try
        {
            unitOfWork.Document.Delete(id);
            // unitOfWork.SaveChange();
            return ResultViewModel.Success("Document deleted successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public IEnumerable<DocumentViewModel> GetDocumentsByLessonId(string lessonId)
    {
        return unitOfWork.Document.BuildQuery(d => d.LessonId == lessonId)
            .Select(d => new DocumentViewModel
            {
                DocumentId = d.DocumentId,
                LessonId = d.LessonId,
                Title = d.Title,
                FilePath = d.FilePath,
                FileType = d.FileType,
                SizeInBytes = d.SizeInBytes,
                CreatedAt = d.CreatedAt
            }).ToList();
    }

    public async Task UploadAndCreateDocument(DocumentViewModel model, IFormFile documentFile, string rootPath, Action<string> setPathCallback)
    {
        if (documentFile == null || documentFile.Length == 0)
            throw new Exception("No document file provided.");

        var folder = Path.Combine(rootPath, "wwwroot/documents");
        Directory.CreateDirectory(folder);

        var fileName = Guid.NewGuid() + Path.GetExtension(documentFile.FileName);
        var savePath = Path.Combine(folder, fileName);

        using (var stream = new FileStream(savePath, FileMode.Create))
        {
            await documentFile.CopyToAsync(stream);
        }

        var url = $"/documents/{fileName}";
        setPathCallback(url);

        var doc = new Document
        {
            DocumentId = Guid.NewGuid().ToString(),
            LessonId = model.LessonId,
            Title = model.Title,
            FilePath = url,
            FileType = Path.GetExtension(documentFile.FileName),
            SizeInBytes = documentFile.Length,
            CreatedAt = DateTime.Now
        };

        await unitOfWork.Document.Add(doc);
    }

    public async Task<(Stream FileStream, string ContentType, string FileName)> GetDocumentStreamById(string id, string webRootPath)
    {
        var doc = unitOfWork.Document.GetById(id);
        if (doc == null || string.IsNullOrEmpty(doc.FilePath))
            return (null, null, null);

        var relative = doc.FilePath.TrimStart('/');
        var absolute = Path.Combine(webRootPath, relative);
        if (!File.Exists(absolute))
            return (null, null, null);

        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(absolute, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        var stream = new FileStream(absolute, FileMode.Open, FileAccess.Read, FileShare.Read);
        return (stream, contentType, Path.GetFileName(absolute));
    }
}