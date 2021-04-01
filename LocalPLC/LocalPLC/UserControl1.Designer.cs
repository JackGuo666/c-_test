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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("DI", 2, 2);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("DO", 3, 3);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("高速计数器", 4, 4);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("高速输出", 8, 8);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("通信线路", 11, 11);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("基本配置", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("ModbusTCP-Client", 16, 16);
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Modbus-Server", 15, 15);
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("ModbusRTU-Master", 17, 17);
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Modbus", 14, 14, new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("AI", 22, 23);
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("AO", 23, 23);
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("面板扩展", 20, 20, new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("扩展串口模块1(型号)", 12, 12);
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("扩展网口模块1(型号)", 13, 13);
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("左模块", 21, 21, new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("AI", 22, 22);
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("模块1(型号)", 22, 22, new System.Windows.Forms.TreeNode[] {
            treeNode17});
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("AO", 23, 23);
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("模块2(型号)", 23, 23, new System.Windows.Forms.TreeNode[] {
            treeNode19});
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("DO", 2, 2);
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("模块3(型号)", 2, 2, new System.Windows.Forms.TreeNode[] {
            treeNode21});
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("DI", 3, 3);
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("模块4(型号)", 3, 3, new System.Windows.Forms.TreeNode[] {
            treeNode23});
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("右模块", 24, 24, new System.Windows.Forms.TreeNode[] {
            treeNode18,
            treeNode20,
            treeNode22,
            treeNode24});
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("扩展模块", 19, 19, new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode16,
            treeNode25});
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("canopen", 25, 25);
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("运动控制", 26, 26);
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("modbus工具", 28, 28);
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("canopen工具", 29, 29);
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("shell工具", 27, 27, new System.Windows.Forms.TreeNode[] {
            treeNode29,
            treeNode30});
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("配置", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode10,
            treeNode26,
            treeNode27,
            treeNode28,
            treeNode31});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl1));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ModbusWindow = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
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
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "DI";
            treeNode1.SelectedImageIndex = 2;
            treeNode1.Tag = "DI";
            treeNode1.Text = "DI";
            treeNode2.ImageIndex = 3;
            treeNode2.Name = "DO";
            treeNode2.SelectedImageIndex = 3;
            treeNode2.Tag = "DO";
            treeNode2.Text = "DO";
            treeNode3.ImageIndex = 4;
            treeNode3.Name = "高速计数器";
            treeNode3.SelectedImageIndex = 4;
            treeNode3.Tag = "HSC";
            treeNode3.Text = "高速计数器";
            treeNode4.ImageIndex = 8;
            treeNode4.Name = "高速输出";
            treeNode4.SelectedImageIndex = 8;
            treeNode4.Tag = "HSP";
            treeNode4.Text = "高速输出";
            treeNode5.ImageIndex = 11;
            treeNode5.Name = "通信线路";
            treeNode5.SelectedImageIndex = 11;
            treeNode5.Tag = "COMMUNICATION_LINE";
            treeNode5.Text = "通信线路";
            treeNode6.ImageIndex = 1;
            treeNode6.Name = "基本配置";
            treeNode6.SelectedImageIndex = 1;
            treeNode6.Tag = "BASE_CONFIG";
            treeNode6.Text = "基本配置";
            treeNode7.ImageIndex = 16;
            treeNode7.Name = "节点00";
            treeNode7.SelectedImageIndex = 16;
            treeNode7.Tag = "MODBUS_CLIENT";
            treeNode7.Text = "ModbusTCP-Client";
            treeNode8.ImageIndex = 15;
            treeNode8.Name = "节点01";
            treeNode8.SelectedImageIndex = 15;
            treeNode8.Tag = "MODBUS_SERVER";
            treeNode8.Text = "Modbus-Server";
            treeNode9.ImageIndex = 17;
            treeNode9.Name = "节点02";
            treeNode9.SelectedImageIndex = 17;
            treeNode9.Tag = "MODBUS_MASTER";
            treeNode9.Text = "ModbusRTU-Master";
            treeNode10.ImageIndex = 14;
            treeNode10.Name = "节点0";
            treeNode10.SelectedImageIndex = 14;
            treeNode10.Tag = "MODBUS";
            treeNode10.Text = "Modbus";
            treeNode11.ImageIndex = 22;
            treeNode11.Name = "AI";
            treeNode11.SelectedImageIndex = 23;
            treeNode11.Tag = "EXPAND_AI";
            treeNode11.Text = "AI";
            treeNode12.ImageIndex = 23;
            treeNode12.Name = "AO";
            treeNode12.SelectedImageIndex = 23;
            treeNode12.Tag = "EXPAND_AO";
            treeNode12.Text = "AO";
            treeNode13.ImageIndex = 20;
            treeNode13.Name = "面板扩展";
            treeNode13.SelectedImageIndex = 20;
            treeNode13.Tag = "EXPAND_PANEL";
            treeNode13.Text = "面板扩展";
            treeNode14.ImageIndex = 12;
            treeNode14.Name = "扩展串口模块1(型号)";
            treeNode14.SelectedImageIndex = 12;
            treeNode14.Tag = "EXPAND_LEFT1";
            treeNode14.Text = "扩展串口模块1(型号)";
            treeNode15.ImageIndex = 13;
            treeNode15.Name = "扩展网口模块1(型号)";
            treeNode15.SelectedImageIndex = 13;
            treeNode15.Tag = "EXPAND_LEFT2";
            treeNode15.Text = "扩展网口模块1(型号)";
            treeNode16.ImageIndex = 21;
            treeNode16.Name = "左模块";
            treeNode16.SelectedImageIndex = 21;
            treeNode16.Tag = "EXPAND_LEFT";
            treeNode16.Text = "左模块";
            treeNode17.ImageIndex = 22;
            treeNode17.Name = "AI";
            treeNode17.SelectedImageIndex = 22;
            treeNode17.Tag = "EXPAND_RIGHT1";
            treeNode17.Text = "AI";
            treeNode18.ImageIndex = 22;
            treeNode18.Name = "模块1(型号)";
            treeNode18.SelectedImageIndex = 22;
            treeNode18.Tag = "EXPAND_RIGHT1";
            treeNode18.Text = "模块1(型号)";
            treeNode19.ImageIndex = 23;
            treeNode19.Name = "AO";
            treeNode19.SelectedImageIndex = 23;
            treeNode19.Tag = "EXPAND_RIGHT2";
            treeNode19.Text = "AO";
            treeNode20.ImageIndex = 23;
            treeNode20.Name = "模块2(型号)";
            treeNode20.SelectedImageIndex = 23;
            treeNode20.Tag = "EXPAND_RIGHT2";
            treeNode20.Text = "模块2(型号)";
            treeNode21.ImageIndex = 2;
            treeNode21.Name = "DI";
            treeNode21.SelectedImageIndex = 2;
            treeNode21.Tag = "EXPAND_RIGHT3";
            treeNode21.Text = "DO";
            treeNode22.ImageIndex = 2;
            treeNode22.Name = "模块3(型号)";
            treeNode22.SelectedImageIndex = 2;
            treeNode22.Tag = "EXPAND_RIGHT3";
            treeNode22.Text = "模块3(型号)";
            treeNode23.ImageIndex = 3;
            treeNode23.Name = "DI";
            treeNode23.SelectedImageIndex = 3;
            treeNode23.Tag = "EXPAND_RIGHT4";
            treeNode23.Text = "DI";
            treeNode24.ImageIndex = 3;
            treeNode24.Name = "模块4(型号)";
            treeNode24.SelectedImageIndex = 3;
            treeNode24.Tag = "EXPAND_RIGHT4";
            treeNode24.Text = "模块4(型号)";
            treeNode25.ImageIndex = 24;
            treeNode25.Name = "右模块";
            treeNode25.SelectedImageIndex = 24;
            treeNode25.Tag = "EXPAND_RIGHT";
            treeNode25.Text = "右模块";
            treeNode26.ImageIndex = 19;
            treeNode26.Name = "扩展模块";
            treeNode26.SelectedImageIndex = 19;
            treeNode26.Tag = "EXPAND";
            treeNode26.Text = "扩展模块";
            treeNode27.ImageIndex = 25;
            treeNode27.Name = "canopen";
            treeNode27.SelectedImageIndex = 25;
            treeNode27.Tag = "CANOPEN";
            treeNode27.Text = "canopen";
            treeNode28.ImageIndex = 26;
            treeNode28.Name = "运动控制";
            treeNode28.SelectedImageIndex = 26;
            treeNode28.Tag = "MOTION_CONTROL";
            treeNode28.Text = "运动控制";
            treeNode29.ImageIndex = 28;
            treeNode29.Name = "节点1";
            treeNode29.SelectedImageIndex = 28;
            treeNode29.Tag = "SHELL_MODBUS";
            treeNode29.Text = "modbus工具";
            treeNode30.ImageIndex = 29;
            treeNode30.Name = "节点2";
            treeNode30.SelectedImageIndex = 29;
            treeNode30.Tag = "SHELL_CANOPEN";
            treeNode30.Text = "canopen工具";
            treeNode31.ImageIndex = 27;
            treeNode31.Name = "节点0";
            treeNode31.SelectedImageIndex = 27;
            treeNode31.Tag = "SHELL";
            treeNode31.Text = "shell工具";
            treeNode32.ImageIndex = 0;
            treeNode32.Name = "配置";
            treeNode32.SelectedImageIndex = 0;
            treeNode32.Tag = "CONFIG";
            treeNode32.Text = "配置";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode32});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(358, 750);
            this.treeView1.TabIndex = 6;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
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
            this.testToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
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
            this.ModbusWindow.BackColor = System.Drawing.Color.White;
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
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(241, 67);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.删除ToolStripMenuItem.Text = "删除轴节点";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
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
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox ModbusWindow;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
    }
}
