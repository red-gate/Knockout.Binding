//// ReSharper disable InconsistentNaming
namespace Knockout.Binding.Sample.ViewModels
{
    internal class HelloWorldViewModel : ViewModelBase
    {
        private string m_LastName;
        private string m_FirstName;

        public HelloWorldViewModel()
        {
            firstName = "Bruce";
            lastName = "Springsteen";
        }

        public string firstName
        {
            get { return m_FirstName; }
            set
            {
                m_FirstName = value;
                OnPropertyChanged("fullName");
            }
        }

        public string lastName
        {
            get { return m_LastName; }
            set
            {
                m_LastName = value;
                OnPropertyChanged("fullName");
            }
        }

        public string fullName
        {
            get { return firstName + " " + lastName; }
            set { /* TODO: Express One Way Bindings */ }
        }

        public override string Name
        {
            get { return "HelloWorldViewModel"; }
        }
    }
}
//// ReSharper restore InconsistentNaming