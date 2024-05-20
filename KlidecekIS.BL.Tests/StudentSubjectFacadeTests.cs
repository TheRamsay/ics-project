using KlidecekIS.BL.Facades;
using KlidecekIS.BL.Mappers;
using KlidecekIS.BL.Models;
using KlidecekIS.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace KlidecekIS.BL.Tests;

[Collection("Sequential")]
public class StudentSubjectFacadeTests : FacadeTestsBase
{
    private readonly IStudentSubjectFacade _studentSubjectFacadeSut;
    
    public StudentSubjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _studentSubjectFacadeSut = new StudentSubjectFacade(Mapper, UnitOfWorkFactory);
    }

    [Fact]
    public async Task EnrollStudentToSubjectOk()
    {
        // Arrange
        var studentId = StudentSeeds.StudentEntity.Id;
        var subjectId = SubjectSeeds.SubjectEntity.Id;

        // Act
        var savedStudentSubject = await _studentSubjectFacadeSut.EnrollStudentToSubject(studentId, subjectId);

        // Assert
        Assert.NotNull(savedStudentSubject);
        Assert.Equal(studentId, savedStudentSubject.StudentId);
        Assert.Equal(subjectId, savedStudentSubject.SubjectId);
    }
    
    [Fact]
    public async Task EnrollStudentToSubjectInvalidSubject()
    {
        // Arrange
        var studentId = StudentSeeds.StudentEntity.Id;
        var subjectId = Guid.NewGuid();
        
        // Act and Assert
        await Assert.ThrowsAnyAsync<Exception>(() => _studentSubjectFacadeSut.EnrollStudentToSubject(studentId, subjectId));
    }
    
    [Fact]
    public async Task EnrollStudentToSubjectInvalidStudent()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        var subjectId = SubjectSeeds.SubjectEntity.Id;
        
        // Act and Assert
        await Assert.ThrowsAnyAsync<Exception>(() => _studentSubjectFacadeSut.EnrollStudentToSubject(studentId, subjectId));
    }
    
    [Fact] // Expected to throw an exception
    public async Task EnrollStudentToSubjectTwice_Throws()
    {
        // Arrange
        var studentId = StudentSeeds.StudentEntity.Id;
        var subjectId = SubjectSeeds.SubjectEntity.Id;
    
        // Act
        var _ = await _studentSubjectFacadeSut.EnrollStudentToSubject(studentId, subjectId);
    
        // Assert
        await Assert.ThrowsAnyAsync<Exception>(() => _studentSubjectFacadeSut.EnrollStudentToSubject(studentId, subjectId));
    }
    
    [Fact]
    public async Task UnEnrollStudentFromSubjectOk()
    {
        // Arrange
        var studentId = StudentSeeds.StudentEntity.Id;
        var subjectId = SubjectSeeds.SubjectEntity.Id;
        
        // Act and Assert
        await _studentSubjectFacadeSut.EnrollStudentToSubject(studentId, subjectId);
        
        await _studentSubjectFacadeSut.UnEnrollStudentFromSubject(studentId, subjectId);
    }
    
    [Fact]
    public async Task UnEnrollStudentFromSubjectInvalidSubject()
    {
        // Arrange
        var studentId = StudentSeeds.StudentEntity.Id;
        var subjectId = Guid.NewGuid();
        
        // Act and Assert
        await Assert.ThrowsAnyAsync<Exception>(() => _studentSubjectFacadeSut.EnrollStudentToSubject(studentId, subjectId));
    }
    
    [Fact]
    public async Task UnEnrollStudentFromSubjectInvalidStudent()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        var subjectId = SubjectSeeds.SubjectEntity.Id;
        
        // Act and Assert
        await Assert.ThrowsAnyAsync<Exception>(() => _studentSubjectFacadeSut.EnrollStudentToSubject(studentId, subjectId));
    }
    
    [Fact]
    public async Task GetAllStudentSubjectEnrollment()
    {
        // Arrange
        await _studentSubjectFacadeSut.EnrollStudentToSubject( StudentSeeds.StudentEntity.Id, SubjectSeeds.SubjectEntity.Id);
        
        // Act
        var studentSubjectEnrollments = await _studentSubjectFacadeSut.GetAsync();
        
        // Assert
        Assert.NotNull(studentSubjectEnrollments);
        Assert.NotEmpty(studentSubjectEnrollments);
    }
    
}