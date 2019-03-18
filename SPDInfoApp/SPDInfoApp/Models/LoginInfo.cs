using System;
using System.Collections.Generic;
using System.Text;

namespace SPDInfoApp.Models
{
  public  class LoginInfo
    {
        public string LoginType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int MPIN { get; set; }
        public int Mobile { get; set; }
        public string Email { get; set; }
        public string ApplicationID { get; set; }
    }
}
