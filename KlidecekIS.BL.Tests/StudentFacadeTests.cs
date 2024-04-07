using KlidecekIS.BL.Facades;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Common.Tests;
using KlidecekIS.Common.Tests.Seeds;
using KlidecekIS.DAL.Entities;
using Xunit;
using Xunit.Abstractions;

namespace KlidecekIS.BL.Tests;

[Collection("Sequential")]
public class StudentFacadeTests : FacadeTestsBase
{
    private readonly IStudentFacade _studentFacadeSut;
    
    public StudentFacadeTests(ITestOutputHelper output) : base(output)
    {
        _studentFacadeSut = new StudentFacade(Mapper, UnitOfWorkFactory);
    }
    
    [Fact]
    public async Task SearchStudentByNameOk()
    {
        // Arrange and Act
        var searchResults = await _studentFacadeSut.SearchStudentByName(StudentSeeds.StudentEntity.Name);
        var student = searchResults.Single(i => i.Id == StudentSeeds.StudentEntity.Id);
        // Assert
        DeepAssert.Equal(Mapper.Map<StudentListModel>(StudentSeeds.StudentEntity), student with {Grades = new List<GradeListModel>(), Subjects = new List<StudentSubjectListModel>()});
        
        // Search should be case insensitive
        // Arrange and Act
        searchResults = await _studentFacadeSut.SearchStudentByName(StudentSeeds.StudentEntity.Name.ToLower());
        student = searchResults.Single(i => i.Id == StudentSeeds.StudentEntity.Id);
        // Assert
        DeepAssert.Equal(Mapper.Map<StudentListModel>(StudentSeeds.StudentEntity), student with {Grades = new List<GradeListModel>(), Subjects = new List<StudentSubjectListModel>()});
    }

    [Fact]
    public async Task SearchNonExistingStudentByName()
    {
        // Arrange and Act
        var searchResults = await _studentFacadeSut.SearchStudentByName("SubjectSeeds.SubjectEntity.Name");
        // Assert
        Assert.Empty(searchResults);
    }

}