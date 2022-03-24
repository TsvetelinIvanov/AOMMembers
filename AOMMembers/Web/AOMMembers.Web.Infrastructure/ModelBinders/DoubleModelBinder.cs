using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AOMMembers.Web.Infrastructure.ModelBinders
{
    public class DoubleModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext
                .ValueProvider
                .GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && !String.IsNullOrEmpty(valueResult.FirstValue))
            {
                double actualValue = 0;
                bool success = false;

                try
                {
                    string doubleValueString = valueResult.FirstValue;
                    doubleValueString = doubleValueString.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    doubleValueString = doubleValueString.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    actualValue = Convert.ToDouble(doubleValueString, CultureInfo.CurrentCulture);
                    success = true;
                }
                catch (FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
                }

                if (success)
                {
                    bindingContext.Result = ModelBindingResult.Success(actualValue);
                }
            }

            return Task.CompletedTask;
        }
    }
}