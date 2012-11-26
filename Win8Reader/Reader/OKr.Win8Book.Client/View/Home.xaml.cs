using OKr.Win8Book.Client.Core.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using OKr.Win8Book.Client.Common;
using System;
using Windows.UI.Xaml.Controls;
using OKr.Win8Book.Client.ViewModel;
using Windows.UI.ApplicationSettings;
using OKr.Win8Book.Client.Controls;
using Windows.UI.Core;
using Windows.System;
using System.Linq;
using Windows.UI.Xaml.Media.Animation;

namespace OKr.Win8Book.Client.View
{
    public sealed partial class Home : OKrPageBase
    {
        #region Properties

        HomeViewModel viewModel
        {
            get
            {
                return App.HomeViewModel;
            }
        }

        #endregion

        #region Ctor

        public Home()
        {
            this.InitializeComponent();
            this.TopAppBar = new NavBar(this, false, true, true);
            SettingsPane.GetForCurrentView().CommandsRequested += SettingCommandsRequested;
        }

        #endregion

        #region Lifecycle

        private bool DataLoaded = false;
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (!DataLoaded)
            {
                this.pr.IsActive = true;
                await viewModel.Load();
                this.pr.IsActive = false;

                this.DataContext = viewModel;
                PrepareCover();
                //continueReadingPanel.Visibility = viewModel.Progress == null ? Visibility.Collapsed : Visibility.Visible;
                DataLoaded = true;
            }

            LoadTheme();
        }

        #endregion

        #region Hanlders

        private void Chapters_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(Viewer), e.ClickedItem as Chapter);
        }

        private void Mark_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChapterMark data = e.ClickedItem as ChapterMark;

            Chapter chapter = new Chapter();
            chapter.Title = data.Title;
            chapter.ChapterNo = data.ChapterNo;
            //chapter.PageCount = data.Current;
            chapter.Pos = data.Current;

            this.Frame.Navigate(typeof(Viewer), chapter);
        }

        private async void App_ItemClick(object sender, ItemClickEventArgs e)
        {
            OKrApp app = e.ClickedItem as OKrApp;

            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:PDP?PFN=" + app.PId, UriKind.RelativeOrAbsolute));
        }

        private void OnGoChapter(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Catalog));
        }

        private void OnGoMark(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Bookmark));
        }

        private void OnGoChapter(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Catalog));
        }

        private void OnGoMark(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Bookmark));
        }

        #endregion

        #region Setting

        private void SettingCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            scAbout = new SettingsCommand("About", "关于", async (x) =>
            {
            });

            scFeedback = new SettingsCommand("Feedback", "用户反馈", async (x) =>
            {
                var success = await Windows.System.Launcher.LaunchUriAsync(new Uri("mailto:service@okr.me", UriKind.RelativeOrAbsolute));
            });

            scPrivacy = new SettingsCommand("Privacy", "隐私策略", async (x) =>
            {
                var success = await Windows.System.Launcher.LaunchUriAsync(new Uri("http://okrwork.sinaapp.com/im/5", UriKind.RelativeOrAbsolute));
            });


            if (!args.Request.ApplicationCommands.Contains(scAbout))
            {
                args.Request.ApplicationCommands.Add(scAbout);
            }

            if (!args.Request.ApplicationCommands.Contains(scFeedback))
            {
                args.Request.ApplicationCommands.Add(scFeedback);
            }

            if (!args.Request.ApplicationCommands.Contains(scPrivacy))
            {
                args.Request.ApplicationCommands.Add(scPrivacy);
            }
        }

        #endregion

        #region App Bar

        private void AppBarThemeButton_Click(object sender, EventArgs e)
        {
            SwitchTheme();
            HideAppBars();
        }

        #endregion

        #region Cover

        private void PrepareCover()
        {
            this.cover.PrepareCover(this);
        }

        #endregion

        #region Continue Reading

        private void coverContinueReadingPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.cover.Unlock(false);
            this.Frame.Navigate(typeof(Viewer), "p");
        }

        private void Snapped_Completed(object sender, object e)
        {
            this.cover.Unlock(false);
        }

        #endregion

        #region Variables

        private SettingsCommand scAbout;
        private SettingsCommand scPrivacy;
        private SettingsCommand scFeedback;

        #endregion

    }
}
