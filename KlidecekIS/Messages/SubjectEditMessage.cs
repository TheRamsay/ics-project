namespace KlidecekIS.Messages;

public record SubjectEditMessage
{
    public required Guid SubjectId { get; set; }
}