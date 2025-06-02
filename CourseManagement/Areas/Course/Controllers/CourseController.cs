using CourseManagement.Models;
using CourseManagement.Services;
using CourseManagement.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Course.Controllers;

[Area("Course")]
public class CourseController: Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CourseController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult GetAllCourses()
    {
        var result = _unitOfWork.CourseRepository.GetAll().Data;
        return Json(new { data = result });
    }
    
    public IActionResult CreateCourse()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CreateCourse(Models.Course course)
    {
        course.CourseId = CourseIdGenerator.GenerateCourseId();
        _unitOfWork.CourseRepository.Add(course);
        _unitOfWork.SaveChange();
        TempData["ToastType"] = "success";
        TempData["ToastMessage"] = "Course created successfully!";
        return RedirectToAction("Index");
    }
    
    public IActionResult EditCourse(string id)
    {
        var data = _unitOfWork.CourseRepository.GetById(id);
        if (!data.IsSuccess) return NotFound(data.ErrorMessage);

        var course = data.Data as Models.Course;
        string categoryName = "";
        if (course?.CategoryId != null)
        {
            var category = _unitOfWork.CategoryRepository.GetById(course.CategoryId.ToString());
            if (category.IsSuccess)
            {
                categoryName = ((Models.Category)category.Data).Name;
            }
        }

        var viewModel = new EditCourseViewModel
        {
            CourseId = course.CourseId,
            Title = course.Title,
            Description = course.Description,
            FullDescription = course.FullDescription,
            Level = course.Level,
            Duration = course.Duration,
            Language = course.Language,
            ThumbnailUrl = course.ThumbnailUrl,
            Price = course.Price,
            IsFree = course.IsFree,
            AuthorName = course.AuthorName,
            CategoryId = course.CategoryId,
            CategoryName = categoryName
        };

        return View(viewModel);
    }


    
    [HttpPost]
    public IActionResult EditCourse(Models.Course course)
    {
        // if (!ModelState.IsValid) return View(course);

        // Lấy lại bản ghi gốc từ DB để đảm bảo không ghi đè các trường hệ thống
        var existingData = _unitOfWork.CourseRepository.GetById(course.CourseId);
        if (!existingData.IsSuccess) return NotFound(existingData.ErrorMessage);

        var existingCourse = existingData.Data as Models.Course;
        if (existingCourse == null) return NotFound();

        // Cập nhật các trường được phép sửa từ form
        existingCourse.Title = course.Title;
        existingCourse.Description = course.Description;
        existingCourse.FullDescription = course.FullDescription;
        existingCourse.Level = course.Level;
        existingCourse.Duration = course.Duration;
        existingCourse.Language = course.Language;
        existingCourse.ThumbnailUrl = course.ThumbnailUrl;
        existingCourse.Price = course.Price;
        existingCourse.IsFree = course.IsFree;
        existingCourse.AuthorName = course.AuthorName;
        existingCourse.CategoryId = course.CategoryId;

        // Cập nhật UpdatedAt đúng chuẩn (nếu repo chưa set tự động)
        existingCourse.SetUpdated();

        _unitOfWork.CourseRepository.Update(existingCourse);
        _unitOfWork.SaveChange();
        
        TempData["ToastType"] = "info";
        TempData["ToastMessage"] = "Course updated successfully!";
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult DeleteCourse(string id)
    {
        var data = _unitOfWork.CourseRepository.GetById(id);
        if (!data.IsSuccess) return NotFound(data.ErrorMessage);
        
        _unitOfWork.CourseRepository.Delete(id);
        _unitOfWork.SaveChange();
        
        TempData["ToastType"] = "error";
        TempData["ToastMessage"] = "Course deleted successfully!";
        return RedirectToAction("Index");
    }

}