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

            pictest1.Parent = pictureBox1;
            pictest2.Parent = pictureBox1;
            pictest3.Parent = pictureBox1;
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

                Pen pen = new Pen(Color.DodgerBlue, 8);
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



        private void pictureBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pic2Selected = true;
            pic3Selected = false;

            if(!split.Panel2.Controls.Contains(dout))
            {
                split.Panel2.Controls.Clear();
                dout.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(dout);

                Refresh();
            }

            myDelegate(ConstVariable.DO);

        }

        public void setDOInfo()
        {
            pic2Selected = true;
            pic3Selected = false;

            pictureBox2_MouseDoubleClick(null, null);
        }

        public void setDIInfo()
        {
            pic2Selected = true;
            pic3Selected = false;

            pictureBox3_MouseDoubleClick(null, null);
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


        private void pictureBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pic3Selected = true;
            pic2Selected = false;

            if (!split.Panel2.Controls.Contains(di))
            {
                split.Panel2.Controls.Clear();
                di.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(di);

                Refresh();
                //有下面一行就刷新，没有就不刷新
                picHighLighted(pictureBox3, 3);
            }

        }


        //显示设备信息
        private UserControlDevice device = new UserControlDevice();
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var v = pictureBox1;
            foreach (Control ct in v.Controls)
            {
                if (ct is pictest)
                {
                    ((pictest)ct).SetAllFlagFalse();
                    ((pictest)ct).Invalidate();
                }   
             }

            if (!split.Panel2.Controls.Contains(device))
            {
                split.Panel2.Controls.Clear();
                device.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(device);
            }


            //        pic2Selected = false;
            //pic3Selected = false;
            ////pictureBox2.Invalidate();
            ////pictureBox3.Invalidate();

            //pictureBox1.Refresh();
            ////显示设备信息
            //picHighLighted(pictureBox1, 2);


        }

        //显示DO信息
        private UserControlDO dout = new UserControlDO();
        private void pictest1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (!split.Panel2.Controls.Contains(dout))
            {
                split.Panel2.Controls.Clear();
                dout.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(dout);
            }

            myDelegate(ConstVariable.DO);
        }

        private UserControlDI di = new UserControlDI();
        private void pictest2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!split.Panel2.Controls.Contains(di))
            {
                split.Panel2.Controls.Clear();
                di.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(di);
            }
        }

        //显示串口信息
        UserControlCom com = new UserControlCom();
        private void pictest3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!split.Panel2.Controls.Contains(com))
            {
                split.Panel2.Controls.Clear();
                com.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(com);
            }
        }
    }
}
