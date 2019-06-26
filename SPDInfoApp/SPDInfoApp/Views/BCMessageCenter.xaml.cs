using SPDInfoApp.HelperClasses;
using SPDInfoApp.Models;
using SPDInfoApp.ViewModels;
using SPDInfoApp.WebServices;
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
    public partial class BCMessageCenter : ContentPage
    {
        private PHPServices _phpService = new PHPServices();
        private ObservableCollection<MessageModel> _messagesList { get; set; }
        public ObservableCollection<MessageModel> MessagesList
        {
            get { return _messagesList; }
            set { _messagesList = value; OnPropertyChanged("MessagesList"); }
        }

        public BCMessageCenter()
        {
            InitializeComponent();
            FetchMassages();
            MessagingCenter.Unsubscribe<NavigationMessage>(this, "BCMessageCenter:UpdateTargets");
            MessagingCenter.Subscribe<NavigationMessage>(this, "BCMessageCenter:UpdateTargets", UpdateTarget);

        }

        private void UpdateTarget(NavigationMessage obj)
        {
            var msg = (MessageModel)obj.Options;

            if ((bool)obj.Options2 == true)//is new message: need to add to the list
            {
                //var maxVal = MessagesList.Max(w => w.MsgID);
                ////MessagesList.Where(w => w.MsgID == msg.MsgID).Select(w => w.MsgFromDate = msg.MsgFromDate).ToList();
                //msg.MsgID = maxVal + 1;
                MessagesList.Add(msg);
                LVMessages.BindingContext = this;
                LVMessages.ItemsSource = null;
                LVMessages.ItemsSource = MessagesList.OrderByDescending(w => w.MsgID).ToList();

            }
            else
            {
                MessagesList.Where(w => w.MsgID == msg.MsgID).Select(w => w.MsgFromDate = msg.MsgFromDate).ToList();
                MessagesList.Where(w => w.MsgID == msg.MsgID).Select(w => w.MsgToDate = msg.MsgToDate).ToList();
                MessagesList.Where(w => w.MsgID == msg.MsgID).Select(w => w.MsgMessage = msg.MsgMessage).ToList();
                MessagesList.Where(w => w.MsgID == msg.MsgID).Select(w => w.MsgMode = msg.MsgMode).ToList();
                MessagesList.Where(w => w.MsgID == msg.MsgID).Select(w => w.IsScroll = msg.IsScroll).ToList();
                MessagesList.Where(w => w.MsgID == msg.MsgID).Select(w => w.MsgAudience = msg.MsgAudience).ToList();
                MessagesList.Where(w => w.MsgID == msg.MsgID).Select(w => w.Remark = msg.Remark).ToList();

            }

        }

        private async void FetchMassages()
        {
            var data = await _phpService.FetchMessages(new string[] { "0" });
            var mydata = Utils.DeserializeFromJson<ObservableCollection<MessageModel>>(data);//List<MessageModel>

            MessagesList = mydata;
           // MessagesList.OrderByDescending(w => w.MsgID);
            LVMessages.BindingContext = this;
            LVMessages.ItemsSource = null;
            LVMessages.ItemsSource = MessagesList.OrderByDescending(w => w.MsgID);


        }

        private async void BtnNew_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BCMessages(null) { Title = "Broadcast Message: New Message", Icon = "SPDInfo.png", BackgroundColor = Color.FloralWhite });

        }

        private async void TGREdit_Tapped(object sender, EventArgs e)
        {
            var s = Int32.Parse(((Image)sender).ClassId);

            var msg = MessagesList.Where(w => w.MsgID == s).FirstOrDefault();

            await Navigation.PushModalAsync(new BCMessages(msg) { Title = "Broadcast Message: Edit Message", Icon = "SPDInfo.png", BackgroundColor = Color.FloralWhite });

        }

        private void TGRDelete_Tapped(object sender, EventArgs e)
        {

        }

        private void TGRViewTargets_Tapped(object sender, EventArgs e)
        {

        }

    }
}