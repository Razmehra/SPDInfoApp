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
	public partial class HomePageAdmin : ContentPage
	{
		public HomePageAdmin ()
		{
			InitializeComponent ();
		}

        private void BtnGetStudentData_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdminStudentReport() { BackgroundColor=Color.GhostWhite, Title="Student Information List" });
        }

        private void BtnGetAlumniData_Clicked(object sender, EventArgs e)
        {

        }
    }
}