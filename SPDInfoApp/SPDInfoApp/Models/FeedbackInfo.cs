using System;
using System.Collections.Generic;
using System.Text;

namespace SPDInfoApp.Models
{
  public  class FeedbackInfo
    {
        public DateTime FBDt { get; set; }
        public LoginInfo loginInfo { get; set; }
        public List<StudentFeedbackModel> StudentFeedback { get; set; }
        public List<AlumniFeedbackModel> AlumniFeedback { get; set; } 
         
    }
}
