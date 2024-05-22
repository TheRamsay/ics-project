using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class StudentSubjectEditViewModel(
        IStudentSubjectFacade studentSubjectFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : ViewModelBase(messengerService)
{
        public Guid Id { get; set; }
        
        public List<SubjectListModel> Subjects { get; set; } = null!;
        
        protected override async Task LoadDataAsync()
        {
                await base.LoadDataAsync();
                
                Subjects = await studentSubjectFacade.GetSubjectsForEnrollment(Id);
        }
        
        [RelayCommand]
        private async Task EnrollSubjectAsync(Guid subjectId)
        {
                await studentSubjectFacade.EnrollStudentToSubject(Id, subjectId);
                
                await LoadDataAsync();
                
                messengerService.Send(new StudentEditMessage() { StudentId = Id });
        }
        
}