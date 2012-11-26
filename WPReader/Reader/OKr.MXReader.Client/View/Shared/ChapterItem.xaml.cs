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
using System.IO.IsolatedStorage;
using OKr.MXReader.Client.Core.Data;

namespace OKr.MXReader.Client.View.Shared
{
    public partial class ChapterItem : UserControl
    {
        public ChapterItem()
        {
            InitializeComponent();
        }

        private void OnRead(object sender, RoutedEventArgs e)
        {
            int chapterNo = int.Parse(((Button)sender).Tag.ToString());
            Progress progress = null;

            IsolatedStorageSettings.ApplicationSettings.TryGetValue<Progress>("current", out progress);
            if (progress == null)
            {
                progress = new Progress();
            }
            progress.Chapter = chapterNo;
            progress.Page = 0;
            progress.Percent = 0.0;
            IsolatedStorageSettings.ApplicationSettings["current"] = progress;

            Dispatcher.BeginInvoke(() => 
            {
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/View/Viewer.xaml", UriKind.Relative));
            });
        }
    }
}
