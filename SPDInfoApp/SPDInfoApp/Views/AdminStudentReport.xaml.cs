using SPDInfoApp.ViewModels;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminStudentReport : ContentPage
    {
        private AdminStdReportViewModel _vm = new AdminStdReportViewModel();
        public AdminStudentReport()
        {
            this.BindingContext = _vm;
            InitializeComponent();
            MyDataGrid.GridLoaded += DataGrid_GridLoaded;

        }

        private void DataGrid_GridLoaded(object sender, GridLoadedEventArgs e)
        {
           // MyDataGrid.Columns["CustomerID"].IsHidden = true;
        }

        private async void SfDataGrid_GridDoubleTapped(object sender, GridDoubleTappedEventArgs e)
        {
            // var xx = "Double tapped";
            await Navigation.PushModalAsync(new StudentInfoViewver((StudentInfo)e.RowData));
        }
    }
}