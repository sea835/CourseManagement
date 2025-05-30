using CourseManagement.Models;

namespace CourseManagement.Repository;

public class DocumentRepository: GenericRepository<Document>
{
    public DocumentRepository(AppDbContext context) : base(context) { }
}