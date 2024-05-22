using KlidecekIS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Seeds;

public static class GradeSeeds
{
    public static readonly GradeEntity GradeEntity = new()
    {
        Id = Guid.Parse("895ecda1-68f7-4144-b91c-133ba803e453"),
        Note = "Dobry vykon.",
        Score = 2.10,
        Activity = ActivitySeeds.ActivityEntity,
        ActivityId = ActivitySeeds.ActivityEntity.Id,
        StudentId = StudentSeeds.Student1.Id,
        Student = StudentSeeds.Student1,
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GradeEntity>().HasData(
            GradeEntity with { Activity = null!, Student = null! }
        );
    }
}