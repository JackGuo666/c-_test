using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalPLC.Base
{

    public partial class FormHighOutput : Form
    {
        Dictionary<int, string> typeDescDic_ = null;
        Point posRegular;
        Point posDoubleWord;
        Point posPeriod;
        Point posFreqency;
        Point posPTO;
        LocalPLC.Base.xml.HSPData hspData_ = null;
        int hspType_ = 0;

        int panel1Height = 0;
        Dictionary<int, string> timeBaseDic = new Dictionary<int, string>();
        Dictionary<int, string> outputPluseDic = new Dictionary<int, string>();
        Dictionary<int, string> outputPluseOnlyPulseDic = new Dictionary<int, string>();
        public enum TimeBase { ZEROPOINTONE, ONE, TEN, ONETHOUSAND}
        public enum OutputMode { PULSE_DIC, CW_CCW, AB_DIRECTION}
        ToolTip tip = new ToolTip();
        public FormHighOutput(Dictionary<int, string> typeDescDic, LocalPLC.Base.xml.HSPData hspData)
        {
            InitializeComponent();

            posRegular = panel1.Location;
            posDoubleWord = panel2.Location;
            posPeriod = panel3.Location;
            posFreqency = panel4.Location;
            posPTO = panel5.Location;

            panel1Height = panel1.Height;

            timeBaseDic.Clear();
            timeBaseDic.Add((int)TimeBase.ZEROPOINTONE, "0.1毫秒");
            timeBaseDic.Add((int)TimeBase.ONE, "1毫秒");
            timeBaseDic.Add((int)TimeBase.TEN, "10毫秒");
            timeBaseDic.Add((int)TimeBase.ONETHOUSAND, "1秒");

            outputPluseDic.Clear();
            outputPluseDic.Add((int)OutputMode.PULSE_DIC, "脉冲 / 方向");
            outputPluseDic.Add((int)OutputMode.CW_CCW, "CW / CCW");
            outputPluseDic.Add((int)OutputMode.AB_DIRECTION, "正交输出");


            outputPluseOnlyPulseDic.Clear();
            outputPluseOnlyPulseDic.Add((int)OutputMode.PULSE_DIC, "脉冲 / 方向");

            this.Text = hspData.name;

            typeDescDic_ = typeDescDic;
            hspData_ = hspData;
            hspType_ = hspData.type;
           
            foreach (var elem in typeDescDic)
            {
                comboBox_outputType.Items.Add(elem.Value);
                comboBox_outputType_pto.Items.Add(elem.Value);
            }

            foreach (var elemTimeBase in timeBaseDic)
            {
                comboBox_timeBase.Items.Add(elemTimeBase.Value);
            }


            if (hspData_.address == "HSP0" || hspData_.address == "HSP2")
            {
                foreach (var outputMode in outputPluseDic)
                {
                    comboBox_outputMode.Items.Add(outputMode.Value);
                }
            }
            else
            {
                foreach (var outputMode in outputPluseOnlyPulseDic)
                {
                    comboBox_outputMode.Items.Add(outputMode.Value);
                }
            }



            //comboBox3.Items.Add("未配置");
            //comboBox3.Items.Add("PLS");
            //comboBox3.Items.Add("PWM");
            //comboBox3.Items.Add("频率发生器");



            foreach(var dOut in UserControlBase.dataManage.doList)
            {
                //comboBox_pulse.Items.Add(dOut.channelName);
                comboBox_pulse_pto.Items.Add(dOut.channelName);
            }

            comboBox_outputType.TextChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);

            if (comboBox_outputType.Items.Count > 0)
            {
                comboBox_outputType.SelectedIndex = hspData.type;
                
            }

            if (comboBox_timeBase.Items.Count > 0)
            {
                comboBox_timeBase.SelectedIndex = hspData.timeBase;
            }

            //预设
            textBox_preset.Text = hspData.preset.ToString();

            checkBox_doubleWord.Checked = hspData.doubleWord;

            textBox_frequency.Text = hspData.signalFrequency.ToString();

            //输出模式
            comboBox_outputMode.SelectedIndex = hspData.outputMode;




            button_valid.Enabled = false;
            button_cancel.Enabled = false;
        }

        void deletePtoUserControl()
        {
            panel1.Controls.Remove(label6);
            panel1.Controls.Remove(comboBox_outputMode);
            panel1.Controls.Remove(label_diretion);
            panel1.Controls.Remove(comboBox_direction);

            panel1.Height = panel1Height;
        }

        

        void setPtoUserControl()
        {
            //label6;
            label6.Parent = panel1;

            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(label1.Location.X, 74);
            //label6.Name = "label6";
            label6.Size = new System.Drawing.Size(label1.Width, label1.Height);
            label6.TabIndex = 0;
            label6.Text = "输出模式";

            panel1.Controls.Add(label6);
            panel1.Height = 74 + label6.Height + 10;

            comboBox_outputMode.Parent = panel1;
            this.comboBox_outputMode.FormattingEnabled = true;
            this.comboBox_outputMode.Location = new System.Drawing.Point(comboBox_outputType.Location.X, 71);
            this.comboBox_outputMode.Name = "comboBox_outputMode";
            this.comboBox_outputMode.Size = new System.Drawing.Size(comboBox_outputType.Width, comboBox_outputType.Height);
            this.comboBox_outputMode.TabIndex = 3;
            panel1.Controls.Add(comboBox_outputMode);

            if (hspData_.address == "HSP2")
            {
                foreach (var dout in UserControlBase.dataManage.doList)
                {
                    if (dout.channelName == "DO03")
                    {
                        if (dout.used && dout.hspUsed != hspData_.address)
                        {
                            this.comboBox_outputMode.Items.Clear();
                            foreach (var outputModeOnlyPulse in outputPluseOnlyPulseDic)
                            {
                                comboBox_outputMode.Items.Add(outputModeOnlyPulse.Value);
                            }
                        }
                    }
                }
            }
            else if(hspData_.address == "HSP0")
            {
                foreach (var dout in UserControlBase.dataManage.doList)
                {
                    if(dout.channelName == "DO01")
                    {
                        if (dout.used && dout.hspUsed != hspData_.address)
                        {
                            this.comboBox_outputMode.Items.Clear();
                            foreach (var outputModeOnlyPulse in outputPluseOnlyPulseDic)
                            {
                                comboBox_outputMode.Items.Add(outputModeOnlyPulse.Value);
                            }
                        }
                    }
                }
            }

            comboBox_outputMode.SelectedIndex = (int)OutputMode.PULSE_DIC;


            // 
            // label_diretion
            // 
            this.label_diretion.AutoSize = true;
            this.label_diretion.Location = new System.Drawing.Point(label_pulse.Location.X, 74);
            this.label_diretion.Name = "label_diretion";
            this.label_diretion.Size = new System.Drawing.Size(label_pulse.Width, label_pulse.Height);
            this.label_diretion.TabIndex = 4;
            panel1.Controls.Add(label_diretion);

            // 
            // comboBox_direction
            // 
            this.comboBox_direction.FormattingEnabled = true;
            this.comboBox_direction.Location = new System.Drawing.Point(comboBox_pulse.Location.X, 69);
            this.comboBox_direction.Name = "comboBox_direction";
            this.comboBox_direction.Size = new System.Drawing.Size(comboBox_pulse.Width, comboBox_pulse.Height);
            this.comboBox_direction.TabIndex = 3;

            panel1.Controls.Add(comboBox_direction);



            string channelName = "";
            //方向初始化值
            if(hspData_.address == "HSP0")
            {
                channelName = "DO04";
            }
            else if(hspData_.address == "HSP1")
            {
                channelName = "DO05";
            }
            else if(hspData_.address == "HSP2")
            {
                channelName = "DO06";
            }
            else if(hspData_.address == "HSP3")
            {
                channelName = "DO07";
            }
            
            {
                var doList = UserControlBase.dataManage.doList;
                //DI04默认使用,如果DI04被使用

                int temp = -1;
                for(int i = 0; i < doList.Count; i++)
                {
                    if(doList[i].channelName == channelName)
                    {
                        if (!doList[i].used)
                        {
                            comboBox_direction.SelectedItem = doList[i].channelName;
                            break;
                        }
                        else
                        {
                            temp = i + 1;
                            for(int j = temp; j < doList.Count; j++)
                            {
                                if(!doList[j].used)
                                {
                                    comboBox_direction.SelectedItem = doList[j].channelName;
                                    break;
                                }
                            }

                            break;
                        }
                    }

                }
            }
        }


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentIndex = this.comboBox_outputType.SelectedIndex;
            if (currentIndex < 0) return;

            if (currentIndex == 0)
            {
                //this.panel2.Controls.Clear();
                deletePtoUserControl();

                label_pulse.Visible = false;
                comboBox_pulse.Visible = false;

                this.Controls.Remove(panel2);
                this.Controls.Remove(panel3);
                this.Controls.Remove(panel4);
                this.Controls.Remove(panel5);

                button_valid.Enabled = true;
                button_cancel.Enabled = true;
            }
            else
            {
                label_pulse.Visible = true;
                comboBox_pulse.Visible = true;

                //PLS
                if (currentIndex == 1)
                {
                    deletePtoUserControl();

                    this.Controls.Remove(panel2);
                    this.Controls.Remove(panel3);
                    this.Controls.Remove(panel4);
                    this.Controls.Remove(panel5);

                    panel2.Location = posDoubleWord;
                    panel3.Location = posPeriod;

                    this.Controls.Add(panel2);
                    this.Controls.Add(panel3);

                    //UserControlPto pto = new UserControlPto("");
                    //this.panel2.Controls.Clear();
                    //pto.Dock = DockStyle.Fill;
                    //this.panel2.Controls.Add(pto);
                    button_valid.Enabled = true;
                    button_cancel.Enabled = true;
                }
                else if (currentIndex == 2)
                {
                    //PWM
                    deletePtoUserControl();

                    this.Controls.Remove(panel2);
                    this.Controls.Remove(panel3);
                    this.Controls.Remove(panel4);
                    this.Controls.Remove(panel5);

                    Point point = /*new System.Drawing.Point(12, 106);*/ panel1.Location;

                    //point.Y += panel1.Height + point.Y - 3;
                    panel3.Location = posDoubleWord;

                    this.Controls.Add(panel3);

                    //UserControlPwm pwm = new UserControlPwm("");
                    //this.panel2.Controls.Clear();
                    //pwm.Dock = DockStyle.Fill;
                    //this.panel2.Controls.Add(pwm);

                    button_valid.Enabled = true;
                    button_cancel.Enabled = true;
                }
                else if (currentIndex == 3)
                {

                    //Frequency
                    deletePtoUserControl();
                    this.Controls.Remove(panel2);
                    this.Controls.Remove(panel3);
                    this.Controls.Remove(panel4);
                    this.Controls.Remove(panel5);

                    panel4.Location = posDoubleWord;
                    this.Controls.Add(panel4);

                    //UserControlFreaGen freaGen = new UserControlFreaGen();
                    //this.panel2.Controls.Clear();
                    //freaGen.Dock = DockStyle.Fill;
                    //this.panel2.Controls.Add(freaGen);

                    button_valid.Enabled = true;
                    button_cancel.Enabled = true;
                }
                else if(currentIndex == 4)
                {
                    this.Controls.Remove(panel2);
                    this.Controls.Remove(panel3);
                    this.Controls.Remove(panel4);
                    this.Controls.Remove(panel5);

                    panel2.Location = posRegular;
                    setPtoUserControl();
                    this.Controls.Add(panel2);

                    button_valid.Enabled = true;
                    button_cancel.Enabled = true;
                }
            }



        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            setButtonEnable(true);
        }

        private void comboBox_OutputMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentIndex = this.comboBox_outputMode.SelectedIndex;
            if (currentIndex < 0) return;
            if(currentIndex == (int)OutputMode.PULSE_DIC)
            {
                comboBox_direction.Enabled = true;
                label_pulse.Text = "脉冲";
                label_diretion.Text = "方向";
            }
            else if(currentIndex == (int)OutputMode.CW_CCW)
            {
                if(hspData_.address == "HSP0")
                {
                    comboBox_direction.Enabled = false;
                    comboBox_direction.SelectedItem = "DO01";
                    label_pulse.Text = "顺时针";
                    label_diretion.Text = "逆时针";
                }
                else if(hspData_.address == "HSP2")
                {
                    comboBox_direction.Enabled = false;
                    comboBox_direction.SelectedItem = "DO03";
                    label_pulse.Text = "顺时针";
                    label_diretion.Text = "逆时针";
                }
            }
            else if(currentIndex == (int)OutputMode.AB_DIRECTION)
            {
                if (hspData_.address == "HSP0")
                {
                    comboBox_direction.Enabled = false;
                    comboBox_direction.SelectedItem = "DO01";
                    label_pulse.Text = "脉冲A";
                    label_diretion.Text = "脉冲B";
                }
                else if (hspData_.address == "HSP2")
                {
                    comboBox_direction.Enabled = false;
                    comboBox_direction.SelectedItem = "DO03";
                    label_pulse.Text = "脉冲A";
                    label_diretion.Text = "脉冲B";
                }
            }
            
        }

        private void FormHighOutput_FormClosing(object sender, FormClosingEventArgs e)
        {
            return;

            //e.Cancel = true;

            if(comboBox_outputType.SelectedIndex == (int)UserControlHighOutput.TYPE.NOTUSED)
            {
                hspData_.type = comboBox_outputType.SelectedIndex;
                hspData_.used = false;
            }
            else if (comboBox_outputType.SelectedIndex == (int)UserControlHighOutput.TYPE.PLS)
            {
                hspData_.type = comboBox_outputType.SelectedIndex;
                hspData_.doubleWord = checkBox_doubleWord.Checked;
                hspData_.timeBase = comboBox_timeBase.SelectedIndex;
                int.TryParse(textBox_preset.Text, out hspData_.preset);
                if(comboBox_pulse.SelectedItem != null)
                {
                    hspData_.pulsePort = comboBox_pulse.SelectedItem.ToString();
                }
                else
                {
                    e.Cancel = true;
                    MessageBox.Show("脉冲端口没有设置");
                    comboBox_pulse.Focus();
                }

                hspData_.used = true;
            }
            else if(comboBox_outputType.SelectedIndex == (int)UserControlHighOutput.TYPE.PWM)
            {
                hspData_.type = comboBox_outputType.SelectedIndex;
                hspData_.timeBase = comboBox_timeBase.SelectedIndex;
                int.TryParse(textBox_preset.Text, out hspData_.preset);
                if (comboBox_pulse.SelectedItem != null)
                {
                    hspData_.pulsePort = comboBox_pulse.SelectedItem.ToString();
                }
                else
                {
                    e.Cancel = true;
                    MessageBox.Show("脉冲端口没有设置");
                    comboBox_pulse.Focus();
                }
                hspData_.used = true;
            }
            else if (comboBox_outputType.SelectedIndex == (int)UserControlHighOutput.TYPE.FREQUENCY)
            {
                hspData_.type = comboBox_outputType.SelectedIndex;
                if (comboBox_pulse.SelectedItem != null)
                {
                    hspData_.pulsePort = comboBox_pulse.SelectedItem.ToString();
                }
                else
                {
                    e.Cancel = true;
                    MessageBox.Show("脉冲端口没有设置");
                    comboBox_pulse.Focus();
                }

                int.TryParse(textBox_frequency.Text, out hspData_.signalFrequency);
                hspData_.used = true;
            }
            else if (comboBox_outputType.SelectedIndex == (int)UserControlHighOutput.TYPE.PTO)
            {
                hspData_.type = comboBox_outputType.SelectedIndex;

                if (comboBox_pulse.SelectedItem != null)
                {
                    hspData_.pulsePort = comboBox_pulse.SelectedItem.ToString();
                }
                else
                {
                    e.Cancel = true;
                    MessageBox.Show("脉冲端口没有设置");
                    comboBox_pulse.Focus();
                }

                if(comboBox_direction.SelectedItem != null)
                {
                    hspData_.directionPort = comboBox_direction.SelectedItem.ToString();
                }
                else
                {
                    e.Cancel = true;
                    MessageBox.Show("方向端口没有设置");
                    comboBox_direction.Focus();
                }


                //输出模式
                hspData_.outputMode = comboBox_outputMode.SelectedIndex;
                hspData_.used = true;
            }
            //if(comboBox_outputType == 0)
            //hspData_.type
        }



        string direction = "未使用";
        private void FormHighOutput_Load(object sender, EventArgs e)
        {
            //脉冲
            comboBox_pulse.SelectedItem = hspData_.pulsePort;
            comboBox_direction.Items.Add(direction);
            if (hspData_.address == "HSP0")
            {
                comboBox_pulse.Items.Add("DO00");
                comboBox_pulse.SelectedIndex = 0;
                comboBox_pulse.Enabled = false;
                foreach (var dOut in UserControlBase.dataManage.doList)
                {
                    if (dOut.channelName == "DO00" || dOut.used)
                    {
                        if(dOut.channelName == hspData_.directionPort)
                        {
                            comboBox_direction.Items.Add(dOut.channelName);
                        }
                    }
                    else
                    {
                        comboBox_direction.Items.Add(dOut.channelName);
                    }
                }
            }
            else if (hspData_.address == "HSP1")
            {
                comboBox_pulse.Items.Add("DO01");
                comboBox_pulse.SelectedIndex = 0;
                comboBox_pulse.Enabled = false;
                foreach (var dOut in UserControlBase.dataManage.doList)
                {
                    if (dOut.channelName == "DO01" || dOut.used)
                    {
                        if (dOut.channelName == hspData_.directionPort)
                        {
                            comboBox_direction.Items.Add(dOut.channelName);
                        }
                    }
                    else
                    {
                        comboBox_direction.Items.Add(dOut.channelName);
                    }
                }
            }
            else if (hspData_.address == "HSP2")
            {
                comboBox_pulse.Items.Add("DO02");
                comboBox_pulse.SelectedIndex = 0;
                comboBox_pulse.Enabled = false;
                foreach (var dOut in UserControlBase.dataManage.doList)
                {
                    if (dOut.channelName == "DO02" || dOut.used)
                    {
                        if (dOut.channelName == hspData_.directionPort)
                        {
                            comboBox_direction.Items.Add(dOut.channelName);
                        }
                    }
                    else
                    {
                        comboBox_direction.Items.Add(dOut.channelName);
                    }
                }
            }
            else if (hspData_.address == "HSP3")
            {
                comboBox_pulse.Items.Add("DO03");
                comboBox_pulse.SelectedIndex = 0;
                comboBox_pulse.Enabled = false;
                foreach (var dOut in UserControlBase.dataManage.doList)
                {
                    if (dOut.channelName == "DO03" || dOut.used)
                    {
                        if (dOut.channelName == hspData_.directionPort)
                        {
                            comboBox_direction.Items.Add(dOut.channelName);
                        }
                    }
                    else
                    {
                        comboBox_direction.Items.Add(dOut.channelName);
                    }
                }
            }
            else
            {

            }

            //方向
            comboBox_direction.SelectedItem = hspData_.directionPort;
            comboBox_outputMode.SelectedIndex = hspData_.outputMode;

            //tip设置
            tip.AutoPopDelay = 10000;
            tip.InitialDelay = 500;
            tip.ReshowDelay = 500;
            tip.ShowAlways = true;

            setButtonEnable(false);
        }

        void getDataFromUI()
        {
            if (comboBox_outputType.SelectedIndex == (int)UserControlHighOutput.TYPE.NOTUSED)
            {
                hspData_.type = comboBox_outputType.SelectedIndex;
                hspData_.used = false;
                //设置为未选
                UserControlBase.dataManage.setDoutUsed(hspData_.pulsePort, false, hspData_.address);
                UserControlBase.dataManage.setDoutUsed(hspData_.directionPort, false, hspData_.address);
                hspData_.directionPort = "";
                hspData_.directionPort = "";
                hspData_.outputMode = -1;
            }
            else if (comboBox_outputType.SelectedIndex == (int)UserControlHighOutput.TYPE.PLS)
            {
                hspData_.type = comboBox_outputType.SelectedIndex;
                hspData_.doubleWord = checkBox_doubleWord.Checked;
                hspData_.timeBase = comboBox_timeBase.SelectedIndex;
                int.TryParse(textBox_preset.Text, out hspData_.preset);
                if (comboBox_pulse.SelectedItem != null)
                {
                    hspData_.pulsePort = comboBox_pulse.SelectedItem.ToString();
                    UserControlBase.dataManage.setDoutUsed(hspData_.pulsePort, true, hspData_.address);
                }
                else
                {
                    MessageBox.Show("脉冲端口没有设置");
                    comboBox_pulse.Focus();
                }

                UserControlBase.dataManage.setDoutUsed(hspData_.directionPort, false, hspData_.address);
                hspData_.directionPort = "";
                hspData_.used = true;
                hspData_.outputMode = -1;
            }
            else if (comboBox_outputType.SelectedIndex == (int)UserControlHighOutput.TYPE.PWM)
            {
                hspData_.type = comboBox_outputType.SelectedIndex;
                hspData_.timeBase = comboBox_timeBase.SelectedIndex;
                int.TryParse(textBox_preset.Text, out hspData_.preset);
                if (comboBox_pulse.SelectedItem != null)
                {
                    hspData_.pulsePort = comboBox_pulse.SelectedItem.ToString();
                    UserControlBase.dataManage.setDoutUsed(hspData_.pulsePort, true, hspData_.address);
                }
                else
                {
                    MessageBox.Show("脉冲端口没有设置");
                    comboBox_pulse.Focus();
                }

                UserControlBase.dataManage.setDoutUsed(hspData_.directionPort, false, hspData_.address);
                hspData_.directionPort = "";
                hspData_.used = true;
                hspData_.outputMode = -1;
            }
            else if (comboBox_outputType.SelectedIndex == (int)UserControlHighOutput.TYPE.FREQUENCY)
            {
                hspData_.type = comboBox_outputType.SelectedIndex;
                if (comboBox_pulse.SelectedItem != null)
                {
                    hspData_.pulsePort = comboBox_pulse.SelectedItem.ToString();
                    UserControlBase.dataManage.setDoutUsed(hspData_.pulsePort, true, hspData_.address);
                }
                else
                {
                    MessageBox.Show("脉冲端口没有设置");
                    comboBox_pulse.Focus();
                }

                int.TryParse(textBox_frequency.Text, out hspData_.signalFrequency);
                UserControlBase.dataManage.setDoutUsed(hspData_.directionPort, false, hspData_.address);
                hspData_.directionPort = "";
                hspData_.used = true;
                hspData_.outputMode = -1;
            }
            else if (comboBox_outputType.SelectedIndex == (int)UserControlHighOutput.TYPE.PTO)
            {
                hspData_.type = comboBox_outputType.SelectedIndex;

                if (comboBox_pulse.SelectedItem != null)
                {
                    hspData_.pulsePort = comboBox_pulse.SelectedItem.ToString();
                    UserControlBase.dataManage.setDoutUsed(hspData_.pulsePort, true, hspData_.address);
                }
                else
                {
                    MessageBox.Show("脉冲端口没有设置");
                    comboBox_pulse.Focus();
                }

                if (comboBox_direction.SelectedItem != null)
                {
                    //把上一次的方向端口设置为未选
                    UserControlBase.dataManage.setDoutUsed(hspData_.directionPort, false, hspData_.address);
                    hspData_.directionPort = comboBox_direction.SelectedItem.ToString();
                    UserControlBase.dataManage.setDoutUsed(hspData_.directionPort, true, hspData_.address);
                }
                else
                {
                    MessageBox.Show("方向端口没有设置");
                    comboBox_direction.Focus();
                }


                //输出模式
                hspData_.outputMode = comboBox_outputMode.SelectedIndex;
                hspData_.used = true;
            }
        }

        void setButtonEnable(bool enable)
        {
            button_valid.Enabled = enable;
            button_cancel.Enabled = enable;
        }

        private void button_valid_Click(object sender, EventArgs e)
        {
            getDataFromUI();

            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox_outputType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_timeBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_outputMode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_direction_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox_preset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void comboBox_timeBase_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_timeBase_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox_preset_TextChanged(object sender, EventArgs e)
        {
            var value = textBox_preset.Text;
            int ret = -1;
            bool flag = int.TryParse(value, out ret);
            if(!flag)
            {
                textBox_preset.BackColor = Color.Red;
                button_valid.Enabled = false;
                button_cancel.Enabled = true;
                tip.SetToolTip(textBox_preset, "输入值必须为整数!");
                setButtonEnable(false);
            }
            else
            {
                if(comboBox_timeBase.SelectedIndex == (int)TimeBase.ZEROPOINTONE)
                {
                    //10000-20000
                    if(ret < 1 || ret > 20000)
                    {
                        textBox_preset.BackColor = Color.Red;
                        button_valid.Enabled = false;
                        button_cancel.Enabled = true;
                        tip.SetToolTip(textBox_preset, "预设值范围: 1 - 20000");
                    }
                    else
                    {
                        textBox_preset.BackColor = Color.White;
                        tip.SetToolTip(textBox_preset, "");
                        button_valid.Enabled = true;
                        button_cancel.Enabled = true;
                    }

                }
                else if(comboBox_timeBase.SelectedIndex == (int)TimeBase.ONE)
                {
                    //1 - 2000
                    if(ret < 1 || ret > 2000)
                    {
                        textBox_preset.BackColor = Color.Red;
                        button_valid.Enabled = false;
                        button_cancel.Enabled = true;
                        tip.SetToolTip(textBox_preset, "预设值范围: 1 - 2000");
                    }
                    else
                    {
                        textBox_preset.BackColor = Color.White;
                        tip.SetToolTip(textBox_preset, "");
                        button_valid.Enabled = true;
                        button_cancel.Enabled = true;
                    }
                }
                else if(comboBox_timeBase.SelectedIndex == (int)TimeBase.TEN)
                {
                    //1 - 200
                    if( ret < 1 || ret > 200)
                    {
                        textBox_preset.BackColor = Color.Red;
                        button_valid.Enabled = false;
                        button_cancel.Enabled = true;
                        tip.SetToolTip(textBox_preset, "预设值范围: 1 - 200");
                    }
                    else
                    {
                        textBox_preset.BackColor = Color.White;
                        tip.SetToolTip(textBox_preset, "");
                        button_valid.Enabled = true;
                        button_cancel.Enabled = true;
                    }
                }
                else if(comboBox_timeBase.SelectedIndex == (int)TimeBase.ONETHOUSAND)
                {
                    //1 - 2
                    if (ret < 1 || ret > 2)
                    {
                        textBox_preset.BackColor = Color.Red;
                        button_valid.Enabled = false;
                        button_cancel.Enabled = true;
                        tip.SetToolTip(textBox_preset, "预设值范围: 1 - 2");
                    }
                    else
                    {
                        textBox_preset.BackColor = Color.White;
                        tip.SetToolTip(textBox_preset, "");
                        button_valid.Enabled = true;
                        button_cancel.Enabled = true;
                    }
                }
                else
                {

                }

                //textBox_preset.BackColor = Color.White;


            }
        }

        private void textBox_frequency_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void textBox_frequency_TextChanged(object sender, EventArgs e)
        {
            var value = textBox_frequency.Text;
            int ret = -1;
            bool flag = int.TryParse(value, out ret);
            if (flag)
            {
                // 0 - 10000
                if (ret < 0 || ret > 100000)
                {
                    textBox_frequency.BackColor = Color.Red;
                    button_valid.Enabled = false;
                    button_cancel.Enabled = true;
                    tip.SetToolTip(textBox_frequency, "频率值范围: 0 - 100000");
                }
                else
                {
                    textBox_frequency.BackColor = Color.White;
                    tip.SetToolTip(textBox_frequency, "");
                    button_valid.Enabled = true;
                    button_cancel.Enabled = true;
                }
            }
            else
            {
                textBox_frequency.BackColor = Color.Red;
                button_valid.Enabled = false;
                button_cancel.Enabled = true;
                tip.SetToolTip(textBox_frequency, "输入值必须为整数!");
            }
            
        }

        private void comboBox_pulse_SelectedIndexChanged(object sender, EventArgs e)
        {
            setButtonEnable(true);
        }

        private void checkBox_doubleWord_CheckedChanged(object sender, EventArgs e)
        {
            setButtonEnable(true);
        }
    }
}
