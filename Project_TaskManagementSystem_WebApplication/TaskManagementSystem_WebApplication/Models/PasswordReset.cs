using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TaskManagementSystem_WebApplication.Models
{
    public class PasswordReset
    {
        [Key]
        public int UserID { get; set; }

        [DisplayName("Email")]
        public string EmailAddress { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string NewPassword {  get; set; }




    }
}