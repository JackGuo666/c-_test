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

        public void getClientData(ref ModbusClientData data)
        {
            data_ = data;
            data_.ID = data.ID;
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
            
            this.comboBox1.Text = data_.transformChannel;
            
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
            DataRow dr = ds.Tables[0].NewRow();
            DeviceData data = new DeviceData();
            dr[(int)COLUMNNAME.ID] = rowcount;
            data.ID = rowcount;
            dr[(int)COLUMNNAME.名称] = "设备" + rowcount.ToString();
            data.nameDev = dr[(int)COLUMNNAME.名称].ToString();
            dr[(int)COLUMNNAME.IP地址] = "";
            data.ipaddr = dr[(int)COLUMNNAME.IP地址].ToString();
            dr[(int)COLUMNNAME.端口号] = 502;
            data.port = Convert.ToInt32(dr[(int)COLUMNNAME.端口号]);
            dr[(int)COLUMNNAME.响应超时] = 1000;
            data.reponseTimeout = Convert.ToInt32(dr[(int)COLUMNNAME.响应超时]);
            dr[(int)COLUMNNAME.允许超时的次数] = 5;
            data.permitTimeoutCount = Convert.ToInt32(dr[(int)COLUMNNAME.允许超时的次数]);
            dr[(int)COLUMNNAME.重连间隔] = 1000;
            data.reconnectInterval = Convert.ToInt32(dr[(int)COLUMNNAME.重连间隔]);
            dr[(int)COLUMNNAME.复位变量] = "";
            data.resetVaraible = dr[(int)COLUMNNAME.复位变量].ToString();
            string devkey = null;
            if(rowcount < 10)
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
            if (i>0)
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
            int n = selectrow;
            if (n < 0)
            {
                return;
            }
            //this.dataGridView1.Rows.RemoveAt(n);
            ds.Tables[Convert.ToInt32(cn)].Rows[n].Delete();
            data_.modbusDeviceList.RemoveAt(n);
            //if (n > 1)
            //{
            //    for (int i = n; i<dataGridView1.RowCount;i++)
            //    {
            //        ds.Tables[Convert.ToInt32(cn)].Rows[i][0] = data_.modbusDeviceList[i - 1].ID + 1;
            //        data_.modbusDeviceList[i].ID = data_.modbusDeviceList[i - 1].ID + 1;
            //    }
                
            //}
            //else if (n == 1)
            //{
            //    ds.Tables[Convert.ToInt32(cn)].Rows[n][0] = 0;
            //    data_.modbusDeviceList[n].ID = 0;
            //}
            for (int i =0;i<ds.Tables[Convert.ToInt32(cn)].Rows.Count;i++)
            {
                ds.Tables[Convert.ToInt32(cn)].Rows[i][0] = i;
                data_.modbusDeviceList[i].ID = i;
            }
            //ds.Tables[n].Clear();
            refresh();
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
                CCl.getModbusClientData(ref datac);
                CCl.ClientNumber(cn);
                CCl.StartPosition = FormStartPosition.CenterScreen;
                channelNo = e.RowIndex;
                CCl.ShowDialog(this);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //DeviceData data = data_.modbusDeviceList.ElementAt(e.RowIndex);
            //CCl.getDeviceData(ref data);
            //CCl.ShowDialog(this);
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
                data_.modbusDeviceList.ElementAt(e.RowIndex).ID = int.Parse(str);
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.名称)
            {
                data_.modbusDeviceList.ElementAt(e.RowIndex).nameDev = str;
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.IP地址)
            {
                data_.modbusDeviceList.ElementAt(e.RowIndex).ipaddr = str;
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.端口号)
            {
                if (Convert.ToInt32(str) >= 500 && Convert.ToInt32(str) <= 600)
                { data_.modbusDeviceList.ElementAt(e.RowIndex).port = Convert.ToInt32(str); }
                else
                { MessageBox.Show("端口范围500-600"); }
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.响应超时)
            {
                int.TryParse(str, out data_.modbusDeviceList.ElementAt(e.RowIndex).reponseTimeout);
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.重连间隔)
            {
                int.TryParse(str, out data_.modbusDeviceList.ElementAt(e.RowIndex).reconnectInterval);
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.允许超时的次数)
            {
                int.TryParse(str, out data_.modbusDeviceList.ElementAt(e.RowIndex).permitTimeoutCount);
            }
            else if (e.ColumnIndex == (int)COLUMNNAME.复位变量)
            {
                data_.modbusDeviceList[e.RowIndex].resetVaraible = str;


            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            data_.transformChannel = comboBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {

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
        }
    }
}
