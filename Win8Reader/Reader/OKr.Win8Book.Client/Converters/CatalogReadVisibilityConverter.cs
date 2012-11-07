using OKr.Win8Book.Client.ViewModel;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace OKr.Win8Book.Client.Converters
{
    public sealed class CatalogReadVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChapterGroup group = value as ChapterGroup;
            if (group != null)
            {
                return group.Chapters.Count(x => x.IsRead) > 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
