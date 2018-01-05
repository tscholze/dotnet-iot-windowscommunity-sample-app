using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
        private FeedViewModel ViewModel { get; set; }

        #endregion

        #region Constructor

        public MainPage()
        {
            InitializeComponent();

            // Customize title bar
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            // Instantiate view model
            ViewModel = new FeedViewModel();
        }

        #endregion

        #region Event handler

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.LoadItemsCommand.Execute(null);
        }

        private async void OnListItemClicked(object sender, ItemClickEventArgs e)
        {
            var item = ((FeedItem)e.ClickedItem);
            await Windows.System.Launcher.LaunchUriAsync(item.Uri);
        }

        #endregion
    }
}
