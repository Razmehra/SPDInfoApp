using Acr.UserDialogs;
using SPDInfoApp.Convertors;
using SPDInfoApp.Models;
using SPDInfoApp.ViewModels;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.Compression;
using Syncfusion.SfDataGrid.XForms.Exporting;
using SPDInfoApp.Interfaces;
using SPDInfoApp.WebServices;

namespace SPDInfoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminStudentReport : ContentPage
    {
      private  GroupColumnDescription GroupingOnEntryDate = new GroupColumnDescription()
        {
            ColumnName = "EntryDate",
            Converter = new GroupConverter()
        };

        private AdminStdReportViewModel _vm = new AdminStdReportViewModel();
        public AdminStudentReport()
        {
            InitializeComponent();
            this.BindingContext = _vm;

            MyDataGrid.AutoGeneratingColumn += GridAutoGeneratingColumns;
            MyDataGrid.AllowPullToRefresh = true;
            MyDataGrid.PullToRefreshCommand = new Command(ExecutePullToRefreshCommand);


        }

        private async void ExecutePullToRefreshCommand()
        {
            MyDataGrid.IsBusy = true;
            await Task.Delay(new TimeSpan(0, 0, 5));
            _vm.FetchData();
            MyDataGrid.IsBusy = false;
            await Task.Delay(1200);
            if (MyDataGrid.GroupColumnDescriptions.Count == 0) MyDataGrid.GroupColumnDescriptions.Add(GroupingOnEntryDate);
            BusyIndicator.IsRunning = false;
            BusyIndicator.IsVisible = false;
            ImgRefresh.IsEnabled = true;
        }
        private void GridAutoGeneratingColumns(object sender, AutoGeneratingColumnEventArgs e)
        {
            if (e.Column.MappingName == "PhotoPath")
            {
                e.Column.IsHidden = true;
            }
        }

        private void DataGrid_GridLoaded(object sender, GridLoadedEventArgs e)
        {
            if (MyDataGrid.Columns.Count > 0)
                MyDataGrid.Columns["PhotoPath"].IsHidden = true;
        }

        private async void SfDataGrid_GridDoubleTapped(object sender, GridDoubleTappedEventArgs e)
        {
            // var xx = "Double tapped";
            using (UserDialogs.Instance.Loading("Fetching data..\nPlease Wait.", null, null, true, MaskType.Black))
            {
                var xx = ((StudentInfo)e.RowData).ApplicationID;
                JsonSpdInfo student = _vm.StudentList.Where(w => w.ApplicationId == ((StudentInfo)e.RowData).ApplicationID).FirstOrDefault();
                await Navigation.PushModalAsync(new StudentInfoViewver(student));
            }
            //_vm.OnDoubleTapped((StudentInfo)e.RowData);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                ImgRefresh.IsEnabled = false;
                BusyIndicator.IsVisible = true;
                BusyIndicator.IsRunning = true;
                BusyIndicator.IsEnabled = true;
                ExecutePullToRefreshCommand();


            }
            catch (Exception)
            {


            }
        }

        private async void BtnExportToXLS_Clicked(object sender, EventArgs e)
        {
            String result;
            PHPServices PHPService = new PHPServices();

            var email = await UserDialogs.Instance.PromptAsync("Please enter valid email id.", "Export to Excel over email", "Ok", "Cancel", "xxxx@xx.com", InputType.Email);
            //if(email.)
            //"rajahsin@gmail.com"
            using (UserDialogs.Instance.Loading("Prepare excel .\nPlease Wait.", null, null, true, MaskType.Black))
            {
                 result = await PHPService.SendExcel2Mail(new string[] { email.Value });
            }

            if (result == "Success")
            {
                await App.Current.MainPage.DisplayAlert(result, "List successfuly sent to " + email.Value + ".\nYou can recieve email shortly. If mail does not shown on inbox then please check in your spam folder.", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(result,"Failed to export data.." , "OK");
            }

            return;

            string fileName = "MyFile.txt";
            string data = "IndrazSolutions Test Text";
            //Write data to Loal File using DependencyService  
            DependencyService.Get<IFileReadWrite>().WriteData(fileName, data);
            //System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
           // File.Create
          
            var filePath = Path.Combine(documentsPath, fileName);
           // File.Create(filePath);
            //File.AppendText()
            File.WriteAllText(filePath, data);


           // return;

            DataGridExcelExportingController excelExport = new DataGridExcelExportingController();
            var excelEngine = excelExport.ExportToExcel(MyDataGrid);
            var workbook = excelEngine.Excel.Workbooks[0];
            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            workbook.Close();
            excelEngine.Dispose();

            Xamarin.Forms.DependencyService.Get<ISave>().Save("DataGrid.xlsx", "application/msexcel", stream);
        }
    }
}