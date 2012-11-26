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
using System.Linq;

namespace OKr.MXReader.Client.Core
{
    public class NavHelper
    {
        public static void Quit(Page page)
        {
            int count = page.NavigationService.BackStack.Count();
            for (int i = 0; i < count; i++)
            {
                page.NavigationService.RemoveBackEntry();
            }
        }
    }
}
