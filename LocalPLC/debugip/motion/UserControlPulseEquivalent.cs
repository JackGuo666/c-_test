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
    public partial class UserControlPulseEquivalent : UserControl
    {

        #region
        Axis data = null;
        TreeNode node_ = null;
        ToolTip tip = new ToolTip();
        #endregion


        #region

        void initPulseEquient()
        {
            textBox1.Text = data.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor.ToString();
            textBox2.Text = data.axisMotionPara.pulseEquivalent.offsetPerReolutionMotor.ToString();
        }
        #endregion

        public UserControlPulseEquivalent(TreeNode node)
        {
            InitializeComponent();
            textBox1.MaxLength = 10;
            textBox2.MaxLength = 10;

            button_valid.Enabled = false;
            button_cancel.Enabled = false;

            if (node.Parent == null)
            {
                LocalPLC.utility.PrintInfo(string.Format("{0}节点没有父节点!", node.Parent));
                return;
            }


            data = node.Parent.Tag as Axis;
            node_ = node;



            initPulseEquient();

            tip.AutoPopDelay = 5000;
            tip.InitialDelay = 500;
            tip.ReshowDelay = 500;

            tip.ShowAlways = true;
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

        void setButtonEnable(bool enable)
        {
            button_valid.Enabled = enable;
            button_cancel.Enabled = enable;
        }

        //System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[1-9]\d*$");
        System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[1-9]([0-9]*)$|^[0-9]$");
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            

            if (data.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor.ToString()
                != (sender as TextBox).Text)
            {
                //setButtonEnable(true);

                //System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[1-9]\d*$");
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
                        textBox1.Text = 4294967295.ToString();
                    }
                    else if(Int64.Parse(str) < 0)
                    {
                        textBox1.Text = 0.ToString();
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
                (sender as TextBox).BackColor = Color.White;
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //模板
            if (textBox2.Text != data.axisMotionPara.pulseEquivalent.offsetPerReolutionMotor.ToString())
            {
               //setButtonEnable(true);


                string str = (sender as TextBox).Text;
                //System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[1-9]\d*$");

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
                        textBox2.Text = 4294967295.ToString();
                    }
                    else if(Int64.Parse(str) < 0)
                    {
                        textBox2.Text = 0.ToString();
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        void setDataFromUI()
        {
            UInt32.TryParse(textBox1.Text, out data.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor);
            UInt32.TryParse(textBox2.Text, out data.axisMotionPara.pulseEquivalent.offsetPerReolutionMotor);
        }

        private void button_valid_Click(object sender, EventArgs e)
        {
            setDataFromUI();
            setButtonEnable(false);
        }


        void refreshData()
        {
            //电机每转脉冲数
            var pulseEquivalent = data.axisMotionPara.pulseEquivalent;
            textBox1.Text = pulseEquivalent.pulsePerRevolutionMotor.ToString();
            //
            textBox2.Text = pulseEquivalent.offsetPerReolutionMotor.ToString();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            refreshData();

            setButtonEnable(false);
        }
    }
}
