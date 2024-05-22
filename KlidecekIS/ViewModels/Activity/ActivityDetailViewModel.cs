using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels.Activity;

[QueryProperty("Id", "Id")]
public partial class ActivityDetailViewModel(
    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService) : ViewModelBase(messengerService), IRecipient<ActivityEditMessage>,
    IRecipient<ActivityDeleteMessage>
{
    public Guid Id { get; set; }
    public ActivityDetailModel? Activity { get; set; }

    public void Receive(ActivityDeleteMessage message)
    {
        throw new NotImplementedException();
    }

    public async void Receive(ActivityEditMessage message)
    {
        // TODO:

        await LoadDataAsync();
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activity = await activityFacade.GetAsync(Id);
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Activity is not null)
        {
            await activityFacade.DeleteAsync(Activity.Id);

            MessengerService.Send(new ActivityDeleteMessage());

            navigationService.SendBackButtonPressed();
        }
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        if (Activity is not null)
            await navigationService.GoToAsync("/edit",
                new Dictionary<string, object?> { [nameof(ActivityEditViewModel.Activity)] = Activity with { } });
    }
}