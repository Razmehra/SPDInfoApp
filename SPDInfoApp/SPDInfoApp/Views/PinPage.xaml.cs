using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using XERP.JSonModels;
//using XERP.WebServices;
using System.Threading;
using Acr.UserDialogs;
using Xamarin.Essentials;
using SPDInfoApp.WebServices;

namespace SPDInfoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PinPage : ContentPage
    {
        public object data;
        string MPIN = "";
        string UName = "";
        string UPW = "";
        string User = "";
        string Mobile = "";
        string loginMode = "";
        public string PinStock = "";


        public PinPage(string loginmode = null)
        {
            InitializeComponent();
            this.loginMode = loginmode;
            InitPage(loginmode);

            //AuthenticationAsync("Test Fingerprint");
        }

        private void InitPage(string loginmode)
        {

            switch (loginmode)
            {
                case "Admin":
                    this.MPIN = (Xamarin.Forms.Application.Current.Properties["AuthMPIN_Admin"].ToString());
                    this.UName = (Xamarin.Forms.Application.Current.Properties["AdminUserName"].ToString());
                    this.UPW = (Xamarin.Forms.Application.Current.Properties["AdminUserPW"].ToString());
                    break;
                case "Student":
                    this.MPIN = (Xamarin.Forms.Application.Current.Properties["AuthMPIN_Student"].ToString());
                    this.UName = (Xamarin.Forms.Application.Current.Properties["StudentName"].ToString());
                    this.Mobile = (Xamarin.Forms.Application.Current.Properties["StudentMobile"].ToString());

                    break;
                case "Alumni":
                    this.MPIN = (Xamarin.Forms.Application.Current.Properties["AuthMPIN_Alumni"].ToString());
                    this.UName = (Xamarin.Forms.Application.Current.Properties["AlumniName"].ToString());
                    this.Mobile = (Xamarin.Forms.Application.Current.Properties["AlumniMobile"].ToString());

                    break;
                default:
                    break;
            }



            var fs = new FormattedString();
            fs.Spans.Add(new Span { Text = "Hello ", ForegroundColor = Color.White, FontSize = 14 });
            fs.Spans.Add(new Span { Text = UName, ForegroundColor = Color.White, FontSize = 14, FontAttributes = FontAttributes.Bold });
            lblUser.FormattedText = fs;

            var fsTitle = new FormattedString();
            fsTitle.Spans.Add(new Span { Text = "Please ", ForegroundColor = Color.White, FontSize = 18 });
            // fsTitle.Spans.Add(new Span { Text = "🔐 Please ", ForegroundColor = Color.White, FontSize = 18 });
            fsTitle.Spans.Add(new Span { Text = ((MPIN == "") ? " SET " : " ENTER "), ForegroundColor = Color.FromHex("#ff7f50"), FontSize = 20, FontAttributes = FontAttributes.Bold });
            fsTitle.Spans.Add(new Span { Text = " Your ", ForegroundColor = Color.White, FontSize = 18 });
            fsTitle.Spans.Add(new Span { Text = "4 digit", ForegroundColor = Color.White, FontSize = 10 });
            fsTitle.Spans.Add(new Span { Text = " MPIN", ForegroundColor = Color.White, FontSize = 18, FontAttributes = FontAttributes.Bold });
            lblTitle.FormattedText = fsTitle;

        }

        //private async void OnAuthenticate(object sender, EventArgs e)
        //{
        //  //  await AuthenticationAsync("Put your finger!");

        //}

        //private async Task AuthenticationAsync(string reason, string cancel = null, string fallback = null, string tooFast = null)
        //{
        //    _cancel =  new CancellationTokenSource();

        //    var dialogConfig = new AuthenticationRequestConfiguration(reason)
        //    { // all optional
        //        CancelTitle = cancel,
        //        FallbackTitle = fallback,
        //        AllowAlternativeAuthentication = true,

        //    };

        //    // optional

        //    var result = await Plugin.Fingerprint.CrossFingerprint.Current.AuthenticateAsync(dialogConfig, _cancel.Token);

        //    await SetResultAsync(result);
        //   // UserDialogs.Instance.Toast(result.Status.ToString(), null);
        //}

        //private async Task SetResultAsync(FingerprintAuthenticationResult result)
        //{
        //    if (result.Authenticated)
        //    {
        //        await DisplayAlert("FingerPrint Sample", "Success", "Ok");
        //    }
        //    else
        //    {
        //        await DisplayAlert("FingerPrint Sample", result.ErrorMessage, "Ok");
        //    }
        //}

        private void AnimateObj()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var duration = TimeSpan.FromSeconds(0.3);
                Vibration.Vibrate(duration);
                // CrossVibrate.Current.Vibration(TimeSpan.FromSeconds(0.3));
                LayOutMPIN.Animate("Shake", Shake(), 16, Convert.ToUInt32(400));
            });

        }
        internal Animation Shake()
        {
            var animation = new Animation();
            animation.WithConcurrent((f) => LayOutMPIN.TranslationX = f,
                LayOutMPIN.TranslationX + 5, LayOutMPIN.TranslationX, Easing.Linear, 0, 0.1);

            animation.WithConcurrent((f) => LayOutMPIN.TranslationX = f,
                LayOutMPIN.TranslationX - 5, LayOutMPIN.TranslationX, Easing.Linear, 0.1, 0.2);

            animation.WithConcurrent((f) => LayOutMPIN.TranslationX = f,
                LayOutMPIN.TranslationX + 5, LayOutMPIN.TranslationX, Easing.Linear, 0.2, 0.3);

            animation.WithConcurrent((f) => LayOutMPIN.TranslationX = f,
                LayOutMPIN.TranslationX - 5, LayOutMPIN.TranslationX, Easing.Linear, 0.3, 0.4);

            animation.WithConcurrent((f) => LayOutMPIN.TranslationX = f,
                LayOutMPIN.TranslationX + 5, LayOutMPIN.TranslationX, Easing.Linear, 0.4, 0.5);

            animation.WithConcurrent((f) => LayOutMPIN.TranslationX = f,
                LayOutMPIN.TranslationX - 5, LayOutMPIN.TranslationX, Easing.Linear, 0.5, 0.6);

            animation.WithConcurrent((f) => LayOutMPIN.TranslationX = f,
                LayOutMPIN.TranslationX - 5, LayOutMPIN.TranslationX, Easing.Linear, 0.6, 0.7);

            animation.WithConcurrent((f) => LayOutMPIN.TranslationX = f,
                LayOutMPIN.TranslationX - 5, LayOutMPIN.TranslationX, Easing.Linear, 0.7, 0.8);

            return animation;
        }


        private async Task NumBtnClicked1(object sender, EventArgs e)
        {
            var MyButton = (Button)sender;
            string numVal = MyButton.CommandParameter.ToString();

            if (numVal == "-1")
            {
                MarkMPIN(PinStock.Length, true);
                PinStock = PinStock.Remove(PinStock.Length - 1, 1);
            }
            else
            {
                if (PinStock.Length <= 4)
                {
                    PinStock = PinStock + (PinStock.Length == 4 ? "" : numVal);
                    MarkMPIN(PinStock.Length, false);
                    if (PinStock.Length == 4)
                    {
                        LayOutMPIN.IsEnabled = false;
                        LayOutMPIN.Opacity = 50;
                        await BtnSubmit_ClickedAsync(PinStock);
                        LayOutMPIN.IsEnabled = true;
                        LayOutMPIN.Opacity = 100;
                    }
                }
            }
        }

        private void MarkMPIN(int Position, bool IsUnmark)
        {
            switch (Position)
            {
                case 1:
                    lblNum1.FontAttributes = FontAttributes.None;
                    lblNum1.Text = IsUnmark ? "◌" : "●";
                    lblNum2.FontAttributes = IsUnmark ? FontAttributes.None : FontAttributes.Bold;
                    break;
                case 2:
                    lblNum1.FontAttributes = FontAttributes.None;
                    lblNum2.Text = IsUnmark ? "◌" : "●";
                    lblNum3.FontAttributes = IsUnmark ? FontAttributes.None : FontAttributes.Bold;
                    break;
                case 3:
                    lblNum2.FontAttributes = FontAttributes.None;
                    lblNum3.Text = IsUnmark ? "◌" : "●";
                    lblNum4.FontAttributes = IsUnmark ? FontAttributes.None : FontAttributes.Bold;
                    break;
                case 4:
                    lblNum3.FontAttributes = FontAttributes.None;
                    lblNum4.Text = IsUnmark ? "◌" : "●";
                    lblNum4.FontAttributes = FontAttributes.None;
                    break;
                default:
                    break;
            }

        }


        private async Task LoadNewPINPageAsync()
        {
            // await Navigation.PushModalAsync(new PinPage1());
        }
        private async Task BtnSubmit_ClickedAsync(string xMPIN)
        {


            if (string.IsNullOrEmpty(xMPIN))
            {
                await DisplayAlert("Wrong/Short MPIN Alert", "Please provide 4 digit MPIN.", "OK");

            }
            else if (xMPIN.Length != 4)
            {
                await DisplayAlert("Wrong/Short MPIN Alert", "Please provide 4 digit MPIN.", "OK");
            }
            else
            {

                if (MPIN == "")
                {
                    var confirmed = await DisplayAlert("Confirm", "Your MPIN is " + xMPIN + "\nAre you sure?", "Yes", "No");
                    if (confirmed)
                    {
                        string authType = loginMode == "Admin" ? "AuthMPIN_Admin" : loginMode == "Student" ? "AuthMPIN_Student" : "AuthMPIN_Alumni";
                        Xamarin.Forms.Application.Current.Properties[authType] = xMPIN;
                        await Xamarin.Forms.Application.Current.SavePropertiesAsync();
                        //  Application.Current.MainPage = new NavigationPage(new MDPageMain());
                        // Application.Current.MainPage = new NavigationPage(new MDPageMain());
                        //** Write code for validate user before home screen
                        //  if (await LoginCheckAsync())
                        //  {

                        //** Write code for validate user before home screen
                        Application.Current.MainPage = new NavigationPage(new MDPage(loginMode));
                        // Application.Current.MainPage = new MDPage();
                        // }
                        // else
                        // {
                        //      await DisplayAlert("User Authentic Error.", data.MessageStatus, "OK");
                        //   }
                        //

                    }
                }
                else if (MPIN == xMPIN)
                {
                    //** Write code for validate user before home screen

                    bool LoginSuccess = false;
                    // BtnSubmit.IsEnabled = false;
                    if (loginMode == "Admin")
                    {
                        LoginSuccess = await LoginCheckAsync();
                        // BtnSubmit.IsEnabled = true;
                    }
                    else { LoginSuccess = true; }

                    if (LoginSuccess)
                    {
                        UserDialogs.Instance.Toast("Success..", null);
                        //** Write code for validate user before home screen
                        // Xamarin.Forms.Application.Current.MainPage = new MDPageMain();
                        Application.Current.MainPage = new NavigationPage(new MDPage(loginMode));
                    }
                    else
                    {
                        await DisplayAlert("User Authentication Error.", "User authentication failed", "OK");
                    }

                }
                else
                {
                    UserDialogs.Instance.Toast("MPIN not matched. Please try again..", null);
                    AnimateObj();
                    // await DisplayAlert("Wrong MPIN Alert", "MPIN not matched. Please try again..", "OK");

                    //xMPIN = "";
                    //txtMPIN.Focus();
                    ResetMPIN();

                }

            }


        }


        private void ResetMPIN()
        {
            PinStock = "";
            lblNum1.Text = "◌";
            lblNum2.Text = "◌";
            lblNum3.Text = "◌";
            lblNum4.Text = "◌";
        }

        private async Task BtnReset_ClickedAsync()
        {

            var confirmed = await DisplayAlert("Reset User", "Are you sure?", "Yes", "No");
            if (confirmed)
            {
                Xamarin.Forms.Application.Current.Properties.Clear();
                Xamarin.Forms.Application.Current.Properties.Add("MobVeryfied", true);
                await Xamarin.Forms.Application.Current.SavePropertiesAsync();
                Xamarin.Forms.Application.Current.MainPage = new LoginMenu();
            }
        }
        private async void OnEntryTextChangedAsync(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            if (entry.Text.Length == 4)
            {
                entry.Keyboard = null;
                ((IEntryController)entry).SendCompleted();
                //    entry.Unfocus();
                //    Thread.Sleep(500);
                //  BtnSubmit.IsEnabled = false;
                //  await BtnSubmit_ClickedAsync();
                //  BtnSubmit.IsEnabled = true;
                entry.Keyboard = Keyboard.Numeric;
            }
            else if (entry.Text.Length > 4)
            {
                string entryText = entry.Text;
                entry.TextChanged -= OnEntryTextChangedAsync;
                entry.Text = e.OldTextValue;
                entry.TextChanged += OnEntryTextChangedAsync;
            }
            return;
        }

        private async Task OnCompeledAsync(object sender, EventArgs e)
        {
            var entry = (Entry)sender;
            if (entry.Text.Length == 4)
            {
                //  BtnSubmit.IsEnabled = false;
                //  BtnReset.IsEnabled = false;
                // await BtnSubmit_ClickedAsync();
                //  BtnSubmit.IsEnabled = true;
                //   BtnReset.IsEnabled = true;
                //   entry.Keyboard = Keyboard.Numeric;
            }


        }


        private async Task<bool> LoginCheckAsync()
        {
            //txtMPIN.Unfocus();

            // txtMPIN = txtMPIN;
            bool IsSeccess = false;
            //

            string[] Logindata = { UName, UPW };

            PHPServices MyService = new PHPServices();
            string result = "";
            BusyIndicator.IsRunning = true;
            BusyIndicator.IsVisible = true;
            lblBusy.IsVisible = true;

            using (UserDialogs.Instance.Loading("Validating user.\nPlease Wait.", null, null, true, MaskType.Black))
            {
                result = await MyService.Login(Logindata);
            }
            
            BusyIndicator.IsRunning = false;
            BusyIndicator.IsVisible = false;
            lblBusy.IsVisible = false;

            return IsSeccess = result.Contains("success");








            //if (CrossConnectivity.Current.IsConnected)
            //{
            //    BusyIndicator.IsRunning = true;
            //    BusyIndicator.IsVisible = true;
            //    lblBusy.IsVisible = true;
            //    string[] Logindata = { this.ClientID, this.UName, this.UPW, this.Mobile };
            //    XERPWebServices myservice = new XERPWebServices();
            //    string xResult = await myservice.Login(Logindata);// XERPWebServices.LoginAsync(Logindata);
            //    data = XLogins.FromJson(xResult);
            //    Xamarin.Forms.Application.Current.Properties["xResult"] = xResult;
            //    Xamarin.Forms.Application.Current.Properties["SessionID"] = data.SessionId;
            //    await Xamarin.Forms.Application.Current.SavePropertiesAsync();
            //    BusyIndicator.IsRunning = false;
            //    BusyIndicator.IsVisible = false;
            //    lblBusy.IsVisible = false;
            //    IsSeccess = data.MessageStatus.ToUpper().Equals("SUCCESS.");
            //    return IsSeccess;
            //}
            //else
            //{
            //    await DisplayAlert("Network Error", "No Internet connection available.\n !Turn ON your data connection or connect using wifi then try again.", "Ok");
            //    return IsSeccess;
            //}

        }

        private async void NumBtnClicked(object sender, EventArgs e)
        {
            var MyButton = (Button)sender;
            string numVal = MyButton.CommandParameter.ToString();

            if (numVal == "-1")
            {
                MarkMPIN(PinStock.Length, true);
                PinStock = PinStock.Remove(PinStock.Length - 1, 1);
            }
            else
            {
                if (PinStock.Length <= 4)
                {
                    PinStock = PinStock + (PinStock.Length == 4 ? "" : numVal);
                    MarkMPIN(PinStock.Length, false);
                    if (PinStock.Length == 4)
                    {
                        LayOutMPIN.IsEnabled = false;
                        LayOutMPIN.Opacity = 50;
                        await BtnSubmit_ClickedAsync(PinStock);
                        LayOutMPIN.IsEnabled = true;
                        LayOutMPIN.Opacity = 100;
                    }
                }
            }
        }
    }
}
