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
    public class SettingContext : OKrStateContextBase<Setting>
    {
        public SettingContext()
            : base(new OKrStorage<Setting>(OKrBookConstant.SETTING), true)
        {
 
        }

        protected async override Task<Setting> DoLoad()
        {
            // init the settings here
            Setting setting = new Setting();
            setting.Font = OKrBookConfig.DEFALUTFONTSIZE;

            return setting;
        }

        protected override void DoSave(Setting data)
        {
            throw new NotImplementedException();
        }
    }
}
