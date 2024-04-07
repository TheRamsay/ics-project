using AutoMapper;
using KlidecekIS.BL.Mappers;
using KlidecekIS.Common.Tests;
using KlidecekIS.Common.Tests.Factories;
using KlidecekIS.DAL;
using KlidecekIS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace KlidecekIS.BL.Tests;

public class FacadeTestsBase : IAsyncLifetime
{
    protected IMapper Mapper;
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);
        
        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);
        
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
        
        Mapper = config.CreateMapper();
        
        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);
        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<KlidecekDbContext> DbContextFactory { get; }

    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    } 
}