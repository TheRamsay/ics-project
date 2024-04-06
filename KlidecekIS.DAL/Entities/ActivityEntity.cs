using KlidecekIS.DAL.Enums;

namespace KlidecekIS.DAL.Entities;

public record ActivityEntity : EntityBase
{
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required ActivityType ActivityType { get; set; }
    public required string Description { get; set; }
    
    public required Guid SubjectId { get; set; }
    public SubjectEntity? Subject { get; set; }
    
    public required Guid RoomId { get; set; }
    public RoomEntity? Room { get; set; }
    
    public ICollection<GradeEntity> Grades { get; init; } = new List<GradeEntity>();
}