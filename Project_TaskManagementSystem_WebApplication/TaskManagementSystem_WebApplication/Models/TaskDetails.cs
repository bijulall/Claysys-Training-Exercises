using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TaskManagementSystem_WebApplication.Models
{
    public class TaskDetails
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
        [DisplayName("Status")]
        public string Status { get; set; }
        [DisplayName("Progress Percentage")]
        public int ProgressPercentage { get; set; }

        [DisplayName("Created by")]
        public string CreatedBy { get; set; }
        [DisplayName("Assigned user")]
        public string AssignedUser { get; set; }

        [DisplayName("Project name")]
        public string ProjectName { get; set; }
        [DisplayName("Client name")]
        public string ClientName { get; set; }

        public string Comment { get; set; }

        public string TaskFile { get; set; }

    }
}