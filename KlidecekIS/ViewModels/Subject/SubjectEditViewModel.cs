using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels;

[QueryProperty("Subject", "Subject")]
public partial class SubjectEditViewModel(
    ISubjectFacade subjectFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public SubjectDetailModel Subject { get; set; } = SubjectDetailModel.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await subjectFacade.SaveAsync(Subject with { Activities= default!, Students = default! });

        MessengerService.Send(new SubjectEditMessage { SubjectId = Subject.Id });

        navigationService.SendBackButtonPressed();
    }
    
    [RelayCommand]
    private async Task CancelAsync()
    {
        navigationService.SendBackButtonPressed();
    }

    private async Task ReloadDataAsync()
    {
        Subject = await subjectFacade.GetAsync(Subject.Id)
                  ?? SubjectDetailModel.Empty;

    }

    [RelayCommand]
    private async Task GoToCreateActivity(Guid subjectId)
    {
        await navigationService.GoToAsync<SubjectActivityEditViewModel>(
            new Dictionary<string, object?>() { [nameof(SubjectActivityEditViewModel.Subject)] = Subject });
    }
    
    [RelayCommand]
    private async Task GoToEditActivity(ActivityDetailModel activity)
    {
        await navigationService.GoToAsync<SubjectActivityEditViewModel>(
            new Dictionary<string, object?>()
            {
                [nameof(SubjectActivityEditViewModel.Subject)] = Subject,
                [nameof(SubjectActivityEditViewModel.Activity)] = activity
            });
    }
}