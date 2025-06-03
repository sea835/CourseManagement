using CourseManagement.IServices;
using CourseManagement.Models;
using CourseManagement.UnitOfWork;
using CourseManagement.ViewModels;

namespace CourseManagement.Services;

public class VideoService: IVideoService
{
    private readonly IUnitOfWork unitOfWork;

    public VideoService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    
    public ResultViewModel UpdateVideo(Video video)
    {
        try
        {
            var result = unitOfWork.Video.Update(video);
            return ResultViewModel.Success("Video updated successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel CreateVideo(Video video)
    {
        try
        {
            var result = unitOfWork.Video.Add(video);
            return ResultViewModel.Success("Video updated successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel DeleteVideo(string id)
    {
        try
        {
            var result = unitOfWork.Video.Delete(id);
            return ResultViewModel.Success("Video updated successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }

    public ResultViewModel GetVideoById(string id)
    {
        try
        {
            var result = unitOfWork.Video.GetById(id);
            return ResultViewModel.Success("Video updated successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel GetAllVideos()
    {
        try
        {
            var result = unitOfWork.Video.GetAll();
            return ResultViewModel.Success("Video updated successfully");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
}