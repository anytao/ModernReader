using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace OKr.Win8Book.Client.Controls
{
    public sealed partial class AppBarThemeButton : UserControl
    {
        #region Property

        protected App App { get { return App.Current as App; } }

        public bool IsLightTheme
        {
            get { return (bool)GetValue(IsLightThemeProperty); }
            set
            {
                SetValue(IsLightThemeProperty, value);
                if (value)
                {
                    this.btnLightTheme.Opacity = 0;
                    this.btnLightTheme.IsHitTestVisible = false;
                    this.btnDarkTheme.Opacity = 1;
                    this.btnDarkTheme.IsHitTestVisible = true;
                }
                else
                {
                    this.btnLightTheme.Opacity = 1;
                    this.btnLightTheme.IsHitTestVisible = true;
                    this.btnDarkTheme.Opacity = 0;
                    this.btnDarkTheme.IsHitTestVisible = false;
                }
            }
        }

        public static readonly DependencyProperty IsLightThemeProperty =
            DependencyProperty.Register("IsLightTheme", typeof(bool), typeof(AppBarThemeButton), new PropertyMetadata(true));

        #endregion

        public AppBarThemeButton()
        {
            this.InitializeComponent();
            this.Loaded += AppBarThemeButton_Loaded;
        }

        void AppBarThemeButton_Loaded(object sender, RoutedEventArgs e)
        {
            this.IsLightTheme = App.IsLightTheme;
        }

        public event EventHandler OnLight;
        public event EventHandler OnDark;

        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            if (OnLight!=null)
            {
                OnLight(this, EventArgs.Empty);
            }
            IsLightTheme = true;
        }

        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
            if (OnDark != null)
            {
                OnDark(this, EventArgs.Empty);
            }
            IsLightTheme = false;
        }
    }
}
