using KlidecekIS.Common.Tests;
using KlidecekIS.Common.Tests.Seeds;
using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace KlidecekIS.DAL.Tests;

[Collection("SequentialTests")]
public class DbContextSubjectTests(ITestOutputHelper outputHelper): DbContextTestsBase(outputHelper)
{
   #region CREATE
   [Fact]
   public async Task AddNew_Subject_Persisted()
   {
      // Arrange
      var subject = SubjectSeeds.EmptySubjectEntity with
      {
         Name = "Signaly a Systemy",
         Short = "ISS",
      };
      
      // Act
      KlidecekDbContextSUT.Subjects.Add(subject);
      await KlidecekDbContextSUT.SaveChangesAsync();
      
      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var subjectFromDb = await dbContext.Subjects.SingleAsync(s => s.Id == subject.Id);
      DeepAssert.Equal(subject, subjectFromDb);
   }
   
   #endregion

   #region GET
   
   [Fact]
   public async Task GetById_Subject()
   {
      // Arrange
      var subject = SubjectSeeds.SubjectEntity;
      
      // Act
      var subjectFromDb = await KlidecekDbContextSUT.Subjects.SingleAsync(s => s.Id == subject.Id);
      
      // Assert
      DeepAssert.Equal(subject with{ Activities = new List<ActivityEntity>(), Students = new List<StudentEntity>() }, subjectFromDb);
   }
   
   [Fact]
   public async Task GetAll_Subjects_ContainsSeeded()
   {
      // Arrange
      var subjects = new List<SubjectEntity>
      {
         SubjectSeeds.SubjectEntity with { Activities = new List<ActivityEntity>(), Students = new List<StudentEntity>() },
         SubjectSeeds.SubjectEntityUpdate with { Activities = new List<ActivityEntity>(), Students = new List<StudentEntity>() },
         SubjectSeeds.SubjectEntityDelete with { Activities = new List<ActivityEntity>(), Students = new List<StudentEntity>() }
      };
      
      // Act
      var subjectsFromDb = await KlidecekDbContextSUT.Subjects.ToListAsync();
      
      // Assert
      DeepAssert.Equal(subjects, subjectsFromDb);
   }
   
   #endregion
   
   #region UPDATE
   
   [Fact]
   public async Task Update_SubjectBasic_Persisted()
   {
      // Arrange
      var subject = SubjectSeeds.SubjectEntityUpdate with
      {
         Name = "Signaly a Systemy",
         Short = "ISS"
      };
      
      // Act
      KlidecekDbContextSUT.Subjects.Update(subject);
      await KlidecekDbContextSUT.SaveChangesAsync();
      
      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var subjectFromDb = await dbContext.Subjects.SingleAsync(s => s.Id == subject.Id);
      DeepAssert.Equal(subject, subjectFromDb);
   }
   
   #endregion
   
   #region DELETE
   
   [Fact]
   public async Task Delete_Subject_Deleted()
   {
      // Arrange
      var subject = SubjectSeeds.SubjectEntityDelete;
      
      // Act
      KlidecekDbContextSUT.Subjects.Remove(subject);
      await KlidecekDbContextSUT.SaveChangesAsync();
      
      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var subjectFromDb = await dbContext.Subjects.SingleOrDefaultAsync(s => s.Id == subject.Id);
      Assert.Null(subjectFromDb);
   }
   
   [Fact]
   public async Task DeleteById_Room_Deleted()
   {
      // Arrange
      var subject = SubjectSeeds.SubjectEntityDelete;
      
      // Act
      KlidecekDbContextSUT.Subjects.Remove(KlidecekDbContextSUT.Subjects.Single(s => s.Id == subject.Id));
      await KlidecekDbContextSUT.SaveChangesAsync();
      
      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var subjectFromDb = await dbContext.Subjects.SingleOrDefaultAsync(s => s.Id == subject.Id);
      Assert.Null(subjectFromDb);
   }
   
   #endregion
}