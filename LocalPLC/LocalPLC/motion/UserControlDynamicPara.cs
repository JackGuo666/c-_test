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
    public partial class UserControlDynamicPara : UserControl
    {
        #region
        Axis data = null;
        TreeNode node_ = null;

        #endregion

        #region
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
        #endregion


        public UserControlDynamicPara(TreeNode node)
        {
            InitializeComponent();

            setButtonEnable(false);

            if (node.Parent == null)
            {
                LocalPLC.utility.PrintInfo(string.Format("{0}节点没有父节点!", node.Parent));
                return;
            }

            data = node.Parent.Tag as Axis;
            node_ = node;

            initDynamic();

        }

        void setDataFromUI()
        {
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
        }



        void refreshData()
        {
            var dynamic = data.axisMotionPara.dynamicPara;
            textBox_MaxSpeed.Text = dynamic.maxSpeed.ToString();
            textBox_AcceleratedSpeed.Text = dynamic.acceleratedSpeed.ToString();
            textBox_DecelerationSpeed.Text = dynamic.decelerationSpeed.ToString();
            textBox_Jerk.Text = dynamic.jerk.ToString();
            textBox_EmeStopDeceSpeed.Text = dynamic.emeStopDeceleration.ToString();
        }

        private void button_valid_Click(object sender, EventArgs e)
        {
            setDataFromUI();
            setButtonEnable(false);
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            refreshData();
            setButtonEnable(false);
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

        System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[1-9]\d*$");
        private void textBox_MaxSpeed_TextChanged(object sender, EventArgs e)
        {
            if (textBox_MaxSpeed.Text !=
                data.axisMotionPara.dynamicPara.maxSpeed.ToString())
            {
                setButtonEnable(true);

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

        private void textBox_AcceleratedSpeed_TextChanged(object sender, EventArgs e)
        {
            if (textBox_AcceleratedSpeed.Text !=
                data.axisMotionPara.dynamicPara.acceleratedSpeed.ToString())
            {

                setButtonEnable(true);
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
            if (textBox_DecelerationSpeed.Text !=
                data.axisMotionPara.dynamicPara.decelerationSpeed.ToString())
            {

                setButtonEnable(true);
                string str = (sender as TextBox).Text;
                if (!reg.IsMatch(str) || Int64.Parse(str) <= 0 || Int64.Parse(str) > 4294967295)
                {
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
                (sender as TextBox).BackColor = Color.White;
            }
        }

        private void textBox_Jerk_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Jerk.Text != data.axisMotionPara.dynamicPara.jerk.ToString())
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
            if (textBox_EmeStopDeceSpeed.Text !=
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
    }
}
