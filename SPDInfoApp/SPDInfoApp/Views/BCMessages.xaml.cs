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
	public partial class BCMessages : ContentPage
	{
        private Button _admButton;

        public BCMessages ()
		{
			InitializeComponent ();
		}

        private void AdmBTN_Clicked(object sender, EventArgs e)
        {
            _admButton = (Button)sender;
            CustDatePicker.IsOpen = !CustDatePicker.IsOpen;
            CustDatePicker.AttechedObject = _admButton;
        }

        private void AdmDateTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            _admButton = img.Equals(ImgAdmDT1) ? btnAdmDt1 : btnAdmDt2 ;
            CustDatePicker.IsOpen = !CustDatePicker.IsOpen;
            CustDatePicker.AttechedObject = _admButton;
        }

        private void AdmDt1_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void CustDatePicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void CustDatePicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {

        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnSubmit_Clicked(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizerEdit_Tapped(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizerDelete_Tapped(object sender, EventArgs e)
        {

        }
    }
}