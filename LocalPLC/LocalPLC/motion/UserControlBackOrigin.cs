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

            ComboboxItem item = new ComboboxItem();
            item.Value = "未配置";
            item.Text = "未配置";
            item.Used = false;
            comboBox_BackOriginal.Items.Add(item);
            comboBox_ZPulseSignal.Items.Add(item);

            foreach (var di in dataManage.diList)
            {
                //if (!di.used)
                {
                    item = new ComboboxItem();
                    item.Value = di.channelName;
                    item.Text = di.channelName;
                    item.Used = di.used;

                    comboBox_BackOriginal.Items.Add(item);
                    comboBox_ZPulseSignal.Items.Add(item);
                }
            }

            //上一次值
            comboBox_BackOriginal.Tag = data.axisMotionPara.backOriginal.orginInputSignal;
            comboBox_BackOriginal.Text = data.axisMotionPara.backOriginal.orginInputSignal;
            comboBox_ZPulseSignal.Tag = data.axisMotionPara.backOriginal.ZPulseSignal;
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


        bool isItemDisabled(string port, string tag, string key)
        {
            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);

            bool ret = false;
            LocalPLC.Base.xml.DIData diTemp = null;
            foreach (var di in dataManage.diList)
            {
                if (di.channelName == port)
                {
                    //被高速输出占用
                    if (di.hscUsed != "")
                    {
                        ret = true;
                    }

                    if (di.motionUsed != "")
                    {
                        ret = true;
                    }
                }

                //if(tag == di.channelName && di.hscUsed == "")
                //{
                //    //pre选择di端口(motion占用)，需要清空
                //    di.motionUsed = "";
                //    di.used = false;
                //}
            }

            return ret;
        }

        void setUsedInMotion(string port, string tag, string key, ComboboxItem curItem, ComboboxItem preItem)
        {
            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            foreach (var di in dataManage.diList)
            {
                if (di.channelName == port)
                {
                    //新设置端口
                    di.motionUsed = key;
                    di.note = di.motionUsed;
                    di.used = true;
                    if (di.hscUsed != "")
                    {

                    }
                    curItem.Used = true;
                }

                if (di.channelName == tag)
                {
                    di.motionUsed = "";
                    di.note = di.motionUsed;
                    di.used = false;
                    if (preItem != null)
                    {
                        preItem.Used = false;
                    }

                }
            }
        }

        private void comboBox_BackOriginal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_BackOriginal.SelectedIndex < 0)
            {
                return;
            }

            if (comboBox_BackOriginal.Tag.ToString() == comboBox_BackOriginal.Text)
            {
                return;
            }

            var used = isItemDisabled(comboBox_BackOriginal.Text, comboBox_BackOriginal.Tag.ToString(),
    UserControlMotionPara.backOriginalKey);

            if (used)
            {
                ComboboxItem temp = null;
                foreach (var item in comboBox_BackOriginal.Items)
                {
                    if ((item as ComboboxItem).Text == comboBox_BackOriginal.Tag.ToString())
                    {
                        temp = item as ComboboxItem;
                    }
                }

                comboBox_BackOriginal.SelectedIndex = -1;
                //清除上一次DI端口状态
                setUsedInMotion("", comboBox_BackOriginal.Tag.ToString(), UserControlMotionPara.backOriginalKey
    , comboBox_BackOriginal.SelectedItem as ComboboxItem, temp);
                comboBox_BackOriginal.Tag = "";
            }
            else
            {
                ComboboxItem temp = null;
                foreach (var item in comboBox_BackOriginal.Items)
                {
                    if ((item as ComboboxItem).Text == comboBox_BackOriginal.Tag.ToString())
                    {
                        temp = item as ComboboxItem;
                    }
                }

                setUsedInMotion(comboBox_BackOriginal.Text, comboBox_BackOriginal.Tag.ToString(), UserControlMotionPara.backOriginalKey
                    , comboBox_BackOriginal.SelectedItem as ComboboxItem, temp);
                comboBox_BackOriginal.Tag = comboBox_BackOriginal.Text;
                //
            }


            //数据刷新到DI DO datarow里,动态更新
            UserControl1.UC.refreshDIUserBaseUI();


            button_valid.Enabled = true;
            button_cancel.Enabled = true;
        }


        private void comboBox_BackOriginal_DrawItem(object sender, DrawItemEventArgs e)
        {
            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            if (e.Index <= 0)
            {
                if (e.Index == 0)
                {
                    e.DrawBackground();
                    e.Graphics.DrawString(comboBox_BackOriginal.Items[e.Index].ToString(), ComboBox.DefaultFont, Brushes.Black, e.Bounds);
                    e.DrawFocusRectangle();
                }

                return;
            }

            var used = dataManage.diList[e.Index - 1].used;
            var item = comboBox_BackOriginal.Items[e.Index] as ComboboxItem;
            //本体不判断
            if (item.Used && item.Text != comboBox_BackOriginal.Text)
            {
                e.Graphics.DrawString(comboBox_BackOriginal.Items[e.Index].ToString(), ComboBox.DefaultFont, Brushes.LightGray, e.Bounds);
            }
            else
            {
                e.DrawBackground();
                e.Graphics.DrawString(comboBox_BackOriginal.Items[e.Index].ToString(), ComboBox.DefaultFont, Brushes.Black, e.Bounds);
                e.DrawFocusRectangle();
            }
        }

        private void comboBox_BackOriginalSelectLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_valid.Enabled = true;
            button_cancel.Enabled = true;
        }


        private void comboBox_ZPulseSignal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_ZPulseSignal.SelectedIndex < 0)
            {
                return;
            }

            if (comboBox_ZPulseSignal.Tag.ToString() == comboBox_ZPulseSignal.Text)
            {
                return;
            }

            var used = isItemDisabled(comboBox_ZPulseSignal.Text, comboBox_ZPulseSignal.Tag.ToString(),
    UserControlMotionPara.zPulseKey);

            if (used)
            {
                ComboboxItem temp = null;
                foreach (var item in comboBox_ZPulseSignal.Items)
                {
                    if ((item as ComboboxItem).Text == comboBox_ZPulseSignal.Tag.ToString())
                    {
                        temp = item as ComboboxItem;
                    }
                }

                comboBox_ZPulseSignal.SelectedIndex = -1;
                //清除上一次DI端口状态
                setUsedInMotion("", comboBox_ZPulseSignal.Tag.ToString(), UserControlMotionPara.zPulseKey
    , comboBox_ZPulseSignal.SelectedItem as ComboboxItem, temp);
                comboBox_ZPulseSignal.Tag = "";
            }
            else
            {
                ComboboxItem temp = null;
                foreach (var item in comboBox_ZPulseSignal.Items)
                {
                    if ((item as ComboboxItem).Text == comboBox_ZPulseSignal.Tag.ToString())
                    {
                        temp = item as ComboboxItem;
                    }
                }

                setUsedInMotion(comboBox_ZPulseSignal.Text, comboBox_ZPulseSignal.Tag.ToString(), UserControlMotionPara.zPulseKey
                    , comboBox_ZPulseSignal.SelectedItem as ComboboxItem, temp);
                comboBox_ZPulseSignal.Tag = comboBox_ZPulseSignal.Text;
                //
            }


            //数据刷新到DI DO datarow里,动态更新
            UserControl1.UC.refreshDIUserBaseUI();

            button_valid.Enabled = true;
            button_cancel.Enabled = true;
        }

        private void comboBox_ZPulseSignal_DrawItem(object sender, DrawItemEventArgs e)
        {
            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            if (e.Index <= 0)
            {
                if (e.Index == 0)
                {
                    e.DrawBackground();
                    e.Graphics.DrawString(comboBox_ZPulseSignal.Items[e.Index].ToString(), ComboBox.DefaultFont, Brushes.Black, e.Bounds);
                    e.DrawFocusRectangle();
                }

                return;
            }

            var used = dataManage.diList[e.Index - 1].used;
            var item = comboBox_ZPulseSignal.Items[e.Index] as ComboboxItem;
            //本体不判断
            if (item.Used && item.Text != comboBox_ZPulseSignal.Text)
            {
                e.Graphics.DrawString(comboBox_ZPulseSignal.Items[e.Index].ToString(), ComboBox.DefaultFont, Brushes.LightGray, e.Bounds);
            }
            else
            {
                e.DrawBackground();
                e.Graphics.DrawString(comboBox_ZPulseSignal.Items[e.Index].ToString(), ComboBox.DefaultFont, Brushes.Black, e.Bounds);
                e.DrawFocusRectangle();
            }
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
