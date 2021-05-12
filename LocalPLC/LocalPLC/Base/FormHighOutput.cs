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
        enum TimeBase { ZEROPOINTONE, ONE, TEN, ONETHOUSAND}
        enum OutputMode { PULSE_DIC, CW_CCW, AB_DIRECTION}
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

            foreach(var outputMode in outputPluseDic)
            {
                comboBox_outputMode.Items.Add(outputMode.Value);
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
            if(hspData_.name == "HSP0")
            {
                channelName = "DO04";
            }
            else if(hspData_.name == "HSP1")
            {
                channelName = "DO05";
            }
            else if(hspData_.name == "HSP2")
            {
                channelName = "DO06";
            }
            else if(hspData_.name == "HSP3")
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
                }
            }



        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void FormHighOutput_FormClosing(object sender, FormClosingEventArgs e)
        {
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
            if (hspData_.name == "HSP0")
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
            else if (hspData_.name == "HSP1")
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
            else if (hspData_.name == "HSP2")
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
            else if (hspData_.name == "HSP3")
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
        }

        void getDataFromUI()
        {
            if (comboBox_outputType.SelectedIndex == (int)UserControlHighOutput.TYPE.NOTUSED)
            {
                hspData_.type = comboBox_outputType.SelectedIndex;
                hspData_.used = false;
                //设置为未选
                UserControlBase.dataManage.setDoutUsed(hspData_.pulsePort, false);
                UserControlBase.dataManage.setDoutUsed(hspData_.directionPort, false);
                hspData_.directionPort = "";
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
                    UserControlBase.dataManage.setDoutUsed(hspData_.pulsePort, true);
                }
                else
                {
                    MessageBox.Show("脉冲端口没有设置");
                    comboBox_pulse.Focus();
                }

                UserControlBase.dataManage.setDoutUsed(hspData_.directionPort, false);
                hspData_.directionPort = "";
                hspData_.used = true;
            }
            else if (comboBox_outputType.SelectedIndex == (int)UserControlHighOutput.TYPE.PWM)
            {
                hspData_.type = comboBox_outputType.SelectedIndex;
                hspData_.timeBase = comboBox_timeBase.SelectedIndex;
                int.TryParse(textBox_preset.Text, out hspData_.preset);
                if (comboBox_pulse.SelectedItem != null)
                {
                    hspData_.pulsePort = comboBox_pulse.SelectedItem.ToString();
                    UserControlBase.dataManage.setDoutUsed(hspData_.pulsePort, true);
                }
                else
                {
                    MessageBox.Show("脉冲端口没有设置");
                    comboBox_pulse.Focus();
                }

                UserControlBase.dataManage.setDoutUsed(hspData_.directionPort, false);
                hspData_.directionPort = "";
                hspData_.used = true;
            }
            else if (comboBox_outputType.SelectedIndex == (int)UserControlHighOutput.TYPE.FREQUENCY)
            {
                hspData_.type = comboBox_outputType.SelectedIndex;
                if (comboBox_pulse.SelectedItem != null)
                {
                    hspData_.pulsePort = comboBox_pulse.SelectedItem.ToString();
                    UserControlBase.dataManage.setDoutUsed(hspData_.pulsePort, true);
                }
                else
                {
                    MessageBox.Show("脉冲端口没有设置");
                    comboBox_pulse.Focus();
                }

                int.TryParse(textBox_frequency.Text, out hspData_.signalFrequency);
                UserControlBase.dataManage.setDoutUsed(hspData_.directionPort, false);
                hspData_.directionPort = "";
                hspData_.used = true;
            }
            else if (comboBox_outputType.SelectedIndex == (int)UserControlHighOutput.TYPE.PTO)
            {
                hspData_.type = comboBox_outputType.SelectedIndex;

                if (comboBox_pulse.SelectedItem != null)
                {
                    hspData_.pulsePort = comboBox_pulse.SelectedItem.ToString();
                    UserControlBase.dataManage.setDoutUsed(hspData_.pulsePort, true);
                }
                else
                {
                    MessageBox.Show("脉冲端口没有设置");
                    comboBox_pulse.Focus();
                }

                if (comboBox_direction.SelectedItem != null)
                {
                    //把上一次的方向端口设置为未选
                    UserControlBase.dataManage.setDoutUsed(hspData_.directionPort, false);
                    hspData_.directionPort = comboBox_direction.SelectedItem.ToString();
                    UserControlBase.dataManage.setDoutUsed(hspData_.directionPort, true);
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

        private void button_valid_Click(object sender, EventArgs e)
        {
            getDataFromUI();

            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
