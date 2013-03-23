namespace Knockout.Binding.Generators
{
    public static class KnockoutProxyGenerator
    {
        public static KnockoutProxy Generate(IBindableToJs javaScript)
        {
            return new KnockoutProxy(javaScript.Name,  
                PrimitiveObservablesGenerator.GetPropertiesAsObservables(javaScript), 
                SimpleMethodsGenerator.GetMethodProxies(javaScript), 
                ObservableArraysGenerator.GetCollectionsAsObservables(javaScript));
        }
    }
}
