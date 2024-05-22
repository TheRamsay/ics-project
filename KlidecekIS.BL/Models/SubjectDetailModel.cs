namespace KlidecekIS.BL.Models;

public record SubjectDetailModel: ModelBase
{
    public required string Name { get; set; }
    public required string Short { get; set; }

    public ICollection<ActivityDetailModel> Activities { get; set; } = new List<ActivityDetailModel>();
    public ICollection<StudentSubjectListModel> Students { get; set; } = new List<StudentSubjectListModel>();
    
    public static SubjectDetailModel Empty => new SubjectDetailModel()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Short = string.Empty,
        Activities = new List<ActivityDetailModel>(),
        Students = new List<StudentSubjectListModel>()
    };
}