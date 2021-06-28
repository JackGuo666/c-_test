namespace LocalPLC.motion
{
    partial class UserControlBackOrigin
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.label32 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.comboBox_ZPulseSignal = new System.Windows.Forms.ComboBox();
            this.comboBox_BackOriginal = new System.Windows.Forms.ComboBox();
            this.comboBox_BackOriginalSelectLevel = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_valid = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label32);
            this.panel4.Controls.Add(this.label25);
            this.panel4.Controls.Add(this.comboBox_ZPulseSignal);
            this.panel4.Controls.Add(this.comboBox_BackOriginal);
            this.panel4.Controls.Add(this.comboBox_BackOriginalSelectLevel);
            this.panel4.Controls.Add(this.label30);
            this.panel4.Controls.Add(this.label26);
            this.panel4.Controls.Add(this.label28);
            this.panel4.Location = new System.Drawing.Point(6, 41);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(807, 115);
            this.panel4.TabIndex = 5;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(45, 14);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(62, 18);
            this.label32.TabIndex = 0;
            this.label32.Text = "回原点";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(44, 14);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(62, 18);
            this.label25.TabIndex = 0;
            this.label25.Text = "回原点";
            // 
            // comboBox_ZPulseSignal
            // 
            this.comboBox_ZPulseSignal.FormattingEnabled = true;
            this.comboBox_ZPulseSignal.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.comboBox_ZPulseSignal.Location = new System.Drawing.Point(293, 72);
            this.comboBox_ZPulseSignal.Name = "comboBox_ZPulseSignal";
            this.comboBox_ZPulseSignal.Size = new System.Drawing.Size(141, 26);
            this.comboBox_ZPulseSignal.TabIndex = 3;
            this.comboBox_ZPulseSignal.SelectedIndexChanged += new System.EventHandler(this.comboBox_ZPulseSignal_SelectedIndexChanged);
            this.comboBox_ZPulseSignal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_ZPulseSignal_KeyPress);
            // 
            // comboBox_BackOriginal
            // 
            this.comboBox_BackOriginal.FormattingEnabled = true;
            this.comboBox_BackOriginal.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.comboBox_BackOriginal.Location = new System.Drawing.Point(293, 38);
            this.comboBox_BackOriginal.Name = "comboBox_BackOriginal";
            this.comboBox_BackOriginal.Size = new System.Drawing.Size(141, 26);
            this.comboBox_BackOriginal.TabIndex = 3;
            this.comboBox_BackOriginal.SelectedIndexChanged += new System.EventHandler(this.comboBox_BackOriginal_SelectedIndexChanged);
            this.comboBox_BackOriginal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_BackOriginal_KeyPress);
            // 
            // comboBox_BackOriginalSelectLevel
            // 
            this.comboBox_BackOriginalSelectLevel.FormattingEnabled = true;
            this.comboBox_BackOriginalSelectLevel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.comboBox_BackOriginalSelectLevel.Location = new System.Drawing.Point(632, 38);
            this.comboBox_BackOriginalSelectLevel.Name = "comboBox_BackOriginalSelectLevel";
            this.comboBox_BackOriginalSelectLevel.Size = new System.Drawing.Size(141, 26);
            this.comboBox_BackOriginalSelectLevel.TabIndex = 3;
            this.comboBox_BackOriginalSelectLevel.SelectedIndexChanged += new System.EventHandler(this.comboBox_BackOriginalSelectLevel_SelectedIndexChanged);
            this.comboBox_BackOriginalSelectLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_BackOriginalSelectLevel_KeyPress);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(91, 75);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(89, 18);
            this.label30.TabIndex = 0;
            this.label30.Text = "Z脉冲信号";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(91, 41);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(116, 18);
            this.label26.TabIndex = 0;
            this.label26.Text = "原点输入信号";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(508, 41);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(80, 18);
            this.label28.TabIndex = 0;
            this.label28.Text = "选择电平";
            // 
            // button_cancel
            // 
            this.button_cancel.BackColor = System.Drawing.Color.White;
            this.button_cancel.Location = new System.Drawing.Point(126, 0);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(120, 35);
            this.button_cancel.TabIndex = 8;
            this.button_cancel.Text = "取消";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_valid
            // 
            this.button_valid.BackColor = System.Drawing.Color.White;
            this.button_valid.Location = new System.Drawing.Point(0, 0);
            this.button_valid.Name = "button_valid";
            this.button_valid.Size = new System.Drawing.Size(120, 35);
            this.button_valid.TabIndex = 9;
            this.button_valid.Text = "应用";
            this.button_valid.UseVisualStyleBackColor = true;
            this.button_valid.Click += new System.EventHandler(this.button_valid_Click);
            // 
            // UserControlBackOrigin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_valid);
            this.Controls.Add(this.panel4);
            this.Name = "UserControlBackOrigin";
            this.Size = new System.Drawing.Size(813, 542);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox comboBox_BackOriginalSelectLevel;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox comboBox_ZPulseSignal;
        private System.Windows.Forms.ComboBox comboBox_BackOriginal;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_valid;
    }
}
