using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AOMMembers.Web.Infrastructure.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext
                .ValueProvider
                .GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && !String.IsNullOrEmpty(valueResult.FirstValue))
            {
                decimal actualValue = 0;
                bool success = false;

                try
                {
                    string decimalValueString = valueResult.FirstValue;
                    decimalValueString = decimalValueString.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    decimalValueString = decimalValueString.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    actualValue = Convert.ToDecimal(decimalValueString, CultureInfo.CurrentCulture);
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