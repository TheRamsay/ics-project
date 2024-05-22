using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels;

public partial class SubjectListViewModel(
    ISubjectFacade subjectFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<SubjectEditMessage>, IRecipient<SubjectDeleteMessage>
{
    public IEnumerable<SubjectListModel> Subjects { get; set; } = null!;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Subjects = await subjectFacade.GetAsync();
        OnPropertyChanged(nameof(Subjects));
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await navigationService.GoToAsync<SubjectDetailViewModel>(
            new Dictionary<string, object?>() { [nameof(SubjectDetailViewModel.Id)] = id });

    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
    }

    public async void Receive(SubjectEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(SubjectDeleteMessage message)
    {
        await LoadDataAsync();
    }
}