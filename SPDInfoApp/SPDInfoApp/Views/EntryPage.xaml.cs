using Acr.UserDialogs;
using SPDInfoApp.HelperClasses;
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
        private string _applicationID;
        public EntryPage(string loginmode = "")
        {
            // BindingContext = new  DatesInfo();
            _vm = new SPDInfoViewModel();
            this.BindingContext = _vm;
            InitializeComponent();
            PopulateData();
            this._applicationID = (Xamarin.Forms.Application.Current.Properties["StudentApplicationID"].ToString());
            txtApplicationID.Text = _applicationID;
            txtStudentFName.Text = (Xamarin.Forms.Application.Current.Properties["StudentName"].ToString());
            txtPhoneMobile.Text = (Xamarin.Forms.Application.Current.Properties["StudentMobile"].ToString());
            txtEMail.Text = (Xamarin.Forms.Application.Current.Properties["StudentEmail"].ToString());
            if (_applicationID != "") FetchData();
        }

        private void PhotoTaken(NavigationMessage obj)
        {
            String photoSource = (string)obj.Options;
            imgPhoto.Source = photoSource;
        }

        private async void BtnApply_Clicked(object sender, EventArgs e)
        {
            try
            {
                SPDInfo data = CollectData();
                if (data == null) return;
                _vm._entryData = data;
                _vm.SubmitCommand.Execute(null);

            }
            catch (Exception ex)
            {

               await App.Current.MainPage.DisplayAlert("Error", "Error while parsing data. Please check your entries.", "OK");
            }

        }

        private SPDInfo CollectData()
        {

            if (!ValidateEntry()) return null;

            try
            {
                SPDInfo sPDInfo = new SPDInfo();
                sPDInfo.AppearingClass =cmbClass.SelectedItem.ToString();
                sPDInfo.IsPG = cmbClass.SelectedItem.ToString().Substring(0, 1) == "B";//.ToString("yyyy/MM/dd")
                if ((bool)rb1.IsChecked) sPDInfo.YearSemester = 1;
                if ((bool)rb2.IsChecked) sPDInfo.YearSemester = 2;
                if ((bool)rb3.IsChecked) sPDInfo.YearSemester = 3;
                if ((bool)rb4.IsChecked) sPDInfo.YearSemester = 4;
                if ((bool)rb5.IsChecked) sPDInfo.YearSemester = 5;
                if ((bool)rb6.IsChecked) sPDInfo.YearSemester = 6;
                if (!string.IsNullOrWhiteSpace(btnAdmDt1.Text)) { sPDInfo.AddmissionDate1 = DateTime.Parse(btnAdmDt1.Text).Date; }
                if (!string.IsNullOrWhiteSpace(btnAdmDt2.Text)) { sPDInfo.AddmissionDate2 = DateTime.Parse(btnAdmDt2.Text).Date; }
                if (!string.IsNullOrWhiteSpace(btnAdmDt3.Text)) { sPDInfo.AddmissionDate3 = DateTime.Parse(btnAdmDt3.Text).Date; }
                sPDInfo.ApplicationID = int.Parse(txtApplicationID.Text);
                sPDInfo.StudentFName = txtStudentFName.Text;
                sPDInfo.StudentMName = txtStudentMName.Text;
                sPDInfo.StudentLName = txtStudentLName.Text;
                sPDInfo.RollNo = txtRollNo.Text;
                sPDInfo.EnrolmentNo = txtEnrolmentNo.Text;
                sPDInfo.DOB = DateTime.Parse(btnDOB.Text);
                sPDInfo.Medium = ((bool)radioButtonHindi.IsChecked ? "Hindi" : (bool)radioButtonEnglish.IsChecked ? "English" : "Other");
                sPDInfo.Gender = ((bool)radioButtonTrans.IsChecked ? "Trans" : (bool)radioButtonFemale.IsChecked ? "Female" : "Male");
                sPDInfo.Category = ((bool)radioButtonSC.IsChecked ? "SC" : (bool)radioButtonST.IsChecked ? "ST" : (bool)radioButtonOBC.IsChecked ? "OBC" : "General");
                sPDInfo.RegCastCertificate = txtRegCastCertificate.Text;
                sPDInfo.IsMinority = (bool)radioButtonMinorYes.IsChecked;
                sPDInfo.Minority = cmbMinority.Text;
                sPDInfo.IsHandicapped = (bool)radioButtonHCYes.IsChecked;
                sPDInfo.HandicapType = cmbHCType.Text;
                sPDInfo.HandicappPercent = txtHCPercent.Text ?? "0";
                sPDInfo.HandicappDetail = txtHCOtherTypeDetail.Text;
                sPDInfo.HandicappUDID = txtUDIDNo.Text;
                sPDInfo.BloodGroup = cmbBloodGroup.Text;// + ((bool)radioButtonPositive.IsChecked ? "+" : (bool)radioButtonNagetive.IsChecked ?"-":"");
                sPDInfo.PhoneMobile = txtPhoneMobile.Text;
                sPDInfo.AadharNo = txtAadharNo.Text;
                sPDInfo.EMail = txtEMail.Text;
                sPDInfo.AddressPermanent = txtAddressPermanent.Text??"";
                sPDInfo.AddressCurrent = txtAddressCurrent.Text??"";
                sPDInfo.IsUrban = (bool)radioButtonUrban.IsChecked;
                sPDInfo.Domicile = cmbDomicile.Text??"";
                sPDInfo.RegDomicileCertificateNo = txtRegNativeCertificateNo.Text??"";
                sPDInfo.SSSMID = txtSSSMId.Text??"";
                sPDInfo.FHName = txtFHName.Text??"";
                sPDInfo.MotherName = txtMotherName.Text??"";
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
                sPDInfo.FamilySSSMID = txtFamilySSSMId.Text;
                sPDInfo.IsNCC = swtNCC.IsToggled;
                sPDInfo.CertNCC = ((bool)chkNCCCA.IsChecked ? "A" : "") + ((bool)chkNCCCB.IsChecked ? "B" : "") + ((bool)chkNCCCC.IsChecked ? "C" : "");
                sPDInfo.CampNCC = chkCamp_CATC.IsChecked.ToString() + "," + chkCamp_NIC.IsChecked.ToString() + "," + chkCamp_TrackingCamp.IsChecked.ToString() + "," + chkCamp_AACamp.IsChecked.ToString() + "," + chkCamp_Other.IsChecked.ToString();
                sPDInfo.NCCCampOtherDetail = txtNCCCampOtherDetail.Text;
                sPDInfo.IsNSS = swtNSS.IsToggled;
                sPDInfo.CertNSS = ((bool)chkNSSA.IsChecked ? "A" : "") + ((bool)chkNSSB.IsChecked ? "B" : "") + ((bool)chkNSSC.IsChecked ? "C" : "");
                sPDInfo.IsScoutGuide = (bool)radioButtonScoutGuideYes.IsChecked ? true : false;
                sPDInfo.IsSports = swtSports.IsToggled;
                sPDInfo.CertSports = chkSportsCDiv.IsChecked.ToString() + "," + chkSportsCInterUni.IsChecked.ToString()
                    + "," + chkSportsCState.IsChecked.ToString() + "," + chkSportsCNational.IsChecked.ToString()
                    + "," + chkSportsCInterNational.IsChecked.ToString() + "," + chkSportsCOther.IsChecked.ToString() + "," + chkSportsCNone.IsChecked.ToString();
                sPDInfo.SportsOtherDetail = txtSportsOtherDetail.Text;
                sPDInfo.PhotoPath = _vm.Photo;
                sPDInfo.PhotoName = _applicationID+"."+_vm.GetFileExtention(_vm.Photo);
                sPDInfo.EntryDate = DateTime.Now;
                return sPDInfo;

            }
            catch (Exception ex)
            {

                App.Current.MainPage.DisplayAlert("Error...", "Something went wrong.. Check your entries..\n" + ex.Message, "OK");
                return null;
            }

        }

        private void FetchData()
        {
            var cuurentLoginInfo = Application.Current.Properties["LoginInfo"];
            var loginInfos = Utils.DeserializeFromJson<List<LoginInfo>>(cuurentLoginInfo.ToString());
            string ApplicationID = Xamarin.Forms.Application.Current.Properties["StudentApplicationID"].ToString();
            SPDInfo sPDInfo = loginInfos.Where(w => w.ApplicationId == ApplicationID).FirstOrDefault().EntryData;
            if (sPDInfo == null) return;
            cmbClass.SelectedItem = sPDInfo.AppearingClass;
            cmbClass.Text = sPDInfo.AppearingClass;
            rb1.IsChecked = sPDInfo.YearSemester == 1;
            rb2.IsChecked = sPDInfo.YearSemester == 2;
            rb3.IsChecked = sPDInfo.YearSemester == 3;
            rb4.IsChecked = sPDInfo.YearSemester == 4;
            rb5.IsChecked = sPDInfo.YearSemester == 5;
            rb6.IsChecked = sPDInfo.YearSemester == 6;

            btnAdmDt1.Text = sPDInfo.AddmissionDate1.Date.Year == 0001 ? "" : sPDInfo.AddmissionDate1.Date.ToString("dd-MMM-yyyy");
            btnAdmDt2.Text = sPDInfo.AddmissionDate2.Date.Year == 0001 ? "" : sPDInfo.AddmissionDate2.Date.ToString("dd-MMM-yyyy");
            btnAdmDt3.Text = sPDInfo.AddmissionDate3.Date.Year == 0001 ? "" : sPDInfo.AddmissionDate3.Date.ToString("dd-MMM-yyyy");

            txtStudentFName.Text = sPDInfo.StudentFName;
            txtStudentMName.Text = sPDInfo.StudentMName;
            txtStudentLName.Text = sPDInfo.StudentLName;
            txtRollNo.Text = sPDInfo.RollNo;
            txtEnrolmentNo.Text= sPDInfo.EnrolmentNo ;
            btnDOB.Text = sPDInfo.DOB.Date.Year == 0001 ? "" : sPDInfo.DOB.Date.ToString("dd-MMM-yyyy");
            radioButtonHindi.IsChecked = sPDInfo.Medium == "Hindi";
            radioButtonEnglish.IsChecked = sPDInfo.Medium == "English";
            radioButtonTrans.IsChecked = sPDInfo.Gender == "Trans";
            radioButtonFemale.IsChecked = sPDInfo.Gender == "Female";
            radioButtonMale.IsChecked = sPDInfo.Gender == "Male";

            radioButtonSC.IsChecked = sPDInfo.Category == "SC";
            radioButtonST.IsChecked = sPDInfo.Category == "ST";
            radioButtonOBC.IsChecked = sPDInfo.Category == "OBC";
            radioButtonGen.IsChecked = sPDInfo.Category == "General";

            txtRegCastCertificate.Text = sPDInfo.RegCastCertificate;
            radioButtonMinorYes.IsChecked = sPDInfo.IsMinority;
            radioButtonMinorNo.IsChecked = sPDInfo.IsMinority == false;
            cmbMinority.Text = sPDInfo.Minority;
            radioButtonHCYes.IsChecked = sPDInfo.IsHandicapped;
            radioButtonHCNo.IsChecked = sPDInfo.IsHandicapped == false;
            cmbHCType.Text = sPDInfo.HandicapType;
            txtHCPercent.Text = sPDInfo.HandicappPercent;
            txtHCOtherTypeDetail.Text = sPDInfo.HandicappDetail;
            txtUDIDNo.Text= sPDInfo.HandicappUDID;
            cmbBloodGroup.Text = sPDInfo.BloodGroup;

            txtPhoneMobile.Text = sPDInfo.PhoneMobile;
            txtAadharNo.Text = sPDInfo.AadharNo;
            txtEMail.Text = sPDInfo.EMail;
            txtAddressPermanent.Text = sPDInfo.AddressPermanent;
            txtAddressCurrent.Text = sPDInfo.AddressCurrent;
            radioButtonUrban.IsChecked = sPDInfo.IsUrban;
            radioButtonRural.IsChecked = sPDInfo.IsUrban == false;
            cmbDomicile.Text = sPDInfo.Domicile;
            txtRegNativeCertificateNo.Text = sPDInfo.RegDomicileCertificateNo;
            txtSSSMId.Text = sPDInfo.SSSMID;
            txtFHName.Text = sPDInfo.FHName;
            txtMotherName.Text = sPDInfo.MotherName;
            txtPhoneMobile_Gaurdian.Text = sPDInfo.PhoneMobile_Gaurdian;
            txtIncomeFather.Text = sPDInfo.IncomeFather.ToString();
            txtOccupationFather.Text = sPDInfo.OccupationFather;
            txtBankAcNo.Text = sPDInfo.BankAcNo;
            txtBankIFSC.Text = sPDInfo.BankIFSC;
            txtBankName.Text = sPDInfo.BankName;
            txtBankBranch.Text = sPDInfo.BankBranch;
            txtVoterID.Text = sPDInfo.VoterID;
            txtPAN.Text = sPDInfo.PANNo;
            txtDrivingLicNo.Text = sPDInfo.DrivingLicNo;
            txtScholershipName.Text = sPDInfo.ScholershipName;
            txtFamilySSSMId.Text = sPDInfo.FamilySSSMID;
            swtNCC.IsToggled = sPDInfo.IsNCC;
            chkNCCCA.IsChecked = sPDInfo.CertNCC.Contains("A");
            chkNCCCB.IsChecked = sPDInfo.CertNCC.Contains("B");
            chkNCCCC.IsChecked = sPDInfo.CertNCC.Contains("C");
            var NCCCampOptions = sPDInfo.CampNCC.Split(',');
            chkCamp_CATC.IsChecked = NCCCampOptions[0] == "True";
            chkCamp_NIC.IsChecked = NCCCampOptions[1] == "True";
            chkCamp_TrackingCamp.IsChecked = NCCCampOptions[2] == "True";
            chkCamp_AACamp.IsChecked = NCCCampOptions[3] == "True";
            chkCamp_Other.IsChecked = NCCCampOptions[4] == "True";
            sPDInfo.NCCCampOtherDetail = txtNCCCampOtherDetail.Text;

            swtNSS.IsToggled = sPDInfo.IsNSS;
            //sPDInfo.CertNSS = ((bool)chkNSSA.IsChecked ? "A" : "") + ((bool)chkNSSB.IsChecked ? "B" : "") + ((bool)chkNSSC.IsChecked ? "C" : "");
            chkNSSA.IsChecked = sPDInfo.CertNSS.Contains("A");
            chkNSSB.IsChecked = sPDInfo.CertNSS.Contains("B");
            chkNSSC.IsChecked = sPDInfo.CertNSS.Contains("C");
            radioButtonScoutGuideYes.IsChecked = sPDInfo.IsScoutGuide;
            radioButtonScoutGuideNo.IsChecked = sPDInfo.IsScoutGuide == false;
            swtSports.IsToggled = sPDInfo.IsSports;
            //temp update
            // sPDInfo.CertSports = "True,True,True,False,True,False,False";
            var sportOptions = sPDInfo.CertSports.Split(',');
            chkSportsCDiv.IsChecked = sportOptions[0] == "True";
            chkSportsCInterUni.IsChecked = sportOptions[1] == "True";
            chkSportsCState.IsChecked = sportOptions[2] == "True";
            chkSportsCNational.IsChecked = sportOptions[3] == "True";
            chkSportsCInterNational.IsChecked = sportOptions[4] == "True";
            chkSportsCOther.IsChecked = sportOptions[5] == "True";
            chkSportsCNone.IsChecked = sportOptions[6] == "True";


            // sPDInfo.CertSports = chkSportsCDiv.IsChecked.ToString() + "," + chkSportsCInterUni.IsChecked.ToString()
            //     + "," + chkSportsCState.IsChecked.ToString() + "," + chkSportsCNational.IsChecked.ToString()
            //     + "," + chkSportsCInterNational.IsChecked.ToString() + "," + chkSportsCOther.IsChecked.ToString() + chkSportsCNone.IsChecked.ToString();

            txtSportsOtherDetail.Text = sPDInfo.SportsOtherDetail;


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
            if (txtStudentFName.Text != null)
            {
                if (txtStudentFName.Text.Trim() == "" || string.IsNullOrEmpty(txtStudentFName.Text))
                {
                    ShowToast("Invalid Student Name..");
                    txtStudentFName.Focus();
                    return false;
                }
            }
            else
            {
                ShowToast("Invalid Student Name..");
                txtStudentFName.Focus();
                return false;
            }

            if (btnDOB.Text != null)
            {
                result = DateTime.TryParse(btnDOB.Text.ToString(), out DateTime dt);
                if (btnDOB.Text.ToString() == "" || string.IsNullOrEmpty(btnDOB.Text.ToString()) || !result)
                {
                    ShowToast("Invalid Date of Birth");
                    btnDOB.Focus();
                    return false;
                }
            }
            else
            {
                ShowToast("Invalid Date of Birth");
                btnDOB.Focus();
                return false;
            }
            //if (txtPhoneMobile.Text != null)
            //{
            //    if (txtPhoneMobile.Text.Trim() == "" || string.IsNullOrEmpty(txtPhoneMobile.Text))
            //    {
            //        txtPhoneMobile.Focus();
            //        Task.Delay(200);
            //        ShowToast("Invalid Mobile/Phone number..");
            //        return false;
            //    }
            //}
            //else
            //{
            //    ShowToast("Invalid Mobile/Phone number..");
            //    txtPhoneMobile.Focus();
            //    return false;
            //}

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

            toastConfig.SetBackgroundColor(Color.FromHex("#00d9ff"));
            toastConfig.SetPosition(ToastPosition.Top);

            UserDialogs.Instance.Toast(toastConfig);

        }
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
            _admButton = img.Equals(ImgAdmDT1) ? btnAdmDt1 : img.Equals(ImgAdmDT2) ? btnAdmDt2 : img.Equals(ImgAdmDT3) ? btnAdmDt3 : btnDOB;
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

            int i = 0;
            foreach (var item in statesName)
            {
                _states.Add(new StatesAndUTs { id = i++, Name = item, isUT = false });
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
            AdmDt1.IsOpen = false;
        }

        private void AdmDt1_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                var month = (e.NewValue as IList)[0].ToString();
                var day = (e.NewValue as IList)[1].ToString();
                var year = (e.NewValue as IList)[2].ToString();

                var SelectedDate = day + "-" + month + "-" + year;
                //  if(AdmDt1.IsOpen)
                if (AdmDt1.IsOpen) _admButton.Text = SelectedDate;

            }
            catch (Exception Ex)
            {

                
            }

        }

        private void ChkSameAsPAddress_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (e.IsChecked == true) txtAddressCurrent.Text = txtAddressPermanent.Text;
        }

        private async void PhotoButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PhotoActionPage());
            //_vm.TackPhotoCommand.Execute(null);
        }
    }
}