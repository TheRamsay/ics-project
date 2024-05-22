using KlidecekIS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.Common.Tests.Seeds;

public static class GradeSeeds
{
    public static readonly GradeEntity EmptyGradeEntity = new()
    {
        Id = default,
        Note = default!,
        Score = default,
        Activity = default!,
        ActivityId = default,
        StudentId = default,
        Student = default!
    };

    public static readonly GradeEntity GradeEntity = new()
    {
        Id = Guid.Parse("895ecda1-68f7-4144-b91c-133ba803e413"),
        Note = "ISS znamka ze cviceni",
        Score = 2.10,
        Activity = ActivitySeeds.ActivityEntity,
        ActivityId = ActivitySeeds.ActivityEntity.Id,
        StudentId = StudentSeeds.StudentEntity.Id,
        Student = StudentSeeds.StudentEntity,
    };
    
    public static readonly GradeEntity GradeEntityUpdate = GradeEntity with
    {
        Id = Guid.Parse("895acdb1-68f7-4144-b11c-166ba223e453"), 
        Activity = null!, 
        Student = null!,
        ActivityId = ActivitySeeds.ActivityEntityUpdate.Id, 
        StudentId = StudentSeeds.StudentEntityUpdate.Id,
    };
    
    public static readonly GradeEntity GradeEntityDelete = GradeEntity with
    {
        Id = Guid.Parse("225ecdb1-68b7-2144-a41c-166ba813e453"), 
        Activity = null!, 
        Student = null!,
        ActivityId = ActivitySeeds.ActivityEntityDelete.Id, 
        StudentId = StudentSeeds.StudentEntityDelete.Id,
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GradeEntity>().HasData(
            GradeEntity with { Activity = null!, Student = null! },
            GradeEntityUpdate,
            GradeEntityDelete
        );
    }
}