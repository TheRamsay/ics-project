using KlidecekIS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.Common.Tests.Seeds;

public static class RoomSeeds
{
   public static readonly RoomEntity EmptyRoomEntity = new()
   {
      Id = default,
      Name = default!
   };
   
   public static readonly RoomEntity RoomEntity = new()
   {
      Id = Guid.Parse("e63122e0-c9bf-4c08-b3f6-06d6d3c7bc15"),
      Name = "D105"
   };
   
   public static readonly RoomEntity RoomEntityWithoutActivities = RoomEntity with
   {
      Id = Guid.Parse("0953F1aE-7B1A-48C1-9796-D2BAC7F67868"), 
      Activites = Array.Empty<ActivityEntity>()
   };
   
   public static readonly RoomEntity RoomEntityUpdate = RoomEntity with
   {
      Id = Guid.Parse("1223F3CE-7B1A-48C1-9796-D2BAC7F67868"), 
      Activites = new List<ActivityEntity>()
   };
   
   public static readonly RoomEntity RoomEntityDelete = RoomEntity with
   {
      Id = Guid.Parse("1253F3CE-7B1A-48C1-9796-D3BAC7F61868"), 
      Activites = new List<ActivityEntity>()
   };
   
    public static void LoadLists()
    {
        RoomEntity.Activites.Add(ActivitySeeds.ActivityEntity);
    }

   public static void Seed(this ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<RoomEntity>().HasData(
         RoomEntity with { Activites = Array.Empty<ActivityEntity>() },
         RoomEntityWithoutActivities,
         RoomEntityUpdate,
         RoomEntityDelete
      );
   }
}