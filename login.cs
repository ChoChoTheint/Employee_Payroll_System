using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace EmployeePayrollManagementSystem
{
    public partial class login : Form
    {
        SqlConnection connect = new SqlConnection("Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=EmpPayrollSystem;User ID=Sa;Password=p@ssword");
        public login()
        {
            InitializeComponent();
        }
        bool close = true;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = userName;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            if (userName.Text == "" && userPw.Text == "")
            {
               MessageBox.Show("Please Fill All Blank Fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(userName.Text == "" || userPw.Text == "")
            {

                MessageBox.Show("Login failed. Invalid username or password.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        if (userName.Text == "Admin" && userPw.Text == "admin10001")
                        {
                            dashboardEmp dashboardEmp = new dashboardEmp();
                            this.Hide();
                            dashboardEmp.Show();
                        }
                        else
                        {
                            MessageBox.Show("Login failed. Invalid username or password.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Login failed. Invalid username or password.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
            
        }
        
        private bool Validation()
        {
            bool result = false;
            if (string.IsNullOrEmpty(userName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(userName, "User Name Required");
            }
            else if (string.IsNullOrEmpty(userPw.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(userPw, "User Password Required");
            }
            else
            {
                errorProvider1.Clear();
                result = true;
            }
            return result;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
           // userId.Text = "";
            userName.Text = "";
            userPw.Text = "";
        }

        private void login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close)
            {
                DialogResult result = MessageBox.Show("Are You Sure You Want To Exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    close = false;
                    Application.Exit();
                }
                else
                {
                    close = true;
                }
            }           
        }

        private void userId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                userName.Focus();
            }
        }

        private void userName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                userPw.Focus();
            }
        }

     
    }
}
