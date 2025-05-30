using CourseManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Document.Controllers;

[Area("Document")]
public class DocumentController: Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public DocumentController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult GetAllDocuments()
    {
        var documents = _unitOfWork.DocumentRepository.GetAll();
        return Json(new { data = documents });
    }
}