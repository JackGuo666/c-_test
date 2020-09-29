using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace form_layout
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Timer timer1 = new Timer();

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            timer1.Start();
            timer1.Tick += timer_Tick;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            foreach(var item in tableLayoutPanel1.Controls)
            {
                if(item is Button)
                {
                    Button button = (Button)item;
                    if(button == button1)
                    {
                        button1.BackColor = Color.Blue;
                    }
                }
            }
        }
    }
}
