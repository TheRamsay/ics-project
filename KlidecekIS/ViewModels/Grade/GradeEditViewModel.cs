using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels;

[QueryProperty(nameof(Grade), nameof(Grade))]
[QueryProperty(nameof(Student), nameof(Student))]
public partial class GradeEditViewModel(
    IGradeFacade studentFacade,
    IActivityFacade activityFacade,
    IGradeFacade gradeFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<GradeEditMessage>
{
    public StudentDetailModel Student { get; set; } = StudentDetailModel.Empty;
    public GradeDetailModel Grade { get; set; } = GradeDetailModel.Empty;

    public ActivityListModel? SelectedActivity { get; set; } = null;
    
    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activities = await activityFacade.GetAsync();
        
        if (Grade.Id != Guid.Empty)
        {
            SelectedActivity = Activities.FirstOrDefault(x => x.Id == Grade.ActivityId);
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (SelectedActivity is null)
        {
            return;
        }
        
        Grade.StudentId = Student.Id;
        Grade.ActivityId = SelectedActivity.Id;
        await studentFacade.SaveAsync(Grade);

        MessengerService.Send(new GradeEditMessage { GradeId = Grade.Id });

        navigationService.SendBackButtonPressed();
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
    
    [RelayCommand]
    private async Task RemoveGradeAsync()
    {
        await gradeFacade.DeleteAsync(Grade.Id);
        
        await ReloadDataAsync();
        
        MessengerService.Send(new StudentEditMessage() { StudentId = Student.Id });
        
        navigationService.SendBackButtonPressed();
    }
    
    private async Task ReloadDataAsync()
    {
        Grade = await studentFacade.GetAsync(Grade.Id)
                  ?? GradeDetailModel.Empty;
    }
}