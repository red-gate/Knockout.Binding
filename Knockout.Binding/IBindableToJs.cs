using System.ComponentModel;

namespace KnckoutBindingGenerater
{
    public interface IBindableToJs : INotifyPropertyChanged
    {
        string Name { get; }
    }
}