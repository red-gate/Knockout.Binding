namespace Knockout.Binding.Generators
{
    public static class KnockoutProxyGenerator
    {
        public static KnockoutProxy Generate(IBindableToJs viewModel)
        {
            return new KnockoutProxy(viewModel.Name,  
                PrimitiveObservablesGenerator.GetPropertiesAsObservables(viewModel), 
                SimpleMethodsGenerator.GetMethodProxies(viewModel), 
                ObservableArraysGenerator.GetCollectionsAsObservables(viewModel));
        }
    }
}
