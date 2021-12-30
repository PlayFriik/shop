﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.API.DTO.v1;

public class Status
{
    public Guid Id { get; set; }
        
    public Guid NameId { get; set; }
        
    [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public string Name { get; set; } = null!;
}