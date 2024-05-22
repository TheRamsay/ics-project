using System.Net.Security;
using System.Runtime.InteropServices.JavaScript;
using KlidecekIS.Common.Tests.Seeds;
using KlidecekIS.DAL;
using Microsoft.EntityFrameworkCore;

namespace KlidecekIS.Common.Tests;

public class KlidecekTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false) 
    : KlidecekDbContext(contextOptions, seedDemoData: false)
{
    private bool _loaded;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (seedTestingData)
        {
            ActivitySeeds.Seed(modelBuilder);
            StudentSeeds.Seed(modelBuilder);
            RoomSeeds.Seed(modelBuilder);
            SubjectSeeds.Seed(modelBuilder);
            GradeSeeds.Seed(modelBuilder);

            if (!_loaded)
            {
                _loaded = true;
                RoomSeeds.LoadLists();
                StudentSeeds.LoadLists();
                SubjectSeeds.LoadLists();
                ActivitySeeds.LoadLists();
            }
        }
    }
}