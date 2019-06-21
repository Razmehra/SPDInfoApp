using System;
using System.Collections.Generic;
using System.Text;

namespace SPDInfoApp.Models
{
    public class MessageTarget
    {
        public int AppID { get; set; }
        public bool IsAlumni { get; set; }
        public bool IsStudent { get; set; }
        public string Name { get; set; }
        public string OtherInfo { get; set; }
        public int ApplicationID { get; set; }
    }
}
