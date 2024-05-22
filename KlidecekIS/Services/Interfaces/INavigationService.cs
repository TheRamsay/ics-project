using KlidecekIS.Models;
using KlidecekIS.ViewModels;
using KlidecekIS.Views;

namespace KlidecekIS.Services.Interfaces;

public interface INavigationService
{
    IEnumerable<RouteModel> Routes { get; }

#nullable enable
    Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel;
#nullable disable
    
    Task GoToAsync(string route);
    bool SendBackButtonPressed();
#nullable enable
    Task GoToAsync(string route, IDictionary<string, object?> parameters);
#nullable disable

    Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel;
}