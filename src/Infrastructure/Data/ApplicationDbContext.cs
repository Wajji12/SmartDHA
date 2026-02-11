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
    public DbSet<UserFamily> UserFamilies { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Property> Properties { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        IdentityBuilder(builder);
        ConfigureResidentVehicle(builder);

        builder.Entity<UserFamily>()
            .HasOne(x => x.ApplicationUser)
            .WithMany(x => x.UserFamilies)
            .HasForeignKey(x => x.ApplicationUserId);

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

            entity.Property(r => r.ResidentCardNumber)
                  .HasMaxLength(200)
                  .IsRequired();

            entity.Property(r => r.Cnic)
                  .HasMaxLength(150);

            entity.Property(r => r.PhoneNumber)
                  .HasMaxLength(50);

            entity.Property(r => r.FatherHusbandName)
                  .HasMaxLength(150);

            entity.Property(r => r.Relation)
                  .HasMaxLength(50);

            entity.Property(v => v.ProfilePicture)
            .HasColumnType("nvarchar(max)");

            entity.Property(r=>r.DateOfBirth)
            .HasColumnType("datetime2")
            .IsRequired();

            entity.Property(p => p.Created)
                              .HasColumnType("datetimeoffset")
                              .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            entity.Property(p => p.LastModified)
                  .HasColumnType("datetimeoffset");

            entity.Property(p => p.CreatedBy)
                  .HasMaxLength(100);

            entity.Property(p => p.LastModifiedBy)
                  .HasMaxLength(100);

            // ===== Soft delete flags =====

            entity.Property(p => p.IsActive)
                  .HasDefaultValue(true);

            entity.Property(p => p.IsDeleted)
                  .HasDefaultValue(false);
        });

        builder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("Vehicles");

            entity.HasKey(v => v.Id);

            entity.Property(v => v.LicenseNo)
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(v => v.Attachment)
            .HasColumnType("nvarchar(max)");

            entity.Property(v => v.Year)
                  .HasMaxLength(4)
                  .IsRequired();

            entity.Property(v => v.Color)
                  .HasMaxLength(50)
                  .IsRequired();
            entity.Property(p => p.Created)
                  .HasColumnType("datetimeoffset")
                  .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            entity.Property(p => p.LastModified)
                  .HasColumnType("datetimeoffset");

            entity.Property(p => p.CreatedBy)
                  .HasMaxLength(100);

            entity.Property(p => p.LastModifiedBy)
                  .HasMaxLength(100);

            // ===== Soft delete flags =====

            entity.Property(p => p.IsActive)
                  .HasDefaultValue(true);

            entity.Property(p => p.IsDeleted)
                  .HasDefaultValue(false);
        });

        builder.Entity<Property>(entity =>
        {
            entity.ToTable("Properties");

            entity.HasKey(p => p.Id);

            // ===== Enums (stored as int — recommended) =====

            entity.Property(p => p.Category)
                  .HasConversion<int>();

            entity.Property(p => p.Type)
                  .HasConversion<int>();

            entity.Property(p => p.Phase)
                  .HasConversion<int>();

            entity.Property(p => p.Zone)
                  .HasConversion<int>();

            entity.Property(p => p.PossessionType)
                  .HasConversion<int>()
                  .IsRequired();

            // ===== Strings =====

            entity.Property(p => p.Khayaban)
                  .HasMaxLength(150);

            entity.Property(p => p.StreetNo)
                  .HasMaxLength(50);

            entity.Property(p => p.Plot)
                  .HasMaxLength(50);

            // ===== Numbers =====

            entity.Property(p => p.Floor)
                  .IsRequired();

            entity.Property(p => p.PlotNo)
                  .IsRequired();

            // ===== Attachments (Base64 or long string) =====
            // Use NVARCHAR(MAX)

            entity.Property(p => p.ProofOfPossessionImage)
                  .HasColumnType("nvarchar(max)");

            entity.Property(p => p.UtilityBillAttachment)
                  .HasColumnType("nvarchar(max)");

            // ===== Auditing (from BaseAuditableEntity) =====

            entity.Property(p => p.Created)
                  .HasColumnType("datetimeoffset")
                  .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            entity.Property(p => p.LastModified)
                  .HasColumnType("datetimeoffset");

            entity.Property(p => p.CreatedBy)
                  .HasMaxLength(100);

            entity.Property(p => p.LastModifiedBy)
                  .HasMaxLength(100);

            // ===== Soft delete flags =====

            entity.Property(p => p.IsActive)
                  .HasDefaultValue(true);

            entity.Property(p => p.IsDeleted)
                  .HasDefaultValue(false);
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

            entity.Property(e => e.EmailAddress)
                  .HasMaxLength(100);

            entity.Property(e => e.Password)
                  .HasMaxLength(500);

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
            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.NormalizedName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(85);
        });

        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
            entity.Property(e => e.UserId).HasMaxLength(450);
            entity.Property(e => e.RoleId).HasMaxLength(450);
        });

        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens")
                  .HasKey(e => new { e.UserId, e.LoginProvider });

            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims");
            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.RoleId).HasMaxLength(450);


        });

    }

}
