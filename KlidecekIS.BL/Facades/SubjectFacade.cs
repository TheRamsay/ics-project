using AutoMapper;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.BL.Facades;

public class SubjectFacade(IMapper modelMapper, IUnitOfWorkFactory unitOfWorkFactory) :
    FacadeBase<
        SubjectEntity,
        SubjectListModel,
        SubjectDetailModel
    >(modelMapper, unitOfWorkFactory), ISubjectFacade
{
    
    protected override IEnumerable<string> IncludesNavigationPathDetail { get; } = new[]
    {
        nameof(SubjectEntity.Activities),
    };
    
    public async Task<List<SubjectListModel>> SearchSubjectByName(string name)
    {
        var uow = UnitOfWorkFactory.Create();
        var studentRepository = uow.GetRepository<SubjectEntity>();

        return await ModelMapper.ProjectTo<SubjectListModel>(
            studentRepository.Get().Where(s => s.Name.ToLower().Contains(name.ToLower()) || s.Short.ToLower().Contains(name.ToLower()))
        ).ToListAsync();
    }

}