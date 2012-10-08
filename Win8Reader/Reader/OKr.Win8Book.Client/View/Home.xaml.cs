using OKr.Win8Book.Client.Core.Context;
using OKr.Win8Book.Client.Core.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using OKr.Win8Book.Client.Common;
using Windows.UI.Xaml.Media;
using System.Collections.Generic;
using System;
using Windows.UI.Xaml.Controls;
using OKr.Win8Book.Client.ViewModel;
using Windows.UI.ApplicationSettings;
using OKr.Win8Book.Client.Controls;

namespace OKr.Win8Book.Client.View
{
    public sealed partial class Home : OKrPageBase
    {
        #region Ctor

        public Home()
        {
            this.InitializeComponent();

            this.TopAppBar = new NavBar(this, false, true, true);

            SettingsPane.GetForCurrentView().CommandsRequested += SettingCommandsRequested;
        }

        #endregion

        #region Properties

        HomeViewModel viewModel
        {
            get
            {
                return App.HomeViewModel;
            }
        }

        #endregion

        #region Hanlders

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
                DataLoaded = true;
            }

            LoadTheme();
        }

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
            chapter.PageCount = data.Current;

            this.Frame.Navigate(typeof(Viewer), chapter);
        }

        private void App_ItemClick(object sender, ItemClickEventArgs e)
        {

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

        private void OnTheme(object sender, RoutedEventArgs e)
        {
            SwitchTheme();
            HideAppBars();
        }

        #endregion

        #region Variables

        private SettingsCommand scAbout;
        private SettingsCommand scPrivacy;
        private SettingsCommand scFeedback;

        #endregion
    }
}
