using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace demo_jicheng_form
{
    public partial class Form1 : BaseForm.Form1
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ShowMsg();
        }

        public override  void ShowMsg()
        {
            MessageBox.Show("这是子窗口提示信息的事件");
        }
    }
}
