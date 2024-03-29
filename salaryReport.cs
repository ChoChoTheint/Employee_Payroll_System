﻿using System;
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
    public partial class salaryReport : Form
    {
        bool close = true;
        public salaryReport()
        {
            InitializeComponent();
        }
        SqlConnection consql;
        string constr;
        DataTable dtsalary;

        void connection()
        {
            constr = "Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=epms;Integrated Security=True";
            consql = new SqlConnection(constr);
            consql.Open();
        }
        private void Setting()
        {
            dgSalary.Columns[0].HeaderText = "ID";
            dgSalary.Columns[1].HeaderText = "Department";
            dgSalary.Columns[2].HeaderText = "Position";
            dgSalary.Columns[3].HeaderText = "Salary";
           
            dgSalary.Columns[0].Width = 100;
            dgSalary.Columns[1].Width = 100;
            dgSalary.Columns[2].Width = 100;
            dgSalary.Columns[3].Width = 100;
           
        }
        private void salaryReport_Load(object sender, EventArgs e)
        {
            connection();
            //string query = "select* from tblEmployee join tblPosition where tblEmployee.PositionID = tblPosition.PositionID";
            string query = "SELECT DISTINCT Name,Address,PhoneNumber,Remark,JoinDate,NoOfSale,BankAccount,Department,Position,BasicSalary FROM tblEmployee INNER JOIN tblPosition ON tblEmployee.PositionID = tblPosition.PositionID LEFT JOIN tblAttendance ON tblEmployee.EmpID = tblAttendance.EmpID";
          //  string query = "SELECT Name,Address,PhoneNumber,Remark,JoinDate,NoOfSale,BankAccount,Department,Position,BasicSalary,Attendance,LateDay,LeaveDay,PaymentAmount,tblForm.Form FROM tblEmployee INNER JOIN tblPosition ON tblEmployee.PositionID = tblPosition.PositionID LEFT JOIN tblAttendance ON tblEmployee.EmpID = tblAttendance.EmpID LEFT JOIN tblPayment ON tblEmployee.EmpID = tblPayment.EmpID LEFT JOIN tblEmployee ON tblEmployee.EmpID = tblEmployee.EmpID LEFT JOIN tblForm ON tblEmployee.EmpID = tblForm.EmpID;";

            SqlDataAdapter adapter = new SqlDataAdapter(query, consql);
            DataSet Dset = new DataSet();
            adapter.Fill(Dset, "salaryReport");
            dtsalary = Dset.Tables["salaryReport"];
            dgSalary.DataSource = dtsalary;
            //Setting();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            /*
           string chk = txtreport.Text;
           SqlConnection con = new SqlConnection("Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=EmpPayrollSystem;User ID=Sa;Password=p@ssword");
           SqlCommand cmd = new SqlCommand("SELECT * FROM tblEmployee INNER JOIN tblPosition ON tblEmployee.PositionID = tblPosition.PositionID WHERE Name LIKE @Name OR Department LIKE @Department OR BasicSalary LIKE @BasicSalary'"+chk+"'", con);
           SqlDataAdapter sda = new SqlDataAdapter(cmd);
           DataSet ds = new DataSet();
           sda.Fill(ds);

           ReportShowForm ReportShowForm = new ReportShowForm();
           employeeReport report = new employeeReport();
           report.Load(Server.MapPath("employeeReport.rpt"));
           report.SetDataSource(ds.Tables["table"]);

         //  ReportShowForm.crystalReportViewer1.ReportSource = report;
           ReportShowForm.crystalReportViewer1.ReportSource = report;
           report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Employee Information");



          report code
           ReportShowForm ReportShowForm = new ReportShowForm();
           employeeReport report = new employeeReport();

           TextObject first =(TextObject) report.ReportDefinition.Sections["Section3"].ReportObjects["empidtext"];
           first.Text = txtreport.Text;
           ReportShowForm.crystalReportViewer1.ReportSource = report;
           ReportShowForm.Show();

           */
          // filter code
            try
            {
                string nameValue = txtreport.Text;
                string query = "SELECT DISTINCT Name,Address,PhoneNumber,Remark,JoinDate,NoOfSale,BankAccount,Department,Position,BasicSalary FROM tblEmployee INNER JOIN tblPosition ON tblEmployee.PositionID = tblPosition.PositionID WHERE Name LIKE @Name OR Department LIKE @Department OR BasicSalary LIKE @BasicSalary";

                SqlDataAdapter adapter = new SqlDataAdapter(query, consql);
                adapter.SelectCommand.Parameters.AddWithValue("@Name", "%" + nameValue + "%");
                adapter.SelectCommand.Parameters.AddWithValue("@Department", "%" + nameValue + "%");
                adapter.SelectCommand.Parameters.AddWithValue("@BasicSalary", "%" + nameValue + "%");

                DataSet Dset = new DataSet();
                adapter.Fill(Dset, "salaryReport");
                dtsalary = Dset.Tables["salaryReport"];
                dgSalary.DataSource = dtsalary;
               // Setting();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
             
        }

        private void salaryReport_FormClosing(object sender, FormClosingEventArgs e)
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

        private void admindashboard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void addposition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void addemployee_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void calculatesalary_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void dgSalary_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public static string fname, SName;
        private void btnReport_Click(object sender, EventArgs e)
        {
            salReport.fname = "SalaryReport";
            salReport.SName = txtreport.Text;
            salReport freport = new salReport();
            freport.Show();
           
        }

        
    }
}
