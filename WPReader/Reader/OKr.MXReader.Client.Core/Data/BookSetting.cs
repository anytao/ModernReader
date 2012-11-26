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
    public class BookSetting //: EntityBase
    {
        public BookSetting()
        {
            this.Theme = "bbg.png";
            this.ThemeIndex = 0;
            this.FontSize = 0x18;
            this.Font = 0;
            this.Screen = 0.0;
            this.LineHeight = 0x10;
            this.FontColor = 0;
            this.IsNightMode = false;
        }

        public int FontColor { get; set; }
        public int Font { get; set; }
        public int FontSize { get; set; }
        public int LineHeight { get; set; }
        public double Screen { get; set; }
        public string Theme { get; set; }
        public int ThemeIndex { get; set; }
        public bool IsNightMode { get; set; }
    }
}
