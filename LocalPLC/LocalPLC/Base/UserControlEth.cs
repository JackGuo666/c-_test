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
using LocalPLC.Interface;

namespace LocalPLC.Base
{
    public partial class UserControlEth : UserControl, IGetModifyFlag
    {
        public ETHERNETData ethernetValueData_ = null;
        bool configured_ = false;
        string etherName = "";
        bool initDone = false;
        public UserControlEth(UserControlBase ub, string name, ETHERNETData ethernetValueData, bool configured = false)
        {
            InitializeComponent();

            UserControl1 us = ub.parent_ as UserControl1;
            setTreeNodeStatusDelegate = new setTreeNodeStatusEventHandler(us.setTreeComEthNodeStats);

            ethernetValueData_ = ethernetValueData;
            configured_ = configured;
            etherName = name;
            var v = ipAddressControl_ipaddr.IPAddress;

            setButtonEnable(false);

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

            initDone = true;
        }

        #region 
        //代理
        public delegate void setTreeNodeStatusEventHandler(string tag, string name);
        setTreeNodeStatusEventHandler setTreeNodeStatusDelegate = null;
        #endregion

        #region
        //接口
        bool modifiedFlag = false;

        void setModifgFlag(bool flag)
        {
            modifiedFlag = flag;
        }

        //接口实现
        public bool getModifyFlag()
        {
            if(!checkValudIP())
            {
                // 不保存
                button_cancel_Click(null, null);
                return false;
            }

            if (modifiedFlag)
            {
                if (MessageBox.Show("是否保存修改数据?", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    // 保存
                    button_valid_Click(null, null);
                }
                else
                {
                    // 不保存
                    button_cancel_Click(null, null);
                }
            }

            return modifiedFlag;
        }


        #endregion


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

            if (!System.Net.IPAddress.TryParse(ipAddressControl_ipaddr.Text.ToString(), out ip))
            {
                string str = string.Format("{0} IP地址 无效!", ethernetValueData_.name);
                //utility.PrintError(str);
                MessageBox.Show(str);
                button_valid.Enabled = false;
                button_cancel.Enabled = true;
                return false;
            }
            else
            {
                ethernetValueData_.ipAddress = ipAddressControl_ipaddr.Text.ToString();
            }

            if (!System.Net.IPAddress.TryParse(ipAddressControl_maskaddr.Text.ToString(), out ip))
            {
                string str = string.Format("{0} 子网掩码地址 无效!", ethernetValueData_.name);
                //utility.PrintError(str);
                MessageBox.Show(str);
                button_valid.Enabled = false;
                button_cancel.Enabled = true;
                return false;
            }
            else
            {
                ethernetValueData_.maskAddress = ipAddressControl_maskaddr.Text.ToString();
            }

            if (!System.Net.IPAddress.TryParse(ipAddressControl_gateway.Text.ToString(), out ip))
            {
                string str = string.Format("{0} 网关地址 无效!", ethernetValueData_.name);
                //utility.PrintError(str);

                MessageBox.Show(str);
                button_valid.Enabled = false;
                button_cancel.Enabled = true;

                return false;
            }
            else
            {
                ethernetValueData_.gatewayAddress = ipAddressControl_gateway.Text.ToString();
            }

            if (!System.Net.IPAddress.TryParse(ipAddressControl_sntpaddr.Text.ToString(), out ip))
            {
                string str = string.Format("{0} sntp地址 无效!", ethernetValueData_.name);
                //utility.PrintError(str);
                MessageBox.Show(str);
                button_valid.Enabled = false;
                button_cancel.Enabled = true;

                return false;
            }
            else
            {
                ethernetValueData_.sntpServerIp = ipAddressControl_sntpaddr.Text.ToString();
            }


            if(checkBox_SNTP.Checked)
            {
                ethernetValueData_.checkSNTP = 1;
            }
            else
            {
                ethernetValueData_.checkSNTP = 0;
            }

            


            return true;
        }

        void refreshData()
        {
            textBox_eth.Text = ethernetValueData_.name;
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

            //IP
            ipAddressControl_ipaddr.IPAddress = System.Net.IPAddress.Parse(ethernetValueData_.ipAddress);
            //mask
            ipAddressControl_maskaddr.IPAddress = System.Net.IPAddress.Parse(ethernetValueData_.maskAddress);
            //gateway
            ipAddressControl_gateway.IPAddress = System.Net.IPAddress.Parse(ethernetValueData_.gatewayAddress);
            //sntp
            ipAddressControl_sntpaddr.IPAddress = System.Net.IPAddress.Parse(ethernetValueData_.gatewayAddress);


            if (ethernetValueData_.checkSNTP == 0)
            {
                checkBox_SNTP.Checked = false;
            }
            else
            {
                checkBox_SNTP.Checked = true;
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

        private void radioButton_dhcp_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton_dhcp.Checked)
            {
                if(initDone)
                {
                    setButtonEnable(true);
                    setModifgFlag(true);
                }
            }
        }

