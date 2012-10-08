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
        public const string SkipTheme = "skiptheme";

        #endregion

        #region Theme

        private bool childrenCollected = false;
        List<TextBlock> textBlocks = new List<TextBlock>();
        Grid pageRootGrid = null;

        protected void SwitchTheme(bool light)
        {
            SolidColorBrush Theme_Foreground = null;
            ImageBrush pageBackgroundBrush = null;

            if (light)
            {
                Theme_Foreground = App.Resources["OKr_Theme_Foreground_Light"] as SolidColorBrush;
                pageBackgroundBrush = App.Resources["OKr_Theme_PageBackground_Light"] as ImageBrush;
            }
            else
            {
                Theme_Foreground = App.Resources["OKr_Theme_Foreground_Dark"] as SolidColorBrush;
                pageBackgroundBrush = App.Resources["OKr_Theme_PageBackground_Dark"] as ImageBrush;
            }

            if (!childrenCollected)
            {
                GetChildrenOfType<TextBlock>(this, textBlocks);
                pageRootGrid = GetFirstChildOfType<Grid>(this);
                childrenCollected = true;
            }

            foreach (var textBlock in textBlocks)
            {
                if (textBlock.Tag != null && textBlock.Tag.ToString().ToLower() == SkipTheme)
                {
                    continue;
                }
                else
                {
                    textBlock.Foreground = Theme_Foreground;
                }
            }

            pageRootGrid.Background = pageBackgroundBrush;
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

        public T GetFirstChildOfType<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetFirstChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }

        #endregion

    }
}
