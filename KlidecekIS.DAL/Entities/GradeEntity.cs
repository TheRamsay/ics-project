namespace KlidecekIS.DAL.Entities;

public class GradeEntity : IEntity
{
    public Guid Id { get; set; }
    public double Score { get; set; }
    public required string Note { get; set; }
    
    public required Guid ActivityId { get; set; }
    public required ActivityEntity Activity { get; set; }
    
    public required Guid StudentId { get; set; }
    public required StudentEntity StudentEntity { get; set; }
}