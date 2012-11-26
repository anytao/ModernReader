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
using OKr.MXReader.Client.Core.Data;
using System.IO.IsolatedStorage;

namespace OKr.MXReader.Client.Core.Context
{
    public class OkrBookContext
    {
        #region Ctor

        private OkrBookContext()
        {

        }

        #endregion

        #region Properties

        public static OkrBookContext Current
        {
            get
            {
                return instance;
            }
        }

        public BookConfig Config { get; set; }

        public OkrApp App { get; set; }

        #endregion

        #region Variables

        private static OkrBookContext instance = new OkrBookContext();

        #endregion
    }
}
