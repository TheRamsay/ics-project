namespace KlidecekIS.DAL.Entities;

public record RoomEntity : IEntity
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
}