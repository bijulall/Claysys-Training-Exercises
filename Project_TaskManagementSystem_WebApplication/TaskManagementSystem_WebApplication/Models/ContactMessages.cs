using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TaskManagementSystem_WebApplication.Models
{
    public class ContactMessages
    {
        [Key]
        [DisplayName("Message id")]
        public int MessageID { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Contact mail")]
        public string Email { get; set; }
        [DisplayName("Subject")]
        public string Subject { get; set; }

        [DisplayName("Message")]
        public string Message { get; set; }
    }
}