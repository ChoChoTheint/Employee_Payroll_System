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
    public partial class filterOffice : Form
    {
        public filterOffice()
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

            if (fname.Contains("ChronicIllness") == true)
            {

                string strRpt;
                string filePath;
                if (SName != "")
                {

                    strRpt = "WITH OrderedResults AS ( SELECT dbo.tblRecords.recordID,dbo.tblRecords.patient_id,dbo.tblRecords.temperature,dbo.tblRecords.weight,dbo.tblRecords.height,";
                    strRpt += " dbo.tblRecords.bp1,dbo.tblRecords.bp2, dbo.tblRecords.spo2, dbo.tblRecords.diabetes,dbo.tblRecords.chronic_diseases,dbo.tblRecords.bmi,dbo.tblRecords.illness,dbo.tblRecords.com_diseases,";
                    strRpt += " dbo.tblRecords.treatment,dbo.tblRecords.remark,dbo.tblRecords.instructions,dbo.tblRecords.doctorID,dbo.tblRecords.created_date,dbo.tblPatients.patientID,dbo.tblPatients.name,";
                    strRpt += " dbo.tblPatients.dob,dbo.tblPatients.phone,dbo.tblPatients.nrc, dbo.tblPatients.father, dbo.tblPatients.gender,dbo.tblPatients.email,dbo.tblPatients.address,dbo.tblPatients.blood_type,";
                    strRpt += " dbo.tblPatients.occupation, dbo.tblPatients.marital_status, dbo.tblPatients.ec_name,dbo.tblPatients.ec_phone, dbo.tblPatients.ec_relationship,dbo.tblPatients.chronic_illness,dbo.tblAdmin.adminID,";
                    strRpt += " dbo.tblAdmin.admin_name,dbo.tblAdmin.role,dbo.tblPatients.note, dbo.tblPatients.medicine_before,ROW_NUMBER() OVER (PARTITION BY dbo.tblRecords.patient_id ORDER BY dbo.tblRecords.created_date DESC) AS RowNum FROM dbo.tblRecords INNER JOIN dbo.tblPatients ON dbo.tblRecords.patient_id = dbo.tblPatients.patientID INNER JOIN dbo.tblAdmin ON dbo.tblRecords.doctorID = dbo.tblAdmin.adminID WHERE (dbo.tblRecords.chronic_diseases = '" + SName + "')) SELECT";
                    strRpt += " recordID, patient_id,temperature,weight,height,bp1,bp2,spo2,diabetes,chronic_diseases, bmi, illness,com_diseases,treatment,remark,instructions,doctorID,created_date, patientID,name,dob As Expr4,phone,nrc,father,gender,email,address,blood_type,occupation, marital_status,ec_name,ec_phone,ec_relationship,chronic_illness,adminID,admin_name,role,note,medicine_before FROM OrderedResults WHERE RowNum = 1;";




                    //AND DATEDIFF(YEAR, dob, GETDATE()) < 18;

                    filePath = "C:\\Users\\cho_chit\\Documents\\Visual Studio 2013\\Projects\\EmployeePayrollManagementSystem\\srptChronic.rpt";

                }
                else
                {
                    //strRpt = "SELECT dbo.tblRecords.recordID, dbo.tblRecords.recordID AS Expr1, dbo.tblRecords.patient_id, dbo.tblRecords.temperature, dbo.tblRecords.weight, dbo.tblRecords.height, dbo.tblRecords.bp1, dbo.tblRecords.bp2, dbo.tblRecords.spo2, dbo.tblRecords.diabetes, dbo.tblRecords.chronic_diseases, dbo.tblRecords.bmi, dbo.tblRecords.illness, dbo.tblRecords.com_diseases, dbo.tblRecords.treatment, dbo.tblRecords.remark, dbo.tblRecords.instructions,dbo.tblRecords.doctorID, dbo.tblRecords.created_date AS Expr3, dbo.tblPatients.patientID, dbo.tblPatients.name, dbo.tblPatients.dob AS Expr4, dbo.tblPatients.phone, dbo.tblPatients.nrc, dbo.tblPatients.father,dbo.tblPatients.gender, dbo.tblPatients.email, dbo.tblPatients.address, dbo.tblPatients.blood_type, dbo.tblPatients.occupation, dbo.tblPatients.marital_status, dbo.tblPatients.ec_name, dbo.tblPatients.ec_phone,dbo.tblPatients.ec_relationship, dbo.tblPatients.chronic_illness, dbo.tblAdmin.adminID, dbo.tblAdmin.admin_name, dbo.tblAdmin.adminID AS Expr5, dbo.tblAdmin.admin_name AS Expr6, dbo.tblAdmin.role, dbo.tblPatients.note, dbo.tblPatients.medicine_before FROM dbo.tblRecords INNER JOIN dbo.tblPatients ON dbo.tblRecords.patient_id = dbo.tblPatients.patientID INNER JOIN dbo.tblAdmin ON dbo.tblRecords.doctorID = dbo.tblAdmin.adminID WHERE (dbo.tblRecords.bmi > '30.0')";
                    //SELECT c.Name, p.productName FROM Customer c JOIN (SELECT dob.tblRecords.patient_id, MAX(dbo.tblRecords.recordID) as MaxRID FROM tbl.Records GROUP BY tbl.patient_id) pu ON c.UserId = pu.UserID JOIN Purchases p ON p.ProductID = pu.MaxPID

                    strRpt = "WITH OrderedResults AS ( SELECT dbo.tblRecords.recordID,dbo.tblRecords.patient_id,dbo.tblRecords.temperature,dbo.tblRecords.weight,dbo.tblRecords.height,";
                    strRpt += " dbo.tblRecords.bp1,dbo.tblRecords.bp2, dbo.tblRecords.spo2, dbo.tblRecords.diabetes,dbo.tblRecords.chronic_diseases,dbo.tblRecords.bmi,dbo.tblRecords.illness,dbo.tblRecords.com_diseases,";
                    strRpt += " dbo.tblRecords.treatment,dbo.tblRecords.remark,dbo.tblRecords.instructions,dbo.tblRecords.doctorID,dbo.tblRecords.created_date,dbo.tblPatients.patientID,dbo.tblPatients.name,";
                    strRpt += " dbo.tblPatients.dob,dbo.tblPatients.phone,dbo.tblPatients.nrc, dbo.tblPatients.father, dbo.tblPatients.gender,dbo.tblPatients.email,dbo.tblPatients.address,dbo.tblPatients.blood_type,";
                    strRpt += " dbo.tblPatients.occupation, dbo.tblPatients.marital_status, dbo.tblPatients.ec_name,dbo.tblPatients.ec_phone, dbo.tblPatients.ec_relationship,dbo.tblPatients.chronic_illness,dbo.tblAdmin.adminID,";
                    strRpt += " dbo.tblAdmin.admin_name,dbo.tblAdmin.role,dbo.tblPatients.note, dbo.tblPatients.medicine_before,ROW_NUMBER() OVER (PARTITION BY dbo.tblRecords.patient_id ORDER BY dbo.tblRecords.created_date DESC) AS RowNum FROM dbo.tblRecords INNER JOIN dbo.tblPatients ON dbo.tblRecords.patient_id = dbo.tblPatients.patientID INNER JOIN dbo.tblAdmin ON dbo.tblRecords.doctorID = dbo.tblAdmin.adminID WHERE (tblRecords.chronic_diseases = '" + SName + "')) SELECT";
                    strRpt += " recordID, patient_id,temperature,weight,height,bp1,bp2,spo2,diabetes,chronic_diseases, bmi, illness,com_diseases,treatment,remark,instructions,doctorID,created_date, patientID,name,dob As Expr4,phone,nrc,father,gender,email,address,blood_type,occupation, marital_status,ec_name,ec_phone,ec_relationship,chronic_illness,adminID,admin_name,role,note,medicine_before FROM OrderedResults WHERE RowNum = 1;";




                    //AND DATEDIFF(YEAR, dob, GETDATE()) < 18;

                    filePath = "C:\\Users\\cho_chit\\Documents\\Visual Studio 2013\\Projects\\EmployeePayrollManagementSystem\\rptChronic.rpt";

                }
                try
                {
                    DataSet dss = new DataSet();
                    SqlCommand testcmd = new SqlCommand(strRpt, consql);
                    testcmd.CommandType = CommandType.Text;
                    SqlDataAdapter testda = new SqlDataAdapter(testcmd);
                    testda.Fill(dss, "vprd");

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

        private void filterOffice_Load(object sender, EventArgs e)
        {

        }
    }
}
