using Base.Domain.Models;

namespace Shop.Domain.Models;

public class AppTranslationString : BaseTranslationString<AppTranslation>
{
    // navigation properties back where this string is used
    // since every string is unique, only one nav property should have value
    // no fk-s here, keys are kept on the other end of relationship
        
    public Category? CategoryName { get; set; }
        
    public Location? LocationName { get; set; }
    public Location? LocationAddress { get; set; }
        
    public Product? ProductName { get; set; }
    public Product? ProductDescription { get; set; }
        
    public Provider? ProviderName { get; set; }
        
    public Status? StatusName { get; set; }

    public AppTranslationString()
    {
            
    }

    public AppTranslationString(string value, string? culture = null) : base(value, culture)
    {
            
    }
        
    // var baseTranslationString = "Foo" + new BaseTranslationString(Bar)
    // "baseTranslationString" will be string "FooBar"
    public static implicit operator string(AppTranslationString? appTranslationString) => appTranslationString?.ToString() ?? "null";

    // AppTranslationString appTranslationString = "Foo"
    public static implicit operator AppTranslationString(string appTranslationString) => new(appTranslationString);
}