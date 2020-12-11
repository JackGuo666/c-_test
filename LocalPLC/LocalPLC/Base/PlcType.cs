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
    public partial class PlcType : UserControl
    {
        bool pic2Selected = false;
        bool pic3Selected = false;

        private SplitContainer split = null;
        public delegate void DoSomethingEventHandler(string s1);
        DoSomethingEventHandler myDelegate = null;
        public PlcType(SplitContainer splitContainer, UserControlBase userBase) 
        {
            InitializeComponent();
            split = splitContainer;

            UserControl1 us1 = (UserControl1)userBase.Parent.Parent;
            myDelegate = new DoSomethingEventHandler(us1.DoSomething);

            pictureBox2.Parent = pictureBox1;
            pictureBox3.Parent = pictureBox1;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics gc = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.DodgerBlue, 3);
            gc.DrawRectangle(pen, 0, 0, pictureBox1.Width /*- borderWidth*/, pictureBox1.Height /*- borderWidth*/);

            gc.Dispose();
        }

        //最小化刷新重写函数
        protected override void OnPaint(PaintEventArgs e)
        {
            if(pic3Selected)
            {
                Graphics gc = pictureBox3.CreateGraphics();
                //gc.DrawLine(new Pen(Color.Red, 5), 0, 0, 500, 500);

                Pen pen = new Pen(Color.DodgerBlue, 3);
                gc.DrawRectangle(pen, 0, 0, pictureBox3.Width /*- borderWidth*/, pictureBox3.Height /*- borderWidth*/);

                gc.Dispose();
                base.OnPaint(e);
            }

            if (pic2Selected)
            {
                Graphics gc = pictureBox2.CreateGraphics();
                //gc.DrawLine(new Pen(Color.Red, 5), 0, 0, 500, 500);

                Pen pen = new Pen(Color.DodgerBlue, 3);
                gc.DrawRectangle(pen, 0, 0, pictureBox2.Width /*- borderWidth*/, pictureBox3.Height /*- borderWidth*/);

                gc.Dispose();
                base.OnPaint(e);
            }

        }

        private void picHighLighted(PictureBox pic, int borderWidth)
        {
            //int borderWidth = 5;
            pic.Refresh();
            Graphics g = pic.CreateGraphics();

            Pen pen = new Pen(Color.DodgerBlue, borderWidth);
            g.DrawRectangle(pen, 0, 0, pic.Width /*- borderWidth*/, pic.Height /*- borderWidth*/);

        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            picHighLighted(pictureBox2, 6);

            pictureBox3.Refresh();

            //存在标志
            pic3Selected = false;

            //显示设备信息
            //picHighLighted(pictureBox1, 2);
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            picHighLighted(pictureBox2, 3);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            picHighLighted(pictureBox2, 3);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            if (pic2Selected == false)
            {
                PictureBox pic = (PictureBox)sender;
                pic.Refresh();
            }
        }


        private UserControlDO dout = new UserControlDO();
        private void pictureBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pic2Selected = true;
            pic3Selected = false;

            split.Panel2.Controls.Clear();
            dout.Dock = DockStyle.Fill;
            split.Panel2.Controls.Add(dout);



            myDelegate("DO");
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            picHighLighted(pictureBox3, 6);

            pictureBox2.Refresh();

            //存在标志
            pic2Selected = false;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            picHighLighted(pictureBox3, 3);
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            picHighLighted(pictureBox3, 3);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            if (pic3Selected == false)
            {
                PictureBox pic = (PictureBox)sender;
                pic.Refresh();
            }
        }

        private UserControlDI di = new UserControlDI();
        private void pictureBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pic3Selected = true;
            pic2Selected = false;

            split.Panel2.Controls.Clear();
            di.Dock = DockStyle.Fill;
            split.Panel2.Controls.Add(di);
        }


    }
}
