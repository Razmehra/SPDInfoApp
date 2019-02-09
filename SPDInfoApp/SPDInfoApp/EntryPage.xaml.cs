using Acr.UserDialogs;
using SPDInfoApp.Models;
using SPDInfoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryPage : ContentPage
    {
        private SPDInfoViewModel _vm;
        public EntryPage()
        {
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
                //{
                sPDInfo.AppearingClass = txtAppearingClass.Text;
                sPDInfo.ApplicationID = int.Parse(txtApplicationID.Text);
                sPDInfo.StudentName = txtStudentName.Text.Trim();
                sPDInfo.RollNo = txtRollNo.Text == null ? 0 : (double.Parse(txtRollNo.Text));
                sPDInfo.EnrolmentNo = txtEnrolmentNo.Text == null ? 0 : double.Parse(txtEnrolmentNo.Text);
                sPDInfo.DOB = DateTime.Parse(txtDOB.Value.ToString());
                sPDInfo.Medium = ((bool)radioButtonHindi.IsChecked ? "Hindi" : (bool)radioButtonEnglish.IsChecked ? "English" : "Other");
                sPDInfo.Gender = ((bool)radioButtonTrans.IsChecked ? "Trans" : (bool)radioButtonFemale.IsChecked ? "Female" : "Male");
                sPDInfo.Category = ((bool)radioButtonSC.IsChecked ? "SC" : (bool)radioButtonST.IsChecked ? "ST" : (bool)radioButtonOBC.IsChecked ? "OBC" : "General");
                sPDInfo.RegCastCertificate = txtRegCastCertificate.Text;
                sPDInfo.IsHandicapped = (bool)radioButtonHCYes.IsChecked ? true : false;
                sPDInfo.HandicappDetail = "";
                sPDInfo.BloodGroup = cmbBloodGroup.Text + ((bool)radioButtonPositive.IsChecked ? "+" : (bool)radioButtonNagetive.IsChecked ?"-":"");
                sPDInfo.PhoneMobile = txtPhoneMobile.Text;
                sPDInfo.SSSMId = txtSSSMId.Text;
                sPDInfo.AadharNo = txtAadharNo.Text;
                sPDInfo.EMail = txtEMail.Text;
                sPDInfo.AddressPermanent = txtAddressPermanent.Text;
                sPDInfo.AddressCurrent = txtAddressCurrent.Text;
                sPDInfo.IsUrban = (bool)radioButtonUrban.IsChecked ? true : false;
                sPDInfo.NativePlace = txtNativePlace.Text;
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
                //};

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
                    using (UserDialogs.Instance.Toast("Invalid ApplicationId")) { };
                    txtApplicationID.Focus();
                    return false;
                }
            }
            else
            {
                using (UserDialogs.Instance.Toast("Invalid ApplicationId")) { };
                txtApplicationID.Focus();
                return false;

            }
            // result = int.TryParse(txtApplicationID.Text, out int resultvalue);
            if (txtStudentName.Text != null)
            {
                if (txtStudentName.Text.Trim() == "" || string.IsNullOrEmpty(txtStudentName.Text))
                {
                    using (UserDialogs.Instance.Toast("Invalid Student Name..")) { };
                    txtStudentName.Focus();
                    return false;
                }
            }
            else
            {
                using (UserDialogs.Instance.Toast("Invalid Student Name..")) { };
                txtStudentName.Focus();
                return false;
            }

            if (txtDOB.Value != null)
            {
                result = DateTime.TryParse(txtDOB.Value.ToString(), out DateTime dt);
                if (txtDOB.Value.ToString() == "" || string.IsNullOrEmpty(txtDOB.Value.ToString()) || !result)
                {
                    using (UserDialogs.Instance.Toast("Invalid Date of Birth")) { };
                    txtDOB.Focus();
                    return false;
                }
            }
            else
            {
                using (UserDialogs.Instance.Toast("Invalid Date of Birth")) { };
                txtDOB.Focus();
                return false;
            }
            if (txtPhoneMobile.Text != null)
            {
                if (txtPhoneMobile.Text.Trim() == "" || string.IsNullOrEmpty(txtPhoneMobile.Text))
                {
                    using (UserDialogs.Instance.Toast("Invalid Mobile/Phone number..")) { };
                    txtPhoneMobile.Focus();
                    return false;
                }
            }
            else
            {
                using (UserDialogs.Instance.Toast("Invalid Mobile/Phone number..")) { };
                txtPhoneMobile.Focus();
                return false;
            }

            if (txtEMail.Text != null)
            {
                if (txtEMail.Text.Trim() == "" || string.IsNullOrEmpty(txtEMail.Text) || !txtEMail.Text.Contains("@"))
                {
                    using (UserDialogs.Instance.Toast("Invalid eMail Address..")) { };
                    txtEMail.Focus();
                    return false;
                }
            }

            if (txtAadharNo.Text != null)
            {
                result = double.TryParse(txtAadharNo.Text, out double resultvalue1);
                if (txtAadharNo.Text.Trim() == "" || string.IsNullOrEmpty(txtAadharNo.Text) || !result)
                {
                    using (UserDialogs.Instance.Toast("Invalid Aadhar No")) { };
                    txtAadharNo.Focus();
                    return false;
                }
            }
            else
            {
                using (UserDialogs.Instance.Toast("Invalid Aadhar No")) { };
                txtAadharNo.Focus();
                return false;
            }

            return true;
        }


    }
}