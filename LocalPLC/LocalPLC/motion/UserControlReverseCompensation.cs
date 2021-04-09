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
    public partial class UserControlReverseCompensation : UserControl
    {
        #region
        Axis data = null;
        TreeNode node_ = null;
        ToolTip tip = new ToolTip();
        #endregion

        #region
        void reverseCompensation()
        {
            textBox_ReverseCompensation.Text = data.axisMotionPara.reverseCompensation.reverseCompensation.ToString();
        }

        #endregion

        public UserControlReverseCompensation(TreeNode node)
        {
            InitializeComponent();

            if (node.Parent == null)
            {
                LocalPLC.utility.PrintInfo(string.Format("{0}节点没有父节点!", node.Parent));
                return;
            }


            data = node.Parent.Tag as Axis;
            node_ = node;

            reverseCompensation();

            tip.AutoPopDelay = 5000;
            tip.InitialDelay = 500;
            tip.ReshowDelay = 500;

            tip.ShowAlways = true;

            button_valid.Enabled = false;
            button_cancel.Enabled = false;
        }

        System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[1-9]([0-9]*)$|^[0-9]$");

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

        private void textBox_ReverseCompensation_TextChanged(object sender, EventArgs e)
        {
            if (textBox_ReverseCompensation.Text !=
                    data.axisMotionPara.reverseCompensation.ToString())
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
                    else if (Int64.Parse(str) > 4294967295)
                    {
                        textBox_ReverseCompensation.Text = 4294967295.ToString();
                    }
                    else if (Int64.Parse(str) < 0)
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

        private void textBox_ReverseCompensation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        void setDataFromUI()
        {
            //反向间隙补偿
            var reverse = data.axisMotionPara.reverseCompensation;
            UInt32.TryParse(textBox_ReverseCompensation.Text, out reverse.reverseCompensation);
        }


        private void button_valid_Click(object sender, EventArgs e)
        {
            setDataFromUI();
            button_valid.Enabled = false;
            button_cancel.Enabled = false;
        }


        void refreshData()
        {
            var reverseCompensation = data.axisMotionPara.reverseCompensation;
            textBox_ReverseCompensation.Text = reverseCompensation.reverseCompensation.ToString();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            refreshData();
            button_valid.Enabled = false;
            button_cancel.Enabled = false;
        }
    }
}
