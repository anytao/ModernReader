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

        //private bool childrenCollected = false;
        List<TextBlock> textBlocks = null;
        Grid pageRootGrid = null;
        FrameworkElement backButton = null;
        List<RichTextBlock> richTextBlocks = null;

        protected void LoadTheme()
        {
            //TO-DO: load last set theme
            SetTheme(App.IsLightTheme);
        }

        protected void SwitchTheme()
        {
            SetTheme(!App.IsLightTheme);
        }

        protected void SetTheme(bool light)
        {
            App.IsLightTheme = light;

            this.UpdateLayout();

            SolidColorBrush Theme_Foreground = null;
            ImageBrush pageBackgroundBrush = null;
            Style backButtonStyle = null;

            if (light)
            {
                Theme_Foreground = App.Resources["OKr_Theme_Foreground_Light"] as SolidColorBrush;
                pageBackgroundBrush = App.Resources["OKr_Theme_PageBackground_Light"] as ImageBrush;
                backButtonStyle = App.Resources["OKrBackButton_Light_Style"] as Style;
            }
            else
            {
                Theme_Foreground = App.Resources["OKr_Theme_Foreground_Dark"] as SolidColorBrush;
                pageBackgroundBrush = App.Resources["OKr_Theme_PageBackground_Dark"] as ImageBrush;
                backButtonStyle = App.Resources["OKrBackButton_Dark_Style"] as Style;
            }

            //if (!childrenCollected)
            //{
            textBlocks = new List<TextBlock>();
            GetChildrenOfType<TextBlock>(this, textBlocks);
            richTextBlocks = new List<RichTextBlock>();
            GetChildrenOfType<RichTextBlock>(this, richTextBlocks);
            pageRootGrid = GetFirstChildOfType<Grid>(this);
            backButton = GetChildByName(this, "backButton");
            //childrenCollected = true;
            //}

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

            foreach (var rtb in richTextBlocks)
            {
                if (rtb.Tag != null && rtb.Tag.ToString().ToLower() == SkipTheme)
                {
                    continue;
                }
                else
                {
                    rtb.Foreground = Theme_Foreground;
                }
            }

            if (backButton!=null)
            {
                backButton.Style = backButtonStyle;
                backButton.UpdateLayout();
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
                if (result != null) 
                    return result;
            }
            return null;
        }

        public FrameworkElement GetChildByName(DependencyObject depObj, string name)
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                string controlName = child.GetValue(Control.NameProperty) as string;

                var result = (controlName == name) ? child : GetChildByName(child, name);

                if (result != null)
                {
                    return result as FrameworkElement;
                }
            }
            return null;
        }

        #endregion

    }
}
