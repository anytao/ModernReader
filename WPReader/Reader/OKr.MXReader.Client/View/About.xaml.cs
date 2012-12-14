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
using OKr.MXReader.Client.Core;
using OKr.MXReader.Client.Core.Context;
using At.Phone.Common.Utils;

namespace OKr.MXReader.Client.View
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.Init();
        }

        private void Init()
        {
            this.tbApp.Text = "好阅：" + OkrBookContext.Current.App.AppName + ", v" + OkrBookContext.Current.App.Version;
            //this.tbBuild.Text = "Build: " + OkrBookContext.Current.App.Build;
            this.tbUs.Text = OkrBookContext.Current.App.Us;
        }

        private void OnUs(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/View/Shared/OkrBrowser.xaml?url={0}", OkrBookContext.Current.App.Url), UriKind.RelativeOrAbsolute));
        }

        private void OnMore(object sender, RoutedEventArgs e)
        {
            OKrHelper.Dowith(OkrBookContext.Current.App.AppName, OkrBookContext.Current.App.Email);
        }

        private void OnOKr(object sender, MouseButtonEventArgs e)
        {
            //NavigationService.Navigate(new Uri(string.Format("/View/Shared/OkrBrowser.xaml?url={0}", OkrBookContext.Current.App.Url), UriKind.RelativeOrAbsolute));
        }

        private void OnUpdate(object sender, EventArgs e)
        {
            string appId = WMAppManifestUtils.GetWMAppManifest().ProductID;

            OKrHelper.Download(appId);
        }
    }
}