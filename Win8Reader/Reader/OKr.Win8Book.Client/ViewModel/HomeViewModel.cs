using OKr.Common.Data;
using OKr.Win8Book.Client.Core.Context;
using OKr.Win8Book.Client.Core.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace OKr.Win8Book.Client.ViewModel
{
    public class HomeViewModel : BindableBase
    {
        #region Properties

        private Book book;
        public Book Book
        {
            get { return book; }
            set
            {
                this.SetProperty(ref book, value);
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

        public OKrApps Apps { get; set; }

        private ObservableCollection<ChapterMark> recentMarks=new ObservableCollection<ChapterMark>();
        public ObservableCollection<ChapterMark> RecentMarks
        {
            get { return recentMarks; }
            set
            {
                this.SetProperty(ref recentMarks, value);
            }
        }

        #endregion

        public async Task Load()
        {
            await LoadBook();
            await LoadMark();
            await LoadApps();
        }

        private async Task LoadApps()
        {
            OKrAppContext oc = new OKrAppContext();
            this.Apps = await oc.Load();
        }

        private async Task LoadBook()
        {
            BookContext bc = new BookContext();
            this.Book = await bc.Load();
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

        public async void NotifyMarksChanged()
        {
            await LoadMark();
            //this.RaisePropertyChanged("Mark");
            //this.RaisePropertyChanged("RecentMarks");
        }
    }
}
