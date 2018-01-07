using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WINsiderIoT.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for view models
    /// Source of inspiration: 
    /// - Author: James Montemagno (https://github.com/jamesmontemagno)
    /// - Github: https://github.com/jamesmontemagno/PlanetXamarin/blob/master/StarterCompatible/PlanetXamarin.Portable/Helpers/ViewModelBase.cs
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <inheritdoc />
        /// <summary>
        /// Property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// When a property is changed notify the Ui.
        /// </summary>
        /// <param name="propName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private bool _isBusy;
        /// <summary>
        /// Gets or sets if the View Model is busy
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
    }
}
