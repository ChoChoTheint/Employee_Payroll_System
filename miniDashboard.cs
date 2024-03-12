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
    public partial class miniDashboard : Form
    {
        public miniDashboard()
        {
            InitializeComponent();
        }
        bool close = true;
        SqlConnection consql;
        string str;
        void connection()
        {
            str = "Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=epms;Integrated Security=True";
            consql = new SqlConnection(str);
            consql.Open();
        }
        private void miniDashboard_Load(object sender, EventArgs e)
        {
            connection();
           string empquery = "SELECT *  FROM tblEmployee";
           SqlCommand mycmd = new SqlCommand(empquery, consql);
           mycmd.ExecuteNonQuery();

           int empCount = Convert.ToInt16(empcount.Text);
           string emp = empCount.ToString();

           int bonusCount = Convert.ToInt16(bonuscount.Text);
           string bonus = bonusCount.ToString();

         
          
            try
            {

                using (SqlConnection consql1 = new SqlConnection("Data Source=DESKTOP-S262IJ9\\SA;Initial Catalog=epms;Integrated Security=True"))
               
                {
                    consql1.Open();

                    string empCountQuery = "SELECT COUNT(*) FROM tblEmployee";
                    using (SqlCommand empCountCmd = new SqlCommand(empCountQuery, consql))
                    {
                        int empCount1 = Convert.ToInt32(empCountCmd.ExecuteScalar());
                        empcount.Text = empCount1.ToString();
                    }

                    string bonusCountQuery = "SELECT COUNT(*) FROM tblPosition";
                    using (SqlCommand bonusCountCmd = new SqlCommand(bonusCountQuery, consql))
                    {
                        int bonusCount1 = Convert.ToInt32(bonusCountCmd.ExecuteScalar());
                        bonuscount.Text = bonusCount1.ToString();
                    }

                    string saleSumQuery = "SELECT COUNT(*) FROM tblDepartment";
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
