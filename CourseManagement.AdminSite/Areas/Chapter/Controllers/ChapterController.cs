using CourseManagement.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.AdminSite.Areas.Chapter.Controllers;

[Area("Chapter")]
public class ChapterController: Controller
{
    private readonly IChapterService chapterService;
    
    public ChapterController(IChapterService chapterService)
    {
        this.chapterService = chapterService;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult GetAllChapters()
    {
        var chapters = chapterService.GetAllChapters().Data;
        return Json(new { data = chapters });
    }
}