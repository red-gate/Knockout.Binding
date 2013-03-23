using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using CefSharp.WinForms;
using Knockout.Binding.Generators;

namespace Knockout.Binding.ExtensionMethods
{
    public static class WebViewExtensions
    {
        //TODO: Do proper comparator
        //TODO: Use weak refs
        private static readonly Dictionary<object, KnockoutProxy> s_Proxies = new Dictionary<object, KnockoutProxy>();

        /// <summary>
        /// Must be called before the webview has been initialized
        /// </summary>
        public static void RegisterForKnockout<T>(this WebView webView, T viewModel) 
            where T : INotifyPropertyChanged, IBindableToJs
        {
            if (webView.IsBrowserInitialized)
            {    
                throw new NotSupportedException("CEF has already beeen initialized, so we can't RegisterForJS");    
            }

            
            webView.RegisterJsObject(viewModel.Name, viewModel);

            s_Proxies[viewModel] = KnockoutProxyGenerator.Generate(viewModel);

            webView.PropertyChanged += (sender, args) => RegisterDomLoadCallback(webView, s_Proxies[viewModel], args);
 
            viewModel.PropertyChanged += (sender, args) => UpdateSimpleObservable(viewModel, args.PropertyName, webView.ExecuteScript);

            RegisterCollectionChangedHandlers(webView, viewModel);
        }

        private static void RegisterCollectionChangedHandlers<T>(WebView webView, T viewModel)
            where T : INotifyPropertyChanged, IBindableToJs
        {
            foreach (var property in viewModel.GetType().ObservableCollectionProperties())
            {
                var collection = (INotifyCollectionChanged) property.GetValue(viewModel, null);

                PropertyInfo info = property;
                collection.CollectionChanged +=
                    (sender, args) => UpdateKnockoutCollection(viewModel, info.Name, (IEnumerable<object>) collection, webView.ExecuteScript);
            }
        }

        private static void RegisterDomLoadCallback(WebView webView, KnockoutProxy proxy, PropertyChangedEventArgs args)
        {
            if (String.Equals(args.PropertyName, "IsLoading", StringComparison.OrdinalIgnoreCase) && !webView.IsLoading)
            {
                webView.ExecuteScript(proxy.KnockoutViewModel);
            }
        }

        private static void UpdateSimpleObservable(IBindableToJs viewModel, string observableName, Action<string> javascriptExecutor)
        {
            var newValueGetter = string.Format("{0}.get_{1}()", viewModel.Name, observableName);

            var updateObservable = string.Format("{0}.{1}({2})", s_Proxies[viewModel].ViewModelInstanceName, observableName, newValueGetter);

            javascriptExecutor(updateObservable);
        }

        private static void UpdateKnockoutCollection(IBindableToJs viewModel, string observableName, IEnumerable<object> values, Action<string> javascriptExecutor)
        {
            var updateObservable = string.Format(@"
                                                  var underlyingArray = {0}.{1}();

                                                  underlyingArray.length = 0;

                                                  ko.utils.arrayPushAll(underlyingArray, {2});

                                                  {0}.{1}.valueHasMutated();
            
            ", s_Proxies[viewModel].ViewModelInstanceName, observableName, values.ToJson());

            javascriptExecutor(updateObservable);
        }
    }
}