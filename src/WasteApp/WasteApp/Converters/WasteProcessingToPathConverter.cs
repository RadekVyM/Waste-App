using System;
using System.Globalization;
using WasteApp.Core;
using Xamarin.Forms;

namespace WasteApp
{
    public class WasteProcessingToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = "";

            if (value == null)
                return path;

            switch ((WasteProcessingEnum)value)
            {
                case WasteProcessingEnum.Recycle:
                    path = App.Current.Resources.GetValue<string>("RecyclePath");
                    break;
                case WasteProcessingEnum.Green:
                    path = App.Current.Resources.GetValue<string>("LeavePath");
                    break;
                case WasteProcessingEnum.Garbage:
                    path = App.Current.Resources.GetValue<string>("RubbishBinPath");
                    break;
                case WasteProcessingEnum.Yard:
                    path = App.Current.Resources.GetValue<string>("TreePath");
                    break;
            }

            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
