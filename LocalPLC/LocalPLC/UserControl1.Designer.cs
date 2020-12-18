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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode41 = new System.Windows.Forms.TreeNode("DI", 2, 2);
            System.Windows.Forms.TreeNode treeNode42 = new System.Windows.Forms.TreeNode("DO", 3, 3);
            System.Windows.Forms.TreeNode treeNode43 = new System.Windows.Forms.TreeNode("正交编码器", 7, 7);
            System.Windows.Forms.TreeNode treeNode44 = new System.Windows.Forms.TreeNode("双向脉冲计数", 5, 5);
            System.Windows.Forms.TreeNode treeNode45 = new System.Windows.Forms.TreeNode("单向脉冲计数", 6, 6);
            System.Windows.Forms.TreeNode treeNode46 = new System.Windows.Forms.TreeNode("高速计数器", 4, 4, new System.Windows.Forms.TreeNode[] {
            treeNode43,
            treeNode44,
            treeNode45});
            System.Windows.Forms.TreeNode treeNode47 = new System.Windows.Forms.TreeNode("PTO", 10, 10);
            System.Windows.Forms.TreeNode treeNode48 = new System.Windows.Forms.TreeNode("PWM", 9, 9);
            System.Windows.Forms.TreeNode treeNode49 = new System.Windows.Forms.TreeNode("高速输出", 8, 8, new System.Windows.Forms.TreeNode[] {
            treeNode47,
            treeNode48});
            System.Windows.Forms.TreeNode treeNode50 = new System.Windows.Forms.TreeNode("本体COM1", 12, 12);
            System.Windows.Forms.TreeNode treeNode51 = new System.Windows.Forms.TreeNode("本体ETH1", 13, 13);
            System.Windows.Forms.TreeNode treeNode52 = new System.Windows.Forms.TreeNode("通信线路", 11, 11, new System.Windows.Forms.TreeNode[] {
            treeNode50,
            treeNode51});
            System.Windows.Forms.TreeNode treeNode53 = new System.Windows.Forms.TreeNode("基本配置", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode41,
            treeNode42,
            treeNode46,
            treeNode49,
            treeNode52});
            System.Windows.Forms.TreeNode treeNode54 = new System.Windows.Forms.TreeNode("ModbusTCP-Client", 16, 16);
            System.Windows.Forms.TreeNode treeNode55 = new System.Windows.Forms.TreeNode("ModbusTCP-Server", 15, 15);
            System.Windows.Forms.TreeNode treeNode56 = new System.Windows.Forms.TreeNode("ModbusRTU-Master", 17, 17);
            System.Windows.Forms.TreeNode treeNode57 = new System.Windows.Forms.TreeNode("ModbusRTU-Slave", 18, 18);
            System.Windows.Forms.TreeNode treeNode58 = new System.Windows.Forms.TreeNode("Modbus", 14, 14, new System.Windows.Forms.TreeNode[] {
            treeNode54,
            treeNode55,
            treeNode56,
            treeNode57});
            System.Windows.Forms.TreeNode treeNode59 = new System.Windows.Forms.TreeNode("AI", 22, 23);
            System.Windows.Forms.TreeNode treeNode60 = new System.Windows.Forms.TreeNode("AO", 23, 23);
            System.Windows.Forms.TreeNode treeNode61 = new System.Windows.Forms.TreeNode("面板扩展", 20, 20, new System.Windows.Forms.TreeNode[] {
            treeNode59,
            treeNode60});
            System.Windows.Forms.TreeNode treeNode62 = new System.Windows.Forms.TreeNode("扩展串口模块1(型号)", 12, 12);
            System.Windows.Forms.TreeNode treeNode63 = new System.Windows.Forms.TreeNode("扩展网口模块1(型号)", 13, 13);
            System.Windows.Forms.TreeNode treeNode64 = new System.Windows.Forms.TreeNode("左模块", 21, 21, new System.Windows.Forms.TreeNode[] {
            treeNode62,
            treeNode63});
            System.Windows.Forms.TreeNode treeNode65 = new System.Windows.Forms.TreeNode("AI", 22, 22);
            System.Windows.Forms.TreeNode treeNode66 = new System.Windows.Forms.TreeNode("模块1(型号)", 22, 22, new System.Windows.Forms.TreeNode[] {
            treeNode65});
            System.Windows.Forms.TreeNode treeNode67 = new System.Windows.Forms.TreeNode("AO", 23, 23);
            System.Windows.Forms.TreeNode treeNode68 = new System.Windows.Forms.TreeNode("模块2(型号)", 23, 23, new System.Windows.Forms.TreeNode[] {
            treeNode67});
            System.Windows.Forms.TreeNode treeNode69 = new System.Windows.Forms.TreeNode("DO", 2, 2);
            System.Windows.Forms.TreeNode treeNode70 = new System.Windows.Forms.TreeNode("模块3(型号)", 2, 2, new System.Windows.Forms.TreeNode[] {
            treeNode69});
            System.Windows.Forms.TreeNode treeNode71 = new System.Windows.Forms.TreeNode("DI", 3, 3);
            System.Windows.Forms.TreeNode treeNode72 = new System.Windows.Forms.TreeNode("模块4(型号)", 3, 3, new System.Windows.Forms.TreeNode[] {
            treeNode71});
            System.Windows.Forms.TreeNode treeNode73 = new System.Windows.Forms.TreeNode("右模块", 24, 24, new System.Windows.Forms.TreeNode[] {
            treeNode66,
            treeNode68,
            treeNode70,
            treeNode72});
            System.Windows.Forms.TreeNode treeNode74 = new System.Windows.Forms.TreeNode("扩展模块", 19, 19, new System.Windows.Forms.TreeNode[] {
            treeNode61,
            treeNode64,
            treeNode73});
            System.Windows.Forms.TreeNode treeNode75 = new System.Windows.Forms.TreeNode("canopen", 25, 25);
            System.Windows.Forms.TreeNode treeNode76 = new System.Windows.Forms.TreeNode("运动控制", 26, 26);
            System.Windows.Forms.TreeNode treeNode77 = new System.Windows.Forms.TreeNode("modbus工具", 28, 28);
            System.Windows.Forms.TreeNode treeNode78 = new System.Windows.Forms.TreeNode("canopen工具", 29, 29);
            System.Windows.Forms.TreeNode treeNode79 = new System.Windows.Forms.TreeNode("shell工具", 27, 27, new System.Windows.Forms.TreeNode[] {
            treeNode77,
            treeNode78});
            System.Windows.Forms.TreeNode treeNode80 = new System.Windows.Forms.TreeNode("配置", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode53,
            treeNode58,
            treeNode74,
            treeNode75,
            treeNode76,
            treeNode79});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl1));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ModbusWindow = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            treeNode41.ImageIndex = 2;
            treeNode41.Name = "DI";
            treeNode41.SelectedImageIndex = 2;
            treeNode41.Text = "DI";
            treeNode42.ImageIndex = 3;
            treeNode42.Name = "DO";
            treeNode42.SelectedImageIndex = 3;
            treeNode42.Text = "DO";
            treeNode43.ImageIndex = 7;
            treeNode43.Name = "正交编码器";
            treeNode43.SelectedImageIndex = 7;
            treeNode43.Text = "正交编码器";
            treeNode44.ImageIndex = 5;
            treeNode44.Name = "双向脉冲计数";
            treeNode44.SelectedImageIndex = 5;
            treeNode44.Text = "双向脉冲计数";
            treeNode45.ImageIndex = 6;
            treeNode45.Name = "单向脉冲计数";
            treeNode45.SelectedImageIndex = 6;
            treeNode45.Text = "单向脉冲计数";
            treeNode46.ImageIndex = 4;
            treeNode46.Name = "高速计数器";
            treeNode46.SelectedImageIndex = 4;
            treeNode46.Text = "高速计数器";
            treeNode47.ImageIndex = 10;
            treeNode47.Name = "PTO";
            treeNode47.SelectedImageIndex = 10;
            treeNode47.Text = "PTO";
            treeNode48.ImageIndex = 9;
            treeNode48.Name = "PWM";
            treeNode48.SelectedImageIndex = 9;
            treeNode48.Text = "PWM";
            treeNode49.ImageIndex = 8;
            treeNode49.Name = "高速输出";
            treeNode49.SelectedImageIndex = 8;
            treeNode49.Text = "高速输出";
            treeNode50.ImageIndex = 12;
            treeNode50.Name = "本体COM1";
            treeNode50.SelectedImageIndex = 12;
            treeNode50.Text = "本体COM1";
            treeNode51.ImageIndex = 13;
            treeNode51.Name = "本体ETH1";
            treeNode51.SelectedImageIndex = 13;
            treeNode51.Text = "本体ETH1";
            treeNode52.ImageIndex = 11;
            treeNode52.Name = "通信线路";
            treeNode52.SelectedImageIndex = 11;
            treeNode52.Text = "通信线路";
            treeNode53.ImageIndex = 1;
            treeNode53.Name = "基本配置";
            treeNode53.SelectedImageIndex = 1;
            treeNode53.Text = "基本配置";
            treeNode54.ImageIndex = 16;
            treeNode54.Name = "节点00";
            treeNode54.SelectedImageIndex = 16;
            treeNode54.Text = "ModbusTCP-Client";
            treeNode55.ImageIndex = 15;
            treeNode55.Name = "节点01";
            treeNode55.SelectedImageIndex = 15;
            treeNode55.Text = "ModbusTCP-Server";
            treeNode56.ImageIndex = 17;
            treeNode56.Name = "节点02";
            treeNode56.SelectedImageIndex = 17;
            treeNode56.Text = "ModbusRTU-Master";
            treeNode57.ImageIndex = 18;
            treeNode57.Name = "节点03";
            treeNode57.SelectedImageIndex = 18;
            treeNode57.Text = "ModbusRTU-Slave";
            treeNode58.ImageIndex = 14;
            treeNode58.Name = "节点0";
            treeNode58.SelectedImageIndex = 14;
            treeNode58.Text = "Modbus";
            treeNode59.ImageIndex = 22;
            treeNode59.Name = "AI";
            treeNode59.SelectedImageIndex = 23;
            treeNode59.Text = "AI";
            treeNode60.ImageIndex = 23;
            treeNode60.Name = "AO";
            treeNode60.SelectedImageIndex = 23;
            treeNode60.Text = "AO";
            treeNode61.ImageIndex = 20;
            treeNode61.Name = "面板扩展";
            treeNode61.SelectedImageIndex = 20;
            treeNode61.Text = "面板扩展";
            treeNode62.ImageIndex = 12;
            treeNode62.Name = "扩展串口模块1(型号)";
            treeNode62.SelectedImageIndex = 12;
            treeNode62.Text = "扩展串口模块1(型号)";
            treeNode63.ImageIndex = 13;
            treeNode63.Name = "扩展网口模块1(型号)";
            treeNode63.SelectedImageIndex = 13;
            treeNode63.Text = "扩展网口模块1(型号)";
            treeNode64.ImageIndex = 21;
            treeNode64.Name = "左模块";
            treeNode64.SelectedImageIndex = 21;
            treeNode64.Text = "左模块";
            treeNode65.ImageIndex = 22;
            treeNode65.Name = "AI";
            treeNode65.SelectedImageIndex = 22;
            treeNode65.Text = "AI";
            treeNode66.ImageIndex = 22;
            treeNode66.Name = "模块1(型号)";
            treeNode66.SelectedImageIndex = 22;
            treeNode66.Text = "模块1(型号)";
            treeNode67.ImageIndex = 23;
            treeNode67.Name = "AO";
            treeNode67.SelectedImageIndex = 23;
            treeNode67.Text = "AO";
            treeNode68.ImageIndex = 23;
            treeNode68.Name = "模块2(型号)";
            treeNode68.SelectedImageIndex = 23;
            treeNode68.Text = "模块2(型号)";
            treeNode69.ImageIndex = 2;
            treeNode69.Name = "DI";
            treeNode69.SelectedImageIndex = 2;
            treeNode69.Text = "DO";
            treeNode70.ImageIndex = 2;
            treeNode70.Name = "模块3(型号)";
            treeNode70.SelectedImageIndex = 2;
            treeNode70.Text = "模块3(型号)";
            treeNode71.ImageIndex = 3;
            treeNode71.Name = "DI";
            treeNode71.SelectedImageIndex = 3;
            treeNode71.Text = "DI";
            treeNode72.ImageIndex = 3;
            treeNode72.Name = "模块4(型号)";
            treeNode72.SelectedImageIndex = 3;
            treeNode72.Text = "模块4(型号)";
            treeNode73.ImageIndex = 24;
            treeNode73.Name = "右模块";
            treeNode73.SelectedImageIndex = 24;
            treeNode73.Text = "右模块";
            treeNode74.ImageIndex = 19;
            treeNode74.Name = "扩展模块";
            treeNode74.SelectedImageIndex = 19;
            treeNode74.Text = "扩展模块";
            treeNode75.ImageIndex = 25;
            treeNode75.Name = "canopen";
            treeNode75.SelectedImageIndex = 25;
            treeNode75.Text = "canopen";
            treeNode76.ImageIndex = 26;
            treeNode76.Name = "运动控制";
            treeNode76.SelectedImageIndex = 26;
            treeNode76.Text = "运动控制";
            treeNode77.ImageIndex = 28;
            treeNode77.Name = "节点1";
            treeNode77.SelectedImageIndex = 28;
            treeNode77.Text = "modbus工具";
            treeNode78.ImageIndex = 29;
            treeNode78.Name = "节点2";
            treeNode78.SelectedImageIndex = 29;
            treeNode78.Text = "canopen工具";
            treeNode79.ImageIndex = 27;
            treeNode79.Name = "节点0";
            treeNode79.SelectedImageIndex = 27;
            treeNode79.Text = "shell工具";
            treeNode80.ImageIndex = 0;
            treeNode80.Name = "配置";
            treeNode80.SelectedImageIndex = 0;
            treeNode80.Text = "配置";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode80});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(358, 750);
            this.treeView1.TabIndex = 6;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(189, 34);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
            this.testToolStripMenuItem.Text = "生成配置文件";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "config.ico");
            this.imageList1.Images.SetKeyName(1, "base_config.ico");
            this.imageList1.Images.SetKeyName(2, "base_di.ico");
            this.imageList1.Images.SetKeyName(3, "base_do.ico");
            this.imageList1.Images.SetKeyName(4, "high_speed_in.ico");
            this.imageList1.Images.SetKeyName(5, "pulse_direction.ico");
            this.imageList1.Images.SetKeyName(6, "single_pulse.ico");
            this.imageList1.Images.SetKeyName(7, "two_pulse.ico");
            this.imageList1.Images.SetKeyName(8, "high_out.ico");
            this.imageList1.Images.SetKeyName(9, "pwm.ico");
            this.imageList1.Images.SetKeyName(10, "pto.ico");
            this.imageList1.Images.SetKeyName(11, "communication_line.ico");
            this.imageList1.Images.SetKeyName(12, "com.png");
            this.imageList1.Images.SetKeyName(13, "eth.png");
            this.imageList1.Images.SetKeyName(14, "modbus.ico");
            this.imageList1.Images.SetKeyName(15, "server.png");
            this.imageList1.Images.SetKeyName(16, "client.png");
            this.imageList1.Images.SetKeyName(17, "master.png");
            this.imageList1.Images.SetKeyName(18, "slave.png");
            this.imageList1.Images.SetKeyName(19, "extension.ico");
            this.imageList1.Images.SetKeyName(20, "panel.png");
            this.imageList1.Images.SetKeyName(21, "net_left_extend.png");
            this.imageList1.Images.SetKeyName(22, "ai.png");
            this.imageList1.Images.SetKeyName(23, "di.png");
            this.imageList1.Images.SetKeyName(24, "right_module.ico");
            this.imageList1.Images.SetKeyName(25, "canopen.ico");
            this.imageList1.Images.SetKeyName(26, "motion_control.png");
            this.imageList1.Images.SetKeyName(27, "shell.png");
            this.imageList1.Images.SetKeyName(28, "shell_modbus.png");
            this.imageList1.Images.SetKeyName(29, "shell_canopen.png");
            // 
            // ModbusWindow
            // 
            this.ModbusWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModbusWindow.Location = new System.Drawing.Point(358, 0);
            this.ModbusWindow.Margin = new System.Windows.Forms.Padding(4);
            this.ModbusWindow.Name = "ModbusWindow";
            this.ModbusWindow.Padding = new System.Windows.Forms.Padding(4);
            this.ModbusWindow.Size = new System.Drawing.Size(1145, 750);
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
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox ModbusWindow;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
    }
}
