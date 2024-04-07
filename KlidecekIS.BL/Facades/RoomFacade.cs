using AutoMapper;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.BL.Facades;

public class RoomFacade(IMapper modelMapper, IUnitOfWorkFactory unitOfWorkFactory) :
    FacadeBase<
        RoomEntity,
        RoomListModel,
        RoomDetailModel
    >(modelMapper, unitOfWorkFactory), IRoomFacade
{
    public async Task<List<RoomListModel>> SearchRoomByName(string name)
    {
        var uow = UnitOfWorkFactory.Create();
        var studentRepository = uow.GetRepository<RoomEntity>();

        return await ModelMapper.ProjectTo<RoomListModel>(
            studentRepository.Get().Where(s => s.Name.ToLower().Contains(name.ToLower()))
        ).ToListAsync();
    }

}