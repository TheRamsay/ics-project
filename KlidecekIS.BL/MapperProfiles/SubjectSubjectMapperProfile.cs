using AutoMapper;
using KlidecekIS.BL.Extensions;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;

namespace KlidecekIS.BL.Mappers;

public class StudentSubjectMapperProfile: Profile
{
    public StudentSubjectMapperProfile()
    {
        CreateMap<StudentSubjectEntity, StudentSubjectDetailModel>().ReverseMap();
        CreateMap<StudentSubjectEntity, StudentSubjectListModel>().ReverseMap();
        // CreateMap<StudentSubjectListModel, SubjectEntity>()
        //     .Ignore(x => x.Activities)
        //     .Ignore(x => x.Short)
        //     .Ignore(x => x.Students);
        // CreateMap<StudentEntity, SubjectDetailModel>().ReverseMap();
    }
}