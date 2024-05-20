using KlidecekIS.BL.Facades;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Mappers;
using KlidecekIS.BL.Models;
using KlidecekIS.Common.Tests;
using KlidecekIS.Common.Tests.Seeds;
using KlidecekIS.DAL.Entities;
using Xunit;
using Xunit.Abstractions;

namespace KlidecekIS.BL.Tests;

[Collection("Sequential")]
public class SubjectFacadeTests : FacadeTestsBase
{
    private readonly ISubjectFacade _subjectFacadeSut;

    public SubjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _subjectFacadeSut = new SubjectFacade(Mapper, UnitOfWorkFactory);
    }

    // Search should be case insensitive
    [Fact]
    public async Task SearchSubjectByNameUpperCase_SubjectEntity()
    {
        // Arrange and Act
        var searchResults = await _subjectFacadeSut.SearchSubjectByName(SubjectSeeds.SubjectEntity.Name.ToUpper());
        var subject = searchResults.Single(i => i.Id == SubjectSeeds.SubjectEntity.Id);
        // Assert
        DeepAssert.Equal(SubjectSeeds.SubjectEntity, Mapper.Map<SubjectEntity>(subject) with {Activities = new List<ActivityEntity>()});
    }
    
    // Search should be case insensitive
    [Fact]
    public async Task SearchSubjectByNameLowerCase_SubjectEntity()
    {
        // Arrange and Act
        var searchResults = await _subjectFacadeSut.SearchSubjectByName(SubjectSeeds.SubjectEntity.Name.ToLower());
        var subject = searchResults.Single(i => i.Id == SubjectSeeds.SubjectEntity.Id);
        // Assert
        DeepAssert.Equal(SubjectSeeds.SubjectEntity, Mapper.Map<SubjectEntity>(subject) with {Activities = new List<ActivityEntity>()});
    }

    [Fact]
    public async Task SearchNonExistingSubjectByName_EmptyList()
    {
        // Arrange and Act
        var searchResults = await _subjectFacadeSut.SearchSubjectByName("SubjectSeeds.SubjectEntity.Name");
        
        // Assert
        Assert.Empty(searchResults);
    }

    [Fact]
    public async Task CreateNewSubject_SubjectCreated()
    {
        // Arrange
        var newSubject = new SubjectDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "Formalni jazyky a prekladace",
            Short = "IFJ"
        };

        // Act
        var dbSubject = await _subjectFacadeSut.SaveAsync(newSubject);

        // Assert
        var subject = await _subjectFacadeSut.GetAsync(dbSubject.Id);
        Assert.NotNull(subject);
        DeepAssert.Equal(dbSubject, Mapper.Map<SubjectDetailModel>(subject));
    }

    [Fact]
    public async Task DeleteExistingSubject_SubjectDeleted()
    {
        // Arrange
        await _subjectFacadeSut.DeleteAsync(SubjectSeeds.SubjectEntity.Id);
        
        // Assert & Act
        var subject = await _subjectFacadeSut.GetAsync(SubjectSeeds.SubjectEntity.Id);
        Assert.Null(subject);
    }

    [Fact]
    public async Task DeleteNonExistingSubject_Throws()
    {
        // Arrange, Assert & Act
        await Assert.ThrowsAnyAsync<Exception>(async () => await _subjectFacadeSut.DeleteAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task UpdateSubject_SubjectUpdated()
    {
        // Arrange
        var subjectModified = new SubjectDetailModel()
        {
            Id = SubjectSeeds.SubjectEntity.Id,
            Name = SubjectSeeds.SubjectEntity.Name,
            Short = "ASD"
        };

        // Act
        await _subjectFacadeSut.SaveAsync(subjectModified);

        // Assert
        var subject = await _subjectFacadeSut.GetAsync(subjectModified.Id);
        DeepAssert.Equal(subjectModified, Mapper.Map<SubjectDetailModel>(subject));
    }

    [Fact]
    public async Task GetExistingSubjectById_SubjectEntity()
    {
        // Arrange & Act
        var subject = await _subjectFacadeSut.GetAsync(SubjectSeeds.SubjectEntity.Id);
        
        // Assert
        Assert.NotNull(subject);
        DeepAssert.Equal(SubjectSeeds.SubjectEntity, Mapper.Map<SubjectEntity>(subject));
    }

    [Fact]
    public async Task GetNonExistingSubjectById_DoesNotThrow()
    {
        // Arrange & Act
        var subject = await _subjectFacadeSut.GetAsync(Guid.NewGuid());
        
        // Assert
        Assert.Null(subject);
    }

    [Fact]
    public async Task GetAllSubjects_SubjectEntity()
    {
        // Arrange & Act
        var subjects = await _subjectFacadeSut.GetAsync();

        // Assert
        Assert.NotNull(subjects);
        Assert.NotEmpty(subjects);

        var subject = subjects.Single(i => i.Id == SubjectSeeds.SubjectEntity.Id);
        DeepAssert.Equal(SubjectSeeds.SubjectEntity, Mapper.Map<SubjectEntity>(subject));
    }

    [Fact]
    public async Task SortSubjectDescending_DescendingList()
    {
        // Arrange & Act
        var searchResults = await _subjectFacadeSut.SortBy(p => p.Short, false);
        
        // Assert
        Assert.Equal(SubjectSeeds.SubjectEntity.Short, searchResults.Last().Short);
    }

    [Fact]
    public async Task SortShortAscendingTest_SortedList()
    {
        // Arrange & Act
        var searchResults = await _subjectFacadeSut.SortBy(p => p.Short, true);
        
        // Assert
        Assert.Equal(SubjectSeeds.SubjectEntity.Short, searchResults.First().Short);
    }
}