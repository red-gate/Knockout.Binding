//// ReSharper disable InconsistentNaming
using System.Collections.ObjectModel;

namespace Knockout.Binding.Sample.ViewModels
{
        /*
         var viewModel = {
                stringValue : ko.observable("Hello"),
                passwordValue : ko.observable("mypass"),
                booleanValue : ko.observable(true),
                optionValues : ["Alpha", "Beta", "Gamma"],
                selectedOptionValue : ko.observable("Gamma"),
                multipleSelectedOptionValues : ko.observable(["Alpha"]),
                radioselectedOptionValue : ko.observable("Beta")
        };
         */
    internal class ControlTypesViewModel : ViewModelBase
    {
        private string m_StringValue;
        private bool m_BooleanValue;
        private string m_SelectedOptionValue;
        private string m_RadioselectedOptionValue;
        private string m_PasswordValue;

        public ControlTypesViewModel()
        {
            stringValue = "Hello";
            booleanValue = true;
            optionValues = new ObservableCollectionEx<string>
                {
                    "Alpha",
                    "Beta",
                    "Gamma"
                };
            selectedOptionValue = "Gamma";
            multipleselectedOptionValues = new ObservableCollectionEx<string> {"Alpha"};
            radioselectedOptionValue = "Beta";
            passwordValue = "myPass";
        }

        public string passwordValue
        {
            get { return m_PasswordValue; }
            set
            {
                m_PasswordValue = value;
                OnPropertyChanged("passwordValue");
            }
        }
        
        public string radioselectedOptionValue
        {
            get { return m_RadioselectedOptionValue; }
            set
            {
                m_RadioselectedOptionValue = value;
                OnPropertyChanged("radioselectedOptionValue");
            }
        }

        public ObservableCollectionEx<string> multipleselectedOptionValues { get; set; }

        public string selectedOptionValue
        {
            get { return m_SelectedOptionValue; }
            set
            {
                m_SelectedOptionValue = value;
                OnPropertyChanged("selectedOptionValue");
            }
        }

        public ObservableCollectionEx<string> optionValues { get; set; }

        public bool booleanValue
        {
            get { return m_BooleanValue; }
            set
            {
                m_BooleanValue = value;
                OnPropertyChanged("booleanValue");
            }
        }

        public string stringValue
        {
            get { return m_StringValue; }
            set
            {
                m_StringValue = value;
                OnPropertyChanged("stringValue");
            }
        }

        public override string Name
        {
            get { return "ControlTypesViewModel"; }
        }
    }
}
//// ReSharper restore InconsistentNaming