using WebApp.Domain.Identity;

#pragma warning disable 1591
namespace WebApp.Areas.Admin.Models.UserRoles;

public class DetailsViewModel
{
    public AppUser User { get; set; } = null!;
    public AppRole Role { get; set; } = null!;
}