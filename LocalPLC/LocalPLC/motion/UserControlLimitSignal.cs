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
    public partial class UserControlLimitSignal : UserControl
    {
        #region
        Axis data = null;
        TreeNode node_ = null;
        Dictionary<int, string> levelDic = new Dictionary<int, string>();
        enum TypeLevel { HIGH_LEVEL, LOW_LEVEL }
        System.Text.RegularExpressions.Regex regZF = new System.Text.RegularExpressions.Regex(@"^(-|\+)?\d+$");  //^((-\d+)|(0+))|(\d+)$
        ToolTip tip = new ToolTip();
        #endregion

        #region
        void initLimitSignal()
        {


            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            comboBox_hardUpLimitInput.Items.Clear();
            comboBox_hardDownLimitInput.Items.Clear();
            foreach (var di in dataManage.diList)
            {
                if (!di.used)
                {
                    comboBox_hardUpLimitInput.Items.Add(di.channelName);
                    comboBox_hardDownLimitInput.Items.Add(di.channelName);
                }
            }

            levelDic.Clear();
            levelDic.Add((int)TypeLevel.HIGH_LEVEL, "高电平有效");
            levelDic.Add((int)TypeLevel.LOW_LEVEL, "低电平有效");
            comboBox_hardUpLimitSelectLevel.Items.Clear();
            comboBox_hardDownLimitSelectLevel.Items.Clear();
            foreach (var level in levelDic)
            {
                comboBox_hardUpLimitSelectLevel.Items.Add(level.Value);
                comboBox_hardDownLimitSelectLevel.Items.Add(level.Value);
            }
            comboBox_hardUpLimitSelectLevel.SelectedIndex = data.axisMotionPara.limitSignal.hardUpLimitInputLevel;
            comboBox_hardDownLimitSelectLevel.SelectedIndex = data.axisMotionPara.limitSignal.hardDownLimitInputLevel;


            //是否启动硬限位信号
            checkBox1.Checked = data.axisMotionPara.limitSignal.hardLimitChecked;
            checkBox1_CheckedChanged(null, null);
            comboBox_hardUpLimitInput.Text = data.axisMotionPara.limitSignal.hardUpLimitInput;
            comboBox_hardDownLimitInput.Text = data.axisMotionPara.limitSignal.hardDownLimitInput;


            checkBox_softLimit.Checked = data.axisMotionPara.limitSignal.softLimitChecked;
            checkBox_softLimit_CheckedChanged(null, null);
            textBox_softUpLimitOffset.Text = data.axisMotionPara.limitSignal.softUpLimitInputOffset.ToString();
            textBox_softDownLimitOffset.Text = data.axisMotionPara.limitSignal.softDownLimitOffset.ToString();


            if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.MM)
            {
                label_softuplimitpos.Text = "mm";
                label_softdownlimitpos.Text = "mm";
            }
            else if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.ANGLE)
            {
                label_softuplimitpos.Text = "°";
                label_softdownlimitpos.Text = "°";
            }
            else if (data.axisBasePara.meaUnit == (int)UserControlMotionBasePara.MEASUREUNIT.PULSE)
            {
                label_softuplimitpos.Text = "pulse";
                label_softdownlimitpos.Text = "pulse";
            }
        }
        #endregion

        bool init = false;
        public UserControlLimitSignal(TreeNode node)
        {
            InitializeComponent();
            panel2.VerticalScroll.Visible = false;
            if (node.Parent == null)
            {
                LocalPLC.utility.PrintInfo(string.Format("{0}节点没有父节点!", node.Parent));
                return;
            }


            data = node.Parent.Tag as Axis;
            node_ = node;


            init = true;
            initLimitSignal();
            init = false;

            button_valid.Enabled = false;
            button_cancel.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboBox_hardUpLimitInput.Enabled = true;
                comboBox_hardDownLimitInput.Enabled = true;
                comboBox_hardUpLimitSelectLevel.Enabled = true;
                comboBox_hardDownLimitSelectLevel.Enabled = true;
            }
            else
            {
                comboBox_hardUpLimitInput.Enabled = false;
                comboBox_hardDownLimitInput.Enabled = false;
                comboBox_hardUpLimitSelectLevel.Enabled = false;
                comboBox_hardDownLimitSelectLevel.Enabled = false;
            }

            button_valid.Enabled = true;
            button_cancel.Enabled = true;
        }

        private void checkBox_softLimit_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_softLimit.Checked)
            {
                textBox_softUpLimitOffset.Enabled = true;
                textBox_softDownLimitOffset.Enabled = true;
            }
            else
            {
                textBox_softUpLimitOffset.Enabled = false;
                textBox_softDownLimitOffset.Enabled = false;
            }

            button_valid.Enabled = true;
            button_cancel.Enabled = true;
        }

        private void comboBox_hardUpLimitInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_valid.Enabled = true;
            button_cancel.Enabled = true;
        }

        private void comboBox_hardDownLimitInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_valid.Enabled = true;
            button_cancel.Enabled = true;
        }

        private void comboBox_hardUpLimitInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_hardDownLimitInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_hardUpLimitSelectLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_hardDownLimitSelectLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }



        void setButtonEnable(bool enable)
        {
            button_valid.Enabled = enable;
            button_cancel.Enabled = enable;
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

        private void textBox_softUpLimitOffset_TextChanged(object sender, EventArgs e)
        {
            if(init)
            {
                return;
            }
            //if (textBox_softUpLimitOffset.Text !=
               // data.axisMotionPara.limitSignal.softUpLimitInputOffset.ToString())
            {
                setButtonEnable(true);
                string str = (sender as TextBox).Text;
                bool btest = regZF.IsMatch(str);
                //btest = Int64.Parse(str) < -2147483648;
                //btest = Int64.Parse(str) > 2147483647;

                if (!regZF.IsMatch(str) || Int64.Parse(str) < -2147483648 || Int64.Parse(str) > 2147483647)
                {
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
                        textBox_softUpLimitOffset.Text = (-2147483648).ToString();
                    }
                    else if (Int64.Parse(str) > 2147483647)
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
                    var tempDown = textBox_softDownLimitOffset.Text;
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
                        tip.SetToolTip((sender as TextBox), string.Format("{0} 上限值要大于下限值 {1}", str, tempDown));
                    }
                    else
                    {
                        setValidButtonWhite(sender as TextBox);
                        textBox_softDownLimitOffset.BackColor = Color.White;
                        button_cancel.Enabled = true;
                        button_valid.Enabled = true;
                        tip.SetToolTip((sender as TextBox), "");
                        tip.SetToolTip(textBox_softDownLimitOffset, "");
                    }
                }
            }
            //else
            //{
            //    //setValidButtonWhite(sender as TextBox);
            //    (sender as TextBox).BackColor = Color.White;
            //}
        }

        private void textBox_softUpLimitOffset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void textBox_softDownLimitOffset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void textBox_softDownLimitOffset_TextChanged(object sender, EventArgs e)
        {
            //if (textBox_softDownLimitOffset.Text !=
     //data.axisMotionPara.limitSignal.softDownLimitOffset.ToString())
            {
                //setButtonEnable(true);
                if (init)
                {
                    return;
                }

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
                        textBox_softDownLimitOffset.Text = (-2147483648).ToString();
                    }
                    else if (Int64.Parse(str) > 2147483647)
                    {
                        textBox_softDownLimitOffset.Text = (2147483647).ToString();
                    }

                    return;
                }
                else
                {
                    var tempUp = textBox_softUpLimitOffset.Text;
                    var tempDown = textBox_softDownLimitOffset.Text;
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

        void setDataFromUI()
        {
            //启动硬限位
            data.axisMotionPara.limitSignal.softLimitChecked = checkBox_softLimit.Checked;
            //硬件上限位输入点
            data.axisMotionPara.limitSignal.hardUpLimitInput = comboBox_hardUpLimitInput.Text;
            data.axisMotionPara.limitSignal.hardUpLimitInputLevel = comboBox_hardUpLimitSelectLevel.SelectedIndex;
            //硬件下限位输入点
            data.axisMotionPara.limitSignal.hardDownLimitInput = comboBox_hardDownLimitInput.Text;
            data.axisMotionPara.limitSignal.hardDownLimitInputLevel = comboBox_hardDownLimitSelectLevel.SelectedIndex;

            //启动软限位
            data.axisMotionPara.limitSignal.softLimitChecked = checkBox_softLimit.Checked;
            int.TryParse(textBox_softUpLimitOffset.Text, out data.axisMotionPara.limitSignal.softUpLimitInputOffset);
            int.TryParse(textBox_softDownLimitOffset.Text, out data.axisMotionPara.limitSignal.softDownLimitOffset);
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

        private void button_valid_Click(object sender, EventArgs e)
        {
            setDataFromUI();
            setEnableButton(false, button_valid);
            setEnableButton(false, button_cancel);
        }

        void refreshData()
        {
            var limitSignal = data.axisMotionPara.limitSignal;
            //启动硬限位
            checkBox1.Checked = limitSignal.hardLimitChecked;
            //硬件上限位
            comboBox_hardUpLimitInput.Text = limitSignal.hardUpLimitInput;
            comboBox_hardUpLimitSelectLevel.SelectedIndex = limitSignal.hardUpLimitInputLevel;
            //硬件下限位
            comboBox_hardDownLimitInput.Text = limitSignal.hardDownLimitInput;
            comboBox_hardDownLimitSelectLevel.SelectedIndex = limitSignal.hardDownLimitInputLevel;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            refreshData();
            setEnableButton(false, button_valid);
            setEnableButton(false, button_cancel);
        }
    }
}
