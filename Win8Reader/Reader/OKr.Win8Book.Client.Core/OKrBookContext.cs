using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKr.Common.Context;
using OKr.Win8Book.Client.Core.Data;

namespace OKr.Win8Book.Client.Core
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
