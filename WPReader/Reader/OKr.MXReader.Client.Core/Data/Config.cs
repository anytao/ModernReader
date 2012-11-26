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
using System.Data.Linq.Mapping;
using At.Phone.Common.Core;

namespace OKr.MXReader.Client.Core.Data
{
    [Table]
    public class Config
    {
        [Column(IsPrimaryKey = true)]
        public int ID { get; set; }

        [Column]
        public string AtKey { get; set; }

        [Column]
        public string AtConfig { get; set; }

        [Column]
        public string Desc { get; set; }
    }
}
