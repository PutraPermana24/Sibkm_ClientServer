using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) { }

    //Introduce the model to the database that eventually become an entity
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Profiling> Profilings { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Role> Roles { get; set; }

    // Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // One University has many Educations
        modelBuilder.Entity<University>()
                    .HasMany(u => u.Educations)
                    .WithOne(e => e.University)
                    .IsRequired(false)
                    .HasForeignKey(e => e.UniversityId)
                    .OnDelete(DeleteBehavior.SetNull);

        // One Education has one Pofilings
        modelBuilder.Entity<Education>()
                    .HasOne(e => e.Profiling)
                    .WithOne(p => p.Education)
                    .IsRequired(false)
                    .HasForeignKey<Profiling>(p => p.EducationId)
                    .OnDelete(DeleteBehavior.SetNull);

        // One profiling has one Employees
        modelBuilder.Entity<Employee>()
                    .HasOne(e => e.Profiling)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Profiling>(p => p.EmployeeNIK)
                    .OnDelete(DeleteBehavior.Restrict);

        //One Employees has one accounts
        modelBuilder.Entity<Employee>()
                    .HasOne(e => e.Account)
                    .WithOne(p => p.Employee)
                    .IsRequired(false)
                    .HasForeignKey<Account>(p => p.EmployeeNik)
                    .OnDelete(DeleteBehavior.Restrict);

        // One account has many AccountRoles
        modelBuilder.Entity<Account>()
                    .HasMany(u => u.AccountRoles)
                    .WithOne(e => e.Account)
                    .IsRequired(false)
                    .HasForeignKey(e => e.AccountNik)
                    .OnDelete(DeleteBehavior.SetNull);

        // One Role has many AccountRoles
        modelBuilder.Entity<Role>()
                    .HasMany(u => u.AccountRoles)
                    .WithOne(e => e.Role)
                    .IsRequired(false)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.SetNull);
    }
}
