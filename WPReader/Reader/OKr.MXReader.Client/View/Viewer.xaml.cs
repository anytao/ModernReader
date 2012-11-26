using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using OKr.MXReader.Client.Core.Data;
using System.Windows.Threading;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using OKr.MXReader.Client.Core.File;
using OKr.MXReader.Client.Core;
using System.Text.RegularExpressions;
using Microsoft.Advertising.Mobile.UI;
using At.Phone.Common;
using OKr.MXReader.Client.Core.Context;
using OKr.MXReader.Client.Core.Config;
using At.Phone.Control.Loader;

namespace OKr.MXReader.Client.View
{
    public partial class Viewer : PhoneApplicationPage
    {
        public Viewer()
        {
            this.flag = OkrBookConfig.Flag;
            this.fontsize = OkrBookConfig.FontSize;
            this.lineHeight = OkrBookConfig.LineHeight;
            this.bookHeight = OkrBookConfig.BookHeight;
            this.height = OkrBookConfig.Height;
   
            this.InitializeComponent();

            IsolatedStorageSettings.ApplicationSettings.TryGetValue<Book>("bookinfo", out this.book);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue<Progress>("current", out this.progress);
            this.currentChapter = this.progress.Chapter;
            
            IsolatedStorageSettings.ApplicationSettings.TryGetValue<Mark>("marks", out this.mark);
            if (this.mark == null)
            {
                this.mark = new Mark();

                IsolatedStorageSettings.ApplicationSettings["marks"] = this.mark;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }

            IsolatedStorageSettings.ApplicationSettings.TryGetValue<BookSetting>("set", out this.set);
            if (this.set == null)
            {
                this.set = new BookSetting();
                IsolatedStorageSettings.ApplicationSettings["set"] = this.set;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }

            this.fontsize = this.set.FontSize;
            ThemeInit(); //this.fontColor = OkrBookConfig.WhiteBrush; // 


            if (OkrBookContext.Current.App.Ad.IsShow) //启用网络判断，非常的慢，&& AtNetwork.IsNetworkAvailable()
            {
                this.bookHeight = OkrBookConfig.AdBookHeight;
                this.height = OkrBookConfig.AdHeight; // 720;
            }

            this.Init();
            base.Loaded += new RoutedEventHandler(OnLoad);
            this.Showtime();
        }

        private void OnLoad(object o, RoutedEventArgs e)
        {
            this.LoadData();
            this.topbar.Visibility = Visibility.Collapsed;
        }

        private void OnManipulationStarted(object o, ManipulationStartedEventArgs e)
        {
            double x = e.ManipulationOrigin.X;
            double y = e.ManipulationOrigin.Y;

            if (this.flag && (y > 200.0))
            {
                if (x < 200.0)
                {
                    if (this.index > 0)
                    {
                        this.index--;
                        this.ChangePage();
                        this.pageno1.Text = this.pageno2.Text;
                        this.pageno2.Text = string.Concat(new object[] { this.index + 1, "/", this.chapter.PageNum, OkrConstant.PAGE });
                        this.SetPage();
                        this.ShowPage(-480, -5, this.trans);
                    }
                    else if (this.currentChapter >= 1)
                    {
                        this.currentChapter--;
                        this.title1.Text = this.title2.Text;
                        this.LoadData(this.currentChapter);
                        this.index = this.chapter.PageNum - 1;
                        this.ChangePage();
                        this.pageno1.Text = this.pageno2.Text;
                        this.pageno2.Text = string.Concat(new object[] { this.index + 1, "/", this.chapter.PageNum, OkrConstant.PAGE });
                        this.SetChapter();
                        this.ShowPage(-480, -5, this.trans);
                    }
                    else
                    {
                        MessageBox.Show(OkrConstant.FIRSTPAGEERR);
                    }
                }
                else if (x > 280.0)
                {
                    if (this.index <= (this.chapter.PageNum - 2))
                    {
                        this.index++;
                        this.ChangePage();
                        this.pageno1.Text = this.pageno2.Text;
                        this.pageno2.Text = string.Concat(new object[] { this.index + 1, "/", this.chapter.PageNum, OkrConstant.PAGE });
                        this.SetPage();
                        this.ShowPage(480, -5, this.trans);
                    }
                    else if (this.currentChapter < (this.book.Chapters.Count - 1))
                    {
                        this.currentChapter++;
                        this.index = 0;
                        this.pageno1.Text = this.pageno2.Text;
                        this.title1.Text = this.title2.Text;
                        this.LoadData(this.currentChapter);
                        this.ChangePage();
                        this.pageno2.Text = string.Concat(new object[] { this.index + 1, "/", this.chapter.PageNum, OkrConstant.PAGE });
                        this.SetChapter();
                        this.ShowPage(480, -5, this.trans);
                    }
                    else
                    {
                        MessageBox.Show(OkrConstant.LASTPAGEERR);
                    }
                }
            }
            else
            {

            }
        }

