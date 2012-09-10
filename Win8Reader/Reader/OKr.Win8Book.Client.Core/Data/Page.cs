using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKr.Common.Data;

namespace OKr.Win8Book.Client.Core.Data
{
    public class Page : EntityBase
    {
        public Page()
        {
            this.Row = new List<string>();
        }

        public int CharNum { get; set; }
        public int FontSize { get; set; }
        public string Result { get; set; }
        public List<string> Row { get; set; }
    }
}
