using SPDInfoApp.Models;
using SPDInfoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentInfoViewver : ContentPage
    {
        private Dictionary<string, string> StudentDic = new Dictionary<string, string>();
        private Dictionary<string, Dictionary<string, string>> StudentData = new Dictionary<string, Dictionary<string, string>>();
        private ObservableCollection<StudentData> StudentDatas = new ObservableCollection<StudentData>();

        public StudentInfoViewver(JsonSpdInfo student)
        {
            InitializeComponent();
            //Source="http://geoinfotech.org.in/spdInfoImages/8888.jpg"
            ProfilePic.Source = "http://geoinfotech.org.in/spdInfoImages/" + student.PhotoName;   //student.ApplicationID.ToString()+"."+"jpg";
            FetchData(student);
            StudentDataList.BindingContext = StudentDatas;
            StudentDataList.ItemsSource = StudentDatas;

        }

        private async void FetchData(JsonSpdInfo student)
        {

            StudentDatas.Clear();
            Type classType = student.GetType();
            foreach (PropertyInfo propertyInfo in classType.GetProperties())
            {
                if (propertyInfo.Name == "Appid" || propertyInfo.Name.Contains("Photo")) continue;
                if (propertyInfo.CanRead)
                {
                    object value = propertyInfo.GetValue(student, null);
                    string EvalValue = value.ToString();
                    var attribute = propertyInfo.GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
                    var description = (DescriptionAttribute)attribute;
                    var Caption = description.Description;

                    switch (propertyInfo.Name)
                    {
                        case "IsPG":

                            break;
                        case "AddmissionDate1":
                            if (((DateTime)value).Year == 0001)
                            {
                                EvalValue = "";
                            }
                            break;
                        case "AddmissionDate2":
                            if (((DateTime)value).Year == 0001)
                            {
                                EvalValue = "";
                            }

                            break;
                        case "AddmissionDate3":
                            if (((DateTime)value).Year == 0001)
                            {
                                EvalValue = "";
                            }

                            break;
                        case "DOB":
                            if (((DateTime)value).Year == 0001)
                            {
                                EvalValue = "";
                            }

                            break;
                        case "IsMinority":
                            break;
                        case "IsHandicapped":
                            break;
                        case "IsUrban":
                            break;
                        case "IsNCC":
                            break;
                        case "CertNCC":
                            break;
                        case "CampNCC":
                            var NCCCampOptions = EvalValue.Split(',');
                            EvalValue = NCCCampOptions[0] == "True" ? "CATC" : "";
                            EvalValue = EvalValue +( NCCCampOptions[1] == "True" ? (EvalValue.Length > 0 ? "," : "") + " NIC" : "");
                            EvalValue = EvalValue +( NCCCampOptions[2] == "True" ? (EvalValue.Length > 0 ? "," : "") + " TRACKING CAMP" : "");
                            EvalValue = EvalValue +( NCCCampOptions[3] == "True" ? (EvalValue.Length > 0 ? "," : "") + " ARMY ATTACHMENT CAMP" : "");
                            EvalValue = EvalValue +( NCCCampOptions[4] == "True" ? (EvalValue.Length > 0 ? "," : "") + " Other" : "");
                            break;
                        case "IsNSS":
                            break;
                        case "CertNSS":

                            break;
                        case "IsScoutGuide":
                            break;
                        case "IsSports":
                            break;
                        case "CertSports":
                            var sportOptions = EvalValue.Split(',');
                            EvalValue = sportOptions[0] == "True" ? "Divisional" : "";
                            EvalValue = EvalValue + (sportOptions[1] == "True" ? (EvalValue.Length > 0 ? "," : "") + " Inter University" : "");
                            EvalValue = EvalValue + (sportOptions[2] == "True" ? (EvalValue.Length > 0 ? "," : "") + " State" : "");
                            EvalValue = EvalValue + (sportOptions[3] == "True" ? (EvalValue.Length > 0 ? "," : "") + " National" : "");
                            EvalValue = EvalValue + (sportOptions[4] == "True" ? (EvalValue.Length > 0 ? "," : "") + " International" : "");
                            EvalValue = EvalValue + (sportOptions[5] == "True" ? (EvalValue.Length > 0 ? "," : "") + " Other" : "");
                            EvalValue = EvalValue + (sportOptions[6] == "True" ? (EvalValue.Length > 0 ? "," : "") + " None" : "");
                            break;
                        default:
                            break;
                    }



                    //StudentDic = new Dictionary<string, string>
                    StudentDic.Add(Caption, EvalValue);
                    StudentDatas.Add(new StudentData { EntryCaption = Caption, EntryValue = EvalValue });
                    // StudentData.Add(propertyInfo.Name, StudentDic);
                }
            }

        }
    }

    public class StudentData
    {
        public string EntryCaption { get; set; }
        public string EntryValue { get; set; }
    }
}