using Microsoft.AspNetCore.Identity;

namespace DewiSimulationMPA201.Models;
public class AppUser : IdentityUser
{
    public string FullName { get; set; } = null!;
}
