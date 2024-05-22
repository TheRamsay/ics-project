using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels.Activity;

public partial class ActivityListViewModel(
    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService) : ViewModelBase(messengerService), IRecipient<ActivityEditMessage>,
    IRecipient<ActivityDeleteMessage>
{
    public IEnumerable<ActivityListModel> Activitys { get; set; } = null!;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activitys = await activityFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await navigationService.GoToAsync<ActivityDetailViewModel>(
            new Dictionary<string, object?>() { [nameof(ActivityDetailViewModel.Id)] = id });

    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
    }

    public async void Receive(ActivityEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }
}