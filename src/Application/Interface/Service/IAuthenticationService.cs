using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DHAFacilitationAPIs.Application.Common.DependencyResolver;
using DHAFacilitationAPIs.Application.Common.Models;
using DHAFacilitationAPIs.Application.ViewModels;

namespace DHAFacilitationAPIs.Application.Interface.Service;
public interface IAuthenticationService : IServicesType.IScopedService
{
    Task<string> GenerateTokenAsync(string cnic, string? role = "User");
    Task<string> GenerateTokenAsync(IEnumerable<Claim> claims);

}
