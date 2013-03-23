using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

//// ReSharper disable InconsistentNaming
namespace Knockout.Binding.Sample.ViewModels
{
    /*
        var BetterListModel = function () {
            this.itemToAdd = ko.observable("");
            this.allItems = ko.observableArray(["Fries", "Eggs Benedict", "Ham", "Cheese"]); // Initial items
            this.selectedItems = ko.observableArray(["Ham"]);                                // Initial selection
 
            this.addItem = function () {
                if ((this.itemToAdd() != "") && (this.allItems.indexOf(this.itemToAdd()) < 0)) // Prevent blanks and duplicates
                    this.allItems.push(this.itemToAdd());
                this.itemToAdd(""); // Clear the text box
            };
 
            this.removeSelected = function () {
                this.allItems.removeAll(this.selectedItems());
                this.selectedItems([]); // Clear selection
            };
 
            this.sortItems = function() {
                this.allItems.sort();
            };
        };
     */

    internal class BetterListViewModel : ViewModelBase
    {
        private string m_ItemToAdd;

        public BetterListViewModel()
        {
            allItems = new ObservableCollectionEx<string>()
                {
                    "Fries",
                    "Eggs Benedict",
                    "Ham",
                    "Cheese"
                };

            selectedItems = new ObservableCollectionEx<string>
                {
                    "Ham"
                };
        }

        public ObservableCollectionEx<string> allItems { get; set; }

        public ObservableCollectionEx<string> selectedItems { get; set; }

        public void removeSelected()
        {
            foreach (var selectedItem in selectedItems)
            {
                allItems.Remove(selectedItem);
            }

            selectedItems.Clear();
        }

        public void sortItems()
        {
            Sort(Comparer<string>.Default);
        }

        private void Sort(IComparer<string> comparer)
        {
            int i, j;
            string index;
            for (i = 1; i < allItems.Count; i++)
            {
                index = allItems[i]; //If you can't read it, it should be index = this[x], where x is i :-)
                j = i;
                while ((j > 0) && (comparer.Compare(allItems[j - 1], index) == 1))
                {
                    allItems[j] = allItems[j - 1];
                    j = j - 1;
                }
                allItems[j] = index;
            }
        }
        
        public void addItem()
        {
            if (!String.IsNullOrEmpty(itemToAdd) && !allItems.Contains(itemToAdd))
                allItems.Add(itemToAdd);

            itemToAdd = "";
        }

        public string itemToAdd
        {
            get { return m_ItemToAdd ?? ""; }
            set
            {
                m_ItemToAdd = value;
                OnPropertyChanged("itemToAdd");
            }
        }

        public override string Name
        {
            get { return "BetterListViewModel"; }
        }
    }
}
//// ReSharper restore InconsistentNaming