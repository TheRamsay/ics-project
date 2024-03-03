using KlidecekIS.Common.Tests;
using KlidecekIS.Common.Tests.Seeds;
using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace KlidecekIS.DAL.Tests;

public class DbContextActivityTests(ITestOutputHelper outputHelper): DbContextTestsBase(outputHelper)
{
   #region CREATE
   [Fact]
   public async Task AddNew_Activity_Persisted()
   {
      // Arrange
      var activity = ActivitySeeds.EmptyActivity with
      {
            Description = "ISS Cviceni cislo 2",
            ActivityType = ActivityType.Lab,
            Start = DateTime.Parse("2023-11-21 10:00"),
            End = DateTime.Parse("2023-11-21 11:40"),
            RoomId = RoomSeeds.RoomEntity.Id,
            SubjectId = SubjectSeeds.SubjectEntity.Id,
            Grades = new List<GradeEntity>(),
      };

      // Act
      KlidecekDbContextSUT.ActivityEntities.Add(activity);
      await KlidecekDbContextSUT.SaveChangesAsync();

      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var activityFromDb = await dbContext.ActivityEntities.SingleAsync(r => r.Id == activity.Id);
      DeepAssert.Equal(activity, activityFromDb);
   }

   #endregion

   #region GET

   [Fact]
   public async Task GetById_Activity()
   {
      // Arrange
      var activity = ActivitySeeds.ActivityEntity;

      // Act
      var activityFromDb = await KlidecekDbContextSUT.ActivityEntities
         .SingleAsync(r => r.Id == activity.Id);

      // Assert
      DeepAssert.Equal(activity with { Grades = new List<GradeEntity>(), Subject = null!, Room = null! }, activityFromDb);
   }

   [Fact]
   public async Task GetAll_Activities_ContainsSeeded()
   {
      // Arrange
      var activities = new List<ActivityEntity>
      {
         ActivitySeeds.ActivityEntity with {Room = null!, Subject = null!, Grades = new List<GradeEntity>() },
         ActivitySeeds.ActivityEntityUpdate with { Room = null!, Subject = null!, Grades = new List<GradeEntity>() },
         ActivitySeeds.ActivityEntityDelete with { Room = null!, Subject = null!, Grades = new List<GradeEntity>() }
      };

      // Act
      var activitiesFromDb = await KlidecekDbContextSUT.ActivityEntities
         .ToListAsync();

      // Assert
      DeepAssert.Equal(activities, activitiesFromDb);
   }
   #endregion

   #region UPDATE

   [Fact]
   public async Task Update_ActivityBasic_Persisted()
   {
      // Arrange
      var activity = ActivitySeeds.ActivityEntityUpdate with
      {
         Description = "Izlo cvika jupi",
      };

      // Act
      KlidecekDbContextSUT.ActivityEntities.Update(activity);
      await KlidecekDbContextSUT.SaveChangesAsync();

      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var activityFromDb = await dbContext.ActivityEntities.SingleAsync(r => r.Id == activity.Id);
      DeepAssert.Equal(activity, activityFromDb);
   }

   // [Fact]
   // public async Task Update_ActivityWithActivity_Persisted()
   // {
   //    // Arrange
   //    var activity = ActivitySeeds.ActivityEntityUpdate with
   //    {
   //       Name = "D107",
   //       Activites = new List<ActivityEntity>()
   //       {
   //          ActivitySeeds.EmptyActivity with
   //          {
   //             Description = "ISS Cviceni cislo 2",
   //             ActivityType = ActivityType.Lab,
   //             Start = DateTime.Parse("2023-11-21 10:00"),
   //             End = DateTime.Parse("2023-11-21 11:40"),
   //             Grades = new List<GradeEntity>(),
   //             Subject = SubjectSeeds.EmptySubjectEntity with
   //             {
   //                Name = "Signaly a Systemy",
   //                Short = "ISS"
   //             },
   //             Activity = ActivitySeeds.EmptyActivityEntity with
   //             {
   //                Name = "D107"
   //             }
   //          }
   //       }
   //    };
   //
   //    // Act
   //    KlidecekDbContextSUT.Activities.Update(activity);
   //    await KlidecekDbContextSUT.SaveChangesAsync();
   //
   //    // Assert
   //    await using var dbContext = await DbContextFactory.CreateDbContextAsync();
   //    var activityFromDb = await dbContext.Activities.Include(r => r.Activites).SingleAsync(r => r.Id == activity.Id);
   //    DeepAssert.Equal(activity, activityFromDb);
   // }

   #endregion

   #region DELETE

   [Fact]
   public async Task Delete_Activity_Deleted()
   {
      // Arrange
      var activity = ActivitySeeds.ActivityEntityDelete;

      // Act
      KlidecekDbContextSUT.ActivityEntities.Remove(activity);
      await KlidecekDbContextSUT.SaveChangesAsync();

      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var activityFromDb = await dbContext.ActivityEntities.SingleOrDefaultAsync(r => r.Id == activity.Id);
      Assert.Null(activityFromDb);
   }

   [Fact]
   public async Task DeleteById_Activity_Deleted()
   {
      // Arrange
      var activity = ActivitySeeds.ActivityEntityDelete;

      // Act
      KlidecekDbContextSUT.ActivityEntities.Remove(KlidecekDbContextSUT.ActivityEntities.Single(r => r.Id == activity.Id));
      await KlidecekDbContextSUT.SaveChangesAsync();

      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var activityFromDb = await dbContext.ActivityEntities.SingleOrDefaultAsync(r => r.Id == activity.Id);
      Assert.Null(activityFromDb);
   }

   #endregion
}