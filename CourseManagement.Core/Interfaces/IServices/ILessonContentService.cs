using CourseManagement.Core.ViewModels;

namespace CourseManagement.Core.Interfaces.IServices
{
    public interface ILessonContentService
    {
        public ContentViewModel GetContentByLessonId(string id);
    }
}
