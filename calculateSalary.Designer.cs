namespace EmployeePayrollManagementSystem
{
    partial class calculateSalary
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.good = new System.Windows.Forms.RadioButton();
            this.excellence = new System.Windows.Forms.RadioButton();
            this.normal = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.empjoindate = new System.Windows.Forms.DateTimePicker();
            this.paymentdate = new System.Windows.Forms.DateTimePicker();
            this.lateday = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.empname = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tranallowance = new System.Windows.Forms.TextBox();
            this.leaveday = new System.Windows.Forms.TextBox();
            this.empbankacc = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.attendance = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.empid = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clearBtn = new System.Windows.Forms.Button();
            this.btncalculate = new System.Windows.Forms.Button();
            this.mealallowance = new System.Windows.Forms.TextBox();
            this.empsalary = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.empCalculatedg = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empCalculatedg)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.ForestGreen;
            this.panel1.Controls.Add(this.empCalculatedg);
            this.panel1.Controls.Add(this.good);
            this.panel1.Controls.Add(this.excellence);
            this.panel1.Controls.Add(this.normal);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.empjoindate);
            this.panel1.Controls.Add(this.paymentdate);
            this.panel1.Controls.Add(this.lateday);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.empname);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.tranallowance);
            this.panel1.Controls.Add(this.leaveday);
            this.panel1.Controls.Add(this.empbankacc);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.attendance);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.empid);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.clearBtn);
            this.panel1.Controls.Add(this.btncalculate);
            this.panel1.Controls.Add(this.mealallowance);
            this.panel1.Controls.Add(this.empsalary);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.ForeColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(15, 18);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1321, 836);
            this.panel1.TabIndex = 6;
            // 
            // good
            // 
            this.good.AutoSize = true;
            this.good.Location = new System.Drawing.Point(1067, 187);
            this.good.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.good.Name = "good";
            this.good.Size = new System.Drawing.Size(64, 21);
            this.good.TabIndex = 25;
            this.good.TabStop = true;
            this.good.Text = "Good";
            this.good.UseVisualStyleBackColor = true;
            // 
            // excellence
            // 
            this.excellence.AutoSize = true;
            this.excellence.Location = new System.Drawing.Point(1067, 233);
            this.excellence.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.excellence.Name = "excellence";
            this.excellence.Size = new System.Drawing.Size(96, 21);
            this.excellence.TabIndex = 24;
            this.excellence.TabStop = true;
            this.excellence.Text = "Excellence";
            this.excellence.UseVisualStyleBackColor = true;
            // 
            // normal
            // 
            this.normal.AutoSize = true;
            this.normal.Checked = true;
            this.normal.Location = new System.Drawing.Point(1067, 141);
            this.normal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.normal.Name = "normal";
            this.normal.Size = new System.Drawing.Size(74, 21);
            this.normal.TabIndex = 23;
            this.normal.TabStop = true;
            this.normal.Text = "Normal";
            this.normal.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(965, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 29);
            this.label8.TabIndex = 22;
            this.label8.Text = "Remark";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(482, 470);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(162, 29);
            this.label7.TabIndex = 21;
            this.label7.Text = "Payment Date";
            // 
            // empjoindate
            // 
            this.empjoindate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.empjoindate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.empjoindate.Location = new System.Drawing.Point(279, 470);
            this.empjoindate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.empjoindate.Name = "empjoindate";
            this.empjoindate.Size = new System.Drawing.Size(161, 34);
            this.empjoindate.TabIndex = 20;
            // 
            // paymentdate
            // 
            this.paymentdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paymentdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.paymentdate.Location = new System.Drawing.Point(746, 470);
            this.paymentdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.paymentdate.Name = "paymentdate";
            this.paymentdate.Size = new System.Drawing.Size(161, 34);
            this.paymentdate.TabIndex = 19;
            // 
            // lateday
            // 
            this.lateday.Location = new System.Drawing.Point(746, 135);
            this.lateday.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lateday.Multiline = true;
            this.lateday.Name = "lateday";
            this.lateday.Size = new System.Drawing.Size(161, 35);
            this.lateday.TabIndex = 18;
            this.lateday.TextChanged += new System.EventHandler(this.lateday_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(486, 383);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(254, 29);
            this.label15.TabIndex = 17;
            this.label15.Text = "Transportion Allownce";
            // 
            // empname
            // 
            this.empname.Location = new System.Drawing.Point(279, 211);
            this.empname.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.empname.Multiline = true;
            this.empname.Name = "empname";
            this.empname.Size = new System.Drawing.Size(161, 35);
            this.empname.TabIndex = 16;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(100, 217);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(78, 29);
            this.label16.TabIndex = 15;
            this.label16.Text = "Name";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(495, 217);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(125, 29);
            this.label14.TabIndex = 13;
            this.label14.Text = "Leave Day";
            // 
            // tranallowance
            // 
            this.tranallowance.Location = new System.Drawing.Point(746, 383);
            this.tranallowance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tranallowance.Multiline = true;
            this.tranallowance.Name = "tranallowance";
            this.tranallowance.Size = new System.Drawing.Size(161, 35);
            this.tranallowance.TabIndex = 11;
            // 
            // leaveday
            // 
            this.leaveday.Location = new System.Drawing.Point(746, 217);
            this.leaveday.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.leaveday.Multiline = true;
            this.leaveday.Name = "leaveday";
            this.leaveday.Size = new System.Drawing.Size(161, 35);
            this.leaveday.TabIndex = 11;
            this.leaveday.TextChanged += new System.EventHandler(this.leaveday_TextChanged);
            // 
            // empbankacc
            // 
            this.empbankacc.Location = new System.Drawing.Point(279, 383);
            this.empbankacc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.empbankacc.Multiline = true;
            this.empbankacc.Name = "empbankacc";
            this.empbankacc.Size = new System.Drawing.Size(161, 35);
            this.empbankacc.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(100, 468);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 29);
            this.label13.TabIndex = 10;
            this.label13.Text = "Join Date";
            // 
            // attendance
            // 
            this.attendance.Location = new System.Drawing.Point(957, 301);
            this.attendance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.attendance.Multiline = true;
            this.attendance.Name = "attendance";
            this.attendance.Size = new System.Drawing.Size(161, 35);
            this.attendance.TabIndex = 12;
            this.attendance.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(100, 389);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(158, 29);
            this.label12.TabIndex = 9;
            this.label12.Text = "Bank Account";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(486, 301);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 29);
            this.label4.TabIndex = 10;
            this.label4.Text = "Meal Allownce";
            // 
            // empid
            // 
            this.empid.FormattingEnabled = true;
            this.empid.Location = new System.Drawing.Point(279, 135);
            this.empid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.empid.Name = "empid";
            this.empid.Size = new System.Drawing.Size(161, 24);
            this.empid.TabIndex = 8;
            this.empid.SelectedIndexChanged += new System.EventHandler(this.empid_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(351, 36);
            this.label1.TabIndex = 7;
            this.label1.Text = "Employee Payroll System";
            // 
            // clearBtn
            // 
            this.clearBtn.BackColor = System.Drawing.Color.ForestGreen;
            this.clearBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearBtn.Location = new System.Drawing.Point(720, 571);
            this.clearBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(187, 49);
            this.clearBtn.TabIndex = 6;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = false;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // btncalculate
            // 
            this.btncalculate.BackColor = System.Drawing.Color.ForestGreen;
            this.btncalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncalculate.Location = new System.Drawing.Point(487, 571);
            this.btncalculate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btncalculate.Name = "btncalculate";
            this.btncalculate.Size = new System.Drawing.Size(187, 47);
            this.btncalculate.TabIndex = 6;
            this.btncalculate.Text = "Calculate";
            this.btncalculate.UseVisualStyleBackColor = false;
            this.btncalculate.Click += new System.EventHandler(this.btncalculate_Click);
            // 
            // mealallowance
            // 
            this.mealallowance.Location = new System.Drawing.Point(746, 301);
            this.mealallowance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mealallowance.Multiline = true;
            this.mealallowance.Name = "mealallowance";
            this.mealallowance.Size = new System.Drawing.Size(161, 35);
            this.mealallowance.TabIndex = 5;
            // 
            // empsalary
            // 
            this.empsalary.Location = new System.Drawing.Point(279, 294);
            this.empsalary.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.empsalary.Multiline = true;
            this.empsalary.Name = "empsalary";
            this.empsalary.Size = new System.Drawing.Size(161, 35);
            this.empsalary.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(100, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(167, 29);
            this.label6.TabIndex = 4;
            this.label6.Text = "Employee\'s ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(486, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "Late Day";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(100, 294);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Basic Salary";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // empCalculatedg
            // 
            this.empCalculatedg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.empCalculatedg.Location = new System.Drawing.Point(417, 683);
            this.empCalculatedg.Name = "empCalculatedg";
            this.empCalculatedg.RowTemplate.Height = 24;
            this.empCalculatedg.Size = new System.Drawing.Size(567, 150);
            this.empCalculatedg.TabIndex = 26;
            this.empCalculatedg.Visible = false;
            // 
            // calculateSalary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 946);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "calculateSalary";
            this.Text = "calculateSalary";
            this.Load += new System.EventHandler(this.calculateSalary_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empCalculatedg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tranallowance;
        private System.Windows.Forms.TextBox leaveday;
        private System.Windows.Forms.TextBox empbankacc;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox attendance;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox empid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button btncalculate;
        private System.Windows.Forms.TextBox mealallowance;
        private System.Windows.Forms.TextBox empsalary;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox lateday;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox empname;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker paymentdate;
        private System.Windows.Forms.DateTimePicker empjoindate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton normal;
        private System.Windows.Forms.RadioButton good;
        private System.Windows.Forms.RadioButton excellence;
        private System.Windows.Forms.DataGridView empCalculatedg;
    }
}