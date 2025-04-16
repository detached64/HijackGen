using System;
using System.Globalization;
using System.Windows.Data;

namespace HijackGen.GUI
{
    /// <summary>
    /// Converting between selected index and output file format.
    /// </summary>
    internal class FormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
                => (int)value == int.Parse(parameter.ToString());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => (bool)value ? parameter : Binding.DoNothing;
    }
}
