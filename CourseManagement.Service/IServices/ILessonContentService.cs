using CourseManagement.Core.ViewModels;

namespace CourseManagement.Service.IServices
{
    public interface ILessonContentService
    {
        public ContentViewModel GetContentByLessonId(string id);
        public ResultViewModel CreateContent(ContentViewModel content);
        public ResultViewModel UpdateContent(ContentViewModel content);
        public ResultViewModel DeleteContent(string id);
        public ResultViewModel UpdateSummary(string id, string summary);
        public ResultViewModel UpdateMainContent(string id, string mainContent);
        
    }
}
