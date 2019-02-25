using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentFeedback : ContentPage
    {

        //protected void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        public StudentFeedback ()
		{
			InitializeComponent ();
            ObservableCollection<FeedbackData> FDList =  PopulateData();
            this.BindingContext = FDList;
            lstFeedback.BindingContext = this;
            lstFeedback.ItemsSource = FDList;
            

        }

        private ObservableCollection<FeedbackData> PopulateData()
        {
            ObservableCollection<FeedbackData> feedbackDatas = new ObservableCollection<FeedbackData>()
            {
               new FeedbackData{ ID=1, FeedbackQues="Teaching & Learning:", FeedbackValue=0, FeedbackValueText=""},
               new FeedbackData{ ID=2, FeedbackQues="Interaction with faculty & teacher:", FeedbackValue=0, FeedbackValueText=""},
               new FeedbackData{ ID=3, FeedbackQues="Interaction with administration and office staff:", FeedbackValue=0, FeedbackValueText=""},
               new FeedbackData{ ID=4, FeedbackQues="Examination and evaluation:", FeedbackValue=0, FeedbackValueText=""},
               new FeedbackData{ ID=5, FeedbackQues="Infrastructre & facilities:", FeedbackValue=0, FeedbackValueText=""},
               new FeedbackData{ ID=6, FeedbackQues="Library facilities:", FeedbackValue=0, FeedbackValueText=""},
               new FeedbackData{ ID=7, FeedbackQues="Sports facilities:", FeedbackValue=0, FeedbackValueText=""},
               new FeedbackData{ ID=8, FeedbackQues="Personality development and Placement facilities:", FeedbackValue=0, FeedbackValueText=""},
               new FeedbackData{ ID=9, FeedbackQues="Internet & Computer facilities:", FeedbackValue=0, FeedbackValueText=""},
               new FeedbackData{ ID=10, FeedbackQues="Extra-curricular activities:", FeedbackValue=0, FeedbackValueText=""},
               new FeedbackData{ ID=11, FeedbackQues="Overall rating:", FeedbackValue=0, FeedbackValueText=""},
            };
            return feedbackDatas;
        }

        private void BtnApply_Clicked(object sender, EventArgs e)
        {

        }
    }

    public class FeedbackData: INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string FeedbackQues { get; set; }
        private double _feedbackValue;
        public double FeedbackValue { get { return _feedbackValue; } set { _feedbackValue = value;OnPropertyChanged("FeedbackValue"); } }
        private string _feedbackText;
        public string FeedbackValueText { get { return _feedbackText; } set { _feedbackText = value;OnPropertyChanged(FeedbackValueText); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public FeedbackData()
        {
            
        }
        protected virtual void OnPropertyChanged(string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}