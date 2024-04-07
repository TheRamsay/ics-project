using KlidecekIS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Seeds;

public static class StudentSeeds
{
    public static readonly StudentEntity Student = new()
    {
        Id = Guid.Parse("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
        ImageUrl = "https://ibb.co/wCJZCvj",
        Name = "Dominik",
        Surname = "Huml",
        Subjects = new List<StudentSubjectEntity>()
    };

    static StudentSeeds()
    {
        // Student.Subjects.Add(SubjectSeeds.SubjectISS);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentEntity>().HasData(Student);
    }
}