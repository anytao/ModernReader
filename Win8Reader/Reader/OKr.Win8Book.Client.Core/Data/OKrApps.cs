using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKr.Common.Data;

namespace OKr.Win8Book.Client.Core.Data
{
    public class OKrApps : EntityBase
    {
        public OKrApps()
        {
            this.Apps = new List<OKrApp>();
        }

        public List<OKrApp> Apps { get; set; }
    }
}
