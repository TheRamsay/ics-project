using CommunityToolkit.Mvvm.Input;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels;

public partial class MainPageViewModel (
        INavigationService navigationService,
        IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    
    [RelayCommand]
    public async Task GoToStudentsAsync()
        => await navigationService.GoToAsync<StudentListViewModel>();

    [RelayCommand]
    public async Task GoToSubjectsAsync()
        => await navigationService.GoToAsync<SubjectListViewModel>();

    [RelayCommand]
    public async Task GoToRoomsAsync()
        => await navigationService.GoToAsync<RoomListViewModel>();
}

