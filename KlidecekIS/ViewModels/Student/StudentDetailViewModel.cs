using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels;

[QueryProperty("Id", "Id")]
public partial class StudentDetailViewModel(
        IStudentFacade studentFacade,
        ISubjectFacade subjectFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : ViewModelBase(messengerService), IRecipient<StudentEditMessage>
{
        public Guid Id { get; set; }
        public StudentDetailModel? Student { get; set; }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Student = await studentFacade.GetAsync(Id);
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (Student is not null)
            {
                await studentFacade.DeleteAsync(Student.Id);
                
                MessengerService.Send(new StudentDeleteMessage());

                navigationService.SendBackButtonPressed();
            }
        }

        [RelayCommand]
        private async Task GoToEditAsync()
        {
            if (Student is not null)
            {
                await navigationService.GoToAsync("/edit",
                    new Dictionary<string, object?> { [nameof(StudentEditViewModel.Student)] = Student });
            }
        }

        public async void Receive(StudentEditMessage message)
        {
            if (message.StudentId == Id)
            {
                await LoadDataAsync();
            }
        }
}