using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity ActivityISS = new()
    {
        Id = Guid.Parse("0fa2e661-1a7a-43a7-8e85-98f0970b5d4a"),
        Description = "ISS Cviceni cislo 2",
        ActivityType = ActivityType.Lab,
        Start = DateTime.Parse("2023-11-21 10:00"),
        End = DateTime.Parse("2023-11-21 11:40"),
        RoomId = RoomSeeds.RoomN.Id,
        Room = RoomSeeds.RoomN,
        SubjectId = SubjectSeeds.SubjectISS.Id,
        Subject = SubjectSeeds.SubjectISS,
        Grades = null!
    };
    
    public static readonly ActivityEntity ActivityIFJ = new()
    {
        Id = Guid.Parse("0fa2e661-1c7a-43a7-8e85-98f0970b5d4a"),
        Description = "IFJ Zkouska radny termin",
        ActivityType = ActivityType.Lab,
        Start = DateTime.Parse("2024-01-03 08:00"),
        End = DateTime.Parse("2023-01-03 10:00"),
        RoomId = RoomSeeds.RoomD.Id,
        Room = RoomSeeds.RoomD,
        SubjectId = SubjectSeeds.SubjectIFJ.Id,
        Subject = SubjectSeeds.SubjectIFJ,
        Grades = null!
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            ActivityISS with { Grades = null!, Room = null!, Subject = null!}
        );
    }
}