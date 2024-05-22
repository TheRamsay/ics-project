namespace KlidecekIS.BL.Models;

public record StudentSubjectListModel: ModelBase
{
    public Guid Id { get; set; } 
    
    public Guid StudentId { get; set; }
    public required StudentListModel Student { get; set; }
    public Guid SubjectId { get; set; }
    public required SubjectListModel Subject { get; set; }
}