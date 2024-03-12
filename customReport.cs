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
    public partial class customReport : Form
    {
        public customReport()
        {
            InitializeComponent();
        }
        private void customReport_Load(object sender, EventArgs e)
        {
            connection();
            fill();
        }
        public static string fname, SName, PName;
        private void empReport_Click(object sender, EventArgs e)
        {
            filterProfile.fname = "EmpReport";
            filterProfile.SName = txtEmpProfile.Text;
            filterProfile freport = new filterProfile();
            freport.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filterMonthly.fname = "MonthReport";
            filterMonthly.SName = dtpMonth.Text;
            filterMonthly freport = new filterMonthly();
            freport.Show();
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            filterSalary.fname = "Salary";
            filterSalary.SName = txtEName.Text;
            filterSalary freport = new filterSalary();
            freport.Show();
        }

        private void btnDept_Click(object sender, EventArgs e)
        {
            filterDepartment.fname = "Department";
            filterDepartment.SName = cboDept.Text;
            filterDepartment freport = new filterDepartment();
            freport.Show();
        }

        private void btnOffice_Click(object sender, EventArgs e)
        {
            filterOffice.fname = "Salary";
            //filterOffice.SName = cboDept.Text;
            filterOffice freport = new filterOffice();
            freport.Show();
        }
        //For Data Connection
        SqlConnection consql;
        string constr; // Variable for connection string
        //DataSet Dset;
        //DataTable dtRecords; // Data table named dtAdmin
        // DB Connection
        void connection()
        {
            //constr = "Data Source=Leoni\\Sa;Initial Catalog=crms;Persist Security Info=True;User ID=Sa;Password=p@ssword";
            constr = "Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=epms;Integrated Security=True";

            consql = new SqlConnection(constr);
            consql.Open();
        }
        private void fill()
        {
            try
            {
                //show department with combo box
                SqlDataAdapter da = new SqlDataAdapter("Select * From tblDepartment", consql);
                DataSet ds = new DataSet();
                DataTable dt;
                da.Fill(ds, "Department");
                dt = ds.Tables["Department"];
                cboDept.DataSource = dt;
                cboDept.DisplayMember = dt.Columns["Department"].ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

       
    }
}