        private void OnMarkManipulationStarted(object o, ManipulationStartedEventArgs e)
        {
            Mark m = this.chapter.Mark;

            if (m == null)
            {
                m = new Mark();
                this.chapter.Mark = m;
            }

            ChapterMark item = m.Marks.FirstOrDefault(x => x.Current == this.index);

            if (item == null)
            {
                item = new ChapterMark();
                item.ChapterNo = this.currentChapter;
                item.Title = this.book.Chapters[this.currentChapter].Title;
                item.Date = DateTime.Now.ToString("yyyy/mm/dd hh:MM:ss");
                item.Current = this.index;
                item.Percent = ((double)this.index) / ((double)this.chapter.PageNum);
                OKr.MXReader.Client.Core.Data.Page setting = this.chapter.Pages[this.index];
                item.Content = setting.Row[0].Trim() + setting.Row[1].Trim();
                this.mark.Marks.Add(item);
                m.Marks.Add(item);               
            }
            else
            {
                int chapterNo = item.ChapterNo;
                List<ChapterMark> list = new List<ChapterMark>();
                for (int i = 0; i < this.mark.Marks.Count; i++)
                {
                    if ((this.mark.Marks[i].ChapterNo != chapterNo) || (this.mark.Marks[i].Current != this.index))
                    {
                        list.Add(this.mark.Marks[i]);
                    }
                }
                this.mark.Marks.Clear();
                this.mark.Marks = list;
                List<ChapterMark> list2 = new List<ChapterMark>();
                for (int j = 0; j < m.Marks.Count; j++)
                {
                    if (m.Marks[j].Current != this.index)
                    {
                        list2.Add(m.Marks[j]);
                    }
                }
                this.chapter.Mark.Marks.Clear();
                this.chapter.Mark.Marks = list2;
            }

            IsolatedStorageSettings.ApplicationSettings["marks"] = this.mark;
            IsolatedStorageSettings.ApplicationSettings["bookinfo"] = this.book;

            this.ShowMark();
        }

        private void OnShow(object sender, EventArgs e)
        {
            this.timetxt1.Text = DateTime.Now.ToShortTimeString(); 
            this.timetxt2.Text = DateTime.Now.ToShortTimeString(); 
        }

        private void OnTopRender(object sender, EventArgs e)
        {
            this.flag = true;
            this.topTimer.Stop();
            this.title1.Text = this.title2.Text;
            this.ShowMark();
        }

