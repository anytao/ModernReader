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
using System.Collections.Generic;
using System.Xml.Linq;

namespace OKr.MXReader.Client.Core.Data
{
    public class AppManager
    {
        static AppManager()
        {
            instance = new AppManager();
            apps = new List<AppInfo>();

            instance.Load();
        }

        public static IList<AppInfo> Apps
        {
            get
            {
                return apps;
            }
        }

        private void Load()
        {
            Uri uriResource = new Uri("_static/data/okrapp.config", UriKind.Relative);
            XElement element = XElement.Load(Application.GetResourceStream(uriResource).Stream);

            // Load the app from config
            var ele = element.Element("apps");

            foreach (var item in ele.Elements())
            {
                AppInfo app = new AppInfo();

                app.AppId = item.Element("id").Value;
                app.Name = item.Element("name").Value;
                app.Icon = item.Element("icon").Value;
                app.Intro = item.Element("intro").Value;
                app.Desc = item.Element("desc").Value;
                app.Type = item.Element("type").Value;

                apps.Add(app);
            }
        }

        private static IList<AppInfo> apps;
        private static AppManager instance;
    }
}
