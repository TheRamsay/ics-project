namespace KlidecekIS.DAL.Entities;

public record GradeEntity : EntityBase
{
    public double Score { get; set; }
    public required string Note { get; set; }
    
    public required Guid ActivityId { get; set; }
    public ActivityEntity? Activity { get; set; }
    
    public required Guid StudentId { get; set; }
    public StudentEntity? Student { get; set; }
}