using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SPDInfoApp.Models;
using SPDInfoApp.WebServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SPDInfoApp.ViewModels
{
    class SPDInfoViewModel : BaseViewModel
    {
        public SPDInfo _entryData { get; set; }// = new List<SPDInfo>();
        public ICommand SubmitCommand { get; set; }
        public DatesInfo datesInfo { get; set; }
        private string _photo;
        public string Photo
        {
            get { return _photo; }
            set { _photo = value; OnPropertyChanged(); }
        }
        public ICommand TackPhotoCommand { get; set; }

        public SPDInfoViewModel(SPDInfo spdinfo = null)
        {
            // datesInfo = new DatesInfo();
            this._entryData = spdinfo;
            SubmitCommand = new Command(OnSubmitAsync);
            TackPhotoCommand = new Command(OnTakePhoto);
            MessagingCenter.Subscribe<NavigationMessage>(this, "EntryPage:PhotoTaken", PhotoTaken);

        }

        private void PhotoTaken(NavigationMessage obj)
        {
            String photoSource = (string)obj.Options;
            Photo = photoSource;

        }

        private async void OnTakePhoto(object obj)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Rear
            });
            var stream = file?.GetStreamWithImageRotatedForExternalStorage();

            if (file == null) return;
            Photo = file.Path;// .AlbumPath;
            file.Dispose();
        }

        public async void OnSubmitAsync()
        {
            if (!await App.Current.MainPage.DisplayAlert("Submit Information", "Are you sure?", "Yes", "No")) return;
            PHPServices MyService = new PHPServices();
            string result = "";
            using (UserDialogs.Instance.Loading("Submiting data.\nPlease Wait.", null, null, true, MaskType.Black))
            {
                result = await MyService.Submit(_entryData);
            }
            //User data = User.FromJson(result);

            if (result == "Success")
            {
                await App.Current.MainPage.DisplayAlert("Sucess", "Information Submitted Successfully..", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failed", "!Oops... Somthing went wrong. \n(" + result + ")", "Ok");
                return;

            }
        }
    }
}
