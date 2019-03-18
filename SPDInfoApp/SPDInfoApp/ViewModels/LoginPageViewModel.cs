using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using SPDInfoApp.WebServices;
using System.Threading.Tasks;
using SPDInfoApp.Views;
using SPDInfoApp.Models;

namespace SPDInfoApp.ViewModels
{
    class LoginPageViewModel : BaseViewModel
    {

        public Action DisplayInvalidLoginPromt;
        private string userName;
        public string UserName
        {
            set { userName = value; OnPropertyChanged(); }
            get { return userName; }
        }

        private string password;
        public string Password
        {
            set { password = value; OnPropertyChanged(); }// PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password")); }
            get { return password; }
        }

        public ICommand SubmitCommand { get; set; }
        public ICommand SkipCommand { get; set; }
        public ICommand CancleCommand { get; set; }

        public LoginPageViewModel()
        {
            SubmitCommand = new Command(OnSubmitAsync);
            SkipCommand = new Command(OnSkipLoginAsync);
            CancleCommand = new Command(OnCancleAsync);
        }

        private async void OnCancleAsync()
        {
            await CancleLogin();
        }

        private async Task CancleLogin()
        {
            Application.Current.MainPage = new LoginMenu();
        }

        private async void OnSkipLoginAsync()
        {
            await SkipLogin();
        }


        private async Task SkipLogin()
        {
            Application.Current.MainPage = new EntryPage();
        }

        public async void OnSubmitAsync()
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)) { DisplayInvalidLoginPromt(); return; }

            //////////////////// Implement Login check through web service
            string[] Logindata = { userName, password };

            PHPServices MyService = new PHPServices();
            string result = "";
            using (UserDialogs.Instance.Loading("Validating user.\nPlease Wait.", null, null, true, MaskType.Black))
            {
                result = await MyService.Login(Logindata);
            }
            //User data = User.FromJson(result);

            if (result == "failure")
            {
                DisplayInvalidLoginPromt();
                return;
            }
            else
            {
                //User data = User.FromJson(result);
                App.Current.Properties["AdminUserName"] = UserName;
                App.Current.Properties["AdminUserPW"] = Password;
                LoginInfo loginInfo = new LoginInfo();


                await App.Current.SavePropertiesAsync();

                Application.Current.MainPage = new PinPage("Admin");
            }

            //////////////////// Implement Login check through web service

        }

    }
}
