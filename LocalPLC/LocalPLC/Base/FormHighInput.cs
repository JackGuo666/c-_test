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
            posDirPortLabel = label1_dirPort.Location;

            posPresetLabel = label_presetInput.Location;
            posPresetCheckBox = checkBox_preset.Location;
            posPresetPortCombo = comboBox_preset.Location;



            posCaptureLabel = label_caputreInput.Location;
            posCaptureCheckBox = checkBox_caputre.Location;
            posCapturePortLabel = comboBox_capture.Location;

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
            

            foreach(var input in inputDic)
            {
                comboBox_inputmode.Items.Add(input.Value);
            }
            comboBox_inputmode.SelectedIndex = 0;

            triggerDic.Clear();
            triggerDic.Add((int)INPUTMODE.PULSE_DIR, "未使用");
            triggerDic.Add((int)INPUTMODE.INTEGRAL_1, "下降沿");
            triggerDic.Add((int)INPUTMODE.INTEGRAL_2, "上升沿");
            triggerDic.Add((int)INPUTMODE.INTEGRAL_4, "上升/下降沿");

            foreach(var trigger in triggerDic)
            {
                comboBox_trigger0.Items.Add(trigger.Value);
                comboBox_trigger1.Items.Add(trigger.Value);
            }



            comboBox_trigger0.SelectedIndex = 0;
            comboBox_trigger1.SelectedIndex = 0;


            comboBox_Type.TextChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
            comboBox_Type.SelectedIndex = 0;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentIndex = this.comboBox_Type.SelectedIndex;
            if (currentIndex < 0) return;

            if(currentIndex == 0)
            {
                label_direction.Visible = false;
                label_pulse.Visible = false;

                label_inputmode.Visible = false;
                comboBox_inputmode.Visible = false;


                groupBox2.Visible = false;
                groupBox3.Visible = false;
                groupBox4.Visible = false;
            }
            else
            {

                label_direction.Visible = true;
                label_pulse.Visible = true;

                label_inputmode.Visible = true;
                comboBox_inputmode.Visible = true;

                if (currentIndex == 1)
                {
                    //单脉冲计数
                    label_direction.Text = "脉冲输入:";
                    label_pulse.Text = "方向输入:";
                    //comboBox_inputmode.Text = "脉冲/方向";
                    comboBox_inputmode.Visible = false;
                    label_inputmode.Visible = false;
                    groupBox2.Visible = true;
                    groupBox3.Visible = true;
                    groupBox4.Visible = false;

                    label_direction.Visible = false;
                    checkBox_direction.Visible = false;
                    label1_dirPort.Visible = false;

                    label_presetInput.Location = posDirLabel;
                    checkBox_preset.Location = posDirCheckBox;
                    comboBox_preset.Location = posDirPortLabel;

                    label_caputreInput.Location = posPresetLabel;
                    checkBox_caputre.Location = posPresetCheckBox;
                    comboBox_capture.Location = posPresetPortCombo;
                }
                else if (currentIndex == 2)
                {
                    //双向
                    label_direction.Text = "脉冲输入:";
                    label_pulse.Text = "脉冲输入:";
                    //comboBox_inputmode.Text = "脉冲/脉冲";
                    groupBox2.Visible = true;
                    groupBox3.Visible = true;
                    groupBox4.Visible = false;

                    label_direction.Visible = true;
                    checkBox_direction.Visible = true;
                    label1_dirPort.Visible = true;


                    label_direction.Location = posDirLabel;
                    checkBox_direction.Location = posDirCheckBox;
                    label1_dirPort.Location = posDirPortLabel;

                    label_presetInput.Location = posPresetLabel;
                    checkBox_preset.Location = posPresetCheckBox;
                    comboBox_preset.Location = posPresetPortCombo;

                    label_caputreInput.Location = posCaptureLabel;
                    checkBox_caputre.Location = posCaptureCheckBox;
                    comboBox_capture.Location = posCapturePortLabel;
                }
                else if (currentIndex == 3)
                {
                    //正交
                    label_direction.Text = "脉冲输入:";
                    label_pulse.Text = "脉冲输入:";
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
    }


}
