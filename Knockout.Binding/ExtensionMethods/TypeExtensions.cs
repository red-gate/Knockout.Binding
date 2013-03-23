using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Knockout.Binding.ExtensionMethods
{
    internal static class TypeExtensions
    {
        internal static IEnumerable<PropertyInfo> ObservableCollectionProperties(this Type type)
        {
            return type.GetProperties().Where(IsObservableCollection);
        }

        private static bool IsObservableCollection(PropertyInfo arg)
        {
            return arg.PropertyType.IsGenericType &&
                   arg.PropertyType.GetGenericTypeDefinition() == typeof(ObservableCollectionEx<>);
        }
    }
}