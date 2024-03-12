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
    public partial class addSalary : Form
    {
        function fn = new function();
        SqlConnection consql;
        string str;
        DataSet Dset;
        DataTable dtArticle;
        public addSalary()
        {
            InitializeComponent();
        }
        void connection()
        {
            str = "Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=epms;Integrated Security=True";
            consql = new SqlConnection(str);
            consql.Open();
        }
        private void addSalary_Load(object sender, EventArgs e)
        {
                connection();
            
                string dept = "SELECT * FROM tblDepartment";
                fn.fillcombox(dept, combodept);

                string position = "SELECT * FROM tblPosition";
                fn.fillcombox(position, comboposition);
                txtbasicsalary.Text = "";
                txtid.Enabled = false;
                FillData();
                AutoID();
        }
        
        void AutoID()
        {
            try
            {
                txtid.Clear();
                int PID = 1;
                SqlDataAdapter ad = new SqlDataAdapter("SELECT SalaryID FROM tblSalary ORDER BY SalaryID DESC", consql);
                DataSet ds = new DataSet();
                ad.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string pID = ds.Tables[0].Rows[0]["SalaryID"].ToString();
                    if (int.TryParse(pID.Substring(2), out PID))
                    {
                        PID++;      
                    }
                }

                txtid.Text = "S_" + PID.ToString("0000000");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void FillData()
        {
           
            SqlDataAdapter da = new SqlDataAdapter("Select * From tblDepartment", consql);
            DataSet ds = new DataSet();
            DataTable dt;
            da.Fill(ds, "Department");
            dt = ds.Tables["Department"];
            combodept.DataSource = dt;
            combodept.DisplayMember = dt.Columns["Department"].ToString();
            combodept.ValueMember = dt.Columns["DeptID"].ToString();
            

            SqlDataAdapter dp = new SqlDataAdapter("Select * From tblPosition", consql);
            DataSet dposition = new DataSet();
            DataTable dposi;
            dp.Fill(dposition, "Position");
            dposi = dposition.Tables["Position"];
            comboposition.DataSource = dposi;
            comboposition.DisplayMember = dposi.Columns["Position"].ToString();

            string query = "SELECT dbo.tblSalary.SalaryID, dbo.tblDepartment.Department, dbo.tblPosition.Position, dbo.tblSalary.BasicSalary FROM dbo.tblSalary INNER JOIN dbo.tblDepartment ON dbo.tblSalary.DeptID = dbo.tblDepartment.DeptID INNER JOIN dbo.tblPosition ON dbo.tblSalary.Position_ID = dbo.tblPosition.Position_ID";
            SqlDataAdapter adapter = new SqlDataAdapter(query, consql);
            Dset = new DataSet();
            adapter.Fill(Dset, "salary");
            dtArticle = Dset.Tables["salary"];
            dataviewsalary.DataSource = dtArticle;


            dataviewsalary.Columns[0].HeaderText = "ID";
            dataviewsalary.Columns[1].HeaderText = "Department";
            dataviewsalary.Columns[2].HeaderText = "Position";
            dataviewsalary.Columns[3].HeaderText = "Basic Salary";

            dataviewsalary.Columns[0].Width = 150;
            dataviewsalary.Columns[1].Width = 200;
            dataviewsalary.Columns[2].Width = 300;
            dataviewsalary.Columns[3].Width = 200;
            
           
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            ClearText();
            AutoID();
            this.ActiveControl = txtbasicsalary;
            errorProvider1.Clear();
            btnsave.Enabled = true;
            btnupdate.Enabled = false;
            btndelete.Enabled = false;
           
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                string positionID = "";
                string deptID = "";

                using (SqlConnection consql = new SqlConnection("Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=epms;Integrated Security=True"))
                {
                    consql.Open();

                    string positionQuery = "SELECT Position_ID FROM tblPosition WHERE Position = @position";
                    using (SqlCommand positionCmd = new SqlCommand(positionQuery, consql))
                    {
                        positionCmd.Parameters.AddWithValue("@position", comboposition.Text);
                        using (SqlDataReader reader = positionCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                positionID = reader["Position_ID"].ToString();
                            }
                        }
                    }
                    string deptQuery = "SELECT DeptID FROM tblDepartment WHERE Department = @department";
                    using (SqlCommand deptCmd = new SqlCommand(deptQuery, consql))
                    {
                        deptCmd.Parameters.AddWithValue("@department", combodept.Text);
                        using (SqlDataReader reader = deptCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                deptID = reader["DeptID"].ToString();
                            }
                        }
                    }
                    string strInsert = "INSERT INTO tblSalary (SalaryID, Position_ID, DeptID, BasicSalary) VALUES (@salaryID, @positionID, @deptID, @basicSalary)";
                    using (SqlCommand insertCmd = new SqlCommand(strInsert, consql))
                    {
                        insertCmd.Parameters.AddWithValue("@salaryID", txtid.Text);
                        insertCmd.Parameters.AddWithValue("@positionID", positionID);
                        insertCmd.Parameters.AddWithValue("@deptID", deptID);
                        insertCmd.Parameters.AddWithValue("@basicSalary", txtbasicsalary.Text);

                        insertCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Finish save information", "Save Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillData();
                AutoID();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text))
            {
                MessageBox.Show("Choose ID", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string positionID = "";
                string deptID = "";

                using (SqlConnection consql = new SqlConnection("Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=epms;Integrated Security=True"))
                {
                    consql.Open();
                    string positionQuery = "SELECT Position_ID FROM tblPosition WHERE Position = @position";
                    using (SqlCommand positionCmd = new SqlCommand(positionQuery, consql))
                    {
                        positionCmd.Parameters.AddWithValue("@position", comboposition.Text);
                        using (SqlDataReader reader = positionCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                positionID = reader["Position_ID"].ToString();
                            }
                        }
                    }
                    string deptQuery = "SELECT DeptID FROM tblDepartment WHERE Department = @department";
                    using (SqlCommand deptCmd = new SqlCommand(deptQuery, consql))
                    {
                        deptCmd.Parameters.AddWithValue("@department", combodept.Text);
                        using (SqlDataReader reader = deptCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                deptID = reader["DeptID"].ToString();
                            }
                        }
                    }
                    string updateQuery = "UPDATE tblSalary SET Position_ID = @positionID, DeptID = @deptID, BasicSalary = @basicSalary WHERE SalaryID = @salaryID";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, consql))
                    {
                        updateCmd.Parameters.AddWithValue("@positionID", positionID);
                        updateCmd.Parameters.AddWithValue("@deptID", deptID);
                        updateCmd.Parameters.AddWithValue("@basicSalary", txtbasicsalary.Text);
                        updateCmd.Parameters.AddWithValue("@salaryID", txtid.Text);

                        updateCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Finish update information", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillData();
                    ClearText();
                }
            }
        }


        private void btndelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text))
            {
                MessageBox.Show("Choose ID", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string str = "Delete from tblSalary Where SalaryID='" + txtid.Text + "'";
                SqlCommand mycmd = new SqlCommand(str, consql);
                mycmd.ExecuteNonQuery();
                MessageBox.Show("Finish delete information ", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillData();
                ClearText();
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtid.Text = "";
            combodept.Text = "";
            comboposition.Text = "";
            txtbasicsalary.Text = "";
        }

        private void dataviewsalary_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow dr;
            int i;
            i = dataviewsalary.CurrentRow.Index;
            dr = dtArticle.Rows[i];
            txtid.Text = dr[0].ToString();
            combodept.Text = dr[1].ToString();
            comboposition.Text = dr[2].ToString();
            txtbasicsalary.Text = dr[3].ToString();

            btnsave.Enabled = false;
            btnupdate.Enabled = true;
            btndelete.Enabled = true;

        }
        void ClearText()
        {
            combodept.Text = "";
            comboposition.Text = "";
            txtbasicsalary.Text = "";
        }
        private bool Validation()
        {
            if (txtid.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtid, "ID is required!");
                return false;
            }
            if (combodept.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(combodept, "Please Choose Department!");
                return false;
            }
            if (comboposition.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboposition, "Please Choose Position");
                return false;
            }
            if (txtbasicsalary.Text == "" || !(txtbasicsalary.Text.All(char.IsDigit)))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtbasicsalary, "Please Enter Salary!");
                return false;
            }
            return true;
        }

        private void txtbasicsalary_TextChanged(object sender, EventArgs e)
        {
            if (!(txtbasicsalary.Text.All(char.IsDigit)))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtbasicsalary, "Invalid Salary Format");
            }
            else
            {
                errorProvider1.Clear();
            }
            
        }

        private void combodept_SelectedIndexChanged(object sender, EventArgs e)
        {
            string deptID = "";
            string deptQuery = "SELECT DeptID FROM tblDepartment WHERE Department = @department";
            using (SqlCommand deptCmd = new SqlCommand(deptQuery, consql))
            {
                deptCmd.Parameters.AddWithValue("@department", combodept.Text);
                using (SqlDataReader reader = deptCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        deptID = reader["DeptID"].ToString();
                    }
                }
            }
            SqlDataAdapter dp = new SqlDataAdapter("Select * From tblPosition WHERE Department='" + deptID + "'", consql);
            DataSet dposition = new DataSet();
            DataTable dposi;
            dp.Fill(dposition, "Position");
            dposi = dposition.Tables["Position"];
            comboposition.DataSource = dposi;
            comboposition.DisplayMember = dposi.Columns["Position"].ToString();
        }

        private void comboposition_SelectedIndexChanged(object sender, EventArgs e)
        {
            string positionID = "";
            string positionQuery = "SELECT Position_ID FROM tblPosition WHERE Position = @position";
            using (SqlCommand positionCmd = new SqlCommand(positionQuery, consql))
            {
                positionCmd.Parameters.AddWithValue("@position", comboposition.Text);
                using (SqlDataReader reader = positionCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        positionID = reader["Position_ID"].ToString();
                    }
                }
            }
            try
            {

                string salQuery = "SELECT BasicSalary FROM tblSalary WHERE Position_ID = @position";
                using (SqlCommand salaryCmd = new SqlCommand(salQuery, consql))
                {
                    salaryCmd.Parameters.AddWithValue("@position", positionID);
                    using (SqlDataReader reader = salaryCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtbasicsalary.Text = reader["BasicSalary"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
