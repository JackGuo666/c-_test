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
    public partial class UserControlPLS : UserControl
    {
        public UserControlPLS(int width)
        {
            InitializeComponent();
            groupBox1.Width = width;
            groupBox2.Width = width;
        }
    }
}
