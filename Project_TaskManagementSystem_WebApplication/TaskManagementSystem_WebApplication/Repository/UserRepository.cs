using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using TaskManagementSystem_WebApplication.Models;
using System.Web.Mvc;

namespace TaskManagementSystem_WebApplication.Repository
{
    [Authorize]
    public class UserRepository
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["adoConnectionString"].ToString();
        /// <summary>
        ///  GetTaskDetailsByID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<UserTaskList> GetTaskDetailsByID(int userID)
        {
            List<UserTaskList> taskList = new List<UserTaskList>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_TaskListWithNames";
                command.Parameters.AddWithValue("@assignedTo", userID);
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableTasks = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableTasks);
                connection.Close();
                foreach (DataRow dataRow in tableTasks.Rows)
                {
                    taskList.Add(new UserTaskList
                    {
                        TaskID = Convert.ToInt32(dataRow["TaskID"]),
                        TaskTitle = dataRow["TaskTitle"].ToString(),
                        Description = dataRow["Description"].ToString(),
                        Priority = dataRow["Priority"].ToString(),
                        Status = dataRow["Status"].ToString(),
                        CreatedDate = Convert.ToDateTime(dataRow["CreatedDate"]),
                        DueDate = Convert.ToDateTime(dataRow["DueDate"]),  
                        CreatedBy = dataRow["CreatedBy"].ToString(),
                        ProjectName = dataRow["ProjectName"].ToString(),
                        ClientName = dataRow["ClientName"].ToString(),
                    });
                }
            }
            return taskList;
        }
        /// <summary>
        ///  GetTaskDetailsByTask
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public List<UserTaskList> GetTaskDetailsByTask(int taskID)
        {
            List<UserTaskList> taskList = new List<UserTaskList>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_TaskListWithNamesByID";
                command.Parameters.AddWithValue("@taskID", taskID);
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableTasks = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableTasks);
                connection.Close();
                foreach (DataRow dataRow in tableTasks.Rows)
                {

                    taskList.Add(new UserTaskList
                    {
                        TaskID = Convert.ToInt32(dataRow["TaskID"]),
                        TaskTitle = dataRow["TaskTitle"].ToString(),
                        Description = dataRow["Description"].ToString(),
                        Priority = dataRow["Priority"].ToString(),
                        Status = dataRow["Status"].ToString(),
                        CreatedDate = Convert.ToDateTime(dataRow["CreatedDate"]),
                        DueDate = Convert.ToDateTime(dataRow["DueDate"]),  
                        CreatedBy = dataRow["CreatedBy"].ToString(),
                        ProjectName = dataRow["ProjectName"].ToString(),
                        ClientName = dataRow["ClientName"].ToString(),
                    });
                }
            }
            return taskList;
        }
        /// <summary>
        /// get for progress update
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public List<TaskProgress> GetTaskDetailsUpdate(int taskID)
        {
            List<TaskProgress> taskList = new List<TaskProgress>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_TaskListWithNamesByID";
                command.Parameters.AddWithValue("@taskID", taskID);
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableTasks = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableTasks);
                connection.Close();
                foreach (DataRow dataRow in tableTasks.Rows)
                {
                    taskList.Add(new TaskProgress
                    {
                        TaskTitle = dataRow["TaskTitle"].ToString(),
                        Status = dataRow["Status"].ToString(),
                        ProgressPercentage = Convert.ToInt32(dataRow["ProgressPercentage"]),
                        Comment = dataRow["Comment"].ToString(),
                        TaskFile = dataRow["TaskFile"].ToString(),
                    });
                } 
            }
             return taskList;
        }
        /// <summary>
        /// update task progress 
        /// </summary>
        /// <param name="taskProgress"></param>
        /// <returns></returns>
        public bool UpdateTaskDetailsByUser(TaskProgress taskProgress)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand("SPU_TaskProgressByUser", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@taskID", taskProgress.TaskID);
                    command.Parameters.AddWithValue("@status", taskProgress.Status); 
                    command.Parameters.AddWithValue("@progressPercentage",taskProgress.ProgressPercentage);
                    command.Parameters.AddWithValue("@comment", taskProgress.Comment);
                    command.Parameters.AddWithValue("@taskFile", (object)taskProgress.TaskFile ?? DBNull.Value);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                // Log or handle the exception
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// DeleteTask
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public bool DeleteTask(int taskId)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPD_Task", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@taskID", taskId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;

            }
        }
        /// <summary>
        /// GetReportByUserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<ReportForUser> GetReportByUserID(int userID)
        {
            List<ReportForUser> taskList = new List<ReportForUser>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_ReportByUserID";
                command.Parameters.AddWithValue("@userID", userID);
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableTasks = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableTasks);
                connection.Close();
                foreach (DataRow dataRow in tableTasks.Rows)
                {

                    taskList.Add(new ReportForUser
                    {
                        ReportID = Convert.ToInt32(dataRow["ReportID"]),
                        TaskID = Convert.ToInt32(dataRow["TaskID"]),
                        TaskTitle = dataRow["Tasktitle"].ToString(),
                        GeneratedBy = Convert.ToInt32(dataRow["GeneratedBy"]),
                        ReportName = dataRow["ReportName"].ToString(),
                        GeneratedOn = Convert.ToDateTime(dataRow["GeneratedOn"]),
                        ReportData = dataRow["ReportData"].ToString(),
                    });
                }
            }
            return taskList;
        }
        /// <summary>
        /// GetReportByID
        /// </summary>
        /// <param name="reportID"></param>
        /// <returns></returns>
        public ReportDetails GetReportByID(int reportID)
        {
            ReportDetails reportDetails = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPS_ReportForUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@reportID", reportID);
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableReports = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableReports);
                connection.Close();
                if (tableReports.Rows.Count > 0)
                {
                    DataRow dataRow = tableReports.Rows[0];
                    reportDetails = new ReportDetails
                    {
                        ReportID = Convert.ToInt32(dataRow["ReportID"]),
                        TaskID = Convert.ToInt32(dataRow["TaskID"]),
                        TaskTitle = dataRow["TaskTitle"].ToString(),
                        GeneratedBy = Convert.ToInt32(dataRow["GeneratedBy"]),
                        GeneratedAdmin = dataRow["GeneratedAdmin"].ToString(),
                        ReportName = dataRow["ReportName"].ToString(),
                        GeneratedOn = Convert.ToDateTime(dataRow["GeneratedOn"]),
                        ReportData = dataRow["ReportData"].ToString(),
                        ReportImage = dataRow["ReportImage"].ToString() // Base64 string
                    };
                }
            }
            return reportDetails;
        }
    }
}
    
   


