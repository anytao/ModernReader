using OKr.Common.Data;
using OKr.Win8Book.Client.Core.Context;
using OKr.Win8Book.Client.Core.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Windows.UI.Xaml.Media;

namespace OKr.Win8Book.Client.ViewModel
{
    public class HomeViewModel : BindableBase
    {
        #region Properties

        protected App App { get { return App.Current as App; } }

        private Book book;
        public Book Book
        {
            get { return book; }
            set
            {
                this.SetProperty(ref book, value);
            }
        }

        private ObservableCollection<Chapter> topChapters = new ObservableCollection<Chapter>();
        public ObservableCollection<Chapter> TopChapters
        {
            get { return topChapters; }
            set
            {
                this.SetProperty(ref topChapters, value);
            }
        }

        private ObservableCollection<ChapterGroup> chapterGroups = new ObservableCollection<ChapterGroup>();
        public ObservableCollection<ChapterGroup> ChapterGroups
        {
            get { return chapterGroups; }
            set
            {
                this.SetProperty(ref chapterGroups, value);
            }
        }

        private Mark mark;
        public Mark Mark
        {
            get { return mark; }
            set
            {
                this.SetProperty(ref mark, value);
            }
        }

        public OKrApps OKrApp { get; set; }

        private ObservableCollection<ChapterMark> recentMarks=new ObservableCollection<ChapterMark>();
        public ObservableCollection<ChapterMark> RecentMarks
        {
            get { return recentMarks; }
            set
            {
                this.SetProperty(ref recentMarks, value);
            }
        }

        private SolidColorBrush themeForeground;
        public SolidColorBrush ThemeForeground
        {
            get { return themeForeground; }
            set
            {
                this.SetProperty(ref themeForeground, value);
            }
        }

        private Progress progress;
        public Progress Progress
        {
            get { return progress; }
            set
            {
                this.SetProperty(ref progress, value);
            }
        }


        private int chapterGroupSize = 20;

        #endregion

        public async Task Load()
        {
            SetTheme(App.IsLightTheme);
            await LoadBook();
            await LoadMark();
            await LoadApps();
            await LoadProgress();
        }

        #region Theme

        public void SwitchTheme()
        {
            SetTheme(!App.IsLightTheme);
        }

        private void SetTheme(bool light)
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

        private async Task LoadApps()
        {
            OKrAppContext oc = new OKrAppContext();
            this.OKrApp = await oc.Load();
        }

        private async Task LoadBook()
        {
            BookContext bc = new BookContext();
            this.Book = await bc.Load();

            TopChapters = new ObservableCollection<Chapter>();
            var filterResult = this.Book.Chapters.Take(10);
            foreach (var chapter in filterResult)
            {
                TopChapters.Add(chapter);
            }

            ChapterGroup group=null;
            for (int i = 0; i < this.Book.Chapters.Count; i++)
            {
                if (group==null)
                {
                    group = new ChapterGroup() { Key = (i + 1).ToString() + "~" + (i+chapterGroupSize).ToString() };
                    ChapterGroups.Add(group);
                }

                group.Chapters.Add(this.Book.Chapters[i]);
                if (((i+1) % chapterGroupSize)==0)
                {
                    group = null;
                }
            }
        }

        private async Task LoadMark()
        {
            MarkContext mc = new MarkContext();
            this.Mark = await mc.Load();

            RecentMarks = new ObservableCollection<ChapterMark>();
            var filterResult = this.Mark.Marks.Skip(this.Mark.Marks.Count - 10).Take(10);
            foreach (var chapterMark in filterResult)
            {
                RecentMarks.Add(chapterMark);
            }
        }

        private async Task LoadProgress()
        {
            ProgressContext pc = new ProgressContext();

            var progress = await pc.Load();

            if (!string.IsNullOrEmpty(progress.Text))
            {
                this.Progress = await pc.Load();
            }
            else
            {
                this.Progress = null;
            }
        }

        public async void NotifyMarksChanged()
        {
            await LoadMark();
            //this.RaisePropertyChanged("Mark");
            //this.RaisePropertyChanged("RecentMarks");
        }
    }
}
