using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKr.Common.Context;
using OKr.Common.Storage;

namespace OKr.Win8Book.Client.Core.Context
{
    public abstract class OKrBookContextBase<TData> : OKrStateContextBase<TData>
    {
        public OKrBookContextBase(OKrStorage<TData> data, bool cached)
            : this(new OKrBookService(), data, cached)
        { 
        }

        public OKrBookContextBase(OKrBookService service, OKrStorage<TData> data, bool cached)
            : base(data, cached)
        {
            this.service = service;
        }

        private OKrBookService service;
    }
}
