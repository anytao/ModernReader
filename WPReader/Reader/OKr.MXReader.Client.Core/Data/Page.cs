using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using At.Phone.Common.Core;

namespace OKr.MXReader.Client.Core.Data
{
    public class Page
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
