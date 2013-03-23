
using System;
using System.Collections.ObjectModel;

//// ReSharper disable InconsistentNaming
namespace Knockout.Binding.Sample.ViewModels
{
    /*
     var SimpleListModel = function(items) {
            this.items = ko.observableArray(items);
            this.itemToAdd = ko.observable("");
            this.addItem = function() {
                if (this.itemToAdd() != "") {
                    this.items.push(this.itemToAdd()); // Adds the item. Writing to the "items" observableArray causes any associated UI to update.
                    this.itemToAdd(""); // Clears the text box, because it's bound to the "itemToAdd" observable
                }
            }.bind(this);  // Ensure that "this" is always this view model
        };
    */
    internal class SimpleListViewModel : ViewModelBase
    {
        private string m_ItemToAdd;

        public SimpleListViewModel()
        {
            items = new ObservableCollectionEx<string>()
                {
                    "Alpha",
                    "Beta",
                    "Gamma"
                };
        }

        public ObservableCollectionEx<string> items { get; set; }

        public string itemToAdd
        {
            get { return m_ItemToAdd ?? ""; }
            set
            {
                m_ItemToAdd = value;
                OnPropertyChanged("itemToAdd");
            }
        }

        public void addItem()
        {
            if (!String.IsNullOrEmpty(itemToAdd))
            {
                items.Add(itemToAdd);
                itemToAdd = "";
            }
        }

        public override string Name
        {
            get { return "SimpleListViewModel"; }
        }
    }
}
//// ReSharper restore InconsistentNaming