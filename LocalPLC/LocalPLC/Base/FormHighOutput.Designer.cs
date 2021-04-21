namespace LocalPLC.Base
{
    partial class FormHighOutput
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_outputType = new System.Windows.Forms.ComboBox();
            this.comboBox_pulse = new System.Windows.Forms.ComboBox();
            this.label_pulse = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox_doubleWord = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox_preset = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_timeBase = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox_frequency = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.comboBox_outputType_pto = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_pulse_pto = new System.Windows.Forms.ComboBox();
            this.comboBox_outputMode = new System.Windows.Forms.ComboBox();
            this.label_diretion = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_direction = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "高速输出类型";
            // 
            // comboBox_outputType
            // 
            this.comboBox_outputType.FormattingEnabled = true;
            this.comboBox_outputType.Location = new System.Drawing.Point(170, 30);
            this.comboBox_outputType.Name = "comboBox_outputType";
            this.comboBox_outputType.Size = new System.Drawing.Size(133, 26);
            this.comboBox_outputType.TabIndex = 3;
            this.comboBox_outputType.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged_1);
            // 
            // comboBox_pulse
            // 
            this.comboBox_pulse.FormattingEnabled = true;
            this.comboBox_pulse.Location = new System.Drawing.Point(506, 30);
            this.comboBox_pulse.Name = "comboBox_pulse";
            this.comboBox_pulse.Size = new System.Drawing.Size(85, 26);
            this.comboBox_pulse.TabIndex = 3;
            // 
            // label_pulse
            // 
            this.label_pulse.AutoSize = true;
            this.label_pulse.Location = new System.Drawing.Point(456, 35);
            this.label_pulse.Name = "label_pulse";
            this.label_pulse.Size = new System.Drawing.Size(44, 18);
            this.label_pulse.TabIndex = 4;
            this.label_pulse.Text = "脉冲";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.comboBox_outputType);
            this.panel1.Controls.Add(this.label_pulse);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox_pulse);
            this.panel1.Location = new System.Drawing.Point(12, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 78);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.checkBox_doubleWord);
            this.panel2.Location = new System.Drawing.Point(12, 106);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(776, 66);
            this.panel2.TabIndex = 5;
            // 
            // checkBox_doubleWord
            // 
            this.checkBox_doubleWord.AutoSize = true;
            this.checkBox_doubleWord.Location = new System.Drawing.Point(46, 29);
            this.checkBox_doubleWord.Name = "checkBox_doubleWord";
            this.checkBox_doubleWord.Size = new System.Drawing.Size(70, 22);
            this.checkBox_doubleWord.TabIndex = 0;
            this.checkBox_doubleWord.Text = "双字";
            this.checkBox_doubleWord.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.textBox_preset);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.comboBox_timeBase);
            this.panel3.Location = new System.Drawing.Point(12, 178);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(776, 111);
            this.panel3.TabIndex = 5;
            // 
            // textBox_preset
            // 
            this.textBox_preset.Location = new System.Drawing.Point(114, 69);
            this.textBox_preset.Name = "textBox_preset";
            this.textBox_preset.Size = new System.Drawing.Size(121, 28);
            this.textBox_preset.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "预设";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "时基";
            // 
            // comboBox_timeBase
            // 
            this.comboBox_timeBase.FormattingEnabled = true;
            this.comboBox_timeBase.Location = new System.Drawing.Point(114, 28);
            this.comboBox_timeBase.Name = "comboBox_timeBase";
            this.comboBox_timeBase.Size = new System.Drawing.Size(93, 26);
            this.comboBox_timeBase.TabIndex = 3;
            this.comboBox_timeBase.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged_1);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.textBox_frequency);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Location = new System.Drawing.Point(12, 299);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(775, 77);
            this.panel4.TabIndex = 6;
            // 
            // textBox_frequency
            // 
            this.textBox_frequency.Location = new System.Drawing.Point(124, 27);
            this.textBox_frequency.Name = "textBox_frequency";
            this.textBox_frequency.Size = new System.Drawing.Size(121, 28);
            this.textBox_frequency.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "频率(Hz)";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.comboBox_outputType_pto);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.comboBox_pulse_pto);
            this.panel5.Controls.Add(this.comboBox_outputMode);
            this.panel5.Controls.Add(this.label_diretion);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.comboBox_direction);
            this.panel5.Location = new System.Drawing.Point(12, 382);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(775, 111);
            this.panel5.TabIndex = 6;
            // 
            // comboBox_outputType_pto
            // 
            this.comboBox_outputType_pto.FormattingEnabled = true;
            this.comboBox_outputType_pto.Location = new System.Drawing.Point(170, 19);
            this.comboBox_outputType_pto.Name = "comboBox_outputType_pto";
            this.comboBox_outputType_pto.Size = new System.Drawing.Size(133, 26);
            this.comboBox_outputType_pto.TabIndex = 6;
            this.comboBox_outputType_pto.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(456, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 18);
            this.label8.TabIndex = 8;
            this.label8.Text = "脉冲";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(39, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 18);
            this.label9.TabIndex = 5;
            this.label9.Text = "高速输出类型";
            // 
            // comboBox_pulse_pto
            // 
            this.comboBox_pulse_pto.FormattingEnabled = true;
            this.comboBox_pulse_pto.Location = new System.Drawing.Point(506, 19);
            this.comboBox_pulse_pto.Name = "comboBox_pulse_pto";
            this.comboBox_pulse_pto.Size = new System.Drawing.Size(85, 26);
            this.comboBox_pulse_pto.TabIndex = 7;
            // 
            // comboBox_outputMode
            // 
            this.comboBox_outputMode.FormattingEnabled = true;
            this.comboBox_outputMode.Location = new System.Drawing.Point(170, 71);
            this.comboBox_outputMode.Name = "comboBox_outputMode";
            this.comboBox_outputMode.Size = new System.Drawing.Size(133, 26);
            this.comboBox_outputMode.TabIndex = 3;
            this.comboBox_outputMode.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged_1);
            // 
            // label_diretion
            // 
            this.label_diretion.AutoSize = true;
            this.label_diretion.Location = new System.Drawing.Point(456, 74);
            this.label_diretion.Name = "label_diretion";
            this.label_diretion.Size = new System.Drawing.Size(44, 18);
            this.label_diretion.TabIndex = 4;
            this.label_diretion.Text = "方向";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(75, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "输出模式";
            // 
            // comboBox_direction
            // 
            this.comboBox_direction.FormattingEnabled = true;
            this.comboBox_direction.Location = new System.Drawing.Point(506, 69);
            this.comboBox_direction.Name = "comboBox_direction";
            this.comboBox_direction.Size = new System.Drawing.Size(85, 26);
            this.comboBox_direction.TabIndex = 3;
            // 
            // FormHighOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 492);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormHighOutput";
            this.Text = "FormHighOutput";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHighOutput_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_outputType;
        private System.Windows.Forms.Label label_pulse;
        private System.Windows.Forms.ComboBox comboBox_pulse;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBox_doubleWord;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox_preset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_timeBase;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBox_frequency;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox comboBox_outputMode;
        private System.Windows.Forms.Label label_diretion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox_direction;
        private System.Windows.Forms.ComboBox comboBox_outputType_pto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox_pulse_pto;
    }
}