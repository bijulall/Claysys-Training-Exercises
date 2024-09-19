using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TaskManagementSystem_WebApplication.Models
{
    public class Projects
    {
        [Key]
        public int ProjectID { get; set; }
        [DisplayName("Project name")]
        public string ProjectName { get; set; }
        [DisplayName("Client id")]
        public int ClientID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]

        [DisplayName("Start date")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]
        [DisplayName("End date")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Created By")]
        public int CreatedBy { get; set; }
        

    }
}