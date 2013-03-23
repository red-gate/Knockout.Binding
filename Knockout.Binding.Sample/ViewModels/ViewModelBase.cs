using System.ComponentModel;

namespace Knockout.Binding.Sample.ViewModels
{
    internal abstract class ViewModelBase : IBindableToJs
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }
        
        public abstract string Name { get; }
    }
}