using Base.Domain.Models;

namespace Shop.Domain.Models;

public class AppTranslation : BaseTranslation
{
    public AppTranslationString? TranslationString { get; set; }
}