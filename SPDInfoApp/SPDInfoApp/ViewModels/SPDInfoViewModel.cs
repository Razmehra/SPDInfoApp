using Acr.UserDialogs;
using SPDInfoApp.Models;
using SPDInfoApp.WebServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SPDInfoApp.ViewModels
{
    class SPDInfoViewModel : BaseViewModel
    {
        public SPDInfo _entryData { get; set; }// = new List<SPDInfo>();
        public ICommand SubmitCommand { get; set; }
        public DatesInfo datesInfo { get; set; }
        public SPDInfoViewModel(SPDInfo spdinfo = null)
        {
            // datesInfo = new DatesInfo();
            this._entryData = spdinfo;
            SubmitCommand = new Command(OnSubmitAsync);
        }

        public async void OnSubmitAsync()
        {
            if (!await App.Current.MainPage.DisplayAlert("Submit Information", "Are you sure?", "Yes", "No")) return;
            PHPServices MyService = new PHPServices();
            string result = "";
            using (UserDialogs.Instance.Loading("Submiting data.\nPlease Wait.", null, null, true, MaskType.Black))
            {
                result = await MyService.Submit(_entryData);
            }
            //User data = User.FromJson(result);

            if (result == "Success")
            {
                await App.Current.MainPage.DisplayAlert("Sucess", "Information Submitted Successfully..", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failed", "!Oops... Somthing went wrong. \n(" + result + ")", "Ok");
                return;

            }
        }
    }
}
