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

namespace LocalPLC.Base
{
    public partial class PlcType : UserControl
    {
        bool pic2Selected = false;
        bool pic3Selected = false;

        Dictionary<string, pictest> picArray = new Dictionary<string, pictest>();
        private SplitContainer split = null;
        public delegate void DoSomethingEventHandler(string s1);
        DoSomethingEventHandler myDelegate = null;
        //从base xml读取内容
        DataManageBase dataManage_ = null;
        public PlcType(SplitContainer splitContainer, UserControlBase userBase
            , DataManageBase dataManage) 
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
            pictest4.Parent = pictureBox1;

            //key value
            //com1 comobject 从配置文件读
            picArray.Add("本体COM1", pictest3);
            picArray.Add("本体ETH1", pictest4);
            picArray.Add("DO", pictest1);
            picArray.Add("DI", pictest2);

            initDIDO();
        }


        void initDIDO()
        {
            //dataManage_.dicBiffield
            //di
            dout.initData();

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

        public void setDOInfo(string name)
        {
            //pic2Selected = true;
            //pic3Selected = false;

            //pictureBox2_MouseDoubleClick(null, null);

            //if (!split.Panel2.Controls.Contains(dout))
            {
                split.Panel2.Controls.Clear();
                dout.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(dout);

                setShow(name, picArray);
            }
        }

        private UserControlDI di = new UserControlDI(null);
        public void setDIInfo(string name)
        {
            //pic2Selected = true;
            //pic3Selected = false;

            //pictureBox3_MouseDoubleClick(null, null);

            UserControlDI di = new UserControlDI(name);

            split.Panel2.Controls.Clear();
            di.Dock = DockStyle.Fill;
            split.Panel2.Controls.Add(di);

            setShow(name, picArray);
        }

        //name就是key，本体COM1，本体COM2等
        public void setCOMInfo(string name)
        {
            UserControlCom com = new UserControlCom(name);
            //if (!split.Panel2.Controls.Contains(com))
            {
                split.Panel2.Controls.Clear();
                com.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(com);

                setShow(name, picArray);

                //if(picArray.ContainsKey(name))
                //{
                //    picArray[name].SetAllFlagFalse();
                //    picArray[name].SetSelectedFlag(true);
                //    picArray[name].Refresh();
                //}
            }
        }


        bool setShow(string name, Dictionary<string, pictest> picArray)
        {
            bool show = false;
            foreach (var pic in picArray)
            {
                if(pic.Key == name)
                {
                    picArray[name].SetAllFlagFalse();
                    picArray[name].SetSelectedFlag(true);
                    picArray[name].Invalidate();

                    show = true;
                }
                else
                {
                    picArray[pic.Key].SetAllFlagFalse();
                    picArray[pic.Key].Invalidate();
                }
            }

            return show;
        }

        public void setETHInfo(string name)
        {
            UserControlEth eth = new UserControlEth(name);
            //if (!split.Panel2.Controls.Contains(com))
            {
                split.Panel2.Controls.Clear();
                eth.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(eth);

                setShow(name, picArray);

                //if (picArray.ContainsKey(name))
                //{
                //    picArray[name].SetAllFlagFalse();
                //    picArray[name].SetSelectedFlag(true);
                //    picArray[name].Invalidate();
                //}
            }
        }

        public void setQuadInfo(string name)
        {
            UserControlQuad eth = new UserControlQuad(name);
            //if (!split.Panel2.Controls.Contains(com))
            {
                split.Panel2.Controls.Clear();
                eth.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(eth);

                setShow(name, picArray);
            }
        }

        public void setBiDirPulseInfo(string name)
        {
            UserControlBidirPulse bi = new UserControlBidirPulse(name);
            //if (!split.Panel2.Controls.Contains(com))
            {
                split.Panel2.Controls.Clear();
                bi.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(bi);

                setShow(name, picArray);
            }
        }

        public void setSinglePulseInfo(string name)
        {
            UserControlSinglePulse pulse = new UserControlSinglePulse(name);
            //if (!split.Panel2.Controls.Contains(com))
            {
                split.Panel2.Controls.Clear();
                pulse.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(pulse);

                setShow(name, picArray);
            }
        }

        public void setPTOInfo(string name)
        {
            UserControlPto pulse = new UserControlPto(name);
            //if (!split.Panel2.Controls.Contains(com))
            {
                split.Panel2.Controls.Clear();
                pulse.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(pulse);

                setShow(name, picArray);
            }
        }

        public void setPWMInfo(string name)
        {
            UserControlPwm pulse = new UserControlPwm(name);
            //if (!split.Panel2.Controls.Contains(com))
            {
                split.Panel2.Controls.Clear();
                pulse.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(pulse);

                setShow(name, picArray);
            }
        }

        public void setExtendAIInfo(string name)
        {
            UserControlExtendAI ai = new UserControlExtendAI();
            //if (!split.Panel2.Controls.Contains(com))
            {
                split.Panel2.Controls.Clear();
                ai.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(ai);

                setShow(name, picArray);
            }
        }

        public void setExtendAOInfo(string name)
        {
            UserControlExtendAO ao = new UserControlExtendAO();
            //if (!split.Panel2.Controls.Contains(com))
            {
                split.Panel2.Controls.Clear();
                ao.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(ao);

                setShow(name, picArray);
            }
        }

        public void setHighInputInfo(string name)
        {
            UserControlHighIn hi = new UserControlHighIn();
            {
                split.Panel2.Controls.Clear();
                hi.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(hi);

                setShow(name, picArray);
            }
        }


        public void setHighOutputInfo(string name)
        {
            UserControlHighOutput hout = new UserControlHighOutput();
            {
                split.Panel2.Controls.Clear();
                hout.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(hout);

                setShow(name, picArray);
            }
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
        private UserControlDO dout = new UserControlDO(null);
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


        private void pictest2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!split.Panel2.Controls.Contains(di))
            {
                split.Panel2.Controls.Clear();
                di.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(di);
            }

            myDelegate(ConstVariable.DI);
        }

        //显示串口信息
        UserControlCom com = new UserControlCom(null);
        private void pictest3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!split.Panel2.Controls.Contains(com))
            {
                split.Panel2.Controls.Clear();
                com.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(com);
            }

            //从配置文件读取的值
            myDelegate("本体COM1");
        }

        //网口信息
        UserControlEth eth = new UserControlEth(null);
        private void pictest4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!split.Panel2.Controls.Contains(eth))
            {
                split.Panel2.Controls.Clear();
                eth.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(eth);
            }

            //从配置文件读取的值
            myDelegate("本体ETH1");
        }
    }
}
