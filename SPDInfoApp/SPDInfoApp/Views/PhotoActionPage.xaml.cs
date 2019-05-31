using Plugin.Media;
using Plugin.Media.Abstractions;
using SPDInfoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SPDInfoApp.HelperClasses;
using PCLStorage;
//using Xamarin.Essentials;
//using PCLStorage;

namespace SPDInfoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoActionPage : ContentPage
    {
        private string _photoPath;
        private Stream _stream;
        private string _applicationID;
        private string _photoName;
        public PhotoActionPage()
        {
            InitializeComponent();
            var StudentApplicationID = (Xamarin.Forms.Application.Current.Properties["StudentApplicationID"].ToString());
            _applicationID = StudentApplicationID;

            takePhoto.Clicked += async (sender, args) =>
            {

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "MyCollegeAppPhoto",
                    SaveToAlbum = true,
                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    RotateImage = true,
                    DefaultCamera = CameraDevice.Front,
                    Name = StudentApplicationID


                });

                if (file == null)
                    return;

                // await DisplayAlert("File Location", file.Path, "OK");
                _photoPath = file.Path;
                _stream = file.GetStream();
                string FileName = _applicationID + "." + GetFileExtention(file.Path);

                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
                _photoName = FileName;

                BtnOk.IsEnabled = true;

            };



            pickPhoto.Clicked += async (sender, args) =>
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                    RotateImage = true,


                });


                if (file == null)
                    return;
                IFolder folder = FileSystem.Current.LocalStorage;

                _photoPath = file.Path;
                var _stream1 = ImageSource.FromStream(() =>
                  {
                      var stream = file.GetStream();
                      file.Dispose();
                      return stream;
                  });

                var stream11 = file.GetStream();
                _stream = file.GetStream();
                string FileName = _applicationID + "." + GetFileExtention(file.Path);
                image.Source = _stream1;
                _photoName = FileName;

                BtnOk.IsEnabled = true;
            };

        }

        private string GetFileExtention(string filename = "")
        {
            if (filename == "") return string.Empty;
            var fn = filename.Split('.');
            return fn[fn.Length - 1];
        }

        private async void BtnOk_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(new NavigationMessage(_photoPath), "EntryPage:SetPhoto");
            MessagingCenter.Send(new NavigationMessage(_photoName, _stream), "EntryPage:PhotoTaken");
            await Navigation.PopModalAsync();
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}