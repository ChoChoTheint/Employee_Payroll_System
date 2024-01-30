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
    public partial class addPosition : Form
    {
        SqlConnection consql;
        string str;
        DataSet Dset;
        bool close = true;
        public addPosition()
        {
            InitializeComponent();
        }
        
        void AutoID()
        {
            int PID = 1; // Initialize CID with 1 as a fallback

            SqlDataAdapter ad = new SqlDataAdapter("SELECT PositionId FROM tblPosition ORDER BY PositionId DESC", consql);
            DataSet ds = new DataSet();
            ad.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string pID = ds.Tables[0].Rows[0]["PositionId"].ToString();

                // Try to parse the numeric part of the last customer ID
                if (int.TryParse(pID.Substring(2), out PID))
                {
                    PID++; // Increment the CID
                }
            }

            txtid.Text = "P_" + PID.ToString("0000000");
        }
      
        private void FillData()
        {
            
            string query = "Select * From tblPosition";
            SqlDataAdapter adapter = new SqlDataAdapter(query, consql);
            Dset = new DataSet();
            adapter.Fill(Dset);
            dataView.DataSource = Dset.Tables[0];

            dataView.Columns[0].HeaderText = "ID";
            dataView.Columns[1].HeaderText = "Department";
            dataView.Columns[2].HeaderText = "Position";
            dataView.Columns[3].HeaderText = "Basic Salary";
           
            dataView.Columns[0].Width = 120;
            dataView.Columns[1].Width = 150;
            dataView.Columns[2].Width = 120;
            dataView.Columns[3].Width = 200;
           
        }


        void ClearText()
        {
            txtid.Text = "";
            txtdept.Text = "";
            txtposition.Text = "";
            txtbasicsalary.Text = "";
           
        }

        private void addPosition_Load(object sender, EventArgs e)
        {
            str = "Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=EmpPayrollSystem;User ID=Sa;Password=p@ssword";
            consql = new SqlConnection(str);
            consql.Open();
            FillData();

            txtid.Enabled = false;
            this.ActiveControl = txtdept;
            
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
             ClearText();
             AutoID();
             this.ActiveControl = txtdept;
        }
        
        private void btnsave_Click(object sender, EventArgs e)
        {
            if(Validation()){
                string strInsert = "Insert Into tblPosition Values ('" + txtid.Text + "','" + txtdept.Text + "','" + txtposition.Text + "','" + txtbasicsalary.Text + "')";
                SqlCommand mycmd = new SqlCommand(strInsert, consql);
                mycmd.ExecuteNonQuery();
                MessageBox.Show("Finish save information", "Save Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillData();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text))
            {
                errorProvider1.Clear();
                MessageBox.Show("Choose ID", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else if(Validation())
            {
                
                string str = "Update tblPosition Set Department='" + txtdept.Text + "',Position='" + txtposition.Text + "',BasicSalary='" + txtbasicsalary.Text + "'  Where PositionID='" + txtid.Text + "'";
                SqlCommand mycmd = new SqlCommand(str, consql);
                mycmd.ExecuteNonQuery();
                MessageBox.Show("Finish update information ", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillData();
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text))
            {
                errorProvider1.Clear();
                MessageBox.Show("Choose ID", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string str = "Delete from tblPosition Where PositionID='" + txtid.Text + "'";
                SqlCommand mycmd = new SqlCommand(str, consql);
                mycmd.ExecuteNonQuery();
                MessageBox.Show("Finish delete information ", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillData();
                ClearText();
            }
        }
        private void dataView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataView.CurrentRow.Index;
            txtid.Text = Dset.Tables[0].Rows[i][0].ToString();
            txtdept.Text = Dset.Tables[0].Rows[i][1].ToString();
            txtposition.Text = Dset.Tables[0].Rows[i][2].ToString();
            txtbasicsalary.Text = Dset.Tables[0].Rows[i][3].ToString();

            btnsave.Enabled = false;
            btnnew.Enabled = false;
        }

        private void addPosition_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (close)
            {
                DialogResult result = MessageBox.Show("Are You Sure You Want To Exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    close = false;
                    Application.Exit();
                    //dashboardEmp dashboardEmp = new dashboardEmp();
                    //this.Hide();
                    //dashboardEmp.Show();
                }
                else
                {
                    close = true;
                }
            }        
        }
        private void txtdept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtposition.Focus();
            }
        }

        private void txtposition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtbasicsalary.Focus();
            }
        }

        private void txtbasicsalary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtdept.Focus();
            }
        }

        
        private bool Validation()
        {
            bool result = false;
            if (string.IsNullOrEmpty(txtdept.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtdept, "User ID Required");
                MessageBox.Show("Department Field Required", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(txtposition.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtposition, "User Name Required");
                MessageBox.Show("Position Field Required", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(txtbasicsalary.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtbasicsalary, "User Password Required");
                MessageBox.Show("Basic Salary Field Required", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                errorProvider1.Clear();
                result = true;
            }
            return result;
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtid.Text = "";
            txtdept.Text = "";
            txtposition.Text = "";
            txtbasicsalary.Text = "";
        }

        private void admindashboard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dashboardEmp dashboardEmp = new dashboardEmp();
            dashboardEmp.Visible = true;
            addPosition addPosition = new addPosition();
            addPosition.Visible = false;
            addEmployee addEmployee = new addEmployee();
            addEmployee.Visible = false;
            calculateSalary calculateSalary = new calculateSalary();
            calculateSalary.Visible = false;
            salaryReport salaryReport = new salaryReport();
            salaryReport.Visible = false;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dashboardEmp dashboardEmp = new dashboardEmp();
            dashboardEmp.Visible = false;
            addPosition addPosition = new addPosition();
            addPosition.Visible = true;
            addEmployee addEmployee = new addEmployee();
            addEmployee.Visible = false;
            calculateSalary calculateSalary = new calculateSalary();
            calculateSalary.Visible = false;
            salaryReport salaryReport = new salaryReport();
            salaryReport.Visible = false;
        }

        private void addemployee_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dashboardEmp dashboardEmp = new dashboardEmp();
            dashboardEmp.Visible = false;
            addPosition addPosition = new addPosition();
            addPosition.Visible = false;
            addEmployee addEmployee = new addEmployee();
            addEmployee.Visible = true;
            calculateSalary calculateSalary = new calculateSalary();
            calculateSalary.Visible = false;
            salaryReport salaryReport = new salaryReport();
            salaryReport.Visible = false;
        }

        private void calculatesalary_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dashboardEmp dashboardEmp = new dashboardEmp();
            dashboardEmp.Visible = false;
            addPosition addPosition = new addPosition();
            addPosition.Visible = false;
            addEmployee addEmployee = new addEmployee();
            addEmployee.Visible = false;
            calculateSalary calculateSalary = new calculateSalary();
            calculateSalary.Visible = true;
            salaryReport salaryReport = new salaryReport();
            salaryReport.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dashboardEmp dashboardEmp = new dashboardEmp();
            dashboardEmp.Visible = false;
            addPosition addPosition = new addPosition();
            addPosition.Visible = false;
            addEmployee addEmployee = new addEmployee();
            addEmployee.Visible = false;
            calculateSalary calculateSalary = new calculateSalary();
            calculateSalary.Visible = false;
            salaryReport salaryReport = new salaryReport();
            salaryReport.Visible = true;
        }

        private void logout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure You Want To Exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                login login = new login();
                this.Hide();
                login.Show();
            }
        }

       

     

  
    }
}
