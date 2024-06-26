namespace KlidecekIS.BL.Models;

public record SubjectListModel: ModelBase
{
    public required string Name { get; set; }
    public required string Short { get; set; }

    public ICollection<ActivityListModel> Activities { get; set; } = new List<ActivityListModel>();
    public ICollection<StudentSubjectListModel> Students { get; set; } = new List<StudentSubjectListModel>();
    
    public static SubjectListModel Empty => new SubjectListModel()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Short = string.Empty,
        Activities = new List<ActivityListModel>(),
        Students = new List<StudentSubjectListModel>()
    };
}