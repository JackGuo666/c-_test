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
    public partial class UserControlBidirPulse : UserControl
    {
        List<string> pulseList = new List<string>();
        public UserControlBidirPulse(string name)
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

            init();
        }

        void init()
        {
            foreach (string str in pulseList)
            {
                comboBox1.Items.Add(str);
                comboBox2.Items.Add(str);
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;





            string u16s = "你"; //默认的字符编码是unicode,也就是utf16  

            //4种编码  
            Encoding utf8 = Encoding.UTF8;
            Encoding utf16 = Encoding.Unicode;
            Encoding gb = Encoding.GetEncoding("gbk");
            Encoding b5 = Encoding.GetEncoding("big5");

            //转换得到4种编码的字节流  
            byte[] u16bytes = utf16.GetBytes(u16s);
            byte[] u8bytes = Encoding.Convert(utf16, utf8, u16bytes);
            byte[] gbytes = Encoding.Convert(utf16, gb, u16bytes);
            byte[] bbytes = Encoding.Convert(utf16, b5, u16bytes);

            string str = System.Text.Encoding.Default.GetString(gbytes);
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
