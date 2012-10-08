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

namespace OKr.Win8Book.Client.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : LayoutAwarePage
    {
        private Book book;
        private Mark mark;

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
                SwitchTheme(true);
            }
            else
            {
                SwitchTheme(false);
            }
        }

        #endregion

        private async void LoadMark()
        {
            MarkContext mc = new MarkContext();
            this.mark = await mc.Load();

            foreach (var mark in this.mark.Marks)
            {
                ChapterMark tmp = mark;
                //BookmarkItem item = new BookmarkItem();
                //item.DataContext = tmp;

                //item.Click += (sender, ex) =>
                //{
                //    Progress progress = null;

                //    IsolatedStorageSettings.ApplicationSettings.TryGetValue<Progress>("current", out progress);
                //    if (progress == null)
                //    {
                //        progress = new Progress();
                //    }
                //    progress.Chapter = tmp.ChapterNo;
                //    progress.Page = tmp.Current;
                //    progress.Percent = tmp.Percent;
                //    IsolatedStorageSettings.ApplicationSettings["current"] = progress;

                //    //Dispatcher.BeginInvoke(() =>
                //    //{
                //    //    (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/View/Viewer.xaml", UriKind.Relative));
                //    //});
                // };

                //this.mlist.Items.Add(item);
            }
        }

    }
}
