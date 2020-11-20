namespace LocalPLC.ModbusClient
{
    partial class Clientindex
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Devname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channelname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.msg_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pollingtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startaddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trigger_offset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.error_offset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 93);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(395, 355);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "添加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(295, -3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 37);
            this.button2.TabIndex = 2;
            this.button2.Text = "删除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(401, 248);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1220, 427);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Devname,
            this.channelname,
            this.msg_type,
            this.pollingtime,
            this.startaddr,
            this.length,
            this.trigger_offset,
            this.error_offset,
            this.note});
            this.dataGridView2.Location = new System.Drawing.Point(410, 93);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 62;
            this.dataGridView2.RowTemplate.Height = 30;
            this.dataGridView2.Size = new System.Drawing.Size(804, 352);
            this.dataGridView2.TabIndex = 5;
            // 
            // Devname
            // 
            this.Devname.HeaderText = "设备名";
            this.Devname.MinimumWidth = 8;
            this.Devname.Name = "Devname";
            this.Devname.Width = 98;
            // 
            // channelname
            // 
            this.channelname.HeaderText = "通道名";
            this.channelname.MinimumWidth = 8;
            this.channelname.Name = "channelname";
            this.channelname.Width = 98;
            // 
            // msg_type
            // 
            this.msg_type.HeaderText = "功能码";
            this.msg_type.MinimumWidth = 8;
            this.msg_type.Name = "msg_type";
            this.msg_type.Width = 98;
            // 
            // pollingtime
            // 
            this.pollingtime.FillWeight = 150F;
            this.pollingtime.HeaderText = "循环触发时间";
            this.pollingtime.MinimumWidth = 8;
            this.pollingtime.Name = "pollingtime";
            this.pollingtime.Width = 107;
            // 
            // startaddr
            // 
            this.startaddr.FillWeight = 70F;
            this.startaddr.HeaderText = "起始地址";
            this.startaddr.MinimumWidth = 8;
            this.startaddr.Name = "startaddr";
            this.startaddr.Width = 91;
            // 
            // length
            // 
            this.length.FillWeight = 70F;
            this.length.HeaderText = "长度";
            this.length.MinimumWidth = 8;
            this.length.Name = "length";
            this.length.Width = 75;
            // 
            // trigger_offset
            // 
            this.trigger_offset.HeaderText = "触发变量";
            this.trigger_offset.MinimumWidth = 8;
            this.trigger_offset.Name = "trigger_offset";
            this.trigger_offset.Width = 91;
            // 
            // error_offset
            // 
            this.error_offset.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.error_offset.HeaderText = "错误偏移";
            this.error_offset.MinimumWidth = 8;
            this.error_offset.Name = "error_offset";
            this.error_offset.Width = 91;
            // 
            // note
            // 
            this.note.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.note.HeaderText = "注释";
            this.note.MinimumWidth = 8;
            this.note.Name = "note";
            this.note.Width = 75;
            // 
            // Clientindex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "Clientindex";
            this.Size = new System.Drawing.Size(1282, 448);
            this.Load += new System.EventHandler(this.Clientindex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Devname;
        private System.Windows.Forms.DataGridViewTextBoxColumn channelname;
        private System.Windows.Forms.DataGridViewTextBoxColumn msg_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn pollingtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn startaddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn length;
        private System.Windows.Forms.DataGridViewTextBoxColumn trigger_offset;
        private System.Windows.Forms.DataGridViewTextBoxColumn error_offset;
        private System.Windows.Forms.DataGridViewTextBoxColumn note;
    }
}
