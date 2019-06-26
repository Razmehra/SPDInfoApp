using Acr.UserDialogs;
using SPDInfoApp.HelperClasses;
using SPDInfoApp.Models;
using SPDInfoApp.Views;
using SPDInfoApp.WebServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;

namespace SPDInfoApp.ViewModels
{
    class AdminStdReportViewModel : BaseViewModel
    {
        private ObservableCollection<JsonSpdInfo> _studentList;
        private ObservableCollection<StudentInfo> _studentInfo;//= new ObservableCollection<StudentInfo>();
        public ObservableCollection<StudentInfo> StudentInfos
        {
            get
            { return _studentInfo; }
            set
            {
                _studentInfo = value;
                OnPropertyChanged("StudentInfos");
            }
        }
        public ObservableCollection<JsonSpdInfo> StudentList
        {
            get { return _studentList; }
            set
            {
                _studentList = value;
                OnPropertyChanged("StudentList");
            }
        }

        public JsonSpdInfo Student { get; set; }

        private PHPServices _phpService = new PHPServices();

        public AdminStdReportViewModel()
        {



            try
            {
                using (UserDialogs.Instance.Loading("Fetching data..\nPlease Wait.", null, null, true, MaskType.Black))
                {
                    try
                    {
                        FetchData();
                    }
                    catch (Exception ex)
                    {

                       // throw;
                    }
                    
                }
            }
            catch (Exception ex)
            {

                App.Current.MainPage.DisplayAlert("Opps", "Something went wrong.. Please tap on refresh button.\nError description:" + ex.Message +"\n"+ex.StackTrace, "OK");
            }
            
        }


        public async void FetchData()
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("No Internet", "Network Error:No Internet connection available.\n !Turn ON your data connection or connect using wifi then try again.", "Ok");
                return;
            }

            var data = await _phpService.FetchStudentInfo(new string[] { "0" });
            var mydata = JsonSpdInfo.FromJson(data);

            StudentList = mydata;
            //var mylist = mydata.ToArray();

            StudentInfos = new ObservableCollection<StudentInfo>();
            foreach (var student in StudentList)
            {
                string dtval = student.EntryDate.ToString("dd-MMM-yyyy");
                StudentInfo info =
                    new StudentInfo
                    {
                        ApplicationID = student.ApplicationId,
                        StudentName = student.StudentFName + " " + student.StudentMName + " " + student.StudentLName,
                        AppearingClass = student.AppearingClass,
                        Mobile = student.PhoneMobile,
                        Email = student.EMail,
                        EntryDate = DateTime.Parse(dtval),
                        PhotoPath = student.PhotoName,
                        IsSelected = false,
                        AppID =(int) student.Appid
                       
                    };
                StudentInfos.Add(info);
                
            }
            //var property = typeof(StudentInfo).GetProperty("EntryDate");
            //var attribute = property.GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
            //var description = (DescriptionAttribute)attribute;
            //var text = description.Description;
           // var value= property.GetValue()
        }

        public async void OnDoubleTapped(StudentInfo student)
        {
            //await Navigation.PushModalAsync(new StudentInfoViewver((StudentInfo)e.RowData));
           // await Navigation.PushModalAsync(new StudentInfoViewver(student));
        }

        

    }
    public class StudentInfo
    {
        public bool IsSelected { get; set; }
        public long ApplicationID { get; set; }
        public string StudentName { get; set; }
        public string AppearingClass { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        [Description("Date of entry submission.")]
        public DateTime EntryDate { get; set; }
        public string PhotoPath { get; set; }
        public int AppID { get; set; }


    }

}
