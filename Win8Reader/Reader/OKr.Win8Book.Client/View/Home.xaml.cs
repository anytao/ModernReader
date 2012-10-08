using OKr.Win8Book.Client.Core.Context;
using OKr.Win8Book.Client.Core.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using OKr.Win8Book.Client.Common;
using Windows.UI.Xaml.Media;
using System.Collections.Generic;
using System;
using Windows.UI.Xaml.Controls;
using OKr.Win8Book.Client.ViewModel;

namespace OKr.Win8Book.Client.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : LayoutAwarePage
    {
        #region Properties

        HomeViewModel viewModel
        {
            get
            {
                return App.HomeViewModel;
            }
        }

        #endregion

        public Home()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                this.pr.IsActive = true;
                await viewModel.Load();
                this.pr.IsActive = false;

                this.DataContext = viewModel;
            }
            LoadTheme();
        }


        private void OnCategory(object sender, TappedRoutedEventArgs e)
        {
            var category = (sender as FrameworkElement).DataContext as Chapter;
            this.Frame.Navigate(typeof(Viewer), category);
        }

        #region Theme

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleSwitch).IsOn)
            {
                SetTheme(false);
            }
            else
            {
                SetTheme(true);
            }
        }

        #endregion

        private void Chapters_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(Viewer), e.ClickedItem as Chapter);
        }

        private void Mark_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChapterMark data = e.ClickedItem as ChapterMark;

            Chapter chapter = new Chapter();
            chapter.Title = data.Title;
            chapter.ChapterNo = data.ChapterNo;
            chapter.PageCount = data.Current;


            this.Frame.Navigate(typeof(Viewer), chapter);
        }

        private void App_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
