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
using System.Xml.Linq;
using System.IO.IsolatedStorage;
using OKr.MXReader.Client.Core.Context;

namespace OKr.MXReader.Client.Core
{
    public class Initilizer
    {
        public static void LoadConfig()
        {
            Uri uriResource = new Uri("_static/data/okr.me", UriKind.Relative);
            XElement element = XElement.Load(Application.GetResourceStream(uriResource).Stream);

            // Load the app from config
            XElement appElement = element.Element("app");
            OkrApp app = new OkrApp();
            app.AppId = appElement.Element("appid").Value;
            app.AppName = appElement.Element("name").Value;
            app.Intro = appElement.Element("intro").Value;
            app.Version = appElement.Element("v").Value;
            app.Build = appElement.Element("build").Value;
            app.Us = appElement.Element("us").Value;
            app.Email = appElement.Element("email").Value;
            app.Url = appElement.Element("url").Value;

            app.Ad = new Ad();
            app.Ad.AppID = appElement.Element("ad").Element("appid").Value;
            app.Ad.UnitID = appElement.Element("ad").Element("unitid").Value;
            app.Ad.IsShow = bool.Parse(appElement.Element("ad").Element("isshow").Value);

            OkrBookContext.Current.App = app;

            // Load the book from config
            XElement bookElement = element.Element("okrbook");
            BookConfig config = new BookConfig();
            config.Name = bookElement.Element("name").Value;
            config.Author = bookElement.Element("author").Value;
            config.Intro = bookElement.Element("intro").Value;
            config.Path = bookElement.Element("basepath").Value;
            config.Data = bookElement.Element("datapath").Value;
            config.Status = bookElement.Element("status").Value;
            config.ReadNum = bookElement.Element("readnum").Value;

            OkrBookContext.Current.Config = config;
        }

        public static void InitData()
        {
            //DataInitilizer init = new DataInitilizer();
            //init.Init();

            LoadConfig();
        }
    }
}
