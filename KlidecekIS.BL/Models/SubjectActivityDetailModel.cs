namespace KlidecekIS.BL.Models;

public record SubjectActivityDetailModel: ModelBase
{
    public Guid Id { get; set; }

    public Guid ActivityId { get; set; }
    public required ActivityDetailModel Activity { get; set; }
    public Guid SubjectId { get; set; }

    public required SubjectDetailModel Subject { get; set; }
}