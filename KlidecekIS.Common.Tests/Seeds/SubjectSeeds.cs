using KlidecekIS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.Common.Tests.Seeds;

public static class SubjectSeeds
{
    public static readonly SubjectEntity EmptySubjectEntity = new()
    {
        Id = default,
        Name = default!,
        Short = default!
    };
    
    public static readonly SubjectEntity SubjectEntity = new SubjectEntity()
    {
        Id = Guid.Parse("57cea729-7930-43e7-b280-831c03bc2e06"),
        Name = "Signaly a Systemy",
        Short = "ISS"
    };
    
    public static readonly SubjectEntity SubjectEntityUpdate = SubjectEntity with
    {
        Id = Guid.Parse("57cea729-1960-43e7-b280-831a03bc2e06"),
        Activities = new List<ActivityEntity>(),
        Students = new List<StudentEntity>()
    };
    
    public static readonly SubjectEntity SubjectEntityDelete = SubjectEntity with
    {
        Id = Guid.Parse("57cea729-7960-23e7-b280-831c02bc2e06"),
        Activities = new List<ActivityEntity>(),
        Students = new List<StudentEntity>()
    };
    
    public static void LoadLists()
    {
    }
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SubjectEntity>().HasData(
            SubjectEntity with { Activities = new List<ActivityEntity>(), Students = new List<StudentEntity>() },
            SubjectEntityUpdate,
            SubjectEntityDelete
        );
    }
}