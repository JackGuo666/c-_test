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
        Dictionary<int, string> timeBaseDic = new Dictionary<int, string>();
        Dictionary<int, string> outputPluseDic = new Dictionary<int, string>();
        enum TimeBase { ZEROPOINTFIVE, ONE, TEN, ONETHOUSAND}
        enum OutputMode {CW_CCW, PULSE_DIC, AB_DIRECTION}
        public FormHighOutput(Dictionary<int, string> typeDescDic, LocalPLC.Base.xml.HSPData hspData)
        {
            InitializeComponent();

            posRegular = panel1.Location;
            posDoubleWord = panel2.Location;
            posPeriod = panel3.Location;
            posFreqency = panel4.Location;
            posPTO = panel5.Location;


            timeBaseDic.Clear();
            timeBaseDic.Add((int)TimeBase.ZEROPOINTFIVE, "0.1毫秒");
            timeBaseDic.Add((int)TimeBase.ONE, "1毫秒");
            timeBaseDic.Add((int)TimeBase.TEN, "10毫秒");
            timeBaseDic.Add((int)TimeBase.ONETHOUSAND, "1秒");

            outputPluseDic.Clear();
            outputPluseDic.Add((int)OutputMode.CW_CCW, "CW / CCW");
            outputPluseDic.Add((int)OutputMode.PULSE_DIC, "脉冲 / 方向");
            outputPluseDic.Add((int)OutputMode.AB_DIRECTION, "正交输出");

            this.Text = hspData.name;

            typeDescDic_ = typeDescDic;
            hspData_ = hspData;
            hspType_ = hspData.type;

            foreach (var elem in typeDescDic)
            {
                comboBox_outputType.Items.Add(elem.Value);
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
                comboBox_pulse.Items.Add(dOut.channelName);
            }

            foreach (var dOut in UserControlBase.dataManage.doList)
            {
                comboBox_direction.Items.Add(dOut.channelName);
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

            //脉冲
            comboBox_pulse.SelectedItem = hspData.pulsePort;
            //方向
            comboBox_direction.SelectedItem = hspData.directionPort;

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentIndex = this.comboBox_outputType.SelectedIndex;
            if (currentIndex < 0) return;

            if (currentIndex == 0)
            {
                //this.panel2.Controls.Clear();
                this.Controls.Remove(panel2);
                this.Controls.Remove(panel3);
                this.Controls.Remove(panel4);
                this.Controls.Remove(panel5);
            }
            else
            {

                //PLS
                if (currentIndex == 1)
                {
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

                    panel5.Location = posDoubleWord;
                    this.Controls.Add(panel5);
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
                hspData_.outputMode = comboBox_direction.SelectedIndex;
                hspData_.used = true;
            }
            //if(comboBox_outputType == 0)
            //hspData_.type
        }
    }
}
