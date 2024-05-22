using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels;

[QueryProperty("Id", "Id")]
public partial class GradeDetailViewModel(
    IGradeFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<GradeEditMessage>
{
    public Guid Id { get; set; }
    public GradeDetailModel? Grade { get; set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Grade = await studentFacade.GetAsync(Id);
        OnPropertyChanged(nameof(Grade));
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Grade is not null)
        {
            await studentFacade.DeleteAsync(Grade.Id);

            MessengerService.Send(new GradeDeleteMessage());

            navigationService.SendBackButtonPressed();
        }
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        if (Grade is not null)
        {
            await navigationService.GoToAsync("/edit",
                new Dictionary<string, object?> { [nameof(GradeEditViewModel.Grade)] = Grade });
        }
    }

    public async void Receive(GradeEditMessage message)
    {
        if (message.GradeId == Id)
        {
            await LoadDataAsync();
        }
    }
}