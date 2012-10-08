using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace OKr.Win8Book.Client.Common
{
    public partial class LayoutAwarePage
    {
        #region Properties

        protected App App { get { return App.Current as App; } }

        #endregion

        #region Theme

        private bool childrenCollected = false;
        List<TextBlock> textBlocks = new List<TextBlock>();

        protected void SwitchTheme(bool light)
        {
            SolidColorBrush Theme_Foreground = null;
            if (light)
            {
                Theme_Foreground = App.Resources["OKr_Light_Theme_Foreground"] as SolidColorBrush;
            }
            else
            {
                Theme_Foreground = App.Resources["OKr_Dark_Theme_Foreground"] as SolidColorBrush;

            }

            if (!childrenCollected)
            {
                GetChildrenOfType<TextBlock>(this, textBlocks);
                childrenCollected = true;
            }

            foreach (var textBlock in textBlocks)
            {
                textBlock.Foreground = Theme_Foreground;
            }
        }

        public void GetChildrenOfType<T>(DependencyObject depObj, List<T> result) where T : DependencyObject
        {
            if (depObj == null) return;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var controlOfType = child as T;
                if (controlOfType != null)
                {
                    result.Add(controlOfType);
                }

                GetChildrenOfType<T>(child, result);
            }
        }

        #endregion

    }
}
