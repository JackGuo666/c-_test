namespace LocalPLC.Base
{
    partial class LocalPLC24P
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Ethernet_1 = new LocalPLC.Base.pictest();
            this.Serial_Line_2 = new LocalPLC.Base.pictest();
            this.Serial_Line_1 = new LocalPLC.Base.pictest();
            this.pictest2 = new LocalPLC.Base.pictest();
            this.pictest1 = new LocalPLC.Base.pictest();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ethernet_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Serial_Line_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Serial_Line_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictest2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictest1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::LocalPLC.Properties.Resources.LocalPLC24P;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(453, 280);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // Ethernet_1
            // 
            this.Ethernet_1.BackColor = System.Drawing.Color.Transparent;
            this.Ethernet_1.Location = new System.Drawing.Point(7, 206);
            this.Ethernet_1.MValue = false;
            this.Ethernet_1.Name = "Ethernet_1";
            this.Ethernet_1.Size = new System.Drawing.Size(76, 46);
            this.Ethernet_1.TabIndex = 6;
            this.Ethernet_1.TabStop = false;
            this.Ethernet_1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictest4_MouseDoubleClick);
            // 
            // Serial_Line_2
            // 
            this.Serial_Line_2.BackColor = System.Drawing.Color.Transparent;
            this.Serial_Line_2.Location = new System.Drawing.Point(7, 27);
            this.Serial_Line_2.MValue = false;
            this.Serial_Line_2.Name = "Serial_Line_2";
            this.Serial_Line_2.Size = new System.Drawing.Size(52, 30);
            this.Serial_Line_2.TabIndex = 5;
            this.Serial_Line_2.TabStop = false;
            this.Serial_Line_2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictest3_MouseDoubleClick);
            // 
            // Serial_Line_1
            // 
            this.Serial_Line_1.BackColor = System.Drawing.Color.Transparent;
            this.Serial_Line_1.Location = new System.Drawing.Point(89, 206);
            this.Serial_Line_1.MValue = false;
            this.Serial_Line_1.Name = "Serial_Line_1";
            this.Serial_Line_1.Size = new System.Drawing.Size(34, 46);
            this.Serial_Line_1.TabIndex = 5;
            this.Serial_Line_1.TabStop = false;
            this.Serial_Line_1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictest3_MouseDoubleClick);
            // 
            // pictest2
            // 
            this.pictest2.BackColor = System.Drawing.Color.Transparent;
            this.pictest2.Location = new System.Drawing.Point(161, 27);
            this.pictest2.MValue = false;
            this.pictest2.Name = "pictest2";
            this.pictest2.Size = new System.Drawing.Size(289, 30);
            this.pictest2.TabIndex = 4;
            this.pictest2.TabStop = false;
            this.pictest2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictest2_MouseDoubleClick);
            // 
            // pictest1
            // 
            this.pictest1.BackColor = System.Drawing.Color.Transparent;
            this.pictest1.Location = new System.Drawing.Point(185, 222);
            this.pictest1.MValue = false;
            this.pictest1.Name = "pictest1";
            this.pictest1.Size = new System.Drawing.Size(265, 30);
            this.pictest1.TabIndex = 3;
            this.pictest1.TabStop = false;
            this.pictest1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictest1_MouseDoubleClick);
            // 
            // LocalPLC24P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.Ethernet_1);
            this.Controls.Add(this.Serial_Line_2);
            this.Controls.Add(this.Serial_Line_1);
            this.Controls.Add(this.pictest2);
            this.Controls.Add(this.pictest1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "LocalPLC24P";
            this.Size = new System.Drawing.Size(453, 280);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ethernet_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Serial_Line_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Serial_Line_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictest2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictest1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private LocalPLC.Base.pictest pictest1;
        private LocalPLC.Base.pictest pictest2;
        private pictest Serial_Line_1;
        private pictest Ethernet_1;
        private pictest Serial_Line_2;
    }
}
