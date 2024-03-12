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
        public addPosition()
        {
            InitializeComponent();
        }

        private void position_Load(object sender, EventArgs e)
        {
            connection();
            FillData();
            AutoID();
            txtid.Enabled = false;
            fillDept();
        }
        SqlConnection consql;
        DataSet Dset;
        string constr;
        DataTable dtDepartment;
        DataTable dtPosition;
        void connection()
        {
            constr = "Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=epms;Integrated Security=True";
            consql = new SqlConnection(constr);
            consql.Open();
        }
        void AutoID()
        {
            int PID = 1;

            SqlDataAdapter ad = new SqlDataAdapter("SELECT Position_ID FROM tblPosition ORDER BY Position_ID DESC", consql);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                string pID = ds.Tables[0].Rows[0]["Position_ID"].ToString();
                if (int.TryParse(pID.Substring(2), out PID))
                {
                    PID++; 
                }
            }

            txtid.Text = "P_" + PID.ToString("0000000");
             
        }
        private void fillDept()
        {
            SqlDataAdapter daDepartment = new SqlDataAdapter("Select * From tblDepartment", consql);
            DataSet dsDepartment = new DataSet();

            daDepartment.Fill(dsDepartment, "Department");
            dtDepartment = dsDepartment.Tables["Department"];
            cboDepartment.DataSource = dtDepartment;
            cboDepartment.DisplayMember = dtDepartment.Columns["Department"].ToString();
        }

        private void FillData()
        {
            
                string query = "Select * from tblPosition";
                SqlDataAdapter adapter = new SqlDataAdapter(query, consql);
                Dset = new DataSet();
                adapter.Fill(Dset, "position");
                dtPosition = Dset.Tables["position"];
                dgPosition.DataSource = dtPosition;

                dgPosition.Columns[0].HeaderText = "ID";
                dgPosition.Columns[1].HeaderText = "Position";
                dgPosition.Columns[2].HeaderText = "Department";

                dgPosition.Columns[0].Width = 130; 
                dgPosition.Columns[1].Width = 180;
                dgPosition.Columns[2].Width = 160; 
           
        }

        private void dgPosition_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow dr;
            int i;
            i = dgPosition.CurrentRow.Index;
            dr = dtPosition.Rows[i]; 
            txtid.Text = dr[0].ToString();
            txtposition.Text = dr[1].ToString();
            cboDepartment.Text = dr[2].ToString();
           
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            FillData();
        }
        private bool Validation()
        {
            if (txtid.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtid, "ID is required!");
                return false;
            }
            if (txtposition.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtposition, "Please Enter Position Name!");
                return false;
            }
            if (txtposition.Text.All(char.IsDigit))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtposition, "Please Enter Position Name!");
                return false;
            }
            string str = "select * from tblPosition where Position='" + txtposition.Text.Trim() + "'";
            SqlDataAdapter ad = new SqlDataAdapter(str, consql);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtposition, "Position already exit!");
                return false;
            }
            return true;
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearText();
            txtposition.Focus();
            errorProvider1.Clear();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoID();
            this.ActiveControl = txtposition;
            errorProvider1.Clear();
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                string depID = "";
                String conStr = "Select DeptID from tblDepartment Where Department ='" + cboDepartment.Text + "'";
                SqlCommand cmd = new SqlCommand(conStr, consql);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    depID = reader[0].ToString();
                }

                consql.Close();
                consql.Open();
                string strInsert = "Insert Into tblPosition Values ('" + txtid.Text + "','" + txtposition.Text + "','" + depID + "')";
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
                if (String.IsNullOrEmpty(txtposition.Text))
                {
                    MessageBox.Show("Position can't be empty", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else
                {
                    try
                    {
                        string str = "Update tblPosition Set Position=@position,Department=@department WHERE Position_ID =@id";
                        SqlCommand SqlComm = new SqlCommand(str, consql);
                        SqlComm.Parameters.AddWithValue("@id", txtid.Text);
                        SqlComm.Parameters.AddWithValue("@position", txtposition.Text);
                        SqlComm.Parameters.AddWithValue("@department", cboDepartment.Text);
                        SqlComm.ExecuteNonQuery();

                        MessageBox.Show("Finish Update information", "Updated Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillData();
                        ClearText();
                        errorProvider1.Clear();
                    }
                  catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
             }
        }

        void ClearText()
        {
            txtid.Text = "";
            txtposition.Text = "";
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

                        string str = "Delete from tblPosition WHERE Position_ID =@id";
                        SqlCommand SqlComm = new SqlCommand(str, consql);
                        SqlComm.Parameters.AddWithValue("@id", txtid.Text);
                        SqlComm.ExecuteNonQuery();

                        MessageBox.Show("Successfully Deleted", "Process Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
    }
}
