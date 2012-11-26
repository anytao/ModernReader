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
using System.Collections.Generic;

namespace OKr.MXReader.Client.Core.Data
{
    public class Book
    {
        public Book()
        {
            this.Chapters = new List<Chapter>();
        }

        public string Name { get; set; }
        public List<Chapter> Chapters { get; set; }
        public int Current { get; set; }
    }
}
