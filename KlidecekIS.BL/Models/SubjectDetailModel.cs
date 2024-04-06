namespace KlidecekIS.BL.Models;

public record SubjectDetailModel: ModelBase
{
    public required string Name { get; set; }
    public required string Short { get; set; }

    public ICollection<ActivityListModel> Activities { get; set; } = new List<ActivityListModel>();
    public ICollection<StudentListModel> Students { get; set; } = new List<StudentListModel>();
    
    public static SubjectDetailModel Empty => new SubjectDetailModel()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Short = string.Empty,
        Activities = new List<ActivityListModel>(),
        Students = new List<StudentListModel>()
    };
}