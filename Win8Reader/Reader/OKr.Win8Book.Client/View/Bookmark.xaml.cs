using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OKr.Win8Book.Client.Common;
using OKr.Win8Book.Client.Controls;
using OKr.Win8Book.Client.Core.Context;
using OKr.Win8Book.Client.Core.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using OKr.Win8Book.Client.ViewModel;

namespace OKr.Win8Book.Client.View
{
    public sealed partial class Bookmark : OKrPageBase
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

        #region Ctor

        public Bookmark()
        {
            this.InitializeComponent();
            this.TopAppBar = new NavBar(this, true, true, false);
            this.DataContext = viewModel;
        }

        #endregion

        #region Lifecycle

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //if (e.NavigationMode == NavigationMode.New)
            //{
            //    BookContext bc = new BookContext();
            //    var book = await bc.Load();

            //    this.pageTitle.Text = book.Name;

            //    MarkContext mc = new MarkContext();
            //    this.DataContext = await mc.Load();
            //}

            LoadTheme();
        }

        #endregion

        #region Handlers

        private void OnMarkItemClick(object sender, ItemClickEventArgs e)
        {
            ChapterMark data = e.ClickedItem as ChapterMark;

            Chapter chapter = new Chapter();
            chapter.Title = data.Title;
            chapter.ChapterNo = data.ChapterNo;
            chapter.PageCount = data.Current;

            this.Frame.Navigate(typeof(Viewer), chapter);
        }

        #endregion

        private void OnTheme(object sender, RoutedEventArgs e)
        {
            SwitchTheme();
            HideAppBars();
        }
    }
}
