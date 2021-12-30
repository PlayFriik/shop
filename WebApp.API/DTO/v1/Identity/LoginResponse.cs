using System.Collections.Generic;

namespace WebApp.API.DTO.v1.Identity;

public class LoginResponse
{
    public string Token { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public List<string> Roles { get; set; } = null!;
}