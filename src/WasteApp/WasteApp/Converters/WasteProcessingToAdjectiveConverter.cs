using System;
using System.Globalization;
using WasteApp.Core;
using Xamarin.Forms;

namespace WasteApp
{
    public class WasteProcessingToAdjectiveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            return (WasteProcessingEnum)value == WasteProcessingEnum.Recycle ? "Recyclable" : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
