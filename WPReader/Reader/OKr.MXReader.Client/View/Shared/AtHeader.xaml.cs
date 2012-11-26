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

namespace OKr.MXReader.Client.View.Shared
{
    public partial class AtHeader : UserControl
    {
        public AtHeader()
        {
            InitializeComponent();
        }

        public string Title
        {
            get
            {
                return this.tbTitle.Text;
            }
            set
            {
                this.tbTitle.Text = value;
            }
        }

        public static DependencyProperty SubTitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(AtHeader), null);
    }
}
