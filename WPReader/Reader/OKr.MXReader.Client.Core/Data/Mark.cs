using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using At.Phone.Common.Core;

namespace OKr.MXReader.Client.Core.Data
{
    public class Mark
    {
        public Mark()
        {
            this.Marks = new List<ChapterMark>();
        }
        
        public List<ChapterMark> Marks { get; set; }
    }
}
