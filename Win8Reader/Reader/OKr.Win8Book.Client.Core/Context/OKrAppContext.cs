using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OKr.Common.Context;
using OKr.Common.Storage;
using OKr.Win8Book.Client.Core.Data;
using Windows.ApplicationModel;
using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace OKr.Win8Book.Client.Core.Context
{
    public class OKrAppContext : OKrStateContextBase<OKrApps>
    {
        public OKrAppContext()
            : base(new OKrStorage<OKrApps>(OKrBookConstant.OKRAPPS), true)
        {
        }

        protected async override Task<OKrApps> DoLoad()
        {
            OKrApps data = new OKrApps();

            Package package = Package.Current;
            StorageFolder installedLocation = package.InstalledLocation;

            var file = await StorageFile.GetFileFromPathAsync(Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, @"Assets\Data\okrapp.config"));

            XmlDocument doc = await XmlDocument.LoadFromFileAsync(file);

            XDocument xdoc = XDocument.Parse(doc.GetXml());

            var ele = xdoc.Element("okrapp").Element("apps").Elements("app");

            foreach (var item in ele)
            {
                OKrApp app = new OKrApp();
                app.AppId = item.Element("id").Value;
                app.PId = item.Element("pid").Value;
                app.AppName = item.Element("name").Value;
                app.Author = item.Element("author").Value;
                app.Intro = item.Element("intro").Value;
                app.Pic = "ms-appx:///Assets/Data/app/" + item.Element("icon").Value;

                data.Apps.Add(app);
            }

            return data;
        }

        protected override void DoSave(OKrApps data)
        {
        }
    }
}
