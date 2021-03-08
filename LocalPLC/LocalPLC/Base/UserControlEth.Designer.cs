namespace LocalPLC.Base
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
            this.checkBox_SNTP = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton_fixed = new System.Windows.Forms.RadioButton();
            this.radioButton_dhcp = new System.Windows.Forms.RadioButton();
            this.ipAddressControl_sntpaddr = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControl_gateway = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControl_maskaddr = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControl_ipaddr = new IPAddressControlLib.IPAddressControl();
            this.textBox_eth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.checkBox_SNTP);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.radioButton_fixed);
            this.panel1.Controls.Add(this.radioButton_dhcp);
            this.panel1.Controls.Add(this.ipAddressControl_sntpaddr);
            this.panel1.Controls.Add(this.ipAddressControl_gateway);
            this.panel1.Controls.Add(this.ipAddressControl_maskaddr);
            this.panel1.Controls.Add(this.ipAddressControl_ipaddr);
            this.panel1.Controls.Add(this.textBox_eth);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 438);
            this.panel1.TabIndex = 4;
            // 
            // checkBox_SNTP
            // 
            this.checkBox_SNTP.AutoSize = true;
            this.checkBox_SNTP.Location = new System.Drawing.Point(182, 331);
            this.checkBox_SNTP.Name = "checkBox_SNTP";
            this.checkBox_SNTP.Size = new System.Drawing.Size(70, 22);
            this.checkBox_SNTP.TabIndex = 10;
            this.checkBox_SNTP.Text = "SNTP";
            this.checkBox_SNTP.UseVisualStyleBackColor = true;
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
            // radioButton_fixed
            // 
            this.radioButton_fixed.AutoSize = true;
            this.radioButton_fixed.Location = new System.Drawing.Point(182, 151);
            this.radioButton_fixed.Name = "radioButton_fixed";
            this.radioButton_fixed.Size = new System.Drawing.Size(123, 22);
            this.radioButton_fixed.TabIndex = 8;
            this.radioButton_fixed.TabStop = true;
            this.radioButton_fixed.Text = "固定IP地址";
            this.radioButton_fixed.UseVisualStyleBackColor = true;
            // 
            // radioButton_dhcp
            // 
            this.radioButton_dhcp.AutoSize = true;
            this.radioButton_dhcp.Location = new System.Drawing.Point(182, 111);
            this.radioButton_dhcp.Name = "radioButton_dhcp";
            this.radioButton_dhcp.Size = new System.Drawing.Size(159, 22);
            this.radioButton_dhcp.TabIndex = 8;
            this.radioButton_dhcp.TabStop = true;
            this.radioButton_dhcp.Text = "DHCP分配IP地址";
            this.radioButton_dhcp.UseVisualStyleBackColor = true;
            // 
            // ipAddressControl_sntpaddr
            // 
            this.ipAddressControl_sntpaddr.AllowInternalTab = false;
            this.ipAddressControl_sntpaddr.AutoHeight = true;
            this.ipAddressControl_sntpaddr.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl_sntpaddr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl_sntpaddr.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl_sntpaddr.Location = new System.Drawing.Point(329, 366);
            this.ipAddressControl_sntpaddr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControl_sntpaddr.MinimumSize = new System.Drawing.Size(141, 28);
            this.ipAddressControl_sntpaddr.Name = "ipAddressControl_sntpaddr";
            this.ipAddressControl_sntpaddr.ReadOnly = false;
            this.ipAddressControl_sntpaddr.Size = new System.Drawing.Size(246, 28);
            this.ipAddressControl_sntpaddr.TabIndex = 6;
            this.ipAddressControl_sntpaddr.Text = "...";
            // 
            // ipAddressControl_gateway
            // 
            this.ipAddressControl_gateway.AllowInternalTab = false;
            this.ipAddressControl_gateway.AutoHeight = true;
            this.ipAddressControl_gateway.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl_gateway.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl_gateway.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl_gateway.Location = new System.Drawing.Point(329, 276);
            this.ipAddressControl_gateway.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControl_gateway.MinimumSize = new System.Drawing.Size(141, 28);
            this.ipAddressControl_gateway.Name = "ipAddressControl_gateway";
            this.ipAddressControl_gateway.ReadOnly = false;
            this.ipAddressControl_gateway.Size = new System.Drawing.Size(246, 28);
            this.ipAddressControl_gateway.TabIndex = 6;
            this.ipAddressControl_gateway.Text = "...";
            // 
            // ipAddressControl_maskaddr
            // 
            this.ipAddressControl_maskaddr.AllowInternalTab = false;
            this.ipAddressControl_maskaddr.AutoHeight = true;
            this.ipAddressControl_maskaddr.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl_maskaddr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl_maskaddr.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl_maskaddr.Location = new System.Drawing.Point(329, 235);
            this.ipAddressControl_maskaddr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControl_maskaddr.MinimumSize = new System.Drawing.Size(141, 28);
            this.ipAddressControl_maskaddr.Name = "ipAddressControl_maskaddr";
            this.ipAddressControl_maskaddr.ReadOnly = false;
            this.ipAddressControl_maskaddr.Size = new System.Drawing.Size(246, 28);
            this.ipAddressControl_maskaddr.TabIndex = 6;
            this.ipAddressControl_maskaddr.Text = "...";
            // 
            // ipAddressControl_ipaddr
            // 
            this.ipAddressControl_ipaddr.AllowInternalTab = false;
            this.ipAddressControl_ipaddr.AutoHeight = true;
            this.ipAddressControl_ipaddr.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl_ipaddr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl_ipaddr.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl_ipaddr.Location = new System.Drawing.Point(329, 194);
            this.ipAddressControl_ipaddr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControl_ipaddr.MinimumSize = new System.Drawing.Size(141, 28);
            this.ipAddressControl_ipaddr.Name = "ipAddressControl_ipaddr";
            this.ipAddressControl_ipaddr.ReadOnly = false;
            this.ipAddressControl_ipaddr.Size = new System.Drawing.Size(246, 28);
            this.ipAddressControl_ipaddr.TabIndex = 6;
            this.ipAddressControl_ipaddr.Text = "...";
            // 
            // textBox_eth
            // 
            this.textBox_eth.Location = new System.Drawing.Point(266, 62);
            this.textBox_eth.Name = "textBox_eth";
            this.textBox_eth.ReadOnly = true;
            this.textBox_eth.Size = new System.Drawing.Size(95, 28);
            this.textBox_eth.TabIndex = 5;
            this.textBox_eth.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox_eth.Validated += new System.EventHandler(this.textBox1_Validated);
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
        private System.Windows.Forms.RadioButton radioButton_dhcp;
        private IPAddressControlLib.IPAddressControl ipAddressControl_ipaddr;
        private System.Windows.Forms.TextBox textBox_eth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private IPAddressControlLib.IPAddressControl ipAddressControl_gateway;
        private IPAddressControlLib.IPAddressControl ipAddressControl_maskaddr;
        private IPAddressControlLib.IPAddressControl ipAddressControl_sntpaddr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButton_fixed;
        private System.Windows.Forms.CheckBox checkBox_SNTP;
    }
}
