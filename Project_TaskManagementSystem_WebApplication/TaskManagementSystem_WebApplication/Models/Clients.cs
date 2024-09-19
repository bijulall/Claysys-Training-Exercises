using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TaskManagementSystem_WebApplication.Models
{
    public class Clients
    {
        [Key]
        [DisplayName("Client id")]
        public int ClientID { get; set; }
        [DisplayName("Client name")]
        public string ClientName { get; set; }
        [DisplayName("Contact mail")]
        public string ContactMail { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Created By")]
        public int CreatedBy { get; set; }


    }
}