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
                    comboBox_hardUpLimitInput.Items.Add(di.channelName);
                    comboBox_hardDownLimitInput.Items.Add(di.channelName);
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
            textBox_HardUpLimitOffset.Text = data.axisMotionPara.limitSignal.hardUpLimitInput.ToString();
            textBox_SoftDownLimitOffset.Text = data.axisMotionPara.limitSignal.hardDownLimitInput.ToString();
        }

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

        void initBackOriginal()
        {
            LocalPLC.Base.xml.DataManageBase dataManage = null;
            LocalPLC.UserControl1.UC.getDataManager(ref dataManage);
            foreach (var di in dataManage.diList)
            {
                if (!di.used)
                {
                    comboBox_BackOriginal.Items.Add(di.channelName);
                    comboBox_ZPulseSignal.Items.Add(di.channelName);
                }
            }

            levelDic.Clear();
            levelDic.Add((int)TypeLevel.HIGH_LEVEL, "高电平有效");
            levelDic.Add((int)TypeLevel.LOW_LEVEL, "低电平有效");
            foreach (var level in levelDic)
            {
                comboBox_BackOriginalSelectLevel.Items.Add(level.Value);
            }
        }


        void reverseCompensation()
        {
            textBox_ReverseCompensation.Text = data.axisMotionPara.reverseCompensation.reverseCompensation.ToString();
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

            //initPulseEquient();
            //initLimitSignal();
            //initDynamic();
            //initBackOriginal();
            //reverseCompensation();
        }

        private void button_valid_Click(object sender, EventArgs e)
        {

            int.TryParse(textBox_pulsePerRevolutionMotor.Text, out data.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor);
            int.TryParse(textBox_offsetPerReolutionMotor.Text, out data.axisMotionPara.pulseEquivalent.offsetPerReolutionMotor);


            data.axisMotionPara.limitSignal.hardLimitChecked = checkBox1.Checked;
            data.axisMotionPara.limitSignal.hardUpLimitInput = comboBox_hardUpLimitInput.Text;



            int.TryParse(comboBox_hardUpLimitLevel.Text, out data.axisMotionPara.limitSignal.hardUpLimitInputLevel);
            data.axisMotionPara.limitSignal.hardDownLimitInput = comboBox_hardDownLimitInput.Text;
            int.TryParse(comboBox_hardDownLimitLevel.Text, out data.axisMotionPara.limitSignal.hardDownLimitInputLevel);


            //启动硬限位
            data.axisMotionPara.limitSignal.softLimitChecked = checkBox_softLimit.Checked;
            //硬件上限位输入点
            data.axisMotionPara.limitSignal.hardUpLimitInput = comboBox_hardUpLimitInput.Text;
            data.axisMotionPara.limitSignal.hardUpLimitInputLevel = comboBox_hardUpLimitLevel.SelectedIndex;
            //硬件下限位输入点
            data.axisMotionPara.limitSignal.hardDownLimitInput = comboBox_hardDownLimitInput.Text;
            data.axisMotionPara.limitSignal.hardDownLimitInputLevel = comboBox_hardUpLimitLevel.SelectedIndex;


            //启动软限位
            data.axisMotionPara.limitSignal.softLimitChecked = checkBox_softLimit.Checked;
            int.TryParse(textBox_HardUpLimitOffset.Text, out data.axisMotionPara.limitSignal.softUpLimitInputOffset);
            int.TryParse(textBox_SoftDownLimitOffset.Text, out data.axisMotionPara.limitSignal.softDownLimitOffset);



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

            //回原点
            var orginal = data.axisMotionPara.backOriginal;
            orginal.orginInputSignal = comboBox_BackOriginal.Text;
            orginal.selectLevel = comboBox_BackOriginalSelectLevel.SelectedIndex;
            orginal.ZPulseSignal = comboBox_ZPulseSignal.Text;

            //反向间隙补偿
            var reverse = data.axisMotionPara.reverseCompensation;
            int.TryParse(textBox_ReverseCompensation.Text, out reverse.reverseCompensation);

        }

        private void textBox_pulsePerRevolutionMotor_TextChanged(object sender, EventArgs e)
        {
            if(textBox_pulsePerRevolutionMotor.Text != data.axisMotionPara.pulseEquivalent.pulsePerRevolutionMotor.ToString())
            {
                //button_valid.BackColor = Color.Red;
            }
        }
    }
}
