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
    protected override IEnumerable<string> IncludesNavigationPathDetail { get; } = new[]
    {
        nameof(StudentSubjectEntity.Student),
        nameof(StudentSubjectEntity.Subject)
    };
    
    public async Task<List<SubjectListModel>> GetSubjectsForEnrollment(Guid studentId)
    {
        var uow = UnitOfWorkFactory.Create();
        var subjectRepository = uow.GetRepository<SubjectEntity>();
        var studentSubjectRepository = uow.GetRepository<StudentSubjectEntity>();
        
        var studentSubjects = await studentSubjectRepository
            .Get()
            .Where(ss => ss.StudentId == studentId)
            .Select(ss => ss.SubjectId)
            .ToListAsync();

        return modelMapper.ProjectTo<SubjectListModel>(
            subjectRepository.Get()
                .Where(s => !studentSubjects.Contains(s.Id))
        ).ToList();
    }

    public async Task<StudentSubjectDetailModel> EnrollStudentToSubject(Guid studentId, Guid subjectId)
    {
        var studentSubjectModel = new StudentSubjectDetailModel()
        {
            StudentId = studentId,
            Student = null!,
            SubjectId = subjectId,
            Subject = null!
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