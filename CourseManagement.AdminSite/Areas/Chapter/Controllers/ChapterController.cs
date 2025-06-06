using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.AdminSite.Areas.Chapter.Controllers;

[Area("Chapter")]
public class ChapterController(IChapterService chapterService): Controller
{
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult GetAllChapters()
    {
        var chapters = chapterService.GetAllChapters();
        return Json(new { data = chapters });
    }
    
    public IActionResult CreateChapter()
    {
        return View();
    }
    
    public IActionResult EditChapter(string id)
    {
        var chapter = chapterService.GetChapterById(id);
        return View(chapter);
    }
    
    [HttpPost]
    public IActionResult CreateChapter(Core.Models.Chapter chapter)
    {
        var result = chapterService.CreateChapter(chapter);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult EditChapter(Core.Models.Chapter chapter)
    {
        var result = chapterService.UpdateChapter(chapter);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult DeleteChapter(string id)
    {
        var result = chapterService.DeleteChapter(id);
        return RedirectToAction("Index");
    }
    
    public IActionResult GetChapterById(string id)
    {
        var chapter = chapterService.GetChapterById(id);
        if (chapter == null)
        {
            return NotFound();
        }
        return Json(new { data = chapter });
    }
    
    public IActionResult GetAllChaptersSelect2()
    {
        var chapters = chapterService.GetAllChaptersSelect2().Data;
        return Json(new { data = chapters });
    }
    
    public IActionResult GetAllChaptersByCourseIdSelect2(string courseId)
    {
        var chapters = chapterService.GetAllChaptersByCourseIdSelect2(courseId).Data;
        return Json(new { data = chapters });
    }
}