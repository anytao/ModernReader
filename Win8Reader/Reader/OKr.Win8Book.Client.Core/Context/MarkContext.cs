using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKr.Common.Context;
using OKr.Common.Storage;
using OKr.Win8Book.Client.Core.Data;

namespace OKr.Win8Book.Client.Core.Context
{
    public class MarkContext : OKrStateContextBase<Mark>
    {
        public MarkContext()
            : base(new OKrStorage<Mark>(OKrBookConstant.MARK), true)
        {
 
        }


        protected async override Task<Mark> DoLoad()
        {
            return new Mark();
        }

        protected override void DoSave(Mark data)
        {
        }
    }
}
