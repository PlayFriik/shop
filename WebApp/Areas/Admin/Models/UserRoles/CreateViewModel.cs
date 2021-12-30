using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Domain.Identity;

#pragma warning disable 1591
namespace WebApp.Areas.Admin.Models.UserRoles;

public class CreateViewModel
{
    public SelectList? Users { get; set; }
    public SelectList? Roles { get; set; }
        
    public AppUser User { get; set; } = null!;
    public AppRole Role { get; set; } = null!;
}