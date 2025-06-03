using CourseManagement.IRepository;
using CourseManagement.Models;

namespace CourseManagement.Repository;

public class DocumentRepository: GenericRepository<Document>, IDocumentRepository
{
    public DocumentRepository(AppDbContext context) : base(context) { }
}