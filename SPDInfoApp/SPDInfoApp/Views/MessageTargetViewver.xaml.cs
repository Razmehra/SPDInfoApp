using SPDInfoApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MessageTargetViewver : ContentPage
	{
        private ObservableCollection<MessageTarget> TargetList { get; set; }


        public MessageTargetViewver (ObservableCollection<MessageTarget> targets=null)
		{
            TargetList = targets;
			InitializeComponent ();
            LVTarget.BindingContext = this;
            LVTarget.ItemsSource = null;
            LVTarget.ItemsSource = TargetList;
        }

        private async void GoBack(object sender, EventArgs e)
        {
          await  Navigation.PopModalAsync(true);
        }
    }
}