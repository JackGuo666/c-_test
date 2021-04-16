using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Net;
namespace LocalPLC.ModbusClient
{
    public partial class modbusclient1 : Form
    {
        private Button channel = new Button();
       
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        
        public modbusclient1()
        {
            InitializeComponent();

            //this.Controls.Add(channel);

            LocalPLC.Base.xml.DataManageBase baseData = null;
            UserControl1.UC.getDataManager(ref baseData);
            foreach (string ethname in baseData.ethernetDic.Keys)
            {
                comboBox1.Items.Add(ethname);

            }
            comboBox1.SelectedIndex = -1;

            channel.Text = ". . .";
            
            DataColumn dc = null;
            //dt.Columns.Add("ID", Type.GetType("System.Int32"));
            //dt.Columns.Add("名称", Type.GetType("System.String"));
            //dt.Columns.Add("IP地址", Type.GetType("System.String"));
            //dt.Columns.Add("响应超时（ms）", Type.GetType("System.Int32"));
            //dt.Columns.Add("允许超时的次数", Type.GetType("System.Int32"));
            //dt.Columns.Add("重连间隔", Type.GetType("System.Int32"));
            //dt.Columns.Add("复位变量", Type.GetType("System.Int32"));
            //dt.Columns.Add(new DataColumn ("通道",typeof(bool)));
            for (int i = 0; i < 16; i++)
            {
                ds.Tables.Add(new DataTable());
                ds.Tables[i].Columns.Add("ID", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("名称", Type.GetType("System.String"));
                ds.Tables[i].Columns.Add("IP地址", Type.GetType("System.String"));
                ds.Tables[i].Columns.Add("端口号", Type.GetType("System.String"));
                ds.Tables[i].Columns.Add("响应超时", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("重连间隔", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("允许超时的次数", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("复位变量", Type.GetType("System.String"));
                //ds.Tables[i].Columns.Add("通道", Type.GetType("System.String"));
            }
            
        }
        private ModbusClientData data_ { get; set; }
        private ModbusClientManage mcm;
        int CID;
        public void getClientData(ref ModbusClientData data,ModbusClientManage clientmanage,int clientid)
        {
            data_ = data;
            data_.ID = data.ID;
            mcm = clientmanage;
            CID = clientid;
        }


        public enum COLUMNNAME : int
        {
            ID, 名称, IP地址,端口号, 响应超时, 重连间隔, 允许超时的次数
                                        , 复位变量, 通道
        };

        public string cn = null;
        public void ClientNumber(string a)
        {
            cn = a;
            
        }
        public string get()
        {
            return cn;
        }
        DataGridViewButtonColumn mcg = new DataGridViewButtonColumn();
        public int channelNo;
        public int channelnumber
        {
            
            get { return channelNo; }
        }
        
        private void modbusclient1_Load(object sender, EventArgs e)
        {
            Clientindex mci = new Clientindex();
            label5.Visible = false;
            this.label5.Text = cn;
            
            // this.dataGridView1.DataSource = dt;
             this.dataGridView1.BeginInvoke(new Action(() => { this.dataGridView1.DataSource = ds.Tables[Convert.ToInt32(cn)]; }));
            //this.dataGridView1.DataSource = ds.Tables[form1.clientnumber];
            //mcg.Name = "通道";
            //mcg.HeaderText = "通道";
            //mcg.DefaultCellStyle.NullValue = ". . .";
            //if(dataGridView1.ColumnCount == 8)
            //{
            //    dataGridView1.Columns.Add(mcg);
            //}

            dataGridView1.ColumnHeadersHeight = 23;
            //列标题自适应
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //dataGridView1.Columns.Add(btn);
            //dataGridView1.RowCount = 1 + data_.modbusDeviceList.Count;
            //ds.Tables[Convert.ToInt32(cn)].Rows.Count = 1 + data_.modbusDeviceList.Count;
            if (ds.Tables[Convert.ToInt32(cn)].Rows.Count < data_.modbusDeviceList.Count)
            {
                int c = data_.modbusDeviceList.Count - ds.Tables[Convert.ToInt32(cn)].Rows.Count;
                for (int j = 0; j < c; j++)
                {
                    DataRow dr1 = ds.Tables[Convert.ToInt32(cn)].NewRow();
                    ds.Tables[Convert.ToInt32(cn)].Rows.Add(dr1);
                }
            }
            dataGridView1.RowTemplate.Height = 30;
            int i = 0;
            foreach (DeviceData devData in data_.modbusDeviceList)
            {
                //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.ID].Value = devData.ID;
                ds.Tables[Convert.ToInt32(cn)].Rows[i][(int)COLUMNNAME.ID] = devData.ID;
                //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.名称].Value = devData.nameDev;
                ds.Tables[Convert.ToInt32(cn)].Rows[i][(int)COLUMNNAME.名称] = devData.nameDev;
                //
                //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.IP地址].Value = devData.serverAddr;
                ds.Tables[Convert.ToInt32(cn)].Rows[i][(int)COLUMNNAME.IP地址] = devData.ipaddr;
                ds.Tables[Convert.ToInt32(cn)].Rows[i][(int)COLUMNNAME.端口号] = devData.port;
                //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.响应超时].Value = devData.reponseTimeout;
                ds.Tables[Convert.ToInt32(cn)].Rows[i][(int)COLUMNNAME.响应超时] = devData.reponseTimeout;
                //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.允许超时的次数].Value = devData.permitTimeoutCount;
                ds.Tables[Convert.ToInt32(cn)].Rows[i][(int)COLUMNNAME.允许超时的次数] = devData.permitTimeoutCount;
                //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.重连间隔].Value = devData.reconnectInterval;
                ds.Tables[Convert.ToInt32(cn)].Rows[i][(int)COLUMNNAME.重连间隔] = devData.reconnectInterval;
                //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.复位变量].Value = devData.resetVaraible;
                ds.Tables[Convert.ToInt32(cn)].Rows[i][(int)COLUMNNAME.复位变量] = devData.resetVaraible;
                //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.通道].Value = "..."/* + i.ToString()*/;
                //ds.Tables[channelnumber].Rows[i][(int)COLUMNNAME.通道] = "...";
                i++;
            }
            dataGridView1.AutoSize = true;
            dataGridView1.AllowUserToAddRows = false;
            //this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            
            this.comboBox1.SelectedItem = data_.transformChannel;
            
            if (data_.transformMode == 0)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else if (data_.transformMode == 1)
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }
        }
        public string clientnumber
        {
            get { return this.label5.Text; }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                data_.transformMode = 0;
            }
            else
            {
                data_.transformMode = 1;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                data_.transformMode = 1;
            }
            else
            {
                data_.transformMode = 0;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            string Item = this.comboBox1.SelectedItem.ToString();
            if (Item == "网口1")
            {

            }
        }

        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
        private int[] temrow = new int[16] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        private void button1_Click(object sender, EventArgs e)
        {
            int rowcount = dataGridView1.RowCount;

            //this.dataGridView1.Rows.Add(rowcount, "设备" + rowcount.ToString(), "", "", "", "", "", "");
            //btn.Name = "Beginbtn";
            //btn.HeaderText = "通道";
            //btn.DefaultCellStyle.NullValue = ". . .";
            //if (dataGridView1.RowCount == 1)
            //{
            //    dataGridView1.Columns.Add(btn);
            //}
            if (utility.masterDeviceCountMax <= dataGridView1.RowCount)
            {
                string err = string.Format("设备最大个数是{0}", utility.masterDeviceCountMax);
                utility.PrintError(err);
                return;
            }
            else
            {
                DataRow dr = ds.Tables[0].NewRow();
                DeviceData data = new DeviceData();
                dr[(int)COLUMNNAME.ID] = rowcount;
                data.ID = rowcount;
                int flag = 0;
                int flag1 = 0;
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    if (dataGridView1.Rows[j].Cells["名称"].Value.ToString() == "设备" + rowcount.ToString())
                    {
                        flag++;
                    }
                }
                if (flag == 0)
                {
                    dr[(int)COLUMNNAME.名称] = "设备" + rowcount.ToString();
                    data.nameDev = dr[(int)COLUMNNAME.名称].ToString();
                }
                //else
                //{
                //    dr[(int)COLUMNNAME.名称] = "设备" + rowcount.ToString();
                //    data.nameDev = dr[(int)COLUMNNAME.名称].ToString();
                //}
                dr[(int)COLUMNNAME.IP地址] = "";
                data.ipaddr = dr[(int)COLUMNNAME.IP地址].ToString();
                if (rowcount >= 1)
                {
                    for (int k = 502; k < 601; k++)
                    {
                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            if (k == Convert.ToInt32(dataGridView1.Rows[j].Cells["端口号"].Value.ToString()))
                            {
                                break;
                            }
                            //if (Convert.ToInt32(dataGridView1.Rows[j].Cells["端口号"].Value.ToString()) == 502 + rowcount)
                            //{
                            //    flag1++;
                            //}
                            else
                            {
                                if(j == dataGridView1.Rows.Count-1)
                                {
                                    flag1 = k;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }

                        if (flag1 != 0)
                        {
                            break;
                        }
                    }


                    dr[(int)COLUMNNAME.端口号] = flag1;
                    data.port = Convert.ToInt32(dr[(int)COLUMNNAME.端口号]);
                }
                else if (rowcount ==0)
                {
                    dr[(int)COLUMNNAME.端口号] = 502;
                    data.port = Convert.ToInt32(dr[(int)COLUMNNAME.端口号]);
                }
               
                dr[(int)COLUMNNAME.响应超时] = 1000;
                data.reponseTimeout = Convert.ToInt32(dr[(int)COLUMNNAME.响应超时]);
                dr[(int)COLUMNNAME.允许超时的次数] = 5;
                data.permitTimeoutCount = Convert.ToInt32(dr[(int)COLUMNNAME.允许超时的次数]);
                dr[(int)COLUMNNAME.重连间隔] = 1000;
                data.reconnectInterval = Convert.ToInt32(dr[(int)COLUMNNAME.重连间隔]);
                dr[(int)COLUMNNAME.复位变量] = "";
                data.resetVaraible = dr[(int)COLUMNNAME.复位变量].ToString();
                string devkey = null;
                if (rowcount < 10)
                {
                    devkey = 0 + rowcount.ToString();
                }
                else if (rowcount >= 10 && rowcount <= 16)
                {
                    devkey = rowcount.ToString();
                }
                data.resetkey[0] = this.label5.Text;
                data.resetkey[1] = devkey;
                //Button add = new Button();
                //add.Text = ". . .";

                //dt.Rows.Add(dr);
                ////ds.Tables.Add(dt);
                ////dataGridView1.DataSource = ds.Tables[0];
                //btn.Name = "Beginbtn";
                //btn.HeaderText = "通道";
                //btn.DefaultCellStyle.NullValue = ". . .";
                int n = Convert.ToInt32(this.label5.Text);
                int i = rowcount;
                //dt.Rows.Add(row);
                if (i > 0)
                {
                    data.devstartaddr = data_.modbusDeviceList[i - 1].devstartaddr + data_.modbusDeviceList[i - 1].devlength;
                }
                else if (i == 0)
                {
                    data.devstartaddr = data_.clientstartaddr;
                }
                ds.Tables[n].Rows.Add(dr.ItemArray);
                int a = ds.Tables[n].Rows.Count;
                //if (dataGridView1.ColumnCount == 7)
                //{
                //    dataGridView1.Columns.Add(mcg);
                //}


                // for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    //dataGridView1.Rows[i].Cells["ID"].Value = row;
                    //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.ID].Value = rowcount;
                    //data.ID = rowcount;

                    //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.名称].Value = "设备" + i.ToString();
                    //data.nameDev = dataGridView1.Rows[i].Cells[(int)COLUMNNAME.名称].Value.ToString();

                    ////
                    //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.IP地址].Value = "";
                    //data.serverAddr = dataGridView1.Rows[i].Cells[(int)COLUMNNAME.IP地址].Value.ToString();

                    //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.响应超时].Value = 1000;
                    //data.reponseTimeout = int.Parse(dataGridView1.Rows[i].Cells[(int)COLUMNNAME.响应超时].Value.ToString());

                    //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.允许超时的次数].Value = 5;
                    //data.permitTimeoutCount = int.Parse(dataGridView1.Rows[i].Cells[(int)COLUMNNAME.允许超时的次数].Value.ToString());

                    //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.重连间隔].Value = 1000;
                    //data.reconnectInterval = int.Parse(dataGridView1.Rows[i].Cells[(int)COLUMNNAME.重连间隔].Value.ToString());

                    //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.复位变量].Value = "";
                    //data.resetVaraible = dataGridView1.Rows[i].Cells[(int)COLUMNNAME.复位变量].Value.ToString(); 


                    //dataGridView1.Rows[i].Cells[(int)COLUMNNAME.通道].Value = "..."/* + i.ToString()*/;
                    //data.


                    //dataGridView1.Cell.Value = "Button " + i.ToString();

                    data_.modbusDeviceList.Add(data);
                    //data_.modbusDeviceList.Insert(1, data);
                }
                for (int j = 0; j < 16; j++)
                {
                    if (temrow[j] == -1)
                    {
                        temrow[j] = rowcount;
                        break;
                    }
                }
            }
        }

        public void refresh()
        {
            for (int i = 0; i<data_.modbusDeviceList.Count ; i++)
            {
                data_.modbusDeviceList[i].ID = i;
                if (i > 0)
                {
                    data_.modbusDeviceList[i].devstartaddr = data_.modbusDeviceList[i - 1].devstartaddr + data_.modbusDeviceList[i - 1].devlength;
                }
                else if (i == 0)
                {
                    data_.modbusDeviceList[i].devstartaddr = data_.clientstartaddr;
                }
            }
        }
        private int selectrow = -1;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int n = dataGridView1.SelectedCells[0].RowIndex;
                if (dataGridView1.SelectedRows.Count != 1)
                {
                    MessageBox.Show("请选择一整行进行删除");
                    return;
                }
                //this.dataGridView1.Rows.RemoveAt(n);
                else
                {
                    //ds.Tables[Convert.ToInt32(cn)].Rows[n].Delete();
                    //data_.modbusDeviceList.RemoveAt(n);
                    ////if (n > 1)
                    ////{
                    ////    for (int i = n; i<dataGridView1.RowCount;i++)
                    ////    {
                    ////        ds.Tables[Convert.ToInt32(cn)].Rows[i][0] = data_.modbusDeviceList[i - 1].ID + 1;
                    ////        data_.modbusDeviceList[i].ID = data_.modbusDeviceList[i - 1].ID + 1;
                    ////    }

                    ////}
                    ////else if (n == 1)
                    ////{
                    ////    ds.Tables[Convert.ToInt32(cn)].Rows[n][0] = 0;
                    ////    data_.modbusDeviceList[n].ID = 0;
                    ////}
                    //for (int i = 0; i < ds.Tables[Convert.ToInt32(cn)].Rows.Count; i++)
                    //{
                    //    ds.Tables[Convert.ToInt32(cn)].Rows[i][0] = i;
                    //    data_.modbusDeviceList[i].ID = i;
                    //}
                    ////ds.Tables[n].Clear();
                    //refresh();

                    CurrencyManager cm = (CurrencyManager)BindingContext[dataGridView1.DataSource];
                    cm.SuspendBinding(); //挂起数据绑定
                    dataGridView1.Rows[n].Visible = false;
                    cm.ResumeBinding(); //恢复数据绑定
                    bool a = dataGridView1.Rows[n].Visible;
                }
                
            }
            catch
            {
                return;
            }
        }
        ModbusClient.ClientChannel CCl = new ClientChannel();
        //ClientChannel CC ;
        //ClientChannel1 CC1 = new ClientChannel1();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ////说明点击的列是DataGridViewButtonColumn列
            ////DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
            ////int currentrow = dataGridView1.SelectedRows[0].Index;
            ////CCl = new ModbusClient.ClientChannel();
            ////CC = new ClientChannel();
            if (e.RowIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex == (int)COLUMNNAME.通道)
            {
                refresh();
                DeviceData data = data_.modbusDeviceList.ElementAt(e.RowIndex);
                
                ModbusClientData datac = data_;
                // ModbusClientData data1 = 
                //CCl.getDeviceData(ref data);
                CCl.getModbusClientData(ref datac,ref mcm,CID);
                CCl.ClientNumber(cn);
                CCl.StartPosition = FormStartPosition.CenterScreen;
                channelNo = e.RowIndex;
                CCl.ShowDialog(this);
            }
        }
        ModbusMaster.ModbusMasterManage masterManage = new ModbusMaster.ModbusMasterManage();
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //DeviceData data = data_.modbusDeviceList.ElementAt(e.RowIndex);
            //CCl.getDeviceData(ref data);
            //CCl.ShowDialog(this);
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            selectrow = e.RowIndex;
        }
        private void dataGridView1_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            object obj = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
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



            if (e.ColumnIndex == (int)COLUMNNAME.ID)
            {
                //data_.modbusDeviceList.ElementAt(e.RowIndex).ID = int.Parse(str);
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.名称)
            {
                //data_.modbusDeviceList.ElementAt(e.RowIndex).nameDev = str;
                int flag = 0;
                for(int i = 0;i<dataGridView1.Rows.Count;i++)
                {
                    if(str == dataGridView1.Rows[i].Cells[1].Value.ToString() && i!=e.RowIndex)
                    {
                        flag++;
                    }
                }
                if(flag>0)
                {
                    MessageBox.Show("设备名重复");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = data_.modbusDeviceList[e.RowIndex].nameDev;
                }
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.IP地址)
            {
                try
                {
                    IPAddress ipTry = IPAddress.Parse(str);
                    //data_.modbusDeviceList.ElementAt(e.RowIndex).ipaddr = str;
                    string[] ip = new string[4];
                    ip = str.Split('.');
                    if(Convert.ToInt32( ip[3])<1 || Convert.ToInt32(ip[3])>247)
                    {
                        MessageBox.Show("从站地址可以配置为1~247");
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    }
                }
                catch
                {
                    MessageBox.Show("请输入正确的IP地址：(0-255).(0-255).(0-255).(1-247)");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    return;
                }
                //data_.modbusDeviceList.ElementAt(e.RowIndex).ipaddr = str;
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.端口号)
            {
                int flag = 0;
                try
                {
                    if (Convert.ToInt32(str) >= 500 && Convert.ToInt32(str) <= 600)
                    {
                        //data_.modbusDeviceList.ElementAt(e.RowIndex).port = Convert.ToInt32(str); 
                        for (int i = 0; i < mcm.modbusClientList.Count; i++)
                        {
                            for (int j = 0; j < mcm.modbusClientList[i].modbusDeviceList.Count; j++)
                            {
                                if (str == mcm.modbusClientList[i].modbusDeviceList[j].port.ToString())
                                {
                                    flag++;
                                }
                            }
                        }
                        for (int k = 0; k < dataGridView1.Rows.Count; k++)
                        {
                            if (str == dataGridView1.Rows[k].Cells[3].Value.ToString() && k != e.RowIndex)
                            {
                                flag++;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("端口范围500-600");
                        dataGridView1.Rows[e.RowIndex].Cells[3].Value = "";
                    }
                    if (flag > 0)
                    {
                        MessageBox.Show("端口号有重复");
                        dataGridView1.Rows[e.RowIndex].Cells[3].Value = "";
                    }
                }
                catch
                {
                    MessageBox.Show("端口范围500-600");
                    dataGridView1.Rows[e.RowIndex].Cells[3].Value = "";
                }
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.响应超时)
            {
                try 
                {
                   int value = Convert.ToInt32(str);
                    if (value >= 0 && value<=10000)
                    {
                        //int.TryParse(str, out data_.modbusDeviceList.ElementAt(e.RowIndex).reponseTimeout);
                    }
                    else
                    {
                        MessageBox.Show("请输入0-10000的数字");
                        dataGridView1.Rows[e.RowIndex].Cells[4].Value = "";
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("请输入0-10000的数字");
                    dataGridView1.Rows[e.RowIndex].Cells[4].Value = "";
                    return;
                }
                  
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.重连间隔)
            {
                try
                {
                    int value = Convert.ToInt32(str);
                    if (value >= 0 && value <= 10000)
                    {
                        //int.TryParse(str, out data_.modbusDeviceList.ElementAt(e.RowIndex).reconnectInterval);
                    }
                    else
                    {
                        MessageBox.Show("请输入0-10000的数字");
                        dataGridView1.Rows[e.RowIndex].Cells[5].Value = "";
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("请输入0-10000的数字");
                    dataGridView1.Rows[e.RowIndex].Cells[5].Value = "";
                    return;
                }
                
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.允许超时的次数)
            {
                try
                {
                    int value = Convert.ToInt32(str);
                    if (value >= 0 && value <= 10)
                    {
                        //int.TryParse(str, out data_.modbusDeviceList.ElementAt(e.RowIndex).permitTimeoutCount);
                    }
                    else
                    {
                        MessageBox.Show("请输入0-10的数字");
                        dataGridView1.Rows[e.RowIndex].Cells[6].Value = "";
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("请输入0-10的数字");
                    dataGridView1.Rows[e.RowIndex].Cells[6].Value = "";
                    return;
                }
                
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.复位变量)
            {
                int flag = 0;
                
                for(int i = 0; i < mcm.modbusClientList.Count; i++)
                {
                    for(int j=0; j< mcm.modbusClientList[i].modbusDeviceList.Count;j++)
                    {
                        if (str == mcm.modbusClientList[i].modbusDeviceList[j].resetVaraible && (i!=CID || j != e.RowIndex) && str!="")
                        {
                            flag++;
                        }
                        for(int k =0; k< mcm.modbusClientList[i].modbusDeviceList[j].modbusChannelList.Count;k++)
                        {
                            if (str == mcm.modbusClientList[i].modbusDeviceList[j].modbusChannelList[k].trigger_offset || 
                                str == mcm.modbusClientList[i].modbusDeviceList[j].modbusChannelList[k].error_offset)
                            {
                                flag++;
                            }
                        }
                    }        
                }
                for (int l = 0;l<dataGridView1.Rows.Count;l++)
                {
                    if (str == dataGridView1.Rows[l].Cells[7].Value.ToString() && l != e.RowIndex)
                    {
                        flag++;
                    }
                }
                for (int a = 0;a<masterManage.modbusMastrList.Count;a++)
                {
                    for (int b = 0; b < masterManage.modbusMastrList[a].modbusDeviceList.Count; b++)
                    {
                        if (str == masterManage.modbusMastrList[a].modbusDeviceList[b].resetVaraible)
                        {
                            flag++;
                        }
                        for(int c = 0; c < masterManage.modbusMastrList[a].modbusDeviceList[b].modbusChannelList.Count; c++)
                        {
                            if(str == masterManage.modbusMastrList[a].modbusDeviceList[b].modbusChannelList[c].trigger || 
                                str == masterManage.modbusMastrList[a].modbusDeviceList[b].modbusChannelList[c].error)
                            {
                                flag++;
                            }
                        }
                    }
                }
                if (flag == 0)
                { 
                    //data_.modbusDeviceList[e.RowIndex].resetVaraible = str;
                    //dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = dataGridView1.Rows[0].Cells[0].Style.BackColor;

                }
                else
                {
                    MessageBox.Show("复位变量名有重复，请检查后重新输入");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    //dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                }

            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //data_.transformChannel = comboBox1.Text;
            try
            {
                for (int i = 0; i < mcm.modbusClientList.Count; i++)
                {
                    if (mcm.modbusClientList[i].transformChannel == comboBox1.SelectedItem.ToString() && i != CID)
                    {
                        MessageBox.Show("网口端口设置存在冲突!");
                        comboBox1.SelectedIndex = -1;
                    }

                }
            }
            catch
            {
                return;
            }
        }

        

        

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void modbusclient1_Shown(object sender, EventArgs e)
        {
            mcg.Name = "通道";
            mcg.DefaultCellStyle.NullValue = ". . .";
            dataGridView1.Columns.Add(mcg);
        }

        private void modbusclient1_FormClosing(object sender, FormClosingEventArgs e)
        {
            refresh();
            int length = 0;
            for(int i =0;i<data_.modbusDeviceList.Count;i++)
            {
                length += data_.modbusDeviceList[i].devlength;
            }
            if(length >= 2000)
            {
                MessageBox.Show("client" + CID.ToString() + "长度超过2000，请重新设置");
                utility.PrintError("client"+CID.ToString()+"长度超过2000，请重新设置");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void ClientSettings_Enter(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 15; i >= 0; i--)
            {
                if (temrow[i] != -1)
                {
                    ds.Tables[Convert.ToInt32(cn)].Rows[temrow[i]].Delete();
                    data_.modbusDeviceList.RemoveAt(temrow[i]);
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
        public void refresheachchannel()
        {
            for(int i =0;i<data_.modbusDeviceList.Count;i++)
            {
                for (int j = 0; j < data_.modbusDeviceList[i].modbusChannelList.Count; j++)
                {
                    if (j == 0)
                    {
                        data_.modbusDeviceList[i].modbusChannelList[j].channelstartaddr = data_.modbusDeviceList[i].devstartaddr + 1;
                    }
                    else if (j>=1)
                    {
                        data_.modbusDeviceList[i].modbusChannelList[j].channelstartaddr = data_.modbusDeviceList[i].modbusChannelList[j - 1].channelstartaddr +
                            data_.modbusDeviceList[i].modbusChannelList[j - 1].Channellength;
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            refreshtemrow();
            bool b = dataGridView1.Rows[0].Visible;
            
            for (int m = dataGridView1.Rows.Count-1; m>=0;m--)
            {
                bool a = dataGridView1.Rows[m].Visible;
                if (dataGridView1.Rows[m].Visible == false)
                {
                    ds.Tables[Convert.ToInt32(cn)].Rows[m].Delete();
                    data_.modbusDeviceList.RemoveAt(m);
                    for (int i = 0; i < ds.Tables[Convert.ToInt32(cn)].Rows.Count; i++)
                    {
                        ds.Tables[Convert.ToInt32(cn)].Rows[i][0] = i;
                        data_.modbusDeviceList[i].ID = i;
                    }
                    refresh();
                    
                }
            }
            if (b == false)
            {
                ds.Tables[Convert.ToInt32(cn)].Rows[0].Delete();
                data_.modbusDeviceList.RemoveAt(0);
            }
            refresh();
            refresheachchannel();
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("传输通道未配置！");
                return;
            }
            int flag1 = 0;
            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {
                if (dataGridView1.Rows[j].Cells["名称"].Value.ToString() == "" || dataGridView1.Rows[j].Cells["名称"].Value.ToString() == "")
                {
                    flag1++;
                }
            }
            if (flag1 > 0)
            {
                MessageBox.Show("还有设备名称和端口号未填写");
                return;
            }
            else
            {
                int row = dataGridView1.Rows.Count;
                data_.transformChannel = comboBox1.SelectedItem.ToString();
                for (int i = 0; i < row; i++)
                {
                    data_.modbusDeviceList[i].ID = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    data_.modbusDeviceList[i].nameDev = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    data_.modbusDeviceList[i].ipaddr = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    data_.modbusDeviceList[i].port = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    int.TryParse(dataGridView1.Rows[i].Cells[4].Value.ToString(), out data_.modbusDeviceList[i].reponseTimeout);
                    int.TryParse(dataGridView1.Rows[i].Cells[5].Value.ToString(), out data_.modbusDeviceList[i].reconnectInterval);
                    int.TryParse(dataGridView1.Rows[i].Cells[6].Value.ToString(), out data_.modbusDeviceList[i].permitTimeoutCount);
                    data_.modbusDeviceList[i].resetVaraible = dataGridView1.Rows[i].Cells[7].Value.ToString();
                }
            }
        }
    }
}
