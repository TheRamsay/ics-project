using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels.Room;

[QueryProperty("Id", "Id")]
public partial class RoomDetailViewModel(
    IRoomFacade roomFacade,
    INavigationService navigationService,
    IMessengerService messengerService) : ViewModelBase(messengerService), IRecipient<RoomEditMessage>,
    IRecipient<RoomDeleteMessage>
{
    public Guid Id { get; set; }
    public RoomDetailModel? Room { get; set; }

    public void Receive(RoomDeleteMessage message)
    {
        throw new NotImplementedException();
    }

    public async void Receive(RoomEditMessage message)
    {
        // TODO:

        await LoadDataAsync();
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Room = await roomFacade.GetAsync(Id);
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Room is not null)
        {
            await roomFacade.DeleteAsync(Room.Id);

            MessengerService.Send(new RoomDeleteMessage());

            navigationService.SendBackButtonPressed();
        }
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        if (Room is not null)
            await navigationService.GoToAsync("/edit",
                new Dictionary<string, object?> { [nameof(RoomEditViewModel.Room)] = Room with { } });
    }
}