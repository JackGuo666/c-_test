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
    
    #region


    #endregion


    public partial class UserControlMotionPara : UserControl
    {


        #region
        Axis data = null;
        TreeNode node_ = null;

        enum TypeLevel { HIGH_LEVEL, LOW_LEVEL}

        Dictionary<int, string> levelDic = new Dictionary<int, string>();
        #endregion



        #region

        void initPulseEquient()
        {
            textBox_pulsePerRevolutionMotor.Text = data.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor.ToString();
            textBox_offsetPerReolutionMotor.Text = data.axisMotionPara.pulseEquivalent.offsetPerReolutionMotor.ToString();
        }

        void initLimitSignal()
        {
            //是否启动限位信号
            checkBox1.Checked = data.axisMotionPara.limitSignal.hardLimitChecked;

            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            foreach(var di in dataManage.diList)
            {
                if(!di.used)
                {
                    comboBox1.Items.Add(di.channelName);
                    comboBox2.Items.Add(di.channelName);
                }
            }

            levelDic.Clear();
            levelDic.Add((int)TypeLevel.HIGH_LEVEL, "高电平有效");
            levelDic.Add((int)TypeLevel.LOW_LEVEL, "低电平有效");
            foreach(var level in levelDic)
            {
                comboBox_hardUpLimitLevel.Items.Add(level.Value);
                comboBox_hardDownLimitLevel.Items.Add(level.Value);
            }
            comboBox_hardUpLimitLevel.SelectedIndex = (int)TypeLevel.HIGH_LEVEL;
            comboBox_hardDownLimitLevel.SelectedIndex = (int)TypeLevel.HIGH_LEVEL;

            checkBox_softLimit.Checked = data.axisMotionPara.limitSignal.softLimitChecked;
            textBox3.Text = data.axisMotionPara.limitSignal.hardUpLimitInput.ToString();
            textBox5.Text = data.axisMotionPara.limitSignal.hardDownLimitInput.ToString();
        }

        void initDynamic()
        {
            textBox_MaxSpeed.Text = data.axisMotionPara.dynamicPara.maxSpeed.ToString();
            textBox_AcceleratedSpeed.Text = data.axisMotionPara.dynamicPara.acceleratedSpeed.ToString();
        }

        #endregion

        public UserControlMotionPara(TreeNode node)
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
            initLimitSignal();
        }
    }
}
