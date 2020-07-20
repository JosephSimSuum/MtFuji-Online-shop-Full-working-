using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MountFuji.Models
{
    public class RegisterationVM
    {
        public long userID { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public Nullable<int> roleID { get; set; }
    }
}