using AutoMapper;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.BL.Facades;

public class ActivityFacade(IMapper modelMapper, IUnitOfWorkFactory unitOfWorkFactory) :
    FacadeBase<
        ActivityEntity,
        ActivityListModel,
        ActivityDetailModel
    >(modelMapper, unitOfWorkFactory), IActivityFacade
{
    
    protected override IEnumerable<string> IncludesNavigationPathDetail { get; } = new[]
    {
        nameof(ActivityEntity.Subject)
    };
    
    public async Task<List<ActivityListModel>> SearchActivityByDescription(string description)
    {
        var uow = UnitOfWorkFactory.Create();
        var studentRepository = uow.GetRepository<ActivityEntity>();

        return await ModelMapper.ProjectTo<ActivityListModel>(
            studentRepository.Get().Where(s => s.Description.ToLower().Contains(description.ToLower()))
        ).ToListAsync();
    }

    public async Task<List<ActivityListModel>> FilterSubjectActivitiesByDateRange(Guid subjectId, DateTime startDate, DateTime endDate)
    {
        var uow = UnitOfWorkFactory.Create();
        var activityRepository = uow.GetRepository<ActivityEntity>();
        
        return await ModelMapper.ProjectTo<ActivityListModel>(
            activityRepository.Get().Where(a => a.SubjectId == subjectId && a.Start >= startDate && a.End <= endDate)
        ).ToListAsync();
    }
    
}