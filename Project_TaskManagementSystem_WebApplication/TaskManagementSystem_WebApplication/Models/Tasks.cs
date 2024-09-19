using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TaskManagementSystem_WebApplication.Models
{
    public class Tasks
    {
        [Key]
        public int TaskID { get; set; }
        [DisplayName("Task title")]
        public string TaskTitle { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("priority")]
        public string Priority { get; set; }
        [DisplayName("Created date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]
        public DateTime? CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]
        public DateTime? DueDate { get; set; }

        [DisplayName("Assigned User")]
        public int AssignedTo { get; set; }
        [DisplayName("Created By")]
        public int CreatedBy { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }
        [DisplayName("Project id")]
        public int ProjectID { get; set; }
        [DisplayName("Client id")]
        public int ClientID { get; set; }
        [DisplayName("Comment")]
        public string Comment { get; set; }



    }
}