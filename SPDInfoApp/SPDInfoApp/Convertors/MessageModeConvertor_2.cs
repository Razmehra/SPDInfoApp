using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
namespace SPDInfoApp.Convertors
{
   public class MessageModeConvertor_2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int iValue = Int32.Parse(value.ToString());
            bool result = iValue == 2;
            return result;

        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

    }
}
