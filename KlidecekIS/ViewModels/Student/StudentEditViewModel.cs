using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels;

[QueryProperty(nameof(Student), nameof(Student))]
public partial class StudentEditViewModel(
    IStudentFacade studentFacade,
    IStudentSubjectFacade studentSubjectFacade,
    IGradeFacade gradeFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<StudentEditMessage>, IRecipient<GradeEditMessage>
{
    public StudentDetailModel Student { get; set; } = StudentDetailModel.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await studentFacade.SaveAsync(Student with { Grades= default!, Subjects = default! });

        MessengerService.Send(new StudentEditMessage(){ StudentId = Student.Id });

        navigationService.SendBackButtonPressed();
    }
    
    public async void Receive(StudentEditMessage message)
    {
        await ReloadDataAsync();
    }
    
    public async void Receive(GradeEditMessage message)
    {
        await ReloadDataAsync();
    }
    
    [RelayCommand]
    private async Task CancelAsync()
    {
        navigationService.SendBackButtonPressed();
    }

    private async Task ReloadDataAsync()
    {
        Student = await studentFacade.GetAsync(Student.Id)
                 ?? StudentDetailModel.Empty;
    }
    
    [RelayCommand]
    private async Task RemoveSubjectAsync(Guid subjectId)
    {
        await studentSubjectFacade.UnEnrollStudentFromSubject(Student.Id, subjectId);
        
        await ReloadDataAsync();
        
        MessengerService.Send(new StudentEditMessage() { StudentId = Student.Id });
    }

    [RelayCommand]
    private async Task GoToEnrollSubjectAsync(Guid subjectId)
    {
        await navigationService.GoToAsync<StudentSubjectEditViewModel>(
            new Dictionary<string, object?>() { [nameof(StudentSubjectEditViewModel.Id)] = Student.Id });
    }
    
    [RelayCommand]
    private async Task EditGradeAsync(GradeDetailModel grade)
    {
        await navigationService.GoToAsync<GradeEditViewModel>(
            new Dictionary<string, object?>()
            {
                [nameof(GradeEditViewModel.Student)] = Student,
                [nameof(GradeEditViewModel.Grade)] = grade
        });
    }
    
    [RelayCommand]
    private async Task GoToAddGradeAsync(StudentDetailModel student)
    {
        await navigationService.GoToAsync("//home/students/edit/grade", new Dictionary<string, object?> {
            [nameof(GradeEditViewModel.Student)] = student
        });
    }
    
    
}