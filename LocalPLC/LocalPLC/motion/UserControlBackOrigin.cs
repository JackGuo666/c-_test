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
    public partial class UserControlBackOrigin : UserControl
    {
        #region
        Axis data = null;
        TreeNode node_ = null;
        Dictionary<int, string> levelDic = new Dictionary<int, string>();
        enum TypeLevel { HIGH_LEVEL, LOW_LEVEL }
        #endregion

        void initBackOriginal()
        {
            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            comboBox_BackOriginal.Items.Clear();
            comboBox_ZPulseSignal.Items.Clear();
            foreach (var di in dataManage.diList)
            {
                if (!di.used)
                {
                    comboBox_BackOriginal.Items.Add(di.channelName);
                    comboBox_ZPulseSignal.Items.Add(di.channelName);
                }
            }

            comboBox_BackOriginal.Text = data.axisMotionPara.backOriginal.orginInputSignal;
            comboBox_ZPulseSignal.Text = data.axisMotionPara.backOriginal.ZPulseSignal;

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

        public UserControlBackOrigin(TreeNode node)
        {
            InitializeComponent();


            if (node.Parent == null)
            {
                LocalPLC.utility.PrintInfo(string.Format("{0}节点没有父节点!", node.Parent));
                return;
            }

            data = node.Parent.Tag as Axis;
            node_ = node;

            initBackOriginal();

            button_valid.Enabled = false;
            button_cancel.Enabled = false;
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

        private void comboBox_BackOriginal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_BackOriginalSelectLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_ZPulseSignal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        private void button_valid_Click(object sender, EventArgs e)
        {
            setDataFromUI();
            button_valid.Enabled = false;
            button_cancel.Enabled = false;
        }

        void setDataFromUI()
        {
            //回原点
            var orginal = data.axisMotionPara.backOriginal;
            orginal.orginInputSignal = comboBox_BackOriginal.Text;
            orginal.selectLevel = comboBox_BackOriginalSelectLevel.SelectedIndex;
            orginal.ZPulseSignal = comboBox_ZPulseSignal.Text;
        }


        void refreshData()
        {
            var original = data.axisMotionPara.backOriginal;
            comboBox_BackOriginal.Text = original.orginInputSignal;
            comboBox_BackOriginalSelectLevel.SelectedIndex = original.selectLevel;
            comboBox_ZPulseSignal.Text = original.ZPulseSignal;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            refreshData();
            button_valid.Enabled = false;
            button_cancel.Enabled = false;
        }
    }
}
