using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SignUpMVCWebApplication.Models;

namespace SignUpMVCWebApplication.DAL
{
    public class SignUp_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionString"].ToString();
        //Get all signups
        public List<SignUp>GetAllSignUps()
        {
           List<SignUp>SignUpList = new List<SignUp>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_tblSignUps_Select";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtSignUps = new DataTable();
                connection.Open();
                sqlDA.Fill(dtSignUps);
                connection.Close();

                foreach (DataRow dr in dtSignUps.Rows)
                {

                    SignUpList.Add(new SignUp
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        UserName = dr["UserName"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Email = dr["Email"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Password = dr["Password"].ToString(),
                    });
                }
            }

            return SignUpList;
        }

        //Create new signup

        public bool InsertSignUp(SignUp signUp)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("usp_tblSignUps_insert", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", signUp.FirstName);
                command.Parameters.AddWithValue("@LastName", signUp.LastName);
                command.Parameters.AddWithValue("@UserName", signUp.UserName);
                command.Parameters.AddWithValue("@Gender", signUp.Gender);
                command.Parameters.AddWithValue("@Email", signUp.Email);
                command.Parameters.AddWithValue("@Phone", signUp.Phone);
                command.Parameters.AddWithValue("@Password", signUp.Password);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
                     
            }
            if(id >0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //Select by id

        public List<SignUp> GetSignUpById(int UserId)
        {
            List<SignUp> SignUpList = new List<SignUp>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_tblSignUps_SelectById";
                command.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtSignUps = new DataTable();
                connection.Open();
                sqlDA.Fill(dtSignUps);
                connection.Close();

                foreach (DataRow dr in dtSignUps.Rows)
                {

                    SignUpList.Add(new SignUp
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        UserName = dr["UserName"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Email = dr["Email"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Password = dr["Password"].ToString(),
                    });
                }
            }

            return SignUpList;
        }

        //Update User data

        public bool UpdateSignUp(SignUp signUp)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("usp_tblSignUps_Update", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", signUp.UserId);
                command.Parameters.AddWithValue("@FirstName", signUp.FirstName);
                command.Parameters.AddWithValue("@LastName", signUp.LastName);
                command.Parameters.AddWithValue("@UserName", signUp.UserName);
                command.Parameters.AddWithValue("@Gender", signUp.Gender);
                command.Parameters.AddWithValue("@Email", signUp.Email);
                command.Parameters.AddWithValue("@Phone", signUp.Phone);
                command.Parameters.AddWithValue("@Password", signUp.Password);

                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();

            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //Delete Product

        public bool DeleteSignUp(int UserId)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("usp_tblSignUps_Delete", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId",UserId);
                connection.Open();
                i=command.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
;



        }

    }




}