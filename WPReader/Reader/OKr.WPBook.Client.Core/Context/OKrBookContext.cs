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
using OKr.WPBook.Client.Core.Data;
using OKr.WPBook.Client.Core.Config;
using OKr.Phone.Common.Context;

namespace OKr.WPBook.Client.Core.Context
{
    public class OKrBookContext : OKrContextBase
    {
        #region Ctor

        private OKrBookContext()
        {

        }

        #endregion

        #region Properties

        public static OKrBookContext Current
        {
            get
            {
                return instance;
            }
        }

        public OKrBookConfig Config { get; set; }

        public OKrApp App { get; set; }

        #endregion

        #region Variables

        private static OKrBookContext instance = new OKrBookContext();

        #endregion
    }
}
