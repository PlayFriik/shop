using Microsoft.AspNetCore.Identity;
using WebApp.Domain.Identity;

#pragma warning disable 1591
namespace WebApp.Areas.Admin.Models.UserRoles;

public class IndexViewModel
{
    public UserManager<AppUser> UserManager { get; set; } = null!;
    public RoleManager<AppRole> RoleManager { get; set; } = null!;

    public List<AppUser> Users { get; set; } = null!;
}