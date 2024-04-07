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

public class SubjectFacadeTests : FacadeTestsBase
{
    private readonly ISubjectFacade _subjectFacadeSut;

    public SubjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _subjectFacadeSut = new SubjectFacade(Mapper, UnitOfWorkFactory);
    }

    [Fact]
    public async Task SearchSubjectByNameOk()
    {
        // Arrange and Act
        var searchResults = await _subjectFacadeSut.SearchSubjectByName(SubjectSeeds.SubjectEntity.Name);
        var subject = searchResults.Single(i => i.Id == SubjectSeeds.SubjectEntity.Id);
        // Assert
        DeepAssert.Equal(SubjectSeeds.SubjectEntity, Mapper.Map<SubjectEntity>(subject) with {Activities = new List<ActivityEntity>()});
        
        // Search should be case insensitive
        // Arrange and Act
        searchResults = await _subjectFacadeSut.SearchSubjectByName(SubjectSeeds.SubjectEntity.Name.ToLower());
        subject = searchResults.Single(i => i.Id == SubjectSeeds.SubjectEntity.Id);
        // Assert
        DeepAssert.Equal(SubjectSeeds.SubjectEntity, Mapper.Map<SubjectEntity>(subject) with {Activities = new List<ActivityEntity>()});
    }

    [Fact]
    public async Task SearchNonExistingSubjectByName()
    {
        // Arrange and Act
        var searchResults = await _subjectFacadeSut.SearchSubjectByName("SubjectSeeds.SubjectEntity.Name");
        
        // Assert
        Assert.Empty(searchResults);
    }

    [Fact]
    public async Task CreateNewSubjectOk()
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
    public async Task DeleteExistingSubject()
    {
        // Arrange
        await _subjectFacadeSut.DeleteAsync(SubjectSeeds.SubjectEntity.Id);
        
        // Assert & Act
        var subject = await _subjectFacadeSut.GetAsync(SubjectSeeds.SubjectEntity.Id);
        Assert.Null(subject);
    }

    [Fact]
    public async Task DeleteNonExistingSubjectThrow()
    {
        // Arrange, Assert & Act
        await Assert.ThrowsAnyAsync<Exception>(async () => await _subjectFacadeSut.DeleteAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task UpdateSubject()
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
    public async Task GetExistingSubjectById()
    {
        // Arrange & Act
        var subject = await _subjectFacadeSut.GetAsync(SubjectSeeds.SubjectEntity.Id);
        
        // Assert
        Assert.NotNull(subject);
        DeepAssert.Equal(SubjectSeeds.SubjectEntity, Mapper.Map<SubjectEntity>(subject));
    }

    [Fact]
    public async Task GetNonExistingSubjectByIdNoThrow()
    {
        // Arrange & Act
        var subject = await _subjectFacadeSut.GetAsync(Guid.NewGuid());
        
        // Assert
        Assert.Null(subject);
    }

    [Fact]
    public async Task GetAllSubjects()
    {
        // Arrange & Act
        var subjects = await _subjectFacadeSut.GetAsync();

        // Assert
        Assert.NotNull(subjects);
        Assert.NotEmpty(subjects);

        var subject = subjects.Single(i => i.Id == SubjectSeeds.SubjectEntity.Id);
        DeepAssert.Equal(SubjectSeeds.SubjectEntity, Mapper.Map<SubjectEntity>(subject));
    }
}