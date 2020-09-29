using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace demo_splitter
{



    public partial class Form1 : Form
    {
        public bool MoveFlag = false;
        int xPos;
        int yPos;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            MoveFlag = true;
            xPos = e.X;//当前x坐标.
            yPos = e.Y;//当前y坐标.
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            MoveFlag = false;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (MoveFlag)
            {
                pictureBox3.Left += Convert.ToInt16(e.X - xPos);//设置x坐标.
                pictureBox3.Top += Convert.ToInt16(e.Y - yPos);//设置y坐标.
                //
                //test
            }
        }
    }
}
