using KlidecekIS.BL.Facades.Interfaces;
using KlidecekIS.DAL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace KlidecekIS.BL;

public static class BLInstaller
{
    public static IServiceCollection AddBLServices(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

        services.Scan(selector => selector
            .FromAssemblyOf<BusinessLogic>()
            .AddClasses(filter => filter.AssignableTo(typeof(IFacade<,,>)))
            .AsMatchingInterface()
            .WithSingletonLifetime());

        return services;
    }
}