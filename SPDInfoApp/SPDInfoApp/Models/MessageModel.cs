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

        [JsonProperty("MsgID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MsgId { get; set; }

        [JsonProperty("MsgDate")]
        public DateTime MsgDate { get; set; }

        [JsonProperty("MsgMessage")]
        public string MsgMessage { get; set; }

        [JsonProperty("MsgFromDate")]
        public DateTime MsgFromDate { get; set; }

        [JsonProperty("MsgToDate")]
        public DateTime MsgToDate { get; set; }

        [JsonProperty("Remark")]
        public string Remark { get; set; }

        //[JsonProperty("MsgStopDate")]
        //public DateTime MsgStopDate { get; set; }

        [JsonProperty("IsActive")]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool IsActive { get; set; }

        [JsonProperty("MsgAudience")]
        public string MsgAudience { get; set; }

        [JsonProperty("MsgMode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MsgMode { get; set; }


        private bool _isShow { get; set; }
        public bool IsShow { get { return _isShow; } set { _isShow = value; OnPropertyChanged("IsShow"); } }
        [JsonProperty("IsScroll")]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool IsScroll { get; set; }


        //private bool _isShow { get; set; }
        //public bool IsShow { get { return _isShow; } set { _isShow = value; OnPropertyChanged("IsShow"); } }
        //public bool IsScroll { get; set; }

        public List<MessageTarget> MsgTarget {get;set;}
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
