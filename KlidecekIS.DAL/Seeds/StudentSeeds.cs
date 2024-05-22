using KlidecekIS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Seeds;

public static class StudentSeeds
{
    public static readonly StudentEntity Student1 = new()
    {
        Id = Guid.Parse("aabde0cd-eefe-443f-baf6-3d96cc2cbf2a"),
        ImageUrl = "https://static.wikia.nocookie.net/raise-a-floppa-roblox/images/b/b1/Pet_floppa.png/revision/latest?cb=20220514210235",
        Name = "Dominik",
        Surname = "Huml"
    };
    
    public static readonly StudentEntity Student2 = new()
    {
        Id = Guid.Parse("fabde0cd-aabe-443f-baf6-3d16cc2cbf2b"),
        ImageUrl = "https://wallpapers.com/images/hd/fat-cat-pictures-4032-x-3024-u3s4q3q2tlm676db.jpg",
        Name = "Albrecht",
        Surname = "Krejzak"
    };
    
    public static readonly StudentEntity Student3 = new()
    {
        Id = Guid.Parse("fabde0cd-aabe-443f-baf6-3d96cc2caf2b"),
        ImageUrl = "https://www.accountingweb.co.uk/sites/default/files/styles/inline_banner/public/istock_struvictory_fat_cat.jpg?itok=dQr1319I",
        Name = "Bernard",
        Surname = "Zetor"
    };
    
    public static void LoadLists()
    {
        Student1.Subjects.Add(StudentSubjectSeeds.StudentSubjectEntity1);
        Student1.Subjects.Add(StudentSubjectSeeds.StudentSubjectEntity2);
        
        // Student1.Grades.Add(GradeSeeds.GradeEntity);
    }
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentEntity>().HasData(
            Student1 with { Subjects = new List<StudentSubjectEntity>() },
            Student2 with { Subjects = new List<StudentSubjectEntity>() },
            Student3 with { Subjects = new List<StudentSubjectEntity>() }
        );
    }
}