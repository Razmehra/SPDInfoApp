using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SPDInfoApp;
using SPDInfoApp.Droid;
using SPDInfoApp.Interfaces;
using Xamarin.Forms;
using System.IO;

[assembly:Dependency(typeof(FileHelper))]
namespace SPDInfoApp.Droid
{
    public class FileHelper : IFileReadWrite
    {
        public string ReadData(string filename)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            return File.ReadAllText(filePath);

        }

        public void WriteData(string filename, string data)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            File.WriteAllText(filePath, data);

        }
    }
}