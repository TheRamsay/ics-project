using KlidecekIS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Seeds;

public static class StudentSubjectSeeds
{
    public static readonly StudentSubjectEntity StudentSubjectEntity1 = new StudentSubjectEntity()
    {
        Id = Guid.Parse("57cea729-7960-43e7-b280-131c03bc2e06"),
        StudentId = StudentSeeds.Student1.Id,
        Student = StudentSeeds.Student1,
        SubjectId = SubjectSeeds.SubjectISS.Id,
        Subject = SubjectSeeds.SubjectISS
    };
    
    public static readonly StudentSubjectEntity StudentSubjectEntity2 = new StudentSubjectEntity()
    {
        Id = Guid.Parse("57cea729-7960-43e7-b280-231c03bc2e01"),
        StudentId = StudentSeeds.Student1.Id,
        Student = StudentSeeds.Student1,
        SubjectId = SubjectSeeds.SubjectIPA.Id,
        Subject = SubjectSeeds.SubjectIPA
    };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentSubjectEntity>().HasData(
            StudentSubjectEntity1 with { Student = null!, Subject = null! },
            StudentSubjectEntity2 with { Student = null!, Subject = null! }
        );
    }
}