﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LocalPLC.ModbusMaster
{


    public partial class modbusmasterchannel : Form
    {
        public enum COLUMNNAME_CHANNLE : int
        {
            ID, NAME, MSGTYPE, POLLINGTIME, READOFFSET, READLENGTH,
            WRITEOFFSET, WRITELENGTH, NOTE
        };

        private DeviceData deviceData_ = null;
        private ModbusMasterData masterData_ = null;

        private int masterStartAddr_ = 0;
        private bool checkMasterLenthValid()
        {
            var length = masterData_.checkMasterLength();
            if(length > utility.modbusMudule)
            {
                string str = string.Format("整个{0}超过最大长度{1}限制!", length, utility.modbusMudule);
                MessageBox.Show(str);
                
                return false;
            }

            return true;
        }

        Dictionary<int, String> dicMsg = new Dictionary<int, String>();
        Dictionary<String, int> dicMsgType = new Dictionary<String, int>();
        HashSet<int> bitMsgTypeSet = new HashSet<int>();
        HashSet<int> byteMsgTypeSet = new HashSet<int>();

        public modbusmasterchannel()
        {
            InitializeComponent();

            bitMsgTypeSet.Clear();
            byteMsgTypeSet.Clear();
            dicMsg.Clear();
            dicMsgType.Clear();

            bitMsgTypeSet.Add(0x01);
            bitMsgTypeSet.Add(0x02);
            bitMsgTypeSet.Add(0x05);
            bitMsgTypeSet.Add(0x0F);

            byteMsgTypeSet.Add(0x03);
            byteMsgTypeSet.Add(0x04);
            byteMsgTypeSet.Add(0x06);
            byteMsgTypeSet.Add(0x10);

            //功能码
            dicMsg.Add(0x01, "读多个位(线圈) - 0x01");
            dicMsg.Add(0x02, "读多个位(离散输入) - 0x02");
            dicMsg.Add(0x03, "读多个字(保持寄存器) - 0x03");
            dicMsg.Add(0x04, "读多个字(输入寄存器) - 0x04");
            dicMsg.Add(0x05, "写单个位(线圈) - 0x05");
            dicMsg.Add(0x06, "写单个字(寄存器) - 0x06");
            dicMsg.Add(0x0F, "写多个位(线圈) - 0x0F");
            dicMsg.Add(0x10, "写多个字(寄存器) - 0x10");

            dicMsgType.Add("读多个位(线圈) - 0x01", 0x01);
            dicMsgType.Add("读多个位(离散输入) - 0x02", 0x02);
            dicMsgType.Add("读多个字(保持寄存器) - 0x03", 0x03);
            dicMsgType.Add("读多个字(输入寄存器) - 0x04", 0x04);
            dicMsgType.Add("写单个位(线圈) - 0x05", 0x05);
            dicMsgType.Add("写单个字(寄存器) - 0x06", 0x06);
            dicMsgType.Add("写多个位(线圈) - 0x0F", 0x0F);
            dicMsgType.Add("写多个字(寄存器) - 0x10", 0x10);

        }

        public void getDeviceData(ref DeviceData data, int masterStartAddr, ref ModbusMasterData masterData)
        {
            deviceData_ = data;
            masterStartAddr_ = masterStartAddr;
            masterData_ = masterData;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex < 0 | e.RowIndex < 0)
            {
                return;
            }
        }

        private void modbusmasterchannel_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn cellColumnID = new DataGridViewTextBoxColumn();
            cellColumnID.Name = "ID";
            DataGridViewTextBoxColumn cellColumnName = new DataGridViewTextBoxColumn();
            cellColumnName.Name = "名称";



            DataGridViewComboBoxColumn columnMsgType = new DataGridViewComboBoxColumn();
            columnMsgType.Name = "消息类型";
            columnMsgType.Items.Add("读多个位(线圈) - 0x01");
            columnMsgType.Items.Add("读多个位(离散输入) - 0x02");
            columnMsgType.Items.Add("读多个字(保持寄存器) - 0x03");
            columnMsgType.Items.Add("读多个字(输入寄存器) - 0x04");
            columnMsgType.Items.Add("写单个位(线圈) - 0x05");
            columnMsgType.Items.Add("写单个字(寄存器) - 0x06");
            columnMsgType.Items.Add("写多个位(线圈) - 0x0F");
            columnMsgType.Items.Add("写多个字(寄存器) - 0x10");


            DataGridViewTextBoxColumn cellColumnPolling = new DataGridViewTextBoxColumn();
            cellColumnPolling.Name = "循环触发事件";
            DataGridViewTextBoxColumn cellColumnReadOffset = new DataGridViewTextBoxColumn();
            cellColumnReadOffset.Name = "偏移";
            DataGridViewTextBoxColumn cellColumnReadLength = new DataGridViewTextBoxColumn();
            cellColumnReadLength.Name = "长度";
            DataGridViewTextBoxColumn cellColumnTriggerVar = new DataGridViewTextBoxColumn();
            cellColumnTriggerVar.ReadOnly = true;
            cellColumnTriggerVar.Name = "触发变量";
            DataGridViewTextBoxColumn cellColumnErrorVar = new DataGridViewTextBoxColumn();
            cellColumnErrorVar.ReadOnly = true;
            cellColumnErrorVar.Name = "错误变量";
            DataGridViewTextBoxColumn cellColumnNote = new DataGridViewTextBoxColumn();
            cellColumnNote.Name = "注释";
            //列标题自适应
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;


            dataGridView1.Columns.Add(cellColumnID);
            dataGridView1.Columns.Add(cellColumnName);
            dataGridView1.Columns.Add(columnMsgType);
            dataGridView1.Columns.Add(cellColumnPolling);
            dataGridView1.Columns.Add(cellColumnReadOffset);
            dataGridView1.Columns.Add(cellColumnReadLength);
            dataGridView1.Columns.Add(cellColumnTriggerVar);
            dataGridView1.Columns.Add(cellColumnErrorVar);
            dataGridView1.Columns.Add(cellColumnNote);
            for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
            {
                dataGridView1.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            dataGridView1.RowCount = 1 + deviceData_.modbusChannelList.Count;

            dataGridView1.Columns[(int)COLUMNNAME_CHANNLE.MSGTYPE].Width = 280;
            //dataGridView1.AllowUserToResizeColumns = true;

            int i = 0;
            foreach (ChannelData channelData in deviceData_.modbusChannelList)
            {
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.ID].Value = channelData.ID;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NAME].Value = channelData.nameChannel;

                ////
                if (!dicMsg.ContainsKey(channelData.msgType))
                { 
                    dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.MSGTYPE].Value = "";
                }
                else
                {
                    string value = dicMsg[channelData.msgType];
                    dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.MSGTYPE].Value = value;
                }
                

                //POLLINGTIME, READOFFSET, READLENGTH,
            //WRITEOFFSET, WRITELENGTH, NOTE


                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.POLLINGTIME].Value = channelData.pollingTime.ToString();

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READOFFSET].Value = channelData.readOffset;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READLENGTH].Value = channelData.readLength;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITEOFFSET].Value = channelData.writeOffset;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITELENGTH].Value = channelData.writeLength;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NOTE].Value = channelData.note;
                i++;
            }

            textBox1.Text =  deviceData_.nameDev.ToString();
            textBox3.Text = deviceData_.slaveAddr.ToString();

            //dataGridView1.RowCount = /*8*/ 1;
            dataGridView1.AutoSize = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (utility.masterDeviceChannleCountMax <= dataGridView1.RowCount)
            {
                string err = string.Format("通道最大个数是{0}", utility.masterDeviceCountMax);
                utility.PrintError(err);
                return;
            }
            
            int row = dataGridView1.RowCount;
            

            // Set the text for each button.
            int i = row;

            ChannelData data = new ChannelData();
            deviceData_.addChannel(data);

            //
            data.ID = row;
            data.nameChannel = "通道" + i.ToString();
            data.msgType = 0x01;
            //0x01 单bit
            data.curChannelLength = 1 + 2;

            data.pollingTime = 1000;
            data.readOffset = 0;
            data.readLength = 1;
            data.note = "";
            //

            //data_.modbusChannelList.Add(data);
            if (!checkMasterLenthValid())
            {
                deviceData_.removeChannel(data);
            }
            else
            {
                dataGridView1.RowCount += 1;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.ID].Value = data.ID;
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NAME].Value = data.nameChannel;
                //
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.MSGTYPE].Value = "读多个位(线圈) - 0x01";
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.POLLINGTIME].Value = data.pollingTime;    //ms
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READOFFSET].Value = data.readOffset;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READLENGTH].Value = data.readLength;
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITEOFFSET].Value = data.writeOffset.ToString();
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITELENGTH].Value = data.writeLength.ToString();

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NOTE].Value = "";
                data.note = dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NOTE].Value.ToString();

            }

        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            // int row = dataGridView1.SelectedRows[0];
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                return;
            }

            for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
            {
                int index = dataGridView1.SelectedRows[i].Index;

                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[i]);
                //deviceData_.modbusChannelList.RemoveAt(index);
                deviceData_.removeChannel(deviceData_.modbusChannelList[index]);
            }

            deviceData_.refreshAddr(deviceData_.curDeviceAddr);
            refreshGridTableTwoVarAddr();
        }

        private void refreshGridTableTwoVarAddr()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITEOFFSET].Value = deviceData_.modbusChannelList[i].writeOffset.ToString();
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITELENGTH].Value = deviceData_.modbusChannelList[i].writeLength.ToString();
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var obj = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string str = "";
            if (obj == null)
            {

            }
            else 
            {
                str = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (str.Equals(""))
                {

                }
            }



            if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.ID)
            {
                deviceData_.modbusChannelList.ElementAt(e.RowIndex).ID = int.Parse(str);
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.NAME)
            {
                deviceData_.modbusChannelList.ElementAt(e.RowIndex).nameChannel = str;
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.MSGTYPE)
            {
                //上一次消息类型值
                int lastMsgType = deviceData_.modbusChannelList.ElementAt(e.RowIndex).msgType;

                if (dicMsgType.ContainsKey(str))
                {
                    deviceData_.modbusChannelList.ElementAt(e.RowIndex).msgType = dicMsgType[str];
                }
                else
                {
                    deviceData_.modbusChannelList.ElementAt(e.RowIndex).msgType = -1;
                }

                //通道长度
                var channel = deviceData_.modbusChannelList.ElementAt(e.RowIndex);
                if (bitMsgTypeSet.Contains(channel.msgType))
                {
                    channel.setChannelLengthBit(channel.readLength);
                }
                else if (byteMsgTypeSet.Contains(channel.msgType))
                {
                    channel.setChannelLengthWord(channel.readLength);
                }

                if(!checkMasterLenthValid())
                {
                    //还原回上一次数值
                    channel.msgType = lastMsgType;

                    if (bitMsgTypeSet.Contains(channel.msgType))
                    {
                        channel.setChannelLengthBit(channel.readLength);
                    }
                    else if (byteMsgTypeSet.Contains(channel.msgType))
                    {
                        channel.setChannelLengthWord(channel.readLength);
                    }
                }

                //通道地址刷新
                deviceData_.refreshAddr(deviceData_.curDeviceAddr);
                //变量地址 错误变量 界面刷新
                refreshGridTableTwoVarAddr();
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.POLLINGTIME)
            {
                int.TryParse(str, out deviceData_.modbusChannelList.ElementAt(e.RowIndex).pollingTime);
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.READOFFSET)
            {
                int.TryParse(str, out deviceData_.modbusChannelList.ElementAt(e.RowIndex).readOffset);
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.READLENGTH)
            {
                int lastLength = deviceData_.modbusChannelList.ElementAt(e.RowIndex).readLength;
                
                int.TryParse(str, out deviceData_.modbusChannelList.ElementAt(e.RowIndex).readLength);
                //通道长度
                var channel = deviceData_.modbusChannelList.ElementAt(e.RowIndex);
                if(bitMsgTypeSet.Contains(channel.msgType))
                {
                    channel.setChannelLengthBit(channel.readLength);
                }
                else if(byteMsgTypeSet.Contains(channel.msgType))
                {
                    channel.setChannelLengthWord(channel.readLength);
                }

                if(!checkMasterLenthValid())
                {
                    //还原回上一次数值
                    channel.readLength = lastLength;

                    if (bitMsgTypeSet.Contains(channel.msgType))
                    {
                        channel.setChannelLengthBit(lastLength);
                    }
                    else if (byteMsgTypeSet.Contains(channel.msgType))
                    {
                        channel.setChannelLengthWord(lastLength);
                    }

                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = lastLength.ToString();

                }
                else
                {

                }

                //通道地址刷新
                deviceData_.refreshAddr(deviceData_.curDeviceAddr);
                //变量地址 错误变量 界面刷新
                refreshGridTableTwoVarAddr();
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.WRITEOFFSET)
            {
                int.TryParse(str, out deviceData_.modbusChannelList.ElementAt(e.RowIndex).writeOffset);
            }
            else if(e.ColumnIndex == (int)COLUMNNAME_CHANNLE.WRITELENGTH)
            {
                int.TryParse(str, out deviceData_.modbusChannelList.ElementAt(e.RowIndex).writeLength);
            }
            else if(e.ColumnIndex == (int)COLUMNNAME_CHANNLE.NOTE)
            {
                deviceData_.modbusChannelList.ElementAt(e.RowIndex).note = str;
            }
        }
    }
}
