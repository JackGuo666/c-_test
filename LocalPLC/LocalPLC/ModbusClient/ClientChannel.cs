using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LocalPLC.ModbusClient
{
    public partial class ClientChannel : Form
    {
        private ComboBox functioncode1 = new ComboBox();
        modbusclient1 mct;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataSet dc = new DataSet();
        DataSet dc1 = new DataSet();
        int rownumber;
        //ClientChannel CC2;
        //public struct channeldate
        //{
        //    public int CID;
        //    public int CName;
        //    public int CFunction;
        //    public int CCycletime;
        //    public int CWriteoffset;
        //    public int CWritelength;
        //    public int CReadoffset;
        //    public int CReadlength;
        //    public string Cannotation;
        //};
        //public channeldate[] CD = new channeldate[16];

        public int a;
        public int devnumber;
        public enum COLUMNNAME_CHANNLE : int
        {
            ID, 功能码, 触发方式, 循环触发时间, 偏移, 长度, 触发变量, 错误变量, 名称, 注释
        };
        private DeviceData data_;
        public ModbusClientData data2 { get; set; }
        private ModbusClientManage clientmanage;
        Dictionary<int, String> dicMsg = new Dictionary<int, String>();
        Dictionary<String, int> dicMsgType = new Dictionary<String, int>();

        //public ClientChannel(int datagrid1cr)//int datagrid1cr)
        public ClientChannel()
        {
            InitializeComponent();
            dicMsg.Add(0x01, "1");
            dicMsg.Add(0x02, "2");
            dicMsg.Add(0x03, "3");
            dicMsg.Add(0x04, "4");
            dicMsg.Add(0x05, "5");
            dicMsg.Add(0x06, "6");
            dicMsg.Add(0x0F, "15");
            dicMsg.Add(0x10, "16");
            dicMsg.Add(0x00, "0");

            dicMsgType.Add("1", 0x01);
            dicMsgType.Add("2", 0x02);
            dicMsgType.Add("3", 0x03);
            dicMsgType.Add("4", 0x04);
            dicMsgType.Add("5", 0x05);
            dicMsgType.Add("6", 0x06);
            dicMsgType.Add("15", 0x0F);
            dicMsgType.Add("16", 0x10);
            dicMsgType.Add("0", 0);
            //this.rownumber = datagrid1cr;


            for (int i = 0; i < 16; i++)
            {
                ds.Tables.Add(new DataTable());
                ds.Tables[i].Columns.Add("ID", Type.GetType("System.Int32"));

                ds.Tables[i].Columns.Add("功能码", Type.GetType("System.String"));
                ds.Tables[i].Columns.Add("触发方式(手动/自动)", Type.GetType("System.String"));
                ds.Tables[i].Columns.Add("循环触发时间", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("偏移", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("长度", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("触发变量", Type.GetType("System.String"));
                ds.Tables[i].Columns.Add("错误变量", Type.GetType("System.String"));
                ds.Tables[i].Columns.Add("名称", Type.GetType("System.String"));
                ds.Tables[i].Columns.Add("注释", Type.GetType("System.String"));
            }
            dc.Tables.Add(new DataTable());
            dc.Tables[0].Columns.Add("functioncode");
            dc.Tables[0].Columns.Add("displayvalue");
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "读多个位(线圈)-Fc01", dc.Tables[0].NewRow()[1] = 0x01);
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "读多个位(离散输入)-Fc02", dc.Tables[0].NewRow()[1] = 0x02);
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "读多个字（保持寄存器）-Fc03", dc.Tables[0].NewRow()[1] = 0x03);
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "读多个字（输入寄存器）-Fc04", dc.Tables[0].NewRow()[1] = 0x04);
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "写单个位（线圈）-Fc05", dc.Tables[0].NewRow()[1] = 0x05);
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "写单个字（寄存器）-Fc06", dc.Tables[0].NewRow()[1] = 0x06);
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "写多个位（线圈）-Fc0F", dc.Tables[0].NewRow()[1] = 0x0F);
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "写多个字（寄存器）-Fc10", dc.Tables[0].NewRow()[1] = 0x10);
            //for (int i = 0; i < a; i++)
            //{

            //    dataGridView2[0, i].Value = CD[i].CID;
            //}
            dc1.Tables.Add(new DataTable());
            dc1.Tables[0].Columns.Add("functioncode");
            dc1.Tables[0].Columns.Add("displayvalue");
            dc1.Tables[0].Rows.Add(dc1.Tables[0].NewRow()[0] = "自动", dc1.Tables[0].NewRow()[1] = 0);
            dc1.Tables[0].Rows.Add(dc1.Tables[0].NewRow()[0] = "手动", dc1.Tables[0].NewRow()[1] = 1);
        }
        public string cn = null;
        public void ClientNumber(string a)
        {
            cn = a;

        }
        public string get()
        {
            return cn;
        }
        int CID;
        public void getModbusClientData(ref ModbusClientData data, ref ModbusClientManage modclientmanage, int clientid)
        {
            data2 = data;
            clientmanage = modclientmanage;
            CID = clientid;
        }
        public void getDeviceData(ref DeviceData data)
        {
            data_ = data;
            //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)] = data_;
        }


        //DataGridViewComboBoxColumn dc = new DataGridViewComboBoxColumn();
        private void ClientChannel_Load(object sender, EventArgs e)
        {
            //dataGridView2.Columns[-1].
            this.label6.Visible = false;
            //this.dataGridView2_CellEndEdit = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            ModbusClient.modbusclient1 mc1 = (modbusclient1)this.Owner;
            this.label3.Text = mc1.channelnumber.ToString();
            this.label5.Text = mc1.channelnumber.ToString();
            this.label8.Text = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].serverAddr.ToString();
            //data2.modbusDeviceList.Add(data_);
            //data2.modbusDeviceList.Insert(Convert.ToInt32(this.label3.Text),data_);
            dataGridView2.AllowUserToAddRows = false;
            //this.dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //列标题自适应
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewComboBoxColumn dc1 = new DataGridViewComboBoxColumn();
            dataGridView2.RowTemplate.Height = 30;
            dc1.DataSource = dc.Tables[0];
            dc1.DisplayMember = "functioncode";
            dc1.ValueMember = "displayvalue";
            dc1.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            //dataGridView2.Rows[i].Cells["功能码"] = cell;
            //dataGridView2.Columns.Insert(1, dc1);
            label6.Visible = false;
            //dataGridView2.RowCount = 1 + data_.modbusChannelList.Count;

            //if (ds.Tables[Convert.ToInt32(mc1.channelnumber)].Rows.Count < data_.modbusChannelList.Count)
            //if (ds.Tables[mc1.channelnumber].Rows.Count < data2.modbusDeviceList[mc1.channelnumber].modbusChannelList.Count)
            //{
            //    //int c = data_.modbusChannelList.Count - ds.Tables[mc1.channelnumber].Rows.Count;
            //    int c = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.Count - ds.Tables[mc1.channelnumber].Rows.Count;
            //    for (int k = 0; k < c; k++)
            //    {
            //        DataRow dr1 = ds.Tables[0].NewRow();
            //        ds.Tables[mc1.channelnumber].Rows.Add(dr1);
            //    }
            //}
            int c = 0;
            for (int x = 0; x < data2.modbusDeviceList.Count; x++)
            {
                int y = data2.modbusDeviceList[x].modbusChannelList.Count;
                if (ds.Tables[x].Rows.Count < data2.modbusDeviceList[x].modbusChannelList.Count)
                {
                    c = data2.modbusDeviceList[x].modbusChannelList.Count - ds.Tables[x].Rows.Count;
                    for (int k = 0; k < c; k++)
                    {
                        DataRow dr1 = ds.Tables[x].NewRow();
                        ds.Tables[x].Rows.Add(dr1.ItemArray);
                    }
                }
            }
            int z = ds.Tables[mc1.channelnumber].Rows.Count;
            //dataGridView2.Columns[(int)COLUMNNAME_CHANNLE.MSGTYPE].Width = 280;
            //dataGridView1.AllowUserToResizeColumns = true;
            //string y = data2.modbusDeviceList[mc1.channelnumber].modbusChannelList[0].nameChannel;
            //int u = data2.modbusDeviceList[mc1.channelnumber].modbusChannelList.Count;
            //ds.Tables[0].Rows[0][(int)COLUMNNAME_CHANNLE.名称] = data2.modbusDeviceList[mc1.channelnumber].modbusChannelList[0].nameChannel;
            //ModbusClientData data = new ModbusClientData();
            //string y = ds.Tables[0].Rows[0][(int)COLUMNNAME_CHANNLE.名称].ToString();


            //foreach (ChannelData channelData in data_.modbusChannelList)
            ////for (i=0;i<data.modbusDeviceList[j].modbusChannelList.Count;i++)
            ////foreach(ChannelData channelData in data2.modbusDeviceList[j].modbusChannelList)
            //{
            //    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.ID].Value = channelData.ID;
            //    ds.Tables[mc1.channelnumber].Rows[i][(int)COLUMNNAME_CHANNLE.ID] = channelData.ID;
            //    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NAME].Value = channelData.nameChannel;
            //    ds.Tables[mc1.channelnumber].Rows[i][(int)COLUMNNAME_CHANNLE.名称] = channelData.nameChannel;
            //    ////
            //    //if (!dicMsg.ContainsKey(channelData.msgType))
            //    //{
            //    //    dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.MSGTYPE].Value = "";
            //    //}
            //    //else
            //    //{
            //    //    string value = dicMsg[channelData.msgType];
            //    //    dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.MSGTYPE].Value = value;
            //    //}


            //    //POLLINGTIME, READOFFSET, READLENGTH,
            //    //WRITEOFFSET, WRITELENGTH, NOTE

            //    string value = dicMsg[channelData.msgType];
            //    ds.Tables[mc1.channelnumber].Rows[i][(int)COLUMNNAME_CHANNLE.功能码] = Convert.ToInt32(value);
            //    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.POLLINGTIME].Value = channelData.pollingTime.ToString();
            //    ds.Tables[mc1.channelnumber].Rows[i][(int)COLUMNNAME_CHANNLE.循环触发时间] = channelData.pollingTime.ToString();
            //    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READOFFSET].Value = channelData.readOffset;
            //    ds.Tables[mc1.channelnumber].Rows[i][(int)COLUMNNAME_CHANNLE.偏移] = channelData.Offset.ToString();
            //    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.读长度].Value = channelData.readLength;
            //    ds.Tables[mc1.channelnumber].Rows[i][(int)COLUMNNAME_CHANNLE.长度] = channelData.Length.ToString();
            //    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.写偏移].Value = channelData.writeOffset;
            //    ds.Tables[mc1.channelnumber].Rows[i][(int)COLUMNNAME_CHANNLE.触发变量] = channelData.trigger_offset.ToString();
            //    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.写长度].Value = channelData.writeLength;
            //    ds.Tables[mc1.channelnumber].Rows[i][(int)COLUMNNAME_CHANNLE.错误变量] = channelData.error_offset.ToString();
            //    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.注释].Value = channelData.note;
            //    ds.Tables[mc1.channelnumber].Rows[i][(int)COLUMNNAME_CHANNLE.注释] = channelData.note.ToString();
            //    i++;
            //}
            //i = 0;
            //int j = 0;
            //foreach (DeviceData devicedata in data2.modbusDeviceList)
            for (int m = 1; m < data2.modbusDeviceList.Count; m++)
            {

                int number = 1;
                while (m >= number && data2.modbusDeviceList[m - number].modbusChannelList.Count == 0)
                {
                    number++;
                }

                if (number > m && this.label3.Text != 0.ToString())
                {

                    MessageBox.Show("请先在该设备前任一设备中添加一条通道");
                    this.Close();
                    //number = 1;
                    return;

                }

                //else
                //{
                //    if (this.label3.Text == 0.ToString())
                //    {       number = 1;}
                //        int channelcount = data2.modbusDeviceList[m - number].modbusChannelList.Count;
                //        if (data2.modbusDeviceList[m].modbusChannelList.Count > 0)
                //        {
                //            if (Convert.ToInt32(data2.modbusDeviceList[m].modbusChannelList[0].trigger_offset) -
                //                Convert.ToInt32(data2.modbusDeviceList[m - number].modbusChannelList[channelcount - 1].trigger_offset) !=
                //                data2.modbusDeviceList[m - number].modbusChannelList[channelcount - 1].Channellength)
                //            {
                //                int diff = Convert.ToInt32(data2.modbusDeviceList[m].modbusChannelList[0].trigger_offset) -
                //                 Convert.ToInt32(data2.modbusDeviceList[m - number].modbusChannelList[channelcount - 1].trigger_offset) -
                //                 data2.modbusDeviceList[m - number].modbusChannelList[channelcount - 1].Channellength;
                //                int n = 0; ;
                //                for (n = m; n < data2.modbusDeviceList.Count; n++)
                //                    for (int o = 0; o < data2.modbusDeviceList[n].modbusChannelList.Count; o++)
                //                    {
                //                        data2.modbusDeviceList[n].modbusChannelList[o].trigger_offset =
                //                            (Convert.ToInt32(data2.modbusDeviceList[n].modbusChannelList[o].trigger_offset) - diff).ToString();
                //                        data2.modbusDeviceList[n].modbusChannelList[o].error_offset =
                //                            (Convert.ToInt32(data2.modbusDeviceList[n].modbusChannelList[o].error_offset) - diff).ToString();
                //                        data2.modbusDeviceList[n].modbusChannelList[o].channelstartaddr =
                //                            data2.modbusDeviceList[n].modbusChannelList[o].channelstartaddr - diff;
                //                    }
                //            }
                //        }

                //}

            }
            for (int j = 0; j < data2.modbusDeviceList.Count; j++)
            {
                //foreach (ChannelData channelData in data2.modbusDeviceList[j].modbusChannelList)
                for (int i = 0; i < data2.modbusDeviceList[j].modbusChannelList.Count; i++)
                //foreach(ChannelData channelData in data2.modbusDeviceList[j].modbusChannelList)
                {
                    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.ID].Value = channelData.ID;
                    ds.Tables[j].Rows[i][(int)COLUMNNAME_CHANNLE.ID] = data2.modbusDeviceList[j].modbusChannelList[i].ID;
                    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NAME].Value = channelData.nameChannel;
                    ds.Tables[j].Rows[i][(int)COLUMNNAME_CHANNLE.名称] = data2.modbusDeviceList[j].modbusChannelList[i].nameChannel;
                    ////
                    //if (!dicMsg.ContainsKey(channelData.msgType))
                    //{
                    //    dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.MSGTYPE].Value = "";
                    //}
                    //else
                    //{
                    //    string value = dicMsg[channelData.msgType];
                    //    dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.MSGTYPE].Value = value;
                    //}


                    //POLLINGTIME, READOFFSET, READLENGTH,
                    //WRITEOFFSET, WRITELENGTH, NOTE

                    string value = dicMsg[data2.modbusDeviceList[j].modbusChannelList[i].msgType];
                    ds.Tables[j].Rows[i][(int)COLUMNNAME_CHANNLE.功能码] = Convert.ToInt32(value);
                    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.POLLINGTIME].Value = channelData.pollingTime.ToString();
                    ds.Tables[j].Rows[i][(int)COLUMNNAME_CHANNLE.触发方式] = data2.modbusDeviceList[j].modbusChannelList[i].trig_mode;
                    ds.Tables[j].Rows[i][(int)COLUMNNAME_CHANNLE.循环触发时间] = data2.modbusDeviceList[j].modbusChannelList[i].pollingTime.ToString();
                    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READOFFSET].Value = channelData.readOffset;
                    ds.Tables[j].Rows[i][(int)COLUMNNAME_CHANNLE.偏移] = data2.modbusDeviceList[j].modbusChannelList[i].Offset.ToString();
                    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.读长度].Value = channelData.readLength;
                    ds.Tables[j].Rows[i][(int)COLUMNNAME_CHANNLE.长度] = data2.modbusDeviceList[j].modbusChannelList[i].Length.ToString();
                    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.写偏移].Value = channelData.writeOffset;
                    ds.Tables[j].Rows[i][(int)COLUMNNAME_CHANNLE.触发变量] = data2.modbusDeviceList[j].modbusChannelList[i].trigger_offset.ToString();
                    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.写长度].Value = channelData.writeLength;
                    ds.Tables[j].Rows[i][(int)COLUMNNAME_CHANNLE.错误变量] = data2.modbusDeviceList[j].modbusChannelList[i].error_offset.ToString();
                    //dataGridView2.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.注释].Value = channelData.note;
                    ds.Tables[j].Rows[i][(int)COLUMNNAME_CHANNLE.注释] = data2.modbusDeviceList[j].modbusChannelList[i].note.ToString();
                    //i++;
                }
                //j++;
            }
          
            this.dataGridView2.BeginInvoke(new Action(() => { this.dataGridView2.DataSource = ds.Tables[mc1.channelnumber]; }));
            //dataGridView2.AutoSize = true;
            dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            //dataGridView2.Columns[6].ReadOnly = true;
            //dataGridView2.Columns[7].ReadOnly = true;
            devnumber = Convert.ToInt32(mc1.clientnumber);
            
        }
        public int clientstartaddr;
        public int devstartaddr;
        public int channelstartaddr;


        private int[] temrow = new int[16] { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1};
        
        private void add_Click(object sender, EventArgs e)
        {
            if (utility.masterDeviceChannleCountMax <= dataGridView2.RowCount)
            {
                string err = string.Format("通道最大个数是{0}", utility.masterDeviceChannleCountMax);
                utility.PrintError(err);
                return;
            }

            //int rowcount = dataGridView2.RowCount;
            //this.dataGridView2.Rows.Add(rowcount, "设备" + rowcount.ToString(), functioncode1, "", "", "", "", "");
            //int z = ds.Tables[0].Rows.Count;
            //modbusclient1 mct1 = new modbusclient1();
            //int l = Convert.ToInt32(mct1.clientnumber);
            //clientstartaddr = 1000*(devnumber+1);
            //int lastendaddr = 1000*(Convert.ToInt32( mci.clientnumber)+1);


            int n = Convert.ToInt32(this.label3.Text);
            int rowcount = dataGridView2.Rows.Count;
            //if(n > 0)
            //{
            //    int b = ds.Tables[n - 1].Rows.Count;
            //    if(b == 0)
            //    {
            //        MessageBox.Show("请先在上一设备中添加一条通道");
            //        this.Close();
            //        return;                    
            //    }
            //    int c = Convert.ToInt32(ds.Tables[n-1].Rows[b-1]["错误变量"]);
            //    devstartaddr = c+1;
            //    //data_.modbusChannelList
            //}
            //else if(n == 0)
            //{
            //    devstartaddr = clientstartaddr;
            //}
            //channelstartaddr = devstartaddr;
            ////for (int i=0;i<n+1;i++)
            ////{
           

            //for (int j = 0; j < ds.Tables[n].Rows.Count; j++)
            //{
            //    //int d = data2.modbusDeviceList[n].modbusChannelList[j].Channellength;
            //    channelstartaddr += data2.modbusDeviceList[n].modbusChannelList[j].Channellength;

            //}
            //}
            DataRow dr = ds.Tables[0].NewRow();
            ChannelData data = new ChannelData();
            int type = 0;
            dr[(int)COLUMNNAME_CHANNLE.ID] = dataGridView2.RowCount;
            data.ID = dataGridView2.RowCount;         
            dr[(int)COLUMNNAME_CHANNLE.功能码] = 1;
            data.msgType = Convert.ToInt32(dr[(int)COLUMNNAME_CHANNLE.功能码]);
            data.msgdiscrib = "读多个位(线圈)-0x01";
            dr[(int)COLUMNNAME_CHANNLE.触发方式] = 0;
            data.trig_mode = Convert.ToInt32(dr[(int)COLUMNNAME_CHANNLE.触发方式]);
            dr[(int)COLUMNNAME_CHANNLE.循环触发时间] = 1000;
            data.pollingTime =Convert.ToInt32(dr[(int)COLUMNNAME_CHANNLE.循环触发时间]);
            dr[(int)COLUMNNAME_CHANNLE.偏移] = 0;
            data.Offset = Convert.ToInt32(dr[(int)COLUMNNAME_CHANNLE.偏移]);
            dr[(int)COLUMNNAME_CHANNLE.长度] = 1;
            data.Length = Convert.ToInt32(dr[(int)COLUMNNAME_CHANNLE.长度]);
            dr[(int)COLUMNNAME_CHANNLE.触发变量] = null;
            data.trigger_offset = (dr[(int)COLUMNNAME_CHANNLE.触发变量].ToString());
            dr[(int)COLUMNNAME_CHANNLE.错误变量] = null;
            data.error_offset = (dr[(int)COLUMNNAME_CHANNLE.错误变量].ToString());
            
            dr[(int)COLUMNNAME_CHANNLE.名称] = "client_" + cn + "_device_" + label3.Text + "_channel_" + data.ID;
            //"通道" + dataGridView2.RowCount;
            data.nameChannel = dr[(int)COLUMNNAME_CHANNLE.名称].ToString();
            dr[(int)COLUMNNAME_CHANNLE.注释] = "";
            data.note = dr[(int)COLUMNNAME_CHANNLE.注释].ToString();
            if(rowcount > 0)
            {
                data.channelstartaddr = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[rowcount - 1].channelstartaddr +
                    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[rowcount - 1].Channellength ;
            }
            else if (rowcount == 0)
            {
                data.channelstartaddr = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].devstartaddr + 1;
            }

            if (data.msgType ==1 || data.msgType == 2 || data.msgType == 5 || data.msgType == 15)
            {
                type = 0;
                data.Channellength = 3 + data.Length;
            }
            else //if(data.msgType == 3 || data.msgType == 4 || data.msgType == 6 || data.msgType == 16)
            {
                type = 1;
                data.Channellength = 3 + data.Length * 2;
            }
            data.type = type;
            data.offsetkey[0] = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].resetkey[0];
            data.offsetkey[1] = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].resetkey[1];
            data.offsetkey[2] = data.ID.ToString();
            data.offsetkey1 = "0";
            data.offsetkey2 = "1";
            //if (Convert.ToInt32(this.label3.Text) > 9)
            //{
            //    data.offsetkey[1] = this.label3.Text;
            //}
            //else if (Convert.ToInt32(this.label3.Text) < 10 && Convert.ToInt32(this.label3.Text)>=0)
            //{
            //    data.offsetkey[1] = 
            //}
            //data.offsetkey[1] = 
            data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.Add(data);
            data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].devlength = 1;
            for (int j =0;j<data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.Count;j++)
            {
                data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].devlength += data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[j].Channellength;
            }
            //data_.modbusChannelList.Add(data);
            //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)]
            
            ds.Tables[n].Rows.Add(dr.ItemArray);
            
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
                cell.DataSource = dc.Tables[0];
                cell.DisplayMember = "functioncode";
                cell.ValueMember = "displayvalue";
                cell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                dataGridView2.Rows[i].Cells["功能码"] = cell;
                DataGridViewComboBoxCell cell1 = new DataGridViewComboBoxCell();
                cell1.DataSource = dc1.Tables[0];
                cell1.DisplayMember = "functioncode";
                cell1.ValueMember = "displayvalue";
                cell1.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                dataGridView2.Rows[i].Cells["触发方式(手动/自动)"] = cell1;
            }

            for (int j =0;j<16;j++)
            {
                if(temrow[j] == -1)
                {
                    temrow[j] = rowcount;
                    break;
                }
            }

        }
        public void refresh()
        {
            data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].devlength = 1;
            for (int i = 0; i < data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.Count;i++)
            {
                data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].ID = i;
                //temrow[i] = i;
                ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[i][0] = i;
                if (data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].nameChannel == dataGridView2.Rows[i].Cells["名称"].Value.ToString()&& 
                    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].edit == 0)
                {
                    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].nameChannel = "client_" + cn + "_device_" + label3.Text + "_channel_" + i;
                    ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[i]["名称"] = "client_" + cn + "_device_" + label3.Text + "_channel_" + i;
                }
                //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i - 1].Channellength = 2 + 
                if (i > 0)
                {
                    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].channelstartaddr =
                        data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i - 1].channelstartaddr +
                        data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i - 1].Channellength;
                }
                else if (i==0)
                {
                    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].channelstartaddr =
                        data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].devstartaddr + 1;
                }
                data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].devlength += data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Channellength;
                int bbb = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Channellength;
            }
            int aaa = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].devlength;

        }

       
        private int selectrow = -1;
        
        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                //int n = Convert.ToInt32(dataGridView2.SelectedRows[0].Index);
                int n = dataGridView2.SelectedCells[0].RowIndex;
                //this.dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[0].Index);
                if (dataGridView2.SelectedRows.Count != 1)
                {
                    MessageBox.Show("请选择一整行进行删除");
                    return;
                }
                else if (dataGridView2.SelectedRows.Count == 1)
                {
                    //ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[n].Delete();
                    ////data_.modbusChannelList.RemoveAt(n);
                    //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.RemoveAt(n);
                    //refresh();
                    CurrencyManager cm = (CurrencyManager)BindingContext[dataGridView2.DataSource];
                    cm.SuspendBinding(); //挂起数据绑定
                    dataGridView2.Rows[n].Visible = false;
                    cm.ResumeBinding(); //恢复数据绑定
                }
            }
            catch
            {
                return;
            }
            //if (n>0)
            //{
                 
            //    for (int i = n ; i < dataGridView2.Rows.Count; i++)
            //    {
            //        ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[i]["触发变量"] =
            //          Convert.ToInt32(ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[i - 1]["触发变量"]) +
            //           data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i-1].Channellength;
            //        data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].channelstartaddr =
            //            Convert.ToInt32(ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[i]["触发变量"]) + 2;

            //    }
            //}
           
        }
        private void refreshtemrow()
        {
            for(int i =0;i<16;i++)
            {
                temrow[i] = -1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshtemrow();
            bool b = true;
            if (dataGridView2.Rows.Count > 0)
            { b = dataGridView2.Rows[0].Visible; }
            for (int m = dataGridView2.Rows.Count-1;m>=0;m--)
            {
                if(dataGridView2.Rows[m].Visible == false)
                {
                    ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[m].Delete();
                    //data_.modbusChannelList.RemoveAt(n);
                    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.RemoveAt(m);
                    refresh();
                }
            }
            if (b == false && dataGridView2.Rows.Count>0)
            {
                ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[0].Delete();
                data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.RemoveAt(0);
            }
            refresh();
            int rowcount = this.dataGridView2.RowCount;
            a = rowcount;
            
            for(int i = 0;i<rowcount;i++)
            {
                data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].ID = int.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString());
               // data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].nameChannel = dataGridView2.Rows[i].Cells["名称"].Value.ToString();
                
                //功能码
                string str = dataGridView2.Rows[i].Cells[1].Value.ToString();
                data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].msgType = dicMsgType[str];
                int type = 0;
                if (dicMsgType[str] == 1 || dicMsgType[str] == 2 || dicMsgType[str] == 5 || dicMsgType[str] == 15)
                {
                    type = 0;
                    int c = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Channellength;
                    //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Channellength = 
                    //    2 + data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Length / 8 + 1;
                    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Channellength =
                        3 + data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Length;
                }
                else //if (dicMsgType[str] == 3 || dicMsgType[str] == 4 || dicMsgType[str] == 6 || dicMsgType[str] == 16)
                {
                    type = 1;
                    int c = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Channellength;
                    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Channellength =
                        3 + data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Length * 2;
                }
                if (dicMsgType[str] == 5 || dicMsgType[str] == 6)
                {
                    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Length = 1;
                    dataGridView2.Rows[i].Cells["长度"].Value = 1;
                    dataGridView2.Rows[i].Cells["长度"].ReadOnly = true;
                    dataGridView2.Rows[i].Cells["长度"].Style.BackColor = Color.Gainsboro;
                }
                else
                {
                    dataGridView2.Rows[i].Cells["长度"].ReadOnly = false;
                    dataGridView2.Rows[i].Cells["长度"].Style.BackColor = Color.White;
                }
                refresh();
                int a = Convert.ToInt32(str) - 1;
                if (a == 14)
                {
                    a = 6;
                }
                else if (a == 15)
                {
                    a = 7;
                }
                //data_.modbusChannelList.ElementAt(e.RowIndex).msgdiscrib = dc.Tables[0].Rows[a][0].ToString();
                data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].msgdiscrib = dc.Tables[0].Rows[a][0].ToString();
                int.TryParse(dataGridView2.Rows[i].Cells[2].Value.ToString(), out data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].trig_mode);
                int.TryParse(dataGridView2.Rows[i].Cells[3].Value.ToString(), out data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].pollingTime);
                int.TryParse(dataGridView2.Rows[i].Cells[4].Value.ToString(), out data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Offset);
                int.TryParse(dataGridView2.Rows[i].Cells[5].Value.ToString(), out data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Length);
                //int type = 0;
                int msg = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].msgType;
                if (type == 0)
                {
                    //type = 0;
                    int c = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Channellength;
                    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Channellength =
                        3 + data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Length;
                    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].type = 0;
                }
                else if (type == 1)
                {
                    //type = 1;
                    int c = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Channellength;
                    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Channellength =
                        3 + data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].Length * 2;
                    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].type = 1;
                }
                refresh();
                data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].trigger_offset = dataGridView2.Rows[i].Cells[6].Value.ToString();
                data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].error_offset = dataGridView2.Rows[i].Cells[7].Value.ToString();
                data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].note = dataGridView2.Rows[i].Cells[9].Value.ToString();
                
            }
            
            for (int j = 0; j < rowcount; j++)
            {
                if (data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[j].nameChannel != dataGridView2.Rows[j].Cells["名称"].Value.ToString())
                {  
                     data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[j].nameChannel = dataGridView2.Rows[j].Cells["名称"].Value.ToString();   
                }
               
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //data2.modbusDeviceList.Add(data_);
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    for (int i = 15; i >= 0; i--)
                    {
                        int a = temrow[i];
                        if (temrow[i] != -1)
                        {
                            ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[temrow[i]].Delete();
                            data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.RemoveAt(temrow[i]);
                        }
                    }
                    refreshtemrow();
                    this.Close();
                }
                else
                {
                    refreshtemrow();
                    this.Close();
                }
            }
            catch
            {
                refreshtemrow();
                this.Close();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex == -1)
            {
                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
                    cell.DataSource = dc.Tables[0];
                    cell.DisplayMember = "functioncode";
                    cell.ValueMember = "displayvalue";
                    cell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    dataGridView2.Rows[i].Cells["功能码"] = cell;
                }
                //return;
            }
            else
            {
                


                DataRowView row1 = (DataRowView)dataGridView2.Rows[e.RowIndex].DataBoundItem;
                if (row1.Row.RowState == DataRowState.Unchanged)
                    return;
                row1.Row.AcceptChanges();
            }
        }

        private void ClientChannel_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
                cell.DataSource = dc.Tables[0];
                cell.DisplayMember = "functioncode";
                cell.ValueMember = "displayvalue";
                cell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                dataGridView2.Rows[i].Cells["功能码"] = cell;
                DataGridViewComboBoxCell cell1 = new DataGridViewComboBoxCell();
                cell1.DataSource = dc1.Tables[0];
                cell1.DisplayMember = "functioncode";
                cell1.ValueMember = "displayvalue";
                cell1.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                dataGridView2.Rows[i].Cells["触发方式(手动/自动)"] = cell1;
            }
            for (int l = 0; l < dataGridView2.Rows.Count; l++)
            {
                if (dataGridView2.Rows[l].Cells["功能码"].Value.ToString() == "5" || dataGridView2.Rows[l].Cells["功能码"].Value.ToString() == "6")
                {
                    dataGridView2.Rows[l].Cells["长度"].ReadOnly = true;
                    dataGridView2.Rows[l].Cells["长度"].Style.BackColor = Color.Gainsboro;
                    dataGridView2.Rows[l].Cells["长度"].Value = 1;

                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //for (int i = 0; i < this.dataGridView2.Columns.Count; i++)
            //{
            //    this.dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
            
            if (e.RowIndex == -1)
            {
                //for (int i = 0; i < dataGridView2.RowCount; i++)
                //{
                //    DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
                //    cell.DataSource = dc.Tables[0];
                //    cell.DisplayMember = "functioncode";
                //    cell.ValueMember = "displayvalue";
                //    cell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                //    dataGridView2.Rows[i].Cells["功能码"] = cell;
                //}

                //return;
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            else
            {
                if (e.ColumnIndex < 0)
                {
                    selectrow = dataGridView2.SelectedRows[0].Index;

                }
                else if (e.ColumnIndex >= 0)
                {
                    selectrow = e.RowIndex;
                }
                this.dataGridView2.BeginEdit(true);
            }
        }

        ModbusMaster.ModbusMasterManage masterManage = new ModbusMaster.ModbusMasterManage();
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Object obj = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string str = "";
            if (obj == null)
            {

            }
            else
            {
                str = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (str.Equals(""))
                {

                }
            }
            if (e.RowIndex <0)
            {
                return;
            }


            if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.ID)
            {
                //data_.modbusChannelList.ElementAt(e.RowIndex).ID = int.Parse(str);
                //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.ElementAt(e.RowIndex).ID = int.Parse(str);
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.名称)
            {
                //data_.modbusChannelList.ElementAt(e.RowIndex).nameChannel = str;
                //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.ElementAt(e.RowIndex).nameChannel = str;
                for(int i = 0;i<dataGridView2.Rows.Count;i++)
                {
                    if(str == dataGridView2.Rows[i].Cells[8].Value.ToString()&&str!=""&&i!=e.RowIndex)
                    {
                        MessageBox.Show("设备名有重复");
                        dataGridView2.Rows[e.RowIndex].Cells[8].Value = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].nameChannel;
                        return;
                    }
                }
                if(dataGridView2.Rows[e.RowIndex].Cells[8].Value.ToString() != data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].nameChannel)
                {
                    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].edit = 1;
                }
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.功能码)
            {
                //if (dicMsgType.ContainsKey(str))
                //{
                //    data_.modbusChannelList.ElementAt(e.RowIndex).msgType = dicMsgType[str];
                //}
                //else
                //{
                //    data_.modbusChannelList.ElementAt(e.RowIndex).msgType = -1;
                //}
                //data_.modbusChannelList.ElementAt(e.RowIndex).msgType = dicMsgType[str];
                //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.ElementAt(e.RowIndex).msgType = dicMsgType[str];
                //int type = 0;
                //if (dicMsgType[str] == 1 || dicMsgType[str] == 2 || dicMsgType[str] == 5 || dicMsgType[str] == 15)
                //{
                //    type = 0;
                //    int c = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Channellength;
                //    //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Channellength = 
                //    //    2 + data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Length / 8 + 1;
                //    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Channellength =
                //        3 + data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Length;
                //}
                //else //if (dicMsgType[str] == 3 || dicMsgType[str] == 4 || dicMsgType[str] == 6 || dicMsgType[str] == 16)
                //{
                //    type = 1;
                //    int c = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Channellength;
                //    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Channellength = 
                //        3 + data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Length * 2;
                //}
                if (dicMsgType[str] == 5 || dicMsgType[str] == 6)
                {
                    //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Length = 1;
                    dataGridView2.Rows[e.RowIndex].Cells["长度"].Value = 1;
                    dataGridView2.Rows[e.RowIndex].Cells["长度"].ReadOnly = true;
                    dataGridView2.Rows[e.RowIndex].Cells["长度"].Style.BackColor = Color.Gainsboro;
                }
                else
                {
                    dataGridView2.Rows[e.RowIndex].Cells["长度"].ReadOnly = false;
                    dataGridView2.Rows[e.RowIndex].Cells["长度"].Style.BackColor = Color.White;
                }
                //refresh();
                ////for (int i = e.RowIndex + 1; i < dataGridView2.Rows.Count; i++)
                ////{
                ////    int a1 = Convert.ToInt32(ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[i - 1]["触发变量"]);
                ////    int b = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i - 1].Channellength;
                ////    ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[i]["触发变量"] =
                ////       Convert.ToInt32(ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[i - 1]["触发变量"]) +
                ////        data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i - 1].Channellength;
                ////    ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[i]["错误变量"] =
                ////         Convert.ToInt32(ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[i]["触发变量"]) + 1;
                ////    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[i].channelstartaddr =
                ////         Convert.ToInt32(ds.Tables[Convert.ToInt32(this.label3.Text)].Rows[i]["触发变量"]) + 2;
                ////}


                //int a = Convert.ToInt32(str)-1;
                //if(a == 14)
                //{
                //    a = 6;
                //}
                //else if (a == 15)
                //{
                //    a = 7;
                //}
                ////data_.modbusChannelList.ElementAt(e.RowIndex).msgdiscrib = dc.Tables[0].Rows[a][0].ToString();
                //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.ElementAt(e.RowIndex).msgdiscrib = dc.Tables[0].Rows[a][0].ToString();
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.触发方式)
            {
                if (Convert.ToInt32(str) != 0 && Convert.ToInt32(str) != 1)
                { 
                    MessageBox.Show("请选择手动或者自动");
                    dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                }
                else
                { 
                    //int.TryParse(str, out data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.ElementAt(e.RowIndex).trig_mode); 
                    if(Convert.ToInt32(str) == 1)
                    {
                        dataGridView2.Rows[e.RowIndex].Cells["循环触发时间"].Style.BackColor = Color.Gainsboro;
                        dataGridView2.Rows[e.RowIndex].Cells["循环触发时间"].ReadOnly = true;
                    }
                    else
                    {
                        dataGridView2.Rows[e.RowIndex].Cells["循环触发时间"].Style.BackColor = dataGridView2.Rows[e.RowIndex].Cells[0].Style.BackColor;
                        dataGridView2.Rows[e.RowIndex].Cells["循环触发时间"].ReadOnly = false;
                    }
                }
             }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.循环触发时间)
            {
                //int.TryParse(str, out data_.modbusChannelList.ElementAt(e.RowIndex).pollingTime);
                try
                {
                    if (Convert.ToInt32(str) <= 50)
                    {
                        MessageBox.Show("循环触发时间最小为50ms");
                        dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1000;
                        return;
                    }
                    //else
                       // int.TryParse(str, out data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.ElementAt(e.RowIndex).pollingTime);
                }
                catch
                {
                    MessageBox.Show("循环触发时间最小为50ms");
                    return;
                }
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.偏移)
            {
                //int.TryParse(str, out data_.modbusChannelList.ElementAt(e.RowIndex).Offset);

                try
                {
                    int value = Convert.ToInt32(str);
                    //int.TryParse(str, out data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.ElementAt(e.RowIndex).Offset);
                    if(value < 0 || value > 9999)
                    {
                        MessageBox.Show("超出偏移范围0-9999,");
                        dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    }
                }
                catch
                {
                    MessageBox.Show("请输入数字");
                    dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    return;
                }
                //int.TryParse(str, out data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.ElementAt(e.RowIndex).Offset);
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.长度)
            {
                //int.TryParse(str, out data_.modbusChannelList.ElementAt(e.RowIndex).Length);
                try
                {
                    int value = Convert.ToInt32(str);
                    //int.TryParse(str, out data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.ElementAt(e.RowIndex).Length);
                    //int type = 0;
                    int msg = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[1].Value);
                    if (msg == 1 || msg == 2 || msg == 5 || msg == 15)
                    {
                        //type = 0;
                        //int c = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Channellength;
                        //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Channellength =
                        //    3 + data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Length;
                        //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].type = 0;
                        if(value <= 0 || value >1000)
                        {
                            MessageBox.Show("长度超出上限1-1000,请重新设置");
                            dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                        }
                    }
                    //else if (msg == 3 || msg == 4 || msg == 6 || msg == 16)
                    //{
                    //    type = 1;
                    //    int c = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Channellength;
                    //    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Channellength =
                    //        3 + data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].Length * 2;
                    //    data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].type = 1;
                    //}
                    //refresh();
                    else if (msg == 3|| msg ==4)
                    {
                        if(value < 0 || value>125)
                        {
                            MessageBox.Show("长度超出上限1-125,请重新设置");
                            dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                        }
                    }
                    else if(msg==6|| msg == 16)
                    {
                        if(value <= 0 || value >123)
                        {
                            MessageBox.Show("长度超出上限1-123,请重新设置");
                            dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("请输入数字");
                    dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                    return;
                }
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.触发变量)
            {
                //data_.modbusChannelList.ElementAt(e.RowIndex).trigger_offset = str;
                int flag = 0;
                for(int i = 0; i < clientmanage.modbusClientList.Count;i++)
                {
                    for (int j = 0; j < clientmanage.modbusClientList[i].modbusDeviceList.Count; j++)
                    {
                        if (str == clientmanage.modbusClientList[i].modbusDeviceList[j].resetVaraible && str!="")
                        {
                            flag++;
                        }
                        for (int k = 0; k < clientmanage.modbusClientList[i].modbusDeviceList[j].modbusChannelList.Count; k++)
                        {
                            bool a = (i == CID && j == Convert.ToInt32(this.label3.Text) && k == e.RowIndex);
                            if ((str == clientmanage.modbusClientList[i].modbusDeviceList[j].modbusChannelList[k].trigger_offset ||
                                str == clientmanage.modbusClientList[i].modbusDeviceList[j].modbusChannelList[k].error_offset) && str!="" &&
                              a == false)
                            {
                                flag++;
                            }
                        }
                    }
                }
                for (int l = 0;l<dataGridView2.Rows.Count;l++)
                {
                    if (str == dataGridView2.Rows[l].Cells[6].Value.ToString() && l!=e.RowIndex && str!="")
                    {
                        flag++;
                    }
                    if (str == dataGridView2.Rows[l].Cells[7].Value.ToString() && str != "")
                    {
                        flag++;
                    }
                }

                for (int a = 0; a < masterManage.modbusMastrList.Count; a++)
                {
                    for (int b = 0; b < masterManage.modbusMastrList[a].modbusDeviceList.Count; b++)
                    {
                        if (str == masterManage.modbusMastrList[a].modbusDeviceList[b].resetVaraible)
                        {
                            flag++;
                        }
                        for (int c = 0; c < masterManage.modbusMastrList[a].modbusDeviceList[b].modbusChannelList.Count; c++)
                        {
                            if (str == masterManage.modbusMastrList[a].modbusDeviceList[b].modbusChannelList[c].trigger ||
                                str == masterManage.modbusMastrList[a].modbusDeviceList[b].modbusChannelList[c].error)
                            {
                                flag++;
                            }
                        }
                    }
                }
                if (flag == 0)
                { //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.ElementAt(e.RowIndex).trigger_offset = str; 
                }
                else
                {
                    MessageBox.Show("输入的变量名有重复，请检查后重新输入");
                    dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].trigger_offset;
                }
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.错误变量)
            {
                //data_.modbusChannelList.ElementAt(e.RowIndex).error_offset = str;
                int flag = 0;
                for (int i = 0; i < clientmanage.modbusClientList.Count; i++)
                {
                    for (int j = 0; j < clientmanage.modbusClientList[i].modbusDeviceList.Count; j++)
                    {
                        if (str == clientmanage.modbusClientList[i].modbusDeviceList[j].resetVaraible && str!="")
                        {
                            flag++;
                        }
                        for (int k = 0; k < clientmanage.modbusClientList[i].modbusDeviceList[j].modbusChannelList.Count; k++)
                        {
                            if ((str == clientmanage.modbusClientList[i].modbusDeviceList[j].modbusChannelList[k].trigger_offset ||
                                str == clientmanage.modbusClientList[i].modbusDeviceList[j].modbusChannelList[k].error_offset) &&
                              (i != CID || j != Convert.ToInt32(this.label3.Text) || k != e.RowIndex) && str!="")
                            {
                                flag++;
                            }
                        }
                    }
                }
                for (int l = 0; l < dataGridView2.Rows.Count; l++)
                {
                    if (str == dataGridView2.Rows[l].Cells[6].Value.ToString() && str != "" )
                    {
                        flag++;
                    }
                    if(str == dataGridView2.Rows[l].Cells[7].Value.ToString() && l != e.RowIndex && str != "")
                    {
                        flag++;
                    }
                }
                for (int a = 0; a < masterManage.modbusMastrList.Count; a++)
                {
                    for (int b = 0; b < masterManage.modbusMastrList[a].modbusDeviceList.Count; b++)
                    {
                        if (str == masterManage.modbusMastrList[a].modbusDeviceList[b].resetVaraible)
                        {
                            flag++;
                        }
                        for (int c = 0; c < masterManage.modbusMastrList[a].modbusDeviceList[b].modbusChannelList.Count; c++)
                        {
                            if (str == masterManage.modbusMastrList[a].modbusDeviceList[b].modbusChannelList[c].trigger ||
                                str == masterManage.modbusMastrList[a].modbusDeviceList[b].modbusChannelList[c].error)
                            {
                                flag++;
                            }
                        }
                    }
                }
                if (flag == 0)
                { 
                   // data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.ElementAt(e.RowIndex).error_offset = str;
                }
                else
                {
                    MessageBox.Show("输入的变量名有重复，请检查后重新输入");
                    dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList[e.RowIndex].error_offset;
                }
            }
            else if (e.ColumnIndex == (int)COLUMNNAME_CHANNLE.注释)
            {
                //data_.modbusChannelList.ElementAt(e.RowIndex).note = str;
                //data2.modbusDeviceList[Convert.ToInt32(this.label3.Text)].modbusChannelList.ElementAt(e.RowIndex).note = str;
            }
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void ClientChannel_FormClosing(object sender, FormClosingEventArgs e)
        {
            refresh();
        }

       
    }
}
