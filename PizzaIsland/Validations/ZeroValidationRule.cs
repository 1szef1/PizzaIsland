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
    public class ZeroValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is BindingExpression)
            {
                var valueTmp1 = ((BindingExpression)value).Evaluate<int>();
                if (value == null || valueTmp1 <= 0)
                    return new ValidationResult(false, "Wartość musi być większa od zera");
                else
                    return new ValidationResult(true, null);
            }

            int valueTmp2 = 0;
            if (value == null || !int.TryParse(value.ToString(), out valueTmp2) || valueTmp2 <= 0)
                return new ValidationResult(false, "Wartość musi być większa od zera");
            else
                return new ValidationResult(true, null);
        }
    }
}
