using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Models.v1;

public class Picture
{
    public Guid Id { get; set; }
        
    public Guid ProductId { get; set; }

    [MaxLength(1024, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public string Path { get; set; } = null!;
}