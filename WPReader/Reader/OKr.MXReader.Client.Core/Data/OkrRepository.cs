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
using System.Linq;

namespace OKr.MXReader.Client.Core.Data
{
    public class OkrRepository
    {
        public static string conn = @"isostore:/okrbook.sdf";

        public static void CreateDB()
        {
            using (OkrDataContext context = new OkrDataContext(conn))
            {
                if (!context.DatabaseExists())
                {
                    context.CreateDatabase();
                }
            }
        }

        public static void DeleteDB()
        {
            using (OkrDataContext context = new OkrDataContext(conn))
            {
                if (context.DatabaseExists())
                {
                    context.DeleteDatabase();
                }
            }
        }

        public static bool ExistDB()
        {
            using (OkrDataContext ctx = new OkrDataContext(conn))
            {
                return ctx.DatabaseExists();
            }
        }

        public static Config GetConfig(string key)
        {
            using (OkrDataContext ctx = new OkrDataContext(conn))
            {
                if (ctx.DatabaseExists())
                {
                    Config entity = ctx.Config.FirstOrDefault(x => x.AtKey == key);

                    return entity;
                }
            }

            return null;
        }
    }
}
