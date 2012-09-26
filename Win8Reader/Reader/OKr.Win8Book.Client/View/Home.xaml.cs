using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OKr.Win8Book.Client.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Windows.UI.Xaml.Controls.Page
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
            this.pr.IsActive = true;

            this.Init();

            this.pr.IsActive = false;
        }

        private async void Init()
        {
            BookContext bc = new BookContext();
            this.book = await bc.Load();

            this.DataContext = book;
        }

        private void OnCategory(object sender, TappedRoutedEventArgs e)
        {
            var category = (sender as FrameworkElement).DataContext as Chapter;
            this.Frame.Navigate(typeof(Viewer), category);
        }
    }
}
