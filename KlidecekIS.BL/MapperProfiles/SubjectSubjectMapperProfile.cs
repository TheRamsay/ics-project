using AutoMapper;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;

namespace KlidecekIS.BL.Mappers;

public class StudentSubjectMapperProfile: Profile
{
    public StudentSubjectMapperProfile()
    {
        CreateMap<StudentSubjectEntity, StudentSubjectDetailModel>().ReverseMap();
        CreateMap<StudentSubjectEntity, StudentSubjectListModel>().ReverseMap();
    }
}