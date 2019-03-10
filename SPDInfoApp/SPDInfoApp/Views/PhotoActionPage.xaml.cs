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

namespace SPDInfoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoActionPage : ContentPage
    {
        private string _photoPath;
        private Stream _stream;
        public PhotoActionPage()
        {
            InitializeComponent();
            takePhoto.Clicked += async (sender, args) =>
            {

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
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
                    RotateImage=true,
                    DefaultCamera = CameraDevice.Front
                    
                });

                if (file == null)
                    return;

                // await DisplayAlert("File Location", file.Path, "OK");
                _photoPath = file.Path;
                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
               // _photoPath = file.AlbumPath;
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
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                    RotateImage = true

                });


                if (file == null)
                    return;
                _photoPath = file.Path;
              var  _stream1= ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
                image.Source = _stream1;
                //_stream = _photoPath;

                BtnOk.IsEnabled = true;
            };

        }

        private async void BtnOk_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(new NavigationMessage(_photoPath,_stream), "EntryPage:PhotoTaken");
            await Navigation.PopModalAsync();
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}