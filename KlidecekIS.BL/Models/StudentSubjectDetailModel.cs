namespace KlidecekIS.BL.Models;

public record StudentSubjectDetailModel: ModelBase
{
    public Guid Id { get; set; } 
    
    public Guid StudentId { get; set; }
    public Guid SubjectId { get; set; }
}