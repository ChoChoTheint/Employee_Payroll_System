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
    public partial class filterDepartment : Form
    {
        public filterDepartment()
        {
            InitializeComponent();
        }
        SqlConnection consql;
        String constr = "";
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

            if (fname.Contains("Department") == true)
            {
                              
                DateTime currentDate = DateTime.Now;
                string strRpt;
                string filePath;
                if (SName != "")
                {

                    //strRpt = "SELECT dbo.tblDepartment.DeptID, dbo.tblDepartment.Department AS dName, dbo.tblPosition.Position_ID, dbo.tblPosition.Position, dbo.tblPosition.Department AS deptName, dbo.tblEmployee.EmpID, dbo.tblEmployee.Name FROM dbo.tblDepartment INNER JOIN dbo.tblPosition ON dbo.tblDepartment.DeptID = dbo.tblPosition.Department INNER JOIN dbo.tblEmployee ON dbo.tblPosition.Position_ID = dbo.tblEmployee.PositionID WHERE dName ='" + SName + "'";
                    //strRpt = "SELECT dbo.tblEmployee.Name, dbo.tblPosition.Position, dbo.tblDepartment.Department, dbo.tblPayment.PaymentDate, dbo.tblPayment.PaymentAmount FROM dbo.tblEmployee INNER JOIN dbo.tblPosition ON dbo.tblEmployee.PositionID = dbo.tblPosition.Position_ID INNER JOIN dbo.tblPayment ON dbo.tblEmployee.EmpID = dbo.tblPayment.EmpID INNER JOIN dbo.tblDepartment ON dbo.tblPosition.Department = dbo.tblDepartment.DeptID WHERE MONTH(dbo.tblPayment.PaymentDate) = MONTH('" + currentDate + "') AND dbo.tblDepartment.Department ='" + SName + "'";
                    strRpt = "SELECT dbo.tblEmployee.EmpID, dbo.tblDepartment.Department, dbo.tblDepartment.DeptID, dbo.tblPosition.Position, dbo.tblEmployee.Name, dbo.tblPayment.PaymentDate, dbo.tblPayment.PaymentAmount FROM dbo.tblEmployee INNER JOIN dbo.tblPosition ON dbo.tblEmployee.PositionID = dbo.tblPosition.Position_ID INNER JOIN dbo.tblDepartment ON dbo.tblPosition.Department = dbo.tblDepartment.DeptID INNER JOIN dbo.tblPayment ON dbo.tblEmployee.EmpID = dbo.tblPayment.EmpID WHERE MONTH(dbo.tblPayment.PaymentDate) = MONTH('" + currentDate + "') AND dbo.tblDepartment.Department ='" + SName + "'";
                    //WHERE d.Department='" + SName + "'

                    //AND DATEDIFF(YEAR, dob, GETDATE()) < 18;

                    filePath = "C:\\Users\\cho_chit\\Documents\\Visual Studio 2013\\Projects\\EmployeePayrollManagementSystem\\rptDept.rpt";

                }
                else
                {
                    //strRpt = "SELECT dbo.tblRecords.recordID, dbo.tblRecords.recordID AS Expr1, dbo.tblRecords.patient_id, dbo.tblRecords.temperature, dbo.tblRecords.weight, dbo.tblRecords.height, dbo.tblRecords.bp1, dbo.tblRecords.bp2, dbo.tblRecords.spo2, dbo.tblRecords.diabetes, dbo.tblRecords.chronic_diseases, dbo.tblRecords.bmi, dbo.tblRecords.illness, dbo.tblRecords.com_diseases, dbo.tblRecords.treatment, dbo.tblRecords.remark, dbo.tblRecords.instructions,dbo.tblRecords.doctorID, dbo.tblRecords.created_date AS Expr3, dbo.tblPatients.patientID, dbo.tblPatients.name, dbo.tblPatients.dob AS Expr4, dbo.tblPatients.phone, dbo.tblPatients.nrc, dbo.tblPatients.father,dbo.tblPatients.gender, dbo.tblPatients.email, dbo.tblPatients.address, dbo.tblPatients.blood_type, dbo.tblPatients.occupation, dbo.tblPatients.marital_status, dbo.tblPatients.ec_name, dbo.tblPatients.ec_phone,dbo.tblPatients.ec_relationship, dbo.tblPatients.chronic_illness, dbo.tblAdmin.adminID, dbo.tblAdmin.admin_name, dbo.tblAdmin.adminID AS Expr5, dbo.tblAdmin.admin_name AS Expr6, dbo.tblAdmin.role, dbo.tblPatients.note, dbo.tblPatients.medicine_before FROM dbo.tblRecords INNER JOIN dbo.tblPatients ON dbo.tblRecords.patient_id = dbo.tblPatients.patientID INNER JOIN dbo.tblAdmin ON dbo.tblRecords.doctorID = dbo.tblAdmin.adminID WHERE (dbo.tblRecords.bmi > '30.0')";
                    //SELECT c.Name, p.productName FROM Customer c JOIN (SELECT dob.tblRecords.patient_id, MAX(dbo.tblRecords.recordID) as MaxRID FROM tbl.Records GROUP BY tbl.patient_id) pu ON c.UserId = pu.UserID JOIN Purchases p ON p.ProductID = pu.MaxPID

                    strRpt = "SELECT dbo.tblEmployee.Name, dbo.tblPosition.Position, dbo.tblDepartment.Department, dbo.tblPayment.PaymentDate, dbo.tblPayment.PaymentAmount FROM dbo.tblEmployee INNER JOIN dbo.tblPosition ON dbo.tblEmployee.PositionID = dbo.tblPosition.Position_ID INNER JOIN dbo.tblPayment ON dbo.tblEmployee.EmpID = dbo.tblPayment.EmpID INNER JOIN dbo.tblDepartment ON dbo.tblPosition.Department = dbo.tblDepartment.DeptID WHERE MONTH(dbo.tblPayment.PaymentDate) = MONTH('" + currentDate + "');";




                    //AND DATEDIFF(YEAR, dob, GETDATE()) < 18;

                    filePath = "C:\\Users\\cho_chit\\Documents\\Visual Studio 2013\\Projects\\EmployeePayrollManagementSystemrptDept.rpt";

                }
                try
                {
                    DataSet dss = new DataSet();
                    SqlCommand testcmd = new SqlCommand(strRpt, consql);
                    testcmd.CommandType = CommandType.Text;
                    SqlDataAdapter testda = new SqlDataAdapter(testcmd);
                    testda.Fill(dss, "vDepartment");

                    ReportDocument myReportDocument = new ReportDocument();
                    myReportDocument.Load(filePath);
                    myReportDocument.SetDataSource(dss);
                    myReportDocument.SetDatabaseLogon("sa", "p@ssw0rd");
                    crystalReportViewer1.ReportSource = myReportDocument;
                    crystalReportViewer1.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Application.Exit();
                }

            }
        }

        private void filterDepartment_Load(object sender, EventArgs e)
        {

        }
    }
}