        private void Init()
        {
            this.topbar = new Canvas();
            this.topbar.Background = new SolidColorBrush(Colors.Transparent);
            this.topbar.HorizontalAlignment = HorizontalAlignment.Left;
            this.topbar.VerticalAlignment = VerticalAlignment.Top;
            this.topbar.Margin = new Thickness(80.0, 380.0, 0.0, 0.0);
            this.topbar.Height = 30.0;
            //this.tbLoading = new TextBlock();
            //this.tbLoading.Height = 40.0;
            //this.tbLoading.HorizontalAlignment = HorizontalAlignment.Left;
            //this.tbLoading.VerticalAlignment = VerticalAlignment.Top;
            //this.tbLoading.Margin = new Thickness(2.0, 0.0, 0.0, 0.0);
            //this.tbLoading.FontSize = 22.0;
            //this.tbLoading.Name = "ftitle";
            //this.tbLoading.Text = OkrConstant.LOADING;
            //this.tbLoading.Foreground = new SolidColorBrush(Colors.Red);
            //this.topbar.Children.Add(this.tbLoading);

            this.loader = new OkrLoader();
            this.loader.Text = OkrConstant.LOADING;
            this.loader.Width = 300;
            this.topbar.Children.Add(this.loader);

            Canvas.SetZIndex(this.topbar, 5);
            this.LayoutRoot.Children.Add(this.topbar);

            this.wrapper1 = new Canvas();
            this.wrapper2 = new Canvas();
            this.screenbrightness = new Canvas();
            this.wrapper1.Width = 480.0;
            this.wrapper1.Height = this.height;
            this.wrapper1.HorizontalAlignment = HorizontalAlignment.Left;
            this.wrapper1.VerticalAlignment = VerticalAlignment.Top;
            this.wrapper2.Width = 480.0;
            this.wrapper2.Height = this.height;
            this.wrapper2.HorizontalAlignment = HorizontalAlignment.Left;
            this.wrapper2.VerticalAlignment = VerticalAlignment.Top;
            // container1/2 is the area of text, including text and page number, not including title.
            this.container1 = new Canvas();
            this.container2 = new Canvas();
            this.container1.Margin = new Thickness(10.0, 35.0, 0.0, 0.0);
            this.container2.Margin = new Thickness(5.0, 35.0, 0.0, 0.0);
            this.container1.Width = 480.0;
            this.container2.Height = 700.0;
            this.container1.HorizontalAlignment = HorizontalAlignment.Left;
            this.container2.HorizontalAlignment = HorizontalAlignment.Left;
            this.container1.VerticalAlignment = VerticalAlignment.Top;
            this.container2.VerticalAlignment = VerticalAlignment.Top;
            this.screenbrightness.Width = 480.0;
            this.screenbrightness.Height = 800.0;
            this.screenbrightness.Opacity = this.set.Screen;
            this.screenbrightness.HorizontalAlignment = HorizontalAlignment.Left;
            this.screenbrightness.Background = new SolidColorBrush(Colors.Black);
            this.screenbrightness.ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(OnManipulationStarted);
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("/_static/img/" + this.set.Theme, UriKind.Relative));
            // wrapper1/2 is the area of text, including text and page number and title.
            this.wrapper1.Background = brush;
            this.wrapper1.Margin = new Thickness(-5.0, 58.0, 0.0, 0.0);
            this.wrapper2.Background = brush;
            this.wrapper2.Margin = new Thickness(-5.0, 58.0, 0.0, 0.0);
            this.LayoutRoot.Background = brush;
            Canvas.SetZIndex(this.wrapper1, 0);
            Canvas.SetZIndex(this.wrapper2, 1);
            Canvas.SetZIndex(this.screenbrightness, 2);
            this.trans = new TranslateTransform();
            this.wrapper2.RenderTransform = this.trans;
            this.title1 = new TextBlock();
            this.title1.Margin = new Thickness(15.0, 0.0, 0.0, 0.0);
            this.title1.HorizontalAlignment = HorizontalAlignment.Left;
            this.title1.VerticalAlignment = VerticalAlignment.Top;
            this.title1.Foreground = this.fontColor;
            this.title1.FontSize = 16.0;
            this.wrapper1.Children.Add(this.title1);
            this.title2 = new TextBlock();
            this.title2.Margin = new Thickness(15.0, 0.0, 0.0, 0.0);
            this.title2.HorizontalAlignment = HorizontalAlignment.Left;
            this.title2.VerticalAlignment = VerticalAlignment.Top;
            this.title2.Foreground = this.fontColor;
            this.title2.FontSize = 16.0;
            this.wrapper2.Children.Add(this.title2);
            this.markbtn = new Image();
            this.markbtn.Margin = new Thickness(410.0, 58.0, 0.0, 0.0);
            this.markbtn.HorizontalAlignment = HorizontalAlignment.Left;
            this.markbtn.VerticalAlignment = VerticalAlignment.Top;
            this.markbtn.Width = 42.0;
            this.markbtn.Height = 66.0;
            this.markbtn.Opacity = 1.0 - this.set.Screen;
            this.markbtn.ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(OnMarkManipulationStarted);
            this.LayoutRoot.Children.Add(this.markbtn);
            Canvas.SetZIndex(this.markbtn, 3);
            this.wrapper1.Children.Add(this.container1);
            this.wrapper2.Children.Add(this.container2);
            this.pageno1 = new TextBlock();
            this.pageno1.VerticalAlignment = VerticalAlignment.Bottom;
            this.pageno1.HorizontalAlignment = HorizontalAlignment.Left;
            this.pageno1.Foreground = this.fontColor;
            this.pageno1.Margin = new Thickness(5.0, (double)this.bookHeight, 0.0, 0.0);
            this.pageno1.TextAlignment = TextAlignment.Center;
            this.pageno1.Width = 100.0;
            this.pageno1.FontSize = 16.0;
            this.wrapper1.Children.Add(this.pageno1);
            this.pageno2 = new TextBlock();
            this.pageno2.VerticalAlignment = VerticalAlignment.Bottom;
            this.pageno2.HorizontalAlignment = HorizontalAlignment.Left;
            this.pageno2.Foreground = this.fontColor;
            this.pageno2.TextAlignment = TextAlignment.Center;
            this.pageno2.Margin = new Thickness(5.0, (double)this.bookHeight, 0.0, 0.0);
            this.pageno2.Width = 100.0;
            this.pageno2.FontSize = 16.0;
            this.wrapper2.Children.Add(this.pageno2);
            this.timetxt1 = new TextBlock();
            this.timetxt1.VerticalAlignment = VerticalAlignment.Bottom;
            this.timetxt1.HorizontalAlignment = HorizontalAlignment.Left;
            this.timetxt1.Foreground = this.fontColor;
            this.timetxt1.TextAlignment = TextAlignment.Center;
            this.timetxt1.Margin = new Thickness(405.0, (double)this.bookHeight, 0.0, 0.0);
            this.timetxt1.Width = 50.0;
            this.timetxt1.FontSize = 16.0;
            this.timetxt1.Text = DateTime.Now.ToShortTimeString(); //.ToString(); // DateUtil.GetH() + ":" + DateUtil.GetM();
            this.wrapper1.Children.Add(this.timetxt1);
            this.timetxt2 = new TextBlock();
            this.timetxt2.VerticalAlignment = VerticalAlignment.Bottom;
            this.timetxt2.HorizontalAlignment = HorizontalAlignment.Left;
            this.timetxt2.Foreground = this.fontColor;
            this.timetxt2.TextAlignment = TextAlignment.Center;
            this.timetxt2.Margin = new Thickness(405.0, (double)this.bookHeight, 0.0, 0.0);
            this.timetxt2.Width = 50.0;
            this.timetxt2.FontSize = 16.0;
            this.timetxt2.Text = DateTime.Now.ToShortTimeString(); //.ToString(); //DateUtil.GetH() + ":" + DateUtil.GetM();
            this.wrapper2.Children.Add(this.timetxt2);
            this.LayoutRoot.Children.Add(this.wrapper1);
            this.LayoutRoot.Children.Add(this.wrapper2);
            this.LayoutRoot.Children.Add(this.screenbrightness);
            this.text_text1 = new TextBlock();
            this.text_text1.HorizontalAlignment = HorizontalAlignment.Left;
            this.text_text1.VerticalAlignment = VerticalAlignment.Top;
            this.text_text1.LineHeight = this.lineHeight + this.fontsize;
            this.text_text1.Width = 460.0;
            this.text_text1.Height = 640.0;
            this.text_text1.FontSize = this.fontsize;
            this.text_text1.Margin = new Thickness(15.0, 0.0, 0.0, 0.0);
            this.text_text1.Foreground = this.fontColor;
            this.text_text2 = new TextBlock();
            this.text_text2.HorizontalAlignment = HorizontalAlignment.Left;
            this.text_text2.VerticalAlignment = VerticalAlignment.Top;
            this.text_text2.LineHeight = this.lineHeight + this.fontsize;
            this.text_text2.Width = 460.0;
            this.text_text2.Height = 640.0;
            this.text_text2.FontSize = this.fontsize;
            this.text_text2.Margin = new Thickness(20.0, 0.0, 0.0, 0.0);
            this.text_text2.Foreground = this.fontColor;
            this.container1.Children.Add(this.text_text1);
            this.container2.Children.Add(this.text_text2);

