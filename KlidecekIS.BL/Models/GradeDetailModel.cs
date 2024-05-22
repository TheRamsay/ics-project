namespace KlidecekIS.BL.Models;

public record GradeDetailModel: ModelBase
{
    public double Score { get; set; }
    public required string Note { get; set; }
    public required Guid ActivityId { get; set; }
    public ActivityDetailModel? Activity { get; set; }
    
    public required Guid StudentId { get; set; }
    public StudentDetailModel? Student { get; set; }
    
    public static GradeDetailModel Empty => new GradeDetailModel()
    {
        Id = Guid.Empty,
        Note = string.Empty,
        ActivityId = Guid.Empty,
        StudentId = Guid.Empty,
        Score = 0,
    };
}