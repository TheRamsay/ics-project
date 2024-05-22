namespace KlidecekIS.Messages;

public record RoomEditMessage
{
    public required Guid RoomId { get; set; }
}