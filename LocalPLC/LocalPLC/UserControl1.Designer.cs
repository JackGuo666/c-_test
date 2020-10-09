namespace LocalPLC
{
    partial class UserControl1
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("MobusTCP-Client");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("ModbusTCP-Server");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("ModbusRTU-Master");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("ModbusRTU-Slave");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Modbus", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.ModbusWindow = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(4, 190);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点00";
            treeNode1.Text = "MobusTCP-Client";
            treeNode2.Name = "节点01";
            treeNode2.Text = "ModbusTCP-Server";
            treeNode3.Name = "节点02";
            treeNode3.Text = "ModbusRTU-Master";
            treeNode4.Name = "节点03";
            treeNode4.Text = "ModbusRTU-Slave";
            treeNode5.Name = "节点0";
            treeNode5.Text = "Modbus";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.treeView1.Size = new System.Drawing.Size(223, 448);
            this.treeView1.TabIndex = 6;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // ModbusWindow
            // 
            this.ModbusWindow.Location = new System.Drawing.Point(356, 93);
            this.ModbusWindow.Margin = new System.Windows.Forms.Padding(4);
            this.ModbusWindow.Name = "ModbusWindow";
            this.ModbusWindow.Padding = new System.Windows.Forms.Padding(4);
            this.ModbusWindow.Size = new System.Drawing.Size(1200, 600);
            this.ModbusWindow.TabIndex = 7;
            this.ModbusWindow.TabStop = false;
            this.ModbusWindow.Text = "配置";
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ModbusWindow);
            this.Controls.Add(this.treeView1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(1503, 750);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox ModbusWindow;
    }
}
