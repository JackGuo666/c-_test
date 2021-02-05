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
    public partial class FormHighInput : Form
    {
        Point posRegular;
        Point posInput;
        Point posFrequency;

        Point posDirLabel;
        Point posDirCheckBox;
        Point posDirPortLabel;
        Point posPresetLabel;
        Point posPresetCheckBox;
        Point posPresetPortCombo;
        Point posCaptureLabel;
        Point posCaptureCheckBox;
        Point posCapturePortLabel;

        int hscType_ = 0;
        xml.HSCData hscData_ = null;

        Dictionary<int, string> inputDic = new Dictionary<int, string>();
        Dictionary<int, string> triggerDic = new Dictionary<int, string>();
        enum INPUTMODE {PULSE_DIR, INTEGRAL_1, INTEGRAL_2, INTEGRAL_4 }
        enum TRIGGER { NOTUSED, FAILING_EDGE, RSIING_EDGE, FAILING_RSIING_EDGE }
        public FormHighInput(Dictionary<int, string> typeDescDic, LocalPLC.Base.xml.HSCData hscData)
        {
            InitializeComponent();

            posRegular = groupBox2.Location;
            posInput = groupBox3.Location;
            posFrequency = groupBox4.Location;

            posDirLabel = label_direction.Location;
            posDirCheckBox = checkBox_direction.Location;
            posDirPortLabel = textBox_dirInputPort.Location;

            posPresetLabel = label_presetInput.Location;
            posPresetCheckBox = checkBox_preset.Location;
            posPresetPortCombo = comboBox_presetPort.Location;

            hscType_ = hscData.type;
            hscData_ = hscData;

            posCaptureLabel = label_caputreInput.Location;
            posCaptureCheckBox = checkBox_caputre.Location;
            posCapturePortLabel = comboBox_capturePort.Location;

            foreach (var elem in typeDescDic)
            {
                comboBox_Type.Items.Add(elem.Value);
            }


            //comboBox_Type.Items.Add("未配置");
            //comboBox_Type.Items.Add("单脉冲计数");
            //comboBox_Type.Items.Add("双相脉冲计数");
            //comboBox_Type.Items.Add("正交编码器");



            inputDic.Clear();
            inputDic.Add((int)INPUTMODE.PULSE_DIR, "脉冲 / 方向");
            inputDic.Add((int)INPUTMODE.INTEGRAL_1, "积分 X1");
            inputDic.Add((int)INPUTMODE.INTEGRAL_2, "积分 X2");
            inputDic.Add((int)INPUTMODE.INTEGRAL_4, "积分 X4");


            foreach (var input in inputDic)
            {
                comboBox_inputmode.Items.Add(input.Value);
            }

            ////输入模式
            comboBox_inputmode.SelectedIndex = hscData_.inputMode;

            triggerDic.Clear();
            triggerDic.Add((int)INPUTMODE.PULSE_DIR, "未使用");
            triggerDic.Add((int)INPUTMODE.INTEGRAL_1, "下降沿");
            triggerDic.Add((int)INPUTMODE.INTEGRAL_2, "上升沿");
            triggerDic.Add((int)INPUTMODE.INTEGRAL_4, "上升/下降沿");

            foreach (var trigger in triggerDic)
            {
                comboBox_trigger0.Items.Add(trigger.Value);
                comboBox_trigger1.Items.Add(trigger.Value);
            }

            foreach(var di in UserControlBase.dataManage.diList)
            {
                comboBox_presetPort.Items.Add(di.channelName);
            }


            foreach (var di in UserControlBase.dataManage.diList)
            {
                comboBox_capturePort.Items.Add(di.channelName);
            }


            comboBox_trigger0.SelectedIndex = hscData_.trigger0;
            comboBox_trigger1.SelectedIndex = hscData_.trigger1;


            comboBox_Type.TextChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
            comboBox_Type.SelectedIndex = hscData_.type;



            this.Name = hscData_.name;
            //双字
            checkBox_doubleWord.Checked = hscData_.doubleWord;
            textBox_presetValue.Text = hscData_.preset.ToString();
            textBox_threshold0.Text = hscData_.thresholdS0.ToString();
            textBox_threshold1.Text = hscData_.thresholdS1.ToString();



            //事件名
            textBox_eventName0.Text = hscData_.eventName0;
            textBox_eventName1.Text = hscData_.eventName1;


            //
            checkBox_pulse.Checked = hscData_.pulseChecked;
            checkBox_direction.Checked = hscData_.dirChecked;
            checkBox_preset.Checked = hscData_.presetChecked;
            checkBox_caputre.Checked = hscData_.captureChecked;
            //
            textBox_pulseInputPort.Text = hscData_.pulsePort;
            textBox_dirInputPort.Text = hscData_.dirPort;
            comboBox_presetPort.Text = hscData_.presetPort;
            comboBox_capturePort.Text = hscData_.capturePort;

            //频率计
            checkBox_frequencyPulse.Checked = hscData_.pulseFrequencyChecked;
            textBox_pulseFrequencyPort.Text = hscData_.pulseFrequencyInputPort;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentIndex = this.comboBox_Type.SelectedIndex;
            if (currentIndex < 0) return;

            if(currentIndex == 0)
            {
                label_inputmode.Visible = false;
                comboBox_inputmode.Visible = false;


                groupBox2.Visible = false;
                groupBox3.Visible = false;
                groupBox4.Visible = false;
            }
            else
            {

                //label_direction.Visible = true;
                //label_pulse.Visible = true;

                label_inputmode.Visible = true;
                comboBox_inputmode.Visible = true;

                if (currentIndex == 1)
                {
                    //单脉冲计数
                    //label_direction.Text = "脉冲输入:";
                    //label_pulse.Text = "方向输入:";
                    //comboBox_inputmode.Text = "脉冲/方向";
                    comboBox_inputmode.Visible = false;
                    label_inputmode.Visible = false;
                    groupBox2.Visible = true;
                    groupBox3.Visible = true;
                    groupBox4.Visible = false;



                    label_direction.Visible = false;
                    checkBox_direction.Visible = false;
                    textBox_dirInputPort.Visible = false;

                    label_presetInput.Location = posDirLabel;
                    checkBox_preset.Location = posDirCheckBox;
                    comboBox_presetPort.Location = posDirPortLabel;
                    label_presetInput.Visible = true;
                    checkBox_preset.Visible = true;
                    comboBox_presetPort.Visible = true;

                    label_caputreInput.Location = posPresetLabel;
                    checkBox_caputre.Location = posPresetCheckBox;
                    comboBox_capturePort.Location = posPresetPortCombo;
                    label_caputreInput.Visible = true;
                    checkBox_caputre.Visible = true;
                    comboBox_capturePort.Visible = true;
                }
                else if (currentIndex == 2)
                {
                    //双向
                    //label_direction.Text = "脉冲输入:";
                    //label_pulse.Text = "脉冲输入:";
                    //comboBox_inputmode.Text = "脉冲/脉冲";
                    groupBox2.Visible = true;
                    groupBox3.Visible = true;
                    groupBox4.Visible = false;

                    label_direction.Visible = true;
                    checkBox_direction.Visible = true;
                    textBox_dirInputPort.Visible = true;


                    label_direction.Location = posDirLabel;
                    checkBox_direction.Location = posDirCheckBox;
                    textBox_dirInputPort.Location = posDirPortLabel;

                    label_presetInput.Location = posPresetLabel;
                    checkBox_preset.Location = posPresetCheckBox;
                    comboBox_presetPort.Location = posPresetPortCombo;

                    label_caputreInput.Location = posCaptureLabel;
                    checkBox_caputre.Location = posCaptureCheckBox;
                    comboBox_capturePort.Location = posCapturePortLabel;
                }
                else if (currentIndex == 3)
                {
                    //正交
                    //label_direction.Text = "脉冲输入:";
                    //label_pulse.Text = "脉冲输入:";
                    //comboBox_inputmode.Text = "脉冲/脉冲";
                    comboBox_inputmode.Visible = false;
                    label_inputmode.Visible = false;
                    groupBox2.Visible = false;
                    groupBox3.Visible = false;
                    groupBox4.Visible = true;

                    groupBox4.Location = posRegular;
                }
            }
            


        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_inputmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(((int)INPUTMODE.PULSE_DIR) == comboBox_inputmode.SelectedIndex &&
                comboBox_Type.SelectedIndex == (int)UserControlHighIn.TYPE.DOUBLEPULSE)
            {
                label_pulse.Text = "脉冲输入";
                label_direction.Text = "方向输入";
            }
            else if(((int)INPUTMODE.INTEGRAL_1) == comboBox_inputmode.SelectedIndex &&
                comboBox_Type.SelectedIndex == (int)UserControlHighIn.TYPE.DOUBLEPULSE)
            {
                label_pulse.Text = "脉冲输入相位A";
                label_direction.Text = "脉冲输入相位B";
            }
            else if (((int)INPUTMODE.INTEGRAL_2) == comboBox_inputmode.SelectedIndex &&
                comboBox_Type.SelectedIndex == (int)UserControlHighIn.TYPE.DOUBLEPULSE)
            {
                label_pulse.Text = "脉冲输入相位A";
                label_direction.Text = "脉冲输入相位B";
            }
            else if (((int)INPUTMODE.INTEGRAL_4) == comboBox_inputmode.SelectedIndex &&
                comboBox_Type.SelectedIndex == (int)UserControlHighIn.TYPE.DOUBLEPULSE)
            {
                label_pulse.Text = "脉冲输入相位A";
                label_direction.Text = "脉冲输入相位B";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormHighInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(comboBox_Type.SelectedIndex == (int)UserControlHighIn.TYPE.NOTUSED)
            {
                hscData_.type = comboBox_Type.SelectedIndex;
            }
            else if(comboBox_Type.SelectedIndex == (int)UserControlHighIn.TYPE.SINGLEPULSE)
            {
                hscData_.type = comboBox_Type.SelectedIndex;
                hscData_.used = true;
                //双字
                hscData_.doubleWord = checkBox_doubleWord.Checked;
                int.TryParse(textBox_presetValue.Text, out hscData_.preset);
                int.TryParse(textBox_threshold0.Text, out hscData_.thresholdS0);
                int.TryParse(textBox_threshold1.Text, out hscData_.thresholdS1);
                hscData_.eventID0 = EVENT_1.Text;
                hscData_.eventID1 = EVENT_2.Text;
                hscData_.eventName0 = textBox_eventName0.Text;
                hscData_.eventName1 = textBox_eventName1.Text;
                hscData_.trigger0 = comboBox_trigger0.SelectedIndex;
                hscData_.trigger1 = comboBox_trigger1.SelectedIndex;

                hscData_.pulseChecked = checkBox_pulse.Checked;
                hscData_.dirChecked = checkBox_direction.Checked;
                hscData_.presetChecked = checkBox_preset.Checked;
                hscData_.captureChecked = checkBox_caputre.Checked;

                hscData_.pulsePort = textBox_pulseInputPort.Text;
                hscData_.dirPort = textBox_dirInputPort.Text;

                if (comboBox_presetPort.SelectedItem != null)
                {
                    hscData_.presetPort = comboBox_presetPort.SelectedItem.ToString();
                }
                else
                {
                    hscData_.presetPort = "";
                }

                if(comboBox_capturePort.SelectedItem != null)
                {
                    hscData_.capturePort = comboBox_capturePort.SelectedItem.ToString();
                }
                else
                {
                    hscData_.capturePort = "";
                }

            }
            else if (comboBox_Type.SelectedIndex == (int)UserControlHighIn.TYPE.DOUBLEPULSE)
            {
                hscData_.type = comboBox_Type.SelectedIndex;
                hscData_.used = true;
                //输入模式
                if (comboBox_inputmode.SelectedItem != null)
                {
                    hscData_.inputMode = comboBox_inputmode.SelectedIndex;
                }
                else
                {
                    hscData_.inputMode = (int)INPUTMODE.PULSE_DIR;
                }


                //双字
                hscData_.doubleWord = checkBox_doubleWord.Checked;
                int.TryParse(textBox_presetValue.Text, out hscData_.preset);
                int.TryParse(textBox_threshold0.Text, out hscData_.thresholdS0);
                int.TryParse(textBox_threshold1.Text, out hscData_.thresholdS1);
                hscData_.eventID0 = EVENT_1.Text;
                hscData_.eventID1 = EVENT_2.Text;
                hscData_.eventName0 = textBox_eventName0.Text;
                hscData_.eventName1 = textBox_eventName1.Text;
                hscData_.trigger0 = comboBox_trigger0.SelectedIndex;
                hscData_.trigger1 = comboBox_trigger1.SelectedIndex;

                hscData_.pulseChecked = checkBox_pulse.Checked;
                hscData_.dirChecked = checkBox_direction.Checked;
                hscData_.presetChecked = checkBox_preset.Checked;
                hscData_.captureChecked = checkBox_caputre.Checked;

                hscData_.pulsePort = textBox_pulseInputPort.Text;
                hscData_.dirPort = textBox_dirInputPort.Text;

                if (comboBox_presetPort.SelectedItem != null)
                {
                    hscData_.presetPort = comboBox_presetPort.SelectedItem.ToString();
                }
                else
                {
                    hscData_.presetPort = "";
                }

                if (comboBox_capturePort.SelectedItem != null)
                {
                    hscData_.capturePort = comboBox_capturePort.SelectedItem.ToString();
                }
                else
                {
                    hscData_.capturePort = "";
                }
            }
            else if(comboBox_Type.SelectedIndex == (int)UserControlHighIn.TYPE.FREQUENCY)
            {
                hscData_.used = true;
                //时间窗口
                hscData_.frequencyDoubleWord = checkBox_frequencyDoubleWord.Checked;
                if (radioButton_100ms.Checked)
                {
                    hscData_.timeWindow = 0;    //100ms
                }
                else if (radioButton_1s.Checked)
                {
                    hscData_.timeWindow = 1;    //1s
                }

                hscData_.pulseFrequencyChecked = checkBox_frequencyPulse.Checked;
                hscData_.pulseFrequencyInputPort = textBox_pulseFrequencyPort.Text;
            }
        }
    }


}
