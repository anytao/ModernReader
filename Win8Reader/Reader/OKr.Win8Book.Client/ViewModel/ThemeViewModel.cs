using OKr.Common.Data;
using OKr.Win8Book.Client.Core.Context;
using OKr.Win8Book.Client.Core.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace OKr.Win8Book.Client.ViewModel
{
    public class ThemeViewModel : BindableBase
    {
        #region Properties

        protected App App { get { return App.Current as App; } }

        private SolidColorBrush themeForeground = new SolidColorBrush();
        public SolidColorBrush ThemeForeground
        {
            get 
            { 
                return themeForeground; 
            }
            set
            {
                this.SetProperty(ref themeForeground, value);
            }
        }

        #endregion

        public ThemeViewModel()
        {
        }

        public void Load()
        {
            SetTheme(App.IsLightTheme);
        }

        #region Theme

        public void SetTheme(bool light)
        {
            App.IsLightTheme = light;
            if (light)
            {
                ThemeForeground = App.Resources["OKr_Theme_Foreground_Light"] as SolidColorBrush;
            }
            else
            {
                ThemeForeground = App.Resources["OKr_Theme_Foreground_Dark"] as SolidColorBrush;
            }
        }

        #endregion

    }
}
