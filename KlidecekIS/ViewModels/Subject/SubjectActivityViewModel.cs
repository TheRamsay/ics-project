using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.BL.Facades;
using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.BL.Models;
using KlidecekIS.DAL.Enums;
using KlidecekIS.Messages;
using KlidecekIS.Services.Interfaces;

namespace KlidecekIS.ViewModels;

[QueryProperty(nameof(Subject), nameof(Subject))]
[QueryProperty(nameof(Activity), nameof(Activity))]
public partial class SubjectActivityEditViewModel(
    IActivityFacade activityFacade,
    IRoomFacade roomFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public SubjectDetailModel Subject { get; set; } = SubjectDetailModel.Empty;
    public ActivityDetailModel Activity { get; set; } = ActivityDetailModel.Empty;
    
    public List<string> ActivityTypes { get; set; }
    public string SelectedActivityType { get; set; } = string.Empty;


    public IEnumerable<RoomListModel> Rooms { get; set; } = null!;

    public RoomListModel? SelectedRoom { get; set; } 
    
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    
    public TimeSpan StartTime { get; set; } = DateTime.Now.TimeOfDay;
    public TimeSpan EndTime { get; set; } = DateTime.Now.TimeOfDay;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        
        ActivityTypes = Enum.GetNames(typeof(ActivityType)).ToList();

        Rooms = await roomFacade.GetAsync();

        if (Activity.Id != Guid.Empty)
        {
            SelectedActivityType = Activity.ActivityType.ToString();
            SelectedRoom = Rooms.FirstOrDefault(x => x.Id == Activity.RoomId);
        }
        
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrEmpty(SelectedActivityType) || SelectedRoom is null)
        {
            return;
        }
        
        Activity.ActivityType = Enum.Parse<ActivityType>(SelectedActivityType); 
        Activity.Start = StartDate.Date.Add(StartTime);
        Activity.End = EndDate.Date.Add(EndTime);
        Activity = Activity with { Grades = default!, SubjectId = Subject.Id, RoomId = SelectedRoom.Id };
        
        await activityFacade.SaveAsync(Activity);

        MessengerService.Send(new SubjectEditMessage(){ SubjectId = Subject.Id});

        navigationService.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        navigationService.SendBackButtonPressed();
    }
    
    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Activity.Id != Guid.Empty)
        {
            await activityFacade.DeleteAsync(Activity.Id);
            
            MessengerService.Send(new SubjectEditMessage(){ SubjectId = Subject.Id});
            
            navigationService.SendBackButtonPressed();
        }
    }

}