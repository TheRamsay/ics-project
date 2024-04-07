using KlidecekIS.BL.Facades;
using KlidecekIS.BL.Models;
using KlidecekIS.Common.Tests;
using KlidecekIS.Common.Tests.Seeds;
using KlidecekIS.DAL.Entities;
using Xunit;
using Xunit.Abstractions;

namespace KlidecekIS.BL.Tests;

public class RoomFacadeTests : FacadeTestsBase
{
    private readonly IRoomFacade _roomFacadeSut;
    
    public RoomFacadeTests(ITestOutputHelper output) : base(output)
    {
        _roomFacadeSut = new RoomFacade(Mapper, UnitOfWorkFactory);
    }
    
    [Fact]
    public async Task SearchRoomByNameOk()
    {
        // Arrange and Act
        var searchResults = await _roomFacadeSut.SearchRoomByName(RoomSeeds.RoomEntity.Name);
        var room = searchResults.Single(i => i.Id == RoomSeeds.RoomEntity.Id);
        // Assert
        DeepAssert.Equal(Mapper.Map<RoomListModel>(RoomSeeds.RoomEntity), room);
        
        // Search should be case insensitive
        // Arrange and Act
        searchResults = await _roomFacadeSut.SearchRoomByName(RoomSeeds.RoomEntity.Name.ToLower());
        room = searchResults.Single(i => i.Id == RoomSeeds.RoomEntity.Id);
        // Assert
        DeepAssert.Equal(Mapper.Map<RoomListModel>(RoomSeeds.RoomEntity), room);
    }
    
    [Fact]
    public async Task SearchNonExistingRoomByName()
    {
        // Arrange and Act
        var searchResults = await _roomFacadeSut.SearchRoomByName("SubjectSeeds.SubjectEntity.Name");
        // Assert
        Assert.Empty(searchResults);
    }

}