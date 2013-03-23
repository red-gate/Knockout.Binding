using System;
using System.Linq;
using System.Reflection;

namespace Knockout.Binding.Generators
{
    internal static class PrimitiveObservablesGenerator
    {
        internal static string GetPropertiesAsObservables(IBindableToJs javaScript)
        {
            var observableProperties = javaScript.GetType().GetProperties().Where(IsObservableWeSupport);

            var observableDefinitions = Enumerable.Select(observableProperties, x => CreateObservableFunction(x.Name, javaScript.Name));

            return String.Join(Environment.NewLine, observableDefinitions);
        }

        private static string CreateObservableFunction(string observableName, string instanceName)
        {
            const string c_ObservableTemplate = @"
                this.{0} = ko.observable({1}.get_{0}());

                this.{0}.subscribe(function(newValue) {{
                    {1}.set_{0}(newValue);
                }});
            ";

            return String.Format(c_ObservableTemplate, observableName, instanceName);
        }

        private static bool IsObservableWeSupport(PropertyInfo info)
        {
            Type propertyType = info.PropertyType;

            return propertyType == typeof (string) ||
                   propertyType == typeof (int) ||
                   propertyType == typeof (bool);
        }
    }
}