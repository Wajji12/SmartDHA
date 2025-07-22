using System.Runtime.InteropServices;
using DHAFacilitationAPIs.Infrastructure.Identity;
using DHAFacilitationAPIs.Application.Common.Models;
using DHAFacilitationAPIs.Domain.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DHAFacilitationAPIs.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

       // await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var roles = Roles.GetRoles();

        foreach (var role in roles)
        {
            if (await _roleManager.RoleExistsAsync(role))
                continue;

            await _roleManager.CreateAsync(new IdentityRole { Name = role });
        }

        // Default users
        var administrator = new ApplicationUser { Name = "Super Admin", UserName = "administrator@localhost.com", Email = "administrator@localhost.com" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            await _userManager.AddToRoleAsync(administrator, Roles.Administrator);

        }

        // Tbo users
        var tboUser = new ApplicationUser { Name = "Tbo User", UserName = "tboUser@localhost.com", Email = "tboUser@localhost.com" };

        if (_userManager.Users.All(u => u.UserName != tboUser.UserName))
        {

            await _userManager.CreateAsync(tboUser, "TboUser@123!");
            await _userManager.AddToRoleAsync(tboUser, Roles.Tbo);

        }

        // Default data
       
    }
}
