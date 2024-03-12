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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace EmployeePayrollManagementSystem
{
    public partial class salReport : Form
    {
        public salReport()
        {
            InitializeComponent();
        }
        SqlConnection consql;
        string constr;
     //   DataTable dtsalary;

        void connection()
        {
            constr = "Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=epms;Integrated Security=True";
            consql = new SqlConnection(constr);
            consql.Open();
        }
        public static string fname, SName;
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            connection();
            if (fname.Contains("SalaryReport") == true)
            {

                string strRpt;
                string filePath;
                if (SName != "")
                {
                    strRpt = "Select * from tblEmployee where Name ='" + SName + "'";


                    filePath = "D:\\Employees Payroll Management System\\EmployeePayrollManagementSystem\\CrystalReport3.rpt";

                }
                else
                {
                    strRpt = "Select * from tblEmployee";
                    filePath = "D:\\Employees Payroll Management System\\EmployeePayrollManagementSystem\\CrystalReport3.rpt";



                }
                DataSet dss = new DataSet();
                SqlCommand testcmd = new SqlCommand(strRpt, consql);
                testcmd.CommandType = CommandType.Text;
                SqlDataAdapter testda = new SqlDataAdapter(testcmd);
                testda.Fill(dss, "tblEmployee");

                ReportDocument myReportDocument = new ReportDocument();
                myReportDocument.Load(filePath);
                myReportDocument.SetDataSource(dss);
                myReportDocument.SetDatabaseLogon("sa", "p@ssw0rd");
                crystalReportViewer1.ReportSource = myReportDocument;
                crystalReportViewer1.Refresh();




            }
        }
    }
}
