using CourseManagement.Core.Models;
using CourseManagement.Core.ViewModels;

namespace CourseManagement.Core.Interfaces.IServices;

public interface IVideoService
{
    public ResultViewModel UpdateVideo(Video video);
    public ResultViewModel CreateVideo(Video video);
    public ResultViewModel DeleteVideo(string id);
    public ResultViewModel GetVideoById(string id);
    public ResultViewModel GetAllVideos();
}