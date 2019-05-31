using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SPDInfoApp.HelperClasses;
using SPDInfoApp.Models;
using SPDInfoApp.WebServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentFeedback : ContentPage
    {
        private ObservableCollection<StudentFeedbackModel> FDList { get; set; }

        public StudentFeedback(string loginmode = "")
        {
            InitializeComponent();
            FDList = PopulateData();
            this.BindingContext = FDList;
            lstFeedback.BindingContext = this;
            lstFeedback.ItemsSource = FDList;
            PopulatePreviousFB();

        }

        private void PopulatePreviousFB()
        {
            var cuurentLoginInfo = Application.Current.Properties["LoginInfo"];
            if (cuurentLoginInfo == null) return;
            var loginInfos = Utils.DeserializeFromJson<List<LoginInfo>>(cuurentLoginInfo.ToString());
            string ApplicationID = Xamarin.Forms.Application.Current.Properties["StudentApplicationID"].ToString();
            var list = loginInfos.Where(w => w.ApplicationId == ApplicationID).FirstOrDefault();
            if (list.StudentFeedback == null) return;
            foreach (var fb in list.StudentFeedback.ToList())
            {
                FDList.Where(w => w.ID == fb.ID).Select(w => w.FeedbackValue = fb.FeedbackValue).ToList();
            }
        }

        private ObservableCollection<StudentFeedbackModel> PopulateData()
        {
            ObservableCollection<StudentFeedbackModel> feedbackDatas = new ObservableCollection<StudentFeedbackModel>()
            {
               new StudentFeedbackModel{ ID=1, FeedbackQues="Teaching & Learning:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=2, FeedbackQues="Interaction with faculty & teacher:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=3, FeedbackQues="Interaction with administration and office staff:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=4, FeedbackQues="Examination and evaluation:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=5, FeedbackQues="Infrastructre & facilities:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=6, FeedbackQues="Library facilities:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=7, FeedbackQues="Sports facilities:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=8, FeedbackQues="Personality development and Placement facilities:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=9, FeedbackQues="Internet & Computer facilities:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=10, FeedbackQues="Extra-curricular activities:", FeedbackValue=0, FeedbackValueText=""},
               new StudentFeedbackModel{ ID=11, FeedbackQues="Overall rating:", FeedbackValue=0, FeedbackValueText=""},
            };
            return feedbackDatas;
        }

        private async void BtnApply_Clicked(object sender, EventArgs e)
        {
            //var xx = this.BindingContext as List<StudentFeedbackModel>;
            // var xx = (List<StudentFeedbackModel>)this.BindingContext;
            IEnumerable<StudentFeedbackModel> obsCollection = (IEnumerable<StudentFeedbackModel>)this.BindingContext;
            var list = new List<StudentFeedbackModel>(obsCollection);
            // Application.Current.Properties["LoginInfo"] = null;

            var cuurentLoginInfo = Application.Current.Properties["LoginInfo"];
            var loginInfos = Utils.DeserializeFromJson<List<LoginInfo>>(cuurentLoginInfo.ToString());
            string ApplicationID = Xamarin.Forms.Application.Current.Properties["StudentApplicationID"].ToString();
            list.Select(w => w.ApplicationID = ApplicationID).ToList();
            //
            DateTime dt = DateTime.Parse( DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            list.Where(w => w.ApplicationID == ApplicationID).Select(w => w.FBDateTime=dt).ToList();

            string serialData = Utils.SerializeToJson(list).ToString();
            loginInfos.Where(w => w.ApplicationId == ApplicationID).Select(w => w.StudentFeedback = list).ToList();
            loginInfos.Where(w => w.ApplicationId == ApplicationID).Select(w => w.SFB = serialData).ToList();



            var jsonstring = Utils.SerializeToJson(loginInfos);

            var yy= Utils.DeserializeFromJson<List<StudentFeedbackModel>>(jsonstring);
            Application.Current.Properties["LoginInfo"] = jsonstring;

            await Xamarin.Forms.Application.Current.SavePropertiesAsync();

            cuurentLoginInfo = Application.Current.Properties["LoginInfo"];
            //fetchFeedback
            string[] FBData = { ApplicationID };

            PHPServices MyService = new PHPServices();
            string result = "";
            using (UserDialogs.Instance.Loading("Fetching student feedback .\nPlease Wait.", null, null, true, MaskType.Black))
            {
                result = await MyService.FetchStudentFeedback(FBData);
            }


            List<StudentFeedbackModel> studentFeedbacks= new List<StudentFeedbackModel>();
            if (result == "failure")
            {
                // DisplayInvalidLoginPromt();
                // return;
            }
            else
            {
                var PreviousFeedbacks = Utils.DeserializeFromJson<List<JsonStudentFeedback>>(result);
                studentFeedbacks = Utils.DeserializeFromJson<List<StudentFeedbackModel>>( PreviousFeedbacks[0].FBJson.ToString());
                //studentFeedbacks.Add(fb);
            }



            //UpdateFeedback
          var conList=  studentFeedbacks.Concat(list);
            studentFeedbacks = conList.ToList();

            JsonStudentFeedback Jfb = new JsonStudentFeedback();
            Jfb.ApplicationID = ApplicationID;
            jsonstring = Utils.SerializeToJson(studentFeedbacks);//loginInfos.LastOrDefault().SFB.ToString();  ;// serialData.ToString();// 
                                                     //var rr= Regex.Unescape(jsonstring);
                                                     // rr = JsonConvert.DeserializeObject<string>(rr.ToString());

            Jfb.FBJson = SanitizeReceivedJson(jsonstring);// JObject.Parse(jsonstring).ToString();

            Jfb.FBDateTime = DateTime.Now;
            

             result = "";
            using (UserDialogs.Instance.Loading("Update feedback .\nPlease Wait.", null, null, true, MaskType.Black))
            {
                result = await MyService.SubmitStudentFeedback(Jfb);
            }
            //User data = User.FromJson(result);

            if (result == "failure")
            {
                // DisplayInvalidLoginPromt();
                // return;
            }
            else
            {

            }
        }

        private string SanitizeReceivedJson(string uglyJson)
        {
            var sb = new StringBuilder(uglyJson);
            sb.Replace("\\\t", "\t");
            sb.Replace("\\\n", "\n");
            sb.Replace("\\\r", "\r");
            sb.Replace(@"\", " ");
            var yourString = uglyJson.Replace("\\", "");
            return yourString;// sb.ToString();
        }

    }
}