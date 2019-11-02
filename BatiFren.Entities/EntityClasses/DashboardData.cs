using System;
using System.Collections.Generic;
using UserManagement.Entities.EntityClasses;

namespace BatiFren.Controllers
{
    public class DashboardData
    {
        public Nullable<int> TotalUsers { get; set; }
        public Nullable<int> NewUsers { get; set; }
        public Nullable<int> UnconfirmedUsers { get; set; }
        public Nullable<int> BannedUsers { get; set; }
        public virtual List<User> UserMast { get; set; }
        public List<User> UsersMapData { get; set; }

    }
}