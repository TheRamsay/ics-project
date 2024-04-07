using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;

public interface IActivityFacade :  IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    Task<List<ActivityListModel>> SearchActivityByDescription(string description);
    Task<List<ActivityListModel>> FilterSubjectActivitiesByDateRange(Guid subjectId, DateTime startDate, DateTime endDate);
}