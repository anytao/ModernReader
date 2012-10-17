using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace OKr.Win8Book.Client.Controls
{
    public sealed partial class AppBarMarkButton : UserControl
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
                    this.btnMark.Opacity = 0;
                    this.btnMark.IsHitTestVisible = false;
                    this.btnUnMark.Opacity = 1;
                    this.btnUnMark.IsHitTestVisible = true;
                }
                else
                {
                    this.btnMark.Opacity = 1;
                    this.btnMark.IsHitTestVisible = true;
                    this.btnUnMark.Opacity = 0;
                    this.btnUnMark.IsHitTestVisible = false;
                }
            }
        }

        public static readonly DependencyProperty IsMarkedProperty =
            DependencyProperty.Register("IsMarked", typeof(bool), typeof(AppBarMarkButton), new PropertyMetadata(false));

        #endregion

        public AppBarMarkButton()
        {
            this.InitializeComponent();
        }

        public event EventHandler OnMarked;
        public event EventHandler OnUnMarked;

        private void Mark_Click(object sender, RoutedEventArgs e)
        {
            if (OnMarked!=null)
            {
                OnMarked(this, EventArgs.Empty);
            }
            IsMarked = true;
        }

        private void UnMark_Click(object sender, RoutedEventArgs e)
        {
            if (OnUnMarked != null)
            {
                OnUnMarked(this, EventArgs.Empty);
            }
            IsMarked = false;
        }
    }
}
