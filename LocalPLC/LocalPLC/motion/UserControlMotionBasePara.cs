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
    public partial class UserControlMotionBasePara : UserControl
    {
        #region
        TreeNode node_ = null;
        LocalPLC.Base.xml.DataManageBase dataManage = null;

        Axis data = null;
        //AxisBasePara axisBasePara = null;
        #endregion

        #region
        //枚举
        enum AXISTYPE { PULSE_AXIS, BUS_AXIS}
        Dictionary<int, string> axisTypeDescDic = new Dictionary<int, string>();


        enum MEASUREUNIT { MM, ANGLE, PULSE}
        Dictionary<int, string> axisMeaUnitDescDic = new Dictionary<int, string>();

        enum OutputMode { CW_CCW, PULSE_DIC, AB_DIRECTION }
        Dictionary<int, string> outputPluseDic = new Dictionary<int, string>();

        #endregion

        #region
        public class ComboboxItem : object
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        #endregion


        #region
        void initCombo()
        {
            //comboBox_AxisType.Items
            foreach(var axis in axisTypeDescDic)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = axis.Value;
                item.Value = axis.Key;

                comboBox_AxisType.Items.Add(item);
            }
            if(comboBox_AxisType.Items.Count > 0)
            {
                comboBox_AxisType.SelectedIndex = 0;
            }


            foreach (var mea in axisMeaUnitDescDic)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = mea.Value;
                item.Value = mea.Key;

                comboBox_MeasureUnit.Items.Add(item);
            }

            if (comboBox_MeasureUnit.Items.Count > 0)
            {
                comboBox_MeasureUnit.SelectedIndex = 0;
            }

            List<string> list = new List<string>();
            getPTOFromHSP(ref list);
            foreach(var pto in list)
            {
                comboBox_HardwareInterface.Items.Add(pto);
            }
        }

        void initDic()
        {
            axisTypeDescDic.Clear();
            axisTypeDescDic.Add(((int)AXISTYPE.PULSE_AXIS), "脉冲轴");
            axisTypeDescDic.Add(((int)AXISTYPE.BUS_AXIS), "总线轴(CANOpen)");


            axisMeaUnitDescDic.Clear();
            axisMeaUnitDescDic.Add(((int)MEASUREUNIT.MM), "mm");
            axisMeaUnitDescDic.Add(((int)MEASUREUNIT.ANGLE), "°");
            axisMeaUnitDescDic.Add(((int)MEASUREUNIT.PULSE), "pulse");

            outputPluseDic.Clear();
            outputPluseDic.Add((int)OutputMode.CW_CCW, "CW / CCW");
            outputPluseDic.Add((int)OutputMode.PULSE_DIC, "脉冲 / 方向");
            outputPluseDic.Add((int)OutputMode.AB_DIRECTION, "正交输出");
        }

        
        void getPTOFromHSP(ref List<string> list)
        {
            UserControl1.UC.getDataManager(ref dataManage);

            foreach(var hsp in dataManage.hspList)
            {
                if(hsp.used && hsp.type == (int)LocalPLC.Base.UserControlHighOutput.TYPE.PTO)
                {
                    //
                    list.Add(hsp.name);
                }
            }
        }

        #endregion

        public UserControlMotionBasePara(TreeNode node, string axisName)
        {
            InitializeComponent();

            initDic();

            initCombo();

            if (node.Parent == null)
            {
                utility.PrintInfo("{0}节点没有父节点!");
                return;
            }

            data = node.Parent.Tag as Axis;

            node_ = node;

            richTextBox_AxisName.Text = data.name;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox_AxisName_TextChanged(object sender, EventArgs e)
        {
            data.name = (sender as RichTextBox).Text;
            node_.Parent.Text = data.name;
        }

        private void comboBox_HardwareInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_HardwareInterface.SelectedItem != null)
            {
                var pto = comboBox_HardwareInterface.SelectedItem.ToString();
                foreach (var hsp in dataManage.hspList)
                {
                    if (pto == hsp.name)
                    {
                        if (outputPluseDic.ContainsKey(hsp.outputMode))
                        {
                            richTextBox_SignalType.Text = outputPluseDic[hsp.outputMode];
                            richTextBox_PulseOutput.Text = hsp.pulsePort;
                            richTextBox_DirOutput.Text = hsp.directionPort;
                        }

                    }
                }
            }
        }

        void setDataFromUI()
        {


            if (comboBox_HardwareInterface.SelectedItem != null)
            {
                data.axisBasePara.hardwareInterface = comboBox_HardwareInterface.SelectedItem.ToString();
            }
            else
            {

            }


            data.axisBasePara.axisName = richTextBox_AxisName.Text;
            data.axisBasePara.axisType = comboBox_AxisType.SelectedIndex;
            data.axisBasePara.meaUnit = comboBox_MeasureUnit.SelectedIndex;


            foreach (var outputPulse in outputPluseDic)
            {
                if (outputPulse.Value == richTextBox_SignalType.Text)
                {
                    data.axisBasePara.signalType = outputPulse.Key;
                }
            }

            data.axisBasePara.pulseoutput = richTextBox_PulseOutput.Text;
            data.axisBasePara.dirOutput = richTextBox_DirOutput.Text;
        }

        void refreshData()
        {
            richTextBox_AxisName.Text = data.axisBasePara.axisName;

            //轴类型
            foreach(var item in comboBox_AxisType.Items)
            {
                var current = item as ComboboxItem;
                if(current == null)
                {
                    return;
                }

                if(current.Value.ToString() == data.axisBasePara.axisType.ToString())
                {
                    comboBox_AxisType.SelectedItem = current.Text;
                }
            }

            //测量单位
            foreach(var item in comboBox_MeasureUnit.Items)
            {
                var current = item as ComboboxItem;
                if(current == null)
                {
                    return;
                }

                if(current.Value.ToString() == data.axisBasePara.meaUnit.ToString())
                {
                    comboBox_MeasureUnit.SelectedItem = current.Text;
                }
            }

            foreach (var item in comboBox_HardwareInterface.Items)
            {
                var current = item;
                if (current == null)
                {
                    return;
                }

                if (current.ToString() == data.axisBasePara.hardwareInterface)
                {
                    comboBox_HardwareInterface.SelectedItem = data.axisBasePara.hardwareInterface;
                }
            }
        }


        private void button_valid_Click(object sender, EventArgs e)
        {
            setDataFromUI();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            refreshData();
        }

        private void comboBox_MeasureUnit_DataSourceChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox_MeasureUnit_KeyDown(object sender, KeyEventArgs e)
        {
            return;
        }

        private void comboBox_MeasureUnit_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox_AxisType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_HardwareInterface_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_MeasureUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