            // Show the ad
            if (OkrBookContext.Current.App.Ad.IsShow) //(this.ad.show_flag.Equals("1") && this.ad.type.Equals("1")) && MobileUtil.GetNetWorkStatus()
            {
                Ad ad = OkrBookContext.Current.App.Ad;
                this.adBar = new AdControl(ad.AppID, ad.UnitID, true);
                this.adBar.VerticalAlignment = VerticalAlignment.Bottom;
                this.adBar.Width = 480;
                this.adBar.Height = 80;
                this.adBar.Margin = new Thickness(0.0, 720.0, 0.0, 0.0);
                this.adBar.Background = new SolidColorBrush(Colors.Green);

                Canvas.SetZIndex(this.adBar, 3);
                this.LayoutRoot.Children.Add(this.adBar);
            }

            this.title.Title = OkrBookContext.Current.Config.Name;
        }

        private void Showtime()
        {
            this.timeTimer = new DispatcherTimer();
            this.timeTimer.Interval = TimeSpan.FromSeconds(1.0);
            this.timeTimer.Tick += new EventHandler(OnShow);
            this.timeTimer.Start();
        }

        private void LoadData()
        {
            int[] count = this.GetCounts(this.fontsize);
            this.chapter = this.book.Chapters[this.currentChapter];
            this.chapter.PageCount = count[0];
            string content = AtFile.GetContent(OkrBookContext.Current.Config.Data +"/" + this.chapter.FileName + ".txt", 4);
            Chapter chapter = null;
            if (content != null)
            {
                chapter = TextParser.GetChapter(content, count);
            }
            this.chapter.PageList = chapter.PageList;
            this.chapter.PageNum = chapter.PageNum;
            this.chapter.Pages = chapter.Pages;
            this.GetThisPage();
            this.ChangeMarkContent();
            this.title1.Text = this.chapter.Title;
            this.title2.Text = this.chapter.Title;
            OKr.MXReader.Client.Core.Data.Page page = this.chapter.Pages[this.index];
            this.text_text1.Text = "";
            this.text_text2.Text = "";
            for (int i = 0; i < page.Row.Count; i++)
            {
                if (Regex.IsMatch(page.Row[i], @"[\r\n]"))
                {
                    this.text_text1.Text = this.text_text1.Text + page.Row[i];
                    this.text_text2.Text = this.text_text2.Text + page.Row[i];
                }
                else
                {
                    this.text_text1.Text = this.text_text1.Text + page.Row[i] + "\n";
                    this.text_text2.Text = this.text_text2.Text + page.Row[i] + "\n";
                }
            }
            this.pageno1.Text = string.Concat(new object[] { this.index + 1, "/", this.chapter.PageNum, OkrConstant.PAGE });
            this.pageno2.Text = string.Concat(new object[] { this.index + 1, "/", this.chapter.PageNum, OkrConstant.PAGE });
            this.ShowMark();
        }

