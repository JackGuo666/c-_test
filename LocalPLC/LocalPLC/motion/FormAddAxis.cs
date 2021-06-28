using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalPLC.motion
{
    public partial class FormAddAxis : Form
    {
        #region
        //枚举 总线轴 虚拟轴 脉冲轴
        enum AXISTYPE { BUS_AXIS, VERTUAL_AXIS, PULSE_AXIS}
        Dictionary<int, string> axisTypeDescDic = new Dictionary<int, string>();
        List<string> list_ = new List<string>();

        public string axisKey = "";
        public string hardwareinterface = "";
        public int axisType = (int)AXISTYPE.BUS_AXIS;
        #endregion


        #region
        void getPTOFromHSP(ref List<string> list)
        {
            LocalPLC.Base.xml.DataManageBase dataManage = null;
            UserControl1.UC.getDataManager(ref dataManage);

            foreach (var hsp in dataManage.hspList)
            {
                if (hsp.used && hsp.type == (int)LocalPLC.Base.UserControlHighOutput.TYPE.PTO)
                {
                    if(!hsp.usedAxis)
                    {
                        //
                        list.Add(hsp.address);
                    }
                }
            }
        }


        #endregion


        public FormAddAxis()
        {
            InitializeComponent();




            axisTypeDescDic.Clear();
            axisTypeDescDic.Add(((int)AXISTYPE.BUS_AXIS), "总线轴(CANOpen)");
            axisTypeDescDic.Add(((int)AXISTYPE.VERTUAL_AXIS), "虚拟轴");
            axisTypeDescDic.Add(((int)AXISTYPE.PULSE_AXIS), "脉冲轴");


            foreach (var axis in axisTypeDescDic)
            {
                comboBox1.Items.Add(axis.Value);
            }

            list_.Clear();
            getPTOFromHSP(ref list_);
            foreach(var hsp in  list_)
            {
                comboBox2.Items.Add(hsp);
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //脉冲轴判断轴对象
            if(comboBox1.SelectedIndex == ((int)AXISTYPE.PULSE_AXIS))
            {
               if (list_.Count == 0)
               {
                   MessageBox.Show(string.Format("请配置高速输出模块配置PTO轴!"));
                   return;
                }
               else if (comboBox2.SelectedIndex < 0)
                {
                    MessageBox.Show(string.Format("请选择轴对象!"));
                    return;
                }

                axisKey = comboBox2.Text;


                hardwareinterface = comboBox2.Text;

                LocalPLC.Base.xml.DataManageBase dataManage = null;
                UserControl1.UC.getDataManager(ref dataManage);

                foreach (var hsp in dataManage.hspList)
                {
                    if(hsp.address == axisKey)
                    {
                        hsp.usedAxis = true;
                    }
                }

                axisType = ((int)AXISTYPE.PULSE_AXIS);
                    this.DialogResult = DialogResult.OK;
                
            }
            else
            {
                axisKey = comboBox2.Text;
                hardwareinterface = comboBox2.Text;

                axisType = comboBox1.SelectedIndex;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == ((int)AXISTYPE.BUS_AXIS))
            {
                //轴对象无效
                comboBox2.Enabled = false;
            }
            else if(comboBox1.SelectedIndex == ((int)AXISTYPE.VERTUAL_AXIS))
            {
                comboBox2.Enabled = false;
            }
            else if(comboBox1.SelectedIndex == ((int)AXISTYPE.PULSE_AXIS))
            {

                comboBox2.Enabled = true;
            }
        }

        private void FormAddAxis_Load(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == ((int)AXISTYPE.PULSE_AXIS))
            {
                comboBox2.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;
            }
        }
    }
}
