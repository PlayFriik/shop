using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Base.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Base.Domain.Models.Identity;

public class BaseUser<TUserRole> : IdentityUser<Guid>, IDomainEntityId
    where TUserRole : IdentityUserRole<Guid>
{
    [MaxLength(16, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
    [MinLength(4, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))]
    public override string PhoneNumber { get; set; } = null!;

    [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))]
    public string FirstName { get; set; } = null!;
        
    [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))]
    public string LastName { get; set; } = null!;

    public string FullName => FirstName + " " + LastName;
        
    public virtual List<TUserRole>? UserRoles { get; set; }
}