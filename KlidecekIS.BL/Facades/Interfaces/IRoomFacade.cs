using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;

public interface IRoomFacade :  IFacade<RoomEntity, RoomListModel, RoomDetailModel>
{
    Task<List<RoomListModel>> SearchRoomByName(string name);
}