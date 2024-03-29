﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SPDInfoApp.Views;
using Plugin.FirebasePushNotification;
using System.Threading.Tasks;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SPDInfoApp
{
    public partial class App : Application
    {
        public static int ApplicationID { get; set; }

        public App()
        {
            //Register Syncfusion license
            ApplicationID = 0;
            //SyncFusion License ver 16.4.0.42
            // Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjYzNTZAMzEzNjJlMzQyZTMwR3UwK1JMV1M1eDNoNmNJaUNQMUNCY2lwcFJxM2h5cUpOWVBUbHNpcjRWVT0=;NjYzNTdAMzEzNjJlMzQyZTMwU25ydlJnb0M5bWhZRWVvUjM1eFkyVDRVTUdabGM4KzRpUk5oVkI4MlA5ST0=;NjYzNThAMzEzNjJlMzQyZTMwWHhSZ3NFbmx4OHlxK0JXSjZ2aXpmVmZwbHk1U254bzJnaDVRZHJVc3ppaz0=;NjYzNTlAMzEzNjJlMzQyZTMwbDlhNnBBVFl6a2ZhaDJDbEJFbUo0NUlMZFdQd3NDekdHMTg2YmZ0N1VuND0=;NjYzNjBAMzEzNjJlMzQyZTMwUTBDZDBBNWlSZ1RVRXhDY2F2V1l3cURQRUgxOGJkQ1doc1JoZ3NjQWZaUT0=;NjYzNjFAMzEzNjJlMzQyZTMwZ1l3dy92bDFWY0lzanMvM1JmbFhQdUFEZU5ZRWlhWTVXeVZ2dHI1NGV3MD0=;NjYzNjJAMzEzNjJlMzQyZTMwUzRNOXVZSDduZ1RiUU9EemlRZ0c4d2hkc253cTNmK1lXN0V2SjFISDh5UT0=;NjYzNjNAMzEzNjJlMzQyZTMwQnZmM0JXVklTd2JTT3d0YzlEanBNOGpyOG9DS1BHanRPcDYraFdXVWtSOD0=;NjYzNjRAMzEzNjJlMzQyZTMwWEk3TDQ5azYrS0c3NnF4ajA1VGlOdkJtWGxMei9TSVFtZThNcTdVeS9MZz0=");
            //SyncFusion License ver 17.1.0.38
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTA1NjcxQDMxMzcyZTMxMmUzMGFvaTdzY1lkZ21yWW1GVUc4eFJxSW1oTHN4a1ZZL240a3FtTDg5SituMzg9;MTA1NjcyQDMxMzcyZTMxMmUzMEEyNDBOODNQK2VkWHhjcDZpWm40Mm9OcHhTNDQxYXdQbVRQN0twQlFTdms9;MTA1NjczQDMxMzcyZTMxMmUzMG1FL3RtekRrU0lOdHpna1FFM0U0S09LN2wrNUtybW1neVNPQ3NLYzR4UTg9;MTA1Njc0QDMxMzcyZTMxMmUzMG5ZV29KVDlBZmpzbzF3WU56aXRNakpHTm9YSUV1ekRreDRVQndiL1dad1k9;MTA1Njc1QDMxMzcyZTMxMmUzMGoyMmZUbGxqMnh2SVV2RG9UREJXU3NCdUVnZ1NGdnVoaXArN09MeTRjUGc9;MTA1Njc2QDMxMzcyZTMxMmUzMGl4Nnk1ekM2NHB2LzJrUkF6TXVuYnNVd1liVG9naVVHZC82bHZSamJWTjQ9");
            bool IsLive = true; // ***********Also change this setting to LoginPage()***************
            string UrlLive = "http://xerp.xtranetindia.com/REST/ServManageMobile.svc/";
            string UrlDemo = "http://www.eatnstay.com/REST/ServManageMobile.svc/";

            if (!Xamarin.Forms.Application.Current.Properties.ContainsKey("BaseUrl"))
            {
                Xamarin.Forms.Application.Current.Properties.Add("BaseUrl", IsLive ? UrlLive : UrlDemo);
            }
            else
            {
                Xamarin.Forms.Application.Current.Properties["BaseUrl"] = IsLive ? UrlLive : UrlDemo;
            }

            if (!Application.Current.Properties.ContainsKey("LoginInfo"))
            {
                Application.Current.Properties.Add("LoginInfo",null);
            }

            if (!Application.Current.Properties.ContainsKey("ApplicationID"))
            {
                Application.Current.Properties.Add("ApplicationID", "");
            }

            if (!Application.Current.Properties.ContainsKey("Feedbacks"))
            {
                Application.Current.Properties.Add("Feedbacks", null);
            }


            if (!Application.Current.Properties.ContainsKey("AdminUserName"))
            {
                Application.Current.Properties.Add("AdminUserName", "");
            }

            if (!Application.Current.Properties.ContainsKey("AdminUserPW"))
            {
                Application.Current.Properties.Add("AdminUserPW", "");
            }


            if (!Application.Current.Properties.ContainsKey("AuthMPIN_Admin"))
            {
                Application.Current.Properties.Add("AuthMPIN_Admin", "");
            }

            if (!Application.Current.Properties.ContainsKey("AuthMPIN_Student"))
            {
                Application.Current.Properties.Add("AuthMPIN_Student", "");
            }

            if (!Application.Current.Properties.ContainsKey("StudentApplicationID"))
            {
                Application.Current.Properties.Add("StudentApplicationID", "");
            }

            if (!Application.Current.Properties.ContainsKey("AuthMPIN_Alumni"))
            {
                Application.Current.Properties.Add("AuthMPIN_Alumni", "");
            }

            if (!Application.Current.Properties.ContainsKey("AlumniName"))
            {
                Application.Current.Properties.Add("AlumniName", "");
            }
            if (!Application.Current.Properties.ContainsKey("AlumniMobile"))
            {
                Application.Current.Properties.Add("AlumniMobile", "");
            }
            if (!Application.Current.Properties.ContainsKey("AlumniEmail"))
            {
                Application.Current.Properties.Add("AlumniEmail", "");
            }

            if (!Application.Current.Properties.ContainsKey("StudentName"))
            {
                Application.Current.Properties.Add("StudentName", "");
            }
            if (!Application.Current.Properties.ContainsKey("StudentMobile"))
            {
                Application.Current.Properties.Add("StudentMobile", "");
            }
            if (!Application.Current.Properties.ContainsKey("StudentEmail"))
            {
                Application.Current.Properties.Add("StudentEmail", "");
            }

            if (!Application.Current.Properties.ContainsKey("StudentPhoto"))
            {
                Application.Current.Properties.Add("StudentPhoto", "");
            }


            if (!Application.Current.Properties.ContainsKey("StudentInfo"))
            {
                Application.Current.Properties.Add("StudentInfo", "");
            }

            if (!Application.Current.Properties.ContainsKey("AdminInfo"))
            {
                Application.Current.Properties.Add("AdminInfo", "");
            }

            if (!Application.Current.Properties.ContainsKey("AlumniInfo"))
            {
                Application.Current.Properties.Add("AlumniInfo", "");
            }



            InitializeComponent();

             MainPage = new LoginMenu();//EntryPage();

           // MainPage =  new BCMessageCenter() { Title="Settings: Broadcast Message Center", BackgroundColor=Color.FloralWhite};

           // LoadMainPage();
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Received");
            };

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }
            };

            CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Action");

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                    foreach (var data in p.Data)
                    {
                        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    }

                }

            };

        }

        //private async Task<Page> LoadMainPage()
        //{
        //    //MainPage = await new BCMessageCenter() { Title = "Settings: Broadcast Message Center", BackgroundColor = Color.FloralWhite };
        //    //return MainPage;
        //}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
