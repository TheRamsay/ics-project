namespace KlidecekIS.BL.Models;

public record StudentSubjectListModel: ModelBase
{
    public Guid Id { get; set; } 
    
    public Guid StudentId { get; set; }
    public Guid SubjectId { get; set; }
}