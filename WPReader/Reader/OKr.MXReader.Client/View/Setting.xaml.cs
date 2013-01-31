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
using OKr.MXReader.Client.Core.Context;
using System.Windows.Media.Imaging;
using OKr.MXReader.Client.Core.Config;
using OKr.MXReader.Client.Core.Data;
using System.IO.IsolatedStorage;

namespace OKr.MXReader.Client.View
{
    public partial class Setting : PhoneApplicationPage
    {
        public Setting()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.Init();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            this.Save();
        }

        private void Save()
        {
            IsolatedStorageSettings.ApplicationSettings["set"] = this.set;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void Init()
        {
            this.tbIntro.Text = OkrBookContext.Current.Config.Intro;

            IsolatedStorageSettings.ApplicationSettings.TryGetValue<BookSetting>("set", out this.set);
            if (this.set == null)
            {
                this.set = new BookSetting();
                IsolatedStorageSettings.ApplicationSettings["set"] = this.set;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }

            this.tsRemind.IsChecked = this.set.IsNightMode;
            this.tbRemind.Text = this.set.IsNightMode ? "开启" : "关闭";
            this.tbIntro.Foreground = this.set.IsNightMode ? OkrBookConfig.WBrush : OkrBookConfig.BBrush;

            FontSet(this.set.FontSize);
        }

        private void OnFont18(object sender, RoutedEventArgs e)
        {
            FontSet(18);

            this.set.FontSize = 18;
        }

        private void OnFont20(object sender, RoutedEventArgs e)
        {
            FontSet(20);

            this.set.FontSize = 20;
        }

        private void OnFont24(object sender, RoutedEventArgs e)
        {
            FontSet(24);

            this.set.FontSize = 24;
        }

        private void OnFont36(object sender, RoutedEventArgs e)
        {
            FontSet(36);

            this.set.FontSize = 36;
        }

        private void OnFont48(object sender, RoutedEventArgs e)
        {
            FontSet(48);

            this.set.FontSize = 48;
        }

        private void FontSet(int font)
        {
            switch (font)
            {
                case 18:
                    this.btn18.Background = greenBrush;
                    this.btn20.Background = lightGreenBrush;
                    this.btn24.Background = lightGreenBrush;
                    this.btn36.Background = lightGreenBrush;
                    this.btn48.Background = lightGreenBrush;

                    this.tbIntro.FontSize = 18;
                    break;
                case 20:
                    this.btn18.Background = lightGreenBrush;
                    this.btn20.Background = greenBrush;
                    this.btn24.Background = lightGreenBrush;
                    this.btn36.Background = lightGreenBrush;
                    this.btn48.Background = lightGreenBrush;

                    this.tbIntro.FontSize = 20;
                    break;
                case 24:
                    this.btn18.Background = lightGreenBrush;
                    this.btn20.Background = lightGreenBrush;
                    this.btn24.Background = greenBrush;
                    this.btn36.Background = lightGreenBrush;
                    this.btn48.Background = lightGreenBrush;

                    this.tbIntro.FontSize = 24;
                    break;
                case 36:
                    this.btn18.Background = lightGreenBrush;
                    this.btn20.Background = lightGreenBrush;
                    this.btn24.Background = lightGreenBrush;
                    this.btn36.Background = greenBrush;
                    this.btn48.Background = lightGreenBrush;

                    this.tbIntro.FontSize = 36;
                    break;
                case 48:
                    this.btn18.Background = lightGreenBrush;
                    this.btn20.Background = lightGreenBrush;
                    this.btn24.Background = lightGreenBrush;
                    this.btn36.Background = lightGreenBrush;
                    this.btn48.Background = greenBrush;

                    this.tbIntro.FontSize = 48;
                    break;
                default:
                    break;
            }
        }

        private void OnRemindChecked(object sender, RoutedEventArgs e)
        {
            this.tbRemind.Text = "开启";

            this.set.IsNightMode = true;
            this.set.Theme = "bbg-night.png";

            //IsolatedStorageSettings.ApplicationSettings["set"] = this.set;
            //IsolatedStorageSettings.ApplicationSettings.Save();

            ImageBrush brush = new ImageBrush();
            brush.ImageSource = bbgNight;

            this.wrapper.Background = brush;
            this.tbIntro.Foreground = OkrBookConfig.WBrush;
            this.tbIntro.InvalidateMeasure();
        }

        private void OnRemindUnchecked(object sender, RoutedEventArgs e)
        {
            this.tbRemind.Text = "关闭";

            this.set.IsNightMode = false;
            this.set.Theme = "bbg.png";

            //IsolatedStorageSettings.ApplicationSettings["set"] = this.set;
            //IsolatedStorageSettings.ApplicationSettings.Save();

            ImageBrush brush = new ImageBrush();
            brush.ImageSource = bbg;

            this.wrapper.Background = brush;
            this.tbIntro.Foreground = OkrBookConfig.BBrush;
            this.tbIntro.InvalidateMeasure();
        }

        private Brush greenBrush = (Brush)Application.Current.Resources["okr-color-green"];
        private Brush lightGreenBrush = (Brush)Application.Current.Resources["okr-color-lightgreen"];

        private BitmapImage bbg = new BitmapImage(new Uri("/_static/img/bbg.png", UriKind.Relative));
        private BitmapImage bbgNight = new BitmapImage(new Uri("/_static/img/bbg-night.png", UriKind.Relative));
        private BookSetting set;


    }
}