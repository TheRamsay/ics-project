using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.Enums;

namespace KlidecekIS.BL.Models;

public record ActivityDetailModel: ModelBase
{
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required ActivityType ActivityType { get; set; }
    public required string Description { get; set; }
    
    public required Guid SubjectId { get; set; }
    public required Guid RoomId { get; set; }
    
    public ICollection<GradeListModel> Grades { get; init; } = new List<GradeListModel>();
    
    public static ActivityDetailModel Empty => new ActivityDetailModel
    {
        Id = Guid.Empty,
        Start = DateTime.Now,
        End = DateTime.Now,
        ActivityType = ActivityType.Exam,
        Description = string.Empty,
        SubjectId = Guid.Empty,
        RoomId = Guid.Empty
    };
}