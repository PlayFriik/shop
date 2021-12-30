using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Base.Domain.Interfaces;

namespace Base.Domain;

public class BaseTranslationString<TTranslation> : BaseTranslationString<Guid, TTranslation>, IDomainEntityId
    where TTranslation : BaseTranslation, new()
{
    public BaseTranslationString()
    {
    }

    public BaseTranslationString(string value, string? culture = null) : base(value, culture)
    {
    }

    public static implicit operator string(BaseTranslationString<TTranslation>? l) => l?.ToString() ?? "null";

    // TranslationString translationString = "Foo"
    public static implicit operator BaseTranslationString<TTranslation>(string translationString) => new(translationString);
}

public class BaseTranslationString<TKey, TTranslation> : DomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TTranslation : BaseTranslation<TKey>, new()
{
    private const string DefaultUICulture = "en-GB";

    public virtual ICollection<TTranslation>? Translations { get; set; }

    public BaseTranslationString()
    {
            
    }

    public BaseTranslationString(string value, string? culture = null)
    {
        SetTranslation(value, culture);
    }

    public virtual void SetTranslation(string value, string? culture = null)
    {
        culture ??= Thread.CurrentThread.CurrentUICulture.Name;

        if (Translations == null)
        {
            if (Id.Equals(default))
            {
                Translations = new List<TTranslation>();
            }
            else
            {
                throw new NullReferenceException("Translations cannot be null. Did you forget to do .ThenInclude?");
            }
        }

        var translation = Translations.FirstOrDefault(translation => translation.Culture == culture);
        if (translation == null)
        {
            Translations.Add(new TTranslation
            {
                Culture = culture,
                Value = value
            });
        }
        else
        {
            translation.Value = value;
        }
    }

    public virtual string? Translate(string? culture = null)
    {
        if (Translations == null)
        {
            if (Id.Equals(default))
            {
                return null;
            }
            else
            {
                throw new NullReferenceException("Translations cannot be null. Did you forget to do .ThenInclude?");
            }
        }

        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;

        /*
         cultures in db
         en, en-GB
         in query
         ru, en, en-US, en-GB
         */

        // Try to find match with the CurrentUICulture string
            
        // Exact match
        var translation = Translations.FirstOrDefault(translation => translation.Culture == culture);
        if (translation != null)
        {
            return translation.Value;
        }

        // Match by ignoring region (en-XX or en)
        translation = Translations.FirstOrDefault(translation => culture.StartsWith(translation.Culture));
        if (translation != null)
        {
            return translation.Value;
        }

        // Try to find match with the DefaultUICulture string
            
        // Exact match
        translation = Translations.FirstOrDefault(translation => translation.Culture == DefaultUICulture);
        if (translation != null)
        {
            return translation.Value;
        }

        // Match by ignoring region (en-XX or en)
        translation = Translations.FirstOrDefault(translation => DefaultUICulture.StartsWith(translation.Culture));
        if (translation != null)
        {
            return translation.Value;
        }

        // Return the first one or null
        return Translations.FirstOrDefault()?.Value;
    }

    public override string ToString()
    {
        return Translate() ?? "Translation not found";
    }

    // var baseTranslationString = "Foo" + new BaseTranslationString(Bar)
    // "baseTranslationString" will be string "FooBar"
    public static implicit operator string(BaseTranslationString<TKey, TTranslation>? baseTranslationString) => baseTranslationString?.ToString() ?? "null";

    // BaseTranslationString baseTranslationString = "Foo"
    public static implicit operator BaseTranslationString<TKey, TTranslation>(string translationString) => new(translationString);
}