        private void radioButton_fixed_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton_fixed.Checked)
            {
                if(initDone)
                {
                    setButtonEnable(true);
                    setModifgFlag(true);
                }
            }
        }

        void setButtonEnable(bool enable)
        {
            button_valid.Enabled = enable;
            button_cancel.Enabled = enable;
        }
        private void ipAddressControl_ipaddr_TextChanged(object sender, EventArgs e)
        {
            if(ipAddressControl_ipaddr.Text != ethernetValueData_.ipAddress)
            {
                if(initDone)
                {

                    
                   setButtonEnable(true);
                    setModifgFlag(true);
                }
            }
        }

        private void ipAddressControl_maskaddr_TextChanged(object sender, EventArgs e)
        {
            if(ipAddressControl_maskaddr.Text != ethernetValueData_.maskAddress)
            {
                if (initDone)
                {
                    setButtonEnable(true);
                    setModifgFlag(true);
                }
            }
        }

        private void ipAddressControl_gateway_TextChanged(object sender, EventArgs e)
        {
            if(initDone)
            {
                setButtonEnable(true);
                setModifgFlag(true);
            }
        }

        private void ipAddressControl_sntpaddr_TextChanged(object sender, EventArgs e)
        {
            if (initDone)
            {
                setButtonEnable(true);
                setModifgFlag(true);
            }
        }

        private void button_valid_Click(object sender, EventArgs e)
        {
            if (!getDataFromUI())
            {
                return;
            }

            setButtonEnable(false);
            setModifgFlag(false);
            setTreeNodeStatusDelegate("ETHERNET", etherName);

            utility.setDebugIP(ipAddressControl_ipaddr.IPAddress.ToString());

        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            refreshData();
            setButtonEnable(false);
            setModifgFlag(false);
        }

        private void checkBox_SNTP_CheckedChanged(object sender, EventArgs e)
        {
            if(initDone)
            {
                setButtonEnable(true);
                setModifgFlag(true);
            }

        }

        private void ipAddressControl_maskaddr_Validated(object sender, EventArgs e)
        {
            //System.Net.IPAddress ip;
            //if (!System.Net.IPAddress.TryParse(ipAddressControl_maskaddr.Text.ToString(), out ip))
            //{
            //    MessageBox.Show("IP地址无效");
            //}
        }

        bool checkValudIP()
        {
            System.Net.IPAddress ip;

            if (!System.Net.IPAddress.TryParse(ipAddressControl_ipaddr.Text.ToString(), out ip))
            {
                return false;
            }

            if (!System.Net.IPAddress.TryParse(ipAddressControl_maskaddr.Text.ToString(), out ip))
            {
                return false;
            }

            if (!System.Net.IPAddress.TryParse(ipAddressControl_gateway.Text.ToString(), out ip))
            {
                return false;
            }

            if (!System.Net.IPAddress.TryParse(ipAddressControl_sntpaddr.Text.ToString(), out ip))
            {
                return false;
            }

            return true;
        }

        private void ipAddressControl_ipaddr_Validated(object sender, EventArgs e)
        {
            //System.Net.IPAddress ip;

            //if (!System.Net.IPAddress.TryParse(ipAddressControl_ipaddr.Text.ToString(), out ip))
            //{
            //    MessageBox.Show("IP地址无效");
            //}
        }

        private void ipAddressControl_gateway_Validated(object sender, EventArgs e)
        {
            //System.Net.IPAddress ip;
            //if (!System.Net.IPAddress.TryParse(ipAddressControl_gateway.Text.ToString(), out ip))
            //{
            //    MessageBox.Show("IP地址无效");
            //}
        }

        private void ipAddressControl_sntpaddr_Validated(object sender, EventArgs e)
        {
            //System.Net.IPAddress ip;
            //if (!System.Net.IPAddress.TryParse(ipAddressControl_sntpaddr.Text.ToString(), out ip))
            //{
            //    MessageBox.Show("IP地址无效");
            //}
        }
    }
}
