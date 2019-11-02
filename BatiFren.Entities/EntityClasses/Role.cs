using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Entities.EntityClasses

{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<int> InsertBy { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }

        public virtual List<tblUserPermission> UserPermission { get; set; }
    }
}