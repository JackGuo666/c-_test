﻿namespace LocalPLC.Base
{
    partial class UserControlEth
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.ipAddressControl4 = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControl3 = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControl2 = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControl1 = new IPAddressControlLib.IPAddressControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.ipAddressControl4);
            this.panel1.Controls.Add(this.ipAddressControl3);
            this.panel1.Controls.Add(this.ipAddressControl2);
            this.panel1.Controls.Add(this.ipAddressControl1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 438);
            this.panel1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(184, 369);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "SNTP_server_ip";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(187, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "网关地址";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "子网掩码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "IP地址";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(182, 111);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(159, 22);
            this.radioButton1.TabIndex = 8;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "DHCP分配IP地址";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // ipAddressControl4
            // 
            this.ipAddressControl4.AllowInternalTab = false;
            this.ipAddressControl4.AutoHeight = true;
            this.ipAddressControl4.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl4.Location = new System.Drawing.Point(329, 369);
            this.ipAddressControl4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControl4.MinimumSize = new System.Drawing.Size(141, 28);
            this.ipAddressControl4.Name = "ipAddressControl4";
            this.ipAddressControl4.ReadOnly = false;
            this.ipAddressControl4.Size = new System.Drawing.Size(246, 28);
            this.ipAddressControl4.TabIndex = 6;
            this.ipAddressControl4.Text = "...";
            // 
            // ipAddressControl3
            // 
            this.ipAddressControl3.AllowInternalTab = false;
            this.ipAddressControl3.AutoHeight = true;
            this.ipAddressControl3.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl3.Location = new System.Drawing.Point(329, 276);
            this.ipAddressControl3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControl3.MinimumSize = new System.Drawing.Size(141, 28);
            this.ipAddressControl3.Name = "ipAddressControl3";
            this.ipAddressControl3.ReadOnly = false;
            this.ipAddressControl3.Size = new System.Drawing.Size(246, 28);
            this.ipAddressControl3.TabIndex = 6;
            this.ipAddressControl3.Text = "...";
            // 
            // ipAddressControl2
            // 
            this.ipAddressControl2.AllowInternalTab = false;
            this.ipAddressControl2.AutoHeight = true;
            this.ipAddressControl2.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl2.Location = new System.Drawing.Point(329, 235);
            this.ipAddressControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControl2.MinimumSize = new System.Drawing.Size(141, 28);
            this.ipAddressControl2.Name = "ipAddressControl2";
            this.ipAddressControl2.ReadOnly = false;
            this.ipAddressControl2.Size = new System.Drawing.Size(246, 28);
            this.ipAddressControl2.TabIndex = 6;
            this.ipAddressControl2.Text = "...";
            // 
            // ipAddressControl1
            // 
            this.ipAddressControl1.AllowInternalTab = false;
            this.ipAddressControl1.AutoHeight = true;
            this.ipAddressControl1.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl1.Location = new System.Drawing.Point(329, 194);
            this.ipAddressControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControl1.MinimumSize = new System.Drawing.Size(141, 28);
            this.ipAddressControl1.Name = "ipAddressControl1";
            this.ipAddressControl1.ReadOnly = false;
            this.ipAddressControl1.Size = new System.Drawing.Size(246, 28);
            this.ipAddressControl1.TabIndex = 6;
            this.ipAddressControl1.Text = "...";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(266, 62);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(95, 28);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Validated += new System.EventHandler(this.textBox1_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(179, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "网口1";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(182, 151);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(123, 22);
            this.radioButton3.TabIndex = 8;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "固定IP地址";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(182, 331);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(70, 22);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "SNTP";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // UserControlEth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UserControlEth";
            this.Size = new System.Drawing.Size(1050, 438);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton1;
        private IPAddressControlLib.IPAddressControl ipAddressControl1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private IPAddressControlLib.IPAddressControl ipAddressControl3;
        private IPAddressControlLib.IPAddressControl ipAddressControl2;
        private IPAddressControlLib.IPAddressControl ipAddressControl4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
