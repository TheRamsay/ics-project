using KlidecekIS.Common.Tests;
using KlidecekIS.Common.Tests.Seeds;
using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace KlidecekIS.DAL.Tests;

[Collection("Sequential")]
public class DbContextRoomTests(ITestOutputHelper outputHelper): DbContextTestsBase(outputHelper)
{
   #region CREATE
   [Fact]
   public async Task AddNew_Room_Persisted()
   {
      // Arrange
      var room = RoomSeeds.EmptyRoomEntity with
      {
         Name = "D105",
         Activites = new List<ActivityEntity>()
      };
      
      // Act
      KlidecekDbContextSUT.Rooms.Add(room);
      await KlidecekDbContextSUT.SaveChangesAsync();
      
      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var roomFromDb = await dbContext.Rooms.SingleAsync(r => r.Id == room.Id);
      DeepAssert.Equal(room, roomFromDb);
   }
   
   [Fact]
   public async Task AddNew_RoomWithActivitiesWithRoomWithSubject_Persisted()
   {
      // Arrange
      var room = RoomSeeds.EmptyRoomEntity with
      {
         Name = "D105",
         Activites = new List<ActivityEntity>()
         {
            ActivitySeeds.EmptyActivity with
            {
               Description = "ISS Cviceni cislo 2",
               ActivityType = ActivityType.Lab,
               Start = DateTime.Parse("2023-11-21 10:00"),
               End = DateTime.Parse("2023-11-21 11:40"),
               Grades = new List<GradeEntity>(),
               Subject = SubjectSeeds.EmptySubjectEntity with
               {
                  Name = "Signaly a Systemy",
                  Short = "ISS"
               },
               Room = RoomSeeds.EmptyRoomEntity with
               {
                  Name = "D105"
               }
            }
         }
      };

      // Act
      KlidecekDbContextSUT.Rooms.Add(room);
      await KlidecekDbContextSUT.SaveChangesAsync();
      
      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var roomFromDb = await dbContext
         .Rooms
         .Include(r => r.Activites).ThenInclude(a => a.Subject)
         .SingleAsync(r => r.Id == room.Id);
      
      DeepAssert.Equal(room, roomFromDb);
   }
   
   [Fact]
   public async Task AddNew_RoomWithActivities_Persisted()
   {
      // Arrange
      var room = RoomSeeds.EmptyRoomEntity with
      {
         Name = "D105",
         Activites = new List<ActivityEntity>()
         {
            ActivitySeeds.EmptyActivity with
            {
               Description = "ISS Cviceni cislo 2",
               ActivityType = ActivityType.Lab,
               Start = DateTime.Parse("2023-11-21 10:00"),
               End = DateTime.Parse("2023-11-21 11:40"),
               Grades = new List<GradeEntity>(),
               SubjectId = SubjectSeeds.SubjectEntity.Id,
               RoomId = RoomSeeds.RoomEntity.Id
            }
         }
      };
      
      // Act
      KlidecekDbContextSUT.Rooms.Add(room);
      await KlidecekDbContextSUT.SaveChangesAsync();
      
      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var roomFromDb = await dbContext
         .Rooms
         .Include(r => r.Activites)
         .SingleAsync(r => r.Id == room.Id);
      
      DeepAssert.Equal(room, roomFromDb);
   }
   
   #endregion

   #region GET
   
   [Fact]
   public async Task GetById_Room()
   {
      // Arrange
      var room = RoomSeeds.RoomEntity;
      
      // Act
      var roomFromDb = await KlidecekDbContextSUT.Rooms
         .SingleAsync(r => r.Id == room.Id);
      
      // Assert
      DeepAssert.Equal(room with {Activites = new List<ActivityEntity>()}, roomFromDb);
   }
   
   [Fact]
   public async Task GetAll_Rooms_ContainsSeeded()
   {
      // Arrange
      var rooms = new List<RoomEntity>
      {
         RoomSeeds.RoomEntity with { Activites = new List<ActivityEntity>() },
         RoomSeeds.RoomEntityWithoutActivities with { Activites = new List<ActivityEntity>() },
         RoomSeeds.RoomEntityUpdate with { Activites = new List<ActivityEntity>() },
         RoomSeeds.RoomEntityDelete with { Activites = new List<ActivityEntity>() }
      };
      
      // Act
      var roomsFromDb = await KlidecekDbContextSUT.Rooms.ToListAsync();

      // Assert
      DeepAssert.Equal(rooms, roomsFromDb);
   }
   #endregion
   
   #region UPDATE
   
   [Fact]
   public async Task Update_RoomBasic_Persisted()
   {
      // Arrange
      var room = RoomSeeds.RoomEntityUpdate with
      {
         Name = "D106"
      };
      
      // Act
      KlidecekDbContextSUT.Rooms.Update(room);
      await KlidecekDbContextSUT.SaveChangesAsync();
      
      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var roomFromDb = await dbContext.Rooms.SingleAsync(r => r.Id == room.Id);
      DeepAssert.Equal(room, roomFromDb);
   }
   #endregion
   
   #region DELETE
   
   [Fact]
   public async Task Delete_Room_Deleted()
   {
      // Arrange
      var room = RoomSeeds.RoomEntityDelete;
      
      // Act
      KlidecekDbContextSUT.Rooms.Remove(room);
      await KlidecekDbContextSUT.SaveChangesAsync();
      
      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var roomFromDb = await dbContext.Rooms.SingleOrDefaultAsync(r => r.Id == room.Id);
      Assert.Null(roomFromDb);
   }
   
   [Fact]
   public async Task DeleteById_Room_Deleted()
   {
      // Arrange
      var room = RoomSeeds.RoomEntityDelete;
      
      // Act
      KlidecekDbContextSUT.Rooms.Remove(KlidecekDbContextSUT.Rooms.Single(r => r.Id == room.Id));
      await KlidecekDbContextSUT.SaveChangesAsync();
      
      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var roomFromDb = await dbContext.Rooms.SingleOrDefaultAsync(r => r.Id == room.Id);
      Assert.Null(roomFromDb);
   }
   
   #endregion
}