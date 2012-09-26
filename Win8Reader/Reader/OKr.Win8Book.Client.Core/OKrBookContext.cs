using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKr.Common.Context;
using OKr.Common.Storage;
using OKr.Win8Book.Client.Core.Data;
using Windows.Storage;

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

        public async Task Init()
        {
            // this.App = 
            // this.Config = 

            this.InitState();
            await this.InitFile();
        }

        private async Task InitFile()
        {
            StorageFile category = await StorageFile.GetFileFromPathAsync(Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, @"Assets\Data\book\category.txt"));

            OKrStorage storage = new OKrStorage();
            StorageFolder folder = await storage.GetFolderIfExistsAsync(Windows.Storage.ApplicationData.Current.LocalFolder, "book");
            if (folder == null)
            {
                folder = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFolderAsync("book");
            }           

            StorageFile categoryFile = null;

            try
            {
                categoryFile = await folder.GetFileAsync("category.txt");
            }
            catch
            {
            }

            if (categoryFile == null)
            {
                await category.CopyAsync(folder);
            }          
        }

        private void InitState()
        {
        }

        #region Variables

        private static OKrBookContext instance = new OKrBookContext();

        #endregion
    }
}
