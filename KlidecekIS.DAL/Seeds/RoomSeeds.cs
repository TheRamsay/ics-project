using KlidecekIS.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.DAL.Seeds;

public static class RoomSeeds
{
   public static readonly RoomEntity RoomD = new()
   {
      Id = Guid.Parse("e63123e0-c9bf-4c08-b3f6-06d6d3c7bc15"),
      Name = "D105",
      Activites = new List<ActivityEntity>()
   };
   
   public static readonly RoomEntity RoomN = new()
   {
      Id = Guid.Parse("a63123e0-c9bf-4c08-b3f6-06d6d3c7bc15"),
      Name = "N203",
      Activites = new List<ActivityEntity>()
   };

   public static void Seed(this ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<RoomEntity>().HasData(RoomN);
   }
}