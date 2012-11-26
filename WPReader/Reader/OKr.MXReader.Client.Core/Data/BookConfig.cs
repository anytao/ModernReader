using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using At.Phone.Common.Core;
using System.Data.Linq.Mapping;

namespace OKr.MXReader.Client.Core.Data
{
    [Table]
    public class BookConfig //: EntityBase
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Intro { get; set; }

        public string Path { get; set; }
        public string Data { get; set; }
        public string ReadNum { get; set; }
        public string Status { get; set; }
    }
}
