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

        public NavBar(OKrPageBase page, bool homeEnabled, bool chapterEnabled, bool markEnabled)
        {
            this.InitializeComponent();
            this.page = page;
            this.btnHome.IsEnabled = homeEnabled;
            this.btnChapter.IsEnabled = chapterEnabled;
            this.btnMark.IsEnabled = markEnabled;
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

    }
}
