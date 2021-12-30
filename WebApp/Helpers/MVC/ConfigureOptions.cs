using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

#pragma warning disable 1591
namespace WebApp.Helpers.MVC
{
    public class ConfigureOptions : IConfigureOptions<MvcOptions>
    {
        public void Configure(MvcOptions options)
        {
            options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(value =>
                $"Value {value} is invalid"
            );
        }
    }
}