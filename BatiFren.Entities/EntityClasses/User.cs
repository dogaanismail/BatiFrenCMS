using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Entities.EntityClasses

{
    public class User
    {
        public int ID { get; set; }
        public string EncriptedID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PhotoPath { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string GoogleLink { get; set; }
        public string LinkedInLink { get; set; }
        public string SkypeID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<bool> IsLockedOut { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<System.DateTime> LastLockoutDate { get; set; }
        public Nullable<int> FailedPasswordCount { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public Nullable<int> InsertBy { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<System.Guid> ActivateGuid { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string introduction { get; set; }
        public string WebSiteLink { get; set; }
        public string CoverPhotoPath { get; set; }
        public string UserCity { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> userTypeID { get; set; }

    }
}