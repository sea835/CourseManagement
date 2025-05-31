using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PersonalExpenseTracker.ViewModels;

public class ResultViewModel
{
    public bool IsSuccess { get; set; }
    public int Code { get; set; }
    public Exception Exception { get; private set; }
    public object Data { get; set; } = null;
    public string ErrorMessage { get; set; }
    public string SuccessMessage { get; set; }
    public string ErrorExceptionMessage { get; set; }
    public string FilePath { get; set; }
    public Dictionary<string, string[]> Errors { get; set; }

    public static ResultViewModel FailWithModelState(ModelStateDictionary modelState)
    {
        return new ResultViewModel()
        {
            IsSuccess = false,
            ErrorMessage = "",
            Errors = modelState?.Where(x => x.Value.Errors.Count > 0).ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                ) ?? null,
            Code = 400,
        };
    }
    public static ResultViewModel Fail(string message)
    {
        return new ResultViewModel()
        {
            IsSuccess = false,
            ErrorMessage = message,
            Code = 400,
        };
    }

    public static ResultViewModel FailException(Exception ex)
    {
        return new ResultViewModel()
        {
            IsSuccess = false,
            ErrorMessage = ex == null ? "Có lỗi đã xảy ra" : ex.Message,
            ErrorExceptionMessage = ex.StackTrace.ToString(),
            Code = 400,
        };
    }

    public static ResultViewModel Success(string message = null)
    {
        return new ResultViewModel()
        {
            IsSuccess = true,
            SuccessMessage = message,
        };
    }

    public static ResultViewModel Success()
    {
        return new ResultViewModel()
        {
            IsSuccess = true
        };
    }

    public static ResultViewModel Success(string message = null, object data = null)
    {
        return new ResultViewModel()
        {
            IsSuccess = true,
            SuccessMessage = message,
            Data = data
        };
    }

    public ResultViewModel()
    {
        Code = 200;
        IsSuccess = true;
    }

    public ResultViewModel(object model)
    {
        Code = 200;
        IsSuccess = true;
        Data = model;
    }

    public ResultViewModel(Exception e)
    {
        Code = 500;
        IsSuccess = false;
        Exception = e;
    }
    public ResultViewModel(string errMsg)
    {
        Code = 400;
        IsSuccess = false;
        Exception = new Exception(errMsg);
    }
    public ResultViewModel(int code, string errMsg)
    {
        Code = code;
        IsSuccess = false;
        Exception = new Exception(errMsg);
    }
}