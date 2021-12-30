using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

#pragma warning disable 1591
namespace WebApp.Helpers.MVC
{
    public class FloatingPointModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(decimal) ||
                context.Metadata.ModelType == typeof(float) ||
                context.Metadata.ModelType == typeof(double))
            {
                return new FloatingPointModelBinder(context.Metadata.ModelType);
            }

            return null;
        }
    }

    public class FloatingPointModelBinder : IModelBinder
    {
        private readonly Type _type;

        public FloatingPointModelBinder(Type type)
        {
            _type = type;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            value = value.Trim();

            value = value.Replace(",", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            value = value.Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            if (_type == typeof(decimal))
            {
                if (!decimal.TryParse(value, out var resultValue))
                {
                    bindingContext.ModelState.TryAddModelError(
                        bindingContext.ModelName,
                        $"Could not parse decimal {value}.");
                    return Task.CompletedTask;
                }

                bindingContext.Result = ModelBindingResult.Success(resultValue);
            }
            else if (_type == typeof(double))
            {
                if (!double.TryParse(value, out var resultValue))
                {
                    bindingContext.ModelState.TryAddModelError(
                        bindingContext.ModelName,
                        $"Could not parse double {value}.");
                    return Task.CompletedTask;
                }

                bindingContext.Result = ModelBindingResult.Success(resultValue);
            }
            else if (_type == typeof(float))
            {
                if (!float.TryParse(value, out var resultValue))
                {
                    bindingContext.ModelState.TryAddModelError(
                        bindingContext.ModelName,
                        $"Could not parse float {value}.");
                    return Task.CompletedTask;
                }

                bindingContext.Result = ModelBindingResult.Success(resultValue);
            }

            return Task.CompletedTask;
        }
    }
}