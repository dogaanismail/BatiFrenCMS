using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Entities.EntityClasses

{
    public class UserLog
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> LogTime { get; set; }
        public string IPAddress { get; set; }
        public string MoreInfo { get; set; }

        public virtual tblUserMast tblUserMast { get; set; }
    }
}