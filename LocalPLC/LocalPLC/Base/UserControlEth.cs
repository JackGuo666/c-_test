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
        public ETHERNETData ethernetValueData_ = null;
        bool configured_ = false;
        string etherName = "";
        public UserControlEth(string name, ETHERNETData ethernetValueData, bool configured = false)
        {
            InitializeComponent();
            ethernetValueData_ = ethernetValueData;
            configured_ = configured;
            etherName = name;
            var v = ipAddressControl_ipaddr.IPAddress;



            init();
            //数据管理里的网口数组 LocalPLC一般是1个网口
            //UserControlBase.dataManage.ethernetDic.Add(etherName, ethernetValueData_);

            var s = "0.0.0.0";
            string maskAddress = "0.0.0.0";
            try
            {
                //IP
                ipAddressControl_ipaddr.IPAddress = System.Net.IPAddress.Parse(ethernetValueData_.ipAddress);
                //mask
                ipAddressControl_maskaddr.IPAddress = System.Net.IPAddress.Parse(ethernetValueData_.maskAddress);
                //gateway
                ipAddressControl_gateway.IPAddress = System.Net.IPAddress.Parse(ethernetValueData_.gatewayAddress);
                //sntp
                ipAddressControl_sntpaddr.IPAddress = System.Net.IPAddress.Parse(ethernetValueData_.sntpServerIp);

                //1 dhcp    0固定
                if (ethernetValueData_.ipMode == 1)
                {
                    radioButton_dhcp.Checked = true;
                    radioButton_fixed.Checked = false;
                }
                else
                {
                    radioButton_dhcp.Checked = false;
                    radioButton_fixed.Checked = true;
                }

                if(ethernetValueData_.checkSNTP == 0)
                {
                    checkBox_SNTP.Checked = false;
                }
                else
                {
                    checkBox_SNTP.Checked = true;
                }

                textBox_eth.Text = etherName;
            }
            catch(Exception e)
            {
                MessageBox.Show(string.Format("{1}模块{0}", e.Message, etherName));
            }
            
        }

        enum EthernetMode { FIXED, DHCP };
        public bool getDataFromUI()
        {
            System.Net.IPAddress ip;
            ethernetValueData_.name = textBox_eth.Text.ToString();
            if (radioButton_dhcp.Checked)
            {
                ethernetValueData_.ipMode = (int)EthernetMode.DHCP;
            }
            else if (radioButton_fixed.Checked)
            {
                ethernetValueData_.ipMode = (int)EthernetMode.FIXED;
            }

            ethernetValueData_.ipAddress = ipAddressControl_ipaddr.Text.ToString();
            ethernetValueData_.maskAddress = ipAddressControl_maskaddr.Text.ToString();
            ethernetValueData_.gatewayAddress = ipAddressControl_gateway.Text.ToString();
            ethernetValueData_.sntpServerIp = ipAddressControl_sntpaddr.Text.ToString();

            if(!System.Net.IPAddress.TryParse(ethernetValueData_.ipAddress, out ip))
            {
                string str = string.Format("{0} IPAddress 无效!", ethernetValueData_.name);
                utility.PrintError(str);
                return false;
            }

            if (!System.Net.IPAddress.TryParse(ethernetValueData_.maskAddress, out ip))
            {
                string str = string.Format("{0} IP Address 无效!", ethernetValueData_.name);
                utility.PrintError(str);
                return false;
            }

            if (!System.Net.IPAddress.TryParse(ethernetValueData_.maskAddress, out ip))
            {
                string str = string.Format("{0} Mask Address 无效!", ethernetValueData_.name);
                utility.PrintError(str);
                return false;
            }

            if (!System.Net.IPAddress.TryParse(ethernetValueData_.gatewayAddress, out ip))
            {
                string str = string.Format("{0} Gateway Address 无效!", ethernetValueData_.name);
                utility.PrintError(str);

                return false;
            }

            if(checkBox_SNTP.Checked)
            {
                ethernetValueData_.checkSNTP = 1;
            }
            else
            {
                ethernetValueData_.checkSNTP = 0;
            }

            
            if (!System.Net.IPAddress.TryParse(ethernetValueData_.sntpServerIp, out ip))
            {
                string str = string.Format("{0} sntp Address 无效!", ethernetValueData_.name);
                utility.PrintError(str);

                return false;
            }

            return true;
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
                                        if(configured_)
                                        {
                                            int.TryParse(ethernetData.defaultValue, out ethernetValueData_.ipMode);
                                        }
                                    }
                                    else if(ethernetData.name == "IPAddress")
                                    {
                                        if (configured_)
                                        {
                                            ethernetValueData_.ipAddress = ethernetData.defaultValue;
                                        }
                                    }
                                    else if(ethernetData.name == "MaskAddress")
                                    {
                                        if (configured_)
                                        {
                                            ethernetValueData_.maskAddress = ethernetData.defaultValue;
                                        }
                                    }
                                    else if(ethernetData.name == "GatewayAddress")
                                    {
                                        if (configured_)
                                        {
                                            ethernetValueData_.gatewayAddress = ethernetData.defaultValue;
                                        }
                                    }
                                    else if(ethernetData.name == "SNTP")
                                    {
                                        if (configured_)
                                        {
                                            int.TryParse(ethernetData.defaultValue, out ethernetValueData_.checkSNTP);
                                        }
                                    }
                                    else if(ethernetData.name == "SNTPIPAddress")
                                    {
                                        if (configured_)
                                        {
                                            ethernetValueData_.sntpServerIp = ethernetData.defaultValue;
                                        }
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
