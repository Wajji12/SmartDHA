using System.Reflection;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Application.Common.Models;
using DHAFacilitationAPIs.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DHAFacilitationAPIs.Infrastructure.Data;

public class ApplicationDbContext
    : IdentityDbContext<Application.Common.Models.ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    public DbSet<UserFamily> Residents => Set<UserFamily>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        IdentityBuilder(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private static void ConfigureResidentVehicle(ModelBuilder builder)
    {
        builder.Entity<UserFamily>(entity =>
        {
            entity.ToTable("UserFamily");

            entity.HasKey(r => r.Id);

            entity.Property(r => r.Name)
                  .HasMaxLength(150)
                  .IsRequired();

            entity.Property(r => r.RfidTag)
                  .HasMaxLength(200)
                  .IsRequired();

            entity.Property(r => r.Cnic)
                  .HasMaxLength(150);

            entity.Property(r => r.PhoneNumber)
                  .HasMaxLength(50);

            entity.Property(r => r.FatherName)
                  .HasMaxLength(150);

            entity.Property(r => r.Relation)
                  .HasMaxLength(50);

            entity.Property(r=>r.DateOfBirth)
            .HasColumnType("datetime2")
            .IsRequired();


            //entity.HasMany(r => r.Vehicles)
            //      .WithOne(v => v.UserFamily)
            //      .HasForeignKey(v => v.UserFamilyId)
            //      .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("Vehicles");

            entity.HasKey(v => v.Id);

            entity.Property(v => v.LicenseNo)
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(v => v.ImageURL)
            .HasColumnType("nvarchar(max)");

            entity.Property(v => v.Year)
                  .HasMaxLength(4)
                  .IsRequired();

            entity.Property(v => v.Color)
                  .HasMaxLength(50)
                  .IsRequired();

            //Foreign key relationship with UserFamily
            //entity.HasOne(v => v.UserFamily)
            //      .WithMany(f => f.Vehicles)   // assuming UserFamily has ICollection<Vehicle>
            //      .HasForeignKey(v => v.UserFamilyId)
            //      .OnDelete(DeleteBehavior.Cascade);
        });

    }

    private static void IdentityBuilder(ModelBuilder builder)
    {
        builder.Entity<Domain.Entities.ApplicationUser>(entity =>
        {
            entity.ToTable("ApplicationUser");
            entity.Property(e => e.FullName)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(e => e.Email)
                  .HasMaxLength(100);

            entity.Property(e => e.Password)
                  .HasMaxLength(500); // allow hashed passwords

            entity.Property(e => e.Category)
                  .HasConversion<int>();

            entity.Property(e => e.Property)
                  .HasConversion<int>();

            entity.Property(e => e.ResidenceStatus)
                  .HasConversion<int>();

            entity.Property(e => e.Phase)
                  .HasConversion<int>();

            entity.Property(e => e.LaneNumber)
                  .HasMaxLength(50);

            entity.Property(e => e.PlotNumber)
                  .HasMaxLength(50);

            entity.Property(e => e.Floor)
                  .HasMaxLength(50);

            entity.Property(e => e.FrontSideCNIC)
                  .HasColumnType("nvarchar(max)");

            entity.Property(e => e.BackSideCNIC)
                  .HasColumnType("nvarchar(max)");

            entity.Property(e => e.CNIC)
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(e => e.IsActive);

            entity.Property(e => e.IsDeleted);
        });


        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable("Role");
            entity.Property(e => e.Id).HasMaxLength(85);
            entity.Property(e => e.NormalizedName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(85);
        });

        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
            entity.Property(e => e.UserId).HasMaxLength(85);
            entity.Property(e => e.RoleId).HasMaxLength(85);
        });

        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims");
            entity.Property(e => e.Id).HasMaxLength(85);
            entity.Property(e => e.UserId).HasMaxLength(85);
        });

        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins");
            entity.Property(e => e.LoginProvider).HasMaxLength(85);
            entity.Property(e => e.ProviderKey).HasMaxLength(85);
            entity.Property(e => e.UserId).HasMaxLength(85);
        });

        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens")
                  .HasKey(e => new { e.UserId, e.LoginProvider });

            entity.Property(e => e.UserId).HasMaxLength(85);
        });

        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims");
            entity.Property(e => e.Id).HasMaxLength(85);
            entity.Property(e => e.RoleId).HasMaxLength(85);
        });
    }
}
