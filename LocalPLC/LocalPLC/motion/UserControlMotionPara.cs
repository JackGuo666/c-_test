using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalPLC.motion
{

    #region


    #endregion

    #region

    #endregion

    public partial class UserControlMotionPara : UserControl
    {


        #region
        Axis data = null;
        TreeNode node_ = null;
        ToolTip tip = new ToolTip();
        enum TypeLevel { HIGH_LEVEL, LOW_LEVEL}

        Dictionary<int, string> levelDic = new Dictionary<int, string>();

        //DI使用的key
        const string hardUpLimitInputKey = "harduplimitinput";
        const string hardDownLimitInputKey = "harddownlimitinput";
        #endregion



        #region

        //闪烁
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0014) // 禁掉清除背景消息
        //        return;
        //    base.WndProc(ref m);
        //}

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //这里写重绘代码

        }

        void initPulseEquient()
        {
            textBox_pulsePerRevolutionMotor.Text = data.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor.ToString();
            textBox_offsetPerReolutionMotor.Text = data.axisMotionPara.pulseEquivalent.offsetPerReolutionMotor.ToString();

            if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.MM)
            {
                label_PayloadOffset.Text = "mm";
            }
            else if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.ANGLE)
            {
                label_PayloadOffset.Text = "°";
            }
            else if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.PULSE)
            {
                label_PayloadOffset.Text = "pulse";
            }
        }

        void initLimitSignal()
        {
            //是否启动限位信号
            checkBox1.Checked = data.axisMotionPara.limitSignal.hardLimitChecked;
            //启动硬信号
            if (checkBox1.Checked)
            {
                comboBox_hardUpLimitInput.Enabled = true;
                comboBox_hardDownLimitInput.Enabled = true;
                comboBox_hardUpLimitLevel.Enabled = true;
                comboBox_hardDownLimitLevel.Enabled = true;
            }
            else
            {
                comboBox_hardUpLimitInput.Enabled = false;
                comboBox_hardDownLimitInput.Enabled = false;
                comboBox_hardUpLimitLevel.Enabled = false;
                comboBox_hardDownLimitLevel.Enabled = false;
            }

            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            comboBox_hardUpLimitInput.Items.Clear();
            comboBox_hardDownLimitInput.Items.Clear();
            foreach (var di in dataManage.diList)
            {
                //if(!di.used)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Value = di.channelName;
                    item.Text = di.channelName;
                    item.Used = di.used;
                    comboBox_hardUpLimitInput.Items.Add(item);
                    comboBox_hardDownLimitInput.Items.Add(item);
                }
            }

            comboBox_hardUpLimitInput.Text = data.axisMotionPara.limitSignal.hardUpLimitInput;
            comboBox_hardUpLimitInput.Tag = data.axisMotionPara.limitSignal.hardUpLimitInput;
            comboBox_hardDownLimitInput.Text = data.axisMotionPara.limitSignal.hardDownLimitInput;
            comboBox_hardDownLimitInput.Tag = data.axisMotionPara.limitSignal.hardDownLimitInput;

            levelDic.Clear();
            levelDic.Add((int)TypeLevel.HIGH_LEVEL, "高电平有效");
            levelDic.Add((int)TypeLevel.LOW_LEVEL, "低电平有效");
            comboBox_hardUpLimitLevel.Items.Clear();
            foreach (var level in levelDic)
            {
                comboBox_hardUpLimitLevel.Items.Add(level.Value);
                comboBox_hardDownLimitLevel.Items.Add(level.Value);
            }
            comboBox_hardUpLimitLevel.SelectedIndex = data.axisMotionPara.limitSignal.hardUpLimitInputLevel;
            comboBox_hardDownLimitLevel.SelectedIndex = data.axisMotionPara.limitSignal.hardDownLimitInputLevel;

            checkBox_softLimit.Checked = data.axisMotionPara.limitSignal.softLimitChecked;

            //启动软限位
            if (checkBox_softLimit.Checked)
            {
                textBox_softUpLimitOffset.Enabled = true;
                textBox_SoftDownLimitOffset.Enabled = true;
            }
            else
            {
                textBox_softUpLimitOffset.Enabled = false;
                textBox_SoftDownLimitOffset.Enabled = false;
            }

            textBox_softUpLimitOffset.Text = data.axisMotionPara.limitSignal.softUpLimitInputOffset.ToString();
            textBox_SoftDownLimitOffset.Text = data.axisMotionPara.limitSignal.softDownLimitOffset.ToString();

            if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.MM)
            {
                label_SoftUpLimitPos.Text = "mm";
                label_SoftDownLimitPos.Text = "mm";

            }
            else if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.ANGLE)
            {
                label_SoftUpLimitPos.Text = "°";
                label_SoftDownLimitPos.Text = "°";
            }
            else if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.PULSE)
            {
                label_SoftUpLimitPos.Text = "pulse";
                label_SoftDownLimitPos.Text = "pulse";
            }

        }

        void initDynamic()
        {
            //最大速度
            textBox_MaxSpeed.Text = data.axisMotionPara.dynamicPara.maxSpeed.ToString();
            //加速度
            textBox_AcceleratedSpeed.Text = data.axisMotionPara.dynamicPara.acceleratedSpeed.ToString();
            //减速度
            textBox_DecelerationSpeed.Text = data.axisMotionPara.dynamicPara.decelerationSpeed.ToString();
            //跃度
            textBox_Jerk.Text = data.axisMotionPara.dynamicPara.jerk.ToString();
            //急停减速度
            textBox_EmeStopDeceSpeed.Text = data.axisMotionPara.dynamicPara.emeStopDeceleration.ToString();

            if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.MM)
            {
                label_MaxSpeed.Text = "mm";
                label_MaxAccSpeed.Text = "mm";
                label_MaxDecSpeed.Text = "mm";
                label_Jerk.Text = "mm";
                label_EmergyDecSpeed.Text = "mm";
            }
            else if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.ANGLE)
            {
                label_MaxSpeed.Text = "°";
                label_MaxAccSpeed.Text = "°";
                label_MaxDecSpeed.Text = "°";
                label_Jerk.Text = "°";
                label_EmergyDecSpeed.Text = "°";
            }
            else if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.PULSE)
            {
                label_MaxSpeed.Text = "pulse";
                label_MaxAccSpeed.Text = "pulse";
                label_MaxDecSpeed.Text = "pulse";
                label_Jerk.Text = "pulse";
                label_EmergyDecSpeed.Text = "pulse";
            }
        }

        void initBackOriginal()
        {
            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            comboBox_BackOriginal.Items.Clear();
            comboBox_ZPulseSignal.Items.Clear();
            foreach (var di in dataManage.diList)
            {
                //if (!di.used)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Value = di.channelName;
                    item.Text = di.channelName;
                    item.Used = di.used;
                    comboBox_BackOriginal.Items.Add(item);
                    comboBox_ZPulseSignal.Items.Add(item);
                }
            }

            comboBox_BackOriginal.Text = data.axisMotionPara.backOriginal.orginInputSignal;
            //保存上一次值
            comboBox_BackOriginal.Tag = data.axisMotionPara.backOriginal.orginInputSignal;
            comboBox_ZPulseSignal.Text = data.axisMotionPara.backOriginal.ZPulseSignal;
            //保存上一次值
            comboBox_ZPulseSignal.Tag = data.axisMotionPara.backOriginal.ZPulseSignal;

            levelDic.Clear();
            levelDic.Add((int)TypeLevel.HIGH_LEVEL, "高电平有效");
            levelDic.Add((int)TypeLevel.LOW_LEVEL, "低电平有效");
            comboBox_BackOriginalSelectLevel.Items.Clear();
            foreach (var level in levelDic)
            {
                comboBox_BackOriginalSelectLevel.Items.Add(level.Value);
            }

            comboBox_BackOriginalSelectLevel.SelectedIndex = data.axisMotionPara.backOriginal.selectLevel;
        }


        void initReverseCompensation()
        {
            textBox_ReverseCompensation.Text = data.axisMotionPara.reverseCompensation.reverseCompensation.ToString();


            if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.MM)
            {
                label_ReverseCompesation.Text = "mm";
            }
            else if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.ANGLE)
            {
                label_ReverseCompesation.Text = "°";
            }
            else if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.PULSE)
            {
                label_ReverseCompesation.Text = "pulse";
            }
        }
        #endregion

        public UserControlMotionPara()
        {
            InitializeComponent();

            SetStyle(
                     ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.ResizeRedraw
                     | ControlStyles.Selectable
                     | ControlStyles.AllPaintingInWmPaint
                     | ControlStyles.UserPaint
                     | ControlStyles.SupportsTransparentBackColor,
                     true);
            this.DoubleBuffered = true;

            tip.AutoPopDelay = 5000;
            tip.InitialDelay = 500;
            tip.ReshowDelay = 500;

            tip.ShowAlways = true;
            setButtonEnable(false);
        }

        public void initData(TreeNode node)
        {
            if (node.Parent == null)
            {
                LocalPLC.utility.PrintInfo(string.Format("{0}节点没有父节点!", node.Parent));
                return;
            }

            data = node.Parent.Tag as Axis;
            node_ = node;

            initPulseEquient();
            init = true;
            initLimitSignal();
            init = false;
            initDynamic();
            initBackOriginal();
            initReverseCompensation();

            button_valid.Enabled = false;
            button_cancel.Enabled = false;
        }

        
        void refreshData()
        {
            //电机每转脉冲数
            var pulseEquivalent = data.axisMotionPara.pulseEquivalent;
            textBox_pulsePerRevolutionMotor.Text = pulseEquivalent.pulsePerRevolutionMotor.ToString();
            //
            textBox_offsetPerReolutionMotor.Text = pulseEquivalent.offsetPerReolutionMotor.ToString();


            var limitSignal = data.axisMotionPara.limitSignal;
            //启动硬限位
            checkBox1.Checked = limitSignal.hardLimitChecked;
            //硬件上限位
            comboBox_hardUpLimitInput.Text = limitSignal.hardUpLimitInput;
            comboBox_hardUpLimitLevel.SelectedIndex = limitSignal.hardUpLimitInputLevel;
            //硬件下限位
            comboBox_hardDownLimitInput.Text = limitSignal.hardDownLimitInput;
            comboBox_hardDownLimitLevel.SelectedIndex = limitSignal.hardDownLimitInputLevel;



            checkBox_softLimit.Checked = limitSignal.softLimitChecked;
            textBox_softUpLimitOffset.Text = limitSignal.softUpLimitInputOffset.ToString();
            textBox_SoftDownLimitOffset.Text = limitSignal.softDownLimitOffset.ToString();




            var dynamic = data.axisMotionPara.dynamicPara;
            textBox_MaxSpeed.Text = dynamic.maxSpeed.ToString();
            textBox_AcceleratedSpeed.Text = dynamic.acceleratedSpeed.ToString();
            textBox_DecelerationSpeed.Text = dynamic.decelerationSpeed.ToString();
            textBox_Jerk.Text = dynamic.jerk.ToString();
            textBox_EmeStopDeceSpeed.Text = dynamic.emeStopDeceleration.ToString();

            var original = data.axisMotionPara.backOriginal;
            comboBox_BackOriginal.Text = original.orginInputSignal;
            comboBox_BackOriginalSelectLevel.SelectedIndex = original.selectLevel;
            comboBox_ZPulseSignal.Text = original.ZPulseSignal;

            var reverseCompensation = data.axisMotionPara.reverseCompensation;
            textBox_ReverseCompensation.Text = reverseCompensation.reverseCompensation.ToString();
        }
        //public UserControlMotionPara(TreeNode node)
        //{
        //    InitializeComponent();

        //    if (node.Parent == null)
        //    {
        //        LocalPLC.utility.PrintInfo(string.Format("{0}节点没有父节点!", node.Parent));
        //        return;
        //    }

        //    data = node.Parent.Tag as Axis;
        //    node_ = node;

        //    initPulseEquient();
        //    initLimitSignal();
        //    initDynamic();
        //    initBackOriginal();
        //    reverseCompensation();
        //}



        void setButtonEnable(bool enable)
        {
            button_valid.Enabled = enable;
            button_cancel.Enabled = enable;
        }

        void setDataFromUI()
        {
            UInt32.TryParse(textBox_pulsePerRevolutionMotor.Text, out data.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor);
            UInt32.TryParse(textBox_offsetPerReolutionMotor.Text, out data.axisMotionPara.pulseEquivalent.offsetPerReolutionMotor);


            data.axisMotionPara.limitSignal.hardLimitChecked = checkBox1.Checked;
            data.axisMotionPara.limitSignal.hardUpLimitInput = comboBox_hardUpLimitInput.Text;



            int.TryParse(comboBox_hardUpLimitLevel.Text, out data.axisMotionPara.limitSignal.hardUpLimitInputLevel);
            data.axisMotionPara.limitSignal.hardDownLimitInput = comboBox_hardDownLimitInput.Text;
            int.TryParse(comboBox_hardDownLimitLevel.Text, out data.axisMotionPara.limitSignal.hardDownLimitInputLevel);


            //启动硬限位
            data.axisMotionPara.limitSignal.softLimitChecked = checkBox_softLimit.Checked;
            //硬件上限位输入点
            data.axisMotionPara.limitSignal.hardUpLimitInput = comboBox_hardUpLimitInput.Text;
            data.axisMotionPara.limitSignal.hardUpLimitInputLevel = comboBox_hardUpLimitLevel.SelectedIndex;
            //硬件下限位输入点
            data.axisMotionPara.limitSignal.hardDownLimitInput = comboBox_hardDownLimitInput.Text;
            data.axisMotionPara.limitSignal.hardDownLimitInputLevel = comboBox_hardDownLimitLevel.SelectedIndex;


            //启动软限位
            data.axisMotionPara.limitSignal.softLimitChecked = checkBox_softLimit.Checked;
            int.TryParse(textBox_softUpLimitOffset.Text, out data.axisMotionPara.limitSignal.softUpLimitInputOffset);
            int.TryParse(textBox_SoftDownLimitOffset.Text, out data.axisMotionPara.limitSignal.softDownLimitOffset);



            //动态参数
            var dynamic = data.axisMotionPara.dynamicPara;
            //最大速度
            UInt32.TryParse(textBox_MaxSpeed.Text, out dynamic.maxSpeed);
            //加速度
            UInt32.TryParse(textBox_AcceleratedSpeed.Text, out dynamic.acceleratedSpeed);
            //减速度
            UInt32.TryParse(textBox_DecelerationSpeed.Text, out dynamic.decelerationSpeed);
            //Jerk
            UInt32.TryParse(textBox_Jerk.Text, out dynamic.jerk);
            //最大速度
            UInt32.TryParse(textBox_EmeStopDeceSpeed.Text, out dynamic.emeStopDeceleration);

            //回原点
            var orginal = data.axisMotionPara.backOriginal;
            orginal.orginInputSignal = comboBox_BackOriginal.Text;
            orginal.selectLevel = comboBox_BackOriginalSelectLevel.SelectedIndex;
            orginal.ZPulseSignal = comboBox_ZPulseSignal.Text;

            //反向间隙补偿
            var reverse = data.axisMotionPara.reverseCompensation;
            UInt32.TryParse(textBox_ReverseCompensation.Text, out reverse.reverseCompensation);
        }

        private void button_valid_Click(object sender, EventArgs e)
        {
            setDataFromUI();
            setEnableButton(false, button_valid);
            setEnableButton(false, button_cancel);



            UserControl1.UC.refreshDIUserBaseUI();
        }

        private void textBox_pulsePerRevolutionMotor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        void setValidButtonRed(TextBox text)
        {
            button_valid.Enabled = false;
            text.BackColor = Color.Red;
        }

        void setValidButtonWhite(TextBox text)
        {
            button_valid.Enabled = true;
            text.BackColor = Color.White;
        }

        int upLimitValue = 100000;
        //^[1-9] ([0 - 9]*)$|^[0-9]$
        //System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[0-9]\d*$");
        System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[1-9]([0-9]*)$|^[0-9]$");
        System.Text.RegularExpressions.Regex regZF = new System.Text.RegularExpressions.Regex(@"^(-|\+)?\d+$");  //^((-\d+)|(0+))|(\d+)$
        private void textBox_pulsePerRevolutionMotor_TextChanged(object sender, EventArgs e)
        {
            if (textBox_pulsePerRevolutionMotor.Text != data.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor.ToString())
            {
                //setButtonEnable(true);

                
                string str = (sender as TextBox).Text;


                if (!reg.IsMatch(str) || Int64.Parse(str) < 0 || Int64.Parse(str) > 4294967295)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor.ToString();
                    setValidButtonRed(sender as TextBox);
                    button_valid.Enabled = false;
                    button_cancel.Enabled = true;
                    if(!reg.IsMatch(str))
                    {
                        tip.SetToolTip((sender as TextBox), string.Format("{0} 格式不对", str));
                    }
                    else if(Int64.Parse(str) > 4294967295)
                    {
                        textBox_pulsePerRevolutionMotor.Text = 4294967295.ToString();
                    }
                    else if(Int64.Parse(str) < 0)
                    {
                        textBox_pulsePerRevolutionMotor.Text = 0.ToString();
                    }

                    //tip.SetToolTip((sender as TextBox), string.Format("{0} 超出值0到4294967295范围", str));
                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
                    button_cancel.Enabled = true;
                    button_valid.Enabled = true;
                    tip.SetToolTip((sender as TextBox), "");
                }
            }
            else
            {
                //setValidButtonWhite(sender as TextBox);
                (sender as TextBox).BackColor = Color.White;

            }
        }

        private void textBox_offsetPerReolutionMotor_TextChanged(object sender, EventArgs e)
        {
            if(textBox_offsetPerReolutionMotor.Text !=
                data.axisMotionPara.pulseEquivalent.offsetPerReolutionMotor.ToString())
            {
                setButtonEnable(true);
                string str = (sender as TextBox).Text;

                if (!reg.IsMatch(str) || Int64.Parse(str) <= 0 || Int64.Parse(str) > 4294967295)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.pulseEquivalent.offsetPerReolutionMotor.ToString();

                    setValidButtonRed(sender as TextBox);
                    button_valid.Enabled = false;
                    button_cancel.Enabled = true;

                    if(!reg.IsMatch(str))
                    {
                        tip.SetToolTip((sender as TextBox), string.Format("{0} 格式不对", str));
                    }
                    else if(Int64.Parse(str) > 4294967295)
                    {
                        textBox_offsetPerReolutionMotor.Text = 4294967295.ToString();
                    }
                    else if(Int64.Parse(str) <= 0)
                    {
                        textBox_offsetPerReolutionMotor.Text = 0.ToString();
                    }


                    //tip.SetToolTip((sender as TextBox), string.Format("{0} 超出值0到4294967295范围", str));
                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
                    button_cancel.Enabled = true;
                    button_valid.Enabled = true;
                    tip.SetToolTip((sender as TextBox), "");
                }
            }
            else
            {
                //setValidButtonWhite(sender as TextBox);
                (sender as TextBox).BackColor = Color.White;
            }
        }
        bool init = false;
        private void textBox_softUpLimitOffset_TextChanged(object sender, EventArgs e)
        {
            //if (textBox_softUpLimitOffset.Text !=
                //data.axisMotionPara.limitSignal.softUpLimitInputOffset.ToString())
            {


                if(init)
                {
                    return;
                }
                //setButtonEnable(true);
                string str = (sender as TextBox).Text;
                bool flag = regZF.IsMatch(str);

                //btest = Int64.Parse(str) < -2147483648;
                //btest = Int64.Parse(str) > 2147483647;

                if (!regZF.IsMatch(str) || Int64.Parse(str) < -2147483648 || Int64.Parse(str) > 2147483647)
                {
                    setValidButtonRed(sender as TextBox);
                    button_valid.Enabled = false;
                    button_cancel.Enabled = true;
                    if (!regZF.IsMatch(str)
                       /* || System.Text.RegularExpressions.Regex.Matches(str, "-").Count > 1*/)
                    {
                        tip.SetToolTip((sender as TextBox), string.Format("{0} 格式不对", str));
                        //MessageBox.Show((sender as TextBox), string.Format("{0} 格式不对", str));
                    }
                    else if(Int64.Parse(str) < -2147483648)
                    {
                        textBox_softUpLimitOffset.Text = (-2147483648).ToString();
                    }
                    else if(Int64.Parse(str) > 2147483647)
                    {
                        textBox_softUpLimitOffset.Text = (2147483647).ToString();
                    }

                    //(sender as TextBox).Text = data.axisMotionPara.limitSignal.softUpLimitInputOffset.ToString();
                   
                    //tip.SetToolTip((sender as TextBox), string.Format("{0} 超出值-2147483648到2147483647范围", str));


                    return;
                }
                else
                {
                    var tempUp = textBox_softUpLimitOffset.Text;
                    var tempDown = textBox_SoftDownLimitOffset.Text;
                    int upLimit;
                    int downLimit;
                    int.TryParse(tempUp, out upLimit);
                    int.TryParse(tempDown, out downLimit);
                    if(downLimit >= upLimit)
                    {
                        setValidButtonRed(sender as TextBox);
                        //MessageBox.Show("上限值要大于下限值");
                        button_valid.Enabled = false;
                        button_cancel.Enabled = true;
                        tip.SetToolTip((sender as TextBox), string.Format("{0} 上限值要大于下限值 {1}", str, tempDown));
                    }
                    else
                    {
                        setValidButtonWhite(sender as TextBox);
                        textBox_SoftDownLimitOffset.BackColor = Color.White;
                        button_cancel.Enabled = true;
                        button_valid.Enabled = true;
                        tip.SetToolTip((sender as TextBox), "");
                        tip.SetToolTip(textBox_SoftDownLimitOffset, "");
                    }
                }
            }
            //else
            //{
            //    //setValidButtonWhite(sender as TextBox);
            //    (sender as TextBox).BackColor = Color.White;
            //}
        }

        private void textBox_SoftDownLimitOffset_TextChanged(object sender, EventArgs e)
        {
            //if(textBox_SoftDownLimitOffset.Text != 
               // data.axisMotionPara.limitSignal.softDownLimitOffset.ToString())
            {
                if (init)
                {
                    return;
                }
                //setButtonEnable(true);
                string str = (sender as TextBox).Text;

                if (!regZF.IsMatch(str) || Int64.Parse(str) < -2147483648 || Int64.Parse(str) > 2147483647)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.limitSignal.softDownLimitOffset.ToString();
                    setValidButtonRed(sender as TextBox);
                    button_valid.Enabled = false;
                    button_cancel.Enabled = true;
                    if (!regZF.IsMatch(str))
                    {
                        tip.SetToolTip((sender as TextBox), string.Format("{0} 格式不对", str));
                        //MessageBox.Show((sender as TextBox), string.Format("{0} 格式不对", str));
                    }
                    else if (Int64.Parse(str) < -2147483648)
                    {
                        textBox_SoftDownLimitOffset.Text = (-2147483648).ToString();
                    }
                    else if (Int64.Parse(str) > 2147483647)
                    {
                        textBox_SoftDownLimitOffset.Text = (2147483647).ToString();
                    }

                    return;
                }
                else
                {
                    var tempUp = textBox_softUpLimitOffset.Text;
                    var tempDown = textBox_SoftDownLimitOffset.Text;
                    int upLimit;
                    int downLimit;
                    int.TryParse(tempUp, out upLimit);
                    int.TryParse(tempDown, out downLimit);
                    if (downLimit >= upLimit)
                    {
                        setValidButtonRed(sender as TextBox);
                        //MessageBox.Show("上限值要大于下限值");
                        button_valid.Enabled = false;
                        button_cancel.Enabled = true;
                        tip.SetToolTip((sender as TextBox), string.Format("{0} 下限值要小于上限值 {1}", str, upLimit));
                    }
                    else
                    {
                        setValidButtonWhite(sender as TextBox);
                        textBox_softUpLimitOffset.BackColor = Color.White;
                        button_cancel.Enabled = true;
                        button_valid.Enabled = true;
                        tip.SetToolTip((sender as TextBox), "");
                        tip.SetToolTip(textBox_softUpLimitOffset, "");
                    }
                }

            }
            //else
            //{
            //    //setValidButtonWhite(sender as TextBox);
            //    (sender as TextBox).BackColor = Color.White;
            //}
        }

        private void textBox_MaxSpeed_TextChanged(object sender, EventArgs e)
        {
            if (textBox_MaxSpeed.Text != 
                data.axisMotionPara.dynamicPara.maxSpeed.ToString())
            {
                string str = (sender as TextBox).Text;
                if(!reg.IsMatch(str) || Int64.Parse(str) < 0 || Int64.Parse(str) > 4294967295)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.dynamicPara.maxSpeed.ToString();
                    setValidButtonRed(sender as TextBox);
                    button_valid.Enabled = false;
                    button_cancel.Enabled = true;
                    if(!reg.IsMatch(str))
                    {
                        tip.SetToolTip((sender as TextBox), string.Format("{0} 格式不对", str));
                    }
                    else if(Int64.Parse(str) > 4294967295)
                    {
                        textBox_MaxSpeed.Text = 4294967295.ToString();
                    }
                    else if(Int64.Parse(str) < 0)
                    {
                        textBox_MaxSpeed.Text = 0.ToString();
                    }
                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
                    button_cancel.Enabled = true;
                    button_valid.Enabled = true;
                    tip.SetToolTip((sender as TextBox), "");
                }
            }
            else
            {
                //setValidButtonWhite(sender as TextBox);
                (sender as TextBox).BackColor = Color.White;
            }
        }


        private void textBox_AcceleratedSpeed_TextChanged(object sender, EventArgs e)
        {
            if (textBox_AcceleratedSpeed.Text !=
                data.axisMotionPara.dynamicPara.acceleratedSpeed.ToString())
            {
                string str = (sender as TextBox).Text;

                if (!reg.IsMatch(str) || Int64.Parse(str) < 0 || Int64.Parse(str) > 4294967295)
                {
                    setValidButtonRed(sender as TextBox);
                    button_valid.Enabled = false;
                    button_cancel.Enabled = true;

                    if (!reg.IsMatch(str))
                    {
                        tip.SetToolTip((sender as TextBox), string.Format("{0} 格式不对", str));
                    }
                    else if(Int64.Parse(str) > 4294967295)
                    {
                        textBox_AcceleratedSpeed.Text = 4294967295.ToString();
                    }
                    else if(Int64.Parse(str) < 0)
                    {
                        textBox_AcceleratedSpeed.Text = 0.ToString();
                    }

                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
                    button_cancel.Enabled = true;
                    button_valid.Enabled = true;
                    tip.SetToolTip((sender as TextBox), "");
                }
            }
            else
            {
                //setValidButtonWhite(sender as TextBox);
                (sender as TextBox).BackColor = Color.White;
            }
        }

        private void textBox_DecelerationSpeed_TextChanged(object sender, EventArgs e)
        {
            if(textBox_DecelerationSpeed.Text !=
                data.axisMotionPara.dynamicPara.decelerationSpeed.ToString())
            {

                //setButtonEnable(true);
                string str = (sender as TextBox).Text;
                if(!reg.IsMatch(str) || Int64.Parse(str) < 0 || Int64.Parse(str) > 4294967295)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.dynamicPara.decelerationSpeed.ToString();
                    setValidButtonRed(sender as TextBox);
                    button_valid.Enabled = false;
                    button_cancel.Enabled = true;
                    if(!reg.IsMatch(str))
                    {
                        tip.SetToolTip((sender as TextBox), string.Format("{0} 格式不对", str));
                    }
                    else if(Int64.Parse(str) > 4294967295)
                    {
                        textBox_DecelerationSpeed.Text = 4294967295.ToString();
                    }
                    else if(Int64.Parse(str) < 0)
                    {
                        textBox_DecelerationSpeed.Text = 0.ToString();
                    }

                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
                    button_cancel.Enabled = true;
                    button_valid.Enabled = true;
                    tip.SetToolTip((sender as TextBox), "");
                }
            }
            else
            {
                //setValidButtonWhite(sender as TextBox);
                (sender as TextBox).BackColor = Color.White;
            }
        }

        private void textBox_Jerk_TextChanged(object sender, EventArgs e)
        {
            if(textBox_Jerk.Text != data.axisMotionPara.dynamicPara.jerk.ToString())
            {
                //setButtonEnable(true);
                string str = (sender as TextBox).Text;
                if (!reg.IsMatch(str) || Int64.Parse(str) < 0 || Int64.Parse(str) > 4294967295)
                {
                    setValidButtonRed(sender as TextBox);
                    button_valid.Enabled = false;
                    button_cancel.Enabled = true;
                    if(!reg.IsMatch(str))
                    {
                        tip.SetToolTip((sender as TextBox), string.Format("{0} 格式不对", str));
                    }
                    else if(Int64.Parse(str) > 4294967295)
                    {
                        textBox_Jerk.Text = 4294967295.ToString();
                    }
                    else if(Int64.Parse(str) < 0)
                    {
                        textBox_Jerk.Text = 0.ToString();
                    }


                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
                    button_cancel.Enabled = true;
                    button_valid.Enabled = true;
                    tip.SetToolTip((sender as TextBox), "");
                }
            }
            else
            {
                //setValidButtonWhite(sender as TextBox);
                (sender as TextBox).BackColor = Color.White;
            }

            

        }


        private void textBox_EmeStopDeceSpeed_TextChanged(object sender, EventArgs e)
        {

            if(textBox_EmeStopDeceSpeed.Text !=
                data.axisMotionPara.dynamicPara.emeStopDeceleration.ToString())
            {
                //setButtonEnable(true);
                string str = (sender as TextBox).Text;
                if (!reg.IsMatch(str) || Int64.Parse(str) < 0 || Int64.Parse(str) > 4294967295)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.dynamicPara.emeStopDeceleration.ToString();
                    setValidButtonRed(sender as TextBox);
                    button_valid.Enabled = false;
                    button_cancel.Enabled = true;

                    if(!reg.IsMatch(str))
                    {
                        tip.SetToolTip((sender as TextBox), string.Format("{0} 格式不对", str));
                    }
                    else if(Int64.Parse(str) > 4294967295)
                    {
                        textBox_EmeStopDeceSpeed.Text = 4294967295.ToString();
                    }
                    else if(Int64.Parse(str) < 0)
                    {
                        textBox_EmeStopDeceSpeed.Text = 0.ToString();
                    }

                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
                    button_cancel.Enabled = true;
                    button_valid.Enabled = true;
                    tip.SetToolTip((sender as TextBox), "");
                }
            }
            else
            {
                //setValidButtonWhite(sender as TextBox);
                (sender as TextBox).BackColor = Color.White;
            }

        }

        private void textBox_ReverseCompensation_TextChanged(object sender, EventArgs e)
        {
            if(textBox_ReverseCompensation.Text != 
                data.axisMotionPara.reverseCompensation.reverseCompensation.ToString())
            {

                //setButtonEnable(true);
                string str = (sender as TextBox).Text;
                if (!reg.IsMatch(str) || Int64.Parse(str) < 0 || Int64.Parse(str) > 4294967295)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.reverseCompensation.reverseCompensation.ToString();
                    setValidButtonRed(sender as TextBox);
                    button_valid.Enabled = false;
                    button_cancel.Enabled = true;
                    if(!reg.IsMatch(str))
                    {
                        tip.SetToolTip((sender as TextBox), string.Format("{0} 格式不对", str));
                    }
                    else if(Int64.Parse(str) > 4294967295)
                    {
                        textBox_ReverseCompensation.Text = 4294967295.ToString();
                    }
                    else if(Int64.Parse(str) < 0)
                    {
                        textBox_ReverseCompensation.Text = 0.ToString();
                    }

                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
                    button_cancel.Enabled = true;
                    button_valid.Enabled = true;
                    tip.SetToolTip((sender as TextBox), "");
                }
            }
            else
            {
                //setValidButtonWhite(sender as TextBox);
                (sender as TextBox).BackColor = Color.White;
            }

        }

        private void comboBox_hardUpLimitInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox_SoftUpLimitOffset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void textBox_SoftDownLimitOffset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        void setEnableButton(bool enable, Button btn)
        {
            if (enable)
            {
                btn.Enabled = enable;
                //btn.BackColor = Color.DarkOliveGreen;
            }
            else
            {
                btn.Enabled = enable;
                //btn.BackColor = Color.White;
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            refreshData();
            setEnableButton(false, button_valid);
            setEnableButton(false, button_cancel);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //启动硬信号
            if(checkBox1.Checked)
            {
                comboBox_hardUpLimitInput.Enabled = true;
                comboBox_hardDownLimitInput.Enabled = true;
                comboBox_hardUpLimitLevel.Enabled = true;
                comboBox_hardDownLimitLevel.Enabled = true;
            }
            else
            {
                comboBox_hardUpLimitInput.Enabled = false;
                comboBox_hardDownLimitInput.Enabled = false;
                comboBox_hardUpLimitLevel.Enabled = false;
                comboBox_hardDownLimitLevel.Enabled = false;
            }

            setButtonEnable(true);

        }

        private void checkBox_softLimit_CheckedChanged(object sender, EventArgs e)
        {
            //启动软限位
            if(checkBox_softLimit.Checked)
            {
                textBox_softUpLimitOffset.Enabled = true;
                textBox_SoftDownLimitOffset.Enabled = true;
            }
            else
            {
                textBox_softUpLimitOffset.Enabled = false;
                textBox_SoftDownLimitOffset.Enabled = false;
            }


            setButtonEnable(true);
        }

        private void textBox_AcceleratedSpeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void comboBox_hardUpLimitLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_valid.Enabled = true;
            button_cancel.Enabled = true;
        }

        private void comboBox_hardDownLimitLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_valid.Enabled = true;
            button_cancel.Enabled = true;
        }

        void addItemCombo(ComboBox item)
        {
            item.Items.Clear();
            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            foreach (var di in dataManage.diList)
            {
                if(!di.used || di.channelName == item.Text)
                {
                    item.Items.Add(di.channelName);
                }
            }
        }

        bool IsItemDisabled(string port, string tag, string key)
        {
            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);

            bool ret = false;
            LocalPLC.Base.xml.DIData diTemp = null;
            foreach (var di in dataManage.diList)
            {
                if(di.channelName == port)
                {
                    //被高速输出占用
                    if (di.hscUsed != "")
                    {
                        ret = true;
                    }

                    if(di.motionUsed != "")
                    {
                        ret = true;                                                                                                                                                                             
                    }
                }

                //if(tag == di.channelName && di.hscUsed == "")
                //{
                //    //pre选择di端口(motion占用)，需要清空
                //    di.motionUsed = "";
                //    di.used = false;
                //}
            }

            return ret;
        }

        void setUsedInMotion(string port, string tag, string key, ComboboxItem curItem, ComboboxItem preItem)
        {
            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            foreach (var di in dataManage.diList)
            {
                if (di.channelName == port)
                {
                    //新设置端口
                    di.motionUsed = key;
                    di.note = di.motionUsed;
                    di.used = true;
                    if(di.hscUsed != "")
                    {

                    }
                    curItem.Used = true;
                }

                if(di.channelName == tag)
                {
                    di.motionUsed = "";
                    di.note = di.motionUsed;
                    di.used = false;
                    if(preItem != null)
                    {
                        preItem.Used = false;
                    }

                }
            }
        }

        private void comboBox_hardUpLimitInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_hardUpLimitInput.SelectedIndex < 0)
            {
                return;
            }

            if (comboBox_hardUpLimitInput.Tag.ToString() == comboBox_hardUpLimitInput.Text)
            {
                return;
            }

            var used = IsItemDisabled(comboBox_hardUpLimitInput.Text, comboBox_hardUpLimitInput.Tag.ToString(),
                hardUpLimitInputKey);

            if (used)
            {
                ComboboxItem temp = null;
                foreach (var item in comboBox_hardUpLimitInput.Items)
                {
                    if ((item as ComboboxItem).Text == comboBox_hardUpLimitInput.Tag.ToString())
                    {
                        temp = item as ComboboxItem;
                    }
                }

                comboBox_hardUpLimitInput.SelectedIndex = -1;
                //清除上一次DI端口状态
                setUsedInMotion("", comboBox_hardUpLimitInput.Tag.ToString(), hardUpLimitInputKey
    , comboBox_hardUpLimitInput.SelectedItem as ComboboxItem, temp);
                comboBox_hardUpLimitInput.Tag = "";
            }
            else
            {
                ComboboxItem temp = null;
                foreach(var item in comboBox_hardUpLimitInput.Items)
                {
                    if((item as ComboboxItem).Text == comboBox_hardUpLimitInput.Tag.ToString())
                    {
                        temp = item as ComboboxItem;
                    }
                }

                setUsedInMotion(comboBox_hardUpLimitInput.Text, comboBox_hardUpLimitInput.Tag.ToString(), "harduplimitinput"
                    , comboBox_hardUpLimitInput.SelectedItem as ComboboxItem, temp);
                comboBox_hardUpLimitInput.Tag = comboBox_hardUpLimitInput.Text;
                //

                //数据刷新到DI DO datarow里,动态更新
                UserControl1.UC.refreshDIUserBaseUI();
            }

        }
        private void comboBox_hardUpLimitInput_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);

            var used = dataManage.diList[e.Index].used;
            var item = comboBox_hardUpLimitInput.Items[e.Index] as ComboboxItem;
            //本体不判断
            if (item.Used && item.Text != comboBox_hardUpLimitInput.Text)
            {
                e.Graphics.DrawString(comboBox_hardUpLimitInput.Items[e.Index].ToString(), ComboBox.DefaultFont, Brushes.LightGray, e.Bounds);
            }
            else
            {
                e.DrawBackground();
                e.Graphics.DrawString(comboBox_hardUpLimitInput.Items[e.Index].ToString(), ComboBox.DefaultFont, Brushes.Black, e.Bounds);
                e.DrawFocusRectangle();
            }
        }
        private void comboBox_hardDownLimitInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_hardDownLimitInput.SelectedIndex < 0)
            {
                return;
            }

            if (comboBox_hardDownLimitInput.Tag.ToString() == comboBox_hardDownLimitInput.Text)
            {
                return;
            }

            var used = IsItemDisabled(comboBox_hardDownLimitInput.Text, comboBox_hardDownLimitInput.Tag.ToString(),
                hardDownLimitInputKey);

            if (used)
            {
                ComboboxItem temp = null;
                foreach (var item in comboBox_hardDownLimitInput.Items)
                {
                    if ((item as ComboboxItem).Text == comboBox_hardDownLimitInput.Tag.ToString())
                    {
                        temp = item as ComboboxItem;
                    }
                }

                comboBox_hardDownLimitInput.SelectedIndex = -1;
                //清除上一次DI端口状态
                setUsedInMotion("", comboBox_hardDownLimitInput.Tag.ToString(), hardDownLimitInputKey
    , comboBox_hardDownLimitInput.SelectedItem as ComboboxItem, temp);
                comboBox_hardDownLimitInput.Tag = "";
            }
            else
            {
                ComboboxItem temp = null;
                foreach (var item in comboBox_hardDownLimitInput.Items)
                {
                    if ((item as ComboboxItem).Text == comboBox_hardDownLimitInput.Tag.ToString())
                    {
                        temp = item as ComboboxItem;
                    }
                }

                setUsedInMotion(comboBox_hardDownLimitInput.Text, comboBox_hardDownLimitInput.Tag.ToString(), "harduplimitinput"
                    , comboBox_hardDownLimitInput.SelectedItem as ComboboxItem, temp);
                comboBox_hardDownLimitInput.Tag = comboBox_hardDownLimitInput.Text;
                //


            }

            button_valid.Enabled = true;
            button_cancel.Enabled = true;
        }
        private void comboBox_hardDownLimitInput_DrawItem(object sender, DrawItemEventArgs e)
        {
            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            if (e.Index < 0 || e.Index >= dataManage.diList.Count)
            {
                return;
            }

            var used = dataManage.diList[e.Index].used;
            var item = comboBox_hardDownLimitInput.Items[e.Index] as ComboboxItem;
            //本体不判断
            if (item.Used && item.Text != comboBox_hardDownLimitInput.Text)
            {
                e.Graphics.DrawString(comboBox_hardDownLimitInput.Items[e.Index].ToString(), ComboBox.DefaultFont, Brushes.LightGray, e.Bounds);
            }
            else
            {
                e.DrawBackground();
                e.Graphics.DrawString(comboBox_hardDownLimitInput.Items[e.Index].ToString(), ComboBox.DefaultFont, Brushes.Black, e.Bounds);
                e.DrawFocusRectangle();
            }
        }

        private void comboBox_BackOriginal_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_valid.Enabled = true;
            button_cancel.Enabled = true;
        }

        private void comboBox_BackOriginalSelectLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_valid.Enabled = true;
            button_cancel.Enabled = true;
        }

        private void comboBox_ZPulseSignal_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_valid.Enabled = true;
            button_cancel.Enabled = true;
        }

        private void UserControlMotionPara_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserControlMotionPara_Load(object sender, EventArgs e)
        {
            //解决闪烁
            //this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        }

        Font myFont = new Font("Aerial", 10, FontStyle.Underline | FontStyle.Regular);
        Font myFont2 = new Font("Aerial", 10, FontStyle.Italic | FontStyle.Strikeout);


    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public bool Used { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
}
