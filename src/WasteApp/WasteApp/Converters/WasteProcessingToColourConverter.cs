using System;
using System.Globalization;
using WasteApp.Core;
using Xamarin.Forms;

namespace WasteApp
{
    public class WasteProcessingToColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.Transparent;

            if (value == null)
                return color;

            switch ((WasteProcessingEnum)value)
            {
                case WasteProcessingEnum.Recycle:
                    color = App.Current.Resources.GetValue<Color>("BlueColour");
                    break;
                case WasteProcessingEnum.Green:
                    color = App.Current.Resources.GetValue<Color>("GreenColour");
                    break;
                case WasteProcessingEnum.Garbage:
                    color = App.Current.Resources.GetValue<Color>("OrangeColour");
                    break;
                case WasteProcessingEnum.Yard:
                    color = App.Current.Resources.GetValue<Color>("YellowColour");
                    break;
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
