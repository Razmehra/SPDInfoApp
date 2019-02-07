using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using SPDInfoApp.WebServices;
using System.Threading.Tasks;

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

        public LoginPageViewModel()
        {
            SubmitCommand = new Command(OnSubmitAsync);
            SkipCommand = new Command(OnSkipLoginAsync);
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
                //App.Current.Properties["UserName"] = data.Result.UserName;
                //App.Current.Properties["FirstName"] = data.Result.FirstName;
                //App.Current.Properties["LastName"] = data.Result.LastName;
                //App.Current.Properties["Role"] = data.Result.Role;
                //App.Current.Properties["Enabled"] = data.Result.Enabled;
                //App.Current.Properties["Email"] = data.Result.Email;
                //await App.Current.SavePropertiesAsync();

                //Application.Current.MainPage = new EntryPage();
            }

            //////////////////// Implement Login check through web service

        }

    }
}
