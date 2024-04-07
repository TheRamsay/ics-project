using KlidecekIS.Common.Tests;
using KlidecekIS.Common.Tests.Seeds;
using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace KlidecekIS.DAL.Tests;

[Collection("Sequential")]
public class DbContextStudentTests(ITestOutputHelper outputHelper): DbContextTestsBase(outputHelper)
{
   #region CREATE
   [Fact]
   public async Task AddNew_Student_Persisted()
   {
      // Arrange
      var student = StudentSeeds.EmptyStudentEntity with
      {
         Name = "Dominik",
         Surname = "Huml",
      };
      
      // Act
      KlidecekDbContextSUT.Students.Add(student);
      await KlidecekDbContextSUT.SaveChangesAsync();
      
      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var studentFromDb = await dbContext.Students.SingleAsync(s => s.Id == student.Id);
      DeepAssert.Equal(student, studentFromDb);
   }
   #endregion

   #region GET
   
   [Fact]
   public async Task GetById_Student()
   {
      // Arrange
      var student = StudentSeeds.StudentEntity;
      
      // Act
      var studentFromDb = await KlidecekDbContextSUT.Students.SingleAsync(s => s.Id == student.Id);
      
      // Assert
      DeepAssert.Equal(student with { Subjects = new List<StudentSubjectEntity>()}, studentFromDb);
   }
   
   [Fact]
   public async Task GetAll_Students_ContainsSeeded()
   {
      // Arrange
      var students = new List<StudentEntity>
      {
         StudentSeeds.StudentEntity with { Subjects = new List<StudentSubjectEntity>()},
         StudentSeeds.StudentEntityUpdate with { Subjects = new List<StudentSubjectEntity>()},
         StudentSeeds.StudentEntityDelete with { Subjects = new List<StudentSubjectEntity>() }
      };
      
      // Act
      var studentsFromDb = await KlidecekDbContextSUT.Students.ToListAsync();
      
      // Assert
      DeepAssert.Equal(students, studentsFromDb);
   }
   #endregion
   
   #region UPDATE
   
   [Fact]
   public async Task Update_StudentBasic_Persisted()
   {
      // Arrange
      var student = StudentSeeds.StudentEntityUpdate with
      {
         Surname = "Krejza"
      };
      
      // Act
      KlidecekDbContextSUT.Students.Update(student);
      await KlidecekDbContextSUT.SaveChangesAsync();
      
      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var studentFromDb = await dbContext.Students.SingleAsync(s => s.Id == student.Id);
      DeepAssert.Equal(student, studentFromDb);
   }
   
   #endregion
   
   #region DELETE
   
   [Fact]
   public async Task Delete_Student_Deleted()
   {
      // Arrange
      var student = StudentSeeds.StudentEntityDelete;
      
      // Act
      KlidecekDbContextSUT.Students.Remove(student);
      await KlidecekDbContextSUT.SaveChangesAsync();
      
      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var studentFromDb = await dbContext.Students.SingleOrDefaultAsync(s => s.Id == student.Id);
      Assert.Null(studentFromDb);
   }
   
   [Fact]
   public async Task DeleteById_Student_Deleted()
   {
      // Arrange
      var student = StudentSeeds.StudentEntityDelete;
      
      // Act
      KlidecekDbContextSUT.Students.Remove(KlidecekDbContextSUT.Students.Single(s => s.Id == student.Id));
      await KlidecekDbContextSUT.SaveChangesAsync();
      
      // Assert
      await using var dbContext = await DbContextFactory.CreateDbContextAsync();
      var studentFromDb = await dbContext.Students.SingleOrDefaultAsync(s => s.Id == student.Id);
      Assert.Null(studentFromDb);
   }
   
   #endregion
}