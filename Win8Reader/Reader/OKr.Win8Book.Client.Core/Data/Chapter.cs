using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKr.Common.Data;

namespace OKr.Win8Book.Client.Core.Data
{
    public class Chapter : EntityBase
    {
        public Chapter()
        {
            this.Pages = new List<Page>();
            this.Pos = 1;
        }

        public string Title { get; set; }
        public int ChapterNo { get; set; }
        public int Size { get; set; }
        public int PageNum { get; set; }
        public int PageCount { get; set; }
        public string FileName { get; set; }
        public Mark Mark { get; set; }
        public List<string> PageList { get; set; }
        public List<Page> Pages { get; set; }

        /// <summary>
        /// Used for nav to location
        /// </summary>
        public int Pos { get; set; }

        private Page currentPage;

        public Page CurrentPage 
        {
            get { return this.currentPage; }
            set
            {
                this.SetProperty(ref this.currentPage, value);
            }
        }

        private Page nextPage;

        public Page NextPage
        {
            get { return this.nextPage; }
            set
            {
                this.SetProperty(ref this.nextPage, value);
            }
        }

        private Page prePage;

        public Page PrePage
        {
            get { return this.prePage; }
            set
            {
                this.SetProperty(ref this.prePage, value);

                 }
        }

        private bool isRead;
        public bool IsRead
        {
            get { return this.isRead; }
            set
            {
                this.SetProperty(ref this.isRead, value);
            }
        }
    }
}
