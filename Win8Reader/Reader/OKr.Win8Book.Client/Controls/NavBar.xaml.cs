using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OKr.Win8Book.Client.View;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using OKr.Win8Book.Client.Common;

namespace OKr.Win8Book.Client.Controls
{
    public sealed partial class NavBar : AppBar
    {
        private OKrPageBase page = null;

        public NavBar(OKrPageBase page, bool homeEnabled, bool chapterEnabled, bool markEnabled, bool viewEnabled = false)
        {
            this.InitializeComponent();
            this.page = page;
            this.btnHome.IsEnabled = homeEnabled;
            this.btnChapter.IsEnabled = chapterEnabled;
            this.btnMark.IsEnabled = markEnabled;

            if (viewEnabled)
            {
                this.btnPre.Visibility = Visibility.Visible;
                this.btnNext.Visibility = Visibility.Visible;
            }
            else
            {
                this.btnPre.Visibility = Visibility.Collapsed;
                this.btnNext.Visibility = Visibility.Collapsed;
            }
        }

        private void OnHome(object sender, RoutedEventArgs e)
        {
            page.HideAppBars();
            this.page.Frame.Navigate(typeof(Home));
        }

        private void OnChapter(object sender, RoutedEventArgs e)
        {
            page.HideAppBars();
            this.page.Frame.Navigate(typeof(Catalog));
        }

        private void OnMark(object sender, RoutedEventArgs e)
        {
            page.HideAppBars();
            this.page.Frame.Navigate(typeof(Bookmark));
        }

        private void OnPre(object sender, RoutedEventArgs e)
        {
            RaisePre(e);
        }

        private void OnNext(object sender, RoutedEventArgs e)
        {
            RaiseNext(e);
        }

        public delegate void ClickHandler(object sender, RoutedEventArgs e);
        public event ClickHandler Pre;
        public event ClickHandler Next;

        private void RaisePre(RoutedEventArgs e)
        {
            if (Pre != null)
            {
                Pre(this, e);
            }
        }

        private void RaiseNext(RoutedEventArgs e)
        {
            if (Next != null)
            {
                Next(this, e);
            }
        }
    }
}
