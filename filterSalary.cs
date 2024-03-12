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
    public partial class filterSalary : Form
    {
        public filterSalary()
        {
            InitializeComponent();
        }

        //For Database Conection
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

            if (fname.Contains("Salary") == true)
            {
                DateTime currentDate = DateTime.Now;
                string strRpt;
                string filePath;
                if (SName != "")
                {

                    //strRpt = "SELECT dbo.tblPayment.*, dbo.tblEmployee.EmpID AS Expr1, dbo.tblEmployee.PositionID, dbo.tblEmployee.Name, dbo.tblEmployee.Address, dbo.tblEmployee.PhoneNumber, dbo.tblEmployee.NRC, dbo.tblEmployee.DOB, dbo.tblEmployee.Gender, dbo.tblEmployee.Email, dbo.tblEmployee.Remark, dbo.tblEmployee.JoinDate, dbo.tblEmployee.BankAccount, dbo.tblSalary.SalaryID, dbo.tblSalary.DeptID, dbo.tblSalary.Position_ID,dbo.tblSalary.BasicSalary, dbo.tblAttendance.AttendanceID, dbo.tblAttendance.EmpID AS Expr2, dbo.tblAttendance.TotalAttendance, dbo.tblAttendance.TotalLateDay, dbo.tblAllowance.AllowanceID,dbo.tblAllowance.EmpID AS Expr3, dbo.tblAllowance.TransporationAllowance, dbo.tblAllowance.MealAllowance, dbo.tblPosition.Position_ID AS Expr4, dbo.tblPosition.Position, dbo.tblPosition.Department, dbo.tblAttendance.TotalLeaveDay FROM dbo.tblPayment INNER JOIN dbo.tblEmployee ON dbo.tblPayment.EmpID = dbo.tblEmployee.EmpID INNER JOIN dbo.tblAttendance ON dbo.tblEmployee.EmpID = dbo.tblAttendance.EmpID INNER JOIN dbo.tblAllowance ON dbo.tblEmployee.EmpID = dbo.tblAllowance.EmpID INNER JOIN dbo.tblPosition ON dbo.tblEmployee.PositionID = dbo.tblPosition.Position_ID INNER JOIN dbo.tblSalary ON dbo.tblPosition.Position_ID = dbo.tblSalary.Position_ID WHERE dbo.tblEmployee.Name = '" + SName + "'";
                    //strRpt = "SELECT dbo.tblPayment.PaymentID, dbo.tblPayment.EmpID, dbo.tblPayment.PaymentDate, dbo.tblPayment.PaymentAmount, dbo.tblEmployee.EmpID AS Expr1, dbo.tblEmployee.PositionID, dbo.tblEmployee.Name, dbo.tblEmployee.Address, dbo.tblEmployee.PhoneNumber, dbo.tblEmployee.NRC, dbo.tblEmployee.DOB, dbo.tblEmployee.Gender, dbo.tblEmployee.Email, dbo.tblEmployee.Remark, dbo.tblEmployee.JoinDate, dbo.tblEmployee.BankAccount, dbo.tblSalary.SalaryID, dbo.tblSalary.DeptID, dbo.tblSalary.Position_ID, dbo.tblSalary.BasicSalary, dbo.tblAttendance.AttendanceID, dbo.tblAttendance.EmpID AS Expr2, dbo.tblAttendance.TotalAttendance, dbo.tblAttendance.TotalLateDay, dbo.tblAllowance.AllowanceID, dbo.tblAllowance.EmpID AS Expr3, dbo.tblAllowance.TransporationAllowance, dbo.tblAllowance.MealAllowance, dbo.tblPosition.Position_ID AS Expr4, dbo.tblPosition.Position, dbo.tblPosition.Department, dbo.tblAttendance.TotalLeaveDay FROM dbo.tblPayment INNER JOIN dbo.tblEmployee ON dbo.tblPayment.EmpID = dbo.tblEmployee.EmpID INNER JOIN dbo.tblAllowance ON dbo.tblEmployee.EmpID = dbo.tblAllowance.EmpID INNER JOIN dbo.tblPosition ON dbo.tblEmployee.PositionID = dbo.tblPosition.Position_ID INNER JOIN dbo.tblSalary ON dbo.tblPosition.Position_ID = dbo.tblSalary.Position_ID WHERE dbo.tblEmployee.Name ='" + SName + "'";
                    
                    //AND DATEDIFF(YEAR, dob, GETDATE()) < 18;
                    strRpt = "SELECT dbo.tblPayment.PaymentID AS pID, dbo.tblPayment.PaymentDate AS payDate, dbo.tblPayment.PaymentAmount AS payAmount, dbo.tblEmployee.Name, dbo.tblEmployee.BankAccount, dbo.tblSalary.BasicSalary, dbo.tblAttendance.TotalAttendance, dbo.tblAttendance.TotalLateDay, dbo.tblAllowance.TransporationAllowance, dbo.tblAllowance.MealAllowance, dbo.tblPosition.Position, dbo.tblAttendance.TotalLeaveDay, dbo.tblDepartment.Department FROM dbo.tblPayment INNER JOIN dbo.tblEmployee ON dbo.tblPayment.EmpID = dbo.tblEmployee.EmpID INNER JOIN dbo.tblAttendance ON dbo.tblEmployee.EmpID = dbo.tblAttendance.EmpID INNER JOIN dbo.tblAllowance ON dbo.tblEmployee.EmpID = dbo.tblAllowance.EmpID INNER JOIN dbo.tblPosition ON dbo.tblEmployee.PositionID = dbo.tblPosition.Position_ID INNER JOIN dbo.tblSalary ON dbo.tblPosition.Position_ID = dbo.tblSalary.Position_ID INNER JOIN dbo.tblDepartment ON dbo.tblSalary.DeptID = dbo.tblDepartment.DeptID WHERE MONTH(dbo.tblPayment.PaymentDate) = MONTH('" + currentDate + "') AND dbo.tblEmployee.Name='" + SName + "' ORDER BY dbo.tblEmployee.Name ASC;";


                    filePath = "C:\\Users\\cho_chit\\Documents\\Visual Studio 2013\\Projects\\EmployeePayrollManagementSystem\\rptSalary.rpt";

                }
                else
                {
                    //strRpt = "SELECT dbo.tblRecords.recordID, dbo.tblRecords.recordID AS Expr1, dbo.tblRecords.patient_id, dbo.tblRecords.temperature, dbo.tblRecords.weight, dbo.tblRecords.height, dbo.tblRecords.bp1, dbo.tblRecords.bp2, dbo.tblRecords.spo2, dbo.tblRecords.diabetes, dbo.tblRecords.chronic_diseases, dbo.tblRecords.bmi, dbo.tblRecords.illness, dbo.tblRecords.com_diseases, dbo.tblRecords.treatment, dbo.tblRecords.remark, dbo.tblRecords.instructions,dbo.tblRecords.doctorID, dbo.tblRecords.created_date AS Expr3, dbo.tblPatients.patientID, dbo.tblPatients.name, dbo.tblPatients.dob AS Expr4, dbo.tblPatients.phone, dbo.tblPatients.nrc, dbo.tblPatients.father,dbo.tblPatients.gender, dbo.tblPatients.email, dbo.tblPatients.address, dbo.tblPatients.blood_type, dbo.tblPatients.occupation, dbo.tblPatients.marital_status, dbo.tblPatients.ec_name, dbo.tblPatients.ec_phone,dbo.tblPatients.ec_relationship, dbo.tblPatients.chronic_illness, dbo.tblAdmin.adminID, dbo.tblAdmin.admin_name, dbo.tblAdmin.adminID AS Expr5, dbo.tblAdmin.admin_name AS Expr6, dbo.tblAdmin.role, dbo.tblPatients.note, dbo.tblPatients.medicine_before FROM dbo.tblRecords INNER JOIN dbo.tblPatients ON dbo.tblRecords.patient_id = dbo.tblPatients.patientID INNER JOIN dbo.tblAdmin ON dbo.tblRecords.doctorID = dbo.tblAdmin.adminID WHERE (dbo.tblRecords.bmi > '30.0')";
                    //SELECT c.Name, p.productName FROM Customer c JOIN (SELECT dob.tblRecords.patient_id, MAX(dbo.tblRecords.recordID) as MaxRID FROM tbl.Records GROUP BY tbl.patient_id) pu ON c.UserId = pu.UserID JOIN Purchases p ON p.ProductID = pu.MaxPID

                    //strRpt = "SELECT dbo.tblPayment.*, dbo.tblEmployee.EmpID AS Expr1, dbo.tblEmployee.PositionID, dbo.tblEmployee.Name, dbo.tblEmployee.Address, dbo.tblEmployee.PhoneNumber, dbo.tblEmployee.NRC, dbo.tblEmployee.DOB, dbo.tblEmployee.Gender, dbo.tblEmployee.Email, dbo.tblEmployee.Remark, dbo.tblEmployee.JoinDate, dbo.tblEmployee.BankAccount, dbo.tblSalary.SalaryID, dbo.tblSalary.DeptID, dbo.tblSalary.Position_ID, dbo.tblSalary.BasicSalary, dbo.tblAttendance.AttendanceID, dbo.tblAttendance.EmpID AS Expr2, dbo.tblAttendance.TotalAttendance, dbo.tblAttendance.TotalLateDay, dbo.tblAllowance.AllowanceID, dbo.tblAllowance.EmpID AS Expr3, dbo.tblAllowance.TransporationAllowance, dbo.tblAllowance.MealAllowance, dbo.tblPosition.Position_ID AS Expr4, dbo.tblPosition.Position, dbo.tblPosition.Department, dbo.tblAttendance.TotalLeaveDay dbo.tblPayment INNER JOIN dbo.tblEmployee ON dbo.tblPayment.EmpID = dbo.tblEmployee.EmpID INNER JOIN dbo.tblAttendance ON dbo.tblEmployee.EmpID = dbo.tblAttendance.EmpID INNER JOIN dbo.tblAllowance ON dbo.tblEmployee.EmpID = dbo.tblAllowance.EmpID INNER JOIN dbo.tblPosition ON dbo.tblEmployee.PositionID = dbo.tblPosition.Position_ID INNER JOIN ydbo.tblSalary ON dbo.tblPosition.Position_ID = dbo.tblSalary.Position_ID";

                    // strRpt = "SELECT dbo.tblPayment.*, dbo.tblEmployee.EmpID AS Expr1, dbo.tblEmployee.PositionID, dbo.tblEmployee.Name, dbo.tblEmployee.Address, dbo.tblEmployee.PhoneNumber, dbo.tblEmployee.NRC, dbo.tblEmployee.DOB, dbo.tblEmployee.Gender, dbo.tblEmployee.Email, dbo.tblEmployee.Remark, dbo.tblEmployee.JoinDate, dbo.tblEmployee.BankAccount, dbo.tblSalary.SalaryID, dbo.tblSalary.DeptID, dbo.tblSalary.Position_ID,dbo.tblSalary.BasicSalary, dbo.tblAttendance.AttendanceID, dbo.tblAttendance.EmpID AS Expr2, dbo.tblAttendance.TotalAttendance, dbo.tblAttendance.TotalLateDay, dbo.tblAllowance.AllowanceID,dbo.tblAllowance.EmpID AS Expr3, dbo.tblAllowance.TransporationAllowance, dbo.tblAllowance.MealAllowance, dbo.tblPosition.Position_ID AS Expr4, dbo.tblPosition.Position, dbo.tblPosition.Department, dbo.tblAttendance.TotalLeaveDay FROM dbo.tblPayment INNER JOIN dbo.tblEmployee ON dbo.tblPayment.EmpID = dbo.tblEmployee.EmpID INNER JOIN dbo.tblAttendance ON dbo.tblEmployee.EmpID = dbo.tblAttendance.EmpID INNER JOIN dbo.tblAllowance ON dbo.tblEmployee.EmpID = dbo.tblAllowance.EmpID INNER JOIN dbo.tblPosition ON dbo.tblEmployee.PositionID = dbo.tblPosition.Position_ID INNER JOIN dbo.tblSalary ON dbo.tblPosition.Position_ID = dbo.tblSalary.Position_ID";

                    //strRpt = "SELECT dbo.tblPayment.PaymentID, dbo.tblPayment.EmpID As empID, dbo.tblPayment.PaymentDate, dbo.tblPayment.PaymentAmount, dbo.tblEmployee.EmpID AS Expr1, dbo.tblEmployee.PositionID As pID, dbo.tblEmployee.Name As eName, dbo.tblEmployee.Address, dbo.tblEmployee.PhoneNumber, dbo.tblEmployee.NRC, dbo.tblEmployee.DOB, dbo.tblEmployee.Gender, dbo.tblEmployee.Email, dbo.tblEmployee.Remark, dbo.tblEmployee.JoinDate, dbo.tblEmployee.BankAccount, dbo.tblSalary.SalaryID As sID, dbo.tblSalary.DeptID As SDID, dbo.tblSalary.Position_ID As SPID, dbo.tblSalary.BasicSalary, dbo.tblAttendance.AttendanceID, dbo.tblAttendance.EmpID AS Expr2, dbo.tblAttendance.TotalAttendance, dbo.tblAttendance.TotalLateDay, dbo.tblAllowance.AllowanceID, dbo.tblAllowance.EmpID AS Expr3, dbo.tblAllowance.TransporationAllowance, dbo.tblAllowance.MealAllowance, dbo.tblPosition.Position_ID AS Expr4, dbo.tblPosition.Position, dbo.tblPosition.Department, dbo.tblAttendance.TotalLeaveDay FROM dbo.tblPayment INNER JOIN dbo.tblEmployee ON dbo.tblPayment.EmpID = dbo.tblEmployee.EmpID INNER JOIN dbo.tblAllowance ON dbo.tblEmployee.EmpID = dbo.tblAllowance.EmpID INNER JOIN dbo.tblPosition ON dbo.tblEmployee.PositionID = dbo.tblPosition.Position_ID INNER JOIN dbo.tblSalary ON dbo.tblPosition.Position_ID = dbo.tblSalary.Position_ID";

                    //AND DATEDIFF(YEAR, dob, GETDATE()) < 18;
                    strRpt = "SELECT dbo.tblPayment.PaymentID AS pID, dbo.tblPayment.EmpID AS eID, dbo.tblPayment.PaymentDate AS payDate, dbo.tblPayment.PaymentAmount AS payAmount, dbo.tblEmployee.EmpID AS eID1, dbo.tblEmployee.Name, dbo.tblEmployee.NRC, dbo.tblEmployee.Gender, dbo.tblEmployee.Email, dbo.tblEmployee.Remark, dbo.tblEmployee.JoinDate, dbo.tblEmployee.BankAccount, dbo.tblSalary.SalaryID, dbo.tblSalary.BasicSalary, dbo.tblAttendance.AttendanceID, dbo.tblAttendance.TotalAttendance, dbo.tblAttendance.TotalLateDay, dbo.tblAllowance.AllowanceID, dbo.tblAllowance.TransporationAllowance, dbo.tblAllowance.MealAllowance, dbo.tblPosition.Position, dbo.tblAttendance.TotalLeaveDay, dbo.tblSalary.DeptID, dbo.tblDepartment.Department, dbo.tblEmployee.PositionID FROM dbo.tblPayment INNER JOIN dbo.tblEmployee ON dbo.tblPayment.EmpID = dbo.tblEmployee.EmpID INNER JOIN dbo.tblAttendance ON dbo.tblEmployee.EmpID = dbo.tblAttendance.EmpID INNER JOIN dbo.tblAllowance ON dbo.tblEmployee.EmpID = dbo.tblAllowance.EmpID INNER JOIN dbo.tblPosition ON dbo.tblEmployee.PositionID = dbo.tblPosition.Position_ID INNER JOIN dbo.tblSalary ON dbo.tblPosition.Position_ID = dbo.tblSalary.Position_ID INNER JOIN dbo.tblDepartment ON dbo.tblSalary.DeptID = dbo.tblDepartment.DeptID";


                    filePath = "C:\\Users\\cho_chit\\Documents\\Visual Studio 2013\\Projects\\EmployeePayrollManagementSystem\\rptSalary.rpt";


                }
                try
                {
                    DataSet dss = new DataSet();
                    SqlCommand testcmd = new SqlCommand(strRpt, consql);
                    testcmd.CommandType = CommandType.Text;
                    SqlDataAdapter testda = new SqlDataAdapter(testcmd);
                    testda.Fill(dss, "vSalary");

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

        private void filterSalary_Load(object sender, EventArgs e)
        {

        }
    }
}
