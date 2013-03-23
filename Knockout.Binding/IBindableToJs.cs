using System.ComponentModel;

namespace Knockout.Binding
{
    public interface IBindableToJs : INotifyPropertyChanged
    {
        string Name { get; }
    }
}