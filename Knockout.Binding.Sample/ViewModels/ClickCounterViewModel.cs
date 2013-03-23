//// ReSharper disable InconsistentNaming
namespace Knockout.Binding.Sample.ViewModels
{
    /*
     var ClickCounterViewModel = function() {
        this.numberOfClicks = ko.observable(0);
 
        this.registerClick = function() {
            this.numberOfClicks(this.numberOfClicks() + 1);
        };
 
        this.resetClicks = function() {
            this.numberOfClicks(0);
        };
 
        this.hasClickedTooManyTimes = ko.computed(function() {
            return this.numberOfClicks() >= 3;
        }, this);
    };
 */

    internal class ClickCounterViewModel : ViewModelBase
    {
        private int m_NumberOfClicks;
        
        public int numberOfClicks
        {
            get { return m_NumberOfClicks; }
            set
            {
                m_NumberOfClicks = value;
                OnPropertyChanged("numberOfClicks");
                OnPropertyChanged("hasClickedTooManyTimes");
            }
        }

        public bool hasClickedTooManyTimes
        {
            get { return numberOfClicks >= 3; }
            set { /*TODO: Need one way bindings*/}
        }

        public void registerClick()
        {
            numberOfClicks++;
        }

        public void resetClicks()
        {
            numberOfClicks = 0;
        }

        public override string Name
        {
            get { return "ClickCounterViewModel"; }
        }
    }
}
//// ReSharper restore InconsistentNaming