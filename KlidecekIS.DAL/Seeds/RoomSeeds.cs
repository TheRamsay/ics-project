using KlidecekIS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Seeds;

public static class RoomSeeds
{
   public static readonly RoomEntity RoomEntity = new()
   {
      Id = Guid.Parse("e63183e0-c9bf-4c08-b3f6-06d6d3c7bc15"),
      Name = "D105"
   };
   
    public static void LoadLists()
    {
        RoomEntity.Activites.Add(ActivitySeeds.ActivityEntity);
    }

   public static void Seed(this ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<RoomEntity>().HasData(
         RoomEntity with { Activites = Array.Empty<ActivityEntity>() }
      );
   }
}