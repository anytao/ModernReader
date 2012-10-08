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

        private HomeViewModel viewModel = new HomeViewModel();

        private Book book;
        private Mark mark;

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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.pr.IsActive = true;

            this.Init();

            this.pr.IsActive = false;
        }

        private async void Init()
        {
            BookContext bc = new BookContext();
            this.book = await bc.Load();

            this.DataContext = book;

            this.LoadMark();
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
                SetTheme(true);
            }
            else
            {
                SetTheme(false);
            }
        }

        #endregion

        private async void LoadMark()
        {
            MarkContext mc = new MarkContext();
            this.mark = await mc.Load();

            this.markSection.DataContext = this.mark;
        }

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
    }
}
