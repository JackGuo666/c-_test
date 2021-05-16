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
        enum INPUTMODE { PULSE_DIR, /*INTEGRAL_1,*/ INTEGRAL_2, INTEGRAL_4 }
        enum TRIGGER { NOTUSED, FAILING_EDGE, RSIING_EDGE, FAILING_RSIING_EDGE }
        public enum TIMEBASE { T100ms, T1s }

        List<Base.xml.DIData> diData_ = null;
        bool init = false;

        ToolTip tip = new ToolTip();
        public FormHighInput(Dictionary<int, string> typeDescDic, LocalPLC.Base.xml.HSCData hscData)
        {
            Base.xml.DataManageBase retDataManageBase = null;
            UserControl1.UC.getDataManager(ref retDataManageBase);
            diData_ = retDataManageBase.diList;
            InitializeComponent();


            /////========================
            init = true;

            textBox_presetValue.MaxLength = 5;

            tip.AutoPopDelay = 10000;
            tip.InitialDelay = 500;
            tip.ReshowDelay = 500;
            tip.ShowAlways = true;

            textBox_eventName0.MaxLength = 7;
            textBox_eventName1.MaxLength = 7;
            /////=========================

            posRegular = groupBox2.Location;
            posInput = groupBox3.Location;
            posFrequency = groupBox4.Location;

            posDirLabel = label_direction.Location;
            posDirCheckBox = checkBox_direction.Location;
            posDirPortLabel = textBox_dirInputPort.Location;

            posPresetLabel = label_presetInput.Location;
            posPresetCheckBox = checkBox_preset.Location;
            posPresetPortCombo = textBox_presetPort.Location;

            hscType_ = hscData.type;
            hscData_ = hscData;

            posCaptureLabel = label_caputreInput.Location;
            posCaptureCheckBox = checkBox_caputre.Location;
            //posCapturePortLabel = comboBox_capturePort.Location;
            posCapturePortLabel = textBox_capturePort.Location;

            //根据DI端口，添加有效类型
            initComboType(typeDescDic);
            //foreach (var elem in typeDescDic)
            //{
            //    ComboboxItem comboItem = new ComboboxItem();
            //    comboItem.Text = elem.Value;
            //    comboItem.Value = elem.Key;
            //    comboBox_Type.Items.Add(comboItem);
            // }


            //comboBox_Type.Items.Add("未配置");
            //comboBox_Type.Items.Add("单脉冲计数");
            //comboBox_Type.Items.Add("双相脉冲计数");
            //comboBox_Type.Items.Add("正交编码器");



            inputDic.Clear();
            inputDic.Add((int)INPUTMODE.PULSE_DIR, "脉冲 / 方向");
            //inputDic.Add((int)INPUTMODE.INTEGRAL_1, "积分 X1");
            inputDic.Add((int)INPUTMODE.INTEGRAL_2, "积分 X2");
            inputDic.Add((int)INPUTMODE.INTEGRAL_4, "积分 X4");


            foreach (var input in inputDic)
            {
                comboBox_inputmode.Items.Add(input.Value);
            }

            ////输入模式
            comboBox_inputmode.SelectedIndex = hscData_.inputMode;

            triggerDic.Clear();
            triggerDic.Add((int)TRIGGER.NOTUSED, "未使用");
            triggerDic.Add((int)TRIGGER.FAILING_EDGE, "下降沿");
            triggerDic.Add((int)TRIGGER.RSIING_EDGE, "上升沿");
            triggerDic.Add((int)TRIGGER.FAILING_RSIING_EDGE, "上升/下降沿");

            foreach (var trigger in triggerDic)
            {
                comboBox_trigger0.Items.Add(trigger.Value);
                comboBox_trigger1.Items.Add(trigger.Value);
            }

            //根据HSC号设置textBox_presetPort
            checkBox_preset.Checked = hscData_.presetChecked;
            textBox_presetPort.Text = hscData_.presetPort;
            //textBox_presetPort.BackColor
            //foreach (var di in UserControlBase.dataManage.diList)
            //{
            //    comboBox_presetPort.Items.Add(di.channelName);
            //}

            //根据HSC号设置textBox_presetPort
            checkBox_caputre.Checked = hscData_.captureChecked;
            textBox_capturePort.Text = hscData_.capturePort;
            //foreach (var di in UserControlBase.dataManage.diList)
            //{
            //    comboBox_capturePort.Items.Add(di.channelName);
            //}


            comboBox_trigger0.SelectedIndex = hscData_.trigger0;
            comboBox_trigger1.SelectedIndex = hscData_.trigger1;

            comboBox_Type.TextChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
            foreach (var item1 in comboBox_Type.Items)
            {
                ComboboxItem typeItem = (ComboboxItem)item1;
                if (typeItem.Value.ToString() == hscData_.type.ToString())
                {
                    comboBox_Type.SelectedItem = typeItem;
                }
            }

            //gw
            this.Text = hscData_.address;
            //双字
            checkBox_doubleWord.Checked = hscData_.doubleWord;
            textBox_presetValue.Text = hscData_.preset.ToString();
            textBox_threshold0.Text = hscData_.thresholdS0.ToString();
            textBox_threshold1.Text = hscData_.thresholdS1.ToString();



            //事件名
            textBox_eventName0.Text = hscData_.eventName0;
            textBox_eventName1.Text = hscData_.eventName1;

            EVENT_1.Text = hscData_.eventID0;
            EVENT_2.Text = hscData_.eventID1;

            //脉冲
            checkBox_pulse.Checked = hscData_.pulseChecked;
            //方向
            checkBox_direction.Checked = hscData_.dirChecked;
            //预设
            checkBox_preset.Checked = hscData_.presetChecked;
            //捕获
            checkBox_caputre.Checked = hscData_.captureChecked;
            //
            textBox_pulseInputPort.Text = hscData_.pulsePort;
            textBox_dirInputPort.Text = hscData_.dirPort;
            //comboBox_presetPort.Text = hscData_.presetPort;
            textBox_presetPort.Text = hscData_.presetPort;
            //comboBox_capturePort.Text = hscData_.capturePort;
            textBox_capturePort.Text = hscData_.capturePort;



            if (hscData_.type == (int)UserControlHighIn.TYPE.DOUBLEPULSE)
            {
                //双相 判断预设和捕获是否被别处使用


            }
            else if (hscData_.type == (int)UserControlHighIn.TYPE.SINGLEPULSE)
            {

            }
            else if (hscData_.type == (int)UserControlHighIn.TYPE.FREQUENCY)
            {
                //频率计
                checkBox_frequencyPulse.Checked = hscData_.pulseFrequencyChecked;
                textBox_pulseFrequencyPort.Text = hscData_.pulseFrequencyInputPort;
                if (hscData_.timeWindow == (int)TIMEBASE.T100ms)
                {
                    radioButton_100ms.Checked = true;
                }
                else
                {
                    radioButton_1s.Checked = true;
                }

                checkBox_frequencyDoubleWord.Checked = hscData_.frequencyDoubleWord;
            }

            init = false;

            setButtonEanble(false);
        }

        #region

        void addTypeNotSingleDoublePulseCombo(Dictionary<int, string> typeDescDic)
        {
            foreach (var elem in typeDescDic)
            {
                if (elem.Key != (int)UserControlHighIn.TYPE.DOUBLEPULSE &&
                    elem.Key != (int)UserControlHighIn.TYPE.SINGLEPULSE)
                {
                    ComboboxItem comboItem = new ComboboxItem();
                    comboItem.Text = elem.Value;
                    comboItem.Value = elem.Key;
                    comboBox_Type.Items.Add(comboItem);
                }
            }
        }

        void addTypeNotDoublePulseCombo(Dictionary<int, string> typeDescDic)
        {
            foreach (var elem in typeDescDic)
            {
                if (elem.Key != (int)UserControlHighIn.TYPE.DOUBLEPULSE)
                {
                    ComboboxItem comboItem = new ComboboxItem();
                    comboItem.Text = elem.Value;
                    comboItem.Value = elem.Key;
                    comboBox_Type.Items.Add(comboItem);
                }
            }
        }

        void addTypeCombo(Dictionary<int, string> typeDescDic)
        {
            foreach (var elem in typeDescDic)
            {
                ComboboxItem comboItem = new ComboboxItem();
                comboItem.Text = elem.Value;
                comboItem.Value = elem.Key;
                comboBox_Type.Items.Add(comboItem);
            }
        }

        void initComboType(Dictionary<int, string> typeDescDic)
        {
            if (hscData_.address == "HSC0")
            {
                foreach (var di in diData_)
                {
                    if (di.channelName == "DI01")
                    {
                        if (di.used && di.hscUsed == "HSC4")
                        {
                            //DI01已使用，并且是被HSC4轴使用的，这时HSC0的类型没有双相
                            addTypeNotDoublePulseCombo(typeDescDic);
                        }
                        else
                        {
                            addTypeCombo(typeDescDic);
                        }
                    }
                }
            }
            else if (hscData_.address == "HSC1")
            {
                foreach (var di in diData_)
                {
                    if (di.channelName == "DI03")
                    {
                        if (di.used && di.hscUsed == "HSC5")
                        {
                            //DI01已使用，并且是被HSC4轴使用的，这时HSC0的类型没有双相
                            addTypeNotDoublePulseCombo(typeDescDic);
                        }
                        else
                        {
                            addTypeCombo(typeDescDic);
                        }
                    }
                }
            }
            else if (hscData_.address == "HSC2")
            {
                foreach (var di in diData_)
                {
                    if (di.channelName == "DI05")
                    {
                        if (di.used && (di.hscUsed == "HSC6" || di.hscUsed == "HSC0"))
                        {
                            //DI01已使用，并且是被HSC4轴使用的，这时HSC0的类型没有双相
                            addTypeNotDoublePulseCombo(typeDescDic);
                        }
                        else
                        {
                            addTypeCombo(typeDescDic);
                        }
                    }
                }
            }
            else if (hscData_.address == "HSC3")
            {
                foreach (var di in diData_)
                {
                    if (di.channelName == "DI07")
                    {
                        if (di.used && (di.hscUsed == "HSC7" || di.hscUsed == "HSC1"))
                        {
                            //DI01已使用，并且是被HSC4轴使用的，这时HSC0的类型没有双相
                            addTypeNotDoublePulseCombo(typeDescDic);
                        }
                        else
                        {
                            addTypeCombo(typeDescDic);
                        }
                    }
                }
            }
            else if (hscData_.address == "HSC4" || hscData_.address == "HSC5" ||
                hscData_.address == "HSC6" || hscData_.address == "HSC7")
            {
                //HSC4没有双相
                //addTypeNotDoublePulseCombo(typeDescDic);
                addTypeNotSingleDoublePulseCombo(typeDescDic);
            }
        }
        #endregion


        void setPresetCapureVisble(bool visible)
        {
            //隐藏textBox_presetPort
            textBox_presetPort.Visible = visible;
            textBox_capturePort.Visible = visible;
            checkBox_preset.Visible = visible;
            checkBox_caputre.Visible = visible;
            label_presetInput.Visible = visible;
            label_caputreInput.Visible = visible;
        }

        void setHSCNotUsed(string name)
        {
            if (name == "HSC0")
            {
                textBox_pulseInputPort.Text = "DI00";
                textBox_dirInputPort.Text = "DI01";
                textBox_presetPort.Text = "DI04";
                textBox_capturePort.Text = "DI05";
            }
        }

        void setHSCPortDoublePulse(string name)
        {
            //双相
            //if (comboBox_Type.SelectedIndex == (int)UserControlHighIn.TYPE.DOUBLEPULSE)
            {
                checkBox_pulse.Checked = true;
                checkBox_direction.Checked = true;
                if (name == "HSC0")
                {
                    //双相
                    textBox_pulseInputPort.Text = "DI01";
                    textBox_dirInputPort.Text = "DI00";
                    textBox_presetPort.Text = "DI04";
                    textBox_capturePort.Text = "DI05";
                }
                else if (name == "HSC1")
                {
                    checkBox_pulse.Checked = true;
                    textBox_pulseInputPort.Text = "DI03";
                    checkBox_direction.Checked = true;
                    textBox_dirInputPort.Text = "DI02";
                    textBox_presetPort.Text = "DI06";
                    textBox_capturePort.Text = "DI07";
                }
                else if (name == "HSC2")
                {
                    textBox_pulseInputPort.Text = "DI05";
                    textBox_dirInputPort.Text = "DI04";
                    //隐藏textBox_presetPort
                    setPresetCapureVisble(false);
                }
                else if (name == "HSC3")
                {
                    textBox_pulseInputPort.Text = "DI07";
                    textBox_dirInputPort.Text = "DI06";
                    //隐藏textBox_presetPort
                    setPresetCapureVisble(false);
                }
            }
        }

        void setHSCPortSinglePulse(string name)
        {
            checkBox_pulse.Checked = true;
            checkBox_direction.Checked = false;
            if (name == "HSC0")
            {
                //有预设和捕获端口
                textBox_pulseInputPort.Text = "DI00";
                textBox_presetPort.Text = "DI04";
                textBox_capturePort.Text = "DI05";
            }
            else if (name == "HSC1")
            {
                //有预设和捕获端口
                textBox_pulseInputPort.Text = "DI02";
                textBox_presetPort.Text = "DI06";
                textBox_capturePort.Text = "DI07";
            }
            else if (name == "HSC2")
            {
                textBox_pulseInputPort.Text = "DI04";
                setPresetCapureVisble(false);
            }
            else if (name == "HSC3")
            {
                textBox_pulseInputPort.Text = "DI06";
                setPresetCapureVisble(false);
            }
            else if (name == "HSC4")
            {
                textBox_pulseInputPort.Text = "DI01";
                setPresetCapureVisble(false);
            }
            else if (name == "HSC5")
            {
                textBox_pulseInputPort.Text = "DI03";
                setPresetCapureVisble(false);
            }
            else if (name == "HSC6")
            {
                textBox_pulseInputPort.Text = "DI05";
                setPresetCapureVisble(false);
            }
            else if (name == "HSC7")
            {
                textBox_pulseInputPort.Text = "DI07";
                setPresetCapureVisble(false);
            }
        }

        void setFrequecyPortVisible(bool visible)
        {
            checkBox_frequencyPulse.Enabled = visible;
            textBox_pulseFrequencyPort.Enabled = visible;
        }

        void setHSCPortFrequecy(string name)
        {
            checkBox_pulse.Checked = false;
            checkBox_direction.Checked = false;
            checkBox_frequencyPulse.Checked = true;
            if (name == "HSC0")
            {
                textBox_pulseFrequencyPort.Text = "DI00";
                setFrequecyPortVisible(false);
            }
            else if (name == "HSC1")
            {
                textBox_pulseFrequencyPort.Text = "DI02";
                setFrequecyPortVisible(false);
            }
            else if (name == "HSC2")
            {
                textBox_pulseFrequencyPort.Text = "DI04";
                setFrequecyPortVisible(false);
            }
            else if (name == "HSC3")
            {
                textBox_pulseFrequencyPort.Text = "DI06";
                setFrequecyPortVisible(false);
            }
            else if (name == "HSC4")
            {
                textBox_pulseFrequencyPort.Text = "DI01";
                setFrequecyPortVisible(false);
            }
            else if (name == "HSC5")
            {
                textBox_pulseFrequencyPort.Text = "DI03";
                setFrequecyPortVisible(false);
            }
            else if (name == "HSC6")
            {
                textBox_pulseFrequencyPort.Text = "DI05";
                setFrequecyPortVisible(false);
            }
            else if (name == "HSC7")
            {
                textBox_pulseFrequencyPort.Text = "DI07";
                setFrequecyPortVisible(false);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!init)
            {
                setButtonEnable(true);
            }



            var item = (ComboboxItem)this.comboBox_Type.SelectedItem;
            if (item == null)
            {
                return;
            }

            int currentIndex = 0;
            try
            {
                currentIndex = Convert.ToInt32(item.Value);
            }
            catch (Exception)
            {

            }


            if (currentIndex < 0) return;

            if (currentIndex == 0)
            {
                label_inputmode.Visible = false;
                comboBox_inputmode.Visible = false;

                //hscData_.used = false;

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
                    setHSCPortSinglePulse(hscData_.address);


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
                    //comboBox_presetPort.Location = posDirPortLabel;
                    textBox_presetPort.Location = posDirPortLabel;
                    //label_presetInput.Visible = true;
                    //checkBox_preset.Visible = true;
                    ////comboBox_presetPort.Visible = true;
                    //textBox_presetPort.Visible = true;

                    label_caputreInput.Location = posPresetLabel;
                    checkBox_caputre.Location = posPresetCheckBox;
                    //comboBox_capturePort.Location = posPresetPortCombo;
                    textBox_capturePort.Location = posPresetPortCombo;

                    //label_caputreInput.Visible = true;
                    //checkBox_caputre.Visible = true;
                    ////comboBox_capturePort.Visible = true;
                    //textBox_capturePort.Visible = true;
                }
                else if (currentIndex == 2)
                {
                    //双向
                    //label_direction.Text = "脉冲输入:";
                    //label_pulse.Text = "脉冲输入:";
                    //comboBox_inputmode.Text = "脉冲/脉冲";

                    setHSCPortDoublePulse(hscData_.address);


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
                    //comboBox_presetPort.Location = posPresetPortCombo;
                    textBox_presetPort.Location = posPresetPortCombo;

                    label_caputreInput.Location = posCaptureLabel;
                    checkBox_caputre.Location = posCaptureCheckBox;
                    textBox_capturePort.Location = posCapturePortLabel;
                }
                else if (currentIndex == /*3*/ (int)UserControlHighIn.TYPE.FREQUENCY)
                {
                    setHSCPortFrequecy(hscData_.address);
                    //hscData_.pulseFrequencyChecked;
                    //hscData_.pulseFrequencyInputPort;

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
            if (((int)INPUTMODE.PULSE_DIR) == comboBox_inputmode.SelectedIndex &&
                comboBox_Type.SelectedIndex == (int)UserControlHighIn.TYPE.DOUBLEPULSE)
            {
                label_pulse.Text = "脉冲输入";
                label_direction.Text = "方向输入";
            }
            //else if (((int)INPUTMODE.INTEGRAL_1) == comboBox_inputmode.SelectedIndex &&
            //    comboBox_Type.SelectedIndex == (int)UserControlHighIn.TYPE.DOUBLEPULSE)
            //{
            //    label_pulse.Text = "脉冲输入相位A";
            //    label_direction.Text = "脉冲输入相位B";
            //}
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

            if (!init)
            {
                setButtonEanble(true);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        //检查辅助端口 (DI04 DI05) (06 07)
        void checkBaseDIAssistUse(bool isChecked, string diPort)
        {
            //if(di)
            if (diPort == "DI04" | diPort == "DI05" | diPort == "DI06" | diPort == "DI07")
            {

                if (!isChecked)
                {
                    //辅助接点不做修改
                    foreach (var port in diData_)
                    {
                        if (port.channelName == diPort &&
                            (port.hscUsed != "HSC2" && port.hscUsed != "HSC3"
                            && port.hscUsed != "HSC6" && port.hscUsed != "HSC7"))
                        {
                            port.used = isChecked;
                            port.hscUsed = /*hscData_.name*/ "";
                            break;
                        }
                    }

                    return;
                }

            }

            checkBaseDIUse(isChecked, diPort);
        }

        void checkBaseDIUse(bool isChecked, string diPort)
        {

            if (diPort.Length == 0)
            {
                return;
            }

            bool ret = false;
            //if(isChecked)
            {
                foreach (var port in diData_)
                {
                    if (port.channelName == diPort)
                    {
                        port.used = isChecked;
                        if (!isChecked)
                        {
                            port.hscUsed = "";
                        }
                        else
                        {
                            port.hscUsed = hscData_.address;
                        }

                        ret = true;
                        break;
                    }
                }

                if (!ret)
                {
                    //utility.PrintError(string.Format("{0}在基本配置DI模块里没有找到!", diPort));
                }
            }

        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="check"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        bool getPulseDirIsJudge(bool check, string port)
        {
            if (check)
            {
                return true;
            }

            if (port == "DI01")
            {
                //DI01可以是HSC4的端口
                foreach (var di in diData_)
                {
                    if (di.channelName == port)
                    {
                        if (di.hscUsed == "HSC4")
                        {
                            //HSC4已使用DI01，HSC0里不对DI01进行判断
                            return false;
                        }
                    }
                }
            }
            else if (port == "DI03")
            {
                foreach (var di in diData_)
                {
                    if (di.channelName == port)
                    {
                        if (di.hscUsed == "HSC5")
                        {
                            //HSC5脉冲端口已使用DI03，HSC1不对DI03进行判断
                            return false;
                        }
                    }
                }
            }
            else if (port == "DI05")
            {
                foreach (var di in diData_)
                {
                    if (di.channelName == port)
                    {
                        if (di.hscUsed == "HSC6")
                        {
                            //HSC6已使用DI05，HSC2不对DI05进行判断
                            return false;
                        }
                    }
                }
            }
            else if (port == "DI07")
            {
                foreach (var di in diData_)
                {
                    if (di.channelName == port)
                    {
                        if (di.hscUsed == "HSC7")
                        {
                            //HSC7已使用DI07，HSC3不对DI07进行判断

                            return false;
                        }
                    }
                }
            }

            return true;
        }

        void resetLastOpe()
        {
            if (hscData_.type == (int)UserControlHighIn.TYPE.FREQUENCY)
            {
                //频率计
                UserControlBase.dataManage.setDinUsed(hscData_.pulseFrequencyInputPort, false, hscData_.name);
            }
            else if(hscData_.type == (int)UserControlHighIn.TYPE.DOUBLEPULSE)
            {
                //脉冲
                UserControlBase.dataManage.setDinUsed(hscData_.pulsePort, false, hscData_.name);
                //方向
                UserControlBase.dataManage.setDinUsed(hscData_.dirPort, false, hscData_.name);
                if(hscData_.captureChecked)
                {
                    UserControlBase.dataManage.setDinUsed(hscData_.capturePort, false, hscData_.name);
                }

                if(hscData_.presetChecked)
                {
                    UserControlBase.dataManage.setDinUsed(hscData_.presetPort, false, hscData_.name);
                }
            }
            else if(hscData_.type == (int)UserControlHighIn.TYPE.SINGLEPULSE)
            {
                //单相
                UserControlBase.dataManage.setDinUsed(hscData_.pulsePort, false, hscData_.name);
            }
        }

        void getDataFromUI()
        {
            var item = (ComboboxItem)this.comboBox_Type.SelectedItem;
            int currentIndex = 0;
            try
            {
                currentIndex = Convert.ToInt32(item.Value);
            }
            catch (Exception)
            {

            }

            if (currentIndex == (int)UserControlHighIn.TYPE.NOTUSED)
            {
                //if (hscData_.type == (int)UserControlHighIn.TYPE.FREQUENCY)
                //{
                //    //频率计
                //    UserControlBase.dataManage.setDinUsed(hscData_.pulseFrequencyInputPort, false, hscData_.name);
                //}

                resetLastOpe();

                hscData_.type = comboBox_Type.SelectedIndex;
                hscData_.used = false;



                //输入模式
                hscData_.inputMode = (int)INPUTMODE.PULSE_DIR;
                hscData_.opr_mode = "";

                hscData_.pulseChecked = false;
                hscData_.dirChecked = false;
                hscData_.presetChecked = false;
                hscData_.captureChecked = false;

                hscData_.pulsePort = textBox_pulseInputPort.Text;
                hscData_.dirPort = textBox_dirInputPort.Text;
                hscData_.presetPort = textBox_presetPort.Text;
                hscData_.capturePort = textBox_capturePort.Text;

                //di判断是否已用
                checkBaseDIUse(hscData_.pulseChecked, hscData_.pulsePort);
                //checkBaseDIUse(hscData_.dirChecked, hscData_.dirPort);
                bool flag = getPulseDirIsJudge(hscData_.dirChecked, hscData_.dirPort);
                if (flag)
                {
                    checkBaseDIUse(hscData_.dirChecked, hscData_.dirPort);
                }


                //辅助端口，false不处理
                checkBaseDIAssistUse(hscData_.presetChecked, hscData_.presetPort);
                checkBaseDIAssistUse(hscData_.captureChecked, hscData_.capturePort);

                hscData_.timeWindow = 2;
            }
            else if (currentIndex == (int)UserControlHighIn.TYPE.SINGLEPULSE)
            {
                resetLastOpe();

                hscData_.type = /*comboBox_Type.SelectedIndex*/ currentIndex;
                hscData_.used = true;
                hscData_.inputMode = (int)INPUTMODE.PULSE_DIR;
                hscData_.opr_mode = "single_pulse";

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
                hscData_.dirChecked = /*checkBox_direction.Checked*/ false;
                hscData_.presetChecked = checkBox_preset.Checked;
                hscData_.captureChecked = checkBox_caputre.Checked;

                hscData_.pulsePort = textBox_pulseInputPort.Text;
                hscData_.dirPort = /*textBox_dirInputPort.Text*/ "";

                //if (comboBox_presetPort.SelectedItem != null)
                //{
                //    hscData_.presetPort = comboBox_presetPort.SelectedItem.ToString();
                //}
                //else
                //{
                //    hscData_.presetPort = "";
                //}
                hscData_.presetPort = textBox_presetPort.Text;

                //if (comboBox_capturePort.SelectedItem != null)
                //{
                //    hscData_.capturePort = comboBox_capturePort.SelectedItem.ToString();
                //}
                //else
                //{
                //    hscData_.capturePort = "";
                //}
                hscData_.capturePort = textBox_capturePort.Text;

                //di判断是否已用
                checkBaseDIUse(hscData_.pulseChecked, hscData_.pulsePort);
                //方向端口是否判断，下面代码注释
                bool flag = getPulseDirIsJudge(hscData_.dirChecked, hscData_.dirPort);
                if (flag)
                {
                    checkBaseDIUse(hscData_.dirChecked, hscData_.dirPort);
                }


                //辅助端口，false不处理
                checkBaseDIAssistUse(hscData_.presetChecked, hscData_.presetPort);
                checkBaseDIAssistUse(hscData_.captureChecked, hscData_.capturePort);

                hscData_.timeWindow = 2;

            }
            else if (currentIndex == (int)UserControlHighIn.TYPE.DOUBLEPULSE)
            {
                hscData_.type = /*comboBox_Type.SelectedIndex*/ currentIndex;
                hscData_.used = true;
                //输入模式
                if (comboBox_inputmode.SelectedItem != null)
                {
                    hscData_.inputMode = comboBox_inputmode.SelectedIndex;
                    if (hscData_.inputMode == (int)INPUTMODE.PULSE_DIR)
                    {
                        //脉冲方向
                        hscData_.opr_mode = "pulse_dir";
                    }
                    //else if (hscData_.inputMode == (int)INPUTMODE.INTEGRAL_1)
                    //{
                    //    hscData_.opr_mode = "pulse_x1";
                    //}
                    else if (hscData_.inputMode == (int)INPUTMODE.INTEGRAL_2)
                    {
                        hscData_.opr_mode = "pulse_x2";
                    }
                    else if (hscData_.inputMode == (int)INPUTMODE.INTEGRAL_4)
                    {
                        hscData_.opr_mode = "pulse_x4";
                    }
                }
                else
                {
                    hscData_.inputMode = (int)INPUTMODE.PULSE_DIR;
                    hscData_.opr_mode = "";
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


                //if (comboBox_presetPort.SelectedItem != null)
                //{
                //    hscData_.presetPort = comboBox_presetPort.SelectedItem.ToString();
                //}
                //else
                //{
                //    hscData_.presetPort = "";
                //}
                hscData_.presetPort = textBox_presetPort.Text;

                //if (comboBox_capturePort.SelectedItem != null)
                //{
                //    hscData_.capturePort = comboBox_capturePort.SelectedItem.ToString();
                //}
                //else
                //{
                //    hscData_.capturePort = "";
                //}
                hscData_.capturePort = textBox_capturePort.Text;

                //清空频率计控件
                checkBox_frequencyPulse.Checked = false;
                hscData_.pulseFrequencyChecked = checkBox_frequencyPulse.Checked;
                textBox_pulseFrequencyPort.Text = "";
                hscData_.pulseFrequencyInputPort = textBox_pulseFrequencyPort.Text;

                //di判断是否已用
                checkBaseDIUse(hscData_.pulseChecked, hscData_.pulsePort);
                checkBaseDIUse(hscData_.dirChecked, hscData_.dirPort);
                //双相 辅助接点为false不做判断
                checkBaseDIAssistUse(hscData_.presetChecked, hscData_.presetPort);
                checkBaseDIAssistUse(hscData_.captureChecked, hscData_.capturePort);

                hscData_.timeWindow = 2;
            }
            else if (currentIndex == (int)UserControlHighIn.TYPE.FREQUENCY)
            {
                resetLastOpe();




                hscData_.type = /*comboBox_Type.SelectedIndex*/ currentIndex;
                hscData_.used = true;
                hscData_.inputMode = (int)INPUTMODE.PULSE_DIR;
                hscData_.opr_mode = "freq_calc";


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


                //di判断是否已用
                checkBaseDIUse(hscData_.pulseFrequencyChecked, hscData_.pulseFrequencyInputPort);

                hscData_.dirChecked = false;
                hscData_.dirPort = "";
                //方向端口是否判断
                bool flag = getPulseDirIsJudge(hscData_.dirChecked, hscData_.dirPort);
                if (flag)
                {
                    checkBaseDIUse(hscData_.dirChecked, hscData_.dirPort);
                }


                //双相 辅助接点为false不做判断
                hscData_.presetChecked = false;
                checkBaseDIAssistUse(hscData_.presetChecked, hscData_.presetPort);
                hscData_.captureChecked = false;
                checkBaseDIAssistUse(hscData_.captureChecked, hscData_.capturePort);

                utility.PrintError(string.Format("频率计{0}不勾选", hscData_.dirPort));

                //
            }
        }


        private void FormHighInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshData();
        }

        private void checkBox_preset_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_caputre_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_preset_MouseDown(object sender, MouseEventArgs e)
        {
            //预设判断别处是否使用
            foreach (var di in diData_)
            {
                if (di.channelName == textBox_presetPort.Text)
                {
                    if (di.hscUsed != hscData_.address)
                    {
                        if (di.used)
                        {
                            (sender as CheckBox).Checked = false;
                            MessageBox.Show(string.Format("{0}已在{1}使用", di.channelName, di.hscUsed));
                        }
                    }
                }
            }
        }

        private void checkBox_caputre_MouseDown(object sender, MouseEventArgs e)
        {
            //预设判断别处是否使用
            foreach (var di in diData_)
            {
                if (di.channelName == textBox_capturePort.Text)
                {
                    if (di.hscUsed != hscData_.address)
                    {
                        if (di.used)
                        {
                            (sender as CheckBox).Checked = false;
                            MessageBox.Show(string.Format("{0}已在{1}使用", di.channelName, di.hscUsed));
                        }
                    }
                }
            }
        }

        private void FormHighInput_Load(object sender, EventArgs e)
        {

        }

        void setButtonEanble(bool enable)
        {
            button_valid.Enabled = enable;
            button_cancel.Enabled = enable;
        }

        private void comboBox_Type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_doubleWord_CheckedChanged(object sender, EventArgs e)
        {
            if (init)
            {
                return;
            }

            setButtonEanble(true);
        }

        private void comboBox_Type_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_inputmode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox_presetValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void textBox_presetValue_TextChanged(object sender, EventArgs e)
        {
            int max = 65535;//首先设置上限值
            if (textBox_presetValue.Text != null && textBox_presetValue.Text != "")//判断TextBox的内容不为空，如果不判断会导致后面的非数字对比异常
            {
                int temp = 0;
                bool flag = int.TryParse(textBox_presetValue.Text, out temp);
                if (flag)
                {
                    if (temp > max)//num就是传进来的值,如果大于上限（输入的值），那就强制为上限-1，或者就是上限值？
                    {
                        textBox_presetValue.Text = max.ToString();
                    }
                }
                else
                {
                    textBox_presetValue.Text = temp.ToString();
                }
                textBox_presetValue.BackColor = Color.White;
                setButtonEanble(true);
                tip.SetToolTip(textBox_presetValue, "");
            }
            else
            {
                if (textBox_presetValue.Text == "")
                {
                    button_valid.Enabled = false;
                    button_cancel.Enabled = true;
                    textBox_presetValue.BackColor = Color.Red;
                    tip.SetToolTip(textBox_presetValue, "输入值必须为整数!");
                }
            }

        }

        private void textBox_threshold0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void textBox_threshold0_TextChanged(object sender, EventArgs e)
        {
            if (init)
            {
                return;
            }

            int max = 65535;//首先设置上限值
            if (textBox_threshold0.Text != "")
            {
                int temp = 0;
                bool flag = int.TryParse(textBox_threshold0.Text, out temp);
                if (flag)
                {
                    if (temp > max)
                    {
                        textBox_threshold0.Text = max.ToString();
                        temp = max;
                    }
                    else
                    {
                        textBox_threshold0.Text = temp.ToString();
                    }

                    int temp1 = 0;
                    flag = int.TryParse(textBox_threshold1.Text, out temp1);
                    if (!flag)
                    {
                        MessageBox.Show("阈值E1输入值无效,请在阈值1输入有效整数!");
                    }
                    else
                    {
                        if (temp >= temp1)
                        {
                            button_valid.Enabled = false;
                            button_cancel.Enabled = true;
                            textBox_threshold0.BackColor = Color.Red;
                            tip.SetToolTip(textBox_threshold0, "阈值E0要小于阈值E1");
                            return;
                        }
                    }

                    textBox_threshold0.BackColor = Color.White;
                    textBox_threshold1.BackColor = Color.White;
                    setButtonEanble(true);
                    tip.SetToolTip(textBox_threshold0, "");
                    tip.SetToolTip(textBox_threshold1, "");
                }
            }
            else
            {
                button_valid.Enabled = false;
                button_cancel.Enabled = true;
                textBox_threshold0.BackColor = Color.Red;
                tip.SetToolTip(textBox_threshold0, "输入值必须为整数!");
            }
        }

        private void textBox_threshold1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }

        private void textBox_threshold1_TextChanged(object sender, EventArgs e)
        {
            if (init)
            {
                return;
            }

            int max = 65535;//首先设置上限值
            if (textBox_threshold1.Text != "")
            {
                int temp = 0;
                bool flag = int.TryParse(textBox_threshold1.Text, out temp);
                if (flag)
                {
                    if (temp > max)
                    {
                        textBox_threshold1.Text = max.ToString();
                        temp = max;
                    }
                    else
                    {
                        textBox_threshold1.Text = temp.ToString();
                    }


                    int temp0 = 0;
                    flag = int.TryParse(textBox_threshold0.Text, out temp0);
                    if (!flag)
                    {
                        MessageBox.Show("阈值E0输入值无效,请在阈值E0输入有效整数!");
                    }
                    else
                    {
                        //有效判断E0、E1大小
                        if (temp0 >= temp)
                        {
                            button_valid.Enabled = false;
                            button_cancel.Enabled = true;
                            textBox_threshold1.BackColor = Color.Red;
                            tip.SetToolTip(textBox_threshold1, "阈值E1要大于阈值E0");

                            return;
                        }
                    }

                    textBox_threshold1.BackColor = Color.White;
                    textBox_threshold0.BackColor = Color.White;
                    setButtonEanble(true);
                    tip.SetToolTip(textBox_threshold1, "");
                    tip.SetToolTip(textBox_threshold0, "");
                }
            }
            else
            {
                button_valid.Enabled = false;
                button_cancel.Enabled = true;
                textBox_threshold1.BackColor = Color.Red;
                tip.SetToolTip(textBox_threshold1, "输入值必须为整数!");
            }
        }

        private void button_valid_Click(object sender, EventArgs e)
        {
            getDataFromUI();
            setButtonEanble(false);
            Close();
        }

        private void comboBox_trigger0_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_trigger1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        void setButtonEnable(bool enable)
        {
            button_valid.Enabled = enable;
            button_cancel.Enabled = enable;
        }

        System.Text.RegularExpressions.Regex regStr = new System.Text.RegularExpressions.Regex(@"^[\w]{0,7}$");
        private void textBox_eventName0_TextChanged(object sender, EventArgs e)
        {
            if (init)
            {
                return;
            }

            if (!regStr.IsMatch(textBox_eventName0.Text))
            {
                textBox_eventName0.BackColor = Color.Red;
                tip.SetToolTip(textBox_eventName0, string.Format("{0} 格式不对", textBox_eventName0.Text));
                button_valid.Enabled = false;
                button_cancel.Enabled = true;
            }
            else
            {
                if (textBox_eventName0.Text.Length == 0)
                {
                    textBox_eventName0.BackColor = Color.White;
                    tip.SetToolTip(textBox_eventName0, "");
                    setButtonEanble(true);
                }
                else
                {
                    char[] c = textBox_eventName0.Text.ToCharArray();
                    if (c[0] >= '0' && c[0] <= '9')
                    {
                        textBox_eventName0.BackColor = Color.Red;
                        tip.SetToolTip(textBox_eventName0, string.Format("{0} 第一个字符不可以为数", textBox_eventName0.Text));
                        button_valid.Enabled = false;
                        button_cancel.Enabled = true;
                    }
                    else
                    {
                        textBox_eventName0.BackColor = Color.White;
                        tip.SetToolTip(textBox_eventName0, "");

                        setButtonEanble(true);
                    }
                }


            }
        }

        private void textBox_eventName1_TextChanged(object sender, EventArgs e)
        {
            if (init)
            {
                return;
            }

            if (!regStr.IsMatch(textBox_eventName1.Text))
            {
                textBox_eventName1.BackColor = Color.Red;
                tip.SetToolTip(textBox_eventName1, string.Format("{0} 格式不对", textBox_eventName1.Text));
                
                button_valid.Enabled = false;
                button_cancel.Enabled = true;
            }
            else
            {
                if (textBox_eventName1.Text.Length == 0)
                {
                    textBox_eventName1.BackColor = Color.White;
                    tip.SetToolTip(textBox_eventName1, "");
                    setButtonEanble(true);
                }
                else
                {
                    char[] c = textBox_eventName1.Text.ToCharArray();
                    if (c[0] >= '0' && c[0] <= '9')
                    {
                        textBox_eventName1.BackColor = Color.Red;
                        tip.SetToolTip(textBox_eventName1, string.Format("{0} 第一个字符不可以为数", textBox_eventName1.Text));
                        button_valid.Enabled = false;
                        button_cancel.Enabled = true;
                    }
                    else
                    {
                        textBox_eventName1.BackColor = Color.White;
                        tip.SetToolTip(textBox_eventName1, "");

                        setButtonEanble(true);
                    }
                }
            }
        }



        void refreshData()
        {
            foreach(var type in comboBox_Type.Items)
            {
                var comboItem = type as ComboboxItem;
                if(comboItem.Value.ToString() == hscData_.type.ToString())
                {
                    comboBox_Type.SelectedItem = comboItem;
                }
            }

            if (hscData_.type == (int)UserControlHighIn.TYPE.DOUBLEPULSE
                || hscData_.type == (int)UserControlHighIn.TYPE.SINGLEPULSE)
            {
                //双相
                //inputMode
                if (hscData_.type == (int)UserControlHighIn.TYPE.DOUBLEPULSE)
                {
                    comboBox_inputmode.SelectedIndex = hscData_.inputMode;
                }

                //foreach(var inputMode in comboBox_inputmode.Items)
                //{
                //    var item = inputMode as ComboboxItem;
                //    if(item.Value.ToString() == hscData_.inputMode.ToString())
                //    {
                //        comboBox_inputmode.SelectedItem = item;
                //    }

                //}

                //双字
                checkBox_doubleWord.Checked = hscData_.doubleWord;
                //预设
                textBox_presetValue.Text = hscData_.preset.ToString();
                //阈值E0
                textBox_threshold0.Text = hscData_.thresholdS0.ToString();
                //阈值E1
                textBox_threshold1.Text = hscData_.thresholdS1.ToString();
                //事件名E0
                textBox_eventName0.Text = hscData_.eventName0.ToString();
                //事件名E1
                textBox_eventName1.Text = hscData_.eventName1.ToString();
                //事件ID E0
                EVENT_1.Text = hscData_.eventID0;
                //事件ID E1
                EVENT_2.Text = hscData_.eventID1;

                //comboBox_trigger0
                //触发器E0
                comboBox_trigger0.SelectedIndex = hscData_.trigger0;
                //foreach(var trigger in comboBox_trigger0.Items)
                //{
                //    var item = trigger as ComboboxItem;
                //    if(item.Value.ToString() == hscData_.trigger0.ToString())
                //    {
                //        comboBox_trigger0.SelectedItem = item;
                //    }
                //}



                //comboBox_trigger1
                //触发器E1
                comboBox_trigger1.SelectedIndex = hscData_.trigger1;
                //foreach (var trigger in comboBox_trigger1.Items)
                //{
                //    var item = trigger as ComboboxItem;
                //    if(item.Value.ToString() == hscData_.trigger1.ToString())
                //    {
                //        comboBox_trigger1.SelectedItem = item;
                //    }
                //}

                //脉冲输入
                checkBox_pulse.Checked = hscData_.pulseChecked;
                //脉冲端口
                textBox_pulseInputPort.Text = hscData_.pulsePort;

                if (hscData_.type == (int)UserControlHighIn.TYPE.DOUBLEPULSE)
                {
                    //方向输入
                    checkBox_direction.Checked = hscData_.dirChecked;
                    //方向端口
                    textBox_dirInputPort.Text = hscData_.dirPort;
                }


                //预设输入
                checkBox_preset.Checked = hscData_.presetChecked;
                //预设端口
                textBox_presetPort.Text = hscData_.presetPort;

                //捕捉输入
                checkBox_caputre.Checked = hscData_.captureChecked;
                //捕捉端口
                textBox_capturePort.Text = hscData_.capturePort;


            }
            else if(hscData_.type == (int)UserControlHighIn.TYPE.FREQUENCY)
            {
                //频率计
                //双字
                checkBox_frequencyDoubleWord.Checked = hscData_.frequencyDoubleWord;
                //时间窗口
                if(hscData_.timeWindow == (int)TIMEBASE.T100ms)
                {
                    radioButton_100ms.Checked = true;
                    radioButton_1s.Checked = false;
                }
                else
                {
                    radioButton_100ms.Checked = false;
                    radioButton_1s.Checked = true;
                }

                //脉冲输入
                checkBox_frequencyPulse.Checked = hscData_.pulseFrequencyChecked;
                //脉冲端口
                textBox_pulseFrequencyPort.Text = hscData_.pulseFrequencyInputPort;
            }
            else if(hscData_.type == (int)UserControlHighIn.TYPE.NOTUSED)
            {
                foreach (var type in comboBox_Type.Items)
                {
                    var comboItem = type as ComboboxItem;
                    if (comboItem.Value.ToString() == hscData_.type.ToString())
                    {
                        comboBox_Type.SelectedItem = comboItem;
                    }
                }

                //hscData_.type = comboBox_Type.SelectedIndex;
                //hscData_.pulseChecked = false;
                //hscData_.dirChecked = false;
                //hscData_.presetChecked = false;
                //hscData_.captureChecked = false;

                //hscData_.pulsePort = textBox_pulseInputPort.Text;
                //hscData_.dirPort = textBox_dirInputPort.Text;
                //hscData_.presetPort = textBox_presetPort.Text;
                //hscData_.capturePort = textBox_capturePort.Text;
            }

        }








        private void button_cancel_Click(object sender, EventArgs e)
        {
            refreshData();
            Close();
        }

        private void checkBox_frequencyDoubleWord_CheckedChanged(object sender, EventArgs e)
        {
            if (init)
            {
                return;
            }

            setButtonEanble(true);
        }

        private void radioButton_100ms_CheckedChanged(object sender, EventArgs e)
        {
            if (init)
            {
                return;
            }

            setButtonEanble(true);
        }

        private void radioButton_1s_CheckedChanged(object sender, EventArgs e)
        {
            if (init)
            {
                return;
            }

            setButtonEanble(true);
        }

        private void checkBox_preset_CheckedChanged_1(object sender, EventArgs e)
        {
            if (init)
            {
                return;
            }

            setButtonEanble(true);
        }

        private void checkBox_caputre_CheckedChanged_1(object sender, EventArgs e)
        {
            if (init)
            {
                return;
            }

            setButtonEanble(true);
        }

        private void comboBox_trigger0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (init)
            {
                return;
            }

            setButtonEanble(true);
        }

        private void comboBox_trigger1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (init)
            {
                return;
            }

            setButtonEanble(true);
        }

        private void comboBox_Type_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
