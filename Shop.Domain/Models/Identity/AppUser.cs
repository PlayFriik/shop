using Base.Domain.Models.Identity;

namespace Shop.Domain.Models.Identity;

public class AppUser : BaseUser<AppUserRole>
{
    public List<Order>? Orders { get; set; }
}