using SPDInfoApp.Models;
using SPDInfoApp.ViewModels;
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
            BindingContext = this;
            InitializeComponent();

            FBList.Add(new MstFeedbackModel() { ID = FBList.Count + 1, FBQuestion = "Ques1", MValue = 1, IsShow = true });
            FBList.Add(new MstFeedbackModel() { ID = FBList.Count + 1, FBQuestion = "Ques2", MValue = 2, IsShow = true });
            FBList.Add(new MstFeedbackModel() { ID = FBList.Count + 1, FBQuestion = "Ques3", MValue = 3, IsShow = true });
            FBList.Add(new MstFeedbackModel() { ID = FBList.Count + 1, FBQuestion = "Ques4", MValue = 4, IsShow = true });
            FBList.Add(new MstFeedbackModel() { ID = FBList.Count + 1, FBQuestion = "Ques5", MValue = 5, IsShow = true });
            FBList.Add(new MstFeedbackModel() { ID = FBList.Count + 1, FBQuestion = "Ques6", MValue = 6, IsShow = true });
            FBList.Add(new MstFeedbackModel() { ID = FBList.Count + 1, FBQuestion = "Ques7", MValue = 7, IsShow = true });
            FBList.Add(new MstFeedbackModel() { ID = FBList.Count + 1, FBQuestion = "Ques8", MValue = 8, IsShow = true });
            FBList.Add(new MstFeedbackModel() { ID = FBList.Count + 1, FBQuestion = "Ques9", MValue = 9, IsShow = true });
            FBList.Add(new MstFeedbackModel() { ID = FBList.Count + 1, FBQuestion = "Ques10", MValue = 10, IsShow = true });
            LVFeedbacks.BindingContext = this;
            LVFeedbacks.ItemsSource = FBList.OrderByDescending(w => w.ID);
        }

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            MstFeedbackModel studentFeedback = new MstFeedbackModel()
            {
                FBQuestion = EntryFBQuestion.Text,
                MValue = Convert.ToInt32(FBValue.Value),
                IsShow = true,
                ID = FBList.Count + 1

            };
            FBList.Add(studentFeedback);
            LVFeedbacks.ItemsSource = null;
            LVFeedbacks.ItemsSource = FBList.OrderByDescending(w => w.ID);
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

            FBList.Where(w => w.ID == s).FirstOrDefault().IsShow = false;
            // LVFeedbacks.ItemsSource = null;
            // LVFeedbacks.ItemsSource = FBList;
        }
        private async void TapGestureRecognizerEdit_Tapped(object sender, EventArgs e)
        {
            AddLayout.IsVisible = false;
            var s = Int32.Parse(((Image)sender).ClassId);

            var FB = FBList.Where(w => w.ID == s).FirstOrDefault();
            EntryFBQuestion.Text = FB.FBQuestion;
            FBValue.Value = FB.MValue;
            EntryFBQuestion.ClassId = FB.ID.ToString();
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
                        FBList.Where(w => w.ID == xID).FirstOrDefault().FBQuestion = EntryFBQuestion.Text;
                        FBList.Where(w => w.ID == xID).FirstOrDefault().MValue = Convert.ToInt32(FBValue.Value);
                        AddButton.ClassId = "N";
                        break;
                    case "N":
                        if (!await DisplayAlert("Add New Feedback", "Sure to save?", "Yes", "No")) return;
                        MstFeedbackModel studentFeedback = new MstFeedbackModel()
                        {
                            FBQuestion = EntryFBQuestion.Text,
                            MValue = Convert.ToInt32(FBValue.Value),
                            IsShow = true,
                            ID = FBList.Count + 1

                        };
                        FBList.Add(studentFeedback);
                        LVFeedbacks.ItemsSource = null;
                        LVFeedbacks.ItemsSource = FBList.OrderByDescending(w => w.ID);
                        break;
                    default:
                        break;
                }
            }

            AddLayout.IsVisible = !AddLayout.IsVisible;
            AddButton.Source = AddLayout.IsVisible ? "Save.png" : "add.png";

            lblFeedback.Text = AddLayout.IsVisible ? "Save Feedback" : "New Feedback";

            //AddButton.ClassId = AddLayout.IsVisible ? "E" : "";

        }
    }
}