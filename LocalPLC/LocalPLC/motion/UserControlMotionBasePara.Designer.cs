namespace LocalPLC.motion
{
    partial class UserControlMotionBasePara
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_HardwareInterface = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_MeasureUnit = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_AxisType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox_AxisName = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox_SignalType = new System.Windows.Forms.RichTextBox();
            this.richTextBox_DirOutput = new System.Windows.Forms.RichTextBox();
            this.richTextBox_PulseOutput = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboBox_HardwareInterface);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBox_MeasureUnit);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBox_AxisType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.richTextBox_PulseOutput);
            this.groupBox1.Controls.Add(this.richTextBox_DirOutput);
            this.groupBox1.Controls.Add(this.richTextBox_SignalType);
            this.groupBox1.Controls.Add(this.richTextBox_AxisName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1010, 641);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::LocalPLC.Properties.Resources.TM221C16R;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(100, 252);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(520, 227);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(97, 227);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "方向输出";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(415, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "脉冲输出";
            // 
            // comboBox_HardwareInterface
            // 
            this.comboBox_HardwareInterface.FormattingEnabled = true;
            this.comboBox_HardwareInterface.Location = new System.Drawing.Point(499, 101);
            this.comboBox_HardwareInterface.Name = "comboBox_HardwareInterface";
            this.comboBox_HardwareInterface.Size = new System.Drawing.Size(121, 26);
            this.comboBox_HardwareInterface.TabIndex = 2;
            this.comboBox_HardwareInterface.SelectedIndexChanged += new System.EventHandler(this.comboBox_HardwareInterface_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "信号类型";
            // 
            // comboBox_MeasureUnit
            // 
            this.comboBox_MeasureUnit.FormattingEnabled = true;
            this.comboBox_MeasureUnit.Location = new System.Drawing.Point(181, 105);
            this.comboBox_MeasureUnit.Name = "comboBox_MeasureUnit";
            this.comboBox_MeasureUnit.Size = new System.Drawing.Size(121, 26);
            this.comboBox_MeasureUnit.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(413, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "硬件接口";
            // 
            // comboBox_AxisType
            // 
            this.comboBox_AxisType.FormattingEnabled = true;
            this.comboBox_AxisType.Location = new System.Drawing.Point(499, 47);
            this.comboBox_AxisType.Name = "comboBox_AxisType";
            this.comboBox_AxisType.Size = new System.Drawing.Size(121, 26);
            this.comboBox_AxisType.TabIndex = 2;
            this.comboBox_AxisType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(97, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "测量单位";
            // 
            // richTextBox_AxisName
            // 
            this.richTextBox_AxisName.Location = new System.Drawing.Point(181, 47);
            this.richTextBox_AxisName.Multiline = false;
            this.richTextBox_AxisName.Name = "richTextBox_AxisName";
            this.richTextBox_AxisName.Size = new System.Drawing.Size(120, 26);
            this.richTextBox_AxisName.TabIndex = 1;
            this.richTextBox_AxisName.Text = "";
            this.richTextBox_AxisName.WordWrap = false;
            this.richTextBox_AxisName.TextChanged += new System.EventHandler(this.richTextBox_AxisName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(415, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "轴类型";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "轴名称";
            // 
            // richTextBox_SignalType
            // 
            this.richTextBox_SignalType.Enabled = false;
            this.richTextBox_SignalType.Location = new System.Drawing.Point(181, 166);
            this.richTextBox_SignalType.Multiline = false;
            this.richTextBox_SignalType.Name = "richTextBox_SignalType";
            this.richTextBox_SignalType.Size = new System.Drawing.Size(120, 26);
            this.richTextBox_SignalType.TabIndex = 1;
            this.richTextBox_SignalType.Text = "";
            this.richTextBox_SignalType.WordWrap = false;
            this.richTextBox_SignalType.TextChanged += new System.EventHandler(this.richTextBox_AxisName_TextChanged);
            // 
            // richTextBox_DirOutput
            // 
            this.richTextBox_DirOutput.Enabled = false;
            this.richTextBox_DirOutput.Location = new System.Drawing.Point(181, 224);
            this.richTextBox_DirOutput.Multiline = false;
            this.richTextBox_DirOutput.Name = "richTextBox_DirOutput";
            this.richTextBox_DirOutput.Size = new System.Drawing.Size(120, 26);
            this.richTextBox_DirOutput.TabIndex = 1;
            this.richTextBox_DirOutput.Text = "";
            this.richTextBox_DirOutput.WordWrap = false;
            this.richTextBox_DirOutput.TextChanged += new System.EventHandler(this.richTextBox_AxisName_TextChanged);
            // 
            // richTextBox_PulseOutput
            // 
            this.richTextBox_PulseOutput.Enabled = false;
            this.richTextBox_PulseOutput.Location = new System.Drawing.Point(499, 166);
            this.richTextBox_PulseOutput.Multiline = false;
            this.richTextBox_PulseOutput.Name = "richTextBox_PulseOutput";
            this.richTextBox_PulseOutput.Size = new System.Drawing.Size(120, 26);
            this.richTextBox_PulseOutput.TabIndex = 1;
            this.richTextBox_PulseOutput.Text = "";
            this.richTextBox_PulseOutput.WordWrap = false;
            this.richTextBox_PulseOutput.TextChanged += new System.EventHandler(this.richTextBox_AxisName_TextChanged);
            // 
            // UserControlMotionBasePara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "UserControlMotionBasePara";
            this.Size = new System.Drawing.Size(1010, 641);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBox_AxisName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_AxisType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox_HardwareInterface;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_MeasureUnit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox richTextBox_PulseOutput;
        private System.Windows.Forms.RichTextBox richTextBox_DirOutput;
        private System.Windows.Forms.RichTextBox richTextBox_SignalType;
    }
}
