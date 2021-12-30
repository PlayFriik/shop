using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.API.DTO.v1;

public class Picture
{
    public Guid Id { get; set; }
        
    public Guid ProductId { get; set; }

    [MaxLength(1024, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public string Path { get; set; } = null!;
}