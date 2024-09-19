using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using TaskManagementSystem_WebApplication.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace TaskManagementSystem_WebApplication.Repository
{
    public class AdminRepository
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["adoConnectionString"].ToString();
        /// <summary>
        /// get all users
        /// </summary>
        /// <returns></returns>
        public List<UserSignUp> GetAllUsers()
        {
            List<UserSignUp> SignUpList = new List<UserSignUp>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_Users";
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableSignUps = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableSignUps);
                connection.Close();
                foreach (DataRow dataRow in tableSignUps.Rows)
                {
                    SignUpList.Add(new UserSignUp
                    {
                        UserID = Convert.ToInt32(dataRow["UserID"]),
                        FirstName = dataRow["FirstName"].ToString(),
                        LastName = dataRow["LastName"].ToString(),
                        DateOfBirth = Convert.ToDateTime(dataRow["DateOfBirth"]),
                        Gender = dataRow["Gender"].ToString(),
                        EmailAddress = dataRow["EmailAddress"].ToString(),
                        PhoneNumber = dataRow["PhoneNumber"].ToString(),
                        Address = dataRow["Address"].ToString(),
                        State = dataRow["State"].ToString(),
                        City = dataRow["City"].ToString(),
                        Username = dataRow["Username"].ToString(),
                        UserRole = dataRow["UserRole"].ToString(),
                    });
                }
            }
            return SignUpList;
        }
        /// <summary>
        /// task creation
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public bool CreateTask(Tasks tasks)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPI_Tasks", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@taskTitle ", tasks.TaskTitle);
                command.Parameters.AddWithValue("@description", tasks.Description);
                command.Parameters.AddWithValue("@priority", tasks.Priority);
                command.Parameters.AddWithValue("@createdDate", tasks.CreatedDate);
                command.Parameters.AddWithValue("@dueDate", tasks.DueDate);
                command.Parameters.AddWithValue("@assignedTo", tasks.AssignedTo);
                command.Parameters.AddWithValue("@createdBy", tasks.CreatedBy);
                command.Parameters.AddWithValue("@status", tasks.Status);
                command.Parameters.AddWithValue("@projectID", tasks.ProjectID);
                command.Parameters.AddWithValue("@clientID", tasks.ClientID);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns></returns>
        public List<UserTaskList> GetAllTasks()
        {
            List<UserTaskList> taskList = new List<UserTaskList>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_AllTasksWithNames";
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
                        CreatedDate = Convert.ToDateTime(dataRow["CreatedDate"]),
                        DueDate = Convert.ToDateTime(dataRow["DueDate"]),
                        AssignedUser = dataRow["AssignedUser"].ToString(),

                        Status = dataRow["Status"].ToString(),
                        CreatedBy = dataRow["CreatedBy"].ToString(),
                        ProjectName = dataRow["ProjectName"].ToString(),
                        ClientName = dataRow["ClientName"].ToString(),

                    });
                }
            }
            return taskList;
        }
        /// <summary>
        /// get client by Id
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public List<Tasks> GetTaskByID(int taskID)
        {
            List<Tasks> taskList = new List<Tasks>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_TasksByID";
                command.Parameters.AddWithValue("@TaskID", taskID);
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableTasks = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableTasks);
                connection.Close();
                foreach (DataRow dataRow in tableTasks.Rows)
                {
                    taskList.Add(new Tasks
                    {
                        TaskID = Convert.ToInt32(dataRow["TaskID"]),
                        TaskTitle = dataRow["TaskTitle"].ToString(),
                        Description = dataRow["Description"].ToString(),
                        Priority = dataRow["Priority"].ToString(),
                        CreatedDate = Convert.ToDateTime(dataRow["CreatedDate"]),
                        DueDate = Convert.ToDateTime(dataRow["DueDate"]),
                        AssignedTo = Convert.ToInt32(dataRow["AssignedTo"]),
                        CreatedBy = Convert.ToInt32(dataRow["CreatedBy"]),
                        Status = dataRow["Status"].ToString(),
                        ProjectID = Convert.ToInt32(dataRow["ClientID"]),
                        ClientID = Convert.ToInt32(dataRow["ClientID"]),
                    });
                }
            }
            return taskList;
        }
        /// <summary>
        /// get task all data for admin
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns> 
        public List<TaskDetails> GetTaskAllDataByID(int taskID)
        {
            List<TaskDetails> taskList = new List<TaskDetails>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_TaskAllDataByID";
                command.Parameters.AddWithValue("@TaskID", taskID);
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableTasks = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableTasks);
                connection.Close();
                foreach (DataRow dataRow in tableTasks.Rows)
                {
                    taskList.Add(new TaskDetails
                    {
                        TaskID = Convert.ToInt32(dataRow["TaskID"]),
                        TaskTitle = dataRow["TaskTitle"].ToString(),
                        Description = dataRow["Description"].ToString(),
                        Priority = dataRow["Priority"].ToString(),
                        CreatedDate = Convert.ToDateTime(dataRow["CreatedDate"]),
                        DueDate = Convert.ToDateTime(dataRow["DueDate"]),
                        AssignedUser = dataRow["AssignedUser"].ToString(),

                        Status = dataRow["Status"].ToString(),
                        ProgressPercentage = Convert.ToInt32(dataRow["ProgressPercentage"]),
                        CreatedBy = dataRow["CreatedBy"].ToString(),
                        ProjectName = dataRow["ProjectName"].ToString(),
                        ClientName = dataRow["ClientName"].ToString(),
                        Comment = dataRow["Comment"].ToString(),
                        TaskFile = dataRow["TaskFile"].ToString(),
                    });
                }
            }
            return taskList;
        }
        /// <summary>
        /// update Task
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public bool UpdateTask(Tasks tasks)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPU_Tasks", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@taskID",tasks.TaskID);
                command.Parameters.AddWithValue("@taskTitle ", tasks.TaskTitle);
                command.Parameters.AddWithValue("@description", tasks.Description);
                command.Parameters.AddWithValue("@priority", tasks.Priority);
                command.Parameters.AddWithValue("@createdDate", tasks.CreatedDate);
                command.Parameters.AddWithValue("@dueDate", tasks.DueDate);
                command.Parameters.AddWithValue("@assignedTo", tasks.AssignedTo);
                command.Parameters.AddWithValue("@createdBy", tasks.CreatedBy);
                command.Parameters.AddWithValue("@status", tasks.Status);
                command.Parameters.AddWithValue("@projectID", tasks.ProjectID);
                command.Parameters.AddWithValue("@clientID", tasks.ClientID);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// delete client
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
        /// Create Project
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        public bool CreateProject(Projects projects)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPI_Project", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProjectName ", projects.ProjectName);
                command.Parameters.AddWithValue("@ClientID", projects.ClientID);
                command.Parameters.AddWithValue("@StartDate", projects.StartDate);
                command.Parameters.AddWithValue("@EndDate", projects.EndDate);
                command.Parameters.AddWithValue("@Description", projects.Description);
                command.Parameters.AddWithValue("@CreatedBy", projects.CreatedBy);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// get all Projects
        /// </summary>
        /// <returns></returns>
        public List<Projects> GetAllProjects()
        {
            List<Projects> projectList = new List<Projects>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_Projects";
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableProjects = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableProjects);
                connection.Close();
                foreach (DataRow dataRow in tableProjects.Rows)
                {

                    projectList.Add(new Projects
                    {
                        ProjectID = Convert.ToInt32(dataRow["ProjectID"]),
                        ProjectName = dataRow["ProjectName"].ToString(),
                        ClientID = Convert.ToInt32(dataRow["ClientID"]),
                        StartDate = Convert.ToDateTime(dataRow["StartDate"]),
                        EndDate = Convert.ToDateTime(dataRow["EndDate"]),
                        Description = dataRow["Description"].ToString(),
                        CreatedBy = Convert.ToInt32(dataRow["CreatedBy"]),  
                    });
                }
            }
            return projectList;
        }
        /// <summary>
        /// getproject by id
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public List<Projects> GetProjectByID(int projectID)
        {
            List<Projects> projectList = new List<Projects>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_ProjectByID";
                command.Parameters.AddWithValue("@ProjectID", projectID);
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableProject = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableProject);
                connection.Close();
                foreach (DataRow dataRow in tableProject.Rows)
                {
                    projectList.Add(new Projects
                    {
                        ProjectID = Convert.ToInt32(dataRow["ProjectID"]),
                        ProjectName = dataRow["ProjectName"].ToString(),
                        ClientID = Convert.ToInt32(dataRow["ClientID"]),
                        StartDate = Convert.ToDateTime(dataRow["StartDate"]),
                        EndDate = Convert.ToDateTime(dataRow["EndDate"]),
                        Description = dataRow["Description"].ToString(),
                        CreatedBy = Convert.ToInt32(dataRow["CreatedBy"]),
                    });
                }
            }
            return projectList;
        }
        //DeleteProject
        /// <summary>
        /// Delete
        /// </summary>
        /// <returns></returns>
        public bool DeleteProject(int ProjectID)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPD_Project", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProjectID", ProjectID);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        //updateProject
        public bool UpdateProject(Projects projects)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPU_Project", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProjectID", projects.ProjectID);
                command.Parameters.AddWithValue("@ProjectName ", projects.ProjectName);
                command.Parameters.AddWithValue("@ClientID", projects.ClientID);
                command.Parameters.AddWithValue("@StartDate", projects.StartDate);
                command.Parameters.AddWithValue("@EndDate", projects.EndDate);
                command.Parameters.AddWithValue("@Description", projects.Description);
                command.Parameters.AddWithValue("@CreatedBy", projects.CreatedBy);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// GET all clients
        /// </summary>
        /// <returns></returns>
        public List<Clients> GetAllClients()
        {
            List<Clients> clientList = new List<Clients>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_Clients";
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableClients = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableClients);
                connection.Close();
                foreach (DataRow dataRow in tableClients.Rows)
                {

                    clientList.Add(new Clients
                    {
                        ClientID = Convert.ToInt32(dataRow["ClientID"]),
                        ClientName = dataRow["ClientName"].ToString(),
                        ContactMail = dataRow["ContactMail"].ToString(),
                        Address = dataRow["Address"].ToString(),
                        CreatedBy = Convert.ToInt32(dataRow["CreatedBy"]),
                    });
                }
            }
            return clientList;
        }
        /// <summary>
        /// Create Client
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        public bool CreateClient(Clients clients)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPI_Clients", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClientName ", clients.ClientName);
                command.Parameters.AddWithValue("@ContactMail", clients.ContactMail);
                command.Parameters.AddWithValue("@Address", clients.Address);
                command.Parameters.AddWithValue("@CreatedBy", clients.CreatedBy);            

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// get client by Id
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public List<Clients> GetClientByID(int clientID)
        {
            List<Clients> clientList = new List<Clients>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_ClientsByID";
                command.Parameters.AddWithValue("@ClientID", clientID);
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableClients = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableClients);
                connection.Close();
                foreach (DataRow dataRow in tableClients.Rows)
                {
                    clientList.Add(new Clients
                    {
                        ClientID = Convert.ToInt32(dataRow["ClientID"]),
                        ClientName = dataRow["ClientName"].ToString(),
                        ContactMail = dataRow["ContactMail"].ToString(),
                        Address = dataRow["Address"].ToString(),
                        CreatedBy = Convert.ToInt32(dataRow["CreatedBy"]),

                    });
                }
            }
            return clientList;
        }
        /// <summary>
        /// update client
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        public bool UpdateClient(Clients clients)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPU_Clients", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClientID", clients.ClientID);
                command.Parameters.AddWithValue("@ClientName ", clients.ClientName);
                command.Parameters.AddWithValue("@ContactMail", clients.ContactMail);
                command.Parameters.AddWithValue("@Address", clients.Address);
                command.Parameters.AddWithValue("@CreatedBy", clients.CreatedBy);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// delete client
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public bool DeleteClient(int clientId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPD_Client", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClientID", clientId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// create report
        /// </summary>
        /// <param name="reports"></param>
        /// <returns></returns>
        public bool CreateReport(Reports reports)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPI_Reports", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TaskID", reports.TaskID);
                command.Parameters.AddWithValue("@GeneratedBy", reports.GeneratedBy);
                command.Parameters.AddWithValue("@ReportName", reports.ReportName);
                command.Parameters.AddWithValue("@GeneratedOn", reports.GeneratedOn);
                command.Parameters.AddWithValue("@ReportData", reports.ReportData);
                command.Parameters.AddWithValue("@ReportImage", (object)reports.ReportImage ?? DBNull.Value);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// GetAllReports
        /// </summary>
        /// <returns></returns>
        public List<Reports> GetAllReports()
        {
            List<Reports> reportList = new List<Reports>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_Reports";
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableReports = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableReports);
                connection.Close();
                foreach (DataRow dataRow in tableReports.Rows)
                {
                    reportList.Add(new Reports
                    {
                        ReportID = Convert.ToInt32(dataRow["ReportID"]),
                        TaskID = Convert.ToInt32(dataRow["TaskID"]),
                        GeneratedBy = Convert.ToInt32(dataRow["GeneratedBy"]),
                        ReportName = dataRow["ReportName"].ToString(),
                        GeneratedOn = Convert.ToDateTime(dataRow["GeneratedOn"]),
                        ReportData = dataRow["ReportData"].ToString(),
                    });
                }
            }
            return reportList;
        }
        /// <summary>
        /// GetReportByID
        /// </summary>
        /// <param name="reportID"></param>
        /// <returns></returns>
        public List<Reports> GetReportByID(int reportID)
        {
            List<Reports> reportList = new List<Reports>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_ReportByID";
                command.Parameters.AddWithValue("@ReportID", reportID);
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableReports = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableReports);
                connection.Close();
                foreach (DataRow dataRow in tableReports.Rows)
                {
                    reportList.Add(new Reports
                    {
                        ReportID = Convert.ToInt32(dataRow["ReportID"]),
                        TaskID = Convert.ToInt32(dataRow["TaskID"]),
                        GeneratedBy = Convert.ToInt32(dataRow["GeneratedBy"]),
                        ReportName = dataRow["ReportName"].ToString(),
                        GeneratedOn = Convert.ToDateTime(dataRow["GeneratedOn"]),
                        ReportData = dataRow["ReportData"].ToString(),
                    });
                }
            }
            return reportList;
        }
        /// <summary>
        /// UpdateRepor
        /// </summary>
        /// <param name="reports"></param>
        /// <returns></returns>
        public bool UpdateReport(Reports reports)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPU_Report", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ReportID", reports.ReportID);
                command.Parameters.AddWithValue("@TaskID", reports.TaskID);
                command.Parameters.AddWithValue("@GeneratedBy", reports.GeneratedBy);
                command.Parameters.AddWithValue("@ReportName", reports.ReportName);
                command.Parameters.AddWithValue("@GeneratedOn", reports.GeneratedOn);
                command.Parameters.AddWithValue("@ReportData", reports.ReportData);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// delete report
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public bool DeleteReport(int reportId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPD_Report", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@reportID", reportId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// GetUserByEmail
        /// </summary>
        /// <param name="EmailAddress"></param>
        /// <returns></returns>
        public PasswordReset GetUserByEmail(string EmailAddress)
        {
            PasswordReset UserLoginDetails = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_UserByEmail";
                command.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                SqlDataAdapter DataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                connection.Open();
                DataAdapter.Fill(dataTable);
                connection.Close();
                if (dataTable.Rows.Count > 0)
                {
                    DataRow dataRow = dataTable.Rows[0];
                    UserLoginDetails = new PasswordReset
                    {
                        UserID = Convert.ToInt32(dataRow["UserID"]),
                       
                        EmailAddress = dataRow["EmailAddress"].ToString(),
                        Password = dataRow["Password"].ToString(),
                    };
                }
            }
            return UserLoginDetails;
        }
        /// <summary>
        /// updatePassword
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool UpdatePassword(string emailAddress,string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPU_Password", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@emailAddress", emailAddress);
                command.Parameters.AddWithValue("@NewPassword", newPassword);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// comment messages
        /// </summary>
        /// <returns></returns>
        public List<ContactMessages> GetAllMessages()
        {
            List<ContactMessages> messageList = new List<ContactMessages>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_Messages";
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableMessages = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableMessages);
                connection.Close();
                foreach (DataRow dataRow in tableMessages.Rows)
                {
                    messageList.Add(new ContactMessages
                    {
                        MessageID = Convert.ToInt32(dataRow["MessageID"]),
                        Name = dataRow["Name"].ToString(),
                        Email = dataRow["Email"].ToString(),
                        Subject = dataRow["Subject"].ToString(),
                        Message = dataRow["Message"].ToString(),
                    });
                }
            }
            return messageList;
        }
        /// <summary>
        /// GetCommentByID
        /// </summary>
        /// <param name="messageID"></param>
        /// <returns></returns>
        public List<ContactMessages> GetCommentByID(int messageID)
        {
            List<ContactMessages> messageList = new List<ContactMessages>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_MessageByID";
                command.Parameters.AddWithValue("@messageID", messageID);
                SqlDataAdapter sqlDataAdaptor = new SqlDataAdapter(command);
                DataTable tableMessages = new DataTable();
                connection.Open();
                sqlDataAdaptor.Fill(tableMessages);
                connection.Close();
                foreach (DataRow dataRow in tableMessages.Rows)
                {
                    messageList.Add(new ContactMessages
                    {
                        MessageID = Convert.ToInt32(dataRow["MessageID"]),
                        Name = dataRow["Name"].ToString(),
                        Email = dataRow["Email"].ToString(),
                        Subject = dataRow["Subject"].ToString(),
                        Message = dataRow["Message"].ToString(),
                    });
                }
            }
            return messageList;
        }
        /// <summary>
        /// Delete Comment
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public bool DeleteMessage(int messageId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPD_Message", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MessageId", messageId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}