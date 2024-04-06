using AutoMapper;
using KlidecekIS.BL.Extensions;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;

namespace KlidecekIS.BL.Mappers;

public class StudentMapperProfile: Profile
{
    public StudentMapperProfile()
    {
        CreateMap<StudentEntity, StudentDetailModel>().ReverseMap();
        CreateMap<StudentEntity, StudentListModel>().ReverseMap();
    }
}