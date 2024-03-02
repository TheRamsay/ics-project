namespace KlidecekIS.DAL.Entities;

public class StudentEntity: IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string ImageUrl { get; set; }

    public required ICollection<SubjectEntity> Subjects { get; set; } = new List<SubjectEntity>();
}