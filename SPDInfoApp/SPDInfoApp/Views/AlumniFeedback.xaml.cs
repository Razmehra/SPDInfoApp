using SPDInfoApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlumniFeedback : ContentPage
    {
        public AlumniFeedback()
        {
            InitializeComponent();
            ObservableCollection<AlumniFeedbackModel> FDList = PopulateData();
            this.BindingContext = FDList;
            lstFeedback.BindingContext = this;
            lstFeedback.ItemsSource = FDList;
        }

        private ObservableCollection<AlumniFeedbackModel> PopulateData()
        {
            ObservableCollection<AlumniFeedbackModel> feedbackDatas = new ObservableCollection<AlumniFeedbackModel>()
            {
               new AlumniFeedbackModel{ ID=1, FeedbackQues="Teaching & Learning:", FeedbackValue=0, FeedbackValueText=""},
               new AlumniFeedbackModel{ ID=2, FeedbackQues="Interaction with faculty & teacher:", FeedbackValue=0, FeedbackValueText=""},
               new AlumniFeedbackModel{ ID=3, FeedbackQues="Interaction with administration and office staff:", FeedbackValue=0, FeedbackValueText=""},
               new AlumniFeedbackModel{ ID=4, FeedbackQues="Examination and evaluation:", FeedbackValue=0, FeedbackValueText=""},
               new AlumniFeedbackModel{ ID=5, FeedbackQues="Infrastructre & facilities:", FeedbackValue=0, FeedbackValueText=""},
               new AlumniFeedbackModel{ ID=6, FeedbackQues="Library facilities:", FeedbackValue=0, FeedbackValueText=""},
               new AlumniFeedbackModel{ ID=7, FeedbackQues="Sports facilities:", FeedbackValue=0, FeedbackValueText=""},
               new AlumniFeedbackModel{ ID=8, FeedbackQues="Personality development and Placement facilities:", FeedbackValue=0, FeedbackValueText=""},
               new AlumniFeedbackModel{ ID=9, FeedbackQues="Internet & Computer facilities:", FeedbackValue=0, FeedbackValueText=""},
               new AlumniFeedbackModel{ ID=10, FeedbackQues="Extra-curricular activities:", FeedbackValue=0, FeedbackValueText=""},
               new AlumniFeedbackModel{ ID=11, FeedbackQues="Overall rating:", FeedbackValue=0, FeedbackValueText=""},
            };
            return feedbackDatas;
        }

        private void BtnApply_Clicked(object sender, EventArgs e)
        {

        }
    }
}