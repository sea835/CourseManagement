using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.AdminSite.Areas.Video.Controllers
{
    [Area("Video")]
    public class VideoController(IVideoService videoService) : Controller
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

        [HttpGet]
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
            if (videoFile == null || videoFile.Length == 0)
                throw new Exception("No video file provided.");

            // Lưu file
            var fileName = Guid.NewGuid() + Path.GetExtension(videoFile.FileName);
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/videos", fileName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await videoFile.CopyToAsync(stream);
            }

            // Tạo URL và lưu DB
            model.Url = Url.Content($"~/videos/{fileName}");
            model.VideoId = Guid.NewGuid().ToString();
            model.CreatedAt = DateTime.Now;

            await videoService.Create(model);

            TempData["SuccessMessage"] = "Video đã được tải lên và lưu thành công.";
            return RedirectToAction("Index", new { lessonId = model.LessonId });
        }

    }
}