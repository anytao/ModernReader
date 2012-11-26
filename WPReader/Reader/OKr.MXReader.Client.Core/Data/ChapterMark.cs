using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using At.Phone.Common.Core;
using System.Data.Linq.Mapping;

namespace OKr.MXReader.Client.Core.Data
{
    [Table]
    public class ChapterMark //: EntityBase
    {
        public int ChapterNo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public double Percent { get; set; }
        public int Current { get; set; }

        //public ChapterMark Clone()
        //{
        //    return (ChapterMark)base.MemberwiseClone();
        //}
    }
}
