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
    public partial class PopUserControl2 : UserControl
    {
        public PopUserControl2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(PopWidth, PopHeight, PopText);
            form1.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [Description("打开窗口宽度"),Category("自定义窗体"),DefaultValue("")]
        public int PopWidth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [Description("打开窗口高度"), Category("自定义窗体"), DefaultValue("")]
        public int PopHeight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [Description("Text"), Category("自定义窗体"), DefaultValue("")]
        public string PopText { get; set; }
    }
}
