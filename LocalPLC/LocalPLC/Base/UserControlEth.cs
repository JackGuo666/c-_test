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
        public UserControlEth()
        {
            InitializeComponent();

            var v = ipAddressControl1.IPAddress;

            var s = "192.168.0.1";
            ipAddressControl1.IPAddress = System.Net.IPAddress.Parse(s);
        }
    }
}
