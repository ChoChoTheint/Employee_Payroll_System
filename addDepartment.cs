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
    public partial class addDepartment : Form
    {
        public addDepartment()
        {
            InitializeComponent();
        }

        private void Department_Load(object sender, EventArgs e)
        {
            connection();
            FillData();
            AutoID();
            txtid.Enabled = false;
        }
        SqlConnection consql;
        DataSet Dset;
        string constr;
        DataTable dtDepartment;
        void connection()
        {
            constr = "Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=epms;Integrated Security=True";
            consql = new SqlConnection(constr);
            consql.Open();
        }
        private bool Validation()
        {
            if (txtid.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtid, "ID is required!");
                return false;
            }
           
            if (txtdept.Text == "" || txtdept.Text.All(char.IsDigit))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtdept, "Please Enter Department Name!");
                return false;
            }

            string str = "select * from tblDepartment where Department='" + txtdept.Text.Trim() + "'";
            SqlDataAdapter ad = new SqlDataAdapter(str, consql);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtdept, "Department already exit!");
                return false;
            }
            return true;
        }

        void AutoID()
        {
            int PID = 1;
            SqlDataAdapter ad = new SqlDataAdapter("SELECT DeptID FROM tblDepartment ORDER BY DeptID DESC", consql);
            DataSet ds = new DataSet();
            ad.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string pID = ds.Tables[0].Rows[0]["DeptID"].ToString();
                if (int.TryParse(pID.Substring(2), out PID))
                {
                    PID++;
                }
            }

            txtid.Text = "D_" + PID.ToString("0000000");
        }

        void ClearText()
        {
            txtid.Text = "";
            txtdept.Text = "";
        }

        private void FillData()
        {
            
                string query = "Select * from tblDepartment";
                SqlDataAdapter adapter = new SqlDataAdapter(query, consql);
                Dset = new DataSet();
                adapter.Fill(Dset, "department");
                dtDepartment = Dset.Tables["department"];
                dgDepartment.DataSource = dtDepartment;

                dgDepartment.Columns[0].HeaderText = "ID";
                dgDepartment.Columns[1].HeaderText = "Position";
               
                dgDepartment.Columns[0].Width = 200;
                dgDepartment.Columns[1].Width = 270;
           
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoID();
            txtdept.Clear();
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            errorProvider1.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                string strInsert = "Insert Into tblDepartment Values ('" + txtid.Text + "','" + txtdept.Text + "')";
                SqlCommand mycmd = new SqlCommand(strInsert, consql);
                mycmd.ExecuteNonQuery();
                MessageBox.Show("Finish save information", "Save Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillData();
                ClearText();
                errorProvider1.Clear();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult dresult = MessageBox.Show("Are you really want to Update data?", "Comfirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dresult == DialogResult.OK)
            {
                if (String.IsNullOrEmpty(txtdept.Text))
                {
                    MessageBox.Show("Department name can't be empty", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else
                {
                    try
                    {
                        string str = "Update tblDepartment Set Department=@department WHERE DeptID =@id";
                        SqlCommand SqlComm = new SqlCommand(str, consql);
                        SqlComm.Parameters.AddWithValue("@id", txtid.Text);
                        SqlComm.Parameters.AddWithValue("@department", txtdept.Text);
                        SqlComm.ExecuteNonQuery();

                        MessageBox.Show("Finish Update information", "Updated Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillData();
                        ClearText();
                        errorProvider1.Clear();
                        btnUpdate.Enabled = false;
                        btnDelete.Enabled = false;
                        btnSave.Enabled = true;
                        AutoID();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dresult = MessageBox.Show("Are you really want to Delete data?", "Comfirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dresult == DialogResult.OK)
            {

                if (String.IsNullOrEmpty(txtid.Text))
                {
                    MessageBox.Show("Please select an ID.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else
                {
                    try
                    {

                        string str = "Delete from tblDepartment WHERE deptID =@id";
                        SqlCommand SqlComm = new SqlCommand(str, consql);
                        SqlComm.Parameters.AddWithValue("@id", txtid.Text);
                        SqlComm.ExecuteNonQuery();

                        MessageBox.Show("Successfully Deleted", "Process Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        btnUpdate.Enabled = false;
                        btnDelete.Enabled = false;
                        btnSave.Enabled = true;
                        AutoID();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    FillData();
                    ClearText();
                }

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearText();
            errorProvider1.Clear();
        }

        private void dgDepartment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataRow dr;
                int i;
                i = dgDepartment.CurrentRow.Index;
                dr = dtDepartment.Rows[i]; // Insert i value to dr


                txtid.Text = dr[0].ToString();
                txtdept.Text = dr[1].ToString();

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                FillData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }



    }
}
