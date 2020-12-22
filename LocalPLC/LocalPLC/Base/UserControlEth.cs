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
    public partial class UserControlEth : UserControl
    {
        public UserControlEth(string name)
        {
            InitializeComponent();

            var v = ipAddressControl1.IPAddress;

            var s = "192.168.0.1";
            ipAddressControl1.IPAddress = System.Net.IPAddress.Parse(s);
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string u16s = "你"; //默认的字符编码是unicode,也就是utf16  




            //4种编码  
            Encoding utf8 = Encoding.UTF8;
            Encoding utf16 = Encoding.Unicode;
            Encoding gb = Encoding.GetEncoding("gbk");
            Encoding b5 = Encoding.GetEncoding("big5");

            byte[] tttbytes = gb.GetBytes(textBox1.Text);


            //转换得到4种编码的字节流  
            //unicode
            byte[] u16bytes = utf16.GetBytes(u16s);
            byte[] u8bytes = Encoding.Convert(utf16, utf8, u16bytes);
            byte[] gbytes = Encoding.Convert(utf16, gb, u16bytes);
            byte[] bbytes = Encoding.Convert(utf16, b5, u16bytes);

            //gdb编码字符串转换成string
            string str = System.Text.Encoding.Default.GetString(gbytes);
            if(textBox1.Text != str)
            {
                //textBox1.Text = str;
            }

        }
    }
}
