using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SPDInfoApp.Models
{
    public class StudentFeedbackModel : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string FeedbackQues { get; set; }
        private double _feedbackValue;
        public double FeedbackValue { get { return _feedbackValue; } set { _feedbackValue = value; OnPropertyChanged("FeedbackValue"); } }

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _feedbackText;

        public event PropertyChangedEventHandler PropertyChanged;

        public string FeedbackValueText { get { return _feedbackText; } set { _feedbackText = value; OnPropertyChanged(FeedbackValueText); } }
    }
}
