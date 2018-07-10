using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PizzaIsland.Extensions
{
    public static class BindingExtensions
    {
        public static T Evaluate<T>(this BindingExpression bindingExpr)
        {
            Type resolvedType = bindingExpr.ResolvedSource.GetType();
            PropertyInfo prop = resolvedType.GetProperty(
                bindingExpr.ResolvedSourcePropertyName);
            return (T)prop.GetValue(bindingExpr.ResolvedSource);
        }
    }
}
