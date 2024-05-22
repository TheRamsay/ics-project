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
    
    // Search should be case insensitive
    [Fact]
    public async Task SearchStudentByNameUpperCase_StudentEntity()
    {
        // Arrange and Act
        var searchResults = await _studentFacadeSut.SearchStudentByName(StudentSeeds.StudentEntity.Name.ToUpper());
        var student = searchResults.Single(i => i.Id == StudentSeeds.StudentEntity.Id);
        // Assert
        DeepAssert.Equal(Mapper.Map<StudentListModel>(StudentSeeds.StudentEntity), student);
    }
    
    // Search should be case insensitive
    [Fact]
    public async Task SearchStudentByNameLowerCase_StudentEntity()
    {
        // Arrange and Act
        var searchResults = await _studentFacadeSut.SearchStudentByName(StudentSeeds.StudentEntity.Name.ToLower());
        var student = searchResults.Single(i => i.Id == StudentSeeds.StudentEntity.Id);
        // Assert
        DeepAssert.Equal(Mapper.Map<StudentListModel>(StudentSeeds.StudentEntity), student);
    }

    [Fact]
    public async Task SearchNonExistingStudentByName_Empty()
    {
        // Arrange and Act
        var searchResults = await _studentFacadeSut.SearchStudentByName("SubjectSeeds.SubjectEntity.Name");
        // Assert
        Assert.Empty(searchResults);
    }

}