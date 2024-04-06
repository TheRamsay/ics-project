namespace KlidecekIS.BL.Models;

public record StudentListModel: ModelBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<SubjectListModel> Subjects { get; set; } = new List<SubjectListModel>();
    public ICollection<GradeListModel> Grades { get; set; } = new List<GradeListModel>();
    
    public static StudentListModel Empty => new StudentListModel()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Surname = string.Empty,
        ImageUrl = string.Empty,
        Subjects = new List<SubjectListModel>(),
        Grades = new List<GradeListModel>()
    };
}