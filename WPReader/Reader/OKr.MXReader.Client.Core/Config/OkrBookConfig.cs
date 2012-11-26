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

namespace OKr.MXReader.Client.Core.Config
{
    public class OkrBookConfig
    {
        public static readonly int FontSize = 22; // 0x18; 
        public static readonly int LineHeight = 0x10;
        public static readonly int BookHeight = 800; // 780;
        public static readonly int Height = 762; // 742 No title, it's 800
        public static readonly int AdBookHeight = 640;// 620 680;
        public static readonly int AdHeight = 662; //662

        public static readonly bool Flag = true;

        public static readonly Brush WhiteBrush = new SolidColorBrush(Color.FromArgb(0xff, 0, 0, 0));

        public static readonly Brush WBrush = new SolidColorBrush(Colors.White);
        public static readonly Brush BBrush = new SolidColorBrush(Color.FromArgb(0xff, 33, 33, 33));

        //Flurry
        //OKRBook-古典：N3FF1WIJ3XDYGMBYAE99
        //OKRBook-历史：QFGZ6YQXKKVBBBPDRBB3
        // 小说+四大名著: "UMVMEU8KGPEQBRGH2ZDF";
        //OKRBook-国外经典: HG6QWFUGFNM2F5539XP6
        public static readonly string FLURRY_APIKEY = "N3FF1WIJ3XDYGMBYAE99"; 
    }
}
