using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Models.v1;

public class Status
{
    public Guid Id { get; set; }

    [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public string Name { get; set; } = null!;
}