namespace KlidecekIS.DAL.Entities;

public record SubjectEntity : EntityBase
{
    public required string Name { get; set; }
    public required string Short { get; set; }

    public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
    public ICollection<StudentSubjectEntity>? Students { get; set; } = new List<StudentSubjectEntity>();
}