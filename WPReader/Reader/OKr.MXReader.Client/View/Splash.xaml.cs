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

namespace OKr.MXReader.Client.View
{
    public partial class Splash : PhoneApplicationPage
    {
        public Splash()
        {
            InitializeComponent();

            this.sbSplash.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            this.sbSplash.Begin();
        }

        private void OnCompleted(object sender, EventArgs e)
        {
            this.Init();
        }

        private void Init()
        {
            Initilizer.InitData();

            NavigationService.Navigate(new Uri(OkrConstant.HOMEVIEW, UriKind.Relative));
        }
    }
}