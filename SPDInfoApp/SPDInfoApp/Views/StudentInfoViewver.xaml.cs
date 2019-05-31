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
	public partial class StudentInfoViewver : ContentPage
	{
		public StudentInfoViewver (StudentInfo student)
		{
			InitializeComponent ();
            //Source="http://geoinfotech.org.in/spdInfoImages/8888.jpg"
            ProfilePic.Source = "http://geoinfotech.org.in/spdInfoImages/" + student.PhotoPath;   //student.ApplicationID.ToString()+"."+"jpg";

        }
	}
}