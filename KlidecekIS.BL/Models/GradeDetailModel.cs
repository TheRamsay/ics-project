namespace KlidecekIS.BL.Models;

public record GradeDetailModel: ModelBase
{
    public double Score { get; set; }
    public required string Note { get; set; }
    
    public required Guid ActivityId { get; set; }
    public required Guid StudentId { get; set; }
    
    public static GradeDetailModel Empty => new GradeDetailModel()
    {
        Id = Guid.Empty,
        Note = string.Empty,
        ActivityId = Guid.Empty,
        StudentId = Guid.Empty
    };
}