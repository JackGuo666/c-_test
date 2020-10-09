namespace LocalPLC.ModbusClient
{
    partial class ClientChannel
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
            this.groupchannel = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functioncode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cycletime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.readoffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.readlength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.writeoffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.writelength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.annotation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupchannel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupchannel
            // 
            this.groupchannel.Controls.Add(this.button2);
            this.groupchannel.Controls.Add(this.button1);
            this.groupchannel.Controls.Add(this.delete);
            this.groupchannel.Controls.Add(this.add);
            this.groupchannel.Controls.Add(this.dataGridView2);
            this.groupchannel.Location = new System.Drawing.Point(72, 325);
            this.groupchannel.Name = "groupchannel";
            this.groupchannel.Size = new System.Drawing.Size(1350, 366);
            this.groupchannel.TabIndex = 9;
            this.groupchannel.TabStop = false;
            this.groupchannel.Text = "通道";
            this.groupchannel.Enter += new System.EventHandler(this.groupchannel_Enter);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1247, 277);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1127, 277);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(1247, 49);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(75, 23);
            this.delete.TabIndex = 2;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(1127, 49);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 1;
            this.add.Text = "添加";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Name1,
            this.functioncode,
            this.cycletime,
            this.readoffset,
            this.readlength,
            this.writeoffset,
            this.writelength,
            this.annotation});
            this.dataGridView2.Location = new System.Drawing.Point(3, 90);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 62;
            this.dataGridView2.RowTemplate.Height = 30;
            this.dataGridView2.Size = new System.Drawing.Size(1347, 150);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(516, 169);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "从站编号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(622, 169);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(346, 169);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 169);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "名称：       设备";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 109);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "通道设置";
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 8;
            this.ID.Name = "ID";
            // 
            // Name1
            // 
            this.Name1.HeaderText = "名称";
            this.Name1.MinimumWidth = 8;
            this.Name1.Name = "Name1";
            // 
            // functioncode
            // 
            this.functioncode.HeaderText = "消息类型（功能码）";
            this.functioncode.MinimumWidth = 8;
            this.functioncode.Name = "functioncode";
            this.functioncode.Width = 250;
            // 
            // cycletime
            // 
            this.cycletime.HeaderText = "循环触发时间（ms）";
            this.cycletime.MinimumWidth = 8;
            this.cycletime.Name = "cycletime";
            this.cycletime.Width = 250;
            // 
            // readoffset
            // 
            this.readoffset.HeaderText = "读偏移";
            this.readoffset.MinimumWidth = 8;
            this.readoffset.Name = "readoffset";
            // 
            // readlength
            // 
            this.readlength.HeaderText = "读长度";
            this.readlength.MinimumWidth = 8;
            this.readlength.Name = "readlength";
            // 
            // writeoffset
            // 
            this.writeoffset.HeaderText = "写偏移";
            this.writeoffset.MinimumWidth = 8;
            this.writeoffset.Name = "writeoffset";
            // 
            // writelength
            // 
            this.writelength.HeaderText = "写长度";
            this.writelength.MinimumWidth = 8;
            this.writelength.Name = "writelength";
            // 
            // annotation
            // 
            this.annotation.HeaderText = "注释";
            this.annotation.MinimumWidth = 8;
            this.annotation.Name = "annotation";
            this.annotation.Width = 300;
            // 
            // ClientChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1494, 801);
            this.Controls.Add(this.groupchannel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ClientChannel";
            this.Text = "ClientChannel";
            this.Load += new System.EventHandler(this.ClientChannel_Load);
            this.groupchannel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupchannel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name1;
        private System.Windows.Forms.DataGridViewTextBoxColumn functioncode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cycletime;
        private System.Windows.Forms.DataGridViewTextBoxColumn readoffset;
        private System.Windows.Forms.DataGridViewTextBoxColumn readlength;
        private System.Windows.Forms.DataGridViewTextBoxColumn writeoffset;
        private System.Windows.Forms.DataGridViewTextBoxColumn writelength;
        private System.Windows.Forms.DataGridViewTextBoxColumn annotation;
    }
}