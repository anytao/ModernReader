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

namespace OKr.MXReader.Client.Core.Data
{
    [Table]
    public class AppInfo
    {
        [Column]
        public string AppId { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Icon { get; set; }

        /// <summary>
        /// The image path of app background
        /// </summary>
        [Column]
        public string Bg { get; set; }

        [Column]
        public string Version { get; set; }

        [Column]
        public string Intro { get; set; }

        [Column]
        public string Type { get; set; }

        [Column]
        public string Desc { get; set; }

        /// <summary>
        /// The date of publishment
        /// </summary>
        [Column]
        public string PubOn { get; set; }

        /// <summary>
        /// The date of creating a new
        /// </summary>
        [Column]
        public string CreateOn { get; set; }
    }
}
