using AutoMapper;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;

namespace KlidecekIS.BL.Mappers;

public class SubjectMapperProfile: Profile
{
    public SubjectMapperProfile()
    {
        CreateMap<SubjectEntity, SubjectDetailModel>().ReverseMap();
        CreateMap<SubjectEntity, SubjectListModel>().ReverseMap();
    }
}