using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using OKr.MXReader.Client.Core.Data;
using Microsoft.Phone.Controls;

namespace OKr.MXReader.Client.View.Shared
{
    public partial class BookmarkItem : UserControl
    {
        public BookmarkItem()
        {
            InitializeComponent();
        }

        public delegate void ClickHandler(object sender, RoutedEventArgs e);
        public event ClickHandler Click;

        private void OnRead(object sender, RoutedEventArgs e)
        {
            RaiseClick(e);
        }

        private void RaiseClick(RoutedEventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }
    }
}
