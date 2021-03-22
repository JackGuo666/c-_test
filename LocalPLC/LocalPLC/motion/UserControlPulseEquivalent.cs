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


            if (node.Parent == null)
            {
                LocalPLC.utility.PrintInfo(string.Format("{0}节点没有父节点!", node.Parent));
                return;
            }


            data = node.Parent.Tag as Axis;
            node_ = node;



            initPulseEquient();
        }
    }
}
