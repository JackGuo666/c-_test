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
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label33);
            this.panel5.Controls.Add(this.label31);
            this.panel5.Controls.Add(this.textBox12);
            this.panel5.Controls.Add(this.label34);
            this.panel5.Location = new System.Drawing.Point(3, 3);
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
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(309, 53);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(140, 28);
            this.textBox12.TabIndex = 1;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(455, 56);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(26, 18);
            this.label34.TabIndex = 0;
            this.label34.Text = "mm";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1, 90);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(745, 487);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "图片";
            // 
            // UserControlReverseCompensation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.richTextBox1);
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
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}
