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
    public sealed partial class BigMark : UserControl
    {
        #region Property

        public bool IsMarked
        {
            get { return (bool)GetValue(IsMarkedProperty); }
            set
            {
                SetValue(IsMarkedProperty, value);
                if (value)
                {
                    VisualStateManager.GoToState(this, "Marked", true);
                }
                else
                {
                    VisualStateManager.GoToState(this, "UnMarked", true);
                }
            }
        }

        public static readonly DependencyProperty IsMarkedProperty =
            DependencyProperty.Register("IsMarked", typeof(bool), typeof(AppBarMarkButton), new PropertyMetadata(false));

        #endregion
        public BigMark()
        {
            this.InitializeComponent();
        }

        public event EventHandler OnMark;

        private void BigMark_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (OnMark != null)
            {
                OnMark(this, EventArgs.Empty);
            }
            //IsMarked = !IsMarked;
        }
    }
}
