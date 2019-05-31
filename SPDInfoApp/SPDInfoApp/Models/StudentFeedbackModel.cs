using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SPDInfoApp.Models
{
    public class StudentFeedbackModel : INotifyPropertyChanged
    {
        public string ApplicationID { get; set; }
        public DateTime FBDateTime { get; set; }
        public int ID { get; set; }
        public string FeedbackQues { get; set; }
        private double _feedbackValue;
        public double FeedbackValue { get { return _feedbackValue; } set { _feedbackValue = value; OnPropertyChanged("FeedbackValue"); } }
        private string _feedbackText;
        public string FeedbackValueText { get { return _feedbackText; } set { _feedbackText = value; OnPropertyChanged("FeedbackValueText"); } }


        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
