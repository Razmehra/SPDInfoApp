using Acr.UserDialogs;
using SPDInfoApp.Models;
using SPDInfoApp.ViewModels;
using System;
using System.Collections;
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
        public List<StatesAndUTs> _states = new List<StatesAndUTs>();

        private Button _admButton;
        private SPDInfoViewModel _vm;
        public EntryPage()
        {
           // BindingContext = new  DatesInfo();
            _vm = new SPDInfoViewModel();
            InitializeComponent();
            PopulateData();
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

        private void AdmBTN_Clicked(object sender, EventArgs e)
        {
            _admButton = (Button)sender;
            AdmDt1.IsOpen = !AdmDt1.IsOpen;
            AdmDt1.AttechedObject = _admButton;
        }

        private void AdmDateTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            _admButton = img.Equals(ImgAdmDT1) ? btnAdmDt1 : img.Equals(ImgAdmDT2) ? btnAdmDt2 : img.Equals(ImgAdmDT3) ? btnAdmDt3: btnDOB;
            AdmDt1.IsOpen = !AdmDt1.IsOpen;
            AdmDt1.AttechedObject = _admButton;
        }

        private void BtnDOB_Clicked(object sender, EventArgs e)
        {

        }

        private void PopulateData()
        {
            string[] statesName = ("Andhra Pradesh,Arunachal Pradesh,Assam,Bihar,Chhattisgarh,Goa,Gujarat,Haryana,Himachal Pradesh,Jammu and Kashmir,Jharkhand,Karnataka,Kerala,Madhya Pradesh,Maharashtra,Manipur,Meghalaya,Mizoram,Nagaland,Odisha,Punjab,Rajasthan,Sikkim,Tamil Nadu,Telangana,Tripura,Uttar Pradesh,Uttarakhand,West Bengal").Split(',');
            string[] uts = ("Andaman and Nicobar Islands,Chandigarh,Dadra and Nagar Haveli,Daman and Diu,Lakshadweep,National Capital Territory of Delhi,Puducherry").Split(',');

            int i=0;
            foreach (var item in statesName)
            {
                _states.Add( new StatesAndUTs { id = i++, Name = item, isUT = false });
            }

            foreach (var item in uts)
            {
                _states.Add(new StatesAndUTs { id = i++, Name = item, isUT = true });
            }

            cmbDomicile.DataSource = _states;
            cmbDomicile.DisplayMemberPath = "Name";
        }

        private void AdmDt1_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            AdmDt1.IsOpen=false;
        }

        private void AdmDt1_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            var month = (e.NewValue as IList)[0].ToString();
            var day = (e.NewValue as IList)[1].ToString();
            var year = (e.NewValue as IList)[2].ToString();

            var SelectedDate = day + "-" + month + "-" + year;
            //  if(AdmDt1.IsOpen)
            if (AdmDt1.IsOpen) _admButton.Text = SelectedDate;

        }
    }
}