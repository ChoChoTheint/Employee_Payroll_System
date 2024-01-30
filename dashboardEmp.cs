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
    public partial class dashboardEmp : Form
    {
        bool close = true;
        SqlConnection consql;
        string str;
        public dashboardEmp()
        {
            InitializeComponent();
        }
        void connection()
        {
            str = "Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=EmpPayrollSystem;User ID=Sa;Password=p@ssword";
            consql = new SqlConnection(str);
            consql.Open();
        }
        private void dashboardEmp_Load(object sender, EventArgs e)
        {
          /*  string empquery = "SELECT *  FROM tblEmployee";
            SqlCommand mycmd = new SqlCommand(empquery, consql);
            mycmd.ExecuteNonQuery();

            int empCount = Convert.ToInt16(empcount.Text);
            string emp = empCount.ToString();

            int bonusCount = Convert.ToInt16(bonuscount.Text);
            string bonus = bonusCount.ToString();

            MessageBox.Show("Emp Count"+emp);
            MessageBox.Show("Bonus Count" + bonus);
           * */
            try
            {
                using (SqlConnection consql = new SqlConnection("Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=EmpPayrollSystem;User ID=Sa;Password=p@ssword"))
                {
                    consql.Open(); 

                    string empCountQuery = "SELECT COUNT(*) FROM tblEmployee";
                    using (SqlCommand empCountCmd = new SqlCommand(empCountQuery, consql))
                    {
                        int empCount = Convert.ToInt32(empCountCmd.ExecuteScalar());
                        empcount.Text = empCount.ToString();
                    }


                    string bonusCountQuery = "SELECT COUNT(Remark) FROM tblEmployee WHERE Remark != ''";
                    using (SqlCommand bonusCountCmd = new SqlCommand(bonusCountQuery, consql))
                    {
                        int bonusCount = Convert.ToInt32(bonusCountCmd.ExecuteScalar());
                        bonuscount.Text = bonusCount.ToString();
                    }

                    string saleSumQuery = "SELECT SUM(ISNULL(NoOfSale, 0)) FROM tblEmployee";
                    using (SqlCommand saleSumCmd = new SqlCommand(saleSumQuery, consql))
                    {
                        int saleSum = Convert.ToInt32(saleSumCmd.ExecuteScalar());
                        sale.Text = saleSum.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while fetching employee count: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void dashboardEmp_FormClosing(object sender, FormClosingEventArgs e)
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

        private void addposition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
