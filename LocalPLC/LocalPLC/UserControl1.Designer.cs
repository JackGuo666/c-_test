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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("DI");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("DO");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("正交编码器");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("双向脉冲计数");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("单向脉冲计数");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("高速计数器", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("PTO");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("PWM");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("高速输出", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("本体COM1");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("本体ETH1");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("通信线路", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("基本配置", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode6,
            treeNode9,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("ModbusTCP-Client");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("ModbusTCP-Server");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("ModbusRTU-Master");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("ModbusRTU-Slave");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Modbus", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17});
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("AI");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("AO");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("面板扩展", new System.Windows.Forms.TreeNode[] {
            treeNode19,
            treeNode20});
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("扩展串口模块1(型号)");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("扩展网口模块1(型号)");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("左模块", new System.Windows.Forms.TreeNode[] {
            treeNode22,
            treeNode23});
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("AI");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("模块1(型号)", new System.Windows.Forms.TreeNode[] {
            treeNode25});
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("AO");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("模块2(型号)", new System.Windows.Forms.TreeNode[] {
            treeNode27});
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("DO");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("模块3(型号)", new System.Windows.Forms.TreeNode[] {
            treeNode29});
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("DI");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("模块4(型号)", new System.Windows.Forms.TreeNode[] {
            treeNode31});
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("右模块", new System.Windows.Forms.TreeNode[] {
            treeNode26,
            treeNode28,
            treeNode30,
            treeNode32});
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("扩展模块", new System.Windows.Forms.TreeNode[] {
            treeNode21,
            treeNode24,
            treeNode33});
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("canopen");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("运动控制");
            System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("modbus工具");
            System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("canopen工具");
            System.Windows.Forms.TreeNode treeNode39 = new System.Windows.Forms.TreeNode("shell工具", new System.Windows.Forms.TreeNode[] {
            treeNode37,
            treeNode38});
            System.Windows.Forms.TreeNode treeNode40 = new System.Windows.Forms.TreeNode("配置", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode18,
            treeNode34,
            treeNode35,
            treeNode36,
            treeNode39});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModbusWindow = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "DI";
            treeNode1.Text = "DI";
            treeNode2.Name = "DO";
            treeNode2.Text = "DO";
            treeNode3.Name = "正交编码器";
            treeNode3.Text = "正交编码器";
            treeNode4.Name = "双向脉冲计数";
            treeNode4.Text = "双向脉冲计数";
            treeNode5.Name = "单向脉冲计数";
            treeNode5.Text = "单向脉冲计数";
            treeNode6.Name = "高速计数器";
            treeNode6.Text = "高速计数器";
            treeNode7.Name = "PTO";
            treeNode7.Text = "PTO";
            treeNode8.Name = "PWM";
            treeNode8.Text = "PWM";
            treeNode9.Name = "高速输出";
            treeNode9.Text = "高速输出";
            treeNode10.Name = "本体COM1";
            treeNode10.Text = "本体COM1";
            treeNode11.Name = "本体ETH1";
            treeNode11.Text = "本体ETH1";
            treeNode12.Name = "通信线路";
            treeNode12.Text = "通信线路";
            treeNode13.Name = "基本配置";
            treeNode13.Text = "基本配置";
            treeNode14.Name = "节点00";
            treeNode14.Text = "ModbusTCP-Client";
            treeNode15.Name = "节点01";
            treeNode15.Text = "ModbusTCP-Server";
            treeNode16.Name = "节点02";
            treeNode16.Text = "ModbusRTU-Master";
            treeNode17.Name = "节点03";
            treeNode17.Text = "ModbusRTU-Slave";
            treeNode18.Name = "节点0";
            treeNode18.Text = "Modbus";
            treeNode19.Name = "AI";
            treeNode19.Text = "AI";
            treeNode20.Name = "AO";
            treeNode20.Text = "AO";
            treeNode21.Name = "面板扩展";
            treeNode21.Text = "面板扩展";
            treeNode22.Name = "扩展串口模块1(型号)";
            treeNode22.Text = "扩展串口模块1(型号)";
            treeNode23.Name = "扩展网口模块1(型号)";
            treeNode23.Text = "扩展网口模块1(型号)";
            treeNode24.Name = "左模块";
            treeNode24.Text = "左模块";
            treeNode25.Name = "AI";
            treeNode25.Text = "AI";
            treeNode26.Name = "模块1(型号)";
            treeNode26.Text = "模块1(型号)";
            treeNode27.Name = "AO";
            treeNode27.Text = "AO";
            treeNode28.Name = "模块2(型号)";
            treeNode28.Text = "模块2(型号)";
            treeNode29.Name = "DI";
            treeNode29.Text = "DO";
            treeNode30.Name = "模块3(型号)";
            treeNode30.Text = "模块3(型号)";
            treeNode31.Name = "DI";
            treeNode31.Text = "DI";
            treeNode32.Name = "模块4(型号)";
            treeNode32.Text = "模块4(型号)";
            treeNode33.Name = "右模块";
            treeNode33.Text = "右模块";
            treeNode34.Name = "扩展模块";
            treeNode34.Text = "扩展模块";
            treeNode35.Name = "canopen";
            treeNode35.Text = "canopen";
            treeNode36.Name = "运动控制";
            treeNode36.Text = "运动控制";
            treeNode37.Name = "节点1";
            treeNode37.Text = "modbus工具";
            treeNode38.Name = "节点2";
            treeNode38.Text = "canopen工具";
            treeNode39.Name = "节点0";
            treeNode39.Text = "shell工具";
            treeNode40.Name = "配置";
            treeNode40.Text = "配置";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode40});
            this.treeView1.Size = new System.Drawing.Size(227, 750);
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
            // ModbusWindow
            // 
            this.ModbusWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ModbusWindow.Location = new System.Drawing.Point(227, 0);
            this.ModbusWindow.Margin = new System.Windows.Forms.Padding(4);
            this.ModbusWindow.Name = "ModbusWindow";
            this.ModbusWindow.Padding = new System.Windows.Forms.Padding(4);
            this.ModbusWindow.Size = new System.Drawing.Size(1276, 750);
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
    }
}
