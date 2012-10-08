using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKr.Common.Data;

namespace OKr.Win8Book.Client.Core.Data
{
    public class Book : EntityBase
    {
        public Book()
        {
            this.Chapters = new List<Chapter>();
        }

        public string Name { get; set; }
        public string Author { get; set; }
        public string Desc { get; set; }

        public List<Chapter> Chapters { get; set; }
        public int Current { get; set; }
    }
}
