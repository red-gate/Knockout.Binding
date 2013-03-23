using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Knockout.Binding
{
    internal class JsonUtils
    {
        internal static string GetJsonForEnumerable(IEnumerable<object> source)
        {
            return JsonConvert.SerializeObject(source);
        }
    }
}
