using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SPDInfoApp
{
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjYzNTZAMzEzNjJlMzQyZTMwR3UwK1JMV1M1eDNoNmNJaUNQMUNCY2lwcFJxM2h5cUpOWVBUbHNpcjRWVT0=;NjYzNTdAMzEzNjJlMzQyZTMwU25ydlJnb0M5bWhZRWVvUjM1eFkyVDRVTUdabGM4KzRpUk5oVkI4MlA5ST0=;NjYzNThAMzEzNjJlMzQyZTMwWHhSZ3NFbmx4OHlxK0JXSjZ2aXpmVmZwbHk1U254bzJnaDVRZHJVc3ppaz0=;NjYzNTlAMzEzNjJlMzQyZTMwbDlhNnBBVFl6a2ZhaDJDbEJFbUo0NUlMZFdQd3NDekdHMTg2YmZ0N1VuND0=;NjYzNjBAMzEzNjJlMzQyZTMwUTBDZDBBNWlSZ1RVRXhDY2F2V1l3cURQRUgxOGJkQ1doc1JoZ3NjQWZaUT0=;NjYzNjFAMzEzNjJlMzQyZTMwZ1l3dy92bDFWY0lzanMvM1JmbFhQdUFEZU5ZRWlhWTVXeVZ2dHI1NGV3MD0=;NjYzNjJAMzEzNjJlMzQyZTMwUzRNOXVZSDduZ1RiUU9EemlRZ0c4d2hkc253cTNmK1lXN0V2SjFISDh5UT0=;NjYzNjNAMzEzNjJlMzQyZTMwQnZmM0JXVklTd2JTT3d0YzlEanBNOGpyOG9DS1BHanRPcDYraFdXVWtSOD0=;NjYzNjRAMzEzNjJlMzQyZTMwWEk3TDQ5azYrS0c3NnF4ajA1VGlOdkJtWGxMei9TSVFtZThNcTdVeS9MZz0=");

            InitializeComponent();

            MainPage = new EntryPage();
        }

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
