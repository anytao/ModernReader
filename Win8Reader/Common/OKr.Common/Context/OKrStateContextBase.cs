// 2012 OKr Works, http://okr.me

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKr.Common.Storage;

namespace OKr.Common.Context
{
    public abstract class OKrStateContextBase<TData> : IOKrStateContext<TData>
    {
        public OKrStateContextBase(OKrStorage<TData> storage, bool cached)
        {
            this.cached = cached;
            this.storage = storage;
            this.data = default(TData);
        }

        public async Task<TData> Load()
        {
            if (cached)
            {
                TData data = await storage.LoadAsync();

                if (data != null)
                {
                    return data;
                }
                else
                {
                    this.data = await DoLoad();

                    //todo: {WT}, save to local storage now
                    storage.SaveAsync(this.data);

                    return this.data;
                }
            }
            else
            {
                this.data = await DoLoad();

                //todo: {WT}, save to local storage now
                storage.SaveAsync(this.data);

                return this.data;
            }
        }

        public async Task Save(TData data)
        {
            try
            {
                await storage.SaveAsync(data);

                DoSave(data);
            }
            catch
            {
            }
        }

        public async Task Clear()
        {
            try
            {
                await storage.DeleteAsync();
            }
            catch
            {
            }
        }


        public TData Data
        {
            get { return this.data; }
        }

        protected abstract Task<TData> DoLoad();
        protected abstract void DoSave(TData data);

        private OKrStorage<TData> storage;
        private bool cached;
        private TData data;
    }
}
