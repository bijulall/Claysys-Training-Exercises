using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TaskManagementSystem_WebApplication.Models;
using System.Web.Mvc;


namespace TaskManagementSystem_WebApplication.Repository
{
    public class SignRepository
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["adoConnectionString"].ToString();
        /// <summary>
        /// sign in
        /// </summary>
        /// <param name="EmailAddress"></param>
        /// <returns></returns>
        public UserLogin GetUserByEmail(string emailAddress)
        {
            UserLogin UserLoginDetails = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_UserByEmail";
                command.Parameters.AddWithValue("@EmailAddress", emailAddress);
                SqlDataAdapter DataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                connection.Open();
                DataAdapter.Fill(dataTable);
                connection.Close();
                if (dataTable.Rows.Count > 0)
                {
                    DataRow dataRow = dataTable.Rows[0];
                    UserLoginDetails = new UserLogin
                    {
                        UserID = Convert.ToInt32(dataRow["UserID"]),
                        FirstName = dataRow["FirstName"].ToString(),
                        EmailAddress = dataRow["EmailAddress"].ToString(),
                        Password = dataRow["Password"].ToString(),
                        UserRole = dataRow["UserRole"].ToString(),
                        ProfilePhoto = dataRow["ProfilePhoto"].ToString()
                    };
                }
            }
            return UserLoginDetails;
        }
        /// <summary>
        /// SignUp
        /// </summary>
        /// <param name="usersignup"></param>
        /// <returns></returns>
        public bool UserSignUpCreate(UserSignUp userSignup)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPI_UserAccount", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", userSignup.FirstName);
                command.Parameters.AddWithValue("@LastName", userSignup.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", userSignup.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", userSignup.Gender);
                command.Parameters.AddWithValue("@EmailAddress", userSignup.EmailAddress);
                command.Parameters.AddWithValue("@PhoneNumber", userSignup.PhoneNumber);
                command.Parameters.AddWithValue("@Address", userSignup.Address);
                command.Parameters.AddWithValue("@State", userSignup.State);
                command.Parameters.AddWithValue("@City", userSignup.City);
                command.Parameters.AddWithValue("@Username", userSignup.Username);
                command.Parameters.AddWithValue("@Password", userSignup.Password);
                command.Parameters.AddWithValue("@UserRole", userSignup.UserRole);
                command.Parameters.AddWithValue("@ProfilePhoto", (object)userSignup.ProfilePhoto ?? DBNull.Value);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// GetStates
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetStates()
        {
            var states = new List<SelectListItem>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("SPS_States", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        states.Add(new SelectListItem
                        {
                            Value = reader["StateID"].ToString(),
                            Text = reader["StateName"].ToString()
                        });
                    }
                }
            }
            return states;
        }

        /// <summary>
        /// Get Cities by StateId
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        public List<SelectListItem> GetCities(int stateId)
        {
            var cities = new List<SelectListItem>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("SPS_CitiesByState", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@StateID", stateId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cities.Add(new SelectListItem
                        {
                            Value = reader["CityID"].ToString(),
                            Text = reader["CityName"].ToString()
                        });
                    }
                }
            }
            return cities;
        }
        /// <summary>
        /// Delete user account
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool DeleteUser(int UserID)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPD_UserAccount", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// UpdateSignUp
        /// </summary>
        /// <param name="userSignup"></param>
        /// <returns></returns>
        public bool UpdateSignUp(UserSignUp userSignup)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPU_UserAccount", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", userSignup.UserID);
                command.Parameters.AddWithValue("@FirstName", userSignup.FirstName);
                command.Parameters.AddWithValue("@LastName", userSignup.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", userSignup.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", userSignup.Gender);
                command.Parameters.AddWithValue("@EmailAddress", userSignup.EmailAddress);
                command.Parameters.AddWithValue("@PhoneNumber", userSignup.PhoneNumber);
                command.Parameters.AddWithValue("@Address", userSignup.Address);
                command.Parameters.AddWithValue("@State", userSignup.State);
                command.Parameters.AddWithValue("@City", userSignup.City);
                command.Parameters.AddWithValue("@Username", userSignup.Username);
                command.Parameters.AddWithValue("@ProfilePhoto", (object)userSignup.ProfilePhoto ?? DBNull.Value);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// get user detail by id
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<UserSignUp>GetSignUpById(int UserID)
        {
            List<UserSignUp> SignUpList = new List<UserSignUp>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPS_UserAccountByID";
                command.Parameters.AddWithValue("@UserID", UserID);
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
                        City= dataRow["City"].ToString(),
                        Username = dataRow["Username"].ToString(),
                        UserRole = dataRow["UserRole"].ToString(),
                    });
                }
            }
            return SignUpList;
        }
        /// <summary>
        /// create contact us
        /// </summary>
        /// <param name="contactMessages"></param>
        /// <returns></returns>
        public bool CreateContactUs(ContactMessages contactMessages)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SPI_Message", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", contactMessages.Name);
                command.Parameters.AddWithValue("@Email", contactMessages.Email);
                command.Parameters.AddWithValue("@Subject", contactMessages.Subject);
                command.Parameters.AddWithValue("@Message", contactMessages.Message);
             

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}


