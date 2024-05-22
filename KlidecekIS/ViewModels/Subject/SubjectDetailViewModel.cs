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
    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<SubjectEditMessage>
{
    public Guid Id { get; set; }
    public SubjectDetailModel? Subject { get; set; }

    public IEnumerable<ActivityDetailModel> Activities { get; set; } = new List<ActivityDetailModel>();
    
    public DateTime FilterStartDate { get; set; } = DateTime.Now;
    public DateTime FilterEndDate { get; set; } = DateTime.Now;
    
    public TimeSpan FilterStartTime { get; set; } = DateTime.Now.TimeOfDay;
    public TimeSpan FilterEndTime { get; set; } = DateTime.Now.TimeOfDay;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Subject = await studentFacade.GetAsync(Id);
        
        if (Subject is null)
        {
            navigationService.SendBackButtonPressed();
        }
        
        Activities = Subject.Activities;
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
    
    [RelayCommand]
    private async Task FilterByDateRangeAsync()
    {
        if (Subject is not null)
        {
            var from = FilterStartDate.Date + FilterStartTime;
            var to = FilterEndDate.Date + FilterEndTime;
            Activities = await activityFacade.FilterSubjectActivitiesByDateRange(Subject.Id, from, to);
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