namespace LocalPLC.motion
{
    partial class UserControlDynamicPara
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox_EmeStopDeceSpeed = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.textBox_Jerk = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox_DecelerationSpeed = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox_AcceleratedSpeed = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox_MaxSpeed = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_valid = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBox_EmeStopDeceSpeed);
            this.panel3.Controls.Add(this.label25);
            this.panel3.Controls.Add(this.textBox_Jerk);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Controls.Add(this.textBox_DecelerationSpeed);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.textBox_AcceleratedSpeed);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.textBox_MaxSpeed);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Location = new System.Drawing.Point(6, 55);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(719, 212);
            this.panel3.TabIndex = 4;
            // 
            // textBox_EmeStopDeceSpeed
            // 
            this.textBox_EmeStopDeceSpeed.Location = new System.Drawing.Point(294, 156);
            this.textBox_EmeStopDeceSpeed.MaxLength = 10;
            this.textBox_EmeStopDeceSpeed.Name = "textBox_EmeStopDeceSpeed";
            this.textBox_EmeStopDeceSpeed.Size = new System.Drawing.Size(140, 28);
            this.textBox_EmeStopDeceSpeed.TabIndex = 1;
            this.textBox_EmeStopDeceSpeed.TextChanged += new System.EventHandler(this.textBox_EmeStopDeceSpeed_TextChanged);
            this.textBox_EmeStopDeceSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_EmeStopDeceSpeed_KeyPress);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(90, 159);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(98, 18);
            this.label25.TabIndex = 0;
            this.label25.Text = "急停减速度";
            // 
            // textBox_Jerk
            // 
            this.textBox_Jerk.Location = new System.Drawing.Point(294, 125);
            this.textBox_Jerk.MaxLength = 10;
            this.textBox_Jerk.Name = "textBox_Jerk";
            this.textBox_Jerk.Size = new System.Drawing.Size(140, 28);
            this.textBox_Jerk.TabIndex = 1;
            this.textBox_Jerk.TextChanged += new System.EventHandler(this.textBox_Jerk_TextChanged);
            this.textBox_Jerk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Jerk_KeyPress);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(90, 128);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(80, 18);
            this.label22.TabIndex = 0;
            this.label22.Text = "跃度Jerk";
            // 
            // textBox_DecelerationSpeed
            // 
            this.textBox_DecelerationSpeed.Location = new System.Drawing.Point(294, 94);
            this.textBox_DecelerationSpeed.MaxLength = 10;
            this.textBox_DecelerationSpeed.Name = "textBox_DecelerationSpeed";
            this.textBox_DecelerationSpeed.Size = new System.Drawing.Size(140, 28);
            this.textBox_DecelerationSpeed.TabIndex = 1;
            this.textBox_DecelerationSpeed.TextChanged += new System.EventHandler(this.textBox_DecelerationSpeed_TextChanged);
            this.textBox_DecelerationSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_DecelerationSpeed_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(90, 97);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(62, 18);
            this.label20.TabIndex = 0;
            this.label20.Text = "减速度";
            // 
            // textBox_AcceleratedSpeed
            // 
            this.textBox_AcceleratedSpeed.Location = new System.Drawing.Point(294, 63);
            this.textBox_AcceleratedSpeed.MaxLength = 10;
            this.textBox_AcceleratedSpeed.Name = "textBox_AcceleratedSpeed";
            this.textBox_AcceleratedSpeed.Size = new System.Drawing.Size(140, 28);
            this.textBox_AcceleratedSpeed.TabIndex = 1;
            this.textBox_AcceleratedSpeed.TextChanged += new System.EventHandler(this.textBox_AcceleratedSpeed_TextChanged);
            this.textBox_AcceleratedSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_AcceleratedSpeed_KeyPress);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(440, 159);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 18);
            this.label23.TabIndex = 0;
            this.label23.Text = "mm/s²";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(90, 66);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 18);
            this.label18.TabIndex = 0;
            this.label18.Text = "加速度";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(440, 128);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 18);
            this.label21.TabIndex = 0;
            this.label21.Text = "mm/s³";
            // 
            // textBox_MaxSpeed
            // 
            this.textBox_MaxSpeed.Location = new System.Drawing.Point(294, 32);
            this.textBox_MaxSpeed.MaxLength = 10;
            this.textBox_MaxSpeed.Name = "textBox_MaxSpeed";
            this.textBox_MaxSpeed.Size = new System.Drawing.Size(140, 28);
            this.textBox_MaxSpeed.TabIndex = 1;
            this.textBox_MaxSpeed.TextChanged += new System.EventHandler(this.textBox_MaxSpeed_TextChanged);
            this.textBox_MaxSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_MaxSpeed_KeyPress);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(440, 97);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 18);
            this.label19.TabIndex = 0;
            this.label19.Text = "mm/s²";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(90, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "最大速度";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(440, 66);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 18);
            this.label17.TabIndex = 0;
            this.label17.Text = "mm/s²";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(45, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 18);
            this.label15.TabIndex = 0;
            this.label15.Text = "动态参数";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(440, 35);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 18);
            this.label16.TabIndex = 0;
            this.label16.Text = "mm/s";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.button_cancel);
            this.groupBox1.Controls.Add(this.button_valid);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(728, 578);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // button_cancel
            // 
            this.button_cancel.BackColor = System.Drawing.Color.White;
            this.button_cancel.Location = new System.Drawing.Point(132, 14);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(120, 35);
            this.button_cancel.TabIndex = 10;
            this.button_cancel.Text = "取消";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_valid
            // 
            this.button_valid.BackColor = System.Drawing.Color.White;
            this.button_valid.Location = new System.Drawing.Point(6, 14);
            this.button_valid.Name = "button_valid";
            this.button_valid.Size = new System.Drawing.Size(120, 35);
            this.button_valid.TabIndex = 11;
            this.button_valid.Text = "应用";
            this.button_valid.UseVisualStyleBackColor = true;
            this.button_valid.Click += new System.EventHandler(this.button_valid_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::LocalPLC.Properties.Resources.LocalPLC24P;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(6, 268);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(718, 309);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // UserControlDynamicPara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Name = "UserControlDynamicPara";
            this.Size = new System.Drawing.Size(728, 578);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox_EmeStopDeceSpeed;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBox_Jerk;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBox_DecelerationSpeed;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox_AcceleratedSpeed;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox_MaxSpeed;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_valid;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
