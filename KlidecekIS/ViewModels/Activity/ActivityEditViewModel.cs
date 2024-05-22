using CommunityToolkit.Mvvm.Input;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels.Activity;

[QueryProperty("Activity", "Activity")]
public partial class ActivityEditViewModel(
    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService) : ViewModelBase(messengerService)
{
    public ActivityDetailModel Activity { get; set; } = ActivityDetailModel.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await activityFacade.SaveAsync(Activity);

        MessengerService.Send(new ActivityEditMessage());

        navigationService.SendBackButtonPressed();
    }

    private async Task ReloadDataAsync()
    {
        Activity = await activityFacade.GetAsync(Activity.Id) ?? ActivityDetailModel.Empty;
    }
}