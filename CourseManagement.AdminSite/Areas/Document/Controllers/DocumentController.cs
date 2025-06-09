using CourseManagement.Service.IServices;
using CourseManagement.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CourseManagement.AdminSite.Areas.Document.Controllers;

[Area("Document")]
public class DocumentController : Controller
{
    private readonly IDocumentService _documentService;
    private readonly IWebHostEnvironment _env;

    public DocumentController(IDocumentService documentService, IWebHostEnvironment env)
    {
        _documentService = documentService;
        _env = env;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetAllDocuments()
    {
        var documents = _documentService.GetAllDocuments().Data;
        return Json(new { data = documents });
    }

    public IActionResult CreateDocument()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UploadAndCreateDocument(DocumentViewModel model, IFormFile documentFile)
    {
        string path = string.Empty;
        await _documentService.UploadAndCreateDocument(model, documentFile, Directory.GetCurrentDirectory(), p => path = p);
        return RedirectToAction("Index", new { lessonId = model.LessonId });
    }

    public IActionResult GetDocumentsByLessonId(string lessonId)
    {
        var docs = _documentService.GetDocumentsByLessonId(lessonId);
        return Json(new { data = docs });
    }

    public async Task<IActionResult> DownloadDocument(string id)
    {
        var (stream, contentType, fileName) = await _documentService.GetDocumentStreamById(id, _env.WebRootPath);
        if (stream == null) return NotFound();
        return File(stream, contentType, fileName);
    }
}
