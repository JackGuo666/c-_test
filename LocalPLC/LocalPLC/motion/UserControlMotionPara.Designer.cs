namespace LocalPLC.motion
{
    partial class UserControlMotionPara
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_pulsePerRevolutionMotor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_offsetPerReolutionMotor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_hardDownLimitLevel = new System.Windows.Forms.ComboBox();
            this.comboBox_hardUpLimitLevel = new System.Windows.Forms.ComboBox();
            this.comboBox_hardDownLimitInput = new System.Windows.Forms.ComboBox();
            this.comboBox_hardUpLimitInput = new System.Windows.Forms.ComboBox();
            this.textBox_SoftDownLimitOffset = new System.Windows.Forms.TextBox();
            this.textBox_softUpLimitOffset = new System.Windows.Forms.TextBox();
            this.checkBox_softLimit = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_EmeStopDeceSpeed = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.textBox_Jerk = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox_DecelerationSpeed = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox_AcceleratedSpeed = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox_MaxSpeed = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.textBox_ReverseCompensation = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label28 = new System.Windows.Forms.Label();
            this.comboBox_ZPulseSignal = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox_BackOriginal = new System.Windows.Forms.ComboBox();
            this.comboBox_BackOriginalSelectLevel = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_valid = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "电机每转脉冲数";
            // 
            // textBox_pulsePerRevolutionMotor
            // 
            this.textBox_pulsePerRevolutionMotor.Location = new System.Drawing.Point(219, 30);
            this.textBox_pulsePerRevolutionMotor.MaxLength = 10;
            this.textBox_pulsePerRevolutionMotor.Name = "textBox_pulsePerRevolutionMotor";
            this.textBox_pulsePerRevolutionMotor.Size = new System.Drawing.Size(140, 28);
            this.textBox_pulsePerRevolutionMotor.TabIndex = 1;
            this.textBox_pulsePerRevolutionMotor.TextChanged += new System.EventHandler(this.textBox_pulsePerRevolutionMotor_TextChanged);
            this.textBox_pulsePerRevolutionMotor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_pulsePerRevolutionMotor_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "电机每转的负载位移";
            // 
            // textBox_offsetPerReolutionMotor
            // 
            this.textBox_offsetPerReolutionMotor.Location = new System.Drawing.Point(219, 64);
            this.textBox_offsetPerReolutionMotor.MaxLength = 10;
            this.textBox_offsetPerReolutionMotor.Name = "textBox_offsetPerReolutionMotor";
            this.textBox_offsetPerReolutionMotor.Size = new System.Drawing.Size(140, 28);
            this.textBox_offsetPerReolutionMotor.TabIndex = 1;
            this.textBox_offsetPerReolutionMotor.TextChanged += new System.EventHandler(this.textBox_offsetPerReolutionMotor_TextChanged);
            this.textBox_offsetPerReolutionMotor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_pulsePerRevolutionMotor_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(365, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "mm";
            // 
            // comboBox_hardDownLimitLevel
            // 
            this.comboBox_hardDownLimitLevel.FormattingEnabled = true;
            this.comboBox_hardDownLimitLevel.Location = new System.Drawing.Point(549, 101);
            this.comboBox_hardDownLimitLevel.Name = "comboBox_hardDownLimitLevel";
            this.comboBox_hardDownLimitLevel.Size = new System.Drawing.Size(141, 26);
            this.comboBox_hardDownLimitLevel.TabIndex = 3;
            this.comboBox_hardDownLimitLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_hardUpLimitInput_KeyPress);
            // 
            // comboBox_hardUpLimitLevel
            // 
            this.comboBox_hardUpLimitLevel.FormattingEnabled = true;
            this.comboBox_hardUpLimitLevel.Location = new System.Drawing.Point(549, 64);
            this.comboBox_hardUpLimitLevel.Name = "comboBox_hardUpLimitLevel";
            this.comboBox_hardUpLimitLevel.Size = new System.Drawing.Size(141, 26);
            this.comboBox_hardUpLimitLevel.TabIndex = 3;
            this.comboBox_hardUpLimitLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_hardUpLimitInput_KeyPress);
            // 
            // comboBox_hardDownLimitInput
            // 
            this.comboBox_hardDownLimitInput.FormattingEnabled = true;
            this.comboBox_hardDownLimitInput.Location = new System.Drawing.Point(209, 101);
            this.comboBox_hardDownLimitInput.Name = "comboBox_hardDownLimitInput";
            this.comboBox_hardDownLimitInput.Size = new System.Drawing.Size(141, 26);
            this.comboBox_hardDownLimitInput.TabIndex = 3;
            this.comboBox_hardDownLimitInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_hardUpLimitInput_KeyPress);
            // 
            // comboBox_hardUpLimitInput
            // 
            this.comboBox_hardUpLimitInput.FormattingEnabled = true;
            this.comboBox_hardUpLimitInput.Location = new System.Drawing.Point(209, 64);
            this.comboBox_hardUpLimitInput.Name = "comboBox_hardUpLimitInput";
            this.comboBox_hardUpLimitInput.Size = new System.Drawing.Size(141, 26);
            this.comboBox_hardUpLimitInput.TabIndex = 3;
            this.comboBox_hardUpLimitInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_hardUpLimitInput_KeyPress);
            // 
            // textBox_SoftDownLimitOffset
            // 
            this.textBox_SoftDownLimitOffset.Location = new System.Drawing.Point(209, 209);
            this.textBox_SoftDownLimitOffset.MaxLength = 11;
            this.textBox_SoftDownLimitOffset.Name = "textBox_SoftDownLimitOffset";
            this.textBox_SoftDownLimitOffset.Size = new System.Drawing.Size(140, 28);
            this.textBox_SoftDownLimitOffset.TabIndex = 1;
            this.textBox_SoftDownLimitOffset.TextChanged += new System.EventHandler(this.textBox_SoftDownLimitOffset_TextChanged);
            this.textBox_SoftDownLimitOffset.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_SoftDownLimitOffset_KeyPress);
            // 
            // textBox_softUpLimitOffset
            // 
            this.textBox_softUpLimitOffset.Location = new System.Drawing.Point(209, 172);
            this.textBox_softUpLimitOffset.MaxLength = 11;
            this.textBox_softUpLimitOffset.Name = "textBox_softUpLimitOffset";
            this.textBox_softUpLimitOffset.Size = new System.Drawing.Size(140, 28);
            this.textBox_softUpLimitOffset.TabIndex = 1;
            this.textBox_softUpLimitOffset.TextChanged += new System.EventHandler(this.textBox_softUpLimitOffset_TextChanged);
            this.textBox_softUpLimitOffset.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_SoftUpLimitOffset_KeyPress);
            // 
            // checkBox_softLimit
            // 
            this.checkBox_softLimit.AutoSize = true;
            this.checkBox_softLimit.Location = new System.Drawing.Point(9, 141);
            this.checkBox_softLimit.Name = "checkBox_softLimit";
            this.checkBox_softLimit.Size = new System.Drawing.Size(124, 22);
            this.checkBox_softLimit.TabIndex = 2;
            this.checkBox_softLimit.Text = "启动软限位";
            this.checkBox_softLimit.UseVisualStyleBackColor = true;
            this.checkBox_softLimit.CheckedChanged += new System.EventHandler(this.checkBox_softLimit_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(355, 212);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 18);
            this.label12.TabIndex = 0;
            this.label12.Text = "mm";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 26);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(124, 22);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "启动硬限位";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(355, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "mm";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(425, 104);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 18);
            this.label14.TabIndex = 0;
            this.label14.Text = "选择电平";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 219);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 18);
            this.label11.TabIndex = 0;
            this.label11.Text = "软件下限位位置";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(152, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "硬件下限位输入点";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(425, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 18);
            this.label13.TabIndex = 0;
            this.label13.Text = "选择电平";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 182);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(134, 18);
            this.label10.TabIndex = 0;
            this.label10.Text = "软件上限位位置";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(152, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "硬件上限位输入点";
            // 
            // textBox_EmeStopDeceSpeed
            // 
            this.textBox_EmeStopDeceSpeed.Location = new System.Drawing.Point(210, 148);
            this.textBox_EmeStopDeceSpeed.MaxLength = 10;
            this.textBox_EmeStopDeceSpeed.Name = "textBox_EmeStopDeceSpeed";
            this.textBox_EmeStopDeceSpeed.Size = new System.Drawing.Size(140, 28);
            this.textBox_EmeStopDeceSpeed.TabIndex = 1;
            this.textBox_EmeStopDeceSpeed.TextChanged += new System.EventHandler(this.textBox_EmeStopDeceSpeed_TextChanged);
            this.textBox_EmeStopDeceSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_pulsePerRevolutionMotor_KeyPress);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 151);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(98, 18);
            this.label24.TabIndex = 0;
            this.label24.Text = "急停减速度";
            // 
            // textBox_Jerk
            // 
            this.textBox_Jerk.Location = new System.Drawing.Point(210, 117);
            this.textBox_Jerk.MaxLength = 10;
            this.textBox_Jerk.Name = "textBox_Jerk";
            this.textBox_Jerk.Size = new System.Drawing.Size(140, 28);
            this.textBox_Jerk.TabIndex = 1;
            this.textBox_Jerk.TextChanged += new System.EventHandler(this.textBox_Jerk_TextChanged);
            this.textBox_Jerk.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_pulsePerRevolutionMotor_KeyPress);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 120);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(80, 18);
            this.label22.TabIndex = 0;
            this.label22.Text = "跃度Jerk";
            // 
            // textBox_DecelerationSpeed
            // 
            this.textBox_DecelerationSpeed.Location = new System.Drawing.Point(210, 86);
            this.textBox_DecelerationSpeed.MaxLength = 10;
            this.textBox_DecelerationSpeed.Name = "textBox_DecelerationSpeed";
            this.textBox_DecelerationSpeed.Size = new System.Drawing.Size(140, 28);
            this.textBox_DecelerationSpeed.TabIndex = 1;
            this.textBox_DecelerationSpeed.TextChanged += new System.EventHandler(this.textBox_DecelerationSpeed_TextChanged);
            this.textBox_DecelerationSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_pulsePerRevolutionMotor_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 89);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(62, 18);
            this.label20.TabIndex = 0;
            this.label20.Text = "减速度";
            // 
            // textBox_AcceleratedSpeed
            // 
            this.textBox_AcceleratedSpeed.Location = new System.Drawing.Point(210, 55);
            this.textBox_AcceleratedSpeed.MaxLength = 10;
            this.textBox_AcceleratedSpeed.Name = "textBox_AcceleratedSpeed";
            this.textBox_AcceleratedSpeed.Size = new System.Drawing.Size(140, 28);
            this.textBox_AcceleratedSpeed.TabIndex = 1;
            this.textBox_AcceleratedSpeed.TextChanged += new System.EventHandler(this.textBox_AcceleratedSpeed_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(356, 151);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 18);
            this.label23.TabIndex = 0;
            this.label23.Text = "mm/s²";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 58);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 18);
            this.label18.TabIndex = 0;
            this.label18.Text = "加速度";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(356, 120);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 18);
            this.label21.TabIndex = 0;
            this.label21.Text = "mm/s³";
            // 
            // textBox_MaxSpeed
            // 
            this.textBox_MaxSpeed.Location = new System.Drawing.Point(210, 24);
            this.textBox_MaxSpeed.MaxLength = 10;
            this.textBox_MaxSpeed.Name = "textBox_MaxSpeed";
            this.textBox_MaxSpeed.Size = new System.Drawing.Size(140, 28);
            this.textBox_MaxSpeed.TabIndex = 1;
            this.textBox_MaxSpeed.TextChanged += new System.EventHandler(this.textBox_MaxSpeed_TextChanged);
            this.textBox_MaxSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_pulsePerRevolutionMotor_KeyPress);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(356, 89);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 18);
            this.label19.TabIndex = 0;
            this.label19.Text = "mm/s²";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "最大速度";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(356, 58);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 18);
            this.label17.TabIndex = 0;
            this.label17.Text = "mm/s²";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(356, 27);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 18);
            this.label16.TabIndex = 0;
            this.label16.Text = "mm/s";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(9, 27);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(116, 18);
            this.label33.TabIndex = 0;
            this.label33.Text = "反向间隙补偿";
            // 
            // textBox_ReverseCompensation
            // 
            this.textBox_ReverseCompensation.Location = new System.Drawing.Point(210, 17);
            this.textBox_ReverseCompensation.MaxLength = 10;
            this.textBox_ReverseCompensation.Name = "textBox_ReverseCompensation";
            this.textBox_ReverseCompensation.Size = new System.Drawing.Size(140, 28);
            this.textBox_ReverseCompensation.TabIndex = 1;
            this.textBox_ReverseCompensation.TextChanged += new System.EventHandler(this.textBox_ReverseCompensation_TextChanged);
            this.textBox_ReverseCompensation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_pulsePerRevolutionMotor_KeyPress);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(356, 20);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(26, 18);
            this.label34.TabIndex = 0;
            this.label34.Text = "mm";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.textBox_offsetPerReolutionMotor);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox_pulsePerRevolutionMotor);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(775, 111);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "脉冲当量";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.comboBox_hardDownLimitLevel);
            this.groupBox3.Controls.Add(this.comboBox_hardUpLimitLevel);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.comboBox_hardDownLimitInput);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.comboBox_hardUpLimitInput);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.textBox_SoftDownLimitOffset);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.textBox_softUpLimitOffset);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.checkBox_softLimit);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Location = new System.Drawing.Point(6, 183);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(775, 261);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "限位信号";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.textBox_EmeStopDeceSpeed);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.textBox_Jerk);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.textBox_DecelerationSpeed);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.textBox_MaxSpeed);
            this.groupBox4.Controls.Add(this.textBox_AcceleratedSpeed);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Location = new System.Drawing.Point(3, 450);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(778, 193);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "动态参数";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label28);
            this.groupBox5.Controls.Add(this.comboBox_ZPulseSignal);
            this.groupBox5.Controls.Add(this.comboBox4);
            this.groupBox5.Controls.Add(this.comboBox_BackOriginal);
            this.groupBox5.Controls.Add(this.comboBox_BackOriginalSelectLevel);
            this.groupBox5.Controls.Add(this.label26);
            this.groupBox5.Controls.Add(this.label30);
            this.groupBox5.Location = new System.Drawing.Point(3, 649);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(778, 100);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "回原点";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(424, 30);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(80, 18);
            this.label28.TabIndex = 0;
            this.label28.Text = "选择电平";
            // 
            // comboBox_ZPulseSignal
            // 
            this.comboBox_ZPulseSignal.FormattingEnabled = true;
            this.comboBox_ZPulseSignal.Location = new System.Drawing.Point(210, 61);
            this.comboBox_ZPulseSignal.Name = "comboBox_ZPulseSignal";
            this.comboBox_ZPulseSignal.Size = new System.Drawing.Size(141, 26);
            this.comboBox_ZPulseSignal.TabIndex = 3;
            this.comboBox_ZPulseSignal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_hardUpLimitInput_KeyPress);
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(395, 64);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(141, 26);
            this.comboBox4.TabIndex = 3;
            // 
            // comboBox_BackOriginal
            // 
            this.comboBox_BackOriginal.FormattingEnabled = true;
            this.comboBox_BackOriginal.Location = new System.Drawing.Point(210, 22);
            this.comboBox_BackOriginal.Name = "comboBox_BackOriginal";
            this.comboBox_BackOriginal.Size = new System.Drawing.Size(141, 26);
            this.comboBox_BackOriginal.TabIndex = 3;
            this.comboBox_BackOriginal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_hardUpLimitInput_KeyPress);
            // 
            // comboBox_BackOriginalSelectLevel
            // 
            this.comboBox_BackOriginalSelectLevel.FormattingEnabled = true;
            this.comboBox_BackOriginalSelectLevel.Location = new System.Drawing.Point(548, 27);
            this.comboBox_BackOriginalSelectLevel.Name = "comboBox_BackOriginalSelectLevel";
            this.comboBox_BackOriginalSelectLevel.Size = new System.Drawing.Size(141, 26);
            this.comboBox_BackOriginalSelectLevel.TabIndex = 3;
            this.comboBox_BackOriginalSelectLevel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_hardUpLimitInput_KeyPress);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(7, 30);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(116, 18);
            this.label26.TabIndex = 0;
            this.label26.Text = "原点输入信号";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(7, 64);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(89, 18);
            this.label30.TabIndex = 0;
            this.label30.Text = "Z脉冲信号";
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.textBox_ReverseCompensation);
            this.groupBox6.Controls.Add(this.label33);
            this.groupBox6.Controls.Add(this.label34);
            this.groupBox6.Location = new System.Drawing.Point(4, 755);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(774, 85);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "反向间隙补偿";
            // 
            // button_cancel
            // 
            this.button_cancel.BackColor = System.Drawing.Color.White;
            this.button_cancel.Location = new System.Drawing.Point(130, 3);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(120, 35);
            this.button_cancel.TabIndex = 6;
            this.button_cancel.Text = "取消";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_valid
            // 
            this.button_valid.BackColor = System.Drawing.Color.White;
            this.button_valid.Location = new System.Drawing.Point(4, 3);
            this.button_valid.Name = "button_valid";
            this.button_valid.Size = new System.Drawing.Size(120, 35);
            this.button_valid.TabIndex = 7;
            this.button_valid.Text = "应用";
            this.button_valid.UseVisualStyleBackColor = true;
            this.button_valid.Click += new System.EventHandler(this.button_valid_Click);
            // 
            // UserControlMotionPara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_valid);
            this.Name = "UserControlMotionPara";
            this.Size = new System.Drawing.Size(781, 934);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_offsetPerReolutionMotor;
        private System.Windows.Forms.TextBox textBox_pulsePerRevolutionMotor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_softUpLimitOffset;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox_hardDownLimitInput;
        private System.Windows.Forms.ComboBox comboBox_hardUpLimitInput;
        private System.Windows.Forms.CheckBox checkBox_softLimit;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_hardDownLimitLevel;
        private System.Windows.Forms.TextBox textBox_SoftDownLimitOffset;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_EmeStopDeceSpeed;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBox_Jerk;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBox_DecelerationSpeed;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox_AcceleratedSpeed;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox_MaxSpeed;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox textBox_ReverseCompensation;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox comboBox_hardUpLimitLevel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox comboBox_BackOriginalSelectLevel;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox comboBox_BackOriginal;
        private System.Windows.Forms.ComboBox comboBox_ZPulseSignal;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_valid;
    }
}
