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
        }
    }
}
