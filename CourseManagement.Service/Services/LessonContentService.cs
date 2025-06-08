// CourseManagement.Service/Services/LessonContentService.cs
using CourseManagement.Core.Interfaces.IServices;
using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;
using CourseManagement.Data.UnitOfWork;

namespace CourseManagement.Service.Services
{
    public class LessonContentService(IUnitOfWork unitOfWork) : ILessonContentService
    {
        public ContentViewModel GetContentByLessonId(string id)
        {
            // Get content for the lesson
            var content = unitOfWork.Content.BuildQuery(c => c.LessonId == id).FirstOrDefault();
            
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
            var content = unitOfWork.Content.BuildQuery(l => l.LessonId == id).FirstOrDefault();
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
        
        public ResultViewModel CreateContent(ContentViewModel content)
        {
            var newContent = new Content
            {
                MainContent = content.MainContent,
                Summary = content.Summary,
                LessonId = content.LessonId,
                ContentId = content.ContentId
            }; 
            unitOfWork.Content.Add(newContent);

            return ResultViewModel.Success("Content created successfully.", newContent);
        }
        
        public ResultViewModel UpdateContent(ContentViewModel content)
        {
            var existingContent = unitOfWork.Content.GetById(content.ContentId);
            if (existingContent == null)
                return ResultViewModel.Fail("Content not found.");

            existingContent.MainContent = content.MainContent;
            existingContent.Summary = content.Summary;
            existingContent.LessonId = content.LessonId;

            unitOfWork.Content.Update(existingContent);
            return ResultViewModel.Success("Content updated successfully.", existingContent);
        }
        
        public ResultViewModel DeleteContent(string id)
        {
            var content = unitOfWork.Content.GetById(id);
            if (content == null)
                return ResultViewModel.Fail("Content not found.");

            unitOfWork.Content.Delete(id);
            return ResultViewModel.Success("Content deleted successfully.");
        }
        
        public ResultViewModel UpdateSummary(string id, string summary)
        {
            var content = unitOfWork.Content.GetById(id);
            if (content == null)
                return ResultViewModel.Fail("Content not found.");

            content.Summary = summary;
            unitOfWork.Content.Update(content);
            return ResultViewModel.Success("Summary updated successfully.");
        }

        public ResultViewModel UpdateMainContent(string id, string mainContent)
        {
            var content = unitOfWork.Content.GetById(id);
            if (content == null)
                return ResultViewModel.Fail("Content not found.");

            content.MainContent = mainContent;
            unitOfWork.Content.Update(content);
            return ResultViewModel.Success("Main content updated successfully.");
        }
    }
}