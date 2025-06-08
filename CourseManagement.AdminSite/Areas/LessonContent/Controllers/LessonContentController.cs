using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.ViewModels;
using CourseManagement.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.AdminSite.Areas.LessonContent.Controllers;

[Area("LessonContent")]
public class LessonContentController(ILessonContentService contentService) : Controller
{
    public IActionResult GetContentByLessonId(string id)
    {
        var content = contentService.GetContentByLessonId(id); // Implement this in your service
        if (content == null)
        {
            return Json(new { success = false, message = "Content not found" });
        }

        return Json(content);
    }
    
    [HttpPost]
    public IActionResult CreateContent(ContentViewModel content)
    {
        contentService.CreateContent(content);
        return Json(new { success = true, message = "Content created successfully" });
    }
    
    [HttpPost]
    public IActionResult UpdateContent(ContentViewModel content)
    {
        var result = contentService.UpdateContent(content);
        if (result.IsSuccess)
        {
            return Json(new { success = true, message = "Content updated successfully" });
        }
        
        return Json(new { success = false, message = result });
    }
    
    [HttpPost]
    public IActionResult DeleteContent(string id)
    {
        var result = contentService.DeleteContent(id);
        if (result.IsSuccess)
        {
            return Json(new { success = true, message = "Content deleted successfully" });
        }
        
        return Json(new { success = false, message = result });
    }
    
    [HttpPost]
    public IActionResult UpdateSummary(string id, string summary)
    {
        var result = contentService.UpdateSummary(id, summary);
        if (result.IsSuccess)
        {
            return Json(new { success = true, message = "Summary updated successfully" });
        }
        
        return Json(new { success = false, message = result });
    }
    
    [HttpPost]
    public IActionResult UpdateMainContent(string id, string mainContent)
    {
        var result = contentService.UpdateMainContent(id, mainContent);
        if (result.IsSuccess)
        {
            return Json(new { success = true, message = "Main content updated successfully" });
        }
        
        return Json(new { success = false, message = result });
    }
}

