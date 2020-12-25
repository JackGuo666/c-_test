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
    public partial class UserControlPwm : UserControl
    {
        List<string> pulseList = new List<string>();
        public UserControlPwm(string name)
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
                comboBox3.Items.Add(str);
                comboBox4.Items.Add(str);
                comboBox5.Items.Add(str);
                comboBox6.Items.Add(str);
            }

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 2;
            comboBox4.SelectedIndex = 3;
            comboBox5.SelectedIndex = 4;
            comboBox6.SelectedIndex = 5;
        }
    }
}
