namespace KlidecekIS.Messages;

public record GradeEditMessage
{
    public required Guid GradeId { get; set; }
}