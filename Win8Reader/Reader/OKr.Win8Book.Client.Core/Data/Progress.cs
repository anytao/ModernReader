using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKr.Win8Book.Client.Core.Data
{
    public class Progress
    {
        public int Chapter { get; set; }
        public int Page { get; set; }
        public int Location { get; set; }
        public string Percent { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
