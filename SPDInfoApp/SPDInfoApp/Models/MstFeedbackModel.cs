using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SPDInfoApp.Models
{
    public class MstFeedbackModel : INotifyPropertyChanged
    {
        public int ID { get; set; }
        private string _fbQuestion { get; set; }
        public string FBQuestion { get { return _fbQuestion; } set { _fbQuestion = value; OnPropertyChanged("FBQuestion"); } }
        private int _mValue { get; set; }
        public int MValue { get { return _mValue; } set { _mValue = value; OnPropertyChanged("MValue"); } }
        private bool _isShow { get; set; }
        public bool IsShow { get { return _isShow; } set { _isShow = value; OnPropertyChanged("IsShow"); } }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null) return;
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
