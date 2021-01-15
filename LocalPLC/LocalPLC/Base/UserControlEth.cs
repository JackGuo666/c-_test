using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LocalPLC.Base.xml;

namespace LocalPLC.Base
{
    public partial class UserControlEth : UserControl
    {
        public ETHERNETData ethernetValueData = new ETHERNETData();
        string etherName = "";
        public UserControlEth(string name)
        {
            InitializeComponent();
            etherName = name;
            var v = ipAddressControl_ipaddr.IPAddress;



            init();
            //数据管理里的网口数组 LocalPLC一般是1个网口
            UserControlBase.dataManage.ethernetDic.Add(etherName, ethernetValueData);

            var s = "0.0.0.0";
            string maskAddress = "0.0.0.0";
            //IP
            ipAddressControl_ipaddr.IPAddress = System.Net.IPAddress.Parse(ethernetValueData.ipAddress);
            //mask
            ipAddressControl_maskaddr.IPAddress = System.Net.IPAddress.Parse(ethernetValueData.maskAddress);
            //gateway
            ipAddressControl_gateway.IPAddress = System.Net.IPAddress.Parse(ethernetValueData.gatewayAddress);
            //sntp
            ipAddressControl_sntpaddr.IPAddress = System.Net.IPAddress.Parse(ethernetValueData.gatewayAddress);

            //0 dhcp    1固定
            if(ethernetValueData.ipMode == 0)
            {
                radioButton_dhcp.Checked = true;
                radioButton_fixed.Checked = false;
            }

            checkBox_SNTP.Checked = false;
            textBox_eth.Text = etherName;
        }

        enum EthernetMode { FIXED, DHCP };
        public void getDataFromUI()
        {
            System.Net.IPAddress ip;
            ethernetValueData.name = textBox_eth.Text.ToString();
            if (radioButton_dhcp.Checked)
            {
                ethernetValueData.ipMode = (int)EthernetMode.DHCP;
            }
            else if (radioButton_fixed.Checked)
            {
                ethernetValueData.ipMode = (int)EthernetMode.FIXED;
            }

            ethernetValueData.ipAddress = ipAddressControl_ipaddr.Text.ToString();
            ethernetValueData.maskAddress = ipAddressControl_maskaddr.Text.ToString();
            ethernetValueData.gatewayAddress = ipAddressControl_gateway.Text.ToString();
            ethernetValueData.sntpServerIp = ipAddressControl_sntpaddr.Text.ToString();

            if(!System.Net.IPAddress.TryParse(ethernetValueData.ipAddress, out ip))
            {
                string str = string.Format("{0} IPAddress 无效!", ethernetValueData.name);
                utility.PrintError(str);
            }

            if (!System.Net.IPAddress.TryParse(ethernetValueData.maskAddress, out ip))
            {
                string str = string.Format("{0} IP Address 无效!", ethernetValueData.name);
                utility.PrintError(str);
            }

            if (!System.Net.IPAddress.TryParse(ethernetValueData.maskAddress, out ip))
            {
                string str = string.Format("{0} Mask Address 无效!", ethernetValueData.name);
                utility.PrintError(str);
            }

            if (!System.Net.IPAddress.TryParse(ethernetValueData.gatewayAddress, out ip))
            {
                string str = string.Format("{0} Gateway Address 无效!", ethernetValueData.name);
                utility.PrintError(str);
            }

            if(checkBox_SNTP.Checked)
            {
                ethernetValueData.checkSNTP = 1;
            }
            else
            {
                ethernetValueData.checkSNTP = 0;
            }

            
            if (!System.Net.IPAddress.TryParse(ethernetValueData.sntpServerIp, out ip))
            {
                string str = string.Format("{0} sntp Address 无效!", ethernetValueData.name);
                utility.PrintError(str);
            }

        }


        void init()
        {
            var list = UserControlBase.dataManage.modules.list;
            foreach (var elem in list)
            {
                if (elem.moduleID == "ETHERNET")
                {
                    
                    foreach(Parameter para in elem.connectModules.list)
                    {
                        string type = para.type;
                        string[] strArr = type.Split(new Char[] { ':' });
                        if (strArr.Length == 2)
                        {
                            //串口type localTypes
                            string localType = strArr.ElementAt(0);
                            string ethernetBusType = strArr.ElementAt(1);
                            if (UserControlBase.dataManage.dicStruct.ContainsKey(ethernetBusType))
                            {
                                //Ethernet line数据结构类型
                                var ethernetStructType = UserControlBase.dataManage.dicStruct[ethernetBusType];
                                foreach(var ethernetData in ethernetStructType.list)
                                {
                                    if(ethernetData.name == "IPConfigMode")
                                    {
                                        //ethernetValueData.ipMode = ethernetData.defaultValue;
                                        int.TryParse(ethernetData.defaultValue, out ethernetValueData.ipMode);
                                    }
                                    else if(ethernetData.name == "IPAddress")
                                    {
                                        ethernetValueData.ipAddress = ethernetData.defaultValue;
                                    }
                                    else if(ethernetData.name == "MaskAddress")
                                    {
                                        ethernetValueData.maskAddress = ethernetData.defaultValue;
                                    }
                                    else if(ethernetData.name == "GatewayAddress")
                                    {
                                        ethernetValueData.gatewayAddress = ethernetValueData.maskAddress;
                                    }
                                    else if(ethernetData.name == "SNTP")
                                    {
                                        int.TryParse(ethernetData.defaultValue, out ethernetValueData.checkSNTP);
                                    }
                                    else if(ethernetData.name == "SNTPIPAddress")
                                    {
                                        ethernetValueData.gatewayAddress = ethernetData.defaultValue;
                                    }
                                }
                            }
                        }
                    }

                }
            }

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

            byte[] tttbytes = gb.GetBytes(textBox_eth.Text);


            //转换得到4种编码的字节流  
            //unicode
            byte[] u16bytes = utf16.GetBytes(u16s);
            byte[] u8bytes = Encoding.Convert(utf16, utf8, u16bytes);
            byte[] gbytes = Encoding.Convert(utf16, gb, u16bytes);
            byte[] bbytes = Encoding.Convert(utf16, b5, u16bytes);

            //gdb编码字符串转换成string
            string str = System.Text.Encoding.Default.GetString(gbytes);
            if(textBox_eth.Text != str)
            {
                //textBox1.Text = str;
            }

        }
    }
}
