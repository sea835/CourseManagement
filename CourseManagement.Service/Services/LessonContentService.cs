// CourseManagement.Service/Services/LessonContentService.cs
using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.ViewModels;
using CourseManagement.Data.UnitOfWork;

namespace CourseManagement.Service.Services
{
    public class LessonContentService : ILessonContentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LessonContentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ContentViewModel GetContentByLessonId(string id)
        {
            // Get content for the lesson
            var content = _unitOfWork.Content.BuildQuery(c => c.LessonId == id).FirstOrDefault();
            
            if (content == null)
                return new ContentViewModel();

            // Create view model
            var contentViewModel = new ContentViewModel
            {
                MainContent = content.MainContent,
                Summary = content.Summary,
                LessonId = content.LessonId,
                ContentId = content.ContentId,
            };

            return contentViewModel;
        }
        
        public ContentViewModel GetById(string id)
        {
            // Get lesson by ID
            var content = _unitOfWork.Content.BuildQuery(l => l.LessonId == id).FirstOrDefault();
            if (content == null)
                return null;

            // Create view model
            var contentViewModel = new ContentViewModel
            {
                MainContent = content.MainContent,
                Summary = content.Summary,
                LessonId = content.LessonId,
                ContentId = content.ContentId,
            };

            return contentViewModel;
        }
    }
}