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
    }
}
