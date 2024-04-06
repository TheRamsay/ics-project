using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.Factory;
using KlidecekIS.DAL.UnitOfWork;

namespace KlidecekIS.DAL.Tests;

// Funny tests to check if my DAL works
// TODO: rework I guess, or move them to integration tests
public class RepositoryTests
{
    [Fact]
    public async Task TestRepositoryInsert()
    {
        var dbContext = new DbContextSqLiteFactory("pepan", false).CreateDbContext();
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
        
        var factory = new UnitOfWorkFactory(new DbContextSqLiteFactory("pepan", false));
        var uow = factory.Create();
        var guid = Guid.NewGuid();
        
        var repository = uow.GetRepository<StudentEntity>();
        repository.Insert(new StudentEntity
        {
            Id = guid,
            Name = "Pepan",
            Surname = "Pepanek"
        });

        await uow.CommitAsync();

        var students = dbContext.Students;
        
        Assert.Single(students);

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }
    
    [Fact]
    public async Task TestRepositoryUpdate()
    {
        var dbContextFactory = new DbContextSqLiteFactory("pepan", false);
        var dbContext = dbContextFactory.CreateDbContext();
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
        
        var factory = new UnitOfWorkFactory(dbContextFactory);
        var uow = factory.Create();
        var guid = Guid.NewGuid();
        
        dbContext.Students.Add(new StudentEntity
        {
            Id = guid,
            Name = "Pepan",
            Surname = "Pepanek"
        });
        
        await dbContext.SaveChangesAsync();

        var repository = uow.GetRepository<StudentEntity>();
        var updatedStudent = new StudentEntity
        {
            Id = guid,
            Name = "brrrr",
            Surname = "brrrrr!!!"
        };
        
        await repository.UpdateAsync(updatedStudent);
        await uow.CommitAsync();

        dbContext = dbContextFactory.CreateDbContext();
        var studentsAfterUpdate = dbContext.Students;
        var studentAfterUpdate = studentsAfterUpdate.First();
        Assert.Equal("brrrr", studentAfterUpdate.Name);

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }
    
    [Fact]
    public async Task TestRepositoryDelete()
    {
        var dbContext = new DbContextSqLiteFactory("pepan", false).CreateDbContext();
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
        
        var factory = new UnitOfWorkFactory(new DbContextSqLiteFactory("pepan", false));
        var uow = factory.Create();
        var guid = Guid.NewGuid();
        
        var repository = uow.GetRepository<StudentEntity>();
        dbContext.Students.Add(new StudentEntity
        {
            Id = guid,
            Name = "Pepan",
            Surname = "Pepanek"
        });

        await dbContext.SaveChangesAsync();
        await repository.DeleteAsync(guid);
        await uow.CommitAsync();
        
        var students = dbContext.Students;
        Assert.Empty(students);

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }
    
    [Fact]
    public async Task TestRepositoryExistsPositive()
    {
        var dbContext = new DbContextSqLiteFactory("pepan", false).CreateDbContext();
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
        
        var factory = new UnitOfWorkFactory(new DbContextSqLiteFactory("pepan", false));
        var uow = factory.Create();
        var guid = Guid.NewGuid();
        
        var entity1 = new StudentEntity
        {
            Id = guid,
            Name = "Pepan",
            Surname = "Pepanek"
        };
        
        var repository = uow.GetRepository<StudentEntity>();
        dbContext.Students.Add(entity1);
        await dbContext.SaveChangesAsync();
        
        Assert.True(await repository.ExistsAsync(entity1));

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }
    
    [Fact]
    public async Task TestRepositoryExistsNegative()
    {
        var dbContext = new DbContextSqLiteFactory("pepan", false).CreateDbContext();
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
        
        var factory = new UnitOfWorkFactory(new DbContextSqLiteFactory("pepan", false));
        var uow = factory.Create();
        var guid = Guid.NewGuid();
        
        var entity1 = new StudentEntity
        {
            Id = guid,
            Name = "Pepan",
            Surname = "Pepanek"
        };
        
        var repository = uow.GetRepository<StudentEntity>();
        
        Assert.False(await repository.ExistsAsync(entity1));

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }
}