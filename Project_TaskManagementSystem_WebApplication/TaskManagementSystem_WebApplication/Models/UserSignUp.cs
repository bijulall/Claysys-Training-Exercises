using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem_WebApplication.Models
{
    public class UserSignUp
    {
        [Key]
        [Required]
        public int UserID { get; set; }
        [DisplayName("First name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last name")]
        [Required]
        public string LastName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]
        [DisplayName("Date of birth")]
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [DisplayName("Gender")]
        [Required]
        public string Gender { get; set; }
        [DisplayName("Email")]
        [Required]
        public string EmailAddress { get; set; }
        [DisplayName("Phone")]
        [Required]
        public string PhoneNumber { get; set; }
        [DisplayName("Address")]
        [Required]
        public string Address { get; set; }
        [DisplayName("State")]
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]

        public string Username { get; set; }
        [Required]

        public string Password { get; set; }
        [DisplayName("User role")]
        [Required]
        public string UserRole { get; set; }
       

        public string ProfilePhoto { get; set; }

        public HttpPostedFileBase ProfilePhotoFile { get; set; }

        public IEnumerable<SelectListItem> States { get; set; } =new List<SelectListItem>();
        public IEnumerable<SelectListItem> Cities { get; set; } = new List<SelectListItem>();
    }
}