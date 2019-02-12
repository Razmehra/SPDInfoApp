using SPDInfoApp.ViewModels;
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
	public partial class LoginMenu : ContentPage
	{
        LoginMenuViewModel viewModel = new LoginMenuViewModel();
		public LoginMenu ()
		{
            this.BindingContext = viewModel;
            InitializeComponent ();
            
        }

        private void BtnSkipLogin_Clicked(object sender, EventArgs e)
        {
           
        }

        private void RbAdminLogin_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {

        }
    }
}