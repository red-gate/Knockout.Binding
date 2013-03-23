using System;
using System.Linq;
using System.Reflection;

namespace Knockout.Binding.Generators
{
    internal static class SimpleMethodsGenerator
    {
        internal static string GetMethodProxies(IBindableToJs toBind)
        {
            var methodInfos = toBind.GetType().GetMethods().Where(IsReturnTypeWeSupport);

            return String.Join(Environment.NewLine, methodInfos.Select(info => CreateMethodProxy(toBind.Name, info.Name)));
        }

        private static bool IsReturnTypeWeSupport(MethodInfo method)
        {
            return method.ReturnType == typeof(void) &&
                   !method.GetParameters().Any();
        }

        private static string CreateMethodProxy(string instanceName, string methodName)
        {
            const string c_MethodTemplate = @"
                this.{0} = function() {{
                    {1}.{0}();
                }}";

            return String.Format(c_MethodTemplate, methodName, instanceName);
        }
    }
}