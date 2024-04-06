namespace KlidecekIS.BL.Models;

public record RoomDetailModel: ModelBase
{
    public required string Name { get; set; }

    public ICollection<ActivityListModel> Activites { get; set; } = new List<ActivityListModel>();
    
    public static RoomDetailModel Empty => new RoomDetailModel()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Activites = new List<ActivityListModel>()
    };
}