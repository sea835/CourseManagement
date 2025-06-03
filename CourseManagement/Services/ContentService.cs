using CourseManagement.IServices;
using CourseManagement.Models;
using CourseManagement.UnitOfWork;
using CourseManagement.ViewModels;

namespace CourseManagement.Services;

public class ContentService: IContentService
{
    private readonly IUnitOfWork unitOfWork;

    public ContentService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    
    public ResultViewModel GetContentById(string id)
    {
        try
        {
            var content = unitOfWork.Content.Find(c => c.ContentId == id);
            return ResultViewModel.Success("Get Content Id: " + id + " Success", content);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel UpdateContent(Content content)
    {
        try
        {
            var existingContent = unitOfWork.Content.Find(c => c.ContentId == content.ContentId).First();
            if (existingContent == null)
            {
                return ResultViewModel.Fail("Content not found");
            }
            unitOfWork.Content.Update(existingContent);
            unitOfWork.SaveChange();
            return ResultViewModel.Success("Update Content Id: " + content.ContentId + " Success");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel CreateContent(Content content)
    {
        try
        {
            unitOfWork.Content.Add(content);
            unitOfWork.SaveChange();
            return ResultViewModel.Success("Create New Content Success");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel DeleteContent(string id)
    {
        try
        {
            unitOfWork.Content.Delete(id);
            unitOfWork.SaveChange();
            return ResultViewModel.Success("Delete Content Id: " + id + " Success");
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
    
    public ResultViewModel GetAllContents()
    {
        try
        {
            var contents = unitOfWork.Content.GetAll();
            return ResultViewModel.Success("Get All Contents Success", contents);
        }
        catch (Exception ex)
        {
            return ResultViewModel.FailException(ex);
        }
    }
}