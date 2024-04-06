using KlidecekIS.DAL.Enums;

namespace KlidecekIS.BL.Models;

public record ActivityListModel: ModelBase
{
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required ActivityType ActivityType { get; set; }
    
    public ICollection<GradeListModel> Grades { get; init; } = new List<GradeListModel>();
    
    public static ActivityListModel Empty => new ActivityListModel
    {
        Id = Guid.Empty,
        Start = DateTime.Now,
        End = DateTime.Now,
        ActivityType = ActivityType.Exam
    };
}