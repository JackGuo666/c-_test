using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalPLC.Base
{
    public partial class FormHighInput : Form
    {
        public FormHighInput()
        {
            InitializeComponent();

            comboBox3.Items.Add("未配置");
            comboBox3.Items.Add("单脉冲计数");
            comboBox3.Items.Add("双相脉冲计数");
            comboBox3.Items.Add("正交编码器");


            comboBox3.TextChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iCurrentIndex = this.comboBox3.SelectedIndex;
            if (iCurrentIndex < 0) return;


        }

    }


}
