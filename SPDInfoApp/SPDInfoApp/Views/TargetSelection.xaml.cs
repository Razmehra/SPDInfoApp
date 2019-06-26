using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SPDInfoApp.Models;
using SPDInfoApp.ViewModels;
using Syncfusion.SfDataGrid.XForms;

namespace SPDInfoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TargetSelection : ContentPage
    {
        private ObservableCollection<JsonSpdInfo> StudentList { get; set; }
        private ObservableCollection<AlumniInfo> AlumniList { get; set; }
        private ObservableCollection<MessageTarget> TargetList { get; set; }
        private AdminStdReportViewModel _vm = new AdminStdReportViewModel();


        public TargetSelection()
        {
            InitializeComponent();
            this.BindingContext = _vm;
            MyDataGrid.AutoGeneratingColumn += GridAutoGeneratingColumns;
            TargetList = new ObservableCollection<MessageTarget>();
        }

        private void GridAutoGeneratingColumns(object sender, AutoGeneratingColumnEventArgs e)
        {
            if (e.Column.MappingName == "IsSelected")
            {
                e.Column.HeaderText = "";
            }
        }

        private void PopulateLists()
        {

        }

        private void MyDataGrid_GridDoubleTapped(object sender, Syncfusion.SfDataGrid.XForms.GridDoubleTappedEventArgs e)
        {

        }

        private  void BtnSelect_Clicked(object sender, EventArgs e)
        {
            var xlist = ((ObservableCollection<StudentInfo>)MyDataGrid.ItemsSource).Where(w => w.IsSelected == true).ToList();// as List<StudentInfo>;
            TargetList.Clear();
            foreach (var student in xlist)
            {
                var stdInfo = _vm.StudentList.Where(w => w.Appid == student.AppID).FirstOrDefault();
                if (stdInfo == null) continue;
                TargetList.Add(new MessageTarget()
                {
                    AppID = Convert.ToInt32(stdInfo.Appid),
                    ApplicationID = (int)stdInfo.ApplicationId,
                    IsAlumni = false,
                    IsStudent = true,
                    Name = stdInfo.StudentFName + " " + stdInfo.StudentMName + " " + stdInfo.StudentLName,
                    OtherInfo = "Class:" + stdInfo.AppearingClass + ", Mobile:" + stdInfo.PhoneMobile + ", Email:" + stdInfo.EMail + ", DOB:" + stdInfo.Dob.ToString("dd/MM/yyyy")
                });
            }

            LVTarget.BindingContext = this;
            LVTarget.ItemsSource = null;
            LVTarget.ItemsSource = TargetList;

            MessagingCenter.Send(new NavigationMessage(TargetList), "BCMessage:UpdateTargets");

        }

        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }

    }
}