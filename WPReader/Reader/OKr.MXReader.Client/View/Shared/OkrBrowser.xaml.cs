using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using At.Phone.Common.Utils;

namespace OKr.MXReader.Client.View.Shared
{
    public partial class OkrBrowser : PhoneApplicationPage
    {
        public OkrBrowser()
        {
            InitializeComponent();

            this.browser.Loaded += (sender, ex) =>
            {
                string url = NavigationContext.QueryString["url"];
                this.browser.Navigate(new Uri(url, UriKind.RelativeOrAbsolute));
            };
        }

        private void OnNavigating(object sender, NavigatingEventArgs e)
        {
            this.loader.Start();
            this.browser.Visibility = Visibility.Collapsed;
        }

        private void OnNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string url = e.Uri.OriginalString;
            if (PhoneHelper.IsMedia(url))
            {
                MediaPlayerLauncher launcher = new MediaPlayerLauncher();
                launcher.Media = new Uri(url, UriKind.RelativeOrAbsolute);
                launcher.Show();
            }

            this.loader.Stop();
            this.browser.Visibility = Visibility.Visible;
        }

        private void OnLoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            this.loader.Stop();
            this.browser.Visibility = Visibility.Visible;
        }
    }
}