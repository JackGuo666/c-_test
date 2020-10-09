using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyControl
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }



        private void UserControl1_Load(object sender, EventArgs e)
        {
            if (CheckIndex == 0)
            {
                checkBox1.Checked = true;
                checkBox2.Checked = false;
            }
            else if (CheckIndex == 1)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = true;
            }
            else 
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
        }


        [Browsable(true)]
        // Description 指定属性或事件的说明
        [Description("默认的复选框"), Category("自定义属性"), DefaultValue("-1")]
        public int CheckIndex { get; set; }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                checkBox2.Checked = false;
            }
            else
            {
                checkBox2.Checked = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
            }
           else
            {
                checkBox1.Checked = true;
            }
        }
    }
}


