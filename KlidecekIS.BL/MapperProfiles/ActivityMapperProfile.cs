using AutoMapper;
using KlidecekIS.BL.Extensions;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;

namespace KlidecekIS.BL.Mappers;

public class ActivityMapperProfile: Profile
{
    public ActivityMapperProfile()
    {
        CreateMap<ActivityEntity, ActivityDetailModel>();
        CreateMap<ActivityEntity, ActivityListModel>();

        CreateMap<ActivityDetailModel, ActivityEntity>()
            .Ignore(x => x.Room)
            .Ignore(x => x.Subject);
        CreateMap<ActivityListModel, ActivityEntity>()
            .Ignore(x => x.Room)
            .Ignore(x => x.Subject)
            .Ignore(x => x.Description)
            .Ignore(x => x.SubjectId)
            .Ignore(x => x.RoomId);
    }
}