using Newtonsoft.Json;
using SPDInfoApp.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SPDInfoApp.Models
{
    public class MessageModel : INotifyPropertyChanged
    {
        //public int MsgID { get; set; }
        //public DateTime MsgDate { get; set; }
        //public string MsgMessage { get; set; }
        //public DateTime MsgFromDate { get; set; }
        //public DateTime MsgToDate { get; set; }
        //public string Remark { get; set; }
        //public DateTime MsgStopDate { get; set; }
        //public int IsActive { get; set; }
        //public string MsgAudience { get; set; }
        //public int MsgMode { get; set; }
        //private bool _isShow { get; set; }
        //public bool IsShow { get { return _isShow; } set { _isShow = value; OnPropertyChanged("IsShow"); } }
        //public bool IsScroll { get; set; }

        private long _MsgID { get; set; }
        [JsonProperty("MsgID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MsgID
        {
            get { return _MsgID; }
            set { _MsgID = value; OnPropertyChanged("MsgID"); }
        }

        private DateTime _MsgDate { get; set; }
        [JsonProperty("MsgDate")]
        public DateTime MsgDate
        {
            get { return _MsgDate; }
            set { _MsgDate = value; OnPropertyChanged("MsgDate"); }
        }

        private string _msgMessage { get; set; }
        [JsonProperty("MsgMessage")]
        public string MsgMessage
        {
            get { return _msgMessage; }
            set { _msgMessage = value; OnPropertyChanged("MsgMessage"); }
        }


        private DateTime _msgFromDate { get; set; }
        [JsonProperty("MsgFromDate")]
        public DateTime MsgFromDate
        {
            get { return _msgFromDate; }
            set { _msgFromDate = value; OnPropertyChanged("MsgFromDate"); }
        }

        private DateTime _msgToDate { get; set; }
        [JsonProperty("MsgToDate")]
        public DateTime MsgToDate
        {
            get { return _msgToDate; }
            set { _msgToDate = value; OnPropertyChanged("MsgToDate"); }
        }

        private string _remark { get; set; }
        [JsonProperty("Remark")]
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; OnPropertyChanged("Remark"); }
        }


        //[JsonProperty("MsgStopDate")]
        //public DateTime MsgStopDate { get; set; }
        private bool _IsActive { get; set; }
        [JsonProperty("IsActive")]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; OnPropertyChanged("IsActive"); }
        }

        private string _MsgAudience { get; set; }
        [JsonProperty("MsgAudience")]
        public string MsgAudience
        {
            get { return _MsgAudience; }
            set { _MsgAudience = value; OnPropertyChanged("MsgAudience"); }
        }

        private long _msgMode { get; set; }
        [JsonProperty("MsgMode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MsgMode
        {
            get { return _msgMode; }
            set { _msgMode = value; OnPropertyChanged("MsgMode"); }
        }
        private bool _isShow { get; set; }
        public bool IsShow { get { return _isShow; } set { _isShow = value; OnPropertyChanged("IsShow"); } }

        private bool _isScroll { get; set; }
        [JsonProperty("IsScroll")]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool IsScroll
        {
            get { return _isScroll; }
            set { _isScroll = value; OnPropertyChanged("IsScroll"); }
        }


        //private bool _isShow { get; set; }
        //public bool IsShow { get { return _isShow; } set { _isShow = value; OnPropertyChanged("IsShow"); } }
        //public bool IsScroll { get; set; }

        public List<MessageTarget> MsgTarget { get; set; }
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
