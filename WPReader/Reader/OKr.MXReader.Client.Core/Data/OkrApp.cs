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

namespace OKr.MXReader.Client.Core.Data
{
    public class OkrApp
    {
        public string AppId { get; set; }
        public string AppName { get; set; }
        public string Intro { get; set; }
        public string Version { get; set; }
        public string Build { get; set; }
        public string Us { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Weibo { get; set; }

        public Ad Ad { get; set; }
    }
}
