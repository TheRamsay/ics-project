using KlidecekIS.DAL.Entities;

namespace KlidecekIS.DAL;
using Microsoft.EntityFrameworkCore;

public class KliedecekDbContext(DbContextOptions contextOptions) : DbContext(contextOptions)
{
    public DbSet<ActivityEntity> ActivityEntities => Set<ActivityEntity>();
    public DbSet<GradeEntity> Grades => Set<GradeEntity>();
    public DbSet<RoomEntity> Rooms => Set<RoomEntity>();
    public DbSet<StudentEntity> Students => Set<StudentEntity>();
    public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentEntity>().HasMany<SubjectEntity>(i => i.Subjects).WithMany(i => i.Students);

        modelBuilder.Entity<ActivityEntity>().HasOne<SubjectEntity>( i => i.Subject).WithMany(i => i.Activites);
        modelBuilder.Entity<ActivityEntity>().HasOne<GradeEntity>( i => i.Grade).WithOne( i => i.Activity).HasForeignKey<GradeEntity>(i => i.ActivityId);

        modelBuilder.Entity<SubjectEntity>().HasMany<ActivityEntity>( i => i.Activites).WithOne(i => i.Subject);
        modelBuilder.Entity<SubjectEntity>().HasMany<StudentEntity>( i => i.Students);

        modelBuilder.Entity<GradeEntity>().HasOne<ActivityEntity>( i => i.Activity).WithOne(i => i.Grade).HasForeignKey<GradeEntity>(i => i.ActivityId);

        modelBuilder.Entity<RoomEntity>().HasMany<ActivityEntity>( i => i.Activites).WithOne(i => i.Room);
    }
}