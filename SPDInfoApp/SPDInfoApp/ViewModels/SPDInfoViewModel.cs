using Acr.UserDialogs;
using PCLStorage;
using Plugin.FileUploader;
using Plugin.FileUploader.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SPDInfoApp.HelperClasses;
using SPDInfoApp.Models;
using SPDInfoApp.WebServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            set { _photo = value; OnPropertyChanged(); UpdatePhoto(); }
        }
        public ICommand TackPhotoCommand { get; set; }

        private bool _IsProgressVisible;
        public bool IsProgressVisible { get { return _IsProgressVisible; } set { _IsProgressVisible = value; OnPropertyChanged(); } }

        private double _ProgessValue;

        private string _applicationID;
        public double ProgessValue { get { return _ProgessValue; } set { _ProgessValue = value; OnPropertyChanged(); } }

        Queue<string> paths = new Queue<string>();
        public string filePath = string.Empty;
        bool isBusy = false;
        private int c;

        private Stream _stream { get; set; }
        private byte[] streamBuffer { get; set; }
        private FileStream FS { get; set; }

        public SPDInfoViewModel(SPDInfo spdinfo = null)
        {
            // datesInfo = new DatesInfo();
            this._entryData = spdinfo;
            SubmitCommand = new Command(OnSubmitAsync);
            TackPhotoCommand = new Command(OnTakePhoto);
            MessagingCenter.Subscribe<NavigationMessage>(this, "EntryPage:SetPhoto", SetPhoto);
            MessagingCenter.Subscribe<NavigationMessage>(this, "EntryPage:PhotoTaken", PhotoTaken);
           this.Photo= Application.Current.Properties["StudentPhoto"].ToString() ;

        }

        private void SetPhoto(NavigationMessage obj)
        {
            String photoSource = (string)obj.Options;
            Photo = photoSource;
            // filePath = Photo;

        }

        private void PhotoTaken(NavigationMessage obj)
        {
            String photoSource = (string)obj.Options;
            // Photo = photoSource;
            filePath = photoSource;

            FS = (System.IO.FileStream)obj.Options2;

            int length = Convert.ToInt32(FS.Length);
            streamBuffer = new byte[length];
            FS.Read(streamBuffer, 0, length);
            FS.Close();
            // streamBuffer = FS.Length;

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
            //return;
            if (!await App.Current.MainPage.DisplayAlert("Submit Information", "Are you sure?", "Yes", "No")) return;
            PHPServices MyService = new PHPServices();
            string result = "";
            using (UserDialogs.Instance.Loading("Submiting data.\nPlease Wait.", null, null, true, MaskType.Black))
            {
                result = await MyService.SubmitStudentInfo(_entryData);
            }
            //User data = User.FromJson(result);

            if (result.Contains( "Success"))
            {
                var cuurentLoginInfo = Application.Current.Properties["LoginInfo"];
                var loginInfos = Utils.DeserializeFromJson<List<LoginInfo>>(cuurentLoginInfo.ToString());
                string ApplicationID = Xamarin.Forms.Application.Current.Properties["StudentApplicationID"].ToString();
                _applicationID = ApplicationID;
                loginInfos.Where(w => w.ApplicationId == ApplicationID).Select(w => w.EntryData = _entryData).ToList();

                var jsonstring = Utils.SerializeToJson(loginInfos);

                Application.Current.Properties["LoginInfo"] = jsonstring;

                await Xamarin.Forms.Application.Current.SavePropertiesAsync();

                cuurentLoginInfo = Application.Current.Properties["LoginInfo"];


                OnUploadAsync();
                await App.Current.MainPage.DisplayAlert("Sucess", "Information Submitted Successfully..", "Ok");

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failed", "!Oops... Somthing went wrong. \n(" + result + ")", "Ok");
                return;

            }
        }

        private void UpdatePhoto()
        {
            //StudentPhoto
            Application.Current.Properties["StudentPhoto"] = Photo;
            Application.Current.SavePropertiesAsync();


        }


        private async void OnUploadAsync()
        {
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    await DBUpdate();
            //    return;
            //}
            if (Photo == "") return;
            if (isBusy)
                return;
            isBusy = true;

            ProgessValue = 0.0f;

            isBusy = true;
            IsProgressVisible = true;
            if (filePath == null || string.IsNullOrEmpty( filePath) || filePath=="")
            {
                //await PCLHelper.LoadImage();
                return;
                FileStream fs = new FileStream(Photo, FileMode.Open, System.IO.FileAccess.Read);
                int length = Convert.ToInt32(fs.Length);
                byte[] streamBuffer = new byte[length];
                fs.Read(streamBuffer, 0, length);
                fs.Close();
                filePath = _applicationID + "." + GetFileExtention(Photo);

                // return;
            }

            await CrossFileUploader.Current.UploadFileAsync("http://geoinfotech.org.in/SPDInfouploadImage.php", new FileBytesItem("uploaded_file", streamBuffer, filePath), new Dictionary<string, string>()
            {
                /*<HEADERS HERE>*/
            });
            //}
            //await CrossFileUploader.Current.UploadFileAsync("http://geoinfotech.org.in/SPDInfouploadImage.php", new FilePathItem("uploaded_file", filePath), new Dictionary<string, string>()
            //{
            //    /*<HEADERS HERE>*/
            //});

            isBusy = false;

        }

        public string GetFileExtention(string filename = "")
        {
            if (filename == "") return string.Empty;
            var fn = filename.Split('.');
            return fn[fn.Length - 1];
        }


        private static byte[] GetBytesFromImage(string imagePath)
        {
            //var imgFile = new Java.Io.File(imagePath);
            //var stream = new FileInputStream(imgFile);
            //var bytes = new byte[imgFile.Length()];
            //stream.Read(bytes);
            //return bytes;
            return null;
        }

        private void Current_FileUploadProgress(object sender, FileUploadProgress e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //progress.Progress = e.Percentage / 100.0f;
                ProgessValue = e.Percentage / 100.0f;
            });
        }

        private void Current_FileUploadError(object sender, FileUploadResponse e)
        {
            isBusy = false;
            System.Diagnostics.Debug.WriteLine($"{e.StatusCode} - {e.Message}");
            Device.BeginInvokeOnMainThread(async () =>
            {
                await App.Current.MainPage.DisplayAlert("File Upload", "Upload Failed", "Ok");
                IsProgressVisible = false;
                ProgessValue = 0.0f;
            });
        }

        private void Current_FileUploadCompleted(object sender, FileUploadResponse e)
        {

            //  System.Diagnostics.Debug.WriteLine($"{e.StatusCode} - {e.Message}");
            Device.BeginInvokeOnMainThread(async () =>
            {


                if (c == 1)
                {
                    isBusy = false;
                    c = 0;
                    ProgessValue = 0.0f;
                    IsProgressVisible = false;
                    //  await UpdateDatabase();
                    await App.Current.MainPage.DisplayAlert("File Upload", "Upload Completed", "Ok");

                    // Init();
                }
                else
                {
                    c++;
                }
            });
        }


    }
}
