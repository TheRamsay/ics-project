using KlidecekIS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Seeds;

public static class SubjectSeeds
{
    public static readonly SubjectEntity SubjectISS = new SubjectEntity()
    {
        Id = Guid.Parse("57cea729-7960-43e7-b280-831c03bc2e06"),
        Name = "Signaly a Systemy",
        Short = "ISS",
        Activities = new List<ActivityEntity>(),
        Students = new List<StudentEntity>()
    };
    
    public static readonly SubjectEntity SubjectIFJ = new SubjectEntity()
    {
        Id = Guid.Parse("57cea729-7960-43e7-a280-831c03bc2e06"),
        Name = "Formalni jazyky a Prekladace",
        Short = "IFJ",
        Activities = new List<ActivityEntity>(),
        Students = new List<StudentEntity>()
    };

    static SubjectSeeds()
    {
        SubjectISS.Activities.Add(ActivitySeeds.ActivityISS);
        SubjectISS.Students.Add(StudentSeeds.Student);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SubjectEntity>().HasData(SubjectISS);
    }
}