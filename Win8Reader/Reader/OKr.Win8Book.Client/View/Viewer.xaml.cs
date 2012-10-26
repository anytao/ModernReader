using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OKr.Common.Storage;
using OKr.Win8Book.Client.Core;
using OKr.Win8Book.Client.Core.Builder;
using OKr.Win8Book.Client.Core.Context;
using OKr.Win8Book.Client.Core.Data;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using OKr.Win8Book.Client.Common;
using Windows.UI.Xaml.Controls;
using OKr.Win8Book.Client.Controls;
using Windows.UI.Core;
using Windows.System;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.Foundation;
using Windows.UI.Xaml.Media;

namespace OKr.Win8Book.Client.View
{
    public sealed partial class Viewer : OKrPageBase
    {
        #region Ctor

        public Viewer()
        {
            this.InitializeComponent();
            this.Loaded += Viewer_Loaded;

            NavBar bar = new NavBar(this, true, true, true, true);
            bar.Pre += async (sender, ex) =>
            {
                await ToPre();
            };

            bar.Next += async (sender, ex) =>
            {
                await ToNext();
            };


            this.TopAppBar = bar;

            CoreDispatcher dispatcher = Window.Current.CoreWindow.Dispatcher;

            dispatcher.AcceleratorKeyActivated += (sender, ex) =>
            {
                VirtualKey key = ex.VirtualKey;
                if ((ex.EventType == CoreAcceleratorKeyEventType.KeyUp))
                {
                    if (((key == VirtualKey.Up) || (key == VirtualKey.Left)) || (key == VirtualKey.PageUp))
                    {
                        ex.Handled = true;

                        PrePage();
                    }
                    else if (((key == VirtualKey.Down) || (key == VirtualKey.Right)) || (key == VirtualKey.PageDown))
                    {
                        ex.Handled = true;
                        NextPage();
                    }
                }
            };
        }

        void Viewer_Loaded(object sender, RoutedEventArgs e)
        {
            PrepareFontPopup();
        }

        #endregion

        #region Lifecycle

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                // load settings
                SettingContext mc = new SettingContext();
                var setting = await mc.Load();

                this.fontsize = setting.Font;

                // load book from progress or chapter
                if (e.Parameter.ToString() == "p")
                {
                    await LoadProgress();
                }
                else
                {
                    var category = e.Parameter as Chapter;
                    await LoadBook(category);
                }                
            }

