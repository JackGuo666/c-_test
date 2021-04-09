using System;
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
            ID, MSGTYPE, TRIG_MODE,POLLINGTIME, READOFFSET, READLENGTH,
            WRITEOFFSET, WRITELENGTH, NAME, NOTE
        };

        private DeviceData deviceData_ = null;
        private ModbusMasterData masterData_ = null;
        private ModbusMasterManage mastermanage = null;

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
        private int MID;
        private int MDID;
        public void getDeviceData(ref DeviceData data, int masterStartAddr, ref ModbusMasterData masterData,ref ModbusMasterManage mmange,int masterid,int masterdevid)
        {
            deviceData_ = data;
            masterStartAddr_ = masterStartAddr;
            masterData_ = masterData;
            mastermanage = mmange;
            MID = masterid;
            MDID = masterdevid;
        }

       

        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex < 0 | e.RowIndex < 0)
            {
                return;
            }
            this.dataGridView1.BeginEdit(true);
        }

        private void modbusmasterchannel_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn cellColumnID = new DataGridViewTextBoxColumn();
            cellColumnID.Name = "ID";
            textBox1.Text = devicename;
            textBox2.Text = MID.ToString();
            textBox4.Text = deviceaddr;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox4.ReadOnly = true;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            DataGridViewComboBoxColumn columnMsgType = new DataGridViewComboBoxColumn();
            columnMsgType.Name = "功能码";
            columnMsgType.Items.Add("读多个位(线圈) - 0x01");
            columnMsgType.Items.Add("读多个位(离散输入) - 0x02");
            columnMsgType.Items.Add("读多个字(保持寄存器) - 0x03");
            columnMsgType.Items.Add("读多个字(输入寄存器) - 0x04");
            columnMsgType.Items.Add("写单个位(线圈) - 0x05");
            columnMsgType.Items.Add("写单个字(寄存器) - 0x06");
            columnMsgType.Items.Add("写多个位(线圈) - 0x0F");
            columnMsgType.Items.Add("写多个字(寄存器) - 0x10");

            DataGridViewTextBoxColumn cellColumntrig_mode = new DataGridViewTextBoxColumn();
            cellColumntrig_mode.Name = "触发方式（0为自动触发，1为手动触发）";
            DataGridViewTextBoxColumn cellColumnPolling = new DataGridViewTextBoxColumn();
            cellColumnPolling.Name = "循环触发事件";
            DataGridViewTextBoxColumn cellColumnReadOffset = new DataGridViewTextBoxColumn();
            cellColumnReadOffset.Name = "偏移";
            DataGridViewTextBoxColumn cellColumnReadLength = new DataGridViewTextBoxColumn();
            cellColumnReadLength.Name = "长度";
            DataGridViewTextBoxColumn cellColumnTriggerVar = new DataGridViewTextBoxColumn();
            //cellColumnTriggerVar.ReadOnly = true;
            cellColumnTriggerVar.Name = "触发变量";
            DataGridViewTextBoxColumn cellColumnErrorVar = new DataGridViewTextBoxColumn();
            //cellColumnErrorVar.ReadOnly = true;
            cellColumnErrorVar.Name = "错误变量";
            
            DataGridViewTextBoxColumn cellColumnName = new DataGridViewTextBoxColumn();
            cellColumnName.Name = "名称";
            DataGridViewTextBoxColumn cellColumnNote = new DataGridViewTextBoxColumn();
            cellColumnNote.Name = "注释";
            //列标题自适应
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;


            dataGridView1.Columns.Add(cellColumnID);
            dataGridView1.Columns.Add(columnMsgType);
            dataGridView1.Columns.Add(cellColumntrig_mode);
            dataGridView1.Columns.Add(cellColumnPolling);
            dataGridView1.Columns.Add(cellColumnReadOffset);
            dataGridView1.Columns.Add(cellColumnReadLength);
            dataGridView1.Columns.Add(cellColumnTriggerVar);
            dataGridView1.Columns.Add(cellColumnErrorVar);
            dataGridView1.Columns.Add(cellColumnName);
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

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.TRIG_MODE].Value = channelData.trig_mode;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.POLLINGTIME].Value = channelData.pollingTime.ToString();

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READOFFSET].Value = channelData.readOffset;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READLENGTH].Value = channelData.readLength;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITEOFFSET].Value = channelData.trigger;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITELENGTH].Value = channelData.error;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NOTE].Value = channelData.note;
                i++;
            }

            textBox1.Text =  deviceData_.nameDev.ToString();
            //textBox3.Text = deviceData_.slaveAddr.ToString();

            //dataGridView1.RowCount = /*8*/ 1;
            dataGridView1.AutoSize = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
        }
        int dn = 0;
        public void devicenumber(int a)
        {
            dn = a;
        }
        string deviceaddr;
        public void devaddr(string b)
        {
            deviceaddr = b;
        }
        string devicename;
        public void devname(string c)
        {
            devicename = c;
        }
        private int[] temrow = new int[16] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
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
            data.nameChannel = "master" + masterData_.ID.ToString() + "_d" + dn + "_c" + i.ToString();
            data.msgType = 0x01;
            //0x01 单bit 默认生成
            data.curChannelLength = 1 + 3;
            data.trig_mode = 0;
            data.pollingTime = 1000;
            data.readOffset = 0;
            data.readLength = 1;
            data.offsetkey[0] = deviceData_.resetkey[0];
            data.offsetkey[1] = deviceData_.resetkey[1];
            data.offsetkey[2] = data.ID.ToString();
            data.offsetkey1 = "0";
            data.offsetkey2 = "1";
            data.note = "";
            //

            //deviceData_.modbusChannelList.Add(data);
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
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.TRIG_MODE].Value = data.trig_mode;
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.POLLINGTIME].Value = data.pollingTime;    //ms
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READOFFSET].Value = data.readOffset;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READLENGTH].Value = data.readLength;
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITEOFFSET].Value = "";
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITELENGTH].Value = "";

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NOTE].Value = "";
                //data.note = dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NOTE].Value.ToString();

            }
            for (int j = 0; j < 16; j++)
            {
                if (temrow[j] == -1)
                {
                    temrow[j] = row;
                    break;
                }
            }

        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.SelectedRows[0].Index;
            try
            {
                if (dataGridView1.SelectedRows.Count <= 0)
                {
                    MessageBox.Show("请选择一整行进行删除");
                    return;
                }
                else
                {
                    dataGridView1.Rows[row].Visible = false;
                }
                //for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                //{
                //    int index = dataGridView1.SelectedRows[i].Index;

                //    dataGridView1.Rows.Remove(dataGridView1.SelectedRows[i]);
                //    //deviceData_.modbusChannelList.RemoveAt(index);
                //    deviceData_.removeChannel(deviceData_.modbusChannelList[index]);
                //}
            }
            catch
            {
                return;
            }
            //deviceData_.refreshAddr(deviceData_.curDeviceAddr);
            //refreshGridTableTwoVarAddr();
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
                //deviceData_.modbusChannelList.ElementAt(e.RowIndex).ID = int.Parse(str);
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.NAME)
            {
                //deviceData_.modbusChannelList.ElementAt(e.RowIndex).nameChannel = str;
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.MSGTYPE)
            {
                ////上一次消息类型值
                //int lastMsgType = deviceData_.modbusChannelList.ElementAt(e.RowIndex).msgType;

                //if (dicMsgType.ContainsKey(str))
                //{
                //    deviceData_.modbusChannelList.ElementAt(e.RowIndex).msgType = dicMsgType[str];
                //}
                //else
                //{
                //    deviceData_.modbusChannelList.ElementAt(e.RowIndex).msgType = -1;
                //}

                ////通道长度
                //var channel = deviceData_.modbusChannelList.ElementAt(e.RowIndex);
                //if (bitMsgTypeSet.Contains(channel.msgType))
                //{
                //    channel.setChannelLengthBit(channel.readLength);
                //}
                //else if (byteMsgTypeSet.Contains(channel.msgType))
                //{
                //    channel.setChannelLengthWord(channel.readLength);
                //}
               
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "写单个位(线圈) - 0x05" ||
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "写单个字(寄存器) - 0x06")
                {
                    dataGridView1.Rows[e.RowIndex].Cells["长度"].ReadOnly = true;
                    dataGridView1.Rows[e.RowIndex].Cells["长度"].Style.BackColor = Color.Gainsboro;
                    //channel.readLength = 1;
                    dataGridView1.Rows[e.RowIndex].Cells["长度"].Value = "1";
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells["长度"].ReadOnly = false;
                    dataGridView1.Rows[e.RowIndex].Cells["长度"].Style.BackColor = Color.White;
                }
                //if (!checkMasterLenthValid())
                //{
                //    //还原回上一次数值
                //    channel.msgType = lastMsgType;

                //    if (bitMsgTypeSet.Contains(channel.msgType))
                //    {
                //        channel.setChannelLengthBit(channel.readLength);
                //    }
                //    else if (byteMsgTypeSet.Contains(channel.msgType))
                //    {
                //        channel.setChannelLengthWord(channel.readLength);
                //    }
                //}

                ////通道地址刷新
                //deviceData_.refreshAddr(deviceData_.curDeviceAddr);
                //变量地址 错误变量 界面刷新
                //refreshGridTableTwoVarAddr();
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.TRIG_MODE)
            {
                if (Convert.ToInt32(str) != 0 && Convert.ToInt32(str) != 1)
                { MessageBox.Show("触发方式只能为0或者1"); }
                else
                { //deviceData_.modbusChannelList.ElementAt(e.RowIndex).trig_mode = Convert.ToInt32(str); 
                }
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.POLLINGTIME)
            {
                try
                {
                    if (Convert.ToInt32(str) <= 50)
                    {
                        MessageBox.Show("循环触发时间最小为50ms");
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1000;
                        return;
                    }
                    //else
                        //int.TryParse(str, out deviceData_.modbusChannelList.ElementAt(e.RowIndex).pollingTime);
                }
                catch
                {
                    MessageBox.Show("循环触发时间最小为50ms");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1000;
                    return;
                }
                
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.READOFFSET)
            {
                try
                {
                    Convert.ToInt32(str);
                    //int.TryParse(str, out deviceData_.modbusChannelList.ElementAt(e.RowIndex).readOffset);
                }
                catch
                {
                    MessageBox.Show("请输入数字");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    return;
                }
                //int.TryParse(str, out deviceData_.modbusChannelList.ElementAt(e.RowIndex).readOffset);
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.READLENGTH)
            {
                try
                {
                    Convert.ToInt32(str);
                    //int lastLength = deviceData_.modbusChannelList.ElementAt(e.RowIndex).readLength;
                    //int.TryParse(str, out deviceData_.modbusChannelList.ElementAt(e.RowIndex).readLength);
                    //通道长度
                    //var channel = deviceData_.modbusChannelList.ElementAt(e.RowIndex);
                    //if (bitMsgTypeSet.Contains(channel.msgType))
                    //{
                    //    channel.setChannelLengthBit(channel.readLength);
                    //}
                    //else if (byteMsgTypeSet.Contains(channel.msgType))
                    //{
                    //    channel.setChannelLengthWord(channel.readLength);
                    //}

                    //if (!checkMasterLenthValid())
                    //{
                    //    //还原回上一次数值
                    //    channel.readLength = lastLength;

                    //    if (bitMsgTypeSet.Contains(channel.msgType))
                    //    {
                    //        channel.setChannelLengthBit(lastLength);
                    //    }
                    //    else if (byteMsgTypeSet.Contains(channel.msgType))
                    //    {
                    //        channel.setChannelLengthWord(lastLength);
                    //    }

                    //    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = lastLength.ToString();

                    //}
                   

                    //通道地址刷新
                    //deviceData_.refreshAddr(deviceData_.curDeviceAddr);
                    //变量地址 错误变量 界面刷新
                    //refreshGridTableTwoVarAddr();
                }
                catch
                {
                    MessageBox.Show("请输入数字");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                    return;
                }
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.WRITEOFFSET)
            {
                //int.TryParse(str, out deviceData_.modbusChannelList.ElementAt(e.RowIndex).writeOffset);
                int flag = 0;

                for (int i = 0; i < mastermanage.modbusMastrList.Count; i++)
                {
                    for (int j = 0; j < mastermanage.modbusMastrList[i].modbusDeviceList.Count; j++)
                    {
                        if (str == mastermanage.modbusMastrList[i].modbusDeviceList[j].resetVaraible && str !="")
                        {
                            flag++;
                        }
                        for (int k = 0; k < mastermanage.modbusMastrList[i].modbusDeviceList[j].modbusChannelList.Count; k++)
                        {
                            if ((str == mastermanage.modbusMastrList[i].modbusDeviceList[j].modbusChannelList[k].trigger ||
                            str == mastermanage.modbusMastrList[i].modbusDeviceList[j].modbusChannelList[k].error) && str!=""&&
                            (i != MID || j!= MDID || k != e.RowIndex))
                            {
                                flag++;
                            }

                        }
                    }
                }
                for (int l = 0;l<dataGridView1.Rows.Count;l++)
                {
                    if(dataGridView1.Rows[l].Cells[6].Value.ToString() == str && l != e.RowIndex && str!="")
                    {
                        flag++;
                    }
                    if (dataGridView1.Rows[l].Cells[7].Value.ToString() == str  && str != "")
                    {
                        flag++;
                    }
                }
                if (flag == 0)
                {
                    //deviceData_.modbusChannelList[e.RowIndex].trigger = str;
                }
                else
                {
                    MessageBox.Show("输入的变量名有重复，请检查后重新输入");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = deviceData_.modbusChannelList[e.RowIndex].trigger;
                    //dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                }

                //deviceData_.modbusChannelList[e.RowIndex].trigger = str;
            }
            else if(e.ColumnIndex == (int)COLUMNNAME_CHANNLE.WRITELENGTH)
            {
                //int.TryParse(str, out deviceData_.modbusChannelList.ElementAt(e.RowIndex).writeLength);
                //deviceData_.modbusChannelList[e.RowIndex].error = str;
                int flag = 0;

                for (int i = 0; i < mastermanage.modbusMastrList.Count; i++)
                {
                    for (int j = 0; j < mastermanage.modbusMastrList[i].modbusDeviceList.Count; j++)
                    {
                        if (str == mastermanage.modbusMastrList[i].modbusDeviceList[j].resetVaraible && str != "")
                        {
                            flag++;
                        }
                        for (int k = 0; k < mastermanage.modbusMastrList[i].modbusDeviceList[j].modbusChannelList.Count; k++)
                        {
                            if ((str == mastermanage.modbusMastrList[i].modbusDeviceList[j].modbusChannelList[k].trigger ||
                            str == mastermanage.modbusMastrList[i].modbusDeviceList[j].modbusChannelList[k].error) && str !="" 
                            && (i != MID || j != MDID || k != e.RowIndex))
                            {
                                flag++;
                            }

                        }
                    }
                }
                for (int l = 0; l < dataGridView1.Rows.Count; l++)
                {
                    if (dataGridView1.Rows[l].Cells[6].Value.ToString() == str&& str!="")
                    {
                        flag++;
                    }
                    if (str == dataGridView1.Rows[l].Cells[7].Value.ToString() && l != e.RowIndex && str != "")
                    {
                        flag++;
                    }
                }
                if (flag == 0)
                {
                    //deviceData_.modbusChannelList[e.RowIndex].error = str;
                }
                else
                {
                    MessageBox.Show("输入的变量名有重复，请检查后重新输入");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = deviceData_.modbusChannelList[e.RowIndex].error;
                    //dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                }
            }
            else if(e.ColumnIndex == (int)COLUMNNAME_CHANNLE.NOTE)
            {
                //deviceData_.modbusChannelList.ElementAt(e.RowIndex).note = str;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 15; i >= 0; i--)
            {
                if (temrow[i] != -1)
                {
                    dataGridView1.Rows.RemoveAt(temrow[i]);
                    deviceData_.modbusChannelList.RemoveAt(temrow[i]);
                    
                }
            }
            this.Close();

        }
        private void refreshtemrow()
        {
            for (int i = 0; i < 16; i++)
            {
                temrow[i] = -1;
            }
        }
        private void refresh()
        {
            for (int i =0;i<deviceData_.modbusChannelList.Count;i++)
            {
                deviceData_.modbusChannelList[i].ID = i;
                dataGridView1.Rows[i].Cells[0].Value = i;
                deviceData_.modbusChannelList[i].nameChannel = "master" + masterData_.ID.ToString() + "_d" + dn + "_c" + i.ToString();
                dataGridView1.Rows[i].Cells["名称"].Value = "master" + masterData_.ID.ToString() + "_d" + dn + "_c" + i.ToString();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            refreshtemrow();
            for (int m = dataGridView1.Rows.Count-1; m >= 0; m--)
            {
                if (dataGridView1.Rows[m].Visible == false)
                {
                    dataGridView1.Rows.RemoveAt(m);
                    deviceData_.modbusChannelList.RemoveAt(m);
                    masterData_.refreshAddr();
                    deviceData_.refreshAddr(deviceData_.curDeviceAddr);
                }
            }
            refresh();
            int sum = 1;
            for (int i = 0; i < dataGridView1.Rows.Count;i++)
            {
                deviceData_.modbusChannelList[i].ID = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                deviceData_.modbusChannelList[i].nameChannel = dataGridView1.Rows[i].Cells[8].Value.ToString();
                //功能码
                //上一次消息类型值
                int lastMsgType = deviceData_.modbusChannelList[i].msgType;
                if (dicMsgType.ContainsKey(dataGridView1.Rows[i].Cells[1].Value.ToString()))
                {
                    deviceData_.modbusChannelList[i].msgType = dicMsgType[dataGridView1.Rows[i].Cells[1].Value.ToString()];
                }
                else
                {
                    deviceData_.modbusChannelList[i].msgType = -1;
                }

                //通道长度
                var channel = deviceData_.modbusChannelList[i];
                if (bitMsgTypeSet.Contains(channel.msgType))
                {
                    channel.setChannelLengthBit(channel.readLength);
                }
                else if (byteMsgTypeSet.Contains(channel.msgType))
                {
                    channel.setChannelLengthWord(channel.readLength);
                }
                //通道地址刷新
                deviceData_.refreshAddr(deviceData_.curDeviceAddr);

                deviceData_.modbusChannelList[i].trig_mode = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());

                int.TryParse(dataGridView1.Rows[i].Cells[3].Value.ToString(), out deviceData_.modbusChannelList[i].pollingTime);

                int.TryParse(dataGridView1.Rows[i].Cells[4].Value.ToString(), out deviceData_.modbusChannelList[i].readOffset);
                //长度
                int lastLength = deviceData_.modbusChannelList[i].readLength;
                int.TryParse(dataGridView1.Rows[i].Cells[5].Value.ToString(), out deviceData_.modbusChannelList[i].readLength);
                deviceData_.refreshAddr(deviceData_.curDeviceAddr);
                //通道长度
                //var channel = deviceData_.modbusChannelList[i];
                if (bitMsgTypeSet.Contains(channel.msgType))
                {
                    channel.setChannelLengthBit(channel.readLength);
                }
                else if (byteMsgTypeSet.Contains(channel.msgType))
                {
                    channel.setChannelLengthWord(channel.readLength);
                }
                //通道长度
                // 触发变量
                deviceData_.modbusChannelList[i].trigger = dataGridView1.Rows[i].Cells[6].Value.ToString();
                //错误变量
                deviceData_.modbusChannelList[i].error = dataGridView1.Rows[i].Cells[7].Value.ToString();
                deviceData_.modbusChannelList[i].note = dataGridView1.Rows[i].Cells[9].Value.ToString();
                masterData_.refreshAddr();
                deviceData_.refreshAddr(deviceData_.curDeviceAddr);

                sum += deviceData_.modbusChannelList[i].curChannelLength;
            }
            deviceData_.curDeviceLength = sum;

            //int a = deviceData_.curDeviceLength;
        }

        private void modbusmasterchannel_Shown(object sender, EventArgs e)
        {
            for (int l = 0; l < dataGridView1.Rows.Count; l++)
            {
                if (dataGridView1.Rows[l].Cells["功能码"].Value.ToString() == "写单个位(线圈) - 0x05" || dataGridView1.Rows[l].Cells["功能码"].Value.ToString() == "写单个字(寄存器) - 0x06")
                {
                    dataGridView1.Rows[l].Cells["长度"].ReadOnly = true;
                    dataGridView1.Rows[l].Cells["长度"].Style.BackColor = Color.Gainsboro;
                    dataGridView1.Rows[l].Cells["长度"].Value = 1;

                }
            }
       
        }
    }
}
