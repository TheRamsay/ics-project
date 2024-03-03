using KlidecekIS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.Common.Tests.Seeds;

public static class StudentSeeds
{
    public static readonly StudentEntity EmptyStudentEntity = new()
    {
        Id = default,
        ImageUrl = default!,
        Name = default!,
        Surname = default!
    };
    
    public static readonly StudentEntity StudentEntity = new()
    {
        Id = Guid.Parse("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
        ImageUrl = "https://ibb.co/wCJZCvj",
        Name = "Dominik",
        Surname = "Huml"
    };
    
    public static readonly StudentEntity StudentEntityUpdate = StudentEntity with
    {
        Id = Guid.Parse("fabde0ca-eefe-443f-baf4-3d96cc2cbf2e"),
        Subjects = new List<SubjectEntity>()
    };
    
    public static readonly StudentEntity StudentEntityDelete = StudentEntity with
    {
        Id = Guid.Parse("facde0cd-aefe-443f-baf6-3d96cc2cbf2e"),
        Subjects = new List<SubjectEntity>()
    };
    
    public static void LoadLists()
    {
        StudentEntity.Subjects.Add(SubjectSeeds.SubjectEntity);
    }
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentEntity>().HasData(
            StudentEntity with { Subjects = new List<SubjectEntity>() },
            StudentEntityUpdate,
            StudentEntityDelete
        );
    }
}