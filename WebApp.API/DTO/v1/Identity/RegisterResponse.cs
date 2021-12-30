using System.Collections.Generic;

namespace WebApp.API.DTO.v1.Identity;

public class RegisterResponse
{
    public string Token { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public List<string> Roles { get; set; } = null!;
}