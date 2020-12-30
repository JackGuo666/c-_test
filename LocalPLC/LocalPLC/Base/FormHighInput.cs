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
            int currentIndex = this.comboBox3.SelectedIndex;
            if (currentIndex < 0) return;

            if(currentIndex == 0)
            {
                label3.Visible = false;
                label4.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
                label2.Visible = false;
                textBox2.Visible = false;
            }
            else
            {

                label3.Visible = true;
                label4.Visible = true;
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                label2.Visible = true;
                textBox2.Visible = true;

                if (currentIndex == 1)
                {
                    //单脉冲计数
                    label3.Text = "脉冲输入:";
                    label4.Text = "方向输入:";
                    textBox2.Text = "脉冲/方向";
                }
                else if (currentIndex == 2)
                {
                    //双向
                    label3.Text = "脉冲输入:";
                    label4.Text = "脉冲输入:";
                    textBox2.Text = "脉冲/脉冲";
                }
                else if (currentIndex == 3)
                {
                    //正交
                    label3.Text = "脉冲输入:";
                    label4.Text = "脉冲输入:";
                    textBox2.Text = "脉冲/脉冲";
                }
            }
            


        }

    }


}
