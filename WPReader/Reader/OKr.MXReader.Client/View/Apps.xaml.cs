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
using OKr.MXReader.Client.Core.Data;

namespace OKr.MXReader.Client.View
{
    public partial class Apps : PhoneApplicationPage
    {
        public Apps()
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
            IList<AppInfo> data = AppManager.Apps;

            this.applist.DataContext = data;
        }

        private void OnGetit(object sender, RoutedEventArgs e)
        {
            string appId = ((Button)sender).Tag.ToString();

            OKrHelper.Download(appId);
        }
    }
}