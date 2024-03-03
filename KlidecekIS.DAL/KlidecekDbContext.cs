using KlidecekIS.DAL.Entities;

namespace KlidecekIS.DAL;

using Microsoft.EntityFrameworkCore;

public class KlidecekDbContext(DbContextOptions contextOptions, bool seedDemoData = false) : DbContext(contextOptions)
{
    public DbSet<ActivityEntity> ActivityEntities => Set<ActivityEntity>();
    public DbSet<GradeEntity> Grades => Set<GradeEntity>();
    public DbSet<RoomEntity> Rooms => Set<RoomEntity>();
    public DbSet<StudentEntity> Students => Set<StudentEntity>();
    public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentEntity>()
            .HasMany<SubjectEntity>(i => i.Subjects)
            .WithMany(i => i.Students);

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
    }
}