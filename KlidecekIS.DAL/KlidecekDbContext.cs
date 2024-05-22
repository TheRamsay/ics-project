using KlidecekIS.DAL.Entities;
using KlidecekIS.DAL.Seeds;

namespace KlidecekIS.DAL;

using Microsoft.EntityFrameworkCore;

public class KlidecekDbContext(DbContextOptions contextOptions, bool seedDemoData) : DbContext(contextOptions)
{
    public DbSet<ActivityEntity> ActivityEntities => Set<ActivityEntity>();
    public DbSet<GradeEntity> Grades => Set<GradeEntity>();
    public DbSet<RoomEntity> Rooms => Set<RoomEntity>();
    public DbSet<StudentEntity> Students => Set<StudentEntity>();
    public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();
    
    private bool _loaded = false;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentSubjectEntity>()
            .HasOne<StudentEntity>(i => i.Student)
            .WithMany(i => i.Subjects)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<StudentSubjectEntity>()
            .HasOne<SubjectEntity>(i => i.Subject)
            .WithMany(i => i.Students)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StudentSubjectEntity>()
            .HasIndex(ss => new { ss.StudentId, ss.SubjectId })
            .IsUnique();

        modelBuilder.Entity<StudentEntity>()
            .HasMany<GradeEntity>(i => i.Grades)
            .WithOne(i => i.Student)
            .HasForeignKey(i => i.StudentId);

        modelBuilder.Entity<ActivityEntity>()
            .HasOne<SubjectEntity>(i => i.Subject)
            .WithMany(i => i.Activities)
            .HasForeignKey(i => i.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ActivityEntity>()
            .HasMany<GradeEntity>(i => i.Grades)
            .WithOne(i => i.Activity)
            .HasForeignKey(i => i.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RoomEntity>()
            .HasMany<ActivityEntity>(i => i.Activites)
            .WithOne(i => i.Room)
            .HasForeignKey(i => i.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        if (seedDemoData)
        {
            ActivitySeeds.Seed(modelBuilder);
            StudentSeeds.Seed(modelBuilder);
            StudentSubjectSeeds.Seed(modelBuilder);
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