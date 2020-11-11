using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
        enum IPCONNECT:int
        { TRUE,FALSE}
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
            if (radioButton3.Checked)
            {
                

                textBox25.ReadOnly = false;
                textBox25.Enabled = true;
                textBox26.ReadOnly = false;
                textBox26.Enabled = true;
                textBox27.ReadOnly = false;
                textBox27.Enabled = true;
                textBox28.ReadOnly = false;
                textBox28.Enabled = true;
                data_.dataDevice_.ipconnect = (int)IPCONNECT.TRUE;
            }
            else if(radioButton3.Checked = false)
            {
                

                textBox25.ReadOnly = true;
                textBox25.Enabled = false;
                textBox26.ReadOnly = true;
                textBox26.Enabled = false;
                textBox27.ReadOnly = true;
                textBox27.Enabled = false;
                textBox28.ReadOnly = true;
                textBox28.Enabled = false;
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
            textBox23.Text = data_.dataDevice_.port.ToString();
            textBox24.Text = data_.dataDevice_.maxconnectnumber.ToString();
            textBox25.Text = data_.dataDevice_.ip0.ToString();
            textBox26.Text = data_.dataDevice_.ip1.ToString();
            textBox27.Text = data_.dataDevice_.ip2.ToString();
            textBox28.Text = data_.dataDevice_.ip3.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out data_.dataDevice_.coilCount);
            textBox5.Text = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox2.Text, out data_.dataDevice_.holdingCount);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox3.Text, out data_.dataDevice_.decreteCount);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox4.Text, out data_.dataDevice_.statusCount);
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

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox23.Text, out data_.dataDevice_.port);
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox24.Text, out data_.dataDevice_.maxconnectnumber);
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
    }
}
