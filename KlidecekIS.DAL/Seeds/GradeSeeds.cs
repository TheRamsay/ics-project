using KlidecekIS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Seeds;

public static class GradeSeeds
{
    public static readonly GradeEntity GradeISS = new()
    {
        Id = Guid.Parse("895ecdb1-68f7-4144-b91c-166ba803e453"),
        Note = "ISS znamka ze cviceni",
        Score = 2.10,
        Activity = ActivitySeeds.ActivityISS,
        ActivityId = ActivitySeeds.ActivityISS.Id,
        StudentId = StudentSeeds.Student.Id,
        Student = StudentSeeds.Student,
    };
    
    public static readonly GradeEntity GradeIFJ = new()
    {
        Id = Guid.Parse("895ecdb1-68f7-1144-b91c-166ba803e453"),
        Note = "IFJ znamka ze zkousky",
        Score = 50.5,
        Activity = ActivitySeeds.ActivityIFJ,
        ActivityId = ActivitySeeds.ActivityIFJ.Id,
        StudentId = StudentSeeds.Student.Id,
        Student = StudentSeeds.Student,
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GradeEntity>().HasData(
            GradeISS with { Activity = null!, Student = null! }
        );
    }
}