using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shop.Application.Models.v1.Internationalization;

namespace Shop.Presentation.ApiControllers.v1.Internationalization;

/// <summary>
/// Controller for managing languages and translations with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class TranslationsController : ControllerBase
{
    private readonly IOptions<RequestLocalizationOptions> _localizationOptions;
        
    /// <inheritdoc />
    public TranslationsController(IOptions<RequestLocalizationOptions> localizationOptions)
    {
        _localizationOptions = localizationOptions;
    }

    // GET: api/Translations/GetLanguages
    /// <summary>
    /// Get a list of supported languages
    /// </summary>
    /// <returns>List of supported languages</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Language>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<Language>> GetLanguages()
    {
        var languages = _localizationOptions
            .Value
            .SupportedUICultures!
            .Select(cultureInfo =>
                new Language
                {
                    Name = cultureInfo.Name,
                    NativeName = cultureInfo.NativeName
                });

        return Ok(languages);
    }
        
    // GET: api/Translations/GetTranslations
    /// <summary>
    /// Get translations in a JSON structure
    /// </summary>
    /// <returns>Translations in a JSON structure</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Translations>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Translations> GetTranslations()
    {
        return Ok(new Translations());
    }
}