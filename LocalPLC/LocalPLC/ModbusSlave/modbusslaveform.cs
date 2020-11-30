using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LocalPLC.ModbusSlave
{


    public partial class modbusslaveform : Form
    {
        //00001至09999是离散输出(线圈)-----Coil status
        private int modbusCoilStartAddr = 1;
        //10001至19999是离散输入(触点)-----Input status
        private int modbusDiscreteStartAddr = 10001;
        private int modbusDiscreteEndAddr = 19999;

        //30001至39999是输入寄存器(通常是模拟量输入)------Input register
        private int modbusInputAddr = 30001;
        //40001至49999是保持寄存器 -------Holding register
        private int modbusHoldingStartAddr = 40001;
        private int modbusHoldingEndAddr = 49999;


        private DataManager dataManager = null;
        ModbusSlaveData data_;
        enum TRANSFORMMODE : int
        { RTU, ASCII}
        public modbusslaveform(int index)
        {
            InitializeComponent();

            dataManager = DataManager.GetInstance();
        }


        public void getSlaveData(ref ModbusSlaveData data)
        {
            data_ = data;
        }

        

        private void textBox_coil_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox_coil.Text, out data_.dataDevice_.coilCount);

            if(textBox_coil_start.Text == "")
            {
                textBox_coil_start.Text = modbusCoilStartAddr.ToString();
            }
            else 
            {
                textBox_coil_start.Text = data_.dataDevice_.coilIoAddrStart.ToString();
            }

            int startAddr = 0;
            int.TryParse(data_.dataDevice_.coilIoAddrStart, out startAddr);
            if(data_.dataDevice_.coilCount + startAddr - 1 < modbusCoilStartAddr)
            {
                textBox_coil_end.Text = modbusCoilStartAddr.ToString();
            }
            else
            {
                textBox_coil_end.Text = (data_.dataDevice_.coilCount + startAddr - 1).ToString();
            }
            
        }


        private void textBox_holding_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox_holding.Text, out data_.dataDevice_.holdingCount);

            if(textBox_holding_start.Text == "")
            {
                textBox_holding_start.Text = modbusHoldingStartAddr.ToString();
            }


            data_.dataDevice_.holdingIoAddrStart = textBox_holding_start.Text;

            //结束地址
            int startAddrTmp = 0;
            int.TryParse(textBox_holding_start.Text, out startAddrTmp);
            
            if(startAddrTmp + data_.dataDevice_.holdingCount - 1 < modbusHoldingStartAddr)
            {
                textBox_holding_end.Text = modbusHoldingStartAddr.ToString();
            }
            else
            {
                textBox_holding_end.Text = (startAddrTmp + data_.dataDevice_.holdingCount - 1).ToString();
            }

            data_.dataDevice_.holdingIoAddrEnd = textBox_holding_end.Text;
        }



        private void textBox_lisan_TextChanged(object sender, EventArgs e)
        {
            int decreteCount = 0;
            int.TryParse(textBox_lisan.Text, out decreteCount);

            if(textBox_lisan_start.Text == "")
            {
                textBox_lisan_start.Text = modbusDiscreteStartAddr.ToString();
            }

            if (textBox_lisan_end.Text == "")
            {
                textBox_lisan_end.Text = modbusDiscreteStartAddr.ToString();
            }



            int deceteAddrStart = 0;
            int.TryParse(textBox_lisan_start.Text, out deceteAddrStart);
            int deceteAddrEnd = deceteAddrStart + decreteCount - 1;
            if(deceteAddrEnd < modbusDiscreteStartAddr | deceteAddrEnd > modbusDiscreteEndAddr)
            {
                return;
            }

            //起始
            data_.dataDevice_.decreteIoAddrStart = deceteAddrStart.ToString();
            //结束
            data_.dataDevice_.decreteIoAddrEnd = deceteAddrEnd.ToString();

            data_.dataDevice_.decreteCount = decreteCount;
            textBox_lisan_end.Text = data_.dataDevice_.decreteIoAddrEnd;
            textBox_lisan_start.Text = data_.dataDevice_.decreteIoAddrStart;
        }


        private void textBox_status_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox_status.Text, out data_.dataDevice_.statusCount);
        }

        private void textBox_coil_start_TextChanged(object sender, EventArgs e)
        {
            data_.dataDevice_.coilIoAddrStart = textBox_coil_start.Text;

            int startAddr = 0;
            int.TryParse(data_.dataDevice_.coilIoAddrStart, out startAddr);
            int.TryParse(textBox_coil.Text, out data_.dataDevice_.coilCount);
            data_.dataDevice_.coilIoAddrEnd = (startAddr + data_.dataDevice_.coilCount - 1).ToString();
            textBox_coil_end.Text = data_.dataDevice_.coilIoAddrEnd;
        }

        private void textBox_holding_start_TextChanged(object sender, EventArgs e)
        {
            int holdingStartAddr = 0;
            int.TryParse(textBox_holding_start.Text, out holdingStartAddr);

            if(holdingStartAddr < modbusHoldingStartAddr | holdingStartAddr > modbusHoldingEndAddr)
            {
                return;
            }

            int holdingEndAddr = holdingStartAddr + data_.dataDevice_.holdingCount - 1;
            if (holdingEndAddr < modbusHoldingStartAddr | holdingEndAddr > modbusHoldingEndAddr)
            {
                return;
            }

            data_.dataDevice_.holdingIoAddrStart = textBox_holding_start.Text;
            data_.dataDevice_.holdingIoAddrEnd = (holdingStartAddr + data_.dataDevice_.holdingCount - 1).ToString();
            textBox_holding_end.Text = holdingEndAddr.ToString();
        }

        private void textBox_lisan_start_TextChanged(object sender, EventArgs e)
        {
                data_.dataDevice_.decreteIoAddrStart = textBox_lisan_start.Text;
        }

        private void textBox_status_start_TextChanged(object sender, EventArgs e)
        {
                data_.dataDevice_.statusIoAddrStart =  textBox_status_start.Text;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                data_.dataDevice_.transformMode = (int)TRANSFORMMODE.RTU;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked == true)
            {
                data_.dataDevice_.transformMode = (int)TRANSFORMMODE.ASCII;
            }
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox23.Text, out data_.dataDevice_.deviceAddr);
        }
        private void textBox_start_total_TextChanged(object sender, EventArgs e)
        {
            data_.dataDevice_.IOAddrRange = textBox_start_total.Text;
        }

        private void textBox_length_total_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox_length_total.Text, out data_.dataDevice_.IOAddrLength);
        }
        private void modbusslaveform_Load(object sender, EventArgs e)
        {
            textBox_coil.Text = data_.dataDevice_.coilCount.ToString();
            textBox_holding.Text = data_.dataDevice_.holdingCount.ToString();
            textBox_lisan.Text = data_.dataDevice_.decreteCount.ToString();
            textBox_status.Text = data_.dataDevice_.statusCount.ToString();
            textBox_coil_start.Text = data_.dataDevice_.coilIoAddrStart;
            textBox_holding_start.Text = data_.dataDevice_.holdingIoAddrStart;
            textBox_lisan_start.Text = data_.dataDevice_.decreteIoAddrStart;
            textBox_status_start.Text = data_.dataDevice_.statusIoAddrStart;
            textBox_start_total.Text = data_.dataDevice_.IOAddrRange;
            textBox_length_total.Text = data_.dataDevice_.IOAddrLength.ToString();

            if (data_.dataDevice_.transformMode == (int)TRANSFORMMODE.RTU)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }

            textBox23.Text = data_.dataDevice_.deviceAddr.ToString();
        }

        
    }
}
