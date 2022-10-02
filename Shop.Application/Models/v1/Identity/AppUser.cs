using Base.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Models.v1.Identity;

public class AppUser : DomainEntityId
{
    [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public string UserName { get; set; } = null!;
        
    [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public string Email { get; set; } = null!;
        
    [MaxLength(16, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    [MinLength(4, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public string PhoneNumber { get; set; } = null!;
        
    [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public string FirstName { get; set; } = null!;
        
    [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public string LastName { get; set; } = null!;

    public string FullName => FirstName + " " + LastName;

    public class LoginRequest
    {
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
        public string Email { get; set; } = null!;

        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
        public string Password { get; set; } = null!;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public List<string> Roles { get; set; } = null!;
    }

    public class RegisterRequest
    {
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
        public string FirstName { get; set; } = null!;

        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
        public string LastName { get; set; } = null!;

        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
        public string Email { get; set; } = null!;

        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
        public string PhoneNumber { get; set; } = null!;

        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
        public string Password { get; set; } = null!;
    }

    public class RegisterResponse
    {
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public List<string> Roles { get; set; } = null!;
    }
}