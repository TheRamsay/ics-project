namespace KlidecekIS.DAL.Entities;

public record SubjectActivityEntity: EntityBase
{
    public Guid ActivityId { get; set; }
    public required ActivityEntity Activity { get; set; }

    public Guid SubjectId { get; set; }
    public required SubjectEntity Subject { get; set; }
}