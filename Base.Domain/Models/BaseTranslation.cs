using System;
using System.ComponentModel.DataAnnotations;
using Base.Domain.Interfaces;

namespace Base.Domain.Models;

public class BaseTranslation : BaseTranslation<Guid>, IDomainEntityId
{
        
}

public class BaseTranslation<TKey>: DomainEntityId<TKey> 
    where TKey : IEquatable<TKey>
{
    public virtual TKey TranslationStringId { get; set; } = default!;
        
    [MaxLength(5)]
    public virtual string Culture { get; set; } = default!;

    [MaxLength(10240)]
    public virtual string Value { get; set; } = "";
}