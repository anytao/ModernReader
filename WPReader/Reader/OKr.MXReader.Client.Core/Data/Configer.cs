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
    public class Configer
    {
        public static string GetValue(string key)
        {
            return OkrRepository.GetConfig(key).AtConfig;
        }
    }
}
