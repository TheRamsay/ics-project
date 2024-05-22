using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels;

[QueryProperty("Subject", "Subject")]
public partial class SubjectEditViewModel(
    ISubjectFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<SubjectEditMessage>
{
    public SubjectDetailModel Subject { get; set; } = SubjectDetailModel.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await studentFacade.SaveAsync(Subject with { Activities= default!, Students = default! });

        MessengerService.Send(new SubjectEditMessage(){ SubjectId = Subject.Id });

        navigationService.SendBackButtonPressed();
    }

    public async void Receive(SubjectEditMessage message)
    {
        await ReloadDataAsync();
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        navigationService.SendBackButtonPressed();
    }

    private async Task ReloadDataAsync()
    {
        Subject = await studentFacade.GetAsync(Subject.Id)
                  ?? SubjectDetailModel.Empty;
    }
}