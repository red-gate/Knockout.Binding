using System;
using System.Collections.Generic;
using System.Linq;

namespace KnckoutBindingGenerater
{
    internal class JsonUtils
    {
        internal static string GetJsonForEnumerable(IEnumerable<object> source)
        {
            return "[" + String.Join(",", source.Select(JsonUtils.GetJsonforObject)) + "]";
        }

        private static string GetJsonforObject(object value)
        {
            var jsonforObject = Convert.ToString(value);

            return value is string ? String.Format("'{0}'", jsonforObject) : jsonforObject;
        }
    }
}
