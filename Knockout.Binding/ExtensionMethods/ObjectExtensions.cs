using Newtonsoft.Json;

namespace Knockout.Binding.ExtensionMethods
{
    internal static class ObjectExtensions
    {
        internal static string ToJson(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }
    }
}