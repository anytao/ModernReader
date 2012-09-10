using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKr.Common.Data;

namespace OKr.Win8Book.Client.Core.Data
{
    public class Mark : EntityBase
    {
        public Mark()
        {
            this.Marks = new List<ChapterMark>();
        }

        public List<ChapterMark> Marks { get; set; }
    }
}
