namespace KlidecekIS.DAL.Entities;

public record StudentEntity: EntityBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<StudentSubjectEntity> Subjects { get; set; } = new List<StudentSubjectEntity>();
    public ICollection<GradeEntity> Grades { get; set; } = new List<GradeEntity>();
}