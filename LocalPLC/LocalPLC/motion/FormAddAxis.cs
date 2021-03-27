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
        //枚举
        enum AXISTYPE { PULSE_AXIS, BUS_AXIS }
        Dictionary<int, string> axisTypeDescDic = new Dictionary<int, string>();
        List<string> list_ = new List<string>();

        public string axisKey = "";
        public string hardwareinterface = "";
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
                    //
                    list.Add(hsp.name);
                }
            }
        }


        #endregion


        public FormAddAxis()
        {
            InitializeComponent();




            axisTypeDescDic.Clear();
            axisTypeDescDic.Add(((int)AXISTYPE.PULSE_AXIS), "脉冲轴");
            axisTypeDescDic.Add(((int)AXISTYPE.BUS_AXIS), "总线轴(CANOpen)");

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

            if(comboBox1.SelectedIndex >= 0)
            {
               if (list_.Count == 0 || comboBox2.SelectedIndex < 0)
               {
                   MessageBox.Show(string.Format("请配置高速输出模块配置PTO轴!"));
                   return;
                }

                axisKey = comboBox2.Text;


                hardwareinterface = comboBox2.Text;

                this.DialogResult = DialogResult.OK;
            }
            else if (comboBox1.SelectedIndex < 0)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                comboBox2.Enabled = true;
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                comboBox2.Enabled = false;
            }
        }
    }
}
