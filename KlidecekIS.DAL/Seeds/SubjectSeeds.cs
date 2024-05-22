using KlidecekIS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Seeds;

public static class SubjectSeeds
{
    public static readonly SubjectEntity SubjectISS = new()
    {
        Id = Guid.Parse("57cea729-7930-43e7-b280-831c03bc2e06"),
        Name = "Signaly a Systemy",
        Short = "ISS"
    };
    
    public static readonly SubjectEntity SubjectIFJ = new()
    {
        Id = Guid.Parse("57cea729-7910-42e7-b280-831c03bc2e06"),
        Name = "Informacni a jazykove struktury",
        Short = "IFJ"
    };
    
    public static readonly SubjectEntity SubjectIPA = new()
    {
        Id = Guid.Parse("57cea729-1130-43e7-b280-831c03bc2e06"),
        Name = "Advanced Assembler Programming",
        Short = "IPA"
    };
    
    public static void LoadLists()
    {
        SubjectISS.Activities.Add(ActivitySeeds.ActivityEntity);
        SubjectISS.Students.Add(StudentSubjectSeeds.StudentSubjectEntity1);
    }
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SubjectEntity>().HasData(
            SubjectISS with { Activities = new List<ActivityEntity>(), Students = new List<StudentSubjectEntity>() },
            SubjectIFJ with { Activities = new List<ActivityEntity>(), Students = new List<StudentSubjectEntity>() },
            SubjectIPA with { Activities = new List<ActivityEntity>(), Students = new List<StudentSubjectEntity>() }
        );
    }
}