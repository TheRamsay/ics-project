using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.Common.Tests.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity EmptyActivity = new()
    {
        Id = default,
        Description = default!,
        ActivityType = default,
        Start = default,
        End = default,
        RoomId = default,
        Room = default!,
        SubjectId = default,
        Subject = default!,
        Grades = default! 
    };
    
    public static readonly ActivityEntity ActivityEntity = new()
    {
        Id = Guid.Parse("0fa2e661-1a7a-43a7-8e85-98f0970b5d4a"),
        Description = "ISS Cviceni cislo 2",
        ActivityType = ActivityType.Lab,
        Start = DateTime.Parse("2023-11-21 10:00"),
        End = DateTime.Parse("2023-11-21 11:40"),
        RoomId = RoomSeeds.RoomEntity.Id,
        Room = RoomSeeds.RoomEntity,
        SubjectId = SubjectSeeds.SubjectEntity.Id,
        Subject = SubjectSeeds.SubjectEntity
    };
    
    public static readonly ActivityEntity ActivityEntityUpdate = ActivityEntity with
    {
        Id = Guid.Parse("0fa2c661-1a7a-43a7-8e85-98f0970b5d4a"),
        Grades = new List<GradeEntity>(),
        Room = null!,
        Subject = null!,
        RoomId = RoomSeeds.RoomEntityUpdate.Id,
        SubjectId = SubjectSeeds.SubjectEntityUpdate.Id
    };
    
    public static readonly ActivityEntity ActivityEntityDelete = ActivityEntity with
    {
        Id = Guid.Parse("0fa1e661-1a7a-43a7-1a85-18f0970b5d4a"),
        Grades = new List<GradeEntity>(),
        Room = null!,
        Subject = null!,
        RoomId = RoomSeeds.RoomEntityDelete.Id,
        SubjectId = SubjectSeeds.SubjectEntityDelete.Id
    };
    
    public static void LoadLists()
    {
        ActivityEntity.Grades.Add(GradeSeeds.GradeEntity);
    }
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            ActivityEntity with { Grades = new List<GradeEntity>(), Room = null!, Subject = null! },
            ActivityEntityUpdate,
            ActivityEntityDelete
        );
    }
}