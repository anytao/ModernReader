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

namespace OKr.MXReader.Client.Core.Data
{
    public class Progress //: EntityBase
    {
        public int Chapter { get; set; }
        public int Page { get; set; }
        public double Percent { get; set; }
    }
}
