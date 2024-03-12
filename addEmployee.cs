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
    public partial class addEmployee : Form
    {
        function fn = new function();
        SqlConnection consql;
        string str;
        DataSet Dset;
        public addEmployee()
        {
            InitializeComponent();
        }

        private void adminDashboard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm dashboard = new MainForm();
            this.Hide();
            dashboard.Show();
        }

        private void addPosition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            addSalary addPosition = new addSalary();
            this.Hide();
            addPosition.Show();
        }

        private void add_Employee_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            addEmployee addEmployee = new addEmployee();
            this.Hide();
            addEmployee.Show();
        }

        private void calculate_Salary_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            calculateSalary calculateSalary = new calculateSalary();
            this.Hide();
            calculateSalary.Show();
        }

        private void log_Out_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login login = new login();
            this.Hide();
            login.Show();
        }



        private void FillData()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From tblDepartment", consql);
            DataSet ds = new DataSet();
            DataTable dt;
            da.Fill(ds, "Department");
            dt = ds.Tables["Department"];
            empdept.DataSource = dt;
            empdept.DisplayMember = dt.Columns["Department"].ToString();

            try
            {


                string query = "SELECT dbo.tblEmployee.*, dbo.tblPosition.Position, dbo.tblDepartment.Department, dbo.tblSalary.BasicSalary FROM dbo.tblEmployee INNER JOIN dbo.tblPosition ON dbo.tblEmployee.PositionID = dbo.tblPosition.Position_ID INNER JOIN dbo.tblSalary ON dbo.tblPosition.Position_ID = dbo.tblSalary.Position_ID INNER JOIN dbo.tblDepartment ON dbo.tblSalary.DeptID = dbo.tblDepartment.DeptID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, consql);
                DataTable dtEmp = new DataTable();
                Dset = new DataSet();
                adapter.Fill(Dset, "Emp");
                dtEmp = Dset.Tables["Emp"];
                dataGridView1.DataSource = dtEmp;

                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "PositioinID";
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].HeaderText = "Name";
                dataGridView1.Columns[3].HeaderText = "Address";
                dataGridView1.Columns[4].HeaderText = "Phone";
                dataGridView1.Columns[5].HeaderText = "NRC";
                dataGridView1.Columns[6].HeaderText = "Date of Birth";
                dataGridView1.Columns[7].HeaderText = "Gender";
                dataGridView1.Columns[8].HeaderText = "Email";
                dataGridView1.Columns[9].HeaderText = "Join Date";
                dataGridView1.Columns[11].HeaderText = "Bank Account";

                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[3].Width = 150;
                dataGridView1.Columns[4].Width = 150;
                dataGridView1.Columns[5].Width = 150;
                dataGridView1.Columns[6].Width = 150;
                dataGridView1.Columns[7].Width = 100;
                dataGridView1.Columns[8].Width = 100;
                dataGridView1.Columns[10].Width = 100;
                dataGridView1.Columns[10].Width = 150;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

              SqlDataAdapter daSalary = new SqlDataAdapter("SELECT s.BasicSalary FROM tblSalary s JOIN tblDepartment d ON s.DeptID = d.DeptID JOIN tblPosition p ON s.Position_ID = p.Position_ID", consql);
              DataSet dsSalary = new DataSet();
              daSalary.Fill(dsSalary, "Salary");

              DataTable dSalary = dsSalary.Tables["Salary"];
              empsalary.DataBindings.Clear();
              empsalary.DataBindings.Add(new Binding("Text", dsSalary.Tables["Salary"], "BasicSalary"));
      
              foreach (DataRow row in dSalary.Rows)
                {
                    string basicSalary = row["BasicSalary"].ToString();
                    empsalary.Text = basicSalary;
                }
        }

      private void addEmployee_Load(object sender, EventArgs e)
        {
            connection();
            AutoID();
            FillData();
            
            empsalary.Enabled = false;
            empid.Enabled = false;
            this.ActiveControl = empname;
        }
        void connection()
        {
            str = "Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=epms;Integrated Security=True";
            consql = new SqlConnection(str);
            consql.Open();
        }


        private void btn_new_Click(object sender, EventArgs e)
        {
            ClearText();
            AutoID();
            this.ActiveControl = empname;
        }
        void AutoID()
        {
            try
            {

                string PName;
                int PID;
                SqlDataAdapter ad = new SqlDataAdapter("Select EmpID from tblEmployee ORDER BY EmpID", consql);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    PName = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0].ToString();
                    PID = int.Parse(PName.Substring(2, (PName.Length - 2)));
                    empid.Text = "E_" + ((PID + 1).ToString("0000000"));
                }
                else
                {
                    empid.Text = "E_0000001";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while AutoID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ClearText()
        {
            empid.Text = "";
            empname.Text = "";
            empemail.Text = "";
            empaddress.Text = "";
            empph.Text = "";
            empdob.Text = "";
            empnrc.Text = "";
            empgender.Text = "";
            empjoindate.Text = "";
            empbankacc.Text = "";
            empdept.Text = "";
            empposition.Text = "";
            empsalary.Text = "";

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            ClearText();
            this.ActiveControl = empname;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    string deptID = "";
                    String conStr1 = "Select DeptID from tblDepartment Where Department ='" + empdept.Text + "'";
                    SqlCommand cmd1 = new SqlCommand(conStr1, consql);
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    if (reader1.Read())
                    {
                        deptID = reader1[0].ToString();
                    }
                    consql.Close();
                    consql.Open();
                    string positionID = "";
                    String conStr = "Select Position_ID from tblPosition Where Department ='" + deptID + "'And Position = '" + empposition.Text + "'";
                    SqlCommand cmd = new SqlCommand(conStr, consql);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        positionID = reader[0].ToString();
                    }
                    consql.Close();
                    consql.Open();

                    string strInsert = "Insert Into tblEmployee (EmpID,PositionID,Name,Email,Address,PhoneNumber,NRC,DOB,Gender,JoinDate,BankAccount) Values ('" + empid.Text + "','" + positionID + "','" + empname.Text + "','" + empemail.Text + "','" + empaddress.Text + "','" + empph.Text + "','" + empnrc.Text + "','" + empdob.Text + "','" + empgender.Text + "','" + empjoindate.Text + "','" + empbankacc.Text + "')";
                    SqlCommand mycmd = new SqlCommand(strInsert, consql);
                    mycmd.ExecuteNonQuery();
                    MessageBox.Show("Finish save information", "Save Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillData();
                    ClearText();
                    AutoID();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while saving data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private bool Validation()
        {
            bool result = false;
            if (string.IsNullOrEmpty(empid.Text))
            {
                 errorProvider1.Clear();
                 errorProvider1.SetError(empid, "ID Required");
            }
            else if (string.IsNullOrEmpty(empname.Text))
            {
                  errorProvider1.Clear();
                  errorProvider1.SetError(empname, "Employee Name Required");
            }
            else if (string.IsNullOrEmpty(empemail.Text))
            {
                  errorProvider1.Clear();
                 errorProvider1.SetError(empemail, "Email Required");
            }
            else if (string.IsNullOrEmpty(empaddress.Text))
            {
                 errorProvider1.Clear();
                 errorProvider1.SetError(empaddress, "Address Required");
            }
            else if (string.IsNullOrEmpty(empph.Text))
            {
                  errorProvider1.Clear();
                  errorProvider1.SetError(empph, "Ph No Required");
            }
            else if (string.IsNullOrEmpty(empdob.Text))
            {
                 errorProvider1.Clear();
                 errorProvider1.SetError(empdob, "DOB Required");
            }
            else if (string.IsNullOrEmpty(empnrc.Text))
            {
                 errorProvider1.Clear();
                 errorProvider1.SetError(empnrc, "NRC Required");
            }
            else if (string.IsNullOrEmpty(empbankacc.Text))
            {
                   errorProvider1.Clear();
                   errorProvider1.SetError(empbankacc, "Address Required");
            }
            else
            {
                errorProvider1.Clear();
                result = true;
            }
            return result;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            empid.Text = Dset.Tables[0].Rows[i][0].ToString();
            empname.Text = Dset.Tables[0].Rows[i][2].ToString();
            empaddress.Text = Dset.Tables[0].Rows[i][3].ToString();
            empph.Text = Dset.Tables[0].Rows[i][4].ToString();

            empemail.Text = Dset.Tables[0].Rows[i][8].ToString();
            empnrc.Text = Dset.Tables[0].Rows[i][5].ToString();
            empgender.Text = Dset.Tables[0].Rows[i][7].ToString();
            empbankacc.Text = Dset.Tables[0].Rows[i][10].ToString();

            string PID = Dset.Tables[0].Rows[i][1].ToString();

            SqlDataAdapter daPositions = new SqlDataAdapter("SELECT * FROM tblPosition", consql);
            DataSet dsPositions = new DataSet();
            DataTable dtPositions = new DataTable();
            daPositions.Fill(dsPositions, "Positions");
            dtPositions = dsPositions.Tables["Positions"];

            empdept.DataSource = dtPositions;
            empdept.DisplayMember = "Department";
            empposition.DataSource = dtPositions;
            empposition.DisplayMember = "Position";

            SqlDataAdapter daSalary = new SqlDataAdapter("SELECT BasicSalary FROM tblSalary WHERE Position_ID = '" + PID + "'", consql);
            DataSet dsSalary = new DataSet();
            DataTable dtSalary = new DataTable();
            daSalary.Fill(dsSalary, "Salary");
            dtSalary = dsSalary.Tables["Salary"];

            empsalary.DataBindings.Clear();
            if (dtSalary.Rows.Count > 0)
            {
                empsalary.DataBindings.Add("Text", dtSalary, "BasicSalary");
                empsalary.Text = dtSalary.Rows[0]["BasicSalary"].ToString();
            }
            else
            {
                empsalary.Text = ""; 
            }
            try
            {
                empdob.Text = Convert.ToDateTime(Dset.Tables[0].Rows[i][6]).ToString("yyyy-MM-dd");
            }
            catch (FormatException)
            {
                 //empdob.Text = "Invalid Date";
            }
            try
            {
                empjoindate.Text = Convert.ToDateTime(Dset.Tables[0].Rows[i][9]).ToString("yyyy-MM-dd");
            }
            catch (FormatException)
            {
                //empjoindate.Text = "Invalid Date";
            }

            btn_save.Enabled = false;
            btn_update.Enabled = true;
            btn_delete.Enabled = true;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(empid.Text))
            {
              
                MessageBox.Show("Choose ID", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string str = "Delete from tblEmployee Where EmpID='" + empid.Text + "'";
                SqlCommand mycmd = new SqlCommand(str, consql);
                mycmd.ExecuteNonQuery();
                MessageBox.Show("Finish delete information ", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillData();
                ClearText();
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                try
                {
                    string positionID = "";
                String conStr = "Select Position_ID from tblPosition Where Department ='" + empdept.Text + "'And Position = '" + "'";
                    SqlCommand cmd = new SqlCommand(conStr, consql);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        positionID = reader[0].ToString();
                    }
                    consql.Close();
                    consql.Open();


                  string str = "Update tblEmployee Set Name='" + empname.Text + "',Email='" + empemail.Text + "',Address='" + empaddress.Text + "',PhoneNumber='" + empph.Text + "',NRC='" + empnrc.Text + "',DOB='" + empdob.Text + "',Gender='" + empgender.Text + "',JoinDate='" + empjoindate.Text + "',BankAccount='" + empbankacc.Text + "'  Where EmpID='" + empid.Text + "'"; 
                  
                    SqlCommand mycmd = new SqlCommand(str, consql);
                    mycmd.ExecuteNonQuery();
                    MessageBox.Show("Finish update information ", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillData();
                    ClearText();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while saving data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        private void empph_TextChanged(object sender, EventArgs e)
        {
            bool isNumeric = !string.IsNullOrEmpty(empph.Text) && empph.Text.All(char.IsDigit) && empph.Text.Length == 11;
            if (!isNumeric)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(empph, "Please enter a valid phone number");
            }
            else
            {
                errorProvider1.Clear();
            }

        }

        private void empemail_TextChanged(object sender, EventArgs e)
        {
            bool isChar = !string.IsNullOrEmpty(empemail.Text)  && empemail.Text == @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!isChar)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(empemail, "Please enter a valid email format");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void empname_TextChanged(object sender, EventArgs e)
        {
            bool isChar = !string.IsNullOrEmpty(empname.Text) && empname.Text.All(char.IsDigit);
            if (isChar == true)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(empname, "Please enter a valid name format");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void empbankacc_TextChanged(object sender, EventArgs e)
        {
            bool isNumeric = !string.IsNullOrEmpty(empbankacc.Text) && empbankacc.Text.All(char.IsDigit) && (empbankacc.Text.Length == 11 || empbankacc.Text.Length == 12);
            if (!isNumeric)
            {
                errorProvider1.SetError(empbankacc, "Please enter a valid bank acc");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void empdept_SelectedIndexChanged(object sender, EventArgs e)
        {
            string deptID = "";
            String conStr = "Select DeptID from tblDepartment Where Department ='" + empdept.Text + "'";
            SqlCommand cmd = new SqlCommand(conStr, consql);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                deptID = reader[0].ToString();
            }
            consql.Close();
            consql.Open();
            SqlDataAdapter dp = new SqlDataAdapter("Select * From tblPosition WHERE Department='" + deptID + "'", consql);
            DataSet dposition = new DataSet();
            DataTable dposi;
            dp.Fill(dposition, "Position");
            dposi = dposition.Tables["Position"];
            empposition.DataSource = dposi;
            empposition.DisplayMember = dposi.Columns["Position"].ToString();
        }

        private void empposition_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string deptID = "";
                String conStr1 = "Select DeptID from tblDepartment Where Department ='" + empdept.Text + "'";
                SqlCommand cmd1 = new SqlCommand(conStr1, consql);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.Read())
                {
                    deptID = reader1[0].ToString();
                }
                consql.Close();
                consql.Open();
                string positionID = "";
                String conStr = "Select Position_ID from tblPosition Where Department ='" + deptID + "'AND Position = '" + empposition.Text + "'";
                SqlCommand cmd = new SqlCommand(conStr, consql);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    positionID = reader[0].ToString();
                }
                consql.Close();
                consql.Open();

                SqlDataAdapter daSalary = new SqlDataAdapter("SELECT BasicSalary FROM tblSalary WHERE Position_ID ='" + positionID + "'", consql);
                
                DataSet dsSalary = new DataSet();
                daSalary.Fill(dsSalary, "Salary");

                DataTable dSalary = dsSalary.Tables["Salary"];
                empsalary.DataBindings.Clear();
                empsalary.DataBindings.Add(new Binding("Text", dsSalary.Tables["Salary"], "BasicSalary"));

                foreach (DataRow row in dSalary.Rows)
                {
                    string basicSalary = row["BasicSalary"].ToString();
                    empsalary.Text = basicSalary;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

