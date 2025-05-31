namespace CourseManagement.Models;

public abstract class BaseModel
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;

    // Gán mặc định khi tạo mới
    public void SetCreated()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = CreatedAt;
        IsDeleted = false;
        IsActive = true;
    }

    // Cập nhật thời gian chỉnh sửa
    public void SetUpdated()
    {
        UpdatedAt = DateTime.Now;
    }

    // Đánh dấu đã xóa (xóa mềm)
    public void SoftDelete()
    {
        IsDeleted = true;
        IsActive = false;
    }

    // Khôi phục đối tượng
    public void Restore()
    {
        IsDeleted = false;
        IsActive = true;
    }
}