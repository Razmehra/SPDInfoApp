using Acr.UserDialogs;
using SPDInfoApp.HelperClasses;
using SPDInfoApp.Models;
using SPDInfoApp.WebServices;
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
    public partial class AdminFeedbacksRep_STD : ContentPage
    {
        public ObservableCollection<JsonStudentFeedback> StudentFeedbacks = new ObservableCollection<JsonStudentFeedback>();
        public ObservableCollection<FBDates> fBDates = new ObservableCollection<FBDates>();
        public ObservableCollection<StudentFeedbackModel> FBList { get; set; }

        public String[] DtArray = new string[] { };
        public AdminFeedbacksRep_STD()
        {
            InitializeComponent();
            FetchFeedbacks();
            // BusyIndicator.
        }

        private async void FetchFeedbacks()
        {
            PHPServices pHPServices = new PHPServices();
            using (UserDialogs.Instance.Loading("Fetching Feedbacks.\nPlease Wait.", null, null, true, MaskType.Black))
            {

                var res = await pHPServices.FetchStudentFeedbacks(new string[] { "0" });
                if (res == "failure")
                {

                }
                else
                {
                    var data = HelperClasses.Utils.DeserializeFromJson<ObservableCollection<JsonStudentFeedback>>(res);
                    LVFBStudent.BindingContext = data;
                    LVFBStudent.ItemsSource = null;
                    LVFBStudent.ItemsSource = data;

                }
            }
        }

        private async void LVFBStudent_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            using (UserDialogs.Instance.Loading("Loading.\nPlease Wait..", null, null, true, MaskType.Black))
            {

                lstFeedback.IsVisible = false;
                BusyIndicator.IsVisible = true;
                BusyIndicator.IsRunning = true;
                var fb = e.SelectedItem as JsonStudentFeedback;
                var fbjson = fb.FBJson;

                var fbs = Utils.DeserializeFromJson<ObservableCollection<StudentFeedbackModel>>(fbjson);
                FBList = fbs;
                var dtList = fbs.Select(w => w.FBDateTime).Distinct().ToList();
                fBDates.Clear();
                foreach (var fbdate in dtList)
                {
                    fBDates.Add(new FBDates() { FBDateTime = fbdate, FormatedDate = fbdate.ToString("dd-MMM-yyy HH:mm:ss tt") });

                }
                cmbFBDates.BindingContext = fBDates;
                cmbFBDates.DataSource = null;
                cmbFBDates.DataSource = fBDates;
                if (fBDates.Count > 0) cmbFBDates.SelectedValue = fBDates.Select(w => w.FBDateTime).FirstOrDefault();
            }

        }

        public class FBDates
        {
            public DateTime FBDateTime { get; set; }
            public string FormatedDate { get; set; }
        }


        private void CmbFBDates_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value.ToString()) || e.Equals("")) return;
            lstFeedback.IsVisible = false;
            BusyIndicator.IsVisible = true;
            BusyIndicator.IsRunning = true;

            var xx = e.Value as FBDates;

            var xx3 = FBList.Where(w => w.FBDateTime.Equals(xx.FBDateTime)).FirstOrDefault();
            FBList.Select(w => w.FBValueString = w.FeedbackValue.ToString()+" Out of "+(w.MValue==0?5:w.MValue).ToString()+" Star").ToList();
            FBList.Where(w => w.FBDateTime.Equals(xx.FBDateTime));
            var xx2 = FBList;
            lstFeedback.BindingContext = null;
            lstFeedback.ItemsSource = null;
            lstFeedback.ItemsSource = null;
            // await Task.Delay(100);
            lstFeedback.BindingContext = FBList;
            lstFeedback.ItemsSource = null;
            lstFeedback.ItemsSource = FBList.Where(w => w.FBDateTime.Equals(xx.FBDateTime));
            lstFeedback.IsVisible = true;
            BusyIndicator.IsVisible = false;
            BusyIndicator.IsRunning = false;


        }
    }
}