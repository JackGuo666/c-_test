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


    public partial class UserControlMotionPara : UserControl
    {


        #region
        Axis data = null;
        TreeNode node_ = null;

        enum TypeLevel { HIGH_LEVEL, LOW_LEVEL}

        Dictionary<int, string> levelDic = new Dictionary<int, string>();
        #endregion



        #region

        void initPulseEquient()
        {
            textBox_pulsePerRevolutionMotor.Text = data.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor.ToString();
            textBox_offsetPerReolutionMotor.Text = data.axisMotionPara.pulseEquivalent.offsetPerReolutionMotor.ToString();
        }

        void initLimitSignal()
        {
            //是否启动限位信号
            checkBox1.Checked = data.axisMotionPara.limitSignal.hardLimitChecked;

            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            comboBox_hardUpLimitInput.Items.Clear();
            foreach (var di in dataManage.diList)
            {
                if(!di.used)
                {
                    comboBox_hardUpLimitInput.Items.Add(di.channelName);
                    comboBox_hardDownLimitInput.Items.Add(di.channelName);
                }
            }

            levelDic.Clear();
            levelDic.Add((int)TypeLevel.HIGH_LEVEL, "高电平有效");
            levelDic.Add((int)TypeLevel.LOW_LEVEL, "低电平有效");
            comboBox_hardUpLimitLevel.Items.Clear();
            foreach (var level in levelDic)
            {
                comboBox_hardUpLimitLevel.Items.Add(level.Value);
                comboBox_hardDownLimitLevel.Items.Add(level.Value);
            }
            comboBox_hardUpLimitLevel.SelectedIndex = (int)TypeLevel.HIGH_LEVEL;
            comboBox_hardDownLimitLevel.SelectedIndex = (int)TypeLevel.HIGH_LEVEL;

            checkBox_softLimit.Checked = data.axisMotionPara.limitSignal.softLimitChecked;
            textBox_softUpLimitOffset.Text = data.axisMotionPara.limitSignal.softUpLimitInputOffset.ToString();
            textBox_SoftDownLimitOffset.Text = data.axisMotionPara.limitSignal.softDownLimitOffset.ToString();
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
        }

        void initBackOriginal()
        {
            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            foreach (var di in dataManage.diList)
            {
                if (!di.used)
                {
                    comboBox_BackOriginal.Items.Add(di.channelName);
                    comboBox_ZPulseSignal.Items.Add(di.channelName);
                }
            }

            levelDic.Clear();
            levelDic.Add((int)TypeLevel.HIGH_LEVEL, "高电平有效");
            levelDic.Add((int)TypeLevel.LOW_LEVEL, "低电平有效");
            foreach (var level in levelDic)
            {
                comboBox_BackOriginalSelectLevel.Items.Add(level.Value);
            }
        }


        void reverseCompensation()
        {
            textBox_ReverseCompensation.Text = data.axisMotionPara.reverseCompensation.reverseCompensation.ToString();
        }
        #endregion

        public UserControlMotionPara()
        {
            InitializeComponent();


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
            initLimitSignal();
            initDynamic();
            initBackOriginal();
            reverseCompensation();
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
        public UserControlMotionPara(TreeNode node)
        {
            InitializeComponent();

            if (node.Parent == null)
            {
                LocalPLC.utility.PrintInfo(string.Format("{0}节点没有父节点!", node.Parent));
                return;
            }

            data = node.Parent.Tag as Axis;
            node_ = node;

            initPulseEquient();
            initLimitSignal();
            initDynamic();
            initBackOriginal();
            reverseCompensation();
        }



        void setButtonEnable(bool enable)
        {
            button_valid.Enabled = enable;
            button_cancel.Enabled = enable;
        }

        void setDataFromUI()
        {
            int.TryParse(textBox_pulsePerRevolutionMotor.Text, out data.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor);
            int.TryParse(textBox_offsetPerReolutionMotor.Text, out data.axisMotionPara.pulseEquivalent.offsetPerReolutionMotor);


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
            data.axisMotionPara.limitSignal.hardDownLimitInputLevel = comboBox_hardUpLimitLevel.SelectedIndex;


            //启动软限位
            data.axisMotionPara.limitSignal.softLimitChecked = checkBox_softLimit.Checked;
            int.TryParse(textBox_softUpLimitOffset.Text, out data.axisMotionPara.limitSignal.softUpLimitInputOffset);
            int.TryParse(textBox_SoftDownLimitOffset.Text, out data.axisMotionPara.limitSignal.softDownLimitOffset);



            //动态参数
            var dynamic = data.axisMotionPara.dynamicPara;
            //最大速度
            int.TryParse(textBox_MaxSpeed.Text, out dynamic.maxSpeed);
            //加速度
            int.TryParse(textBox_AcceleratedSpeed.Text, out dynamic.acceleratedSpeed);
            //减速度
            int.TryParse(textBox_DecelerationSpeed.Text, out dynamic.decelerationSpeed);
            //Jerk
            int.TryParse(textBox_Jerk.Text, out dynamic.jerk);
            //最大速度
            int.TryParse(textBox_EmeStopDeceSpeed.Text, out dynamic.emeStopDeceleration);

            //回原点
            var orginal = data.axisMotionPara.backOriginal;
            orginal.orginInputSignal = comboBox_BackOriginal.Text;
            orginal.selectLevel = comboBox_BackOriginalSelectLevel.SelectedIndex;
            orginal.ZPulseSignal = comboBox_ZPulseSignal.Text;

            //反向间隙补偿
            var reverse = data.axisMotionPara.reverseCompensation;
            int.TryParse(textBox_ReverseCompensation.Text, out reverse.reverseCompensation);
        }

        private void button_valid_Click(object sender, EventArgs e)
        {
            setDataFromUI();
            setEnableButton(false, button_valid);
            setEnableButton(false, button_cancel);
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

        System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[1-9]\d*$");
        System.Text.RegularExpressions.Regex regZF = new System.Text.RegularExpressions.Regex(@"^((-\d+)|(0+))|(\d+)$");
        private void textBox_pulsePerRevolutionMotor_TextChanged(object sender, EventArgs e)
        {
            if (textBox_pulsePerRevolutionMotor.Text != data.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor.ToString())
            {
                setButtonEnable(true);

                
                string str = (sender as TextBox).Text;


                if (!reg.IsMatch(str) || Int64.Parse(str) <= 0 || Int64.Parse(str) > 4294967295)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor.ToString();
                    setValidButtonRed(sender as TextBox);
                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
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

                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
                }
            }
            else
            {
                //setValidButtonWhite(sender as TextBox);
                (sender as TextBox).BackColor = Color.White;
            }
        }

        private void textBox_HardUpLimitOffset_TextChanged(object sender, EventArgs e)
        {
            if(textBox_softUpLimitOffset.Text !=
                data.axisMotionPara.limitSignal.softUpLimitInputOffset.ToString())
            {
                setButtonEnable(true);
                string str = (sender as TextBox).Text;
                bool btest = regZF.IsMatch(str);
                //btest = Int64.Parse(str) < -2147483648;
                //btest = Int64.Parse(str) > 2147483647;
                if (!regZF.IsMatch(str) || Int64.Parse(str) < -2147483648 || Int64.Parse(str) > 2147483647)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.limitSignal.softUpLimitInputOffset.ToString();
                    setValidButtonRed(sender as TextBox);
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
                    if(downLimit > upLimit)
                    {
                        setValidButtonRed(sender as TextBox);
                        MessageBox.Show("上限值要大于下限值");
                    }
                    else
                    {
                        setValidButtonWhite(sender as TextBox);
                    }
                }
            }
            else
            {
                //setValidButtonWhite(sender as TextBox);
                (sender as TextBox).BackColor = Color.White;
            }
        }

        private void textBox_SoftDownLimitOffset_TextChanged(object sender, EventArgs e)
        {
            if(textBox_SoftDownLimitOffset.Text != 
                data.axisMotionPara.limitSignal.softDownLimitOffset.ToString())
            {
                setButtonEnable(true);
                string str = (sender as TextBox).Text;
                if(!regZF.IsMatch(str) || Int64.Parse(str) <= -2147483648 || Int64.Parse(str) > 2147483647)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.limitSignal.softDownLimitOffset.ToString();
                    setValidButtonRed(sender as TextBox);
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
                    if (downLimit > upLimit)
                    {
                        setValidButtonRed(sender as TextBox);
                        MessageBox.Show("上限值要大于下限值");
                    }
                    else
                    {
                        setValidButtonWhite(sender as TextBox);
                    }
                }

            }
            else
            {
                //setValidButtonWhite(sender as TextBox);
                (sender as TextBox).BackColor = Color.White;
            }
        }

        private void textBox_MaxSpeed_TextChanged(object sender, EventArgs e)
        {
            if (textBox_MaxSpeed.Text != 
                data.axisMotionPara.dynamicPara.maxSpeed.ToString())
            {
                string str = (sender as TextBox).Text;
                if(!reg.IsMatch(str) || Int64.Parse(str) <= 0 || Int64.Parse(str) > 4294967295)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.dynamicPara.maxSpeed.ToString();
                    setValidButtonRed(sender as TextBox);
                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
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
                if (!reg.IsMatch(str) || Int64.Parse(str) <= 0 || Int64.Parse(str) > 4294967295)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.dynamicPara.maxSpeed.ToString();
                    setValidButtonRed(sender as TextBox);
                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
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

                setButtonEnable(true);
                string str = (sender as TextBox).Text;
                if(!reg.IsMatch(str) || Int64.Parse(str) <= 0 || Int64.Parse(str) > 4294967295)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.dynamicPara.decelerationSpeed.ToString();
                    setValidButtonRed(sender as TextBox);
                    return;
                }
                else
                {

                    setValidButtonWhite(sender as TextBox);
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
                setButtonEnable(true);
                string str = (sender as TextBox).Text;
                if (!reg.IsMatch(str) || Int64.Parse(str) <= 0 || Int64.Parse(str) > 4294967295)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.dynamicPara.jerk.ToString();
                    setValidButtonRed(sender as TextBox);
                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
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

                setButtonEnable(true);
                string str = (sender as TextBox).Text;
                if (!reg.IsMatch(str) || Int64.Parse(str) <= 0 || Int64.Parse(str) > 4294967295)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.dynamicPara.emeStopDeceleration.ToString();
                    setValidButtonRed(sender as TextBox);
                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
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

                setButtonEnable(true);
                string str = (sender as TextBox).Text;
                if (!reg.IsMatch(str) || Int64.Parse(str) <= 0 || Int64.Parse(str) > 4294967295)
                {
                    //(sender as TextBox).Text = data.axisMotionPara.reverseCompensation.reverseCompensation.ToString();
                    setValidButtonRed(sender as TextBox);
                    return;
                }
                else
                {
                    setValidButtonWhite(sender as TextBox);
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

        private void textBox_HardUpLimitOffset_KeyPress(object sender, KeyPressEventArgs e)
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


    }
}
