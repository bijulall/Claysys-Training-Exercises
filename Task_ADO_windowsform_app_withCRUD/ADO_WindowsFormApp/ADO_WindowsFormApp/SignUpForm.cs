using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace ADO_WindowsFormApp
{
    public partial class SignUpForm : Form

    {
        string connectionString = "Server=DESKTOP-70RTERT\\SQLEXPRESS;Database=training_tasks_db;Integrated Security=True;TrustServerCertificate=True;";
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtUserName.Text) ||
                string.IsNullOrWhiteSpace(txtGender.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string plainPassword = txtPassword.Text;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO tblUserSignUps (firstName,lastName,userName,gender,email,phone,userPassword) VALUES (@firstName,@lastName ,@userName,@gender,@email,@phone,@userPassword)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                        command.Parameters.AddWithValue("@lastName", txtLastName.Text);
                        command.Parameters.AddWithValue("@userName", txtUserName.Text);
                        command.Parameters.AddWithValue("@gender", txtGender.Text);
                        command.Parameters.AddWithValue("@email", txtEmail.Text);
                        command.Parameters.AddWithValue("@phone", txtPhone.Text);
                        command.Parameters.AddWithValue("@userPassword", plainPassword);  

                        command.ExecuteNonQuery();
                        MessageBox.Show("Sign up successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        

                        btnCancel_Click(sender, e);
                        this.Close();



                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) 
                    {
                        MessageBox.Show("A user with this username or email already exists.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"SQL Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtUserName.Clear();
            txtGender.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtPassword.Clear();
        }
       


    }
}

