using SPDInfoApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SPDInfoApp.ViewModels
{
    class LoginMenuViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; set; }
        public bool isAdmin { get; set; }
        public bool isStudent { get; set; }
        public bool isAlumni { get; set; }
        public LoginMenuViewModel()
        {
            LoginCommand = new Command (OnLoginSubmit);
            isAdmin = false;
            isStudent = false;
            isAlumni = false;
        }

        private void OnLoginSubmit()
        {
            if (isAdmin)
            {
                string MPIN = (Xamarin.Forms.Application.Current.Properties["AuthMPIN_Admin"].ToString());
                string UName = (Xamarin.Forms.Application.Current.Properties["AdminUserName"].ToString());
                string UPW = (Xamarin.Forms.Application.Current.Properties["AdminUserPW"].ToString());
                if (MPIN != "" && UName != "" && UPW != "")
                {
                    Application.Current.MainPage = new PinPage("Admin");
                }
                else
                {
                    Application.Current.MainPage = new LoginPage();
                }
                return;
            }
            else
            {
                string MPIN;
                string UName;
                string Mobile;
                if (isStudent)
                {
                    MPIN = (Xamarin.Forms.Application.Current.Properties["AuthMPIN_Student"].ToString());
                    UName = (Xamarin.Forms.Application.Current.Properties["StudentName"].ToString());
                    Mobile = (Xamarin.Forms.Application.Current.Properties["StudentMobile"].ToString());

                }
                else
                {
                    MPIN = (Xamarin.Forms.Application.Current.Properties["AuthMPIN_Alumni"].ToString());
                    UName = (Xamarin.Forms.Application.Current.Properties["AlumniName"].ToString());
                    Mobile = (Xamarin.Forms.Application.Current.Properties["AlumniMobile"].ToString());
                }

                if (MPIN != "" && UName != "")
                {
                    Application.Current.MainPage = new PinPage(isStudent ? "Student" : "Alumni");
                }
                else
                {

                    Application.Current.MainPage = new RequiredPage(isStudent ? "Student" : "Alumni");
                }
            }

        }
    }
}
