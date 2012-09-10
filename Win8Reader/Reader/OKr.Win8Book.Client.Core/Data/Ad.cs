using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKr.Common.Data;

namespace OKr.Win8Book.Client.Core.Data
{
    public class Ad : EntityBase
    {
        public string AppID { get; set; }
        public string UnitID { get; set; }
        public string Type { get; set; }
        public bool IsShow { get; set; }
    }
}
