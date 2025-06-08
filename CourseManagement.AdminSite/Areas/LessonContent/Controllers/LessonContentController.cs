using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.AdminSite.Areas.LessonContent.Controllers;

[Area("LessonContent")]
public class LessonContentController(ILessonContentService _contentService) : Controller
{
    public IActionResult GetContentByLessonId(string id)
    {
        var content = _contentService.GetContentByLessonId(id); // Implement this in your service
        if (content == null)
        {
            return Json(new { success = false, message = "Content not found" });
        }

        return Json(content);
    }
}

