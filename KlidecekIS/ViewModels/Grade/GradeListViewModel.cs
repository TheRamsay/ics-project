using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels;

public partial class GradeListViewModel(
    IGradeFacade subjectFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<GradeEditMessage>, IRecipient<GradeDeleteMessage>
{
    public IEnumerable<GradeListModel> Grades { get; set; } = null!;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Grades = await subjectFacade.GetAsync();
        OnPropertyChanged(nameof(Grades));
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await navigationService.GoToAsync<GradeDetailViewModel>(
            new Dictionary<string, object?>() { [nameof(GradeDetailViewModel.Id)] = id });

    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
    }

    public async void Receive(GradeEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(GradeDeleteMessage message)
    {
        await LoadDataAsync();
    }
}