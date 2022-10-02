using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Models.v1;

public class Location
{
    public Guid Id { get; set; }

    public Guid ProviderId { get; set; }

    public Provider? Provider { get; set; }

    [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public string Name { get; set; } = null!;

    [MaxLength(1024, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public string Address { get; set; } = null!;
}