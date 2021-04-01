using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using LocalPLC;

namespace LocalPLC.ModbusServer
{
    public partial class modbusserver : Form
    {
        //struct serversettings
        //{
        //   public int coils;
        //   public int holds;
        //   public int discrete;
        //   public int state;
        //   public string coilstart;
        //   public string holdstart;
        //   public string discretestart;
        //   public string statestart;
        //   public string coilend;
        //   public string holdend;
        //   public string discreteend;
        //   public string stateend;
        //   public string Icoilname;
        //   public string Iholdname;
        //   public string Idiscretename;

        //    public string Istatename;
        //    public string Ocoilname;
        //    public string Oholdname;
        //    public string Odiscretename;
        //    public string Ostatename;
        //    public string IOstartaddress;
        //   public int IOaddresslength;
        //   public int port;
        //   public int maxconnectiong;
        //   public int ip1;
        //   public int ip2;
        //   public int ip3;
        //   public int ip4;
        //   public bool transmission;//传输模式，true为TCP，false为UDP
        //    public bool IPconnect;
        //};
        private DataManager dataManager = null;
        ModbusServerData data_;
        public modbusserver server;
        private int close = 0;

        enum TRANSFORMMODE : int
        { TCP, UDP }
        enum SLAVETRANS : int
        { RTU,ASCII}
        enum IPCONNECT:int
        { FALSE,TRUE}
        
        public modbusserver(int index)
        {
            InitializeComponent();
            server = this;
            dataManager = DataManager.GetInstance();
            
        }
        private string eth;
        private int SID;
        public void getServerData(ref ModbusServerData data,int serverid)
        {
            data_ = data;
            //eth = comboBox2.Text;
            SID = serverid;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            
            if(radioButton3.Checked == true)
            {
                data_.dataDevice_.ipfixed = true;
                int connectnumber = Convert.ToInt32(textBox24.Text);
                switch(connectnumber)
                {
                    case 1:
                        { 
                            textBox25.ReadOnly = false;
                            textBox25.Enabled = true;
                            textBox26.ReadOnly = false;
                            textBox26.Enabled = true;
                            textBox27.ReadOnly = false;
                            textBox27.Enabled = true;
                            textBox28.ReadOnly = false;
                            textBox28.Enabled = true;
                            textBox25.Text = data_.dataDevice_.ip0.ToString();
                            textBox26.Text = data_.dataDevice_.ip1.ToString();
                            textBox27.Text = data_.dataDevice_.ip2.ToString();
                            textBox28.Text = data_.dataDevice_.ip3.ToString();
                        }
                        break;
                    case 2:
                        {
                            textBox25.ReadOnly = false;
                            textBox25.Enabled = true;
                            textBox26.ReadOnly = false;
                            textBox26.Enabled = true;
                            textBox27.ReadOnly = false;
                            textBox27.Enabled = true;
                            textBox28.ReadOnly = false;
                            textBox28.Enabled = true;
                            textBox9.ReadOnly = false;
                            textBox9.Enabled = true;
                            textBox10.ReadOnly = false;
                            textBox10.Enabled = true;
                            textBox12.ReadOnly = false;
                            textBox12.Enabled = true;
                            textBox11.ReadOnly = false;
                            textBox11.Enabled = true;
                            textBox25.Text = data_.dataDevice_.ip0.ToString();
                            textBox26.Text = data_.dataDevice_.ip1.ToString();
                            textBox27.Text = data_.dataDevice_.ip2.ToString();
                            textBox28.Text = data_.dataDevice_.ip3.ToString();
                            textBox9.Text = data_.dataDevice_.ip10.ToString();
                            textBox10.Text = data_.dataDevice_.ip11.ToString();
                            textBox11.Text = data_.dataDevice_.ip12.ToString();
                            textBox12.Text = data_.dataDevice_.ip13.ToString();
                        }
                        break;
                    case 3:
                        {
                            textBox25.ReadOnly = false;
                            textBox25.Enabled = true;
                            textBox26.ReadOnly = false;
                            textBox26.Enabled = true;
                            textBox27.ReadOnly = false;
                            textBox27.Enabled = true;
                            textBox28.ReadOnly = false;
                            textBox28.Enabled = true;
                            textBox9.ReadOnly = false;
                            textBox9.Enabled = true;
                            textBox10.ReadOnly = false;
                            textBox10.Enabled = true;
                            textBox12.ReadOnly = false;
                            textBox12.Enabled = true;
                            textBox11.ReadOnly = false;
                            textBox11.Enabled = true;
                            textBox13.ReadOnly = false;
                            textBox13.Enabled = true;
                            textBox14.ReadOnly = false;
                            textBox14.Enabled = true;
                            textBox15.ReadOnly = false;
                            textBox15.Enabled = true;
                            textBox16.ReadOnly = false;
                            textBox16.Enabled = true;
                            textBox25.Text = data_.dataDevice_.ip0.ToString();
                            textBox26.Text = data_.dataDevice_.ip1.ToString();
                            textBox27.Text = data_.dataDevice_.ip2.ToString();
                            textBox28.Text = data_.dataDevice_.ip3.ToString();
                            textBox9.Text = data_.dataDevice_.ip10.ToString();
                            textBox10.Text = data_.dataDevice_.ip11.ToString();
                            textBox11.Text = data_.dataDevice_.ip12.ToString();
                            textBox12.Text = data_.dataDevice_.ip13.ToString();
                            textBox13.Text = data_.dataDevice_.ip20.ToString();
                            textBox14.Text = data_.dataDevice_.ip21.ToString();
                            textBox15.Text = data_.dataDevice_.ip22.ToString();
                            textBox16.Text = data_.dataDevice_.ip23.ToString();
                        }
                        break;
                    case 4:
                        {
                            textBox25.ReadOnly = false;
                            textBox25.Enabled = true;
                            textBox26.ReadOnly = false;
                            textBox26.Enabled = true;
                            textBox27.ReadOnly = false;
                            textBox27.Enabled = true;
                            textBox28.ReadOnly = false;
                            textBox28.Enabled = true;
                            textBox9.ReadOnly = false;
                            textBox9.Enabled = true;
                            textBox10.ReadOnly = false;
                            textBox10.Enabled = true;
                            textBox12.ReadOnly = false;
                            textBox12.Enabled = true;
                            textBox11.ReadOnly = false;
                            textBox11.Enabled = true;
                            textBox13.ReadOnly = false;
                            textBox13.Enabled = true;
                            textBox14.ReadOnly = false;
                            textBox14.Enabled = true;
                            textBox15.ReadOnly = false;
                            textBox15.Enabled = true;
                            textBox16.ReadOnly = false;
                            textBox16.Enabled = true;
                            textBox17.ReadOnly = false;
                            textBox17.Enabled = true;
                            textBox18.ReadOnly = false;
                            textBox18.Enabled = true;
                            textBox19.ReadOnly = false;
                            textBox19.Enabled = true;
                            textBox20.ReadOnly = false;
                            textBox20.Enabled = true;
                            textBox25.Text = data_.dataDevice_.ip0.ToString();
                            textBox26.Text = data_.dataDevice_.ip1.ToString();
                            textBox27.Text = data_.dataDevice_.ip2.ToString();
                            textBox28.Text = data_.dataDevice_.ip3.ToString();
                            textBox9.Text = data_.dataDevice_.ip10.ToString();
                            textBox10.Text = data_.dataDevice_.ip11.ToString();
                            textBox11.Text = data_.dataDevice_.ip12.ToString();
                            textBox12.Text = data_.dataDevice_.ip13.ToString();
                            textBox13.Text = data_.dataDevice_.ip20.ToString();
                            textBox14.Text = data_.dataDevice_.ip21.ToString();
                            textBox15.Text = data_.dataDevice_.ip22.ToString();
                            textBox16.Text = data_.dataDevice_.ip23.ToString();
                            textBox17.Text = data_.dataDevice_.ip30.ToString();
                            textBox18.Text = data_.dataDevice_.ip31.ToString();
                            textBox19.Text = data_.dataDevice_.ip32.ToString();
                            textBox20.Text = data_.dataDevice_.ip33.ToString();
                        }
                        break;
                }
                
                data_.dataDevice_.ipconnect = (int)IPCONNECT.TRUE;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                data_.dataDevice_.ipfixed = false;
                textBox25.ReadOnly = true;
                textBox25.Enabled = false;
                textBox26.ReadOnly = true;
                textBox26.Enabled = false;
                textBox27.ReadOnly = true;
                textBox27.Enabled = false;
                textBox28.ReadOnly = true;
                textBox28.Enabled = false;
                textBox9.ReadOnly = true;
                textBox9.Enabled = false;
                textBox10.ReadOnly = true;
                textBox10.Enabled = false;
                textBox11.ReadOnly = true;
                textBox11.Enabled = false;
                textBox12.ReadOnly = true;
                textBox12.Enabled = false;
                textBox13.ReadOnly = true;
                textBox13.Enabled = false;
                textBox14.ReadOnly = true;
                textBox14.Enabled = false;
                textBox15.ReadOnly = true;
                textBox15.Enabled = false;
                textBox16.ReadOnly = true;
                textBox16.Enabled = false;
                textBox17.ReadOnly = true;
                textBox17.Enabled = false;
                textBox18.ReadOnly = true;
                textBox18.Enabled = false;
                textBox19.ReadOnly = true;
                textBox19.Enabled = false;
                textBox20.ReadOnly = true;
                textBox20.Enabled = false;
                data_.dataDevice_.ipconnect = (int)IPCONNECT.FALSE;
            
            }
        }

