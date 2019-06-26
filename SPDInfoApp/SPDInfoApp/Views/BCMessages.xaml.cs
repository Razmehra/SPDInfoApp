using SPDInfoApp.HelperClasses;
using SPDInfoApp.Models;
using SPDInfoApp.WebServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPDInfoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BCMessages : INotifyPropertyChanged
    {
        private bool _isnew;
        private Button _admButton;
        private ObservableCollection<MessageTarget> TargetList { get; set; }

        private MessageModel _msg { get; set; }

        public MessageModel _message
        {
            get { return _msg; }
            set {
                _msg = value;
                OnPropertyChanged("_message");
            }
        }




        public BCMessages(MessageModel message = null)
        {
            InitializeComponent();
            MessagingCenter.Unsubscribe<NavigationMessage>(this, "BCMessage:UpdateTargets");
            MessagingCenter.Subscribe<NavigationMessage>(this, "BCMessage:UpdateTargets", UpdateTarget);
            _message = message;
            if (message == null)
            {
                
                MessageModel messageModel = new MessageModel();
                _message = messageModel;// new MessageModel();
                _message.MsgMode = 3;
                _message.IsScroll = false;
                _message.MsgFromDate = DateTime.Now.Date;
                _message.MsgToDate = DateTime.Now.Date;
            }

            rgButton4Student.IsChecked = _message.MsgMode == 1;
            rgButton4Alumni.IsChecked = _message.MsgMode == 2;
            rgButton4All.IsChecked = _message.MsgMode == 3;

            rgButtonIsScroll.IsChecked = _message.IsScroll;
            rgButtonIsStatic.IsChecked = _message.IsScroll == false;
            BindingContext = _message;
            _isnew = message == null;

        }

        //public BCMessages(NavigationMessage message = null)
        //{
        //    InitializeComponent();
        //    MessagingCenter.Unsubscribe<NavigationMessage>(this, "BCMessage:UpdateTargets");
        //    MessagingCenter.Subscribe<NavigationMessage>(this, "BCMessage:UpdateTargets", UpdateTarget);
        //    if(message.Options==null)
        //  //  _message = message;
        //    BindingContext = _message;

        //}


        private void UpdateTarget(NavigationMessage obj)
        {
            TargetList = (ObservableCollection<MessageTarget>)obj.Options;
            LVTarget.BindingContext = this;
            LVTarget.ItemsSource = null;
            LVTarget.ItemsSource = TargetList;
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
            _admButton = img.Equals(ImgAdmDT1) ? btnAdmDt1 : btnAdmDt2;
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

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            
            if (!await DisplayAlert("Save Message Notification", "Are you sure?", "Yes", "No")) return;

            var tList = LVTarget.ItemsSource;
            var MyJsonString = Utils.SerializeToJson(tList);

            _message.MsgAudience = MyJsonString;
            _message.IsScroll = rgButtonIsScroll.IsChecked == true;
            _message.MsgMode = rgButton4Student.IsChecked == true ? 1 : rgButton4Alumni.IsChecked==true?2:3;
            _message.MsgDate = DateTime.Now;
            PHPServices webservice = new PHPServices();
           var retval= await webservice.SubmitMessages(_message);
           if(retval.Contains("Success."))
            {
                if (retval.Contains("#"))
                {
                    var LastID = retval.Split('#')[1];
                    _message.MsgID = long.Parse( LastID);
                }
            }
            MessagingCenter.Send(new NavigationMessage(_message,_isnew), "BCMessageCenter:UpdateTargets");
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
           await Navigation.PopModalAsync();
        }

        private async void BtnSubmit_Clicked(object sender, EventArgs e)
        {
            PHPServices pHPServices = new PHPServices();
            if (_message == null)
            {

            }
            await pHPServices.SubmitMessages(_message);

        }

        private void TapGestureRecognizerEdit_Tapped(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizerDelete_Tapped(object sender, EventArgs e)
        {

        }

        private async void BtnSelectTargets_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TargetSelection() { Title = "Target Selection" });
        }
    }
}