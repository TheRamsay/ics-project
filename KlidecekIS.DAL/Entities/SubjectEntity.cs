namespace KlidecekIS.DAL.Entities;

public class SubjectEntity : IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Short { get; set; }

    public required ICollection<ActivityEntity> Activites { get; set; } = new List<ActivityEntity>();
    public required ICollection<StudentEntity> Students { get; set; } = new List<StudentEntity>();
}