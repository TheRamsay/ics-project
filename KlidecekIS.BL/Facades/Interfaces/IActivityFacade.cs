using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;

public interface IActivityFacade :  IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    Task<List<ActivityListModel>> SearchActivityByDescription(string description);
    Task<List<ActivityDetailModel>> FilterSubjectActivitiesByDateRange(Guid subjectId, DateTime startDate, DateTime endDate);
    Task<ActivityDetailModel> EnrollActivityToSubject(Guid activityId, Guid subjectId);
    Task UnEnrollActivityFromSubject(Guid activityId, Guid subjectId);
    Task<List<ActivityListModel>> GetActivitiesForSubject(Guid subjectId);
}