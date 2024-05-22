using CommunityToolkit.Mvvm.Input;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels;

[QueryProperty("Room", "Room")]
public partial class RoomEditViewModel(
    IRoomFacade roomFacade,
    INavigationService navigationService,
    IMessengerService messengerService) : ViewModelBase(messengerService)
{
    public RoomDetailModel Room { get; set; } = RoomDetailModel.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await roomFacade.SaveAsync(Room);

        MessengerService.Send(new RoomEditMessage(){ RoomId = Room.Id });

        navigationService.SendBackButtonPressed();
    }
    
    [RelayCommand]
    private async Task CancelAsync()
    {
        navigationService.SendBackButtonPressed();
    }

    private async Task ReloadDataAsync()
    {
        Room = await roomFacade.GetAsync(Room.Id) ?? RoomDetailModel.Empty;
    }
    
}