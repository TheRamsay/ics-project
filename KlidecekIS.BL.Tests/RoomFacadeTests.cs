using KlidecekIS.BL.Facades;
using KlidecekIS.BL.Models;
using KlidecekIS.Common.Tests;
using KlidecekIS.Common.Tests.Seeds;
using KlidecekIS.DAL.Entities;
using Xunit;
using Xunit.Abstractions;

namespace KlidecekIS.BL.Tests;

[Collection("Sequential")]
public class RoomFacadeTests : FacadeTestsBase
{
    private readonly IRoomFacade _roomFacadeSut;
    
    public RoomFacadeTests(ITestOutputHelper output) : base(output)
    {
        _roomFacadeSut = new RoomFacade(Mapper, UnitOfWorkFactory);
    }
    
    // Search should be case insensitive
    [Fact]
    public async Task SearchRoomByNameUpperCase_RoomEntity()
    {
        // Arrange and Act
        var searchResults = await _roomFacadeSut.SearchRoomByName(RoomSeeds.RoomEntity.Name.ToUpper());
        var room = searchResults.Single(i => i.Id == RoomSeeds.RoomEntity.Id);
        // Assert
        DeepAssert.Equal(Mapper.Map<RoomListModel>(RoomSeeds.RoomEntity) with { Activites = new List<ActivityListModel>() }, room with { Activites = new List<ActivityListModel>()});
    }
    
    // Search should be case insensitive
    [Fact]
    public async Task SearchRoomByNameLowerCase_RoomEntity()
    {
        // Arrange and Act
        var searchResults = await _roomFacadeSut.SearchRoomByName(RoomSeeds.RoomEntity.Name.ToLower());
        var room = searchResults.Single(i => i.Id == RoomSeeds.RoomEntity.Id);
        // Assert
        DeepAssert.Equal(Mapper.Map<RoomListModel>(RoomSeeds.RoomEntity) with { Activites = new List<ActivityListModel>() }, room with { Activites = new List<ActivityListModel>()});
    }
    
    [Fact]
    public async Task SearchNonExistingRoomByName_Empty()
    {
        // Arrange and Act
        var searchResults = await _roomFacadeSut.SearchRoomByName("SubjectSeeds.SubjectEntity.Name");
        // Assert
        Assert.Empty(searchResults);
    }

}