        // serversettings ss1;
        private void button1_Click(object sender, EventArgs e)
        {


            //ss1.coils = Convert.ToInt32(textBox1.Text);
            //ss1.holds = Convert.ToInt32(textBox2.Text);
            //ss1.discrete = Convert.ToInt32(textBox3.Text);
            //ss1.state = Convert.ToInt32(textBox4.Text);
            //ss1.coilstart = textBox5.Text;
            //ss1.holdstart = textBox6.Text;
            //ss1.discretestart = textBox7.Text;
            //ss1.statestart = textBox8.Text;
            //ss1.coilend = textBox9.Text;
            //ss1.holdend = textBox10.Text;
            //ss1.discreteend = textBox11.Text;
            //ss1.holdend = textBox12.Text;
            //ss1.Icoilname = textBox13.Text;
            //ss1.Iholdname = textBox14.Text;
            //ss1.Idiscretename = textBox15.Text;
            //ss1.Istatename = textBox16.Text;
            //ss1.Ocoilname = textBox17.Text;
            //ss1.Oholdname = textBox18.Text;
            //ss1.Odiscretename = textBox19.Text;
            //ss1.Ostatename = textBox20.Text;
            //ss1.IOstartaddress = textBox21.Text;
            //ss1.IOaddresslength = Convert.ToInt32(textBox22.Text);
            //ss1.port = Convert.ToInt32(textBox23.Text);
            //ss1.maxconnectiong = Convert.ToInt32(textBox24.Text);
            //ss1.ip1 = Convert.ToInt32(textBox25.Text);
            //ss1.ip2 = Convert.ToInt32(textBox25.Text);
            //ss1.ip3 = Convert.ToInt32(textBox26.Text);
            //ss1.ip4 = Convert.ToInt32(textBox27.Text);
            int.TryParse(textBox1.Text, out data_.dataDevice_.coilCount);
            textBox5.Text = textBox1.Text;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                data_.dataDevice_.transformMode = (int)TRANSFORMMODE.TCP;
            }
            else if (radioButton2.Checked == true)
            {
                data_.dataDevice_.transformMode = (int)TRANSFORMMODE.UDP;
            }
        }

