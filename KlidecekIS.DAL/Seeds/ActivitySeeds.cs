using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity ActivityEntity = new()
    {
        Id = Guid.Parse("0fa2e661-1b7a-43a7-8e85-98f0970b5d4a"),
        Description = "ISS Cviceni cislo 2",
        ActivityType = ActivityType.Lab,
        Start = DateTime.Parse("2023-11-21 10:00"),
        End = DateTime.Parse("2023-11-21 11:40"),
        RoomId = RoomSeeds.RoomEntity.Id,
        Room = RoomSeeds.RoomEntity,
        SubjectId = SubjectSeeds.SubjectISS.Id,
        Subject = SubjectSeeds.SubjectISS
    };
    
    public static void LoadLists()
    {
        ActivityEntity.Grades.Add(GradeSeeds.GradeEntity);
    }
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            ActivityEntity with { Grades = new List<GradeEntity>(), Room = null!, Subject = null! }
        );
    }
}