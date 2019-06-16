using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPDInfoApp.Models
{
   public class JsonStudentFeedback:JsonSpdInfo
    {
        [JsonProperty("ApplicationID")]
        public string ApplicationID { get; set; }
        [JsonProperty("FBJson")]
         public string FBJson { get; set; }
        [JsonProperty("FBDateTime")]
        public DateTime FBDateTime { get; set; }
    }

}
