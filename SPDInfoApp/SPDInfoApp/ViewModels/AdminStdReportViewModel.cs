using SPDInfoApp.HelperClasses;
using SPDInfoApp.Models;
using SPDInfoApp.WebServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
        private PHPServices _phpService = new PHPServices();

        public AdminStdReportViewModel()
        {
            FetchData();
        }

        private async void FetchData()
        {
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
                        PhotoPath=student.PhotoName
                    };
                StudentInfos.Add(info);

            }

            

        }

        public async void OnDoubleTapped(StudentInfo student)
        {
           
        }

    }
    public class StudentInfo
    {
        public long ApplicationID { get; set; }
        public string StudentName { get; set; }
        public string AppearingClass { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime EntryDate { get; set; }
        public string PhotoPath { get; set; }
    }

}
