using KlidecekIS.BL.Facades;
using KlidecekIS.BL.Models;
using KlidecekIS.Common.Tests;
using KlidecekIS.Common.Tests.Seeds;
using KlidecekIS.DAL.Entities;
using Xunit;
using Xunit.Abstractions;

namespace KlidecekIS.BL.Tests;

[Collection("Sequential")]
public class ActivityFacadeTests : FacadeTestsBase
{
    private readonly IActivityFacade _activityFacadeSut;
    
    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {
        _activityFacadeSut = new ActivityFacade(Mapper, UnitOfWorkFactory);
    }
    
    [Fact]
    public async Task SearchActivityByDescriptionOk()
    {
        // Arrange and Act
        var searchResults = await _activityFacadeSut.SearchActivityByDescription(ActivitySeeds.ActivityEntity.Description);
        var activity = searchResults.Single(i => i.Id == ActivitySeeds.ActivityEntity.Id);
        // Assert
        var x = Mapper.Map<ActivityListModel>(ActivitySeeds.ActivityEntity);
        DeepAssert.Equal(Mapper.Map<ActivityListModel>(ActivitySeeds.ActivityEntity) with { Grades = new List<GradeListModel>() }, activity  with { Grades = new List<GradeListModel>() });
        
        // Search should be case insensitive
        // Arrange and Act
        searchResults = await _activityFacadeSut.SearchActivityByDescription(ActivitySeeds.ActivityEntity.Description.ToLower());
        activity = searchResults.Single(i => i.Id == ActivitySeeds.ActivityEntity.Id);
        // Assert
        DeepAssert.Equal(Mapper.Map<ActivityListModel>(ActivitySeeds.ActivityEntity), activity);
    }
    
    [Fact]
    public async Task SearchNonExistingActivityByDescription()
    {
        // Arrange and Act
        var searchResults = await _activityFacadeSut.SearchActivityByDescription("SubjectSeeds.SubjectEntity.Name");
        // Assert
        Assert.Empty(searchResults);
    }

    [Fact]
    public async Task FilterSubjectActivitiesByDateRangeInsideInterval()
    {
        // Arrange
        var startDate = DateTime.Parse("2023-11-20 00:00");
        var endDate = DateTime.Parse("2023-11-22 23:59");
        // Act
        var filterResults =
            await _activityFacadeSut.FilterSubjectActivitiesByDateRange(SubjectSeeds.SubjectEntity.Id, startDate, endDate);
        // Assert
        Assert.Contains(filterResults, a => a.Id == ActivitySeeds.ActivityEntity.Id);
    }

    [Fact]
    public async Task FilterSubjectActivitiesByDateRangeOutsideInterval()
    {
        // Arrange
        var startDate = DateTime.Parse("2023-11-23 00:00");
        var endDate = DateTime.Parse("2023-11-24 23:59");
        // Act
        var filterResults =
            await _activityFacadeSut.FilterSubjectActivitiesByDateRange(SubjectSeeds.SubjectEntity.Id, startDate, endDate);
        // Assert
        Assert.Empty(filterResults);
    }

    [Fact]
    public async Task FilterSubjectActivitiesByDateRangeOverlapInterval()
    {
        // Arrange
        var startDate = DateTime.Parse("2023-11-21 09:00");
        var endDate = DateTime.Parse("2023-11-21 11:00");
        // Act
        var filterResults =
            await _activityFacadeSut.FilterSubjectActivitiesByDateRange(SubjectSeeds.SubjectEntity.Id, startDate, endDate);
        // Assert
        Assert.Empty(filterResults);
    }

}