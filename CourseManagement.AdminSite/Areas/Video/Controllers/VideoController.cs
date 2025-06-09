using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.AdminSite.Areas.Video.Controllers
{
    [Area("Video")]
    public class VideoController(IVideoService videoService, IWebHostEnvironment env) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult GetAllVideos()
        {
            var videos = videoService.GetAllVideos().Result;
            return Json(new { data = videos });
        }
        
        public async Task<IActionResult> GetVideoById(string id)
        {
            var video = await videoService.GetById(id);
            if (video == null) return NotFound();
            return Json(video);
        }

        public IActionResult CreateVideo()
        {
            return View();
        }
        
        public async Task<IActionResult> StreamLessonVideo(string id)
        {
            var (fileStream, contentType) = await videoService.GetVideoStreamById(id, env.WebRootPath);

            if (fileStream == null)
                return NotFound();

            return File(fileStream, contentType, enableRangeProcessing: true);
        }
        
        public IActionResult EditVideo(string id)
        {
            var video = videoService.GetById(id).Result;
            if (video == null) return NotFound();
            return View(video);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVideo(VideoViewModel model)
        {
            await videoService.Create(model);
            return Json(new { success = true, message = "Video created." });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVideo(VideoViewModel model)
        {
            await videoService.Update(model);
            return Json(new { success = true, message = "Video updated." });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVideo(string id)
        {
            await videoService.Delete(id);
            return Json(new { success = true, message = "Video deleted." });
        }

        [HttpPost]
        public async Task<IActionResult> RestoreVideo(string id)
        {
            await videoService.Restore(id);
            return Json(new { success = true, message = "Video restored." });
        }
        
        [HttpPost]
        public async Task<IActionResult> UploadAndCreateVideo(VideoViewModel model, IFormFile videoFile)
        {
            string savedUrl = string.Empty;
            await videoService.UploadAndCreateVideo(
                model, 
                videoFile, 
                Directory.GetCurrentDirectory(),
                url => savedUrl = url
            );
            return RedirectToAction("Index", new { lessonId = model.LessonId });
        }

    }
}