namespace KlidecekIS.DAL.Entities;

public record StudentSubjectEntity: EntityBase
{
    public Guid StudentId { get; set; }
    public required StudentEntity Student { get; set; }
    
    public Guid SubjectId { get; set; }
    public required SubjectEntity Subject { get; set; }
}