        private void LoadData(int index)
        {
            int[] count = this.GetCounts(this.fontsize);
            this.chapter = this.book.Chapters[index];
            string content = AtFile.GetContent(OkrBookContext.Current.Config.Data + "/" + this.chapter.FileName + ".txt", 4); 
            Chapter chapter = null;
            if (content != null)
            {
                chapter = TextParser.GetChapter(content, count);
            }
            this.chapter.Pages = chapter.Pages;
            this.chapter.PageList = chapter.PageList;
            this.chapter.PageNum = chapter.PageNum;
            
            this.title2.Text = this.chapter.Title;
            this.ShowMark();
        }

        private void ChangeMarkContent()
        {
            if (this.index != this.progress.Page)
            {
                for (int i = 0; i < this.mark.Marks.Count; i++)
                {
                    if ((this.mark.Marks[i].ChapterNo == this.currentChapter) && (this.mark.Marks[i].Current == this.progress.Page))
                    {
                        this.mark.Marks[i].Current = this.index;
                        for (int j = 0; j < this.chapter.Mark.Marks.Count; j++)
                        {
                            if (this.chapter.Mark.Marks[j].Current == this.progress.Page)
                            {
                                this.chapter.Mark.Marks[j].Current = this.index;
                                OKr.MXReader.Client.Core.Data.Page page = this.chapter.Pages[this.index];
                                this.chapter.Mark.Marks[j].Content = page.Row[0];
                                this.mark.Marks[i].Content = page.Row[0];
                                break;
                            }
                        }
                        break;
                    }
                }
                IsolatedStorageSettings.ApplicationSettings["marks"] = this.mark;
                IsolatedStorageSettings.ApplicationSettings["bookinfo"] = this.book;
            }
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

        private void GetThisPage()
        {
            double num = this.chapter.PageNum * this.progress.Percent;
            if (num > ((int)num))
            {
                this.index = ((int)num) + 1;
            }
            else
            {
                this.index = (int)num;
            }
        }

        private void ChangePage()
        {
            OKr.MXReader.Client.Core.Data.Page page = this.chapter.Pages[this.index];
            this.text_text1.Text = this.text_text2.Text;
            this.text_text2.Text = "";
            for (int i = 0; i < page.Row.Count; i++)
            {
                if (Regex.IsMatch(page.Row[i], @"[\r\n]"))
                {
                    this.text_text2.Text = this.text_text2.Text + page.Row[i];
                }
                else
                {
                    this.text_text2.Text = this.text_text2.Text + page.Row[i] + "\n";
                }
            }
        }

        private void SetPage()
        {
            this.progress.Page = this.index;
            this.progress.Percent = ((double)this.index) / ((double)this.chapter.PageNum);
            IsolatedStorageSettings.ApplicationSettings["current"] = this.progress;
        }

        private void SetChapter()
        {
            this.progress.Chapter = this.currentChapter;
            this.progress.Page = this.index;
            this.progress.Percent = 0.0;
            IsolatedStorageSettings.ApplicationSettings["current"] = this.progress;
        }

        private void ShowPage(int from, int to, TranslateTransform Transform)
        {
            DoubleAnimation timeline = new DoubleAnimation();
            timeline.From = new double?((double)from);
            timeline.To = new double?((double)to);
            timeline.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            Storyboard.SetTarget(timeline, Transform);
            Storyboard.SetTargetProperty(timeline, new PropertyPath(TranslateTransform.XProperty));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(timeline);
            storyboard.Begin();
            this.flag = false;
            this.SetTimer(400);
        }

        private void SetTimer(int value)
        {
            this.topTimer = new DispatcherTimer();
            this.topTimer.Interval = TimeSpan.FromMilliseconds((double)value);
            this.topTimer.Tick += new EventHandler(this.OnTopRender);
            this.topTimer.Start();
        }

        private void ShowMark()
        {
            Mark mark = this.chapter.Mark;
            ChapterMark currentMark = null;
            if (mark != null)
            {
                foreach (var item in mark.Marks)
                {
                    if (item.Current == this.index)
                    {
                        currentMark = item;
                        break;
                    }
                }
            }

            ImageBrush brush = new ImageBrush();
            if (currentMark != null)
            {
                brush.ImageSource = new BitmapImage(new Uri("/_static/img/okr-marked.png", UriKind.Relative));
            }
            else
            {
                brush.ImageSource = new BitmapImage(new Uri("/_static/img/okr-marking.png", UriKind.Relative));
            }

            Dispatcher.BeginInvoke(delegate
            {
                this.markbtn.Source = brush.ImageSource;
            });
        }

        #region Use if needed

        private void ThemeInit()
        {
            if (this.set.Theme.Equals("bbg.png"))
            {
                this.fontColor = OkrBookConfig.WhiteBrush;
            }
            else if (this.set.Theme.Equals("bbg-night.png"))
            {
                this.fontColor = OkrBookConfig.WBrush; // new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));
            }
            //else if (this.set.Theme.Equals("3.png"))
            //{
            //    this.fontColor = new SolidColorBrush(Color.FromArgb(0xff, 0x33, 0x5d, 0x60));
            //}
        }

        #endregion

        #region Variables

        private Progress progress;
        private BookSetting set;

        private Book book;
        private Mark mark;
        private Chapter chapter;

        private DispatcherTimer timeTimer;
        private DispatcherTimer topTimer;

        private TranslateTransform trans;

        private Ad ad;
        private AdControl adBar;
        private Canvas container1;
        private Canvas container2;
        private Canvas wrapper1;
        private Canvas wrapper2;
        private Canvas screenbrightness;
        private Canvas topbar;


        private bool flag;
        private int fontsize;
        private int index;
        private int height;
        private int lineHeight;
        private int bookHeight;
        private int currentChapter;

        private Brush fontColor;
        private Image markbtn;
        //private TextBlock tbLoading;
        private TextBlock pageno1;
        private TextBlock pageno2;
        private TextBlock text_text1;
        private TextBlock text_text2;
        private TextBlock timetxt1;
        private TextBlock timetxt2;
        private TextBlock title1;
        private TextBlock title2;

        private OkrLoader loader;

        #endregion
    }
}