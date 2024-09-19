using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TaskManagementSystem_WebApplication.Models
{
    public class TaskProgress
    {
        [Key]
        public int TaskID { get; set; }
        [DisplayName("Task title")]
        public string TaskTitle { get; set; }
       
       
        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Progress Percentage")]
        public int ProgressPercentage{ get; set; }

        [DisplayName("Comment")]
       
        public string Comment { get; set; }

        public string TaskFile { get; set; }

        public HttpPostedFileBase TaskProgressFile { get; set; }



    }
}