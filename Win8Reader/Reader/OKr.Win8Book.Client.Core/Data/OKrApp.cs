using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKr.Common.Data;

namespace OKr.Win8Book.Client.Core.Data
{
    public class OKrApp : EntityBase
    {
        public string AppId { get; set; }
        public string PId { get; set; }
        public string AppName { get; set; }
        public string Author { get; set; }
        public string Intro { get; set; }
        public string Version { get; set; }
        public string Build { get; set; }
        public string Us { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Weibo { get; set; }
        public string Pic { get; set; }

        public Ad Ad { get; set; }
    }
}
