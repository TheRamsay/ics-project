using AutoMapper;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.Enums;
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

    public async Task<List<ActivityDetailModel>> FilterSubjectActivitiesByDateRange(Guid subjectId, DateTime startDate, DateTime endDate)
    {
        var uow = UnitOfWorkFactory.Create();
        var activityRepository = uow.GetRepository<ActivityEntity>();
        
        return await ModelMapper.ProjectTo<ActivityDetailModel>(
            activityRepository.Get().Where(a => a.SubjectId == subjectId && a.Start >= startDate && a.End <= endDate)
        ).ToListAsync();
    }

    public async Task<ActivityDetailModel> EnrollActivityToSubject(Guid activityId, Guid subjectId)
    {
        var studentSubjectModel = new ActivityDetailModel
        {
            SubjectId = subjectId,
            Start = default,
            End = default,
            ActivityType = ActivityType.Exam,
            Description = null,
            RoomId = default,
        };

        return await SaveAsync(studentSubjectModel);
    }

    public async Task UnEnrollActivityFromSubject(Guid activityId, Guid subjectId)
    {
        var uow = UnitOfWorkFactory.Create();
        var subjectActivityRepository = uow.GetRepository<SubjectActivityEntity>();

        var subjectActivity = await subjectActivityRepository
            .Get()
            .SingleOrDefaultAsync(sa => sa.SubjectId == subjectId && sa.ActivityId == activityId);

        if (subjectActivity is null)
        {
            throw new InvalidOperationException("Student is not enrolled to this subject.");
        }

        await subjectActivityRepository.DeleteAsync(subjectActivity.Id);
        await uow.CommitAsync();
    }

    public async Task<List<ActivityListModel>> GetActivitiesForSubject(Guid subjectId)
    {
        var uow = UnitOfWorkFactory.Create();
        // var subjectRepository = uow.GetRepository<SubjectEntity>();
        var activityRepository = uow.GetRepository<ActivityEntity>();


        var subjectActivityRepository = uow.GetRepository<SubjectActivityEntity>();

        var activities =  activityRepository.Get().Where(a => a.SubjectId == subjectId);

        return modelMapper.ProjectTo<ActivityListModel>(
            activities
        ).ToList();
    }
    
}