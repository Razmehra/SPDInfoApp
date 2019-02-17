using Acr.UserDialogs;
using SPDInfoApp.Models;
using SPDInfoApp.ViewModels;
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
    public partial class EntryPage : ContentPage
    {
        private SPDInfoViewModel _vm;
        public EntryPage()
        {
            BindingContext = new  DatesInfo();
            _vm = new SPDInfoViewModel();
            InitializeComponent();
        }

        private void BtnApply_Clicked(object sender, EventArgs e)
        {
            SPDInfo data = CollectData();
            if (data == null) return;
            _vm = new SPDInfoViewModel(data);
            _vm.SubmitCommand.Execute(null);

        }

        private SPDInfo CollectData()
        {

            if (!ValidateEntry()) return null;

            try
            {
                SPDInfo sPDInfo = new SPDInfo();
                sPDInfo.AppearingClass = txtAppearingClass.Text;
                sPDInfo.ApplicationID = int.Parse(txtApplicationID.Text);
                sPDInfo.StudentFName = txtStudentName.Text.Trim();
                sPDInfo.RollNo = txtRollNo.Text == null ? 0 : (double.Parse(txtRollNo.Text));
                sPDInfo.EnrolmentNo = txtEnrolmentNo.Text == null ? 0 : double.Parse(txtEnrolmentNo.Text);
                sPDInfo.DOB = DateTime.Parse(lblDOB.Text.ToString());
                sPDInfo.Medium = ((bool)radioButtonHindi.IsChecked ? "Hindi" : (bool)radioButtonEnglish.IsChecked ? "English" : "Other");
                sPDInfo.Gender = ((bool)radioButtonTrans.IsChecked ? "Trans" : (bool)radioButtonFemale.IsChecked ? "Female" : "Male");
                sPDInfo.Category = ((bool)radioButtonSC.IsChecked ? "SC" : (bool)radioButtonST.IsChecked ? "ST" : (bool)radioButtonOBC.IsChecked ? "OBC" : "General");
                sPDInfo.RegCastCertificate = txtRegCastCertificate.Text;
                sPDInfo.IsHandicapped = (bool)radioButtonHCYes.IsChecked ? true : false;
                sPDInfo.HandicapType = cmbHCType.Text;
                sPDInfo.HandicappPercent = txtHCPercent.Text == null ? "0" : txtHCPercent.Text;
                sPDInfo.HandicappDetail = txtHCOtherTypeDetail.Text;
                sPDInfo.BloodGroup = cmbBloodGroup.Text;// + ((bool)radioButtonPositive.IsChecked ? "+" : (bool)radioButtonNagetive.IsChecked ?"-":"");
                sPDInfo.PhoneMobile = txtPhoneMobile.Text;
                sPDInfo.AadharNo = txtAadharNo.Text;
                sPDInfo.EMail = txtEMail.Text;
                sPDInfo.AddressPermanent = txtAddressPermanent.Text;
                sPDInfo.AddressCurrent = txtAddressCurrent.Text;
                sPDInfo.IsUrban = (bool)radioButtonUrban.IsChecked ? true : false;
                sPDInfo.Domicile = txtNativePlace.Text;
                sPDInfo.RegNativeCertificateNo = txtRegNativeCertificateNo.Text;
                sPDInfo.FHName = txtFHName.Text;
                sPDInfo.MotherName = txtMotherName.Text;
                sPDInfo.PhoneMobile_Gaurdian = txtPhoneMobile_Gaurdian.Text;
                sPDInfo.IncomeFather = txtIncomeFather.Text == null ? 0 : double.Parse(txtIncomeFather.Text);
                sPDInfo.OccupationFather = txtOccupationFather.Text;
                sPDInfo.BankAcNo = txtBankAcNo.Text;
                sPDInfo.BankIFSC = txtBankIFSC.Text;
                sPDInfo.BankName = txtBankName.Text;
                sPDInfo.BankBranch = txtBankBranch.Text;
                sPDInfo.VoterID = txtVoterID.Text;
                sPDInfo.PANNo = txtPAN.Text;
                sPDInfo.DrivingLicNo = txtDrivingLicNo.Text;
                sPDInfo.ScholershipName = txtScholershipName.Text;
                sPDInfo.FamilySSSMID = txtSSSMId.Text;

                return sPDInfo;

            }
            catch (Exception ex)
            {

                App.Current.MainPage.DisplayAlert("Error...", "Something went wrong.. Check your entries..\n" + ex.Message, "OK");
                return null;
            }

        }

        private bool ValidateEntry()
        {
            bool result = int.TryParse(txtApplicationID.Text, out int resultvalue);
            if (txtApplicationID.Text != null)
            {
                if (txtApplicationID.Text.Trim() == "" || string.IsNullOrEmpty(txtApplicationID.Text) || !result)
                {
                    txtApplicationID.Focus();
                    ShowToast("Invalid ApplicationId");
                    return false;
                }
            }
            else
            {
                txtApplicationID.Focus();
                ShowToast("Invalid ApplicationId");

                return false;

            }
            // result = int.TryParse(txtApplicationID.Text, out int resultvalue);
            if (txtStudentName.Text != null)
            {
                if (txtStudentName.Text.Trim() == "" || string.IsNullOrEmpty(txtStudentName.Text))
                {
                    ShowToast("Invalid Student Name..");
                    txtStudentName.Focus();
                    return false;
                }
            }
            else
            {
                ShowToast("Invalid Student Name..");
                txtStudentName.Focus();
                return false;
            }

            if (lblDOB.Text != null)
            {
                result = DateTime.TryParse(lblDOB.Text.ToString(), out DateTime dt);
                if (lblDOB.Text.ToString() == "" || string.IsNullOrEmpty(lblDOB.Text.ToString()) || !result)
                {
                    ShowToast("Invalid Date of Birth");
                    PickerDOB.Focus();
                    return false;
                }
            }
            else
            {
                ShowToast("Invalid Date of Birth");
                PickerDOB.Focus();
                return false;
            }
            if (txtPhoneMobile.Text != null)
            {
                if (txtPhoneMobile.Text.Trim() == "" || string.IsNullOrEmpty(txtPhoneMobile.Text))
                {
                    txtPhoneMobile.Focus();
                    Task.Delay(200);
                    ShowToast("Invalid Mobile/Phone number..");
                    return false;
                }
            }
            else
            {
                ShowToast("Invalid Mobile/Phone number..");
                txtPhoneMobile.Focus();
                return false;
            }

            if (txtEMail.Text != null)
            {
                if (txtEMail.Text.Trim() == "" || string.IsNullOrEmpty(txtEMail.Text) || !txtEMail.Text.Contains("@"))
                {
                    ShowToast("Invalid eMail Address..");
                    txtEMail.Focus();
                    return false;
                }
            }
            else
            {
                ShowToast("Invalid eMail Address..");
                txtEMail.Focus();
                return false;
            }


            if (txtAadharNo.Text != null)
            {
                result = double.TryParse(txtAadharNo.Text, out double resultvalue1);
                if (txtAadharNo.Text.Trim() == "" || string.IsNullOrEmpty(txtAadharNo.Text) || !result)
                {
                    ShowToast("Invalid Aadhar No");
                    txtAadharNo.Focus();
                    return false;
                }
            }
            else
            {
                ShowToast("Invalid Aadhar No");
                txtAadharNo.Focus();
                return false;
            }

            if (!(bool)radioButtonEnglish.IsChecked && !(bool)radioButtonHindi.IsChecked && !(bool)radioButtonOther.IsChecked)
            {
                ShowToast("Must specify the language Medium..");
                radioGroupMedium.Focus();
                radioButtonEnglish.Focus();
                return false;

            }

            if (!(bool)radioButtonMale.IsChecked && !(bool)radioButtonFemale.IsChecked && !(bool)radioButtonTrans.IsChecked)
            {
                ShowToast("Must specify the Gender..");
                radioGroupGender.Focus();
                return false;

            }

            if (!(bool)radioButtonSC.IsChecked && !(bool)radioButtonST.IsChecked && !(bool)radioButtonOBC.IsChecked && !(bool)radioButtonGen.IsChecked)
            {
                ShowToast("Must specify the Cast Cetegory..");
                radioGroupCategory.Focus();
                return false;

            }

            if (!(bool)radioButtonHCYes.IsChecked && !(bool)radioButtonHCNo.IsChecked)
            {
                ShowToast("Must specify the Handicap status..");
                radioGroupIsHandicapped.Focus();
                return false;

            }

            if (!(bool)radioButtonUrban.IsChecked && !(bool)radioButtonRural.IsChecked)
            {
                ShowToast("Must specify the Living Area status..");
                radioGroupLivingArea.Focus();
                return false;

            }

            return true;
        }

        private void ShowToast(string msg)
        {
            var toastConfig = new ToastConfig(msg);
            toastConfig.SetDuration(3000);

            toastConfig.SetBackgroundColor(Color.FromHex("#00d9ff"));// System.Drawing.Color.FromArgb(255, 10,10));
            toastConfig.SetPosition(ToastPosition.Top);

            UserDialogs.Instance.Toast(toastConfig);

        }

        //private void TxtDOB_ValueChanged(object sender, Syncfusion.XForms.MaskedEdit.ValueChangedEventArgs eventArgs)
        //{
        //    SfMaskedEdit maskedEdit = sender as SfMaskedEdit;
        //    if (maskedEdit.HasError)
        //    {
        //        DisplayAlert("Alert", "Please enter valid date", "OK");
        //    }
        //}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PickerDOB.Focus();
        }

        private void PickerDOB_DateSelected(object sender, DateChangedEventArgs e)
        {
            lblDOB.Text = PickerDOB.Date.ToString("dd-MMM-yyyy");
        }
    }

    public class DatesInfo
    {
        public ObservableCollection<object> Dates { get; set; }

        //Day is the collection of day numbers
        private ObservableCollection<string> Day { get; set; }

        //Month is the collection of Month Names
        private ObservableCollection<string> Month { get; set; }

        //Year is the collection of Years from 1990 to 2050
        private ObservableCollection<string> Year { get; set; }

        public ObservableCollection<string> Headers { get; set; }

        private object _selecteddate;

        //update the selected dates
        public object SelectedDate
        {
            get { return _selecteddate; }
            set { _selecteddate = value; }
        }

        public DatesInfo()
        {
            Dates = new ObservableCollection<object>();
            Headers = new ObservableCollection<string>();

            //Populate Day, Month and Year values of each collection
            PopulateDates();

            //first column of SfPicker
            Dates.Add(Day);
            //Second column of SfPicker
            Dates.Add(Month);
            //Third column of SfPicker
            Dates.Add(Year);

            //first column header of SfPicker
            Headers.Add("Day");
            //second column header of SfPicker
            Headers.Add("Month");
            //third column header of SfPicker
            Headers.Add("Year");

            //  SelectedDate = new ObservableCollection<object>() {DateTime.Now.Day.ToString(), System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month),DateTime.Now.Year.ToString() };
        }

        private void PopulateDates()
        {
            Day = new ObservableCollection<string>();
            Month = new ObservableCollection<string>();
            Year = new ObservableCollection<string>();

            for (int i = 1; i <= 31; i++)
                Day.Add(i.ToString());

            for (int i = 1; i <= 12; i++)
                Month.Add(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));

            for (int i = 1990; i <= 2050; i++)
                Year.Add(i.ToString());
        }
    }

}