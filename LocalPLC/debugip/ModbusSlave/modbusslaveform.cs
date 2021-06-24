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
    //coil decrete holding status(input)
    //比例 1 1 4 4

    public partial class modbusslaveform : Form
    {
        //00001至09999是离散输出(线圈)-----Coil status
        private int modbusCoilStartAddr = 1;
        private int modbusCoilEndAddr = 9999;

        //10001至19999是离散输入(触点)-----Input status
        private int modbusDiscreteStartAddr = 10001;
        private int modbusDiscreteEndAddr = 19999;

        //30001至39999是输入寄存器(通常是模拟量输入)------Input register status
        private int modbusInputStartAddr = 30001;
        private int modbusInputEndAddr = 39999;


        //40001至49999是保持寄存器 -------Holding register
        private int modbusHoldingStartAddr = 40001;
        private int modbusHoldingEndAddr = 49999;


        private DataManager dataManager = null;
        private ModbusSlaveData data_;
        private int slaveStartAddr_ = 0;
        enum TRANSFORMMODE : int
        { RTU, ASCII}
        public modbusslaveform(int index)
        {
            InitializeComponent();

            dataManager = DataManager.GetInstance();
        }


        public void getSlaveData(ref ModbusSlaveData data, int slaveStartAddr)
        {
            data_ = data;
            slaveStartAddr_ = slaveStartAddr;
        }

        

        private void textBox_coil_TextChanged(object sender, EventArgs e)
        {
            if(!loadFlag)
            {
                return;
            }
            
            int.TryParse(textBox_coil.Text, out data_.dataDevice_.coilCount);

            if(textBox_coil_start.Text == "")
            {
                textBox_coil_start.Text = modbusCoilStartAddr.ToString();
            }
            else 
            {
                textBox_coil_start.Text = data_.dataDevice_.coilModbusAddrStart.ToString();
            }

            int startAddr = 0;
            int.TryParse(data_.dataDevice_.coilModbusAddrStart, out startAddr);
            if(data_.dataDevice_.coilCount + startAddr - 1 < modbusCoilStartAddr)
            {
                textBox_coil_end.Text = modbusCoilStartAddr.ToString();
            }
            else
            {
                textBox_coil_end.Text = (data_.dataDevice_.coilCount + startAddr - 1).ToString();
            }

            data_.dataDevice_.coilIoAddrEnd = textBox_coil_end.Text;
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
            if (!loadFlag)
            {
                return;
            }

            int decreteCount = 0;
            int.TryParse(textBox_lisan.Text, out decreteCount);

            if(textBox_lisan_start.Text == "")
            {
                textBox_lisan_start.Text = modbusDiscreteStartAddr.ToString();
            }
            else
            {
                textBox_lisan_start.Text = data_.dataDevice_.decreteIoAddrStart;
            }

            if (textBox_lisan_end.Text == "")
            {
                textBox_lisan_end.Text = modbusDiscreteStartAddr.ToString();
            }
            else
            {
                textBox_lisan_end.Text = data_.dataDevice_.decreteIoAddrEnd;
            }



            int deceteAddrStart = 0;
            int.TryParse(textBox_lisan_start.Text, out deceteAddrStart);
            int deceteAddrEnd = deceteAddrStart + decreteCount - 1;
            if(deceteAddrEnd < modbusDiscreteStartAddr)
            {
                deceteAddrEnd = modbusDiscreteStartAddr;
            }
            else if(deceteAddrEnd > modbusDiscreteEndAddr)
            {
                deceteAddrEnd = modbusDiscreteEndAddr;
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
            if (!loadFlag)
            {
                return;
            }

            int.TryParse(textBox_status.Text, out data_.dataDevice_.statusCount);
            data_.dataDevice_.statusIoAddrStart = textBox_status_start.Text;
            //data_.dataDevice_.statusIoAddrEnd = textBox_status_end.Text;

            int statusStartAddr = 0;
            int.TryParse(data_.dataDevice_.statusIoAddrStart, out statusStartAddr);

            int statusEndAddr = statusStartAddr + data_.dataDevice_.statusCount - 1;
            if(statusEndAddr > modbusInputEndAddr)
            {
                return;
            }

            //界面
            textBox_status_end.Text = statusEndAddr.ToString();
            //value
            data_.dataDevice_.statusIoAddrEnd = statusEndAddr.ToString();
        }

        private void textBox_coil_start_TextChanged(object sender, EventArgs e)
        {
            if (!loadFlag)
            {
                return;
            }

            data_.dataDevice_.coilModbusAddrStart = textBox_coil_start.Text;

            int startAddr = 0;
            int.TryParse(data_.dataDevice_.coilModbusAddrStart, out startAddr);
            int.TryParse(textBox_coil.Text, out data_.dataDevice_.coilCount);
            data_.dataDevice_.coilIoAddrEnd = (startAddr + data_.dataDevice_.coilCount - 1).ToString();
            textBox_coil_end.Text = data_.dataDevice_.coilIoAddrEnd;
        }

        private void textBox_holding_start_TextChanged(object sender, EventArgs e)
        {
            if (!loadFlag)
            {
                return;
            }

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
            if (!loadFlag)
            {
                return;
            }

            int decreteStartAddr = 0;
            int.TryParse(textBox_lisan_start.Text, out decreteStartAddr);
            if (decreteStartAddr < modbusDiscreteStartAddr | decreteStartAddr > modbusDiscreteEndAddr)
            { 
                return;
            }

            int decreteCount = 0;
            int.TryParse(textBox_lisan.Text, out decreteCount);
            int decreteEndAddr = decreteStartAddr + decreteCount - 1;
            if (decreteEndAddr < modbusDiscreteStartAddr | decreteEndAddr > modbusDiscreteEndAddr)
            {
                return;
            }

            data_.dataDevice_.decreteIoAddrStart = textBox_lisan_start.Text;
            data_.dataDevice_.decreteIoAddrEnd = decreteEndAddr.ToString();

            textBox_lisan_end.Text = data_.dataDevice_.decreteIoAddrEnd;
        }

        private void textBox_status_start_TextChanged(object sender, EventArgs e)
        {
            if (!loadFlag)
            {
                return;
            }

            int statusCount = 0;
            int.TryParse(textBox_status.Text, out statusCount);
            data_.dataDevice_.statusIoAddrStart =  textBox_status_start.Text;
            int statusStartAddr = 0;
            int.TryParse(textBox_status_start.Text, out statusStartAddr);

            int statusEndAdd = 0;
            if (statusCount == 0)
            {
                statusEndAdd = 0;
                textBox_status_end.Text = "";
                
            }
            else
            {
                statusEndAdd = statusStartAddr + statusCount - 1;
                textBox_status_end.Text = statusEndAdd.ToString();
            }

            data_.dataDevice_.statusIoAddrStart = textBox_status_start.Text;
            data_.dataDevice_.statusIoAddrEnd = textBox_status_end.Text;






        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!loadFlag)
            {
                return;
            }

            if (radioButton1.Checked == true)
            {
                data_.dataDevice_.transformMode = (int)TRANSFORMMODE.RTU;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!loadFlag)
            {
                return;
            }

            if (radioButton2.Checked == true)
            {
                data_.dataDevice_.transformMode = (int)TRANSFORMMODE.ASCII;
            }
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            if (!loadFlag)
            {
                return;
            }

            int.TryParse(textBox23.Text, out data_.dataDevice_.deviceAddr);
        }
        private void textBox_start_total_TextChanged(object sender, EventArgs e)
        {
            if (!loadFlag)
            {
                return;
            }

            data_.dataDevice_.IOAddrRange = textBox_start_total.Text;
        }

        private void textBox_length_total_TextChanged(object sender, EventArgs e)
        {
            if (!loadFlag)
            {
                return;
            }

            int.TryParse(textBox_length_total.Text, out data_.dataDevice_.IOAddrLength);
        }

        bool loadFlag = false;
        private void modbusslaveform_Load(object sender, EventArgs e)
        {
            loadFlag = false;
            textBox_coil.Text = data_.dataDevice_.coilCount.ToString();
            if(data_.dataDevice_.coilModbusAddrStart == "")
            {
                data_.dataDevice_.coilModbusAddrStart = modbusDiscreteStartAddr.ToString();
            }
            if (data_.dataDevice_.coilIoAddrEnd == "")
            {
                data_.dataDevice_.coilIoAddrEnd = modbusDiscreteEndAddr.ToString();
            }
            textBox_coil_start.Text = data_.dataDevice_.coilModbusAddrStart;
            textBox_coil_end.Text = data_.dataDevice_.coilIoAddrEnd;
            

            textBox_holding.Text = data_.dataDevice_.holdingCount.ToString();
            if(data_.dataDevice_.holdingIoAddrStart == "")
            {
                data_.dataDevice_.holdingIoAddrStart = modbusHoldingStartAddr.ToString();
            }
            if(data_.dataDevice_.holdingIoAddrEnd == "")
            {
                data_.dataDevice_.holdingIoAddrEnd = modbusHoldingStartAddr.ToString();
            }
            textBox_holding_start.Text = data_.dataDevice_.holdingIoAddrStart;
            textBox_holding_end.Text = data_.dataDevice_.holdingIoAddrEnd;

            textBox_lisan.Text = data_.dataDevice_.decreteCount.ToString();
            if(data_.dataDevice_.decreteIoAddrStart == "")
            {
                data_.dataDevice_.decreteIoAddrStart = modbusDiscreteStartAddr.ToString();
            }
            if(data_.dataDevice_.decreteIoAddrEnd == "")
            {
                data_.dataDevice_.decreteIoAddrEnd = modbusDiscreteStartAddr.ToString();
            }
            textBox_lisan_start.Text = data_.dataDevice_.decreteIoAddrStart;
            textBox_lisan_end.Text = data_.dataDevice_.decreteIoAddrEnd;

            textBox_status.Text = data_.dataDevice_.statusCount.ToString();
            if(data_.dataDevice_.statusIoAddrStart == "")
            {
                data_.dataDevice_.statusIoAddrStart = modbusInputStartAddr.ToString();
            }
            if(data_.dataDevice_.statusIoAddrEnd == "")
            {
                data_.dataDevice_.statusIoAddrEnd = modbusInputStartAddr.ToString();
            }

            textBox_status_start.Text = data_.dataDevice_.statusIoAddrStart;
            textBox_status_end.Text = data_.dataDevice_.statusIoAddrEnd;



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

            loadFlag = true;
        }

        private void textBox_status_start_Validated(object sender, EventArgs e)
        {
            if (!loadFlag)
            {
                return;
            }

        }

        private void modbusslaveform_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
