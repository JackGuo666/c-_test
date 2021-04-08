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
        #endregion

        #region
        //void initLimitSignal()
        //{


        //    LocalPLC.Base.xml.DataManageBase dataManage = null;
        //    LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
        //    comboBox_hardUpLimitInput.Items.Clear();
        //    comboBox_hardDownLimitInput.Items.Clear();
        //    foreach (var di in dataManage.diList)
        //    {
        //        if (!di.used)
        //        {
        //            comboBox_hardUpLimitInput.Items.Add(di.channelName);
        //            comboBox_hardDownLimitInput.Items.Add(di.channelName);
        //        }
        //    }

        //    levelDic.Clear();
        //    levelDic.Add((int)TypeLevel.HIGH_LEVEL, "高电平有效");
        //    levelDic.Add((int)TypeLevel.LOW_LEVEL, "低电平有效");
        //    comboBox_hardUpLimitSelectLevel.Items.Clear();
        //    comboBox_hardDownLimitSelectLevel.Items.Clear();
        //    foreach (var level in levelDic)
        //    {
        //        comboBox_hardUpLimitSelectLevel.Items.Add(level.Value);
        //        comboBox_hardDownLimitSelectLevel.Items.Add(level.Value);
        //    }
        //    comboBox_hardUpLimitSelectLevel.SelectedIndex = data.axisMotionPara.limitSignal.hardUpLimitInputLevel;
        //    comboBox_hardDownLimitSelectLevel.SelectedIndex = data.axisMotionPara.limitSignal.hardDownLimitInputLevel;


        //    //是否启动硬限位信号
        //    checkBox1.Checked = data.axisMotionPara.limitSignal.hardLimitChecked;
        //    checkBox1_CheckedChanged(null, null);
        //    comboBox_hardUpLimitInput.Text = data.axisMotionPara.limitSignal.hardUpLimitInput;
        //    comboBox_hardDownLimitInput.Text = data.axisMotionPara.limitSignal.hardDownLimitInput;


        //    checkBox_softLimit.Checked = data.axisMotionPara.limitSignal.softLimitChecked;
        //    checkBox_softLimit_CheckedChanged(null, null);
        //    textBox_softUpLimitOffset.Text = data.axisMotionPara.limitSignal.softUpLimitInputOffset.ToString();
        //    textBox_softDownLimitOffset.Text = data.axisMotionPara.limitSignal.softDownLimitOffset.ToString();
        //}
        #endregion

        public UserControlLimitSignal(TreeNode node)
        {
            InitializeComponent();
            //panel2.HorizontalScroll.Visible = false;
            if (node.Parent == null)
            {
                LocalPLC.utility.PrintInfo(string.Format("{0}节点没有父节点!", node.Parent));
                return;
            }


            data = node.Parent.Tag as Axis;
            node_ = node;

            //initLimitSignal();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_softLimit_CheckedChanged(object sender, EventArgs e)
        {

        }

        //private void checkBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    if(checkBox1.Checked)
        //    {
        //        comboBox_hardUpLimitInput.Enabled = true;
        //        comboBox_hardDownLimitInput.Enabled = true;
        //        comboBox_hardUpLimitSelectLevel.Enabled = true;
        //        comboBox_hardDownLimitSelectLevel.Enabled = true;
        //    }
        //    else
        //    {
        //        comboBox_hardUpLimitInput.Enabled = false;
        //        comboBox_hardDownLimitInput.Enabled = false;
        //        comboBox_hardUpLimitSelectLevel.Enabled = false;
        //        comboBox_hardDownLimitSelectLevel.Enabled = false;
        //    }
        //}

        //private void checkBox_softLimit_CheckedChanged(object sender, EventArgs e)
        //{
        //    if(checkBox_softLimit.Checked)
        //    {
        //        textBox_softUpLimitOffset.Enabled = true;
        //        textBox_softDownLimitOffset.Enabled = true;
        //    }
        //    else
        //    {
        //        textBox_softUpLimitOffset.Enabled = false;
        //        textBox_softDownLimitOffset.Enabled = false;
        //    }
        //}
    }
}
