using CourseManagement.Models;
using CourseManagement.ViewModels;

namespace CourseManagement.IServices;

public interface IVideoService
{
    public ResultViewModel UpdateVideo(Video video);
    public ResultViewModel CreateVideo(Video video);
    public ResultViewModel DeleteVideo(string id);
    public ResultViewModel GetVideoById(string id);
    public ResultViewModel GetAllVideos();
}