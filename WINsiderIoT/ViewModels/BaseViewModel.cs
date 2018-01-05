using System.ComponentModel;

namespace WINsiderIoT.ViewModels
{
    /// <summary>
    /// Base class for view models
    /// Source of inspiration: 
    /// - Author: James Montemagno (https://github.com/jamesmontemagno)
    /// - Github: https://github.com/jamesmontemagno/PlanetXamarin/blob/master/StarterCompatible/PlanetXamarin.Portable/Helpers/ViewModelBase.cs
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// When a property is changed notify the Ui.
        /// </summary>
        /// <param name="propName"></param>
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private bool isBusy;

        /// <summary>
        /// Gets or sets if the View Model is busy
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set {
                isBusy = value;
                OnPropertyChanged("IsBusy"); }
        }
    }
}
