using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SPDInfoApp.Models
{
    public class MessageModel : INotifyPropertyChanged
    {
        public int MsgID { get; set; }
        public DateTime MsgDate { get; set; }
        public string MsgMessage { get; set; }
        public DateTime MsgFromDate { get; set; }
        public DateTime MsgToDate { get; set; }
        public string Remark { get; set; }
        public DateTime MsgStopDate { get; set; }
        public int IsActive { get; set; }
        public string MsgAudience { get; set; }
        public int MsgMode { get; set; }
        private bool _isShow { get; set; }
        public bool IsShow { get { return _isShow; } set { _isShow = value; OnPropertyChanged("IsShow"); } }
        public bool IsScroll { get; set; }
        public List<MessageTarget> MsgTarget {get;set;}
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
