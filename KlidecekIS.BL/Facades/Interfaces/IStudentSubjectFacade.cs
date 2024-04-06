using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;

namespace KlidecekIS.BL.Facades;

public interface IStudentSubjectFacade: IFacade<StudentSubjectEntity, StudentSubjectListModel, StudentSubjectDetailModel>
{
    Task<StudentSubjectDetailModel> EnrollStudentToSubject(Guid studentId, Guid subjectId);
    Task UnEnrollStudentFromSubject(Guid studentId, Guid subjectId);
}