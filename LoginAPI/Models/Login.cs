using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginAPI.Models
{
    public class Login
    {
        public string UserName { set; get; }
        public string Password { set; get; }
    }
    public class Registration : EmployeeMaster { }
    public class Row
    {
        public int UserId { set; get; }
        public string User { set; get; }
        public string LoginName { set; get; }
        public string password { set; get; }
        public string Email { set; get; }
        public int? IsApproved { set; get; }
        public int? Status { set; get; }
        public string Approved { set; get; }
    }
}