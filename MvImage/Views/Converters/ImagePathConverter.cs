using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace MvImage.Views.Converters
{
    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 入力が文字列か確認
            if (value is not string path)
            {
                return null;
            }

            // 存在しないパスを Image コントロールに返却すると例外がスローされるため、その場合は null を返す。
            return !File.Exists(path) ? null : path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}