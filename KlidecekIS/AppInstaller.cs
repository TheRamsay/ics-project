using CommunityToolkit.Mvvm.Messaging;
using KlidecekIS.Services;
using KlidecekIS.Services.Interfaces;
using KlidecekIS.ViewModels;
using KlidecekIS.Views;

namespace KlidecekIS;

public static class AppInstaller
{
   public static IServiceCollection AddAppServices(this IServiceCollection services)
   {
      services.AddSingleton<AppShell>();

      services.AddSingleton<IMessenger>(_ => StrongReferenceMessenger.Default);
      services.AddSingleton<IMessengerService, MessengerService>();

      services.AddSingleton<IAlertService, AlertService>();

      services.Scan(selector => selector
         .FromAssemblyOf<App>()
         .AddClasses(filter => filter.AssignableTo<ContentPageBase>())
         .AsSelf()
         .WithTransientLifetime()
      );
      
      services.Scan(selector => selector
         .FromAssemblyOf<App>()
         .AddClasses(filter => filter.AssignableTo<IViewModel>())
         .AsSelf()
         .WithTransientLifetime()
      );

      services.AddTransient<INavigationService, NavigationService>();

      return services;
   }
}