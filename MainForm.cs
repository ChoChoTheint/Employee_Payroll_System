using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeePayrollManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
           openDashboard(new miniDashboard());
        }
        
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_main.Controls.Add(childForm);
            panel_main.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void openDashboard(Form dashboard)
        {
            if (activeForm == null)
            {
                activeForm = dashboard;
                dashboard.TopLevel = false;
                dashboard.FormBorderStyle = FormBorderStyle.None;
                dashboard.Dock = DockStyle.Fill;
                panel_main.Controls.Add(dashboard);
                panel_main.Tag = dashboard;
                dashboard.BringToFront();
                dashboard.Show();
            }
            
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

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openChildForm(new miniDashboard());
        }

        private void addposition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            openChildForm(new addPosition());

        }

        private void addemployee_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openChildForm(new addEmployee());
        }

        private void calculatesalary_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openChildForm(new calculateSalary());
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openChildForm(new customReport());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addDept_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openChildForm(new addDepartment());
        }

        private void addSalary_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openChildForm(new addSalary());
        }

        private void panel_main_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
