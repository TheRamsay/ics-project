using KlidecekIS.DAL.Enums;

namespace KlidecekIS.DAL.Entities;

public class ActivityEntity : IEntity
{
    public Guid Id { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required ActivityType ActivityType { get; set; }
    public required string Description { get; set; }
    
    public required Guid SubjectId { get; set; }
    public required SubjectEntity Subject { get; set; }
    
    public required Guid GradeId { get; set; }
    public required GradeEntity Grade { get; set; }
    
    public required Guid RoomId { get; set; }
    public required RoomEntity Room { get; set; }
}