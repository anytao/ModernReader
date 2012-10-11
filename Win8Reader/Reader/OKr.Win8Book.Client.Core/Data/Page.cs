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
            this.Locations = new List<int>();
        }

        public int CharNum { get; set; }
        public int FontSize { get; set; }
        public string Result { get; set; }
        public List<string> Row { get; set; }

        public List<int> Locations { get; set; }

        public string Text 
        {
            get 
            {
                string text = "";

                foreach (var item in Row)
                {
                    text += item;
                }

                return text;
            }
        }
    }
}
