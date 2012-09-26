using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OKr.Common.Storage;
using OKr.Win8Book.Client.Core.Builder;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Viewer : Windows.UI.Xaml.Controls.Page
    {
        public Viewer()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var category = e.Parameter as Chapter;
            if (e.NavigationMode == NavigationMode.New)
            {
                this.pageTitle.Text = category.Title;
                this.currentChapter = category.ChapterNo;

                this.chapter = category;

                await LoadData();
            }

        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
        }

        private async Task LoadData()
        {
            int[] count = this.GetCounts(this.fontsize);
            //this.chapter = this.book.Chapters[this.currentChapter];
            //this.chapter.PageCount = count[0];

            OKrStorage storage = new OKrStorage();

            Windows.ApplicationModel.Package package = Windows.ApplicationModel.Package.Current;
            Windows.Storage.StorageFolder installedLocation = package.InstalledLocation;
            var file = await installedLocation.GetFileAsync("/Assets/Data/book/" + this.chapter.FileName + ".txt");

            var content = await FileIO.ReadTextAsync(file, Windows.Storage.Streams.UnicodeEncoding.Utf8); 

            //string content = await storage.ReadString(); //"ms-appx:///Assets/Data/book/" + this.chapter.FileName + ".txt"

            //string content = AtFile.GetContent(OkrBookContext.Current.Config.Data + "/" + this.chapter.FileName + ".txt", 4);
            Chapter chapter = null;
            if (content != null)
            {
                chapter = TextParser.GetChapter(content, count);
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

        private int fontsize = 22;
        private int height = 26;
        private int lineHeight = 28;
        private int currentChapter;


        private Progress progress;

        private Book book;
        private Mark mark;
        private Chapter chapter;
    }
}
