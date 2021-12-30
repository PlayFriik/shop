using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.BLL.DTO;

public class Status : DomainEntityId
{
    public Guid NameId { get; set; }

    [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public string Name { get; set; } = null!;

    public List<Order>? Orders { get; set; }
}