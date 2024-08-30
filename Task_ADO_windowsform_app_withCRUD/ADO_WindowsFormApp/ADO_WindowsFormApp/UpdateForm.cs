using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ADO_WindowsFormApp
{
    public partial class UpdateForm : Form
    {
        private int _userId;
        public UpdateForm(int userId, string firstName, string lastName, string userName, string gender, string email, string phone, string userPassword)
        {
            InitializeComponent();
            _userId = userId;

            
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            txtUserName.Text = userName;
            txtGender.Text = gender; 
            txtEmail.Text = email;
            txtPhone.Text = phone;
            txtPassword.Text = userPassword;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string userName = txtUserName.Text;
            string gender = txtGender.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string userPassword = txtPassword.Text;

        
            UpdateRecord(_userId, firstName, lastName, userName, gender, email, phone, userPassword);
            this.Close();
        }

        private void UpdateRecord(int userId, string firstName, string lastName, string userName, string gender, string email, string phone, string userPassword)
        {
            string query = "UPDATE tblUserSignUps SET FirstName = @FirstName, LastName = @LastName, UserName = @UserName, Gender = @Gender, Email = @Email, Phone = @Phone, UserPassword = @UserPassword WHERE userId = @UserId";

            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-70RTERT\\SQLEXPRESS;Database=training_tasks_db;Integrated Security=True;TrustServerCertificate=True;"))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@UserPassword", userPassword);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

       
    }
}
