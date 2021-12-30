using System;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.BLL.DTO;

public class Picture : DomainEntityId
{
    public Guid ProductId { get; set; }
        
    public Product? Product { get; set; }

    [MaxLength(1024, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public string Path { get; set; } = null!;
}