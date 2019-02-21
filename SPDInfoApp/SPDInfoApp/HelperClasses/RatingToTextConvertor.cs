using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SPDInfoApp.HelperClasses
{
    public class RatingToTextConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var feedbackValue = (double)value;
            if (feedbackValue == 0) feedbackValue = 0;
            string feedbackText;
            switch (feedbackValue)
            {
                case 1:
                    feedbackText = "Poor";
                    break;
                case 2:
                    feedbackText = "Tolerable";
                    break;

                case 3:
                    feedbackText = "Moderate";
                    break;
                case 4:
                    feedbackText = "Good";
                    break;
                case 5:
                    feedbackText = "Excellent";
                    break;
                default:
                    feedbackText = "";
                    break;
            }
            return feedbackText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
