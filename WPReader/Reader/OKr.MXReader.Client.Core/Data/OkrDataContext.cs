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
using System.Data.Linq;

namespace OKr.MXReader.Client.Core.Data
{
    public class OkrDataContext : DataContext
    {
        public OkrDataContext(string conn)
            : base(conn)
        {
 
        }

        public Table<Chapter> Chapters;
        public Table<BookConfig> BookConfig;
        public Table<ChapterMark> Marks;

        public Table<Config> Config;
    }
}
