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
         bool close = true;
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
            addPosition addPosition = new addPosition();
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
            SqlDataAdapter daPosition = new SqlDataAdapter("Select * From tblPosition", consql);
            DataSet dsPosition = new DataSet();
            DataTable dt;
            daPosition.Fill(dsPosition, "Positions");
            dt = dsPosition.Tables["Positions"];
            empdept.DataSource = dt;
            empposition.DataSource = dt;
            empdept.DisplayMember = dt.Columns["Department"].ToString();
            empposition.DisplayMember = dt.Columns["Position"].ToString();

            empsalary.DataBindings.Clear(); // Clear Data For data binding
            //Data binding for Position field.
            empsalary.DataBindings.Add(new Binding("Text", dsPosition.Tables["Positions"], "BasicSalary"));

            string query = "Select Empid,PositionID,Name,Email,Address,PhoneNumber,NRC,DOB,Gender,JoinDate,BankAccount From tblEmployee";
            SqlDataAdapter adapter = new SqlDataAdapter(query, consql);
            Dset = new DataSet();
            adapter.Fill(Dset);
            dataGridView1.DataSource = Dset.Tables[0];

            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "PositioinID";
            dataGridView1.Columns[1].Visible = false;//Hide Position ID
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Email";
            dataGridView1.Columns[4].HeaderText = "Address";
            dataGridView1.Columns[5].HeaderText = "Phone";
            dataGridView1.Columns[6].HeaderText = "NRC";
            dataGridView1.Columns[7].HeaderText = "DOB";
            dataGridView1.Columns[8].HeaderText = "Gender";
            dataGridView1.Columns[9].HeaderText = "Join Date";
            dataGridView1.Columns[10].HeaderText = "Bank Account";

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].Width = 50;
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[10].Width = 100;

        }
       private void addEmployee_Load(object sender, EventArgs e)
        {
            connection();
            AutoID();
            string emp_dept = "SELECT DISTINCT Department FROM tblPosition";
            fn.fillcombox(emp_dept, empdept);
            FillData();

            empsalary.Enabled = false;
            empid.Enabled = false;
            this.ActiveControl = empname;
        }
        
        void connection()
        {
            str = "Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=EmpPayrollSystem;User ID=Sa;Password=p@ssword";
            consql = new SqlConnection(str);
            consql.Open();
        }


        private void btn_new_Click(object sender, EventArgs e)
        {
            ClearText();
            AutoID();
            this.ActiveControl = empname;
        }
        // Auto ID for Employee
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
                    //To Get ID From tblPosition
                    string positionID = "";
                    String conStr = "Select PositionID from tblPosition Where Department ='" + empdept.Text + "'And Position = '" + empposition.Text + "' And BasicSalary ='" + empsalary.Text + "'";
                    SqlCommand cmd = new SqlCommand(conStr, consql);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        positionID = reader[0].ToString();
                    }
                    consql.Close();
                    consql.Open();

                    //For Save Data to tblEmployee
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
                
                //ErrorProvider.SetError(empid,"ID Required");
                 errorProvider1.Clear();
                 errorProvider1.SetError(empid, "ID Required");
                //MessageBox.Show("ID Field Required", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(empname.Text))
            {
                  errorProvider1.Clear();
                  errorProvider1.SetError(empname, "Employee Name Required");
                //MessageBox.Show("Name Field Required", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(empemail.Text))
            {
                  errorProvider1.Clear();
                 errorProvider1.SetError(empemail, "Email Required");
               // MessageBox.Show("Email Field Required", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(empaddress.Text))
            {
                 errorProvider1.Clear();
                 errorProvider1.SetError(empaddress, "Address Required");
                //MessageBox.Show("Address Field Required", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(empph.Text))
            {
                  errorProvider1.Clear();
                  errorProvider1.SetError(empph, "Ph No Required");
                //MessageBox.Show("Ph No Field Required", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(empdob.Text))
            {
                 errorProvider1.Clear();
                 errorProvider1.SetError(empdob, "DOB Required");
                //MessageBox.Show("DOB Field Required", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(empnrc.Text))
            {
                 errorProvider1.Clear();
                 errorProvider1.SetError(empnrc, "NRC Required");
                //MessageBox.Show("NRC Field Required", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(empbankacc.Text))
            {
                   errorProvider1.Clear();
                   errorProvider1.SetError(empbankacc, "Address Required");
                //MessageBox.Show("Bank Account Field Required", "Delete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            empemail.Text = Dset.Tables[0].Rows[i][3].ToString();
            empaddress.Text = Dset.Tables[0].Rows[i][4].ToString();
            empph.Text = Dset.Tables[0].Rows[i][5].ToString();
            empnrc.Text = Dset.Tables[0].Rows[i][6].ToString();
            empgender.Text = Dset.Tables[0].Rows[i][8].ToString();
            empbankacc.Text = Dset.Tables[0].Rows[i][10].ToString();

          //  empdept.Text = Dset.Tables[0].Rows[i][11].ToString();
           // empposition.Text = Dset.Tables[0].Rows[i][12].ToString();
          //  empsalary.Text = Dset.Tables[0].Rows[i][13].ToString();

            // Following code are to fill  Department Position and salary in Edit Form
            string PID = Dset.Tables[0].Rows[i][1].ToString();//TO Get Position ID

            //TO Get All Positioion and Department data to display
            SqlDataAdapter daPositions = new SqlDataAdapter("Select * From tblPosition", consql);
            DataSet dsPositions = new DataSet();
            DataTable dts;
            daPositions.Fill(dsPositions, "Positions");
            dts = dsPositions.Tables["Positions"];
            empdept.DataSource = dts;
            empposition.DataSource = dts;
            empdept.DisplayMember = dts.Columns["Department"].ToString();
            empposition.DisplayMember = dts.Columns["Position"].ToString();
            empsalary.DataBindings.Clear(); // Clear Data For data binding
            //Data binding for Position field.
            empsalary.DataBindings.Add(new Binding("Text", dsPositions.Tables["Positions"], "BasicSalary"));

            String conStr = "Select Department,Position,BasicSalary From tblPosition Where PositionID = '" + PID + "'";
            SqlCommand cmd = new SqlCommand(conStr, consql);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {

                empdept.Text = reader[0].ToString();
                empposition.Text = reader[1].ToString();
                empsalary.Text = reader[2].ToString();
            }
            consql.Close();
            consql.Open();




            try
            {
                empdob.Text = Convert.ToDateTime(Dset.Tables[0].Rows[i][7]).ToString("yyyy-MM-dd");
            }
            catch (FormatException)
            {
                // empdob.Text = "Invalid Date";
            }

            try
            {
                empjoindate.Text = Convert.ToDateTime(Dset.Tables[0].Rows[i][9]).ToString("yyyy-MM-dd");
            }
            catch (FormatException)
            {
                //  empjoindate.Text = "Invalid Date";
            }

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(empid.Text))
            {
                //   errorProvider1.Clear();
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
                    //To Get ID From tblPosition
                    string positionID = "";
                    String conStr = "Select PositionID from tblPosition Where Department ='" + empdept.Text + "'And Position = '" + empposition.Text + "' And BasicSalary ='" + empsalary.Text + "'";
                    SqlCommand cmd = new SqlCommand(conStr, consql);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        positionID = reader[0].ToString();
                    }
                    consql.Close();
                    consql.Open();


                    
                    string str = "Update tblEmployee Set PositionID='" + positionID + "',Name='" + empname.Text + "',Email='" + empemail.Text + "',Address='" + empaddress.Text + "',PhoneNumber='" + empph.Text + "',NRC='" + empnrc.Text + "',DOB='" + empdob.Text + "',Gender='" + empgender.Text + "',JoinDate='" + empjoindate.Text + "',BankAccount='" + empbankacc.Text + "'  Where EmpID='" + empid.Text + "'";
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

        private void addEmployee_FormClosing(object sender, FormClosingEventArgs e)
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

        private void admindashboard_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void addposition_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

