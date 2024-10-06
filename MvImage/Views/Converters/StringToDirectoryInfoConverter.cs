using System;
using System.Globalization;
using System.IO.Abstractions;
using System.Windows.Data;

namespace MvImage.Views.Converters
{
    public class StringToDirectoryInfoConverter : IValueConverter
    {
        private readonly IFileSystem fileSystem = new FileSystem();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = (DirectoryInfoWrapper)value;
            return d != null ? d.FullName : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // テキストボックスに入力された文字列が null または空白の場合
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return null;
            }

            var directoryPath = value.ToString();
            return string.IsNullOrWhiteSpace(directoryPath) ? null : fileSystem.DirectoryInfo.New(directoryPath);
        }
    }
}