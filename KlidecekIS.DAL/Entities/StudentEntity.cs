namespace KlidecekIS.DAL.Entities;

public record StudentEntity: EntityBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<SubjectEntity> Subjects { get; set; } = new List<SubjectEntity>();
}