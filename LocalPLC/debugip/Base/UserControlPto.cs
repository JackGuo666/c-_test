using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalPLC.Base
{
    public partial class UserControlPto : UserControl
    {
        List<string> pulseList = new List<string>();
        public UserControlPto(string name)
        {
            InitializeComponent();

            pulseList.Add("DI0");
            pulseList.Add("DI1");
            pulseList.Add("DI2");
            pulseList.Add("DI3");
            pulseList.Add("DI4");
            pulseList.Add("DI5");
            pulseList.Add("DI6");
            pulseList.Add("DI7");

            comboBox7.Items.Add("正交输出");
            comboBox7.Items.Add("双相输出");
            comboBox7.Items.Add("单路输出");

            comboBox7.SelectedIndex = 1;


            init();
        }

        void init()
        {
            foreach (string str in pulseList)
            {
                comboBox1.Items.Add(str);
                comboBox2.Items.Add(str);
            }

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentIndex = this.comboBox7.SelectedIndex;
            if (currentIndex < 0) return;

            if(comboBox7.Text == "正交输出")
            {
                label2.Text = "输出模式:脉冲/脉冲";
                label6.Text = "脉冲输入";
            }
            else if(comboBox7.Text == "双相输出")
            {
                label2.Text = "输出模式:脉冲/脉冲";
                label6.Text = "脉冲输入";
            }
            else if(comboBox7.Text == "单路输出")
            {
                label2.Text = "输出模式:脉冲/方向";
                label6.Text = "方向输入";
            }
        }
    }
}
