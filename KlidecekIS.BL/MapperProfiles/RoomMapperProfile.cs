using AutoMapper;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;

namespace KlidecekIS.BL.Mappers;

public class RoomMapperProfile: Profile
{
    public RoomMapperProfile()
    {
        CreateMap<RoomEntity, RoomDetailModel>().ReverseMap();
        CreateMap<RoomEntity, RoomListModel>().ReverseMap();
    }
}