            LoadTheme();
        }

        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            var chapter = this.book.Chapters.FirstOrDefault(x => x.ChapterNo == this.currentChapter);

            if (chapter != null)
            {
                chapter.IsRead = true;

                await bc.Save(this.book);
            }
        }

        private async Task ToNext()
        {
            if (this.chapter.ChapterNo + 1 <= this.book.Chapters.Count)
            {
                var category = App.HomeViewModel.Book.Chapters.FirstOrDefault(x => x.ChapterNo == this.chapter.ChapterNo + 1);

                if (category != null)
                {
                    await LoadBook(category);
                }
            }
        }

        private async Task ToPre()
        {
            if (this.chapter.ChapterNo - 1 >= 0)
            {
                var category = App.HomeViewModel.Book.Chapters.FirstOrDefault(x => x.ChapterNo == this.chapter.ChapterNo - 1);

                if (category != null)
                {
                    await LoadBook(category);
                }
            }
        }

        #endregion

        #region Handlers

        bool dragging = false;
        private void bodyGrid_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            if (transitionInProcess)
            {
                return;
            }
            dragging = true;
        }

        private void bodyGrid_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (transitionInProcess)
            {
                return;
            }
            if (dragging)
            {
                var transform = (CompositeTransform)bodyGrid.RenderTransform;
                transform.TranslateX += e.Delta.Translation.X;
            }
        }

        private void bodyGrid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (transitionInProcess)
            {
                return;
            }
            dragging = false;
            if (e.Cumulative.Translation.X > 0)
            {
                FlipTransition(false);
            }
            else
            {
                FlipTransition(true);
            }
        }

        private void OnFontChange(object sender, RoutedEventArgs e)
        {
            ShowFontPopup((UIElement)sender);
        }

        private async void OnMark(object sender, EventArgs e)//(object sender, RoutedEventArgs e)
        {
            Mark m = this.chapter.Mark;

            if (m == null)
            {
                m = new Mark();
                this.chapter.Mark = m;
            }

            ChapterMark item = m.Marks.FirstOrDefault(x => x.Current == this.location && x.ChapterNo == this.currentChapter);

            if (item == null)
            {
                item = new ChapterMark();
                item.ChapterNo = this.currentChapter;
                item.Title = this.chapter.Title;
                item.Date = DateTime.Now.ToString("yyyy/mm/dd hh:MM:ss");
                item.Current = this.location;
                item.Percent = ((((double)this.current) / ((double)this.chapter.PageNum)) * 100).ToString("N2") + "%";
                OKr.Win8Book.Client.Core.Data.Page page = this.chapter.Pages[this.current];
                item.Content = page.Row[0].Trim() + page.Row[1].Trim();

                this.mark.Marks.Insert(0, item);

                this.bigMark.IsMarked = true;
                this.appBarMark.IsMarked = true;
            }
            else
            {
                int chapterNo = item.ChapterNo;

                List<ChapterMark> list = new List<ChapterMark>();
                list = this.mark.Marks.Where(x => x.ChapterNo == chapterNo && x.Current == this.location).ToList();

                foreach (var mark in list)
                {
                    this.mark.Marks.Remove(mark);
                }

                this.bigMark.IsMarked = false;
                this.appBarMark.IsMarked = false;
            }

            this.ShowMark();

            await this.mc.Save(this.mark);

            App.HomeViewModel.NotifyMarksChanged();
        }

        #endregion

        #region Private Methods

        private void ChangePage(bool nextPage)
        {
            if (nextPage)
            {
                NextPage();
            }
            else
            {
                PrePage();
            }

            this.SetPage();

            this.SetMarkStatus();
        }

        private async void SetChapter()
        {
            this.progress.Chapter = this.currentChapter;
            this.progress.Page = this.current;
            this.progress.Percent = "0.0";

            await pc.Save(this.progress);
        }

        private async Task<Chapter> LoadData(int index, string title)
        {
            int[] count = this.GetCounts(this.fontsize);

            OKrStorage storage = new OKrStorage();

            Windows.ApplicationModel.Package package = Windows.ApplicationModel.Package.Current;
            Windows.Storage.StorageFolder installedLocation = package.InstalledLocation;

            this.chapter = this.book.Chapters[index];

            var file = await StorageFile.GetFileFromPathAsync(Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, @"Assets\Data\book\" + this.chapter.FileName + ".txt"));

            var content = await FileIO.ReadTextAsync(file, Windows.Storage.Streams.UnicodeEncoding.Utf8);

            Chapter chapter = null;
            if (content != null)
            {
                chapter = TextParser.GetChapter(content, count);
                chapter.Title = title;
                chapter.ChapterNo = index;
            }

            return chapter;
        }

        private int[] GetCounts(int fontSize)
        {
            int[] numArray = new int[2];
            int num = 0;
            int num2 = 0;
            int num3 = (fontSize * 0x20) / 100;
            num2 = 460 / (fontSize - num3);
            num = this.height / (fontSize + this.lineHeight);
            numArray[0] = num;
            numArray[1] = num2;
            return numArray;
        }

        private async void NextPage()
        {
            if (this.current >= this.chapter.Pages.Count - 1)
            {
                await ToNext();
            }
            else
            {
                this.current++;

                this.chapter.CurrentPage = this.chapter.Pages[this.current];
                if (this.chapter.CurrentPage.Locations.Count > 0)
                {
                    this.location = this.chapter.CurrentPage.Locations[0];
                }

                this.UpdatePage();
            }
        }

        private async void PrePage()
        {
            if (this.current <= 0)
            {
                await ToPre();
            }
            else
            {
                this.current--;

                this.chapter.CurrentPage = this.chapter.Pages[this.current];
                if (this.chapter.CurrentPage.Locations.Count > 0)
                {
                    this.location = this.chapter.CurrentPage.Locations[0];
                }


                this.UpdatePage();
            }
        }

        private void ShowMark()
        {
            Mark mark = this.chapter.Mark;
            ChapterMark currentMark = null;
            if (mark != null)
            {
                foreach (var item in mark.Marks)
                {
                    if (item.Current == this.current)
                    {
                        currentMark = item;
                        break;
                    }
                }
            }
        }

        private async void SetPage()
        {
            this.progress.Chapter = this.chapter.ChapterNo;
            this.progress.Page = this.current;
            this.progress.Percent = ((((double)this.current) / ((double)this.chapter.PageNum)) * 100).ToString("N2") + "%";

            if (this.chapter.CurrentPage != null)
            {
                this.progress.Location = this.chapter.CurrentPage.Locations[0];
                this.progress.Text = this.chapter.CurrentPage.Row[0] + " " + this.chapter.CurrentPage.Row[1];
            }

            await pc.Save(this.progress);
        }

        private async Task LoadBook(Chapter category)
        {
            this.currentChapter = category.ChapterNo;

            this.chapter = category;
            this.current = category.PageCount;
            this.location = category.Pos;

            this.book = App.HomeViewModel.Book;
            this.chapter = await LoadData(this.currentChapter, category.Title);
            chapter.CurrentPage = GetCurrent(chapter, this.location);

            this.DataContext = chapter;

            this.mark = await mc.Load();
            this.progress = await pc.Load();

            this.chapter.Mark = this.mark;

            this.pageTitle.Text = this.book.Name;

            this.SetMarkStatus();

            this.UpdatePage();
        }

        private async Task LoadProgress()
        {
            this.progress = await pc.Load();

            Chapter chapter = null;

            if (this.progress != null)
            {
                chapter = this.book.Chapters.FirstOrDefault(x => x.ChapterNo == this.progress.Chapter);
            }
            else
            {
                chapter = this.book.Chapters[0];
            }

            if (chapter != null)
            {
                chapter.PageCount = this.progress.Page;
                chapter.Pos = this.progress.Location;

                await LoadBook(chapter);
            }
        }

        private async void UpdatePage()
        {
            int index = chapter.Pages.FindIndex(x => x.CharNum == chapter.CurrentPage.CharNum);

            if (index < this.chapter.Pages.Count - 1)
            {
                chapter.NextPage = chapter.Pages[index + 1];
            }
            else
            {
                chapter.NextPage = null;

                //if (this.currentChapter < this.book.Chapters.Count - 1)
                //{
                //    var nextChapter = await LoadData(this.currentChapter + 1, "");

                //    chapter.NextPage = nextChapter.Pages[0];
                //}
            }

            if (index > 0)
            {
                chapter.PrePage = chapter.Pages[index - 1];
            }
            else
            {
                chapter.PrePage = null;

                //if (this.currentChapter > 0)
                //{
                //    var preChapter = await LoadData(this.currentChapter - 1, "");
                //    chapter.PrePage = preChapter.Pages[0];
                //}
                //else
                //{
                //    chapter.PrePage = null;
                //}
            }
        }

        private void SetMarkStatus()
        {
            ChapterMark item = this.mark.Marks.FirstOrDefault(x => x.Current == this.location && x.ChapterNo == this.currentChapter);

            this.bigMark.IsMarked = item != null;
            this.appBarMark.IsMarked = item != null;
        }

        private OKr.Win8Book.Client.Core.Data.Page GetCurrent(Chapter chapter, int pos)
        {
            if (chapter != null)
            {
                if (chapter.Pages != null && chapter.Pages.Count > 0)
                {
                    foreach (OKr.Win8Book.Client.Core.Data.Page page in chapter.Pages)
                    {
                        if (page.Locations.Contains(pos))
                        {
                            return page;
                        }
                    }
                }
            }

            return null;
        }

        #endregion

        #region App Bar

        private void AppBarThemeButton_Click(object sender, EventArgs e)
        {
            SwitchTheme();
            HideAppBars();
        }

        #endregion

        #region Variables

        private int fontsize;
        private int height = OKrBookConfig.HEIGHT; //762;
        private int lineHeight = OKrBookConfig.LINEHEIGHT; //0x10;
        private int currentChapter;

        MarkContext mc = new MarkContext();
        ProgressContext pc = new ProgressContext();
        BookContext bc = new BookContext();

        private Progress progress;
        private Book book;
        private Mark mark;
        private Chapter chapter;

        private int current;

        private int location;

        #endregion

        private void viewArticle_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.ViewManagement.ApplicationView.TryUnsnap();
        }

        #region Font Flyout

        private FrameworkElement _fontSizePopupMenu = null;
        private Popup _popUp = null;

        private void PrepareFontPopup()
        {
            _fontSizePopupMenu = this.fontPopup;
            var parent = _fontSizePopupMenu.Parent as Grid;
            if (parent != null)
            {
                parent.Children.Remove(_fontSizePopupMenu);
                _fontSizePopupMenu.Visibility = Visibility.Visible;
            }
            if (_popUp == null)
            {
                _popUp = new Popup();
                _popUp.IsLightDismissEnabled = true;
            }
        }

        private void ShowFontPopup(UIElement targetElement)
        {
            _popUp.Child = _fontSizePopupMenu;
            var transform = targetElement.TransformToVisual(null);
            var point = transform.TransformPoint(new Point(0, 0));
            _popUp.HorizontalOffset = point.X - 40;// this.ScreenWidth - 180;
            _popUp.VerticalOffset = point.Y - 220;

            _popUp.IsOpen = true;
        }

        private void HideFontPopup()
        {
            _popUp.IsOpen = false;
        }

        private void fontMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            string caption = (e.ClickedItem as TextBlock).Text;
            switch (caption)
            {
                case "大号字体":
                    break;
                case "中号字体":
                    break;
                case "小号字体":
                    break;
                default:
                    break;
            }
            HideFontPopup();
            base.HideAppBars();
        }

        #endregion

        #region Page Flip Transition

        private bool flippingLeft = true;
        private bool transitionInProcess = false;
        private void FlipTransition(bool toLeft)
        {
            flippingLeft = toLeft;
            var transform = (CompositeTransform)bodyGrid.RenderTransform;
            keyFrameFrom.Value = transform.TranslateX;
            keyFrameTo.Value = toLeft ? (0 - ScreenWidth) : ScreenWidth;
            storyPageTransition.Begin();
            transitionInProcess = true;
        }

        private void storyPageTransition_Completed(object sender, object e)
        {
            var transform = (CompositeTransform)bodyGrid.RenderTransform;
            transform.TranslateX = 0;
            ChangePage(flippingLeft);
            transitionInProcess = false;
        }

        #endregion
    }
}