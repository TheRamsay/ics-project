using AutoMapper;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.BL.Facades;

public class StudentFacade(IMapper modelMapper, IUnitOfWorkFactory unitOfWorkFactory) :
    FacadeBase<
        StudentEntity,
        StudentListModel,
        StudentDetailModel
    >(modelMapper, unitOfWorkFactory), IStudentFacade
{
    public async Task<List<StudentListModel>> SearchStudentByName(string name)
    {
        var uow = UnitOfWorkFactory.Create();
        var studentRepository = uow.GetRepository<StudentEntity>();

        return await ModelMapper.ProjectTo<StudentListModel>(
            studentRepository.Get().Where(s => s.Name.ToLower().Contains(name.ToLower()) || s.Surname.ToLower().Contains(name.ToLower()))
        ).ToListAsync();
    }

}