using AutoMapper;
using KlidecekIS.BL.Extensions;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;

namespace KlidecekIS.BL.Mappers;

public class GradeMapperProfile: Profile
{
    public GradeMapperProfile()
    {
        CreateMap<GradeEntity, GradeDetailModel>();
        CreateMap<GradeEntity, GradeListModel>();

        CreateMap<GradeDetailModel, GradeEntity>()
            .Ignore(x => x.Activity)
            .Ignore(x => x.Student);
        CreateMap<GradeListModel, GradeEntity>()
            .Ignore(x => x.Activity)
            .Ignore(x => x.Student)
            .Ignore(x => x.ActivityId)
            .Ignore(x => x.StudentId);
    }
}