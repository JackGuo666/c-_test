namespace LocalPLC.Base
{
    partial class FormHighInput
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
            this.label_pulse = new System.Windows.Forms.Label();
            this.label_direction = new System.Windows.Forms.Label();
            this.label_inputmode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Type = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_valid = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox_pulseFrequencyPort = new System.Windows.Forms.TextBox();
            this.checkBox_frequencyPulse = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.radioButton_1s = new System.Windows.Forms.RadioButton();
            this.radioButton_100ms = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.checkBox_frequencyDoubleWord = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox_capturePort = new System.Windows.Forms.TextBox();
            this.textBox_pulseInputPort = new System.Windows.Forms.TextBox();
            this.textBox_presetPort = new System.Windows.Forms.TextBox();
            this.textBox_dirInputPort = new System.Windows.Forms.TextBox();
            this.checkBox_caputre = new System.Windows.Forms.CheckBox();
            this.checkBox_preset = new System.Windows.Forms.CheckBox();
            this.checkBox_direction = new System.Windows.Forms.CheckBox();
            this.checkBox_pulse = new System.Windows.Forms.CheckBox();
            this.label_presetInput = new System.Windows.Forms.Label();
            this.label_caputreInput = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox_trigger1 = new System.Windows.Forms.ComboBox();
            this.comboBox_trigger0 = new System.Windows.Forms.ComboBox();
            this.textBox_eventName1 = new System.Windows.Forms.TextBox();
            this.textBox_eventName0 = new System.Windows.Forms.TextBox();
            this.textBox_threshold1 = new System.Windows.Forms.TextBox();
            this.textBox_threshold0 = new System.Windows.Forms.TextBox();
            this.textBox_presetValue = new System.Windows.Forms.TextBox();
            this.label_limite1 = new System.Windows.Forms.Label();
            this.label_limite0 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EVENT_2 = new System.Windows.Forms.Label();
            this.EVENT_1 = new System.Windows.Forms.Label();
            this.label_presetValue = new System.Windows.Forms.Label();
            this.checkBox_doubleWord = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox_inputmode = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_pulse
            // 
            this.label_pulse.AutoSize = true;
            this.label_pulse.Location = new System.Drawing.Point(21, 48);
            this.label_pulse.Name = "label_pulse";
            this.label_pulse.Size = new System.Drawing.Size(89, 18);
            this.label_pulse.TabIndex = 0;
            this.label_pulse.Text = "脉冲输入:";
            // 
            // label_direction
            // 
            this.label_direction.AutoSize = true;
            this.label_direction.Location = new System.Drawing.Point(21, 83);
            this.label_direction.Name = "label_direction";
            this.label_direction.Size = new System.Drawing.Size(89, 18);
            this.label_direction.TabIndex = 0;
            this.label_direction.Text = "方向输入:";
            // 
            // label_inputmode
            // 
            this.label_inputmode.AutoSize = true;
            this.label_inputmode.Location = new System.Drawing.Point(495, 47);
            this.label_inputmode.Name = "label_inputmode";
            this.label_inputmode.Size = new System.Drawing.Size(89, 18);
            this.label_inputmode.TabIndex = 0;
            this.label_inputmode.Text = "输入模式:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "HSC类型:";
            // 
            // comboBox_Type
            // 
            this.comboBox_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Type.FormattingEnabled = true;
            this.comboBox_Type.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.comboBox_Type.Location = new System.Drawing.Point(109, 44);
            this.comboBox_Type.Name = "comboBox_Type";
            this.comboBox_Type.Size = new System.Drawing.Size(143, 26);
            this.comboBox_Type.TabIndex = 3;
            this.comboBox_Type.SelectedIndexChanged += new System.EventHandler(this.comboBox_Type_SelectedIndexChanged_1);
            this.comboBox_Type.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_Type_KeyPress);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.button_cancel);
            this.panel2.Controls.Add(this.button_valid);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(939, 838);
            this.panel2.TabIndex = 5;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.button_cancel.Location = new System.Drawing.Point(785, 786);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(101, 40);
            this.button_cancel.TabIndex = 8;
            this.button_cancel.Text = "取消";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_valid
            // 
            this.button_valid.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.button_valid.Location = new System.Drawing.Point(662, 786);
            this.button_valid.Name = "button_valid";
            this.button_valid.Size = new System.Drawing.Size(101, 40);
            this.button_valid.TabIndex = 8;
            this.button_valid.Text = "应用";
            this.button_valid.UseVisualStyleBackColor = true;
            this.button_valid.Click += new System.EventHandler(this.button_valid_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox_pulseFrequencyPort);
            this.groupBox4.Controls.Add(this.checkBox_frequencyPulse);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.radioButton_1s);
            this.groupBox4.Controls.Add(this.radioButton_100ms);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.checkBox_frequencyDoubleWord);
            this.groupBox4.Location = new System.Drawing.Point(5, 558);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(905, 222);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            // 
            // textBox_pulseFrequencyPort
            // 
            this.textBox_pulseFrequencyPort.Location = new System.Drawing.Point(382, 166);
            this.textBox_pulseFrequencyPort.Name = "textBox_pulseFrequencyPort";
            this.textBox_pulseFrequencyPort.Size = new System.Drawing.Size(101, 28);
            this.textBox_pulseFrequencyPort.TabIndex = 4;
            this.textBox_pulseFrequencyPort.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // checkBox_frequencyPulse
            // 
            this.checkBox_frequencyPulse.AutoSize = true;
            this.checkBox_frequencyPulse.Checked = true;
            this.checkBox_frequencyPulse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_frequencyPulse.Location = new System.Drawing.Point(209, 168);
            this.checkBox_frequencyPulse.Name = "checkBox_frequencyPulse";
            this.checkBox_frequencyPulse.Size = new System.Drawing.Size(70, 22);
            this.checkBox_frequencyPulse.TabIndex = 6;
            this.checkBox_frequencyPulse.Text = "启用";
            this.checkBox_frequencyPulse.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(28, 172);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 18);
            this.label14.TabIndex = 4;
            this.label14.Text = "脉冲输入:";
            // 
            // radioButton_1s
            // 
            this.radioButton_1s.AutoSize = true;
            this.radioButton_1s.Checked = true;
            this.radioButton_1s.Location = new System.Drawing.Point(107, 130);
            this.radioButton_1s.Name = "radioButton_1s";
            this.radioButton_1s.Size = new System.Drawing.Size(51, 22);
            this.radioButton_1s.TabIndex = 1;
            this.radioButton_1s.TabStop = true;
            this.radioButton_1s.Text = "1s";
            this.radioButton_1s.UseVisualStyleBackColor = true;
            this.radioButton_1s.CheckedChanged += new System.EventHandler(this.radioButton_1s_CheckedChanged);
            // 
            // radioButton_100ms
            // 
            this.radioButton_100ms.AutoSize = true;
            this.radioButton_100ms.Location = new System.Drawing.Point(107, 102);
            this.radioButton_100ms.Name = "radioButton_100ms";
            this.radioButton_100ms.Size = new System.Drawing.Size(78, 22);
            this.radioButton_100ms.TabIndex = 1;
            this.radioButton_100ms.TabStop = true;
            this.radioButton_100ms.Text = "100ms";
            this.radioButton_100ms.UseVisualStyleBackColor = true;
            this.radioButton_100ms.CheckedChanged += new System.EventHandler(this.radioButton_100ms_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(26, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 18);
            this.label13.TabIndex = 0;
            this.label13.Text = "时间窗口";
            // 
            // checkBox_frequencyDoubleWord
            // 
            this.checkBox_frequencyDoubleWord.AutoSize = true;
            this.checkBox_frequencyDoubleWord.Location = new System.Drawing.Point(31, 27);
            this.checkBox_frequencyDoubleWord.Name = "checkBox_frequencyDoubleWord";
            this.checkBox_frequencyDoubleWord.Size = new System.Drawing.Size(70, 22);
            this.checkBox_frequencyDoubleWord.TabIndex = 0;
            this.checkBox_frequencyDoubleWord.Text = "双字";
            this.checkBox_frequencyDoubleWord.UseVisualStyleBackColor = true;
            this.checkBox_frequencyDoubleWord.CheckedChanged += new System.EventHandler(this.checkBox_frequencyDoubleWord_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox_capturePort);
            this.groupBox3.Controls.Add(this.textBox_pulseInputPort);
            this.groupBox3.Controls.Add(this.textBox_presetPort);
            this.groupBox3.Controls.Add(this.textBox_dirInputPort);
            this.groupBox3.Controls.Add(this.checkBox_caputre);
            this.groupBox3.Controls.Add(this.checkBox_preset);
            this.groupBox3.Controls.Add(this.checkBox_direction);
            this.groupBox3.Controls.Add(this.checkBox_pulse);
            this.groupBox3.Controls.Add(this.label_presetInput);
            this.groupBox3.Controls.Add(this.label_pulse);
            this.groupBox3.Controls.Add(this.label_caputreInput);
            this.groupBox3.Controls.Add(this.label_direction);
            this.groupBox3.Location = new System.Drawing.Point(5, 352);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(906, 200);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // textBox_capturePort
            // 
            this.textBox_capturePort.Enabled = false;
            this.textBox_capturePort.Location = new System.Drawing.Point(382, 150);
            this.textBox_capturePort.Name = "textBox_capturePort";
            this.textBox_capturePort.Size = new System.Drawing.Size(113, 28);
            this.textBox_capturePort.TabIndex = 5;
            // 
            // textBox_pulseInputPort
            // 
            this.textBox_pulseInputPort.Enabled = false;
            this.textBox_pulseInputPort.Location = new System.Drawing.Point(382, 41);
            this.textBox_pulseInputPort.Name = "textBox_pulseInputPort";
            this.textBox_pulseInputPort.Size = new System.Drawing.Size(113, 28);
            this.textBox_pulseInputPort.TabIndex = 4;
            this.textBox_pulseInputPort.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox_presetPort
            // 
            this.textBox_presetPort.Enabled = false;
            this.textBox_presetPort.Location = new System.Drawing.Point(382, 112);
            this.textBox_presetPort.Name = "textBox_presetPort";
            this.textBox_presetPort.Size = new System.Drawing.Size(113, 28);
            this.textBox_presetPort.TabIndex = 4;
            this.textBox_presetPort.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox_dirInputPort
            // 
            this.textBox_dirInputPort.Enabled = false;
            this.textBox_dirInputPort.Location = new System.Drawing.Point(382, 75);
            this.textBox_dirInputPort.Name = "textBox_dirInputPort";
            this.textBox_dirInputPort.Size = new System.Drawing.Size(113, 28);
            this.textBox_dirInputPort.TabIndex = 4;
            this.textBox_dirInputPort.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // checkBox_caputre
            // 
            this.checkBox_caputre.AutoSize = true;
            this.checkBox_caputre.Location = new System.Drawing.Point(209, 149);
            this.checkBox_caputre.Name = "checkBox_caputre";
            this.checkBox_caputre.Size = new System.Drawing.Size(70, 22);
            this.checkBox_caputre.TabIndex = 3;
            this.checkBox_caputre.Text = "启用";
            this.checkBox_caputre.UseVisualStyleBackColor = true;
            this.checkBox_caputre.CheckedChanged += new System.EventHandler(this.checkBox_caputre_CheckedChanged_1);
            this.checkBox_caputre.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkBox_caputre_MouseDown);
            // 
            // checkBox_preset
            // 
            this.checkBox_preset.AutoSize = true;
            this.checkBox_preset.Location = new System.Drawing.Point(209, 114);
            this.checkBox_preset.Name = "checkBox_preset";
            this.checkBox_preset.Size = new System.Drawing.Size(70, 22);
            this.checkBox_preset.TabIndex = 3;
            this.checkBox_preset.Text = "启用";
            this.checkBox_preset.UseVisualStyleBackColor = true;
            this.checkBox_preset.CheckedChanged += new System.EventHandler(this.checkBox_preset_CheckedChanged_1);
            this.checkBox_preset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkBox_preset_MouseDown);
            // 
            // checkBox_direction
            // 
            this.checkBox_direction.AutoSize = true;
            this.checkBox_direction.Checked = true;
            this.checkBox_direction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_direction.Enabled = false;
            this.checkBox_direction.Location = new System.Drawing.Point(209, 79);
            this.checkBox_direction.Name = "checkBox_direction";
            this.checkBox_direction.Size = new System.Drawing.Size(70, 22);
            this.checkBox_direction.TabIndex = 3;
            this.checkBox_direction.Text = "启用";
            this.checkBox_direction.UseVisualStyleBackColor = true;
            // 
            // checkBox_pulse
            // 
            this.checkBox_pulse.AutoSize = true;
            this.checkBox_pulse.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_pulse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkBox_pulse.Checked = true;
            this.checkBox_pulse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_pulse.Enabled = false;
            this.checkBox_pulse.Location = new System.Drawing.Point(209, 44);
            this.checkBox_pulse.Name = "checkBox_pulse";
            this.checkBox_pulse.Size = new System.Drawing.Size(70, 22);
            this.checkBox_pulse.TabIndex = 3;
            this.checkBox_pulse.Text = "启用";
            this.checkBox_pulse.UseVisualStyleBackColor = false;
            // 
            // label_presetInput
            // 
            this.label_presetInput.AutoSize = true;
            this.label_presetInput.Location = new System.Drawing.Point(21, 118);
            this.label_presetInput.Name = "label_presetInput";
            this.label_presetInput.Size = new System.Drawing.Size(89, 18);
            this.label_presetInput.TabIndex = 0;
            this.label_presetInput.Text = "预设输入:";
            // 
            // label_caputreInput
            // 
            this.label_caputreInput.AutoSize = true;
            this.label_caputreInput.Location = new System.Drawing.Point(21, 153);
            this.label_caputreInput.Name = "label_caputreInput";
            this.label_caputreInput.Size = new System.Drawing.Size(89, 18);
            this.label_caputreInput.TabIndex = 0;
            this.label_caputreInput.Text = "捕捉输入:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox_trigger1);
            this.groupBox2.Controls.Add(this.comboBox_trigger0);
            this.groupBox2.Controls.Add(this.textBox_eventName1);
            this.groupBox2.Controls.Add(this.textBox_eventName0);
            this.groupBox2.Controls.Add(this.textBox_threshold1);
            this.groupBox2.Controls.Add(this.textBox_threshold0);
            this.groupBox2.Controls.Add(this.textBox_presetValue);
            this.groupBox2.Controls.Add(this.label_limite1);
            this.groupBox2.Controls.Add(this.label_limite0);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.EVENT_2);
            this.groupBox2.Controls.Add(this.EVENT_1);
            this.groupBox2.Controls.Add(this.label_presetValue);
            this.groupBox2.Controls.Add(this.checkBox_doubleWord);
            this.groupBox2.Location = new System.Drawing.Point(6, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(908, 228);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // comboBox_trigger1
            // 
            this.comboBox_trigger1.FormattingEnabled = true;
            this.comboBox_trigger1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.comboBox_trigger1.Location = new System.Drawing.Point(646, 185);
            this.comboBox_trigger1.Name = "comboBox_trigger1";
            this.comboBox_trigger1.Size = new System.Drawing.Size(111, 26);
            this.comboBox_trigger1.TabIndex = 4;
            this.comboBox_trigger1.SelectedIndexChanged += new System.EventHandler(this.comboBox_trigger1_SelectedIndexChanged);
            this.comboBox_trigger1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_trigger1_KeyPress);
            // 
            // comboBox_trigger0
            // 
            this.comboBox_trigger0.FormattingEnabled = true;
            this.comboBox_trigger0.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.comboBox_trigger0.Location = new System.Drawing.Point(646, 130);
            this.comboBox_trigger0.Name = "comboBox_trigger0";
            this.comboBox_trigger0.Size = new System.Drawing.Size(111, 26);
            this.comboBox_trigger0.TabIndex = 4;
            this.comboBox_trigger0.SelectedIndexChanged += new System.EventHandler(this.comboBox_trigger0_SelectedIndexChanged);
            this.comboBox_trigger0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_trigger0_KeyPress);
            // 
            // textBox_eventName1
            // 
            this.textBox_eventName1.Location = new System.Drawing.Point(301, 185);
            this.textBox_eventName1.Name = "textBox_eventName1";
            this.textBox_eventName1.Size = new System.Drawing.Size(109, 28);
            this.textBox_eventName1.TabIndex = 3;
            this.textBox_eventName1.TextChanged += new System.EventHandler(this.textBox_eventName1_TextChanged);
            // 
            // textBox_eventName0
            // 
            this.textBox_eventName0.Location = new System.Drawing.Point(301, 132);
            this.textBox_eventName0.Name = "textBox_eventName0";
            this.textBox_eventName0.Size = new System.Drawing.Size(109, 28);
            this.textBox_eventName0.TabIndex = 3;
            this.textBox_eventName0.TextChanged += new System.EventHandler(this.textBox_eventName0_TextChanged);
            // 
            // textBox_threshold1
            // 
            this.textBox_threshold1.Location = new System.Drawing.Point(113, 184);
            this.textBox_threshold1.Name = "textBox_threshold1";
            this.textBox_threshold1.Size = new System.Drawing.Size(107, 28);
            this.textBox_threshold1.TabIndex = 2;
            this.textBox_threshold1.TextChanged += new System.EventHandler(this.textBox_threshold1_TextChanged);
            this.textBox_threshold1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_threshold1_KeyPress);
            // 
            // textBox_threshold0
            // 
            this.textBox_threshold0.Location = new System.Drawing.Point(113, 132);
            this.textBox_threshold0.Name = "textBox_threshold0";
            this.textBox_threshold0.Size = new System.Drawing.Size(107, 28);
            this.textBox_threshold0.TabIndex = 2;
            this.textBox_threshold0.TextChanged += new System.EventHandler(this.textBox_threshold0_TextChanged);
            this.textBox_threshold0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_threshold0_KeyPress);
            // 
            // textBox_presetValue
            // 
            this.textBox_presetValue.Location = new System.Drawing.Point(113, 80);
            this.textBox_presetValue.Name = "textBox_presetValue";
            this.textBox_presetValue.Size = new System.Drawing.Size(107, 28);
            this.textBox_presetValue.TabIndex = 2;
            this.textBox_presetValue.TextChanged += new System.EventHandler(this.textBox_presetValue_TextChanged);
            this.textBox_presetValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_presetValue_KeyPress);
            // 
            // label_limite1
            // 
            this.label_limite1.AutoSize = true;
            this.label_limite1.Location = new System.Drawing.Point(24, 187);
            this.label_limite1.Name = "label_limite1";
            this.label_limite1.Size = new System.Drawing.Size(62, 18);
            this.label_limite1.TabIndex = 1;
            this.label_limite1.Text = "阈值E1";
            // 
            // label_limite0
            // 
            this.label_limite0.AutoSize = true;
            this.label_limite0.Location = new System.Drawing.Point(24, 135);
            this.label_limite0.Name = "label_limite0";
            this.label_limite0.Size = new System.Drawing.Size(62, 18);
            this.label_limite0.TabIndex = 1;
            this.label_limite0.Text = "阈值E0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(647, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 18);
            this.label9.TabIndex = 1;
            this.label9.Text = "触发器";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "事件名";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(475, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 18);
            this.label6.TabIndex = 1;
            this.label6.Text = "事件ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "值";
            // 
            // EVENT_2
            // 
            this.EVENT_2.AutoSize = true;
            this.EVENT_2.Location = new System.Drawing.Point(475, 188);
            this.EVENT_2.Name = "EVENT_2";
            this.EVENT_2.Size = new System.Drawing.Size(71, 18);
            this.EVENT_2.TabIndex = 1;
            this.EVENT_2.Text = "EVENT_2";
            // 
            // EVENT_1
            // 
            this.EVENT_1.AutoSize = true;
            this.EVENT_1.Location = new System.Drawing.Point(475, 135);
            this.EVENT_1.Name = "EVENT_1";
            this.EVENT_1.Size = new System.Drawing.Size(71, 18);
            this.EVENT_1.TabIndex = 1;
            this.EVENT_1.Text = "EVENT_1";
            // 
            // label_presetValue
            // 
            this.label_presetValue.AutoSize = true;
            this.label_presetValue.Location = new System.Drawing.Point(24, 83);
            this.label_presetValue.Name = "label_presetValue";
            this.label_presetValue.Size = new System.Drawing.Size(44, 18);
            this.label_presetValue.TabIndex = 1;
            this.label_presetValue.Text = "预设";
            // 
            // checkBox_doubleWord
            // 
            this.checkBox_doubleWord.AutoSize = true;
            this.checkBox_doubleWord.Location = new System.Drawing.Point(30, 23);
            this.checkBox_doubleWord.Name = "checkBox_doubleWord";
            this.checkBox_doubleWord.Size = new System.Drawing.Size(70, 22);
            this.checkBox_doubleWord.TabIndex = 0;
            this.checkBox_doubleWord.Text = "双字";
            this.checkBox_doubleWord.UseVisualStyleBackColor = true;
            this.checkBox_doubleWord.Visible = false;
            this.checkBox_doubleWord.CheckedChanged += new System.EventHandler(this.checkBox_doubleWord_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox_inputmode);
            this.groupBox1.Controls.Add(this.comboBox_Type);
            this.groupBox1.Controls.Add(this.label_inputmode);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(908, 109);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // comboBox_inputmode
            // 
            this.comboBox_inputmode.BackColor = System.Drawing.Color.White;
            this.comboBox_inputmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_inputmode.FormattingEnabled = true;
            this.comboBox_inputmode.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.comboBox_inputmode.Location = new System.Drawing.Point(590, 44);
            this.comboBox_inputmode.Name = "comboBox_inputmode";
            this.comboBox_inputmode.Size = new System.Drawing.Size(141, 26);
            this.comboBox_inputmode.TabIndex = 2;
            this.comboBox_inputmode.SelectedIndexChanged += new System.EventHandler(this.comboBox_inputmode_SelectedIndexChanged);
            this.comboBox_inputmode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_inputmode_KeyPress);
            // 
            // FormHighInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 838);
            this.Controls.Add(this.panel2);
            this.Name = "FormHighInput";
            this.Text = "FormHighInput";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHighInput_FormClosing);
            this.Load += new System.EventHandler(this.FormHighInput_Load);
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label_inputmode;
        private System.Windows.Forms.Label label_pulse;
        private System.Windows.Forms.Label label_direction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Type;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox_inputmode;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox_caputre;
        private System.Windows.Forms.CheckBox checkBox_preset;
        private System.Windows.Forms.CheckBox checkBox_direction;
        private System.Windows.Forms.CheckBox checkBox_pulse;
        private System.Windows.Forms.Label label_presetInput;
        private System.Windows.Forms.Label label_caputreInput;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox_trigger1;
        private System.Windows.Forms.ComboBox comboBox_trigger0;
        private System.Windows.Forms.TextBox textBox_eventName1;
        private System.Windows.Forms.TextBox textBox_eventName0;
        private System.Windows.Forms.TextBox textBox_threshold1;
        private System.Windows.Forms.TextBox textBox_threshold0;
        private System.Windows.Forms.TextBox textBox_presetValue;
        private System.Windows.Forms.Label label_limite1;
        private System.Windows.Forms.Label label_limite0;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label EVENT_2;
        private System.Windows.Forms.Label EVENT_1;
        private System.Windows.Forms.Label label_presetValue;
        private System.Windows.Forms.CheckBox checkBox_doubleWord;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBox_frequencyPulse;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton radioButton_1s;
        private System.Windows.Forms.RadioButton radioButton_100ms;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_dirInputPort;
        private System.Windows.Forms.TextBox textBox_pulseInputPort;
        private System.Windows.Forms.TextBox textBox_pulseFrequencyPort;
        private System.Windows.Forms.CheckBox checkBox_frequencyDoubleWord;
        private System.Windows.Forms.TextBox textBox_presetPort;
        private System.Windows.Forms.TextBox textBox_capturePort;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_valid;
    }
}