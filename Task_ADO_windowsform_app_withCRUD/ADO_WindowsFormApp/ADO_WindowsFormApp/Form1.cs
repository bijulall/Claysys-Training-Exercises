using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADO_WindowsFormApp;
using Microsoft.Data.SqlClient;

namespace ADO_WindowsFormApp
{
    public partial class crudForm : Form
    {
        string connectionString = "Server=DESKTOP-70RTERT\\SQLEXPRESS;Database=training_tasks_db;Integrated Security=True;TrustServerCertificate=True;";
        public crudForm()
        {
            InitializeComponent();
        }

        private void crudForm_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=DESKTOP-70RTERT\\SQLEXPRESS;Database=training_tasks_db;Integrated Security=True;TrustServerCertificate=True;";
            string query = "SELECT * FROM tblUserSignUps";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
              
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }

        }
        //Create
        private void CreateButton_Click_1(object sender, EventArgs e)
        {
            SignUpForm form1 = new SignUpForm();
            form1.Show();
            form1.FormClosed += (s, args) => LoadData();
        }
        //Delete
        private void DeleteButton_Click_1(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                int id = Convert.ToInt32(selectedRow.Cells[0].Value);

                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DeleteRecord(id);

                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void DeleteRecord(int id)
        {
            string query = "DELETE FROM tblUserSignUps WHERE userId = @userId"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void LoadData()
        {
            string query = "SELECT * FROM tblUserSignUps";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
        //update
        private void UpdateButton_Click_1(object sender, EventArgs e)
        {
           
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                
                int userId = Convert.ToInt32(selectedRow.Cells["userId"].Value); 
                string firstName = Convert.ToString(selectedRow.Cells["FirstName"].Value);
                string lastName = Convert.ToString(selectedRow.Cells["LastName"].Value);
                string userName = Convert.ToString(selectedRow.Cells["UserName"].Value);
                string gender = Convert.ToString(selectedRow.Cells["Gender"].Value);
                string email = Convert.ToString(selectedRow.Cells["Email"].Value);
                string phone = Convert.ToString(selectedRow.Cells["Phone"].Value);
                string userPassword = Convert.ToString(selectedRow.Cells["UserPassword"].Value);

                UpdateForm updateForm = new UpdateForm(userId, firstName, lastName, userName, gender, email, phone, userPassword);
                updateForm.FormClosed += (s, args) => LoadData();
                updateForm.Show();
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
        }

       
    }
}