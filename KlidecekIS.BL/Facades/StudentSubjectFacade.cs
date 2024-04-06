using AutoMapper;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.BL.Facades;

public class StudentSubjectFacade(IMapper modelMapper, IUnitOfWorkFactory unitOfWorkFactory) : 
    FacadeBase<
        StudentSubjectEntity, 
        StudentSubjectListModel, 
        StudentSubjectDetailModel
    >(modelMapper, unitOfWorkFactory), IStudentSubjectFacade
{
    public async Task<StudentSubjectDetailModel> EnrollStudentToSubject(Guid studentId, Guid subjectId)
    {
        var studentSubjectModel = new StudentSubjectDetailModel()
        {
            StudentId = studentId,
            SubjectId = subjectId
        };
        
        return await SaveAsync(studentSubjectModel);
    }

    public async Task UnEnrollStudentFromSubject(Guid studentId, Guid subjectId)
    {
        var uow = UnitOfWorkFactory.Create();
        var studentSubjectRepository = uow.GetRepository<StudentSubjectEntity>();
        
        var studentSubject = await studentSubjectRepository
            .Get()
            .SingleOrDefaultAsync(ss => ss.StudentId == studentId && ss.SubjectId == subjectId);
        
        if (studentSubject is null)
        {
            throw new InvalidOperationException("Student is not enrolled to this subject.");
        }
        
        await studentSubjectRepository.DeleteAsync(studentSubject.Id);
        await uow.CommitAsync();
    }
}