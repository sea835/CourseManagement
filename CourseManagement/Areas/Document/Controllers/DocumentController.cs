using CourseManagement.IServices;
using CourseManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Document.Controllers;

[Area("Document")]
public class DocumentController: Controller
{
    private readonly IDocumentService documentService;
    
    public DocumentController(IDocumentService documentService)
    {
        this.documentService = documentService;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult GetAllDocuments()
    {
        var documents = documentService.GetAllDocuments();
        return Json(new { data = documents });
    }
}