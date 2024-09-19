using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementSystem_WebApplication.Models
{
    public class UserLogin
    {

        [Key]
        public int UserID { get; set; }

        [DisplayName("Email")]
        [Required]
        public string EmailAddress { get; set; }
        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }

        public string UserRole { get; set; }
        [DisplayName("First name")]
        public string FirstName { get; set; }
        public string ProfilePhoto { get; set; }
    }
}