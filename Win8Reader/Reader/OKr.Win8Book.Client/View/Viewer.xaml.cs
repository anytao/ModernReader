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
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OKr.Win8Book.Client.View
{
    public sealed partial class Viewer : Windows.UI.Xaml.Controls.Page
    {
        public Viewer()
        {
            this.InitializeComponent();
        }
        
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var category = e.Parameter as Chapter;
            if (e.NavigationMode == NavigationMode.New)
            {
                this.pageTitle.Text = category.Title;
                this.currentChapter = category.ChapterNo;

                this.chapter = category;

                this.chapter = await LoadData();
                chapter.CurrentPage = chapter.Pages[0];
                this.current = 0;

                this.DataContext = chapter;

                
                this.mark = await mc.Load();
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
        }

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
        }

        private void OnFontChange(object sender, RoutedEventArgs e)
        {

        }

        private void OnMark(object sender, RoutedEventArgs e)
        {
            Mark m = this.chapter.Mark;

            if (m == null)
            {
                m = new Mark();
                this.chapter.Mark = m;
            }

            ChapterMark item = m.Marks.FirstOrDefault(x => x.Current == this.current);

            if (item == null)
            {
                item = new ChapterMark();
                item.ChapterNo = this.currentChapter;
                item.Title = this.chapter.Title;
                item.Date = DateTime.Now.ToString("yyyy/mm/dd hh:MM:ss");
                item.Current = this.current;
                item.Percent = ((double)this.current) / ((double)this.chapter.PageNum);
                // At.Okr.Client.Core.Data.Page setting = this.chapter.Pages[this.index];
                // item.Content = setting.Row[0].Trim() + setting.Row[1].Trim();
                this.mark.Marks.Add(item);
                m.Marks.Add(item);
            }
            else
            {
                int chapterNo = item.ChapterNo;
                List<ChapterMark> list = new List<ChapterMark>();
                for (int i = 0; i < this.mark.Marks.Count; i++)
                {
                    if ((this.mark.Marks[i].ChapterNo != chapterNo) || (this.mark.Marks[i].Current != this.current))
                    {
                        list.Add(this.mark.Marks[i]);
                    }
                }
                this.mark.Marks.Clear();
                this.mark.Marks = list;
                List<ChapterMark> list2 = new List<ChapterMark>();
                for (int j = 0; j < m.Marks.Count; j++)
                {
                    if (m.Marks[j].Current != this.current)
                    {
                        list2.Add(m.Marks[j]);
                    }
                }
                this.chapter.Mark.Marks.Clear();
                this.chapter.Mark.Marks = list2;
            }

            // IsolatedStorageSettings.ApplicationSettings["marks"] = this.mark;
            // IsolatedStorageSettings.ApplicationSettings["bookinfo"] = this.book;

            this.ShowMark();

            this.mc.Save(this.mark);
        }

        private async Task<Chapter> LoadData()
        {
            int[] count = this.GetCounts(this.fontsize);

            OKrStorage storage = new OKrStorage();

            Windows.ApplicationModel.Package package = Windows.ApplicationModel.Package.Current;
            Windows.Storage.StorageFolder installedLocation = package.InstalledLocation;

            var file = await StorageFile.GetFileFromPathAsync(Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, @"Assets\Data\book\" + this.chapter.FileName + ".txt"));

            var content = await FileIO.ReadTextAsync(file, Windows.Storage.Streams.UnicodeEncoding.Utf8);

            Chapter chapter = null;
            if (content != null)
            {
                chapter = TextParser.GetChapter(content, count);
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

        private void NextPage()
        {
            this.current++;

            this.chapter.CurrentPage = this.chapter.Pages[this.current];
        }

        private void PrePage()
        {
            this.current--;

            this.chapter.CurrentPage = this.chapter.Pages[this.current];
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

        private int fontsize = OKrBookConfig.DEFALUTFONTSIZE;
        private int height = OKrBookConfig.HEIGHT; //762;
        private int lineHeight = OKrBookConfig.LINEHEIGHT; //0x10;
        private int currentChapter;

        MarkContext mc = new MarkContext();

        private Progress progress;
        private Book book;
        private Mark mark;
        private Chapter chapter;

        private int current;        
    }
}