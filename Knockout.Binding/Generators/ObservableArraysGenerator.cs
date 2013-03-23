using System;
using System.Linq;
using Knockout.Binding.ExtensionMethods;

namespace Knockout.Binding.Generators
{
    internal static class ObservableArraysGenerator
    {
        private const string c_ObservableTemplate = @"
                this.{0} = ko.observableArray({2});

                this.{0}.subscribe(function(newValue) {{    
                    var actualClrCollection = {1}.get_{0}().DisableNotifications();

                    actualClrCollection.Clear();

                    for (var i = 0; i < newValue.length; i++) {{
                        actualClrCollection.Add(newValue[i]);
                    }}
                }});
            ";

        internal static string GetCollectionsAsObservables(IBindableToJs viewModel)
        {
            var observableCollections = viewModel.GetType().ObservableCollectionProperties();

            var observableArrays = observableCollections.Select(info => CreateObservableArrayWithListener(info.Name, viewModel.Name, info.GetValue(viewModel, null))).ToList();

            return String.Join(Environment.NewLine, observableArrays);
        }

        private static string CreateObservableArrayWithListener(string propertyName, string proxyInstanceName, object values)
        {
            return String.Format(c_ObservableTemplate, propertyName, proxyInstanceName, values.ToJson());
        }
    }
}