namespace LocalPLC.motion
{
    partial class UserControlReverseCompensation
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.label33 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.textBox_ReverseCompensation = new System.Windows.Forms.TextBox();
            this.label_Unit = new System.Windows.Forms.Label();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_valid = new System.Windows.Forms.Button();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label33);
            this.panel5.Controls.Add(this.label31);
            this.panel5.Controls.Add(this.textBox_ReverseCompensation);
            this.panel5.Controls.Add(this.label_Unit);
            this.panel5.Location = new System.Drawing.Point(3, 44);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(741, 94);
            this.panel5.TabIndex = 6;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(108, 56);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(116, 18);
            this.label33.TabIndex = 0;
            this.label33.Text = "反向间隙补偿";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(71, 13);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(116, 18);
            this.label31.TabIndex = 0;
            this.label31.Text = "反向间隙补偿";
            // 
            // textBox_ReverseCompensation
            // 
            this.textBox_ReverseCompensation.Location = new System.Drawing.Point(309, 53);
            this.textBox_ReverseCompensation.MaxLength = 10;
            this.textBox_ReverseCompensation.Name = "textBox_ReverseCompensation";
            this.textBox_ReverseCompensation.Size = new System.Drawing.Size(140, 28);
            this.textBox_ReverseCompensation.TabIndex = 1;
            this.textBox_ReverseCompensation.TextChanged += new System.EventHandler(this.textBox_ReverseCompensation_TextChanged);
            this.textBox_ReverseCompensation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_ReverseCompensation_KeyPress);
            // 
            // label_Unit
            // 
            this.label_Unit.AutoSize = true;
            this.label_Unit.Location = new System.Drawing.Point(455, 56);
            this.label_Unit.Name = "label_Unit";
            this.label_Unit.Size = new System.Drawing.Size(26, 18);
            this.label_Unit.TabIndex = 0;
            this.label_Unit.Text = "mm";
            // 
            // button_cancel
            // 
            this.button_cancel.BackColor = System.Drawing.Color.White;
            this.button_cancel.Location = new System.Drawing.Point(129, 3);
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
            this.button_valid.Location = new System.Drawing.Point(3, 3);
            this.button_valid.Name = "button_valid";
            this.button_valid.Size = new System.Drawing.Size(120, 35);
            this.button_valid.TabIndex = 9;
            this.button_valid.Text = "应用";
            this.button_valid.UseVisualStyleBackColor = true;
            this.button_valid.Click += new System.EventHandler(this.button_valid_Click);
            // 
            // UserControlReverseCompensation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_valid);
            this.Controls.Add(this.panel5);
            this.Name = "UserControlReverseCompensation";
            this.Size = new System.Drawing.Size(747, 578);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox textBox_ReverseCompensation;
        private System.Windows.Forms.Label label_Unit;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_valid;
    }
}
