using SPDInfoApp.Models;
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


        public StudentFeedback ()
		{
			InitializeComponent ();
            ObservableCollection<StudentFeedbackModel> FDList =  PopulateData();
            this.BindingContext = FDList;
            lstFeedback.BindingContext = this;
            lstFeedback.ItemsSource = FDList;
        }

        private ObservableCollection<StudentFeedbackModel> PopulateData()
        {
            ObservableCollection<StudentFeedbackModel> feedbackDatas = new ObservableCollection<StudentFeedbackModel>()
            {
               new StudentFeedbackModel{ ID=1, FeedbackQues="Teaching & Learning:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=2, FeedbackQues="Interaction with faculty & teacher:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=3, FeedbackQues="Interaction with administration and office staff:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=4, FeedbackQues="Examination and evaluation:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=5, FeedbackQues="Infrastructre & facilities:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=6, FeedbackQues="Library facilities:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=7, FeedbackQues="Sports facilities:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=8, FeedbackQues="Personality development and Placement facilities:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=9, FeedbackQues="Internet & Computer facilities:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=10, FeedbackQues="Extra-curricular activities:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=11, FeedbackQues="Overall rating:", FeedbackValue=0, FeedbackValueText=""},
            };
            return feedbackDatas;
        }

        private void BtnApply_Clicked(object sender, EventArgs e)
        {

        }
    }
}