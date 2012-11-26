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

namespace At.Okr.Client.Core.Data
{
    public class AppInfo
    {
        public string AppId { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        /// <summary>
        /// The image path of app background
        /// </summary>
        public string Bg { get; set; }

        public string Version { get; set; }

        public string Intro { get; set; }

        public string Type { get; set; }

        public string Desc { get; set; }

        /// <summary>
        /// The date of publishment
        /// </summary>
        public string PubOn { get; set; }

        /// <summary>
        /// The date of creating a new
        /// </summary>
        public string CreateOn { get; set; }
    }
}
