using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using CourseManagement.Data.UnitOfWork;
using CourseManagement.Service.IServices;

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
            unitOfWork.SaveChange();
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
                var existingDocument = unitOfWork.Document
                    .BuildQuery(d => d.DocumentId == document.DocumentId)
                    .FirstOrDefault();

                if (existingDocument == null)
                {
                    return ResultViewModel.Fail("Document not found");
                }

                existingDocument.Title = document.Title;
                existingDocument.FilePath = document.FilePath;
                existingDocument.FileType = document.FileType;
                existingDocument.LessonId = document.LessonId;
                existingDocument.SizeInBytes = document.SizeInBytes;
                existingDocument.SetUpdated();

                unitOfWork.Document.Update(existingDocument);
                unitOfWork.SaveChange();
                return ResultViewModel.Success(
                    "Document updated successfully",
                    existingDocument);
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
            unitOfWork.SaveChange();
            return ResultViewModel.Success("Document deleted successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
}