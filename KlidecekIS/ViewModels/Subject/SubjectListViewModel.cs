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
    
    public string SearchText { get; set; } = string.Empty;

    private bool NameSortAscending = true;
    private bool ShortSortAscending = true;

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


    [RelayCommand]
    private async Task AddNewActivity()
    {
        await navigationService.GoToAsync("/edit");
    }

    [RelayCommand]
    public async Task SearchSubjects()
    {
        Subjects = await subjectFacade.SearchSubjectByName(SearchText);
        OnPropertyChanged(nameof(Subjects));
    }

    [RelayCommand]
    private async Task SortByName()
    {
        Subjects = await subjectFacade.SortBy(subject => subject.Name, NameSortAscending);
        NameSortAscending = !NameSortAscending;
    }

    [RelayCommand]
    private async Task SortByShort()
    {
        Subjects = await subjectFacade.SortBy(subject => subject.Short, ShortSortAscending);
        ShortSortAscending = !ShortSortAscending;
    }

    public async void Receive(SubjectEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(SubjectDeleteMessage message)
    {
        await LoadDataAsync();
    }
    
    [RelayCommand]
    private async Task AddNewSubject()
    {
        await navigationService.GoToAsync("/edit");
    } 
}