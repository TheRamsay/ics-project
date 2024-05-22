using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels.Room;

public partial class RoomListViewModel(
    IRoomFacade roomFacade,
    INavigationService navigationService,
    IMessengerService messengerService) : ViewModelBase(messengerService), IRecipient<RoomEditMessage>,
    IRecipient<RoomDeleteMessage>
{
    public IEnumerable<RoomListModel> Rooms { get; set; } = null!;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Rooms = await roomFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await navigationService.GoToAsync<RoomDetailViewModel>(
            new Dictionary<string, object?>() { [nameof(RoomDetailViewModel.Id)] = id });

    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
    }

    public async void Receive(RoomEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(RoomDeleteMessage message)
    {
        await LoadDataAsync();
    }
}