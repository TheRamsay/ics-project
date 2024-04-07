using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;

namespace KlidecekIS.BL.Facades.Interfaces;

public interface ISubjectFacade :  IFacade<SubjectEntity, SubjectListModel, SubjectDetailModel>
{
    Task<List<SubjectListModel>> SearchSubjectByName(string name);
}