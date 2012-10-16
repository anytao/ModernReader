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
using Sina.View.Common.Toast;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using OKr.Win8Book.Client.Common;
using Windows.UI.Xaml.Controls;
using OKr.Win8Book.Client.Controls;
using Windows.UI.Core;
using Windows.System;

namespace OKr.Win8Book.Client.View
{
    public sealed partial class Viewer : OKrPageBase
    {
        #region Ctor

        public Viewer()
        {
            this.InitializeComponent();
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

        #endregion

        #region Lifecycle

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var category = e.Parameter as Chapter;
            if (e.NavigationMode == NavigationMode.New)
            {
                await LoadBook(category);
            }

            LoadTheme();
        }

        #endregion

        private async Task LoadBook(Chapter category) 
        {
            this.currentChapter = category.ChapterNo;

            this.chapter = category;
            this.current = category.PageCount;
            this.location = category.Pos;


            this.book = await bc.Load();
            this.chapter = await LoadData(this.currentChapter, category.Title);
            chapter.CurrentPage = GetCurrent(chapter, this.location);

            this.DataContext = chapter;

            this.mark = await mc.Load();
            this.progress = await pc.Load();

            this.chapter.Mark = this.mark;

            //this.pageTitle.Text = this.book.Name;
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

        #region Handlers

        private void bodyGrid_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
        }

        private void bodyGrid_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {

        }

        private void bodyGrid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Velocities.Linear.X > 2)
            {
                PrePage();
            }
            else if (e.Velocities.Linear.X < -2)
            {
                NextPage();
            }

            this.SetPage();
        }

        private void OnFontChange(object sender, RoutedEventArgs e)
        {

        }

        private async void OnMark(object sender, RoutedEventArgs e)
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
            }

            this.ShowMark();

            await this.mc.Save(this.mark);

            App.HomeViewModel.NotifyMarksChanged();
        }

        #endregion

        #region Private Methods

        private void ChangePage()
        {
        }

        private async void SetChapter()
        {
            this.progress.Chapter = this.currentChapter;
            this.progress.Page = this.current;
            this.progress.Percent = 0.0;

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

                this.location = this.chapter.CurrentPage.Locations[0];
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

                this.location = this.chapter.CurrentPage.Locations[0];
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

            //ImageBrush brush = new ImageBrush();
            //if (currentMark != null)
            //{
            //    brush.ImageSource = new BitmapImage(new Uri("/_static/img/okr-marked.png", UriKind.Relative));
            //}
            //else
            //{
            //    brush.ImageSource = new BitmapImage(new Uri("/_static/img/okr-marking.png", UriKind.Relative));
            //}

            //Dispatcher.BeginInvoke(delegate
            //{
            //    this.markbtn.Source = brush.ImageSource;
            //});
        }

        private async void SetPage()
        {
            this.progress.Page = this.current;
            this.progress.Percent = ((double)this.current) / ((double)this.chapter.PageNum);
            await pc.Save(this.progress);
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

        private int fontsize = OKrBookConfig.DEFALUTFONTSIZE;
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

        private void OnGridKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.RightButton)
            {
                NextPage();
            }

            if (e.Key == Windows.System.VirtualKey.LeftButton)
            {
                PrePage();
            }
        }

        private void viewArticle_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.ViewManagement.ApplicationView.TryUnsnap();
        }
    }
}