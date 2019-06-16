using SPDInfoApp.HelperClasses;
using SPDInfoApp.Models;
using SPDInfoApp.ViewModels;
using SPDInfoApp.WebServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackEntry : INotifyPropertyChanged
    {
        private ObservableCollection<MstFeedbackModel> _fblist = new ObservableCollection<MstFeedbackModel>();
        public ObservableCollection<MstFeedbackModel> FBList
        {
            get { return _fblist; }
            set
            {
                _fblist = value;
                OnPropertyChanged("FBList");
            }
        }



        public FeedbackEntry()
        {
            BindingContext = FBList;
            InitializeComponent();
            FetchFeedbacks();
        }

        private async void FetchFeedbacks()
        {
            PHPServices service = new PHPServices();

            var result = await service.FetchStudentMasterFeedbacks(new string[] { "0" });

            if (result.ToString() == "failure")
            {
                return;
            }
            var data = Utils.DeserializeFromJson<ObservableCollection<MstFeedbackModel>>(result);

            foreach (var item in data)
            {
                item.IsShow = true;
            }
            FBList = data;
            LVFeedbacks.BindingContext = FBList;
            LVFeedbacks.ItemsSource = null;
            LVFeedbacks.ItemsSource = FBList.OrderByDescending(w => w.FBID);

        }

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            MstFeedbackModel studentFeedback = new MstFeedbackModel()
            {
                FBQuestion = EntryFBQuestion.Text,
                MValue = Convert.ToInt32(FBValue.Value),
                IsShow = true,
                FBID = FBList.Count + 1

            };
            FBList.Add(studentFeedback);
            LVFeedbacks.ItemsSource = null;
            LVFeedbacks.ItemsSource = FBList.OrderByDescending(w => w.FBID);
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            LblStars.Text = "No of Stars: " + e.NewValue.ToString();
            ;
        }

        // TappedEventArgs
        private async void TapGestureRecognizerDelete_Tapped(object sender, EventArgs e)
        {
            if (!await DisplayAlert("Delete Feedback", "Sure to delete?", "Yes", "No")) return;

            var s = Int32.Parse(((Image)sender).ClassId);

            FBList.Where(w => w.FBID == s).FirstOrDefault().IsShow = false;
            LVFeedbacks.BindingContext = FBList;
            LVFeedbacks.ItemsSource = null;
            LVFeedbacks.ItemsSource = FBList.OrderByDescending(w => w.FBID);
            BtnSubmit.IsVisible = true;
        }
        private async void TapGestureRecognizerEdit_Tapped(object sender, EventArgs e)
        {
            AddLayout.IsVisible = false;
            var s = Int32.Parse(((Image)sender).ClassId);

            var FB = FBList.Where(w => w.FBID == s).FirstOrDefault();
            EntryFBQuestion.Text = FB.FBQuestion;
            FBValue.Value = FB.MValue;
            EntryFBQuestion.ClassId = FB.FBID.ToString();
            AddButton.ClassId = "E";
            await CRUDEFeedback();
        }


        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            //AddButton.ClassId = "N";
            await CRUDEFeedback();
        }

        private async Task CRUDEFeedback()
        {


            if (AddLayout.IsVisible)
            {
                switch (AddButton.ClassId)
                {
                    case "":

                        break;
                    case "E":
                        if (!await DisplayAlert("Edit Feedback", "Sure to update?", "Yes", "No")) return;
                        var xID = Int32.Parse(EntryFBQuestion.ClassId);
                        FBList.Where(w => w.FBID == xID).FirstOrDefault().FBQuestion = EntryFBQuestion.Text;
                        FBList.Where(w => w.FBID == xID).FirstOrDefault().MValue = Convert.ToInt32(FBValue.Value);
                        AddButton.ClassId = "N";
                        EntryFBQuestion.Text = "";
                        FBValue.Value = 5;
                        BtnSubmit.IsVisible = true;
                        break;
                    case "N":
                        if (!await DisplayAlert("Add New Feedback", "Sure to save?", "Yes", "No")) return;
                        MstFeedbackModel studentFeedback = new MstFeedbackModel()
                        {
                            FBQuestion = EntryFBQuestion.Text,
                            MValue = Convert.ToInt32(FBValue.Value),
                            IsShow = true,
                            FBID = FBList.Count + 1

                        };
                        FBList.Add(studentFeedback);
                        LVFeedbacks.ItemsSource = null;
                        LVFeedbacks.ItemsSource = FBList.OrderByDescending(w => w.FBID);
                        EntryFBQuestion.Text = "";
                        FBValue.Value = 5;
                        BtnSubmit.IsVisible = true;
                        break;

                    case "C":
                        if (!await DisplayAlert("Add New Feedback", "Sure to Cancel?", "Yes", "No")) return;
                        EntryFBQuestion.Text = "";
                        FBValue.Value = 5;
                        // BtnSubmit.IsVisible = false;
                        break;

                    default:
                        break;
                }
            }

            AddLayout.IsVisible = !AddLayout.IsVisible;
            AddButton.Source = AddLayout.IsVisible ? "Save.png" : "add.png";

            lblFeedback.Text = AddLayout.IsVisible ? "Save Feedback" : "New Feedback";
            CancelButton.IsVisible = AddLayout.IsVisible;
            lblFeedbackCancel.IsVisible = AddLayout.IsVisible;
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            AddButton.ClassId = "C";
            await CRUDEFeedback();
        }

        private async void BtnSubmit_Clicked(object sender, EventArgs e)
        {
            PHPServices service = new PHPServices();

            foreach (var FB in FBList)
            {
                var result = await service.UpdateStudentMasterFeedbacks(new string[] { FB.FBID.ToString(), FB.FBQuestion, FB.MValue.ToString(), FB.IsShow ? "False" : "True" });
            }
            var result1 = await service.ResetStudentMasterFeedbacks(new string[] { "0" });
            await App.Current.MainPage.DisplayAlert("Feedbacks", "Feedbacks updated successfully.", "OK");
            BtnSubmit.IsVisible = false;
        }
    }
}