using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels;

public partial class StudentListViewModel(
        IStudentFacade studentFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<StudentEditMessage>, IRecipient<StudentDeleteMessage>
{
    public IEnumerable<StudentListModel> Students { get; set; } = null!;

    private bool NameSortAscending = true;
    private bool SurnameSortAscending = true;

    public string SearchText { get; set; } = string.Empty;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Students = await studentFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await navigationService.GoToAsync<StudentDetailViewModel>(
            new Dictionary<string, object?>() { [nameof(StudentDetailViewModel.Id)] = id });

    [RelayCommand]
    private async Task AddNewStudent()
    {
        await navigationService.GoToAsync("/edit");
    }

    [RelayCommand]
    public async Task SearchStudents()
    {
        Students = await studentFacade.SearchStudentByName(SearchText);
        OnPropertyChanged(nameof(Students));
    }


    [RelayCommand]
    private async Task SortByName()
    {
        Students = await studentFacade.SortBy(student => student.Name, NameSortAscending);
        NameSortAscending = !NameSortAscending;
    }

    [RelayCommand]
    private async Task SortBySurname()
    {
        Students = await studentFacade.SortBy(student => student.Surname, SurnameSortAscending);
        SurnameSortAscending = !SurnameSortAscending;
    }

    public async void Receive(StudentEditMessage message)
    {
        await LoadDataAsync();
    }
    
    public async void Receive(StudentDeleteMessage message)
    {
        await LoadDataAsync();
    }
}