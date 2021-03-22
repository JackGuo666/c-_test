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
            textBox7.Text = data.axisMotionPara.dynamicPara.decelerationSpeed.ToString();
            //跃度
            textBox8.Text = data.axisMotionPara.dynamicPara.jerk.ToString();
            //急停减速度
            textBox9.Text = data.axisMotionPara.dynamicPara.emeStopDeceleration.ToString();
        }
        #endregion

        public UserControlDynamicPara(TreeNode node)
        {
            InitializeComponent();

            if (node.Parent == null)
            {
                LocalPLC.utility.PrintInfo(string.Format("{0}节点没有父节点!", node.Parent));
                return;
            }

            data = node.Parent.Tag as Axis;
            node_ = node;

            initDynamic();

        }
    }
}
