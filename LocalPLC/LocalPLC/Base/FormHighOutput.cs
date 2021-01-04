using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalPLC.Base
{
    public partial class FormHighOutput : Form
    {
        public FormHighOutput()
        {
            InitializeComponent();

            comboBox3.Items.Add("未配置");
            comboBox3.Items.Add("PTO");
            comboBox3.Items.Add("PWM");
            comboBox3.Items.Add("频率发生器");


            comboBox3.TextChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentIndex = this.comboBox3.SelectedIndex;
            if (currentIndex < 0) return;

            if (currentIndex == 0)
            {
                this.panel2.Controls.Clear();
            }
            else
            {


                if (currentIndex == 1)
                {


                    UserControlPto pto = new UserControlPto("");
                    this.panel2.Controls.Clear();
                    pto.Dock = DockStyle.Fill;
                    this.panel2.Controls.Add(pto);
                }
                else if (currentIndex == 2)
                {
                    UserControlPwm pwm = new UserControlPwm("");
                    this.panel2.Controls.Clear();
                    pwm.Dock = DockStyle.Fill;
                    this.panel2.Controls.Add(pwm);
                }
                else if (currentIndex == 3)
                {
                    UserControlFreaGen freaGen = new UserControlFreaGen();
                    this.panel2.Controls.Clear();
                    freaGen.Dock = DockStyle.Fill;
                    this.panel2.Controls.Add(freaGen);
                }
            }



        }
    }
}
