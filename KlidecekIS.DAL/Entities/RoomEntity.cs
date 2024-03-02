namespace KlidecekIS.DAL.Entities;

public record RoomEntity : EntityBase
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }

    public required ICollection<ActivityEntity> Activites { get; set; } = new List<ActivityEntity>();
}