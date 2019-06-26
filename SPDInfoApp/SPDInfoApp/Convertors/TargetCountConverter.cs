using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace SPDInfoApp.Convertors
{
    public class TargetCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string targetJson = (string)value;
            var matches = Regex.Matches(targetJson, "AppID");
            return " "+ matches.Count.ToString()+ " Targets  ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string targetJson = (string)value;
            var matches = Regex.Matches(targetJson, "AppID");
            return " "+ matches.Count.ToString() + " Targets  ";

        }
    }
}
