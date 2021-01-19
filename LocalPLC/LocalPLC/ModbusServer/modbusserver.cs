using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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
        enum TRANSFORMMODE : int
        { TCP, UDP }
        enum SLAVETRANS : int
        { RTU,ASCII}
        enum IPCONNECT:int
        { FALSE,TRUE}
        public modbusserver(int index)
        {
            InitializeComponent();

            dataManager = DataManager.GetInstance();
        }

        public void getServerData(ref ModbusServerData data)
        {
            data_ = data;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            
                
            if(radioButton3.Checked == true)
            {
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
            textBox1.Text = data_.dataDevice_.coilCount.ToString();
            textBox2.Text = data_.dataDevice_.holdingCount.ToString();
            textBox3.Text = data_.dataDevice_.decreteCount.ToString();
            textBox4.Text = data_.dataDevice_.statusCount.ToString();
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
            textBox25.Text = data_.dataDevice_.ip0.ToString();
            textBox26.Text = data_.dataDevice_.ip1.ToString();
            textBox27.Text = data_.dataDevice_.ip2.ToString();
            textBox28.Text = data_.dataDevice_.ip3.ToString();
            textBox5.Text = data_.dataDevice_.coilIoAddrStart;
            textBox6.Text = data_.dataDevice_.holdingIoAddrStart;
            textBox7.Text = data_.dataDevice_.decreteIoAddrStart;
            textBox8.Text = data_.dataDevice_.statusIoAddrStart;

            textBox21.Text = data_.serverstartaddr.ToString();
            textBox21.ReadOnly = true;
            textBox22.Text = data_.dataDevice_.IOAddrLength.ToString();
            if(data_.dataDevice_.ipconnect == (int)IPCONNECT.TRUE)
            {
                radioButton3.Checked = true;
                textBox25.ReadOnly = false;
                textBox25.Enabled = true;
                textBox26.ReadOnly = false;
                textBox26.Enabled = true;
                textBox27.ReadOnly = false;
                textBox27.Enabled = true;
                textBox28.ReadOnly = false;
                textBox28.Enabled = true;
            }
            else
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
            //textBox13.ReadOnly = true;
            //textBox13.Enabled = false;
            //textBox14.ReadOnly = true;
            //textBox14.Enabled = false;
            //textBox15.ReadOnly = true;
            //textBox15.Enabled = false;
            //textBox16.ReadOnly = true;
            //textBox16.Enabled = false;
            comboBox1.SelectedIndex = data_.dataDevice_.transform;
            textBox21.ReadOnly = true;
            textBox22.ReadOnly = true;
            textBox29.ReadOnly = true;
            textBox22.Text = data_.dataDevice_.IOAddrLength.ToString();
            textBox29.Text = data_.dataDevice_.shmlength.ToString();
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
            int.TryParse(textBox23.Text, out data_.dataDevice_.port);
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            
            if (radioButton3.Checked == true)
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

                        }
                        break;
                }
            }
            if(Convert.ToInt32( textBox24.Text )< 1 || Convert.ToInt32(textBox24.Text) > 4)
            {
                MessageBox.Show("超出范围");
                textBox24.Text = data_.dataDevice_.maxconnectnumber.ToString();
            }
            else
            {
                int.TryParse(textBox24.Text, out data_.dataDevice_.maxconnectnumber);
            }
         }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox25.Text, out data_.dataDevice_.ip0);
        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox26.Text, out data_.dataDevice_.ip1);
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox27.Text, out data_.dataDevice_.ip2);
        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox28.Text, out data_.dataDevice_.ip3);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            data_.dataDevice_.coilIoAddrStart = textBox5.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            data_.dataDevice_.holdingIoAddrStart = textBox6.Text;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            data_.dataDevice_.decreteIoAddrStart = textBox7.Text;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            data_.dataDevice_.statusIoAddrStart = textBox8.Text;
        }

        

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            //data_.dataDevice_.IOAddrLength = Convert.ToInt32( textBox22.Text);
            if (Convert.ToInt32( textBox22.Text ) > 1000 )
            {
                MessageBox.Show("长度超出范围，请重新设置");
                textBox22.BackColor = Color.Red;
                data_.dataDevice_.isready = false;
            }
            else
            {
                textBox22.BackColor = Color.White;
                data_.dataDevice_.isready = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton5.Enabled = true;
                radioButton6.Enabled = true;
                data_.dataDevice_.transform = 0;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton5.Enabled = false;
                radioButton6.Enabled = false;
                data_.dataDevice_.transform = 1;
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

        private void textBox3_MouseLeave(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(textBox3.Text) < 0 || Convert.ToInt32(textBox3.Text) > 8000)
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

        private void textBox3_Validated(object sender, EventArgs e)
        {
            bool number = isNumber(textBox3.Text);
            if (number == true)
            {
                if (Convert.ToInt32(textBox3.Text) < 0 || Convert.ToInt32(textBox3.Text) > 8000)
                {
                    MessageBox.Show("超出范围");
                    textBox3.Text = data_.dataDevice_.decreteCount.ToString();
                }
                else
                {
                    data_.dataDevice_.IOAddrLength -= data_.dataDevice_.decreteCount / 8 + 1;
                    int.TryParse(textBox3.Text, out data_.dataDevice_.decreteCount);
                    data_.dataDevice_.IOAddrLength += data_.dataDevice_.decreteCount / 8 + 1;
                    textBox22.Text = data_.dataDevice_.IOAddrLength.ToString();
                }
            }
            else
            {
                data_.dataDevice_.IOAddrLength -= data_.dataDevice_.decreteCount / 8;
                data_.dataDevice_.decreteCount = 0;
                textBox3.Text = data_.dataDevice_.decreteCount.ToString();
                return;
            }
                
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
            bool number = isNumber(textBox4.Text);
            if(number == true)
            {
                if (Convert.ToInt32(textBox4.Text) < 0 || Convert.ToInt32(textBox4.Text) > 500)
                {
                    MessageBox.Show("超出范围");
                    textBox4.Text = data_.dataDevice_.decreteCount.ToString();
                }
                else
                {
                    data_.dataDevice_.IOAddrLength -= data_.dataDevice_.statusCount * 2;
                    int.TryParse(textBox4.Text, out data_.dataDevice_.statusCount);
                    data_.dataDevice_.IOAddrLength += data_.dataDevice_.statusCount * 2;
                    textBox22.Text = data_.dataDevice_.IOAddrLength.ToString();
                }
            }
           else
            {
                data_.dataDevice_.IOAddrLength -= data_.dataDevice_.statusCount * 2;
                data_.dataDevice_.statusCount = 0;
                textBox4.Text = data_.dataDevice_.statusCount.ToString();
                return;
            }
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
            int.TryParse(textBox9.Text, out data_.dataDevice_.ip10);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox10.Text, out data_.dataDevice_.ip11);
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox11.Text, out data_.dataDevice_.ip12);
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox12.Text, out data_.dataDevice_.ip13);
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox13.Text, out data_.dataDevice_.ip20);
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox14.Text, out data_.dataDevice_.ip21);
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox15.Text, out data_.dataDevice_.ip22);
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox16.Text, out data_.dataDevice_.ip23);
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox17.Text, out data_.dataDevice_.ip30);
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox18.Text, out data_.dataDevice_.ip31);
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox19.Text, out data_.dataDevice_.ip32);
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox20.Text, out data_.dataDevice_.ip33);
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            bool number = isNumber(textBox1.Text);
            if (number == true)
            {
                if (data_.dataDevice_.coilCount == 0 && Convert.ToInt32(textBox1.Text) != 0)
                {
                    data_.dataDevice_.shmlength -= data_.dataDevice_.coilCount / 8 ;
                    int.TryParse(textBox1.Text, out data_.dataDevice_.coilCount);
                    data_.dataDevice_.shmlength += data_.dataDevice_.coilCount / 8 + 1;
                    textBox29.Text = data_.dataDevice_.shmlength.ToString();
                }
                else if (data_.dataDevice_.coilCount == 0 && Convert.ToInt32(textBox1.Text) == 0)
                {
                    data_.dataDevice_.shmlength -= data_.dataDevice_.coilCount / 8;
                    int.TryParse(textBox1.Text, out data_.dataDevice_.coilCount);
                    data_.dataDevice_.shmlength += data_.dataDevice_.coilCount / 8;
                    textBox29.Text = data_.dataDevice_.shmlength.ToString();
                }
                else if(data_.dataDevice_.coilCount != 0 && Convert.ToInt32(textBox1.Text) == 0)
                {
                    data_.dataDevice_.shmlength -= data_.dataDevice_.coilCount / 8 + 1;
                    int.TryParse(textBox1.Text, out data_.dataDevice_.coilCount);
                    data_.dataDevice_.shmlength += data_.dataDevice_.coilCount / 8;
                    textBox29.Text = data_.dataDevice_.shmlength.ToString();
                }
                else
                {
                    data_.dataDevice_.shmlength -= data_.dataDevice_.coilCount / 8 + 1;
                    int.TryParse(textBox1.Text, out data_.dataDevice_.coilCount);
                    data_.dataDevice_.shmlength += data_.dataDevice_.coilCount / 8 + 1;
                    textBox29.Text = data_.dataDevice_.shmlength.ToString();
                }
            }
            else
            {
                data_.dataDevice_.shmlength -= data_.dataDevice_.coilCount / 8;
                data_.dataDevice_.coilCount = 0;
                textBox1.Text = data_.dataDevice_.coilCount.ToString();
                //data_.dataDevice_.mholdingstart = data_.dataDevice_.shmrange + data_.dataDevice_.coilCount / 8;
                return;
            }
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            bool number = isNumber(textBox2.Text);
            if (number == true)
            {
                //if (Convert.ToInt32(textBox4.Text) < 0 || Convert.ToInt32(textBox4.Text) > 500)
                //{
                //    MessageBox.Show("超出范围");
                //    textBox4.Text = data_.dataDevice_.decreteCount.ToString();
                //}
                //else
                //{
                    data_.dataDevice_.shmlength -= data_.dataDevice_.holdingCount * 2;
                    int.TryParse(textBox2.Text, out data_.dataDevice_.holdingCount);
                    data_.dataDevice_.shmlength += data_.dataDevice_.holdingCount * 2;
                    textBox29.Text = data_.dataDevice_.shmlength.ToString();
                //}
            }
            else
            {
                data_.dataDevice_.shmlength -= data_.dataDevice_.holdingCount * 2;
                data_.dataDevice_.holdingCount = 0;
                textBox2.Text = data_.dataDevice_.holdingCount.ToString();
                return;
            }
        }
        private void checklength()
        {

        }
    }
}
