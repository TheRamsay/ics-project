namespace KlidecekIS.DAL.Entities;

public record RoomEntity : EntityBase
{
    public required string Name { get; set; }

    public ICollection<ActivityEntity> Activites { get; set; } = new List<ActivityEntity>();
}