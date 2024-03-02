namespace KlidecekIS.DAL.Entities;

public record StudentEntity: EntityBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string ImageUrl { get; set; }

    public required ICollection<SubjectEntity> Subjects { get; set; } = new List<SubjectEntity>();
}