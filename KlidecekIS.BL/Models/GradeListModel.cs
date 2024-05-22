namespace KlidecekIS.BL.Models;

public record GradeListModel: ModelBase
{
    public double Score { get; set; }
    public required string Note { get; set; }
    
    public required Guid ActivityId { get; set; }
    public ActivityListModel? Activity { get; set; }
    
    public required Guid StudentId { get; set; }
    public StudentListModel? Student { get; set; }
    
    public static GradeListModel Empty => new GradeListModel()
    {
        Id = Guid.Empty,
        Note = string.Empty,
        ActivityId = Guid.Empty,
        StudentId = Guid.Empty,
        Score = 0,
    };
}