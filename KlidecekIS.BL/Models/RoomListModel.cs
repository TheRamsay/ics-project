namespace KlidecekIS.BL.Models;

public record RoomListModel: ModelBase
{
    public required string Name { get; set; }

    public ICollection<ActivityListModel> Activites { get; set; } = new List<ActivityListModel>();
    
    public static RoomListModel Empty => new RoomListModel()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Activites = new List<ActivityListModel>()
    };
}