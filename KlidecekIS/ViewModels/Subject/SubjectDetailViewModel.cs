using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels;

[QueryProperty("Id", "Id")]
public partial class SubjectDetailViewModel(
    ISubjectFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<SubjectEditMessage>
{
    public Guid Id { get; set; }
    public SubjectDetailModel? Subject { get; set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Subject = await studentFacade.GetAsync(Id);
        OnPropertyChanged(nameof(Subject));
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Subject is not null)
        {
            await studentFacade.DeleteAsync(Subject.Id);

            MessengerService.Send(new SubjectDeleteMessage());

            navigationService.SendBackButtonPressed();
        }
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        if (Subject is not null)
        {
            await navigationService.GoToAsync("/edit",
                new Dictionary<string, object?> { [nameof(SubjectEditViewModel.Subject)] = Subject });
        }
    }

    public async void Receive(SubjectEditMessage message)
    {
        if (message.SubjectId == Id)
        {
            await LoadDataAsync();
        }
    }
}