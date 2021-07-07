namespace LocalPLC.Debug
{
    partial class FormSetIP
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
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton_fixed = new System.Windows.Forms.RadioButton();
            this.radioButton_dhcp = new System.Windows.Forms.RadioButton();
            this.ipAddressControl_gateway = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControl_maskaddr = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControl_ipaddr = new IPAddressControlLib.IPAddressControl();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 242);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 15;
            this.label5.Text = "网关地址";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 16;
            this.label3.Text = "子网掩码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "IP地址";
            // 
            // radioButton_fixed
            // 
            this.radioButton_fixed.AutoSize = true;
            this.radioButton_fixed.Location = new System.Drawing.Point(60, 117);
            this.radioButton_fixed.Name = "radioButton_fixed";
            this.radioButton_fixed.Size = new System.Drawing.Size(123, 22);
            this.radioButton_fixed.TabIndex = 13;
            this.radioButton_fixed.TabStop = true;
            this.radioButton_fixed.Text = "固定IP地址";
            this.radioButton_fixed.UseVisualStyleBackColor = true;
            this.radioButton_fixed.CheckedChanged += new System.EventHandler(this.radioButton_fixed_CheckedChanged);
            // 
            // radioButton_dhcp
            // 
            this.radioButton_dhcp.AutoSize = true;
            this.radioButton_dhcp.Location = new System.Drawing.Point(60, 77);
            this.radioButton_dhcp.Name = "radioButton_dhcp";
            this.radioButton_dhcp.Size = new System.Drawing.Size(159, 22);
            this.radioButton_dhcp.TabIndex = 14;
            this.radioButton_dhcp.TabStop = true;
            this.radioButton_dhcp.Text = "DHCP分配IP地址";
            this.radioButton_dhcp.UseVisualStyleBackColor = true;
            this.radioButton_dhcp.CheckedChanged += new System.EventHandler(this.radioButton_dhcp_CheckedChanged);
            // 
            // ipAddressControl_gateway
            // 
            this.ipAddressControl_gateway.AllowInternalTab = false;
            this.ipAddressControl_gateway.AutoHeight = true;
            this.ipAddressControl_gateway.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl_gateway.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl_gateway.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl_gateway.Location = new System.Drawing.Point(259, 240);
            this.ipAddressControl_gateway.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControl_gateway.MinimumSize = new System.Drawing.Size(141, 28);
            this.ipAddressControl_gateway.Name = "ipAddressControl_gateway";
            this.ipAddressControl_gateway.ReadOnly = false;
            this.ipAddressControl_gateway.Size = new System.Drawing.Size(246, 28);
            this.ipAddressControl_gateway.TabIndex = 10;
            this.ipAddressControl_gateway.Text = "...";
            // 
            // ipAddressControl_maskaddr
            // 
            this.ipAddressControl_maskaddr.AllowInternalTab = false;
            this.ipAddressControl_maskaddr.AutoHeight = true;
            this.ipAddressControl_maskaddr.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl_maskaddr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl_maskaddr.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl_maskaddr.Location = new System.Drawing.Point(259, 199);
            this.ipAddressControl_maskaddr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControl_maskaddr.MinimumSize = new System.Drawing.Size(141, 28);
            this.ipAddressControl_maskaddr.Name = "ipAddressControl_maskaddr";
            this.ipAddressControl_maskaddr.ReadOnly = false;
            this.ipAddressControl_maskaddr.Size = new System.Drawing.Size(246, 28);
            this.ipAddressControl_maskaddr.TabIndex = 11;
            this.ipAddressControl_maskaddr.Text = "...";
            // 
            // ipAddressControl_ipaddr
            // 
            this.ipAddressControl_ipaddr.AllowInternalTab = false;
            this.ipAddressControl_ipaddr.AutoHeight = true;
            this.ipAddressControl_ipaddr.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl_ipaddr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl_ipaddr.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl_ipaddr.Location = new System.Drawing.Point(259, 158);
            this.ipAddressControl_ipaddr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControl_ipaddr.MinimumSize = new System.Drawing.Size(141, 28);
            this.ipAddressControl_ipaddr.Name = "ipAddressControl_ipaddr";
            this.ipAddressControl_ipaddr.ReadOnly = false;
            this.ipAddressControl_ipaddr.Size = new System.Drawing.Size(246, 28);
            this.ipAddressControl_ipaddr.TabIndex = 12;
            this.ipAddressControl_ipaddr.Text = "...";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(433, 374);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 35);
            this.button1.TabIndex = 19;
            this.button1.Text = "应用";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(551, 374);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 35);
            this.button2.TabIndex = 19;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormSetIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 435);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton_fixed);
            this.Controls.Add(this.radioButton_dhcp);
            this.Controls.Add(this.ipAddressControl_gateway);
            this.Controls.Add(this.ipAddressControl_maskaddr);
            this.Controls.Add(this.ipAddressControl_ipaddr);
            this.Name = "FormSetIP";
            this.Text = "FormSetIP";
            this.Load += new System.EventHandler(this.FormSetIP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton_fixed;
        private System.Windows.Forms.RadioButton radioButton_dhcp;
        private IPAddressControlLib.IPAddressControl ipAddressControl_gateway;
        private IPAddressControlLib.IPAddressControl ipAddressControl_maskaddr;
        private IPAddressControlLib.IPAddressControl ipAddressControl_ipaddr;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}