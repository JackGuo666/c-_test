namespace test_usercontrol
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.userControl11 = new MyControl.UserControl1();
            this.popUserControl21 = new MyControl.PopUserControl2();
            this.SuspendLayout();
            // 
            // userControl11
            // 
            this.userControl11.BackColor = System.Drawing.Color.Transparent;
            this.userControl11.CheckIndex = 0;
            this.userControl11.Location = new System.Drawing.Point(149, 120);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(234, 92);
            this.userControl11.TabIndex = 0;
            // 
            // popUserControl21
            // 
            this.popUserControl21.BackColor = System.Drawing.Color.Transparent;
            this.popUserControl21.Location = new System.Drawing.Point(186, 248);
            this.popUserControl21.Name = "popUserControl21";
            this.popUserControl21.PopHeight = 0;
            this.popUserControl21.PopText = null;
            this.popUserControl21.PopWidth = 0;
            this.popUserControl21.Size = new System.Drawing.Size(307, 66);
            this.popUserControl21.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.popUserControl21);
            this.Controls.Add(this.userControl11);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private MyControl.UserControl1 userControl11;
        private MyControl.PopUserControl2 popUserControl21;
    }
}

