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
            string[] dataArray = {
                _entryData.AppearingClass,
                _entryData.ApplicationID.ToString(),
                _entryData.StudentName,
                _entryData.RollNo.ToString(),
                _entryData.EnrolmentNo.ToString(),
                _entryData.DOB.ToString("yyyy/MM/dd"),
                _entryData.Medium,
                _entryData.Gender,
                _entryData.Category,
                _entryData.RegCastCertificate,
                _entryData.IsHandicapped.ToString(),
                _entryData.HandicappDetail,
                _entryData.BloodGroup,
                _entryData.PhoneMobile,
                _entryData.SSSMId,
                _entryData.AadharNo,
                _entryData.EMail,
                _entryData.AddressPermanent,
                _entryData.AddressCurrent,
                _entryData.IsUrban.ToString(),
                _entryData.NativePlace,
                _entryData.RegNativeCertificateNo,
                _entryData.FHName,
                _entryData.MotherName,
                _entryData.PhoneMobile_Gaurdian,
                _entryData.IncomeFather.ToString(),
                _entryData.OccupationFather,
                _entryData.BankAcNo,
                _entryData.BankIFSC,
                _entryData.BankName,
                _entryData.BankBranch,
                _entryData.VoterID,
                _entryData.PANNo,
                _entryData.DrivingLicNo,
                _entryData.ScholershipName
            };

            

            //string[] dataArray = _entryData as string[];
            using (UserDialogs.Instance.Loading("Submiting data.\nPlease Wait.", null, null, true, MaskType.Black))
            {
                result = await MyService.Submit(dataArray);
            }
            //User data = User.FromJson(result);

            if (result == "failure")
            {
              await   App.Current.MainPage.DisplayAlert("Failed", "!Oops... Somthing went wrong. \n(" + result+")","Ok");
                return;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Sucess", "Information Submitted Successfully..", "Ok");
            }
        }
    }
}
