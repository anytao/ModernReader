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
    public partial class AtTitle : UserControl
    {
        public AtTitle()
        {
            InitializeComponent();
        }

        public string SubTitle
        {
            get
            {
                return this.tbSubtitle.Text;
            }
            set
            {
                this.tbSubtitle.Text = value;
            }
        }

        public static DependencyProperty SubTitleProperty = DependencyProperty.Register("SubTitle", typeof(string), typeof(AtTitle), null);
    }
}
