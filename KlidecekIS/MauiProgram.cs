using System.Reflection;
using AutoMapper;
using KlidecekIS.BL;
using KlidecekIS.BL.Mappers;
using KlidecekIS.DAL;
using KlidecekIS.DAL.Migrator;
using KlidecekIS.DAL.Options;
using KlidecekIS.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sharpnado.MaterialFrame;

[assembly:System.Resources.NeutralResourcesLanguage("en")]
namespace KlidecekIS;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseSharpnadoMaterialFrame(loggerEnable: false)
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        MaterialFrame.ChangeGlobalTheme(MaterialFrame.Theme.AcrylicBlur);
        
        ConfigureAppSettings(builder);

        builder.Services
            .AddDALServices(GetDALOptions(builder.Configuration))
            .AddAppServices()
            .AddBLServices();
        
            
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<StudentMapperProfile>();
            cfg.AddProfile<RoomMapperProfile>();
            cfg.AddProfile<ActivityMapperProfile>();
            cfg.AddProfile<SubjectMapperProfile>();
            cfg.AddProfile<GradeMapperProfile>();
            cfg.AddProfile<StudentSubjectMapperProfile>();
        });
        
        var mapper = config.CreateMapper();
        builder.Services.AddSingleton(mapper);

        
#if DEBUG
        builder.Logging.AddDebug();
#endif
        var app = builder.Build();
        
        MigrateDb(app.Services.GetRequiredService<IDbMigrator>());
        RegisterRouting(app.Services.GetRequiredService<INavigationService>());

        return app;
    }
    
    private static void ConfigureAppSettings(MauiAppBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder();

        var assembly = Assembly.GetExecutingAssembly();
        const string appSettingsFilePath = "KlidecekIS.appsettings.json";
        using var appSettingsStream = assembly.GetManifestResourceStream(appSettingsFilePath);
        if (appSettingsStream is not null)
        {
            configurationBuilder.AddJsonStream(appSettingsStream);
        }

        var configuration = configurationBuilder.Build();
        builder.Configuration.AddConfiguration(configuration);
    }

    private static void RegisterRouting(INavigationService navigationService)
    {
        foreach (var route in navigationService.Routes)
        {
            Routing.RegisterRoute(route.Route, route.ViewType);
        }
    }

    private static DalOptions GetDALOptions(IConfiguration configuration)
    {
        var dalOptions = new DalOptions()
        {
            DatabaseDirectory = FileSystem.AppDataDirectory
        };

        configuration.GetSection("KlidecekIS:DAL").Bind(dalOptions);
        return dalOptions;
    }

    private static void MigrateDb(IDbMigrator migrator) => migrator.Migrate(); 
}