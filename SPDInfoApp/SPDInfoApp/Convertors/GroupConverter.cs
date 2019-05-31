using SPDInfoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SPDInfoApp.Convertors
{
    public class GroupConverter : IValueConverter
    {
        public GroupConverter()
        {

        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // var orderInfo = value as OrderInfo;
            var orderInfo = value as StudentInfo;
            return orderInfo.EntryDate.Date.ToString("dd-MMM-yyyy");
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
