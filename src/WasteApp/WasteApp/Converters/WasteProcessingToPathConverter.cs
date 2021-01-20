using System;
using System.Globalization;
using WasteApp.Core;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace WasteApp
{
    public class WasteProcessingToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Geometry path = null;

            if (value == null)
                return path;

            switch ((WasteProcessingEnum)value)
            {
                case WasteProcessingEnum.Recycle:
                    path = App.Current.Resources.GetValue<Geometry>("RecycleGeometry");
                    break;
                case WasteProcessingEnum.Green:
                    path = App.Current.Resources.GetValue<Geometry>("LeaveGeometry");
                    break;
                case WasteProcessingEnum.Garbage:
                    path = App.Current.Resources.GetValue<Geometry>("RubbishBinGeometry");
                    break;
                case WasteProcessingEnum.Yard:
                    path = App.Current.Resources.GetValue<Geometry>("TreeGeometry");
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
