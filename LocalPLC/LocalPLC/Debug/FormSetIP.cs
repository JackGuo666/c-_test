using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JsonSerializerAndDeSerializer;
using DebugLib.Protocols;
using DebugLib;

namespace LocalPLC.Debug
{
    public partial class FormSetIP : Form
    {
        readonly UserControlDebug debug_ = null;
        public SendModel model_ = null;
        public FormSetIP(UserControlDebug debug, SendModel model)
        {
            InitializeComponent();
            this.Text = "IP设置";

            debug_ = debug;
            model_ = model;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendModel model = new SendModel();
            model.cmd = DebugCommand.CommandSetIP;
            if(radioButton_dhcp.Checked)
            {
                model.dhcp = true;

                System.Net.IPAddress ip;
                if (!System.Net.IPAddress.TryParse(ipAddressControl_ipaddr.Text.ToString(), out ip))
                {
                    string str = string.Format("{0} IP地址 无效!", ipAddressControl_ipaddr.Text);
                    //utility.PrintError(str);
                    MessageBox.Show(str);
                    return;
                }

                if (!System.Net.IPAddress.TryParse(ipAddressControl_maskaddr.Text.ToString(), out ip))
                {
                    string str = string.Format("{0} 子网掩码地址 无效!", ipAddressControl_maskaddr.Text);
                    //utility.PrintError(str);
                    MessageBox.Show(str);
                    return;
                }

                if (!System.Net.IPAddress.TryParse(ipAddressControl_gateway.Text.ToString(), out ip))
                {
                    string str = string.Format("{0} 网关地址 无效!", ipAddressControl_gateway.Text);
                    //utility.PrintError(str);
                    MessageBox.Show(str);

                    return;
                }

                model.ip = ipAddressControl_ipaddr.Text;
                model.subnet_mask = ipAddressControl_maskaddr.Text;
                model.gateway = ipAddressControl_gateway.Text;

            }
            else if(radioButton_fixed.Checked)
            {
                model.dhcp = false;

                System.Net.IPAddress ip;
                if (!System.Net.IPAddress.TryParse(ipAddressControl_ipaddr.Text.ToString(), out ip))
                {
                    string str = string.Format("{0} IP地址 无效!", ipAddressControl_ipaddr.Text);
                    //utility.PrintError(str);
                    MessageBox.Show(str);
                    return;
                }

                if (!System.Net.IPAddress.TryParse(ipAddressControl_maskaddr.Text.ToString(), out ip))
                {
                    string str = string.Format("{0} 子网掩码地址 无效!", ipAddressControl_maskaddr.Text);
                    //utility.PrintError(str);
                    MessageBox.Show(str);
                    return;
                }

                if (!System.Net.IPAddress.TryParse(ipAddressControl_gateway.Text.ToString(), out ip))
                {
                    string str = string.Format("{0} 网关地址 无效!", ipAddressControl_gateway.Text);
                    //utility.PrintError(str);
                    MessageBox.Show(str);

                    return;
                }


            }



            model.ip = ipAddressControl_ipaddr.Text;
            model.subnet_mask = ipAddressControl_maskaddr.Text;
            model.gateway = ipAddressControl_gateway.Text;
            model.dev_mac = model_.dev_mac;
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            model.timestamp = Convert.ToInt64(ts.TotalSeconds);

            bool error = false;
            foreach (var driver in LocalPLC.UserControl1.ucDebug.driverDic)
            {
                var command = new DebugCommand(model) { };
                var result = driver.Key.ExecuteGeneric(driver.Value, command);

                if(result.Status == DebugLib.CommResponse.Critical)
                {
                    error = true;
                }
            }

            if(error)
            {
                   MessageBox.Show("IP地址修改出现问题，请重新启动multiprog!");
            }
            else
            {
                MessageBox.Show("IP地址修改成功，请重新启动设备生效修改ip配置!");
            }

            model_ = model;

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButton_dhcp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FormSetIP_Load(object sender, EventArgs e)
        {
            if(model_.dhcp)
            {
                radioButton_dhcp.Checked = true;
                radioButton_fixed.Checked = false;
            }
            else
            {
                radioButton_dhcp.Checked = false;
                radioButton_fixed.Checked = true;
            }

            ipAddressControl_ipaddr.Text = model_.ip;
            ipAddressControl_maskaddr.Text = model_.subnet_mask;
            ipAddressControl_gateway.Text = model_.gateway;

            //checkBox1.Checked = model_.persist;
        }
    }
}
