using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using PizzaIsland.Extensions;

namespace PizzaIsland.Validations
{
    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is BindingExpression)
            {
                var valueTmp = ((BindingExpression)value).Evaluate<string>();
                if (!valueTmp.IsValidEmail())
                    return new ValidationResult(false, "Niepoprawny email");
                else
                    return new ValidationResult(true, null);
            }

            if (!value.ToString().IsValidEmail())
                return new ValidationResult(false, "Niepoprawny email");
            else
                return new ValidationResult(true, null);


        }
    }
}
