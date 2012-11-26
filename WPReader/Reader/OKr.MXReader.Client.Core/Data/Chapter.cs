using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using At.Phone.Common.Core;
using System.Data.Linq.Mapping;

namespace OKr.MXReader.Client.Core.Data
{
    [Table]
    public class Chapter
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
