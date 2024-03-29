﻿using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageAdmin : ContentPage
    {
        public HomePageAdmin()
        {
            InitializeComponent();
        }

        private void BtnGetStudentData_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdminStudentReport() { BackgroundColor = Color.GhostWhite, Title = "Student Information List" });
        }

        private void BtnGetAlumniData_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnFeedback_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FeedbackEntry() { BackgroundColor = Color.GhostWhite, Title = "Feedback Master (Student)" });

        }

        private void Entries_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnEntries_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdminStudentReport() { BackgroundColor = Color.GhostWhite, Title = "Student Information List", Icon = "SPDLogo.png" });
        }

        private void BtnFeedbackSTD_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdminFeedbacksRep_STD() { BackgroundColor = Color.GhostWhite, Title = "Feedbacks (Student)", Icon = "SPDLogo.png" });
        }

        private void BtnAlumnyEntries_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnAlumnyFeedbacks_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnfeedbackMaster_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FeedbackEntry() { BackgroundColor = Color.GhostWhite, Title = "Feedback Master (Student)", Icon = "SPDLogo.png" });
        }

        private void BtnfeedbackMasterAlumni_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnBroadcastMsg_Clicked(object sender, EventArgs e)
        {
            //  Navigation.PushAsync(new BCMessages() { BackgroundColor = Color.GhostWhite, Title = "Settings: Broadcast Messages", Icon = "SPDLogo.png" });
            Navigation.PushAsync(new BCMessageCenter() { BackgroundColor = Color.FloralWhite, Title = "Settings: Broadcast Messages Center", Icon = "SPDLogo.png" });
        }

        private void BtnBroadcastEvent_Clicked(object sender, EventArgs e)
        {

        }
    }
}