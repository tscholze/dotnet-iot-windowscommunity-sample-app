using System;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WINsiderIoT.Models;
using WINsiderIoT.ViewModels;

namespace WINsiderIoT
{
    /// <summary>
    /// This page is responsible to present a Hub element with 
    /// retrieved feed items.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Private members

        /// <summary>
        /// Gets or sets the view model of this page.
        /// </summary>
        private readonly FeedViewModel _viewModel;

        #endregion

        #region Constructor

        public MainPage()
        {
            InitializeComponent();

            // Customize title bar
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            // Instantiate view model
            _viewModel = new FeedViewModel();
            DataContext = _viewModel;
        }

        #endregion

        #region Event handler

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _viewModel.LoadItemsCommand.Execute(null);
        }

        private async void OnListItemClicked(object sender, ItemClickEventArgs e)
        {
            var item = (FeedItem)e.ClickedItem;
            await Launcher.LaunchUriAsync(item.Uri);
        }

        #endregion
    }
}
