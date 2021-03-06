﻿using System;
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

    public interface IWeapon
    {
        void Fire();
        void setDIInfo(string name);
        void setDOInfo(string name);
        void setHighInputInfo(string name);
        void setHighOutputInfo(string name);
        void setCOMInfo(string name);
        void setETHInfo(string eth);

        void addCommNode(TreeNode tn);

        //
        void getDataFromUI();

        void refreshData();

        void refreshDIData();
        void refreshDOData();

        void getModifyFlag();
    }

    public partial class LocalPLC24P : UserControl, IWeapon
    {
        bool pic2Selected = false;
        bool pic3Selected = false;

        Dictionary<string, pictest> picArray = new Dictionary<string, pictest>();
        private SplitContainer split = null;
        public delegate void DoSomethingEventHandler(string s1, string tagName);
        DoSomethingEventHandler myDelegate = null;
        //从base xml读取内容
        DataManageBase dataManage_ = null;

        #region
        public void Fire()
        {
            setDIInfo("DI");
        }

        public void getModifyFlag()
        {
            di.getModifyFlag();

        }
        #endregion


        //动态添加不带参数构造函数
        public LocalPLC24P()
        {

        }

        UserControlBase userBase_ = null;
        public LocalPLC24P(SplitContainer splitContainer, UserControlBase userBase
            , DataManageBase dataManage) 
        {
            try
            {
                //控制器类型
                this.Tag = 0;

                InitializeComponent();
                split = splitContainer;
                userBase_ = userBase;
                UserControl1 us1 = (UserControl1)userBase.parent_;
                myDelegate = new DoSomethingEventHandler(us1.DoSomething);

                di = new UserControlDI(us1);
                dout =  new UserControlDO(/*null, */ us1);
                hi = new UserControlHighIn(us1);
                hout = new UserControlHighOutput(us1);

                // Create the ToolTip and associate with the Form container.
                ToolTip toolTip1 = new ToolTip();
                // Set up the delays for the ToolTip.
                toolTip1.AutoPopDelay = 5000;
                toolTip1.InitialDelay = 500;
                toolTip1.ReshowDelay = 200;
                // Force the ToolTip text to be displayed whether or not the form is active.
                toolTip1.ShowAlways = true;

                //pictureBox2.Parent = pictureBox1;
                //pictureBox3.Parent = pictureBox1;

                pictest1.Parent = pictureBox1;
                toolTip1.SetToolTip(pictest1, "DO");
                pictest2.Parent = pictureBox1;
                toolTip1.SetToolTip(pictest2, "DI");

                Serial_Line_1.Parent = pictureBox1;
                Serial_Line_1.Tag = "COM1";
                // Set up the ToolTip text for the Button and Checkbox.
                toolTip1.SetToolTip(this.Serial_Line_1, Serial_Line_1.Tag.ToString());
                Ethernet_1.Parent = pictureBox1;
                Ethernet_1.Tag = "NET1";
                toolTip1.SetToolTip(Ethernet_1, Ethernet_1.Tag.ToString());

                Serial_Line_2.Parent = pictureBox1;
                Serial_Line_2.Tag = "COM2";
                toolTip1.SetToolTip(this.Serial_Line_2, Serial_Line_2.Tag.ToString());

                //key value
                //com1 comobject 从配置文件读
                picArray.Add("COM1", Serial_Line_1);
                picArray.Add("COM2", Serial_Line_2);
                picArray.Add("NET1", Ethernet_1);
                picArray.Add("DO", pictest1);
                picArray.Add("DI", pictest2);

                if(!dataManage.newControlerFlag)
                {
                    refreshData();
                }
                else
                {
                    initDIDO();
                }

            }
            catch
            {


            }
        }

        public void refreshData()
        {
            di.refreshData();
            dout.refreshData();
            hout.refreshData();
            hi.refreshData();
        }


        public void refreshDIData()
        {
            di.refreshData();
        }

        public void refreshDOData()
        {
            dout.refreshData();
        }

        void initDIDO()
        {
            //dataManage_.dicBiffield
            //di
            dout.initData();
            di.initData();
            //
            hout.initData();
            hi.initData();

            //com.

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
            //if(pic3Selected)
            //{
            //    Graphics gc = pictureBox3.CreateGraphics();
            //    //gc.DrawLine(new Pen(Color.Red, 5), 0, 0, 500, 500);

            //    Pen pen = new Pen(Color.DodgerBlue, 8);
            //    gc.DrawRectangle(pen, 0, 0, pictureBox3.Width /*- borderWidth*/, pictureBox3.Height /*- borderWidth*/);

            //    gc.Dispose();
            //    base.OnPaint(e);
            //}


            //if (pic2Selected)
            //{
            //    Graphics gc = pictureBox2.CreateGraphics();
            //    //gc.DrawLine(new Pen(Color.Red, 5), 0, 0, 500, 500);

            //    Pen pen = new Pen(Color.DodgerBlue, 3);
            //    gc.DrawRectangle(pen, 0, 0, pictureBox2.Width /*- borderWidth*/, pictureBox3.Height /*- borderWidth*/);

            //    gc.Dispose();

            //    base.OnPaint(e);
            //}

        }

        private void picHighLighted(PictureBox pic, int borderWidth)
        {
            //int borderWidth = 5;
            pic.Refresh();
            Graphics g = pic.CreateGraphics();

            Pen pen = new Pen(Color.DodgerBlue, borderWidth);
            g.DrawRectangle(pen, 0, 0, pic.Width /*- borderWidth*/, pic.Height /*- borderWidth*/);

        }

        //private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        //{
        //    picHighLighted(pictureBox2, 6);

        //    pictureBox3.Refresh();

        //    //存在标志
        //    pic3Selected = false;

        //    //显示设备信息
        //    //picHighLighted(pictureBox1, 2);
        //}

        //private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        //{
        //    picHighLighted(pictureBox2, 3);
        //}

        //private void pictureBox2_MouseEnter(object sender, EventArgs e)
        //{
        //    picHighLighted(pictureBox2, 3);
        //}

        //private void pictureBox2_MouseLeave(object sender, EventArgs e)
        //{
        //    if (pic2Selected == false)
        //    {
        //        PictureBox pic = (PictureBox)sender;
        //        pic.Refresh();
        //    }
        //}



        //private void pictureBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    pic2Selected = true;
        //    pic3Selected = false;

        //    if(!split.Panel2.Controls.Contains(dout))
        //    {
        //        split.Panel2.Controls.Clear();
        //        dout.Dock = DockStyle.Fill;
        //        split.Panel2.Controls.Add(dout);

        //        Refresh();
        //    }

        //    myDelegate(ConstVariable.DO);

        //}

        enum COLUMN_DI{ USED, VARNAME, FITERTIME, CHANNELNAME, ADDRESS, NOTE};
        enum COLUMN_DO { USED, VARNAME, CHANNELNAME, ADDRESS, NOTE };
        public void getDataFromUI()
        {
            //控制器类里DI、DO、HOUT从UI界面值传到datamanage
            var listDI = UserControlBase.dataManage.diList;

            int row = 0;
            foreach (DataRow dr in di.dtData.Rows)
            {
                listDI[row].used = bool.Parse(dr[(int)COLUMN_DI.USED].ToString());

                utility.PrintInfo(string.Format("{0} {1}", listDI[row].channelName, listDI[row].used));
                listDI[row].varName = dr[(int)COLUMN_DI.VARNAME].ToString();
                uint.TryParse(dr[(int)COLUMN_DI.FITERTIME].ToString(), out listDI[row].filterTime);
                listDI[row].channelName = dr[(int)COLUMN_DI.CHANNELNAME].ToString();
                listDI[row].address = dr[(int)COLUMN_DI.ADDRESS].ToString();
                listDI[row].note = dr[(int)COLUMN_DI.NOTE].ToString();

                row++;
            }

            var listDO = UserControlBase.dataManage.doList;
            row = 0;
            foreach (DataRow dr in dout.dtData.Rows)
            {
                listDO[row].used = bool.Parse(dr[(int)COLUMN_DO.USED].ToString());
                listDO[row].varName = dr[(int)COLUMN_DO.VARNAME].ToString();
                listDO[row].channelName = dr[(int)COLUMN_DO.CHANNELNAME].ToString();
                listDO[row].address = dr[(int)COLUMN_DO.ADDRESS].ToString();
                listDO[row].note = dr[(int)COLUMN_DO.NOTE].ToString();

                row++;
            }

            //
            di.getDataFromUI();


            row = 0;
            var hspList = UserControlBase.dataManage.hspList;
            foreach(DataRow dr in hout.dtData.Rows)
            {
                bool.TryParse(dr[(int)UserControlHighOutput.columnUsedIndex].ToString(), out hspList[row].used);
                hspList[row].name = dr[UserControlHighOutput.columnVarIndex].ToString();
                hspList[row].address = dr[UserControlHighOutput.columnAddressIndex].ToString();
                hspList[row].note = dr[UserControlHighOutput.columnNoteIndex].ToString();

                row++;
            }
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

        private UserControlDI di = null;/*new UserControlDI(null);*/
        public void setDIInfo(string name)
        {
            //pic2Selected = true;
            //pic3Selected = false;

            //pictureBox3_MouseDoubleClick(null, null);

            //UserControlDI di = new UserControlDI(name);
 //gw暂时去掉不刷新           di.refreshData();
            split.Panel2.Controls.Clear();
            di.Dock = DockStyle.Fill;
            split.Panel2.Controls.Add(di);

            setShow(name, picArray);
        }


        //name就是key，本体COM1，本体COM2等
        public void setCOMInfo(string name)
        {
            if(userBase_.comDic.ContainsKey(name))
            {
                UserControlCom com = /*new UserControlCom(name)*/ userBase_.comDic[name];
                if (!split.Panel2.Controls.Contains(com))
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
            if(userBase_.ethDic.ContainsKey(name))
            {
                UserControlEth eth = userBase_.ethDic[name]; ;
                if (!split.Panel2.Controls.Contains(eth))
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

        public void addCommNode(TreeNode tn)
        {
            
        }


        UserControlHighIn hi = /*new UserControlHighIn()*/ null;
        public void setHighInputInfo(string name)
        {

            {
                split.Panel2.Controls.Clear();
                hi.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(hi);

                setShow(name, picArray);
            }
        }

        UserControlHighOutput hout = null;
        public void setHighOutputInfo(string name)
        {

            {
                split.Panel2.Controls.Clear();
                hout.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(hout);

                setShow(name, picArray);
            }
        }

        //private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        //{
        //    picHighLighted(pictureBox3, 6);

        //    pictureBox2.Refresh();

        //    //存在标志
        //    pic2Selected = false;
        //}

        //private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        //{
        //    picHighLighted(pictureBox3, 3);
        //}

        //private void pictureBox3_MouseEnter(object sender, EventArgs e)
        //{
        //    picHighLighted(pictureBox3, 3);
        //}

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            if (pic3Selected == false)
            {
                PictureBox pic = (PictureBox)sender;
                pic.Refresh();
            }
        }


        //private void pictureBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    pic3Selected = true;
        //    pic2Selected = false;

        //    if (!split.Panel2.Controls.Contains(di))
        //    {
        //        split.Panel2.Controls.Clear();
        //        di.Dock = DockStyle.Fill;
        //        split.Panel2.Controls.Add(di);

        //        Refresh();
        //        //有下面一行就刷新，没有就不刷新
        //        picHighLighted(pictureBox3, 3);
        //    }

        //}


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
        private UserControlDO dout = null;
        private void pictest1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (!split.Panel2.Controls.Contains(dout))
            {
                split.Panel2.Controls.Clear();
                dout.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(dout);
            }

            myDelegate(ConstVariable.DO, ConstVariable.DO);
        }

        private void pictest1_Click(object sender, EventArgs e)
        {
            if (!split.Panel2.Controls.Contains(dout))
            {
                split.Panel2.Controls.Clear();
                dout.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(dout);
            }

            myDelegate(ConstVariable.DO, ConstVariable.DO);
        }


        private void pictest2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!split.Panel2.Controls.Contains(di))
            {
                split.Panel2.Controls.Clear();
                di.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(di);
            }

            myDelegate(ConstVariable.DI, ConstVariable.DI);
        }

        private void pictest2_Click(object sender, EventArgs e)
        {
            if (!split.Panel2.Controls.Contains(di))
            {
                split.Panel2.Controls.Clear();
                di.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(di);
            }

            myDelegate(ConstVariable.DI, ConstVariable.DI);
        }

        //显示串口信息
        //UserControlCom com = new UserControlCom(null);
        private void pictest3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pictest pic = (pictest)sender;
            
            if(userBase_.comDic.ContainsKey(pic.Tag.ToString()))
            {
                var com = userBase_.comDic[pic.Tag.ToString()];
                if (!split.Panel2.Controls.Contains(com))
                {
                    split.Panel2.Controls.Clear();
                    com.Dock = DockStyle.Fill;
                    split.Panel2.Controls.Add(com);
                }

                //从配置文件读取的值
                myDelegate(pic.Tag.ToString(), "SERIAL_LINE");
            }
        }

        private void Serial_Line_1_Click(object sender, EventArgs e)
        {
            pictest pic = (pictest)sender;

            if (userBase_.comDic.ContainsKey(pic.Tag.ToString()))
            {
                var com = userBase_.comDic[pic.Tag.ToString()];
                if (!split.Panel2.Controls.Contains(com))
                {
                    split.Panel2.Controls.Clear();
                    com.Dock = DockStyle.Fill;
                    split.Panel2.Controls.Add(com);
                }

                //从配置文件读取的值
                myDelegate(pic.Tag.ToString(), "SERIAL_LINE");
            }
        }

        private void Serial_Line_2_Click(object sender, EventArgs e)
        {
            pictest pic = (pictest)sender;

            if (userBase_.comDic.ContainsKey(pic.Tag.ToString()))
            {
                var com = userBase_.comDic[pic.Tag.ToString()];
                if (!split.Panel2.Controls.Contains(com))
                {
                    split.Panel2.Controls.Clear();
                    com.Dock = DockStyle.Fill;
                    split.Panel2.Controls.Add(com);
                }

                //从配置文件读取的值
                myDelegate(pic.Tag.ToString(), "SERIAL_LINE");
            }
        }

        //网口信息
        //UserControlEth eth = new UserControlEth(null);
        private void pictest4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pictest pic = (pictest)sender;
            if (userBase_.ethDic.ContainsKey(pic.Tag.ToString()))
            {
                var eth = userBase_.ethDic[pic.Tag.ToString()];
                split.Panel2.Controls.Clear();
                eth.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(eth);
            }

            //从配置文件读取的值
            myDelegate(pic.Tag.ToString(), "ETHERNET");
        }

        private void Ethernet_1_Click(object sender, EventArgs e)
        {
            pictest pic = (pictest)sender;
            if (userBase_.ethDic.ContainsKey(pic.Tag.ToString()))
            {
                var eth = userBase_.ethDic[pic.Tag.ToString()];
                split.Panel2.Controls.Clear();
                eth.Dock = DockStyle.Fill;
                split.Panel2.Controls.Add(eth);
            }

            //从配置文件读取的值
            myDelegate(pic.Tag.ToString(), "ETHERNET");
        }
    }
}
