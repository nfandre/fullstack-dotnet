using Microsoft.AspNetCore.Identity;

namespace Dima.Api.Models;

public class User : IdentityUser<long>
{
    // RBAC -> Role-based access control
    public List<IdentityRole<long>> Roles { get; set; }
}