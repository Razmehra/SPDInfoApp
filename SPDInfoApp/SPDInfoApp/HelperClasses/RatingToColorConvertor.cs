using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SPDInfoApp.HelperClasses
{
    class RatingToColorConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var feedbackValue = (double)value;
            if (feedbackValue == 0) feedbackValue = 0;
            Color feedbackColor;
            switch (feedbackValue)
            {
                case 1:
                    feedbackColor = Color.Red;
                    break;
                case 2:
                    feedbackColor = Color.FromHex("#ff8000");
                    break;

                case 3:
                    feedbackColor = Color.Purple;
                    break;
                case 4:
                    feedbackColor = Color.FromHex("#325b32");
                    break;
                case 5:
                    feedbackColor = Color.ForestGreen;
                    break;
                default:
                    feedbackColor = Color.Red;
                    break;
            }
            return feedbackColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.Red;
        }
    }
}