        private void modbusserver_Load(object sender, EventArgs e)
        {
            //dataManager.listServer[0]
            textBox1.Text = dataManager.listServer[0].dataDevice_.coilCount.ToString();
            textBox2.Text = dataManager.listServer[0].dataDevice_.holdingCount.ToString();
            textBox3.Text = dataManager.listServer[0].dataDevice_.decreteCount.ToString();
            textBox4.Text = dataManager.listServer[0].dataDevice_.statusCount.ToString();
            //comboBox1.SelectedItem = data_.dataDevice_.transform;
            if (data_.dataDevice_.transformMode == (int)TRANSFORMMODE.TCP)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            if (data_.dataDevice_.slavetansformMode == (int)SLAVETRANS.RTU)
            {
                radioButton6.Checked = true;
            }
            else
            {
                radioButton5.Checked = true;
            }
            textBox23.Text = data_.dataDevice_.port.ToString();
            textBox24.Text = data_.dataDevice_.maxconnectnumber.ToString();
            textBox22.ReadOnly = true;
            textBox25.ReadOnly = true;
            textBox26.ReadOnly = true;
            textBox27.ReadOnly = true;
            textBox28.ReadOnly = true;
            textBox25.ReadOnly = true;
            textBox26.ReadOnly = true;
            textBox27.ReadOnly = true;
            textBox28.ReadOnly = true;
            textBox9.ReadOnly = true;
            textBox10.ReadOnly = true;
            textBox11.ReadOnly = true;
            textBox12.ReadOnly = true;
            textBox13.ReadOnly = true;
            textBox14.ReadOnly = true;
            textBox15.ReadOnly = true;
            textBox16.ReadOnly = true;
            textBox17.ReadOnly = true;
            textBox18.ReadOnly = true;
            textBox19.ReadOnly = true;
            textBox20.ReadOnly = true;
            textBox5.Text = dataManager.listServer[0].dataDevice_.coilIoAddrStart;
            textBox6.Text = dataManager.listServer[0].dataDevice_.holdingIoAddrStart;
            textBox7.Text = dataManager.listServer[0].dataDevice_.decreteIoAddrStart;
            textBox8.Text = dataManager.listServer[0].dataDevice_.statusIoAddrStart;

            textBox21.Text = dataManager.listServer[0].serverstartaddr.ToString();
            textBox21.ReadOnly = true;
            textBox22.Text = dataManager.listServer[0].dataDevice_.IOAddrLength.ToString();
            
            if (data_.dataDevice_.ipfixed == true)
            {
                
                radioButton3.Checked = true;
                radioButton4.Checked = false;
                int connectnumber = data_.dataDevice_.maxconnectnumber;
                if (connectnumber == 1)
                {
                    textBox25.ReadOnly = false;
                    textBox25.Enabled = true;
                    textBox26.ReadOnly = false;
                    textBox26.Enabled = true;
                    textBox27.ReadOnly = false;
                    textBox27.Enabled = true;
                    textBox28.ReadOnly = false;
                    textBox28.Enabled = true;
                    textBox25.Text = data_.dataDevice_.ip0.ToString();
                    textBox26.Text = data_.dataDevice_.ip1.ToString();
                    textBox27.Text = data_.dataDevice_.ip2.ToString();
                    textBox28.Text = data_.dataDevice_.ip3.ToString();
                }
                else if (connectnumber == 2)
                {
                    textBox25.ReadOnly = false;
                    textBox25.Enabled = true;
                    textBox26.ReadOnly = false;
                    textBox26.Enabled = true;
                    textBox27.ReadOnly = false;
                    textBox27.Enabled = true;
                    textBox28.ReadOnly = false;
                    textBox28.Enabled = true;
                    textBox9.ReadOnly = false;
                    textBox9.Enabled = true;
                    textBox10.ReadOnly = false;
                    textBox10.Enabled = true;
                    textBox12.ReadOnly = false;
                    textBox12.Enabled = true;
                    textBox11.ReadOnly = false;
                    textBox11.Enabled = true;
                    textBox25.Text = data_.dataDevice_.ip0.ToString();
                    textBox26.Text = data_.dataDevice_.ip1.ToString();
                    textBox27.Text = data_.dataDevice_.ip2.ToString();
                    textBox28.Text = data_.dataDevice_.ip3.ToString();
                    textBox9.Text = data_.dataDevice_.ip10.ToString();
                    textBox10.Text = data_.dataDevice_.ip11.ToString();
                    textBox11.Text = data_.dataDevice_.ip12.ToString();
                    textBox12.Text = data_.dataDevice_.ip13.ToString();
                }
                else if (connectnumber == 3)
                {
                    textBox25.ReadOnly = false;
                    textBox25.Enabled = true;
                    textBox26.ReadOnly = false;
                    textBox26.Enabled = true;
                    textBox27.ReadOnly = false;
                    textBox27.Enabled = true;
                    textBox28.ReadOnly = false;
                    textBox28.Enabled = true;
                    textBox9.ReadOnly = false;
                    textBox9.Enabled = true;
                    textBox10.ReadOnly = false;
                    textBox10.Enabled = true;
                    textBox12.ReadOnly = false;
                    textBox12.Enabled = true;
                    textBox11.ReadOnly = false;
                    textBox11.Enabled = true;
                    textBox13.ReadOnly = false;
                    textBox13.Enabled = true;
                    textBox14.ReadOnly = false;
                    textBox14.Enabled = true;
                    textBox15.ReadOnly = false;
                    textBox15.Enabled = true;
                    textBox16.ReadOnly = false;
                    textBox16.Enabled = true;
                    textBox25.Text = data_.dataDevice_.ip0.ToString();
                    textBox26.Text = data_.dataDevice_.ip1.ToString();
                    textBox27.Text = data_.dataDevice_.ip2.ToString();
                    textBox28.Text = data_.dataDevice_.ip3.ToString();
                    textBox9.Text = data_.dataDevice_.ip10.ToString();
                    textBox10.Text = data_.dataDevice_.ip11.ToString();
                    textBox11.Text = data_.dataDevice_.ip12.ToString();
                    textBox12.Text = data_.dataDevice_.ip13.ToString();
                    textBox13.Text = data_.dataDevice_.ip20.ToString();
                    textBox14.Text = data_.dataDevice_.ip21.ToString();
                    textBox15.Text = data_.dataDevice_.ip22.ToString();
                    textBox16.Text = data_.dataDevice_.ip23.ToString();
                }
                else if (connectnumber == 4)
                {
                    textBox25.ReadOnly = false;
                    textBox25.Enabled = true;
                    textBox26.ReadOnly = false;
                    textBox26.Enabled = true;
                    textBox27.ReadOnly = false;
                    textBox27.Enabled = true;
                    textBox28.ReadOnly = false;
                    textBox28.Enabled = true;
                    textBox9.ReadOnly = false;
                    textBox9.Enabled = true;
                    textBox10.ReadOnly = false;
                    textBox10.Enabled = true;
                    textBox12.ReadOnly = false;
                    textBox12.Enabled = true;
                    textBox11.ReadOnly = false;
                    textBox11.Enabled = true;
                    textBox13.ReadOnly = false;
                    textBox13.Enabled = true;
                    textBox14.ReadOnly = false;
                    textBox14.Enabled = true;
                    textBox15.ReadOnly = false;
                    textBox15.Enabled = true;
                    textBox16.ReadOnly = false;
                    textBox16.Enabled = true;
                    textBox17.ReadOnly = false;
                    textBox17.Enabled = true;
                    textBox18.ReadOnly = false;
                    textBox18.Enabled = true;
                    textBox19.ReadOnly = false;
                    textBox19.Enabled = true;
                    textBox20.ReadOnly = false;
                    textBox20.Enabled = true;
                    textBox25.Text = data_.dataDevice_.ip0.ToString();
                    textBox26.Text = data_.dataDevice_.ip1.ToString();
                    textBox27.Text = data_.dataDevice_.ip2.ToString();
                    textBox28.Text = data_.dataDevice_.ip3.ToString();
                    textBox9.Text = data_.dataDevice_.ip10.ToString();
                    textBox10.Text = data_.dataDevice_.ip11.ToString();
                    textBox11.Text = data_.dataDevice_.ip12.ToString();
                    textBox12.Text = data_.dataDevice_.ip13.ToString();
                    textBox13.Text = data_.dataDevice_.ip20.ToString();
                    textBox14.Text = data_.dataDevice_.ip21.ToString();
                    textBox15.Text = data_.dataDevice_.ip22.ToString();
                    textBox16.Text = data_.dataDevice_.ip23.ToString();
                    textBox17.Text = data_.dataDevice_.ip30.ToString();
                    textBox18.Text = data_.dataDevice_.ip31.ToString();
                    textBox19.Text = data_.dataDevice_.ip32.ToString();
                    textBox20.Text = data_.dataDevice_.ip33.ToString();
                }
    }
                else if (data_.dataDevice_.ipfixed == false)
                {
                    radioButton4.Checked = true;
                    radioButton3.Checked = false;
                    textBox25.ReadOnly = true;
                    textBox25.Enabled = false;
                    textBox26.ReadOnly = true;
                    textBox26.Enabled = false;
                    textBox27.ReadOnly = true;
                    textBox27.Enabled = false;
                    textBox28.ReadOnly = true;
                    textBox28.Enabled = false;
                }

            //textBox13.ReadOnly = true;
            //textBox13.Enabled = false;
            //textBox14.ReadOnly = true;
            //textBox14.Enabled = false;
            //textBox15.ReadOnly = true;
            //textBox15.Enabled = false;
            //textBox16.ReadOnly = true;
            //textBox16.Enabled = false;
            //for (int i = 0; i < dataManager.listServer.Count; i++)
            //{
            //    if (data_.dataDevice_.deviceAddr == dataManager.listServer[i].dataDevice_.deviceAddr && data_.ID != dataManager.listServer[i].ID)
            //    {
            //        data_.dataDevice_.deviceAddr++;
            //    }
            //}
            data_.dataDevice_.deviceAddr = data_.ID+1;
            textBox30.Text = data_.dataDevice_.deviceAddr.ToString();
            comboBox1.SelectedIndex = data_.dataDevice_.transform;
            comboBox2.SelectedIndex = data_.dataDevice_.transformport;
            textBox21.ReadOnly = true;
            textBox22.ReadOnly = true;
            textBox29.ReadOnly = true;
            textBox22.Text = dataManager.listServer[0].dataDevice_.IOAddrLength.ToString();
            textBox29.Text = dataManager.listServer[0].dataDevice_.shmlength.ToString();
            if(data_.dataDevice_.port == 0)
            data_.dataDevice_.port = 502 + data_.ID;
            textBox23.Text = data_.dataDevice_.port.ToString();

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox1.Text, out data_.dataDevice_.coilCount);
            //textBox5.Text = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox2.Text, out data_.dataDevice_.holdingCount);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //if(Convert.ToInt32( textBox3.Text) <0 || Convert.ToInt32(textBox3.Text) > 8000)
            //{
            //    MessageBox.Show("超出范围");
            //    textBox3.Text = data_.dataDevice_.decreteCount.ToString();
            //}
            //else
            //{
            //    data_.dataDevice_.IOAddrLength -= data_.dataDevice_.decreteCount / 8 + 1;
            //    int.TryParse(textBox3.Text, out data_.dataDevice_.decreteCount);
            //    data_.dataDevice_.IOAddrLength += data_.dataDevice_.decreteCount / 8 + 1;
            //    textBox22.Text = data_.dataDevice_.IOAddrLength.ToString();
            //}
           
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(textBox4.Text) < 0 || Convert.ToInt32(textBox4.Text) > 500)
            //{
            //    MessageBox.Show("超出范围");
            //    textBox4.Text = data_.dataDevice_.decreteCount.ToString();
            //}
            //else
            //{
            //    data_.dataDevice_.IOAddrLength -= data_.dataDevice_.statusCount*2;
            //    int.TryParse(textBox4.Text, out data_.dataDevice_.statusCount);
            //    data_.dataDevice_.IOAddrLength += data_.dataDevice_.statusCount * 2;
            //    textBox22.Text = data_.dataDevice_.IOAddrLength.ToString();
            //}
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                data_.dataDevice_.transformMode = (int)TRANSFORMMODE.TCP;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                data_.dataDevice_.transformMode = (int)TRANSFORMMODE.UDP;
            }
        }
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true)
            {
                data_.dataDevice_.slavetansformMode = (int)TRANSFORMMODE.TCP;
            }
        }
        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox23.Text, out data_.dataDevice_.port);
            int flag = 0;
            bool number = isNumber(textBox23.Text);
            if (number == true)
            {
                int port = Convert.ToInt32(textBox23.Text);
                for(int i = 0;i<dataManager.listServer.Count;i++)
                {
                    if (port == dataManager.listServer[i].dataDevice_.port && i!=SID && port != 0)
                    {
                        flag++;
                    }
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox23.Text = data_.dataDevice_.port.ToString();
            }
            if(flag != 0)
            {
                MessageBox.Show("端口号有重复，请检查");
                textBox23.Text = data_.dataDevice_.port.ToString();
            }
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool number = isNumber(textBox24.Text);
                if (radioButton3.Checked == true && number == true)
                {
                    int connectnumber = Convert.ToInt32(textBox24.Text);
                    switch (connectnumber)
                    {
                        case 1:
                            {
                                textBox25.ReadOnly = false;
                                textBox25.Enabled = true;
                                textBox26.ReadOnly = false;
                                textBox26.Enabled = true;
                                textBox27.ReadOnly = false;
                                textBox27.Enabled = true;
                                textBox28.ReadOnly = false;
                                textBox28.Enabled = true;
                                textBox9.ReadOnly = true;
                                textBox9.Enabled = false;
                                textBox10.ReadOnly = true;
                                textBox10.Enabled = false;
                                textBox12.ReadOnly = true;
                                textBox12.Enabled = false;
                                textBox11.ReadOnly = true;
                                textBox11.Enabled = false;
                                textBox13.ReadOnly = true;
                                textBox13.Enabled = false;
                                textBox14.ReadOnly = true;
                                textBox14.Enabled = false;
                                textBox15.ReadOnly = true;
                                textBox15.Enabled = false;
                                textBox16.ReadOnly = true;
                                textBox16.Enabled = false;
                                textBox17.ReadOnly = true;
                                textBox17.Enabled = false;
                                textBox18.ReadOnly = true;
                                textBox18.Enabled = false;
                                textBox19.ReadOnly = true;
                                textBox19.Enabled = false;
                                textBox20.ReadOnly = true;
                                textBox20.Enabled = false;
                                textBox25.Text = data_.dataDevice_.ip0.ToString();
                                textBox26.Text = data_.dataDevice_.ip1.ToString();
                                textBox27.Text = data_.dataDevice_.ip2.ToString();
                                textBox28.Text = data_.dataDevice_.ip3.ToString();
                            }
                            break;
                        case 2:
                            {
                                textBox25.ReadOnly = false;
                                textBox25.Enabled = true;
                                textBox26.ReadOnly = false;
                                textBox26.Enabled = true;
                                textBox27.ReadOnly = false;
                                textBox27.Enabled = true;
                                textBox28.ReadOnly = false;
                                textBox28.Enabled = true;
                                textBox9.ReadOnly = false;
                                textBox9.Enabled = true;
                                textBox10.ReadOnly = false;
                                textBox10.Enabled = true;
                                textBox12.ReadOnly = false;
                                textBox12.Enabled = true;
                                textBox11.ReadOnly = false;
                                textBox11.Enabled = true;
                                textBox13.ReadOnly = true;
                                textBox13.Enabled = false;
                                textBox14.ReadOnly = true;
                                textBox14.Enabled = false;
                                textBox15.ReadOnly = true;
                                textBox15.Enabled = false;
                                textBox16.ReadOnly = true;
                                textBox16.Enabled = false;
                                textBox17.ReadOnly = true;
                                textBox17.Enabled = false;
                                textBox18.ReadOnly = true;
                                textBox18.Enabled = false;
                                textBox19.ReadOnly = true;
                                textBox19.Enabled = false;
                                textBox20.ReadOnly = true;
                                textBox20.Enabled = false;
                                textBox25.Text = data_.dataDevice_.ip0.ToString();
                                textBox26.Text = data_.dataDevice_.ip1.ToString();
                                textBox27.Text = data_.dataDevice_.ip2.ToString();
                                textBox28.Text = data_.dataDevice_.ip3.ToString();
                                textBox9.Text = data_.dataDevice_.ip10.ToString();
                                textBox10.Text = data_.dataDevice_.ip11.ToString();
                                textBox11.Text = data_.dataDevice_.ip12.ToString();
                                textBox12.Text = data_.dataDevice_.ip13.ToString();
                            }
                            break;
                        case 3:
                            {
                                textBox25.ReadOnly = false;
                                textBox25.Enabled = true;
                                textBox26.ReadOnly = false;
                                textBox26.Enabled = true;
                                textBox27.ReadOnly = false;
                                textBox27.Enabled = true;
                                textBox28.ReadOnly = false;
                                textBox28.Enabled = true;
                                textBox9.ReadOnly = false;
                                textBox9.Enabled = true;
                                textBox10.ReadOnly = false;
                                textBox10.Enabled = true;
                                textBox12.ReadOnly = false;
                                textBox12.Enabled = true;
                                textBox11.ReadOnly = false;
                                textBox11.Enabled = true;
                                textBox13.ReadOnly = false;
                                textBox13.Enabled = true;
                                textBox14.ReadOnly = false;
                                textBox14.Enabled = true;
                                textBox15.ReadOnly = false;
                                textBox15.Enabled = true;
                                textBox16.ReadOnly = false;
                                textBox16.Enabled = true;
                                textBox17.ReadOnly = true;
                                textBox17.Enabled = false;
                                textBox18.ReadOnly = true;
                                textBox18.Enabled = false;
                                textBox19.ReadOnly = true;
                                textBox19.Enabled = false;
                                textBox20.ReadOnly = true;
                                textBox20.Enabled = false;
                                textBox25.Text = data_.dataDevice_.ip0.ToString();
                                textBox26.Text = data_.dataDevice_.ip1.ToString();
                                textBox27.Text = data_.dataDevice_.ip2.ToString();
                                textBox28.Text = data_.dataDevice_.ip3.ToString();
                                textBox9.Text = data_.dataDevice_.ip10.ToString();
                                textBox10.Text = data_.dataDevice_.ip11.ToString();
                                textBox11.Text = data_.dataDevice_.ip12.ToString();
                                textBox12.Text = data_.dataDevice_.ip13.ToString();
                                textBox13.Text = data_.dataDevice_.ip20.ToString();
                                textBox14.Text = data_.dataDevice_.ip21.ToString();
                                textBox15.Text = data_.dataDevice_.ip22.ToString();
                                textBox16.Text = data_.dataDevice_.ip23.ToString();
                            }
                            break;
                        case 4:
                            {
                                textBox25.ReadOnly = false;
                                textBox25.Enabled = true;
                                textBox26.ReadOnly = false;
                                textBox26.Enabled = true;
                                textBox27.ReadOnly = false;
                                textBox27.Enabled = true;
                                textBox28.ReadOnly = false;
                                textBox28.Enabled = true;
                                textBox9.ReadOnly = false;
                                textBox9.Enabled = true;
                                textBox10.ReadOnly = false;
                                textBox10.Enabled = true;
                                textBox12.ReadOnly = false;
                                textBox12.Enabled = true;
                                textBox11.ReadOnly = false;
                                textBox11.Enabled = true;
                                textBox13.ReadOnly = false;
                                textBox13.Enabled = true;
                                textBox14.ReadOnly = false;
                                textBox14.Enabled = true;
                                textBox15.ReadOnly = false;
                                textBox15.Enabled = true;
                                textBox16.ReadOnly = false;
                                textBox16.Enabled = true;
                                textBox17.ReadOnly = false;
                                textBox17.Enabled = true;
                                textBox18.ReadOnly = false;
                                textBox18.Enabled = true;
                                textBox19.ReadOnly = false;
                                textBox19.Enabled = true;
                                textBox20.ReadOnly = false;
                                textBox20.Enabled = true;
                                textBox25.Text = data_.dataDevice_.ip0.ToString();
                                textBox26.Text = data_.dataDevice_.ip1.ToString();
                                textBox27.Text = data_.dataDevice_.ip2.ToString();
                                textBox28.Text = data_.dataDevice_.ip3.ToString();
                                textBox9.Text = data_.dataDevice_.ip10.ToString();
                                textBox10.Text = data_.dataDevice_.ip11.ToString();
                                textBox11.Text = data_.dataDevice_.ip12.ToString();
                                textBox12.Text = data_.dataDevice_.ip13.ToString();
                                textBox13.Text = data_.dataDevice_.ip20.ToString();
                                textBox14.Text = data_.dataDevice_.ip21.ToString();
                                textBox15.Text = data_.dataDevice_.ip22.ToString();
                                textBox16.Text = data_.dataDevice_.ip23.ToString();
                                textBox17.Text = data_.dataDevice_.ip30.ToString();
                                textBox18.Text = data_.dataDevice_.ip31.ToString();
                                textBox19.Text = data_.dataDevice_.ip32.ToString();
                                textBox20.Text = data_.dataDevice_.ip33.ToString();
                            }
                            break;
                    }
                }
                if (Convert.ToInt32(textBox24.Text) < 1 || Convert.ToInt32(textBox24.Text) > 4)
                {
                    MessageBox.Show("超出范围");
                    textBox24.Text = data_.dataDevice_.maxconnectnumber.ToString();
                }
                else
                {
                    //int.TryParse(textBox24.Text, out data_.dataDevice_.maxconnectnumber);
                }
            }
            catch
            {
                return;
            }
         }
        
        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox25.Text, out data_.dataDevice_.ip0);
            
        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox26.Text, out data_.dataDevice_.ip1);
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox27.Text, out data_.dataDevice_.ip2);
        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox28.Text, out data_.dataDevice_.ip3);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //data_.dataDevice_.coilIoAddrStart = textBox5.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //data_.dataDevice_.holdingIoAddrStart = textBox6.Text;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //data_.dataDevice_.decreteIoAddrStart = textBox7.Text;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            //data_.dataDevice_.statusIoAddrStart = textBox8.Text;
        }

        

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            //data_.dataDevice_.IOAddrLength = Convert.ToInt32( textBox22.Text);
            close = 0;
            if (Convert.ToInt32( textBox22.Text ) > 1000 )
            {
                MessageBox.Show("长度超出范围，请重新设置");
                textBox22.BackColor = Color.Red;
                data_.dataDevice_.isready = false;
                close = 1;
            }
            else
            {
                textBox22.BackColor = textBox21.BackColor;
                data_.dataDevice_.isready = true;
                close = 0;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocalPLC.Base.xml.DataManageBase baseData = null;
            UserControl1.UC.getDataManager(ref baseData);
            //串口
            if (comboBox1.SelectedIndex == 0)
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton5.Enabled = true;
                radioButton6.Enabled = true;
                panel6.Visible = false;
                textBox30.Enabled = true;
                textBox30.ReadOnly = false;
                //data_.dataDevice_.transform = 0;
                comboBox2.Items.Clear();
                foreach (string serialname in baseData.serialDic.Keys)
                {
                    comboBox2.Items.Add(serialname);

                }
                comboBox2.SelectedIndex = -1;
                comboBox2.Text = null;
                textBox30_Leave(sender, e);
               
            }
            //网口
            else if (comboBox1.SelectedIndex == 1)
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton5.Enabled = false;
                radioButton6.Enabled = false;
                panel6.Visible = true;
                textBox30.Enabled = false;
                textBox30.ReadOnly = true;
                //data_.dataDevice_.transform = 1;
                comboBox2.Text = null;
                comboBox2.Items.Clear();
                foreach (string ethname in baseData.ethernetDic.Keys)
                {
                    comboBox2.Items.Add(ethname);

                }
                comboBox2.SelectedIndex = -1;
               
            }
        }

        private void textBox4_MouseLeave(object sender, EventArgs e)
        {
            
            //if (Convert.ToInt32(textBox4.Text) < 0 || Convert.ToInt32(textBox4.Text) > 500)
            //{
            //    MessageBox.Show("超出范围");
            //    textBox4.Text = data_.dataDevice_.decreteCount.ToString();
            //}
            //else
            //{
            //    data_.dataDevice_.IOAddrLength -= data_.dataDevice_.statusCount * 2;
            //    int.TryParse(textBox4.Text, out data_.dataDevice_.statusCount);
            //    data_.dataDevice_.IOAddrLength += data_.dataDevice_.statusCount * 2;
            //    textBox22.Text = data_.dataDevice_.IOAddrLength.ToString();
            //}
        }

     

        private void textBox3_Validated(object sender, EventArgs e)
        {
            //bool number = isNumber(textBox3.Text);
            //if (number == true)
            //{
            //    if (Convert.ToInt32(textBox3.Text) < 0 || Convert.ToInt32(textBox3.Text) > 8000)
            //    {
            //        MessageBox.Show("超出范围");
            //        textBox3.Text = data_.dataDevice_.decreteCount.ToString();
            //    }
            //    else
            //    {
            //        data_.dataDevice_.IOAddrLength -= data_.dataDevice_.decreteCount / 8 + 1;
            //        int.TryParse(textBox3.Text, out data_.dataDevice_.decreteCount);
            //        data_.dataDevice_.IOAddrLength += data_.dataDevice_.decreteCount / 8 + 1;
            //        textBox22.Text = data_.dataDevice_.IOAddrLength.ToString();
            //    }
            //}
            //else
            //{
            //    data_.dataDevice_.IOAddrLength -= data_.dataDevice_.decreteCount / 8;
            //    data_.dataDevice_.decreteCount = 0;
            //    textBox3.Text = data_.dataDevice_.decreteCount.ToString();
            //    return;
            //}
                
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
           // bool number = isNumber(textBox4.Text);
           // if(number == true)
           // {
           //     if (Convert.ToInt32(textBox4.Text) < 0 || Convert.ToInt32(textBox4.Text) > 500)
           //     {
           //         MessageBox.Show("超出范围");
           //         textBox4.Text = data_.dataDevice_.decreteCount.ToString();
           //     }
           //     else
           //     {
           //         data_.dataDevice_.IOAddrLength -= data_.dataDevice_.statusCount * 2;
           //         int.TryParse(textBox4.Text, out data_.dataDevice_.statusCount);
           //         data_.dataDevice_.IOAddrLength += data_.dataDevice_.statusCount * 2;
           //         textBox22.Text = data_.dataDevice_.IOAddrLength.ToString();
           //     }
           // }
           //else
           // {
           //     data_.dataDevice_.IOAddrLength -= data_.dataDevice_.statusCount * 2;
           //     data_.dataDevice_.statusCount = 0;
           //     textBox4.Text = data_.dataDevice_.statusCount.ToString();
           //     return;
           // }
        }
        public bool isNumber(string message)
        {
            try
            {
                int a = Convert.ToInt32(message);
                return true;
            }
            catch
            {
                MessageBox.Show("请输入范围内的数字");
                return false;
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
           // int.TryParse(textBox9.Text, out data_.dataDevice_.ip10);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox10.Text, out data_.dataDevice_.ip11);
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox11.Text, out data_.dataDevice_.ip12);
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox12.Text, out data_.dataDevice_.ip13);
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox13.Text, out data_.dataDevice_.ip20);
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox14.Text, out data_.dataDevice_.ip21);
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox15.Text, out data_.dataDevice_.ip22);
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox16.Text, out data_.dataDevice_.ip23);
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox17.Text, out data_.dataDevice_.ip30);
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox18.Text, out data_.dataDevice_.ip31);
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox19.Text, out data_.dataDevice_.ip32);
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            //int.TryParse(textBox20.Text, out data_.dataDevice_.ip33);
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            //bool number = isNumber(textBox1.Text);
            //if (number == true)
            //{
            //    if (data_.dataDevice_.coilCount == 0 && Convert.ToInt32(textBox1.Text) != 0)
            //    {
            //        data_.dataDevice_.shmlength -= data_.dataDevice_.coilCount / 8 ;
            //        int.TryParse(textBox1.Text, out data_.dataDevice_.coilCount);
            //        data_.dataDevice_.shmlength += data_.dataDevice_.coilCount / 8 + 1;
            //        textBox29.Text = data_.dataDevice_.shmlength.ToString();
            //    }
            //    else if (data_.dataDevice_.coilCount == 0 && Convert.ToInt32(textBox1.Text) == 0)
            //    {
            //        data_.dataDevice_.shmlength -= data_.dataDevice_.coilCount / 8;
            //        int.TryParse(textBox1.Text, out data_.dataDevice_.coilCount);
            //        data_.dataDevice_.shmlength += data_.dataDevice_.coilCount / 8;
            //        textBox29.Text = data_.dataDevice_.shmlength.ToString();
            //    }
            //    else if(data_.dataDevice_.coilCount != 0 && Convert.ToInt32(textBox1.Text) == 0)
            //    {
            //        data_.dataDevice_.shmlength -= data_.dataDevice_.coilCount / 8 + 1;
            //        int.TryParse(textBox1.Text, out data_.dataDevice_.coilCount);
            //        data_.dataDevice_.shmlength += data_.dataDevice_.coilCount / 8;
            //        textBox29.Text = data_.dataDevice_.shmlength.ToString();
            //    }
            //    else
            //    {
            //        data_.dataDevice_.shmlength -= data_.dataDevice_.coilCount / 8 + 1;
            //        int.TryParse(textBox1.Text, out data_.dataDevice_.coilCount);
            //        data_.dataDevice_.shmlength += data_.dataDevice_.coilCount / 8 + 1;
            //        textBox29.Text = data_.dataDevice_.shmlength.ToString();
            //    }
            //}
            //else
            //{
            //    data_.dataDevice_.shmlength -= data_.dataDevice_.coilCount / 8;
            //    data_.dataDevice_.coilCount = 0;
            //    textBox1.Text = data_.dataDevice_.coilCount.ToString();
            //    //data_.dataDevice_.mholdingstart = data_.dataDevice_.shmrange + data_.dataDevice_.coilCount / 8;
            //    return;
            //}
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            //bool number = isNumber(textBox2.Text);
            //if (number == true)
            //{
            //    //if (Convert.ToInt32(textBox4.Text) < 0 || Convert.ToInt32(textBox4.Text) > 500)
            //    //{
            //    //    MessageBox.Show("超出范围");
            //    //    textBox4.Text = data_.dataDevice_.decreteCount.ToString();
            //    //}
            //    //else
            //    //{
            //        data_.dataDevice_.shmlength -= data_.dataDevice_.holdingCount * 2;
            //        int.TryParse(textBox2.Text, out data_.dataDevice_.holdingCount);
            //        data_.dataDevice_.shmlength += data_.dataDevice_.holdingCount * 2;
            //        textBox29.Text = data_.dataDevice_.shmlength.ToString();
            //    //}
            //}
            //else
            //{
            //    data_.dataDevice_.shmlength -= data_.dataDevice_.holdingCount * 2;
            //    data_.dataDevice_.holdingCount = 0;
            //    textBox2.Text = data_.dataDevice_.holdingCount.ToString();
            //    return;
            //}
        }
        private void checklength()
        {

        }

        private void modbusserver_Shown(object sender, EventArgs e)
        {
            if (data_.dataDevice_.ipfixed == true)
            {
                radioButton3.Checked = true;

                int connectnumber = data_.dataDevice_.maxconnectnumber;            
                   if(connectnumber == 1)
                   {
                            textBox25.ReadOnly = false;
                            textBox25.Enabled = true;
                            textBox26.ReadOnly = false;
                            textBox26.Enabled = true;
                            textBox27.ReadOnly = false;
                            textBox27.Enabled = true;
                            textBox28.ReadOnly = false;
                            textBox28.Enabled = true;
                    }
                    else if (connectnumber == 2)
                    {
                            textBox25.ReadOnly = false;
                            textBox25.Enabled = true;
                            textBox26.ReadOnly = false;
                            textBox26.Enabled = true;
                            textBox27.ReadOnly = false;
                            textBox27.Enabled = true;
                            textBox28.ReadOnly = false;
                            textBox28.Enabled = true;
                            textBox9.ReadOnly = false;
                            textBox9.Enabled = true;
                            textBox10.ReadOnly = false;
                            textBox10.Enabled = true;
                            textBox12.ReadOnly = false;
                            textBox12.Enabled = true;
                            textBox11.ReadOnly = false;
                            textBox11.Enabled = true;
                     }
                    else if(connectnumber == 3)
                     {
                            textBox25.ReadOnly = false;
                            textBox25.Enabled = true;
                            textBox26.ReadOnly = false;
                            textBox26.Enabled = true;
                            textBox27.ReadOnly = false;
                            textBox27.Enabled = true;
                            textBox28.ReadOnly = false;
                            textBox28.Enabled = true;
                            textBox9.ReadOnly = false;
                            textBox9.Enabled = true;
                            textBox10.ReadOnly = false;
                            textBox10.Enabled = true;
                            textBox12.ReadOnly = false;
                            textBox12.Enabled = true;
                            textBox11.ReadOnly = false;
                            textBox11.Enabled = true;
                            textBox13.ReadOnly = false;
                            textBox13.Enabled = true;
                            textBox14.ReadOnly = false;
                            textBox14.Enabled = true;
                            textBox15.ReadOnly = false;
                            textBox15.Enabled = true;
                            textBox16.ReadOnly = false;
                            textBox16.Enabled = true;
                     }
                    else if (connectnumber == 4)
                     {
                            textBox25.ReadOnly = false;
                            textBox25.Enabled = true;
                            textBox26.ReadOnly = false;
                            textBox26.Enabled = true;
                            textBox27.ReadOnly = false;
                            textBox27.Enabled = true;
                            textBox28.ReadOnly = false;
                            textBox28.Enabled = true;
                            textBox9.ReadOnly = false;
                            textBox9.Enabled = true;
                            textBox10.ReadOnly = false;
                            textBox10.Enabled = true;
                            textBox12.ReadOnly = false;
                            textBox12.Enabled = true;
                            textBox11.ReadOnly = false;
                            textBox11.Enabled = true;
                            textBox13.ReadOnly = false;
                            textBox13.Enabled = true;
                            textBox14.ReadOnly = false;
                            textBox14.Enabled = true;
                            textBox15.ReadOnly = false;
                            textBox15.Enabled = true;
                            textBox16.ReadOnly = false;
                            textBox16.Enabled = true;
                            textBox17.ReadOnly = false;
                            textBox17.Enabled = true;
                            textBox18.ReadOnly = false;
                            textBox18.Enabled = true;
                            textBox19.ReadOnly = false;
                            textBox19.Enabled = true;
                            textBox20.ReadOnly = false;
                            textBox20.Enabled = true;
                     }
                           
            else if (data_.dataDevice_.ipfixed == false)
            {
                radioButton4.Checked = true;
                textBox25.ReadOnly = true;
                textBox25.Enabled = false;
                textBox26.ReadOnly = true;
                textBox26.Enabled = false;
                textBox27.ReadOnly = true;
                textBox27.Enabled = false;
                textBox28.ReadOnly = true;
                textBox28.Enabled = false;
            }
        }
     }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            
            bool number = isNumber(textBox1.Text);
            
            if (number == true)
            {
                int sum = Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox5.Text);
                int value = Convert.ToInt32(textBox1.Text);
                if ( sum >= 10000 || (value < 0 || value > 1000))
                {
                    MessageBox.Show("超出范围");
                    textBox1.Text = dataManager.listServer[0].dataDevice_.coilCount.ToString();
                }
                else
                {
                    //if (dataManager.listServer[0].dataDevice_.coilCount == 0 && Convert.ToInt32(textBox1.Text) != 0)
                    //{
                    //    dataManager.listServer[0].dataDevice_.shmlength -= dataManager.listServer[0].dataDevice_.coilCount / 8;
                    //    int.TryParse(textBox1.Text, out dataManager.listServer[0].dataDevice_.coilCount);
                    //    dataManager.listServer[0].dataDevice_.shmlength += dataManager.listServer[0].dataDevice_.coilCount / 8 + 1;
                    //    textBox29.Text = dataManager.listServer[0].dataDevice_.shmlength.ToString();
                    //}
                    //else if (dataManager.listServer[0].dataDevice_.coilCount == 0 && Convert.ToInt32(textBox1.Text) == 0)
                    //{
                    //    dataManager.listServer[0].dataDevice_.shmlength -= dataManager.listServer[0].dataDevice_.coilCount / 8;
                    //    int.TryParse(textBox1.Text, out dataManager.listServer[0].dataDevice_.coilCount);
                    //    dataManager.listServer[0].dataDevice_.shmlength += dataManager.listServer[0].dataDevice_.coilCount / 8;
                    //    textBox29.Text = dataManager.listServer[0].dataDevice_.shmlength.ToString();
                    //}
                    //else if (dataManager.listServer[0].dataDevice_.coilCount != 0 && Convert.ToInt32(textBox1.Text) == 0)
                    //{
                    //    dataManager.listServer[0].dataDevice_.shmlength -= dataManager.listServer[0].dataDevice_.coilCount / 8 + 1;
                    //    int.TryParse(textBox1.Text, out dataManager.listServer[0].dataDevice_.coilCount);
                    //    dataManager.listServer[0].dataDevice_.shmlength += dataManager.listServer[0].dataDevice_.coilCount / 8;
                    //    textBox29.Text = dataManager.listServer[0].dataDevice_.shmlength.ToString();
                    //}
                    //else
                    //{
                    //    dataManager.listServer[0].dataDevice_.shmlength -= dataManager.listServer[0].dataDevice_.coilCount / 8 + 1;
                    //    int.TryParse(textBox1.Text, out dataManager.listServer[0].dataDevice_.coilCount);
                    //    dataManager.listServer[0].dataDevice_.shmlength += dataManager.listServer[0].dataDevice_.coilCount / 8 + 1;
                    //    textBox29.Text = dataManager.listServer[0].dataDevice_.shmlength.ToString();
                    //}


                    //dataManager.listServer[0].dataDevice_.shmlength -= dataManager.listServer[0].dataDevice_.coilCount;
                    //int.TryParse(textBox1.Text, out dataManager.listServer[0].dataDevice_.coilCount);
                    //dataManager.listServer[0].dataDevice_.shmlength += dataManager.listServer[0].dataDevice_.coilCount;
                    //textBox29.Text = dataManager.listServer[0].dataDevice_.shmlength.ToString();

                    textBox29.Text = (Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text)*2).ToString();
                }
            }
            else
            {
                //dataManager.listServer[0].dataDevice_.shmlength -= dataManager.listServer[0].dataDevice_.coilCount;
                //dataManager.listServer[0].dataDevice_.coilCount = 0;
                textBox1.Text = dataManager.listServer[0].dataDevice_.coilCount.ToString();
                //data_.dataDevice_.mholdingstart = data_.dataDevice_.shmrange + data_.dataDevice_.coilCount / 8;
                return;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox2.Text);
            if (number == true)
            {
                int sum = Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox6.Text);
                int value = Convert.ToInt32(textBox2.Text);
                if (sum > 49999 || (value < 0 || value > 500 ))
                {
                    MessageBox.Show("超出范围");
                    textBox2.Text = data_.dataDevice_.holdingCount.ToString();
                }
                else
                {
                    //dataManager.listServer[0].dataDevice_.shmlength -= dataManager.listServer[0].dataDevice_.holdingCount * 2;
                    //int.TryParse(textBox2.Text, out dataManager.listServer[0].dataDevice_.holdingCount);
                    //dataManager.listServer[0].dataDevice_.shmlength += dataManager.listServer[0].dataDevice_.holdingCount * 2;
                    //textBox29.Text = dataManager.listServer[0].dataDevice_.shmlength.ToString();

                    textBox29.Text = (Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text) * 2).ToString();
                }
            }
            else
            {
                //dataManager.listServer[0].dataDevice_.shmlength -= dataManager.listServer[0].dataDevice_.holdingCount * 2;
                //dataManager.listServer[0].dataDevice_.holdingCount = 0;
                textBox2.Text = dataManager.listServer[0].dataDevice_.holdingCount.ToString();
                return;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox3.Text);
            if (number == true)
            {
                int sum = Convert.ToInt32(textBox3.Text) + Convert.ToInt32(textBox7.Text);
                int value = Convert.ToInt32(textBox3.Text);
                if (sum > 19999 || (value < 0 || value > 1000))
                {
                    MessageBox.Show("超出范围");
                    textBox3.Text = dataManager.listServer[0].dataDevice_.decreteCount.ToString();
                }
                else
                {
                    //dataManager.listServer[0].dataDevice_.IOAddrLength -= dataManager.listServer[0].dataDevice_.decreteCount / 8 + 1;
                    //int.TryParse(textBox3.Text, out dataManager.listServer[0].dataDevice_.decreteCount);
                    //dataManager.listServer[0].dataDevice_.IOAddrLength += dataManager.listServer[0].dataDevice_.decreteCount / 8 + 1;
                    //textBox22.Text = dataManager.listServer[0].dataDevice_.IOAddrLength.ToString();
                    //if (dataManager.listServer[0].dataDevice_.decreteCount == 0 && Convert.ToInt32(textBox3.Text) != 0)
                    //{
                    //    dataManager.listServer[0].dataDevice_.IOAddrLength -= dataManager.listServer[0].dataDevice_.decreteCount / 8;
                    //    int.TryParse(textBox3.Text, out dataManager.listServer[0].dataDevice_.decreteCount);
                    //    dataManager.listServer[0].dataDevice_.IOAddrLength += dataManager.listServer[0].dataDevice_.decreteCount / 8 + 1;
                    //    textBox22.Text = dataManager.listServer[0].dataDevice_.IOAddrLength.ToString();
                    //}
                    //else if (dataManager.listServer[0].dataDevice_.decreteCount == 0 && Convert.ToInt32(textBox3.Text) == 0)
                    //{
                    //    dataManager.listServer[0].dataDevice_.IOAddrLength -= dataManager.listServer[0].dataDevice_.decreteCount / 8;
                    //    int.TryParse(textBox3.Text, out dataManager.listServer[0].dataDevice_.decreteCount);
                    //    dataManager.listServer[0].dataDevice_.IOAddrLength += dataManager.listServer[0].dataDevice_.decreteCount / 8;
                    //    textBox22.Text = dataManager.listServer[0].dataDevice_.IOAddrLength.ToString();
                    //}
                    //else if (dataManager.listServer[0].dataDevice_.decreteCount != 0 && Convert.ToInt32(textBox3.Text) == 0)
                    //{
                    //    dataManager.listServer[0].dataDevice_.IOAddrLength -= dataManager.listServer[0].dataDevice_.decreteCount / 8 + 1;
                    //    int.TryParse(textBox3.Text, out dataManager.listServer[0].dataDevice_.decreteCount);
                    //    dataManager.listServer[0].dataDevice_.IOAddrLength += dataManager.listServer[0].dataDevice_.decreteCount / 8;
                    //    textBox22.Text = dataManager.listServer[0].dataDevice_.IOAddrLength.ToString();
                    //}
                    //else
                    //{
                    //    dataManager.listServer[0].dataDevice_.IOAddrLength -= dataManager.listServer[0].dataDevice_.decreteCount / 8 + 1;
                    //    int.TryParse(textBox3.Text, out dataManager.listServer[0].dataDevice_.decreteCount);
                    //    dataManager.listServer[0].dataDevice_.IOAddrLength += dataManager.listServer[0].dataDevice_.decreteCount / 8 + 1;
                    //    textBox22.Text = dataManager.listServer[0].dataDevice_.IOAddrLength.ToString();
                    //}
                    //dataManager.listServer[0].dataDevice_.IOAddrLength -= dataManager.listServer[0].dataDevice_.decreteCount;
                    //int.TryParse(textBox3.Text, out dataManager.listServer[0].dataDevice_.decreteCount);
                    //dataManager.listServer[0].dataDevice_.IOAddrLength += dataManager.listServer[0].dataDevice_.decreteCount;
                    //textBox22.Text = dataManager.listServer[0].dataDevice_.IOAddrLength.ToString();

                    textBox22.Text = (Convert.ToInt32(textBox3.Text) + Convert.ToInt32(textBox4.Text)*2).ToString();
                }
            }
            else
            {
                //dataManager.listServer[0].dataDevice_.IOAddrLength -= dataManager.listServer[0].dataDevice_.decreteCount;
                //dataManager.listServer[0].dataDevice_.decreteCount = 0;
                textBox3.Text = dataManager.listServer[0].dataDevice_.decreteCount.ToString();
                return;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox4.Text);
            if (number == true)
            {
                int sum = Convert.ToInt32(textBox4.Text) + Convert.ToInt32(textBox8.Text);
                int value = Convert.ToInt32(textBox4.Text);
                if (sum > 39999 || (value < 0 || value > 500))
                {
                    MessageBox.Show("超出范围");
                    textBox4.Text = dataManager.listServer[0].dataDevice_.statusCount.ToString();
                }
                else
                {
                    //dataManager.listServer[0].dataDevice_.IOAddrLength -= dataManager.listServer[0].dataDevice_.statusCount * 2;
                    //int.TryParse(textBox4.Text, out dataManager.listServer[0].dataDevice_.statusCount);
                    //dataManager.listServer[0].dataDevice_.IOAddrLength += dataManager.listServer[0].dataDevice_.statusCount * 2;
                    //textBox22.Text = dataManager.listServer[0].dataDevice_.IOAddrLength.ToString();

                    textBox22.Text = (Convert.ToInt32(textBox3.Text) + Convert.ToInt32(textBox4.Text) * 2).ToString();
                }
            }
            else
            {
                //dataManager.listServer[0].dataDevice_.IOAddrLength -= dataManager.listServer[0].dataDevice_.statusCount * 2;
                //dataManager.listServer[0].dataDevice_.statusCount = 0;
                textBox4.Text = dataManager.listServer[0].dataDevice_.statusCount.ToString();
                return;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int flag = 0;

            //data_.dataDevice_.transformport = comboBox2.SelectedIndex;
            //data_.dataDevice_.transformportdescribe = comboBox2.SelectedItem.ToString();
            try
            {
                //串口
                if (comboBox1.SelectedIndex == 0)
                {
                    for (int i = 0; i < dataManager.listServer.Count; i++)
                    {
                        if (dataManager.listServer[i].dataDevice_.transformportdescribe == comboBox2.SelectedItem.ToString() && i!=SID)
                        {
                            flag++;
                        }
                    }
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    for (int i = 0; i < dataManager.listServer.Count; i++)
                    {
                        if (dataManager.listServer[i].dataDevice_.transformportdescribe == comboBox2.SelectedItem.ToString() && i!=SID)
                        {
                            flag++;
                        }
                    }
                }
                if (flag > 0)
                {
                    MessageBox.Show("串口/网口端口选择有重复，请检查后重新配置");
                    comboBox2.SelectedIndex = -1;

                }
            }
            catch
            {
                return;
            }
        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            close = 0;
            if (Convert.ToInt32(textBox29.Text) > 1000)
            {
                MessageBox.Show("长度超出范围，请重新设置");
                textBox29.BackColor = Color.Red;
                data_.dataDevice_.isready = false;
                close = 1;
            }
            else
            {
                textBox29.BackColor = textBox21.BackColor;
                data_.dataDevice_.isready = true;
                close = 0;
            }
        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            //bool number = isNumber(textBox30.Text);
            //if (number == true && (Convert.ToInt32(textBox30.Text) >= 1 && Convert.ToInt32(textBox30.Text) <= 100))
            //{
            //    int flag = 0;
            //    for (int i = 0; i < dataManager.listServer.Count; i++)
            //    {
            //        if (textBox30.Text == dataManager.listServer[i].dataDevice_.deviceAddr.ToString())
            //        {
            //            flag++;
            //        }

            //        if (flag == 0)
            //        {
            //            dataManager.listServer[i].dataDevice_.deviceAddr = Convert.ToInt32(textBox30.Text);
            //        }
            //        else
            //        {
            //            MessageBox.Show("从站地址有重复，请重新设置");
            //            textBox30.Text = null;
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请输入1-100的数字作为从站地址");
            //    textBox30.Text = null;
            //}
        }

        private void textBox30_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox30.Text);
            close = 0;
            if (number == true)
            {
                int x = 0;
                for (int i = 0; i < dataManager.listServer.Count; i++)
                {
                    if (dataManager.listServer[i].dataDevice_.deviceAddr.ToString() == textBox30.Text && dataManager.listServer[i].dataDevice_.transform == 0 && dataManager.listServer[i].ID
                        != data_.ID)
                    {
                        x++;
                    }
                }
                if (Convert.ToInt32(textBox30.Text) >= 0 && Convert.ToInt32(textBox30.Text)<= 100 && x == 0)
                {
                    
                    //data_.dataDevice_.deviceAddr = Convert.ToInt32(textBox30.Text);
                    textBox30.BackColor = textBox1.BackColor;
                }
                else if(x != 0)
                {
                    MessageBox.Show("设备id有重复，请检查");
                    textBox30.BackColor = Color.Red;
                    close = 1;
                    //textBox30.Text = data_.dataDevice_.deviceAddr.ToString();
                    x = 0;
                }
                else if (Convert.ToInt32(textBox30.Text) >= 0 && Convert.ToInt32(textBox30.Text) > 100)
                {
                    MessageBox.Show("设备id超出范围");
                    //textBox30.BackColor = Color.Red;
                    textBox30.Text = data_.dataDevice_.deviceAddr.ToString();
                    x = 0;
                }
               
            }
            else if(number == false)
            {
                    MessageBox.Show("请输入范围内的数字");
                    textBox30.Text = data_.dataDevice_.deviceAddr.ToString();
                    
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox5.Text);
            if (number == true)
            {
                int sum = Convert.ToInt32(textBox5.Text) + Convert.ToInt32(textBox1.Text);
                int value = Convert.ToInt32(textBox5.Text);
                if (sum <= 9999 && (value >=1 && value <= 9999))
                {
                    //dataManager.listServer[0].dataDevice_.coilIoAddrStart = textBox5.Text;
                }
                else
                {
                    MessageBox.Show("线圈寄存器起始地址超出范围");
                    textBox5.Text = dataManager.listServer[0].dataDevice_.coilIoAddrStart;
                }
            }
            else
            {
                     MessageBox.Show("请输入范围内的数字");
                     textBox5.Text = dataManager.listServer[0].dataDevice_.coilIoAddrStart;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox6.Text);
            if (number == true)
            {
                int sum = Convert.ToInt32(textBox6.Text) + Convert.ToInt32(textBox2.Text);
                int value = Convert.ToInt32(textBox6.Text);
                if (value >= 40001 && sum <= 49999)
                {
                    //dataManager.listServer[0].dataDevice_.holdingIoAddrStart = textBox6.Text;
                }
                else
                {
                    MessageBox.Show("线圈寄存器起始地址超出范围");
                    textBox6.Text = dataManager.listServer[0].dataDevice_.holdingIoAddrStart;
                }
            }
            else
            {
                MessageBox.Show("请输入范围内的数字");
                textBox6.Text = dataManager.listServer[0].dataDevice_.holdingIoAddrStart;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox7.Text);
            if (number == true)
            {
                int sum = Convert.ToInt32(textBox7.Text) + Convert.ToInt32(textBox3.Text);
                int value = Convert.ToInt32(textBox7.Text);
                if (value >= 10001 && sum <= 19999)
                {
                    //dataManager.listServer[0].dataDevice_.decreteIoAddrStart = textBox7.Text;
                }
                else
                {
                    MessageBox.Show("线圈寄存器起始地址超出范围");
                    textBox7.Text = dataManager.listServer[0].dataDevice_.decreteIoAddrStart;
                }
            }
            else
            {
                MessageBox.Show("请输入范围内的数字");
                textBox7.Text = dataManager.listServer[0].dataDevice_.decreteIoAddrStart;
            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox8.Text);
            if (number == true)
            {
                int sum = Convert.ToInt32(textBox8.Text) + Convert.ToInt32(textBox4.Text);
                int value = Convert.ToInt32(textBox8.Text);
                if (value >= 30001 && sum <= 39999)
                {
                    //dataManager.listServer[0].dataDevice_.statusIoAddrStart = textBox8.Text;
                }
                else
                {
                    MessageBox.Show("线圈寄存器起始地址超出范围");
                    textBox8.Text = dataManager.listServer[0].dataDevice_.statusIoAddrStart;
                }
            }
            else
            {
                MessageBox.Show("请输入范围内的数字");
                textBox8.Text = dataManager.listServer[0].dataDevice_.statusIoAddrStart;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if(comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("请设置传输端口");
                return;
            }
            //各个server的通用配置保存（寄存器线圈等）
            dataManager.listServer[0].dataDevice_.coilCount = Convert.ToInt32(textBox1.Text);
            dataManager.listServer[0].dataDevice_.holdingCount = Convert.ToInt32(textBox2.Text);
            dataManager.listServer[0].dataDevice_.decreteCount = Convert.ToInt32(textBox3.Text);
            dataManager.listServer[0].dataDevice_.statusCount = Convert.ToInt32(textBox4.Text);
            dataManager.listServer[0].dataDevice_.coilIoAddrStart = textBox5.Text;
            dataManager.listServer[0].dataDevice_.holdingIoAddrStart = textBox6.Text;
            dataManager.listServer[0].dataDevice_.decreteIoAddrStart = textBox7.Text;
            dataManager.listServer[0].dataDevice_.statusIoAddrStart = textBox8.Text;
            dataManager.listServer[0].dataDevice_.IOAddrLength = Convert.ToInt32(textBox22.Text);
            dataManager.listServer[0].dataDevice_.shmlength = Convert.ToInt32(textBox29.Text);
            //传输端口
            data_.dataDevice_.transform = comboBox1.SelectedIndex;
            data_.dataDevice_.transformport = comboBox2.SelectedIndex;
            data_.dataDevice_.transformportdescribe = comboBox2.SelectedItem.ToString();
            try
            { 
                data_.dataDevice_.transformportdescribe = comboBox2.SelectedItem.ToString(); 
            }
            catch
            {
                data_.dataDevice_.transformportdescribe = "";
            }
            //传输模式选择
            if (radioButton5.Checked == true)
            {
                data_.dataDevice_.slavetansformMode = (int)SLAVETRANS.ASCII;
            }
            if (radioButton6.Checked == true)
            {
                data_.dataDevice_.slavetansformMode = (int)SLAVETRANS.RTU;
            }
            if (radioButton1.Checked == true)
            {
                data_.dataDevice_.transformMode = (int)TRANSFORMMODE.TCP;
            }
            if (radioButton2.Checked == true)
            {
                data_.dataDevice_.transformMode = (int)TRANSFORMMODE.UDP;
            }
            data_.dataDevice_.deviceAddr = Convert.ToInt32(textBox30.Text);
            // 网口模式下的配置信息
            if(textBox23.Visible == true)
            {
                data_.dataDevice_.port = Convert.ToInt32(textBox23.Text);
            }
            else
            {
                data_.dataDevice_.port = 0;
            }
            string ip0 = textBox25.Text + textBox26.Text + textBox27.Text + textBox28.Text;
            string ip1 = textBox9.Text + textBox10.Text + textBox11.Text + textBox12.Text;
            string ip2 = textBox13.Text + textBox14.Text + textBox15.Text + textBox16.Text;
            string ip3 = textBox17.Text + textBox18.Text + textBox19.Text + textBox20.Text;
            string[] ip = new string[4];
            ip[0] = ip0;
            ip[1] = ip1;
            ip[2] = ip2;
            ip[3] = ip3;
            int flag = 0;
            //data_.dataDevice_.port =Convert.ToInt32(textBox23.Text);
            if (radioButton3.Checked == true)
            { 
                data_.dataDevice_.ipfixed = true;
                data_.dataDevice_.maxconnectnumber = Convert.ToInt32(textBox24.Text);
                if (data_.dataDevice_.maxconnectnumber > 1)
                {
                    for (int i = 0; i < data_.dataDevice_.maxconnectnumber; i++)
                    {
                        for(int j = i+1;j< data_.dataDevice_.maxconnectnumber;j++)
                        {
                            if (ip[i] == ip[j])
                            {
                                flag++;
                            }
                        }
                    }
                }
                if (flag>0)
                {
                    MessageBox.Show("ip有重复，请重新设置");
                    
                }
                else
                {
                    int.TryParse(textBox25.Text, out data_.dataDevice_.ip0);
                    int.TryParse(textBox26.Text, out data_.dataDevice_.ip1);
                    int.TryParse(textBox27.Text, out data_.dataDevice_.ip2);
                    int.TryParse(textBox28.Text, out data_.dataDevice_.ip3);

                    int.TryParse(textBox9.Text, out data_.dataDevice_.ip10);
                    int.TryParse(textBox10.Text, out data_.dataDevice_.ip11);
                    int.TryParse(textBox11.Text, out data_.dataDevice_.ip12);
                    int.TryParse(textBox12.Text, out data_.dataDevice_.ip13);

                    int.TryParse(textBox13.Text, out data_.dataDevice_.ip20);
                    int.TryParse(textBox14.Text, out data_.dataDevice_.ip21);
                    int.TryParse(textBox15.Text, out data_.dataDevice_.ip22);
                    int.TryParse(textBox16.Text, out data_.dataDevice_.ip23);

                    int.TryParse(textBox17.Text, out data_.dataDevice_.ip30);
                    int.TryParse(textBox18.Text, out data_.dataDevice_.ip31);
                    int.TryParse(textBox19.Text, out data_.dataDevice_.ip32);
                    int.TryParse(textBox20.Text, out data_.dataDevice_.ip33);
                    
                }
            }
            else
            { 
                data_.dataDevice_.ipfixed = false; 
            }
            

            
            
            

            //data_.dataDevice_.ip0 = Convert.ToInt32(textBox25.Text);
            //data_.dataDevice_.ip1 = Convert.ToInt32(textBox26.Text);
            //data_.dataDevice_.ip2 = Convert.ToInt32(textBox27.Text);
            //data_.dataDevice_.ip3 = Convert.ToInt32(textBox28.Text);

            //data_.dataDevice_.ip10 = Convert.ToInt32(textBox9.Text);
            //data_.dataDevice_.ip11 = Convert.ToInt32(textBox10.Text);
            //data_.dataDevice_.ip12 = Convert.ToInt32(textBox11.Text);
            //data_.dataDevice_.ip13 = Convert.ToInt32(textBox12.Text);

            //data_.dataDevice_.ip20 = Convert.ToInt32(textBox13.Text);
            //data_.dataDevice_.ip21 = Convert.ToInt32(textBox14.Text);
            //data_.dataDevice_.ip22 = Convert.ToInt32(textBox15.Text);
            //data_.dataDevice_.ip23 = Convert.ToInt32(textBox16.Text);

            //data_.dataDevice_.ip30 = Convert.ToInt32(textBox17.Text);
            //data_.dataDevice_.ip31 = Convert.ToInt32(textBox18.Text);
            //data_.dataDevice_.ip32 = Convert.ToInt32(textBox19.Text);
            //data_.dataDevice_.ip33 = Convert.ToInt32(textBox20.Text);
        }
        public bool error()
        {
            string a = textBox29.Text;
            if (data_.dataDevice_.IOAddrLength >= 1000 || data_.dataDevice_.shmlength >= 1000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //modbusserver msv = new modbusserver(0);
        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();

        }
        
        private void textBox25_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox25.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox25.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox25.Text = data_.dataDevice_.ip0.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox25.Text = data_.dataDevice_.ip0.ToString();
            }
        }

        private void textBox26_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox26.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox26.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox26.Text = data_.dataDevice_.ip1.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox26.Text = data_.dataDevice_.ip1.ToString();
            }
        }

        private void textBox27_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox27.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox27.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox27.Text = data_.dataDevice_.ip2.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox27.Text = data_.dataDevice_.ip2.ToString();
            }
        }

        private void textBox28_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox28.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox28.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox28.Text = data_.dataDevice_.ip3.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox28.Text = data_.dataDevice_.ip3.ToString();
            }
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox9.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox9.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox9.Text = data_.dataDevice_.ip10.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox9.Text = data_.dataDevice_.ip10.ToString();
            }
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox10.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox10.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox10.Text = data_.dataDevice_.ip11.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox10.Text = data_.dataDevice_.ip11.ToString();
            }
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox11.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox11.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox11.Text = data_.dataDevice_.ip12.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox11.Text = data_.dataDevice_.ip12.ToString();
            }
        }

        private void textBox12_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox12.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox12.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox12.Text = data_.dataDevice_.ip13.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox12.Text = data_.dataDevice_.ip13.ToString();
            }
        }

        private void textBox13_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox13.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox13.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox13.Text = data_.dataDevice_.ip20.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox13.Text = data_.dataDevice_.ip20.ToString();
            }
        }

        private void textBox14_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox14.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox14.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox14.Text = data_.dataDevice_.ip21.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox14.Text = data_.dataDevice_.ip21.ToString();
            }
        }

        private void textBox15_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox15.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox15.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox15.Text = data_.dataDevice_.ip22.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox15.Text = data_.dataDevice_.ip22.ToString();
            }
        }

        private void textBox16_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox16.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox16.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox16.Text = data_.dataDevice_.ip23.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox16.Text = data_.dataDevice_.ip23.ToString();
            }
        }

        private void textBox17_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox17.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox17.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox17.Text = data_.dataDevice_.ip30.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox17.Text = data_.dataDevice_.ip30.ToString();
            }
        }

        private void textBox18_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox18.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox18.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox25.Text = data_.dataDevice_.ip31.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox18.Text = data_.dataDevice_.ip31.ToString();
            }
        }

        private void textBox19_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox19.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox19.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox19.Text = data_.dataDevice_.ip32.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox19.Text = data_.dataDevice_.ip32.ToString();
            }
        }

        private void textBox20_Leave(object sender, EventArgs e)
        {
            bool number = isNumber(textBox20.Text);
            if (number == true)
            {
                int value = Convert.ToInt32(textBox20.Text);
                if (value < 0 || value > 255)
                {
                    MessageBox.Show("ip超出范围");
                    textBox20.Text = data_.dataDevice_.ip33.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
                textBox20.Text = data_.dataDevice_.ip33.ToString();
            }
        }

        private void modbusserver_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if(close == 1 && comboBox2.SelectedIndex == -1)
            //{
            //    MessageBox.Show("配置存在错误！");
            //    e.Cancel = true;
            //}

            //else
            //{
            //    try
            //    {
            //        for (int i = 0; i < dataManager.listServer.Count; i++)
            //        {
            //            if (dataManager.listServer[i].dataDevice_.transformportdescribe == comboBox2.SelectedItem.ToString() && i != SID)
            //            {
            //                MessageBox.Show("端口使用重复，请更换！");
            //                e.Cancel = true;

            //            }
            //        }
            //    }
            //    catch
            //    {
            //        MessageBox.Show("端口未设置，请设置！");
            //        //e.Cancel = true;
            //    }
            //    e.Cancel = false;
            //}
            if (textBox23.Visible == false)
            {
                data_.dataDevice_.port = 0;
            }

        }
    }
}
