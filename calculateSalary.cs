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
    public partial class calculateSalary : Form
    {
        bool close = true;
        SqlConnection consql;
        string str;
        DataSet Dset;
        public calculateSalary()
        {
            InitializeComponent();
        }
        void connection()
        {
            str = "Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=EmpPayrollSystem;User ID=Sa;Password=p@ssword";
            consql = new SqlConnection(str);
            consql.Open();
        }
        private void admindashboard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm dashboard = new MainForm();
            this.Hide();
            dashboard.Show();
        }

        private void addposition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            addPosition addPosition = new addPosition();
            this.Hide();
            addPosition.Show();
        }

        private void addemployee_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void logOut_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login login = new login();
            this.Hide();
            login.Show();
        }

        private void calculateSalary_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FillData()
        {
                SqlDataAdapter daEmp = new SqlDataAdapter("Select * From tblEmployee", consql);
                DataSet dsEmp = new DataSet();
                DataTable dt;
            
                daEmp.Fill(dsEmp, "Employee");
                dt = dsEmp.Tables["Employee"];
                empid.DataSource = dt;
                empid.DisplayMember = dt.Columns["EmpID"].ToString();

                string query = "Select * From tblEmployee";
                SqlDataAdapter adapter = new SqlDataAdapter(query, consql);
                Dset = new DataSet();
                adapter.Fill(Dset);
                empCalculatedg.DataSource = Dset.Tables[0];

                empCalculatedg.Columns[0].HeaderText = "ID";
                empCalculatedg.Columns[1].HeaderText = "PositioinID";
                empCalculatedg.Columns[2].HeaderText = "Name";
                empCalculatedg.Columns[3].HeaderText = "Address";
                empCalculatedg.Columns[4].HeaderText = "Phone No";
                empCalculatedg.Columns[5].HeaderText = "NRC";
                empCalculatedg.Columns[6].HeaderText = "DOB";
                empCalculatedg.Columns[7].HeaderText = "Gender";
                empCalculatedg.Columns[8].HeaderText = "Email";
                empCalculatedg.Columns[9].HeaderText = "Remark";
                empCalculatedg.Columns[10].HeaderText = "Join Date";

                empCalculatedg.Columns[0].Width = 100;
                empCalculatedg.Columns[1].Width = 100;
                empCalculatedg.Columns[2].Width = 100;
                empCalculatedg.Columns[3].Width = 100;
                empCalculatedg.Columns[4].Width = 100;
                empCalculatedg.Columns[5].Width = 100;
                empCalculatedg.Columns[6].Width = 100;
                empCalculatedg.Columns[7].Width = 50;
                empCalculatedg.Columns[8].Width = 100;
                empCalculatedg.Columns[9].Width = 100;
                empCalculatedg.Columns[10].Width = 100;


          
          /*  SqlDataAdapter daEmp = new SqlDataAdapter("SELECT * FROM tblEmployee", consql);
            DataSet dsEmp = new DataSet();

            try
            {
                consql.Open();
                daEmp.Fill(dsEmp, "Employee");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                consql.Close();
            }

            // Bind the employee ID ComboBox
            DataTable dt = dsEmp.Tables["Employee"];
            empid.DataSource = dt;
            empid.DisplayMember = "EmpID";
           */
        }

        private void calculateSalary_Load(object sender, EventArgs e)
        {
            connection();
            FillData();
            LoadEmployeeData();
            Clear();
            empsalary.Enabled = false;
            empbankacc.Enabled = false;
            empjoindate.Enabled = false;
            mealallowance.Text = "30000";
            tranallowance.Text = "30000";
            mealallowance.Enabled = false;
            tranallowance.Enabled = false;
        }
        private void Clear()
        {
            empid.Text = "";
            empname.Text = "";
            empsalary.Text = "";
            empbankacc.Text = "";
            paymentdate.Text = "";
            empjoindate.Text = "";
            attendance.Text = "";
            lateday.Text = "";
            leaveday.Text = "";
            mealallowance.Text = "30000";
            tranallowance.Text = "30000";
           // tranallowance.Text = "";
           // mealallowance.Text = "";
            attendance.Focus();
           
        }
        private void clearBtn_Click(object sender, EventArgs e)
        {
            Clear();
            errorProvider1.Clear();
        }
        private void btncalculate_Click(object sender, EventArgs e)
        {
            if (empid.Text != "")
            {
                string empJoinDateText = empjoindate.Text;
                DateTime empJoinDate = DateTime.Parse(empJoinDateText);
                TimeSpan difference = DateTime.Today - empJoinDate;
             //   int months = (DateTime.Today.Year - empJoinDate.Year) * 12 + DateTime.Today.Month - empJoinDate.Month;
                int months = difference.Days / 30; 
             //   MessageBox.Show("months " + months);
                int daysDifference = difference.Days;
                if (daysDifference <= 30 && months == 0)
                {
                    MessageBox.Show("Join Date is " + daysDifference + "\n Please Enter " + daysDifference + " In Attendance!");
                    MessageBox.Show("This months salary must be get");
                }

                DateTime currentDateOnly = DateTime.Today;
                int monthsDifference = ((currentDateOnly.Year - empJoinDate.Year) * 12) + currentDateOnly.Month - empJoinDate.Month;


                int leave = 0;
                int late = 0;

                decimal mealAllowance = 0;
                decimal tranAllowance = 0;
                decimal salebonus = 0;
                //   decimal paymentamount = 0;
                decimal totalpayment = 0;
                string value = "";

                bool isCheckedNormal = normal.Checked;
                bool isCheckedGood = good.Checked;

                if (monthsDifference >= 6)
                {
                    //start  for bonus 
                    if (isCheckedNormal)
                    {
                        value = normal.Text;
                    }
                    else if (isCheckedGood)
                    {
                        value = good.Text;
                    }
                    else
                    {
                        value = excellence.Text;
                    }
                    // end  for bonus 

                    if (Validation())
                    {
                        if (leaveday.Text != "")
                        {
                            leave = Convert.ToInt16(leaveday.Text);
                        }
                        if (lateday.Text != "")
                        {
                            late = Convert.ToInt16(lateday.Text);
                        }
                        if (mealallowance.Text != "")
                        {
                            mealAllowance = Convert.ToDecimal(mealallowance.Text);
                        }
                        if (tranallowance.Text != "")
                        {
                            tranAllowance = Convert.ToDecimal(tranallowance.Text);
                        }


                        decimal salaryValue = Convert.ToDecimal(empsalary.Text);
                        int att = Convert.ToInt16(attendance.Text);
                        decimal onedaysalary = salaryValue / 30;

                        if (salaryValue < 500000)
                        {

                            //  start for sale person
                            /*    if (sale == 0)
                                {
                                    totalpayment = (onedaysalary * att) - (leave * onedaysalary) + mealAllowance + tranAllowance;
                                }
                             * */
                            mealAllowance = 30000;
                            tranAllowance = 30000;

                            if (late <= 2)
                            {

                                totalpayment = (onedaysalary * att) - (leave * onedaysalary) + mealAllowance + tranAllowance;
                                if (value == "Normal")
                                {
                                    totalpayment += 30000;
                                }
                                else if (value == "Good")
                                {
                                    totalpayment += 50000;
                                }
                                else
                                {
                                    totalpayment += 70000;
                                }

                            }

                            if (late == 3)
                            {
                                decimal totalsalary = (onedaysalary * att) - (leave * onedaysalary) - onedaysalary;
                                totalpayment = totalsalary + mealAllowance + tranAllowance;
                                if (value == "Normal")
                                {
                                    totalpayment += 30000;
                                }
                                else if (value == "Good")
                                {
                                    totalpayment += 50000;
                                }
                                else
                                {
                                    totalpayment += 70000;
                                }
                            }
                            else if (late >= 3)
                            {
                                decimal totalsalary = (onedaysalary * att) - (leave * onedaysalary) - (2 * onedaysalary);
                                totalpayment = totalsalary + mealAllowance + tranAllowance;
                                if (value == "Normal")
                                {
                                    totalpayment += 30000; ;
                                }
                                else if (value == "Good")
                                {
                                    totalpayment += 50000;
                                }
                                else
                                {
                                    totalpayment += 70000;
                                }
                            }
                        }

                        else if (salaryValue > 500000)
                        {
                            mealAllowance = 0;
                            tranAllowance = 0;

                            if (late <= 2)
                            {
                                totalpayment = (onedaysalary * att) - (leave * onedaysalary);
                                //   MessageBox.Show("hi");
                                if (value == "Normal")
                                {
                                    totalpayment += 30000;
                                }
                                else if (value == "Good")
                                {
                                    totalpayment += 50000;
                                }
                                else
                                {
                                    totalpayment += 70000;
                                }
                            }
                            if (late == 3)
                            {
                                totalpayment = (onedaysalary * att) - (leave * onedaysalary) - onedaysalary;
                                if (value == "Normal")
                                {
                                    totalpayment += 30000;
                                }
                                else if (value == "Good")
                                {
                                    totalpayment += 50000;
                                }
                                else
                                {
                                    totalpayment += 70000;
                                }
                            }
                            else if (late >= 3)
                            {
                                totalpayment = (onedaysalary * att) - (leave * onedaysalary) - (2 * onedaysalary);
                                if (value == "Normal")
                                {
                                    totalpayment += 30000;
                                }
                                else if (value == "Good")
                                {
                                    totalpayment += 50000;
                                }
                                else
                                {
                                    totalpayment += 70000;
                                }
                            }
                        }
                        empsalary.Text = salaryValue.ToString();
                    }

                    //sale bonus start
                    int sale = 0;
                    if (noofsale.Text != "")
                    {
                        sale = Convert.ToInt16(noofsale.Text);
                      //  MessageBox.Show(sale.ToString());
                        if (sale >= 1 && sale <= 10)
                        {
                            salebonus = 20000;
                            totalpayment += salebonus;
                        }
                        else if (sale >= 11 && sale <= 20)
                        {
                            salebonus = 40000;
                            totalpayment += salebonus;
                        }
                        else
                        {
                            salebonus = 60000;
                            totalpayment += salebonus;
                        }
                    }
                    //sale bonus end
                }
                else
                {
                    if (Validation())
                    {

                        decimal salaryValue = Convert.ToDecimal(empsalary.Text);
                        int att = Convert.ToInt16(attendance.Text);
                        if (leaveday.Text != "")
                        {
                            leave = Convert.ToInt16(leaveday.Text);
                        }
                        if (lateday.Text != "")
                        {
                            late = Convert.ToInt16(lateday.Text);
                        }


                        decimal onedaysalary = salaryValue / 30;

                        if (salaryValue < 500000)
                        {
                            if (late <= 2)
                            {
                                totalpayment = (onedaysalary * att) - (leave * onedaysalary);
                            }

                            if (late == 3)
                            {
                                totalpayment = (onedaysalary * att) - (leave * onedaysalary) - onedaysalary;

                            }
                            else if (late >= 3 && late <= 6)
                            {
                                totalpayment = (onedaysalary * att) - (leave * onedaysalary) - (2 * onedaysalary);
                            }
                        }

                        else if (salaryValue > 500000)
                        {
                            if (late <= 2)
                            {
                                totalpayment = (onedaysalary * att) - (leave * onedaysalary);
                            }
                            if (late == 3)
                            {
                                totalpayment = (onedaysalary * att) - (leave * onedaysalary) - onedaysalary;
                            }
                            else if (late >= 3 && late <= 6)
                            {
                                totalpayment = (onedaysalary * att) - (leave * onedaysalary) - (2 * onedaysalary);
                            }
                        }

                        empsalary.Text = salaryValue.ToString();
                    }

                }

                string paymentamount = totalpayment.ToString("0.");
                MessageBox.Show("Payment Amount: " + paymentamount + "\n sale bonus " + salebonus + "\n Attendance " + attendance.Text + "\n Late day " + lateday.Text + "\n Leave day " + leaveday.Text);


                SavePaymentInformation(paymentamount, empid.Text);
                SaveAttendanceInformation(attendance.Text, lateday.Text, leaveday.Text, empid.Text);
                SaveAllowanceInformation(mealAllowance, tranAllowance, empid.Text);

               // int nosale = 0;
                string remark = "";
                if (normal.Checked == true)
                {
                    remark = "normal";
                }
                if (good.Checked == true)
                {
                    remark = "good";
                }
                if (excellence.Checked == true)
                {
                    remark = "Excellence";
                }
                saveEmployeeRemark(remark, empid.Text);
                //saveNoOfSale(nosale, empid.Text);
                FillData();
            }
            else
            {
                MessageBox.Show("Choose ID");
            }
        }
            

        private void saveEmployeeRemark(string remark, string empID)
        { 
         try{
                string noOfSale = noofsale.Text;
                string strInsert = "Update tblEmployee Set Remark = @remark,NoOfSale = @sale Where EmpID = @empID";
                SqlCommand mycmd = new SqlCommand(strInsert, consql);
                mycmd.Parameters.AddWithValue("@remark", remark);
                mycmd.Parameters.AddWithValue("@empID", empID);
                mycmd.Parameters.AddWithValue("@sale", noOfSale);               
                mycmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        // MessageBox.Show(remark);
        }
        private void saveNoOfSale(int nosale, string empID)
        {
           
            try
            {
                  //  int nosale = 0;
                    using (SqlConnection consql = new SqlConnection("Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=EmpPayrollSystem;User ID=Sa;Password=p@ssword"))
                    {
                        consql.Open();

                        string strInsert = "UPDATE tblEmployee SET NoOfSale = @nosale WHERE EmpID = @empID";
                        using (SqlCommand mycmd = new SqlCommand(strInsert, consql))
                        {
                            mycmd.Parameters.AddWithValue("@nosale", nosale);
                            mycmd.Parameters.AddWithValue("@empID", empID);
                            mycmd.ExecuteNonQuery();
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
          //  MessageBox.Show(nosale.ToString());
        }

        private void SaveAllowanceInformation(decimal mealallowance, decimal tranallowance, string empID)
        {

            try
           {


                SqlDataAdapter ad = new SqlDataAdapter("SELECT AllowanceID FROM tblAllowance", consql);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                int allowanceID = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    allowanceID = ds.Tables[0].Rows.Count + 1;
                }
                else
                {
                    allowanceID = 1;
                }

                string strInsert = "INSERT INTO tblAllowance (AllowanceID, EmpID, TransporationAllowance, MealAllowance) VALUES (@AllowanceID, @EmpID, @TransporationAllowance, @MealAllowance)";
                SqlCommand mycmd = new SqlCommand(strInsert, consql);

                // Ensure that you are using the values, not the controls directly
                mycmd.Parameters.AddWithValue("@AllowanceID", allowanceID);
                mycmd.Parameters.AddWithValue("@EmpID", empID);
                mycmd.Parameters.AddWithValue("@TransporationAllowance", tranallowance);  // Use .Text to get the value
                mycmd.Parameters.AddWithValue("@MealAllowance", mealallowance);
                mycmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
        }
        private void SavePaymentInformation(string paymentamount, string empID)
        {
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("SELECT PaymentID FROM tblPayment", consql);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                int paymentID = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    paymentID = ds.Tables[0].Rows.Count + 1;
                }
                else
                {
                    paymentID = 1;
                }

                string strInsert = "INSERT INTO tblPayment (PaymentID, EmpID, PaymentDate, PaymentAmount) VALUES (@PaymentID, @EmpID, @PaymentDate, @PaymentAmount)";
                SqlCommand mycmd = new SqlCommand(strInsert, consql);

                // Ensure that you are using the values, not the controls directly
                mycmd.Parameters.AddWithValue("@PaymentID", paymentID);
                mycmd.Parameters.AddWithValue("@EmpID", empID);
                mycmd.Parameters.AddWithValue("@PaymentDate", paymentdate.Text);  // Use .Text to get the value
                mycmd.Parameters.AddWithValue("@PaymentAmount", paymentamount);
                mycmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void SaveAttendanceInformation(string totalAttendance, string totalLateDay, string totalLeaveDay, string empID)
        {
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("SELECT AttendanceID FROM tblAttendance", consql);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                int attendanceID = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    attendanceID = ds.Tables[0].Rows.Count + 1;
                }
                else {
                    attendanceID = 1;
                }
                string strInsert = "INSERT INTO tblAttendance (AttendanceID, EmpID, TotalAttendance, TotalLateDay, TotalLeaveDay) VALUES (@AttendanceID, @EmpID, @TotalAttendance, @TotalLateDay, @TotalLeaveDay)";
                SqlCommand mycmd = new SqlCommand(strInsert, consql);

                // Ensure that you are using the values, not the controls directly
                mycmd.Parameters.AddWithValue("@AttendanceID", attendanceID);
                mycmd.Parameters.AddWithValue("@EmpID", empID);
                mycmd.Parameters.AddWithValue("@TotalAttendance", attendance.Text);  // Use .Text to get the value
                mycmd.Parameters.AddWithValue("@TotalLateDay", lateday.Text);
                mycmd.Parameters.AddWithValue("@TotalLeaveDay", leaveday.Text);

                mycmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
               
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

        private void addemployee_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void logout_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Sure You Want To Exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                login login = new login();
                this.Hide();
                login.Show();
            }
        }

        private bool Validation()
        {
            bool result = false;
          
            if (string.IsNullOrEmpty(empid.Text))
            {
                errorProvider1.SetError(empid, "Please Choose Employee ID");
            }else if (string.IsNullOrEmpty(attendance.Text))
            {
                errorProvider1.SetError(attendance, "Attendance Required");
            }
            else
            {
                errorProvider1.Clear();
                result = true;
            }
            return result;
        }
        private void empid_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {

                string str = "SELECT e.Name, e.BankAccount,e.JoinDate, p.BasicSalary " +
                         "FROM tblEmployee e " +
                         "INNER JOIN tblPosition p ON e.PositionID = p.PositionID " +
                         "WHERE e.EmpID = @EmpID";

                using (SqlDataAdapter adEmp = new SqlDataAdapter(str, consql))
                {
                    adEmp.SelectCommand.Parameters.AddWithValue("@EmpID", empid.Text);

                    DataSet dsEmp = new DataSet();
                    adEmp.Fill(dsEmp, "EmployeeData");

                    DataTable dt = dsEmp.Tables["EmployeeData"];

                    if (dt.Rows.Count > 0)
                    {

                        empname.Text = dt.Rows[0]["Name"].ToString();
                        empbankacc.Text = dt.Rows[0]["BankAccount"].ToString();
                        empsalary.Text = dt.Rows[0]["BasicSalary"].ToString();
                        empjoindate.Text = dt.Rows[0]["JoinDate"].ToString();

                    }
                    else
                    {
                        empname.Text = "Employee not found";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadEmployeeData()
        {
              SqlDataAdapter daEmpData = new SqlDataAdapter("select * from tblEmployee", consql);
              DataTable dtEmpData = new DataTable();

              daEmpData.Fill(dtEmpData);
              empCalculatedg.DataSource = dtEmpData;


           /*  string query = "SELECT Name,Address,PhoneNumber,Remark,JoinDate,NoOfSale,BankAccount,Department,Position,BasicSalary FROM tblEmployee INNER JOIN tblPosition ON tblEmployee.PositionID = tblPosition.PositionID LEFT JOIN tblAttendance ON tblEmployee.EmpID = tblAttendance.EmpID";
            SqlDataAdapter adapter = new SqlDataAdapter(query, consql);

            daEmpData.Fill(dtEmpData);
            empCalculatedg.DataSource = dtEmpData;

           DataSet Dset = new DataSet();
            adapter.Fill(Dset, "salaryReport");
            dtsalary = Dset.Tables["salaryReport"];
            dgSalary.DataSource = dtsalary;

            */
        }

        public string value { get; set; }

        public object empID { get; set; }

        public string attendanceID { get; set; }




        public string empJoinDate { get; set; }

        public string currentDate { get; set; }

        public int sale { get; set; }

        public int nosale { get; set; }

        private void empCalculatedg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = empCalculatedg.CurrentRow.Index;
            empid.Text = Dset.Tables[0].Rows[i][0].ToString();
            empname.Text = Dset.Tables[0].Rows[i][1].ToString();
            empsalary.Text = Dset.Tables[0].Rows[i][2].ToString();
            empbankacc.Text = Dset.Tables[0].Rows[i][3].ToString();
            empjoindate.Text = Dset.Tables[0].Rows[i][3].ToString();

        }

        private void attendance_TextChanged(object sender, EventArgs e)
        {
            bool isNumeric = !string.IsNullOrEmpty(attendance.Text) && attendance.Text.All(char.IsDigit);
            if (!isNumeric)
            {
                errorProvider1.SetError(attendance, "Please enter a valid");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void lateday_TextChanged(object sender, EventArgs e)
        {
            bool isNumeric = !string.IsNullOrEmpty(lateday.Text) && lateday.Text.All(char.IsDigit);
            if (!isNumeric)
            {
                errorProvider1.SetError(lateday, "Please enter a valid");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void leaveday_TextChanged(object sender, EventArgs e)
        {
            bool isNumeric = !string.IsNullOrEmpty(leaveday.Text) && leaveday.Text.All(char.IsDigit);
            if (!isNumeric)
            {
                errorProvider1.SetError(leaveday, "Please enter a valid");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void noofsale_TextChanged(object sender, EventArgs e)
        {
            bool isNumeric = !string.IsNullOrEmpty(noofsale.Text) && noofsale.Text.All(char.IsDigit);
            if (!isNumeric)
            {
                errorProvider1.SetError(noofsale, "Please enter a valid");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
    }
}
