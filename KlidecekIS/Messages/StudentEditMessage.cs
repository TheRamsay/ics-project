namespace KlidecekIS.Messages;

public record StudentEditMessage
{
    public required Guid StudentId { get; set; }
}