using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SPDInfoApp.ViewModels
{
    public class DatesInfo

    {

        public ObservableCollection<object> Dates { get; set; }

        //Day is the collection of day numbers

        private ObservableCollection<string> Day { get; set; }

        //Month is the collection of Month Names

        private ObservableCollection<string> Month { get; set; }

        //Year is the collection of Years from 1990 to 2050

        private ObservableCollection<string> Year { get; set; }

        public DatesInfo()

        {

            Dates = new ObservableCollection<object>();

            //Populate Day, Month and Year values of each collection

            PopulateDates();

            //first column of SfPicker

            Dates.Add(Day);

            //Second column of SfPicker

            Dates.Add(Month);

            //Third column of SfPicker

            Dates.Add(Year);

        }

        private void PopulateDates()

        {

            Day = new ObservableCollection<string>();

            Month = new ObservableCollection<string>();

            Year = new ObservableCollection<string>();

            for (int i = 1; i <= 31; i++)

                Day.Add(i.ToString());

            for (int i = 1; i <= 12; i++)

                Month.Add(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));

            for (int i = 1990; i <= 2050; i++)

                Year.Add(i.ToString());

        }

    }
}
