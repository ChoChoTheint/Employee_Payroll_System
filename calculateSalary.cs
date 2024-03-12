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
        SqlConnection consql;
        string str;
        DataSet Dset;
        public calculateSalary()
        {
            InitializeComponent();
        }
        void connection()
        {
            str = "Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=epms;Integrated Security=True";
            consql = new SqlConnection(str);
            consql.Open();
        }
        
        private void calculateSalary_FormClosing(object sender, FormClosingEventArgs e)
        {
           
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

        }

        private void calculateSalary_Load(object sender, EventArgs e)
        {
            CheckLastDayOfMonth();
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
            attendance.Focus();
           
        }
        private void clearBtn_Click(object sender, EventArgs e)
        {
            Clear();
            errorProvider1.Clear();
        }
        private void btncalculate_Click(object sender, EventArgs e)
        {
            int lateDay;
            if(string.IsNullOrEmpty(lateday.Text))
            {
                lateDay = 0;
            }
            else
            {
                lateDay = Convert.ToInt16(lateday.Text);
            }
           
            if (lateDay > 6)
            {
                MessageBox.Show("Late day can't be more than 6");
                errorProvider1.SetError(lateday, "Omg, you can't late more than 6 days.");
                lateday.Focus();
                return;
                
            }
            else
            {
                if (empid.Text != "")
                {
                    int att;
                    string empJoinDateText = empjoindate.Text;
                    DateTime empJoinDate = DateTime.Parse(empJoinDateText);
                    TimeSpan difference = DateTime.Today - empJoinDate;
                    int months = DateTime.Today.Month;
                   
                    int daysDifference = difference.Days;
                    if (daysDifference <= 30)
                    {
                        att = daysDifference;
                        attendance.Enabled = false;
                    }
                    else
                    {
                        att = 30;
                        attendance.Enabled = false;
                    }

                    DateTime currentDateOnly = DateTime.Today;
                    int monthsDifference = ((currentDateOnly.Year - empJoinDate.Year) * 12) + currentDateOnly.Month - empJoinDate.Month;


                    int leave = 0;
                    int late = 0;

                    decimal mealAllowance = 0;
                    decimal tranAllowance = 0;
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

                        if (Validation())
                        {
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
                            if (late > 6)
                            {
                                MessageBox.Show("Late day can't be more than 6");
                                lateday.Focus();
                            }


                            decimal salaryValue = Convert.ToDecimal(empsalary.Text);
                            decimal onedaysalary = salaryValue / 30;

                            if (salaryValue < 500000)
                            {
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
                    }
                    else
                    {
                        if (Validation())
                        {

                            decimal salaryValue = Convert.ToDecimal(empsalary.Text);
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
                    MessageBox.Show(" \n Payment Amount: " + paymentamount + "\n Attendance " + att + "\n Late day " + lateday.Text + "\n Leave day " + leaveday.Text);

                    DateTime currentDate = DateTime.Now;
                   
                    string searchQuery = "SELECT EmpID, PaymentDate FROM tblPayment WHERE EmpID =@empid AND MONTH(dbo.tblPayment.PaymentDate) = MONTH('" + currentDate + "')";
                    SqlCommand cmd = new SqlCommand(searchQuery, consql);
                    cmd.Parameters.AddWithValue("@empid", empid.Text);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read() == true)
                    {
                        MessageBox.Show("Salary already Calculate and added for theis month", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        reader.Close();
                    }
                    else
                    {
                        reader.Close();
                        SavePaymentInformation(paymentamount, empid.Text);
                        SaveAttendanceInformation(attendance.Text, lateday.Text, leaveday.Text, empid.Text);
                        SaveAllowanceInformation(mealAllowance, tranAllowance, empid.Text);
                       
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
                        FillData();
                        IsPaymentAlreadyDoneThisMonth();
                    }

                    
                   
                }
                else
                {
                    MessageBox.Show("Choose ID");
                }
            }
            
        }
        private bool IsPaymentAlreadyDoneThisMonth()
        {
            string connectionString = "Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=epms;Integrated Security=True";
            string query = "SELECT COUNT(*) FROM tblPayment WHERE EmpID = @EmpID AND YEAR(PaymentDate) = YEAR(GETDATE()) AND MONTH(PaymentDate) = MONTH(GETDATE())";
            int paymentCount = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmpID", empid.Text);
                        paymentCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            if (paymentCount > 1)
            {
                MessageBox.Show("This Employee's payment is done!");
                Clear();
            }
           
            return paymentCount > 0;
        }
        private void saveEmployeeRemark(string remark, string empID)
        { 
         try{
                string strInsert = "Update tblEmployee Set Remark = @remark Where EmpID = @empID";
                SqlCommand mycmd = new SqlCommand(strInsert, consql);
                mycmd.Parameters.AddWithValue("@remark", remark);
                mycmd.Parameters.AddWithValue("@empID", empID);              
                mycmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void saveNoOfSale(int nosale, string empID)
        {
           
            try
            {
                using (SqlConnection consql = new SqlConnection("Data Source=ADMINISTRATOR;Initial Catalog=epms;Persist Security Info=True;User ID=sa;Password=p@ssw0rd"))
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
                mycmd.Parameters.AddWithValue("@AllowanceID", allowanceID);
                mycmd.Parameters.AddWithValue("@EmpID", empID);
                mycmd.Parameters.AddWithValue("@TransporationAllowance", tranallowance); 
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

                mycmd.Parameters.AddWithValue("@PaymentID", paymentID);
                mycmd.Parameters.AddWithValue("@EmpID", empID);
                mycmd.Parameters.AddWithValue("@PaymentDate", paymentdate.Text);  
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
            addSalary addPosition = new addSalary();
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
            addSalary addPosition = new addSalary();
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
            addSalary addPosition = new addSalary();
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
            addSalary addPosition = new addSalary();
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
            addSalary addPosition = new addSalary();
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
                         "INNER JOIN tblSalary p ON e.PositionID = p.Position_ID " +
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
            empjoindate.Text = Dset.Tables[0].Rows[i][4].ToString();

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

        private void CheckLastDayOfMonth()
        {
            DateTime currentDate = new DateTime(2024, 2, 29); 
            
            if (currentDate.Day == DateTime.DaysInMonth(currentDate.Year, currentDate.Month))
            {
                MessageBox.Show("This form will load only on the last day of the month.");
                
            }
            else
            {
                MessageBox.Show("This form will not load today. Har Ha");
                this.Close();
            }
        }
    }
}
