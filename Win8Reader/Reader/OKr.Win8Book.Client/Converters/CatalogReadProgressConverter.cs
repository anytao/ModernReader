using OKr.Win8Book.Client.ViewModel;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace OKr.Win8Book.Client.Converters
{
    public sealed class CatalogReadProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChapterGroup group = value as ChapterGroup;
            if (group!=null)
            {
                var readCount = group.Chapters.Count(x => x.IsRead);
                var progress = (double)readCount / (double)group.Chapters.Count;
                return progress;
            }
            return 0d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
