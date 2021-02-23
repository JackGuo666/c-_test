using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ADELib;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace LocalPLC.ModbusServer
{
    

    public partial class ServerIndex : UserControl
    {
        public DataManager serverDataManager = null;
        public ServerIndex()
        {
            InitializeComponent();
            

            serverDataManager = DataManager.GetInstance();
            
        }

        public void deleteTableRow()
        {
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView1.Rows.RemoveAt(i);
            }
        }

        private void ServerIndex_Load(object sender, EventArgs e)
        {
            init = true;
            
            DataGridViewDisableButtonColumn buttonColumn = new DataGridViewDisableButtonColumn();
            buttonColumn.Name = "配置";
            buttonColumn.HeaderText = "配置";
            DataGridViewDisableButtonColumn btn = new DataGridViewDisableButtonColumn();
            btn.Name = "info";
            btn.HeaderText = "详细信息";
            DataGridViewTextBoxColumn cellColumn = new DataGridViewTextBoxColumn();
            cellColumn.Name = "ID";
            cellColumn.HeaderText = "ID";
            //DataGridViewTextBoxColumn celltype = new DataGridViewTextBoxColumn();
            //celltype.Name = "Mode";
            //celltype.HeaderText = "Mode";
            buttonColumn.DefaultCellStyle.NullValue = ". . .";
            btn.DefaultCellStyle.NullValue = ". . .";

            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Columns.Add(cellColumn);
            dataGridView1.Columns.Add(buttonColumn);
            dataGridView1.Columns.Add(btn);
            //dataGridView1.Columns.Add(celltype);

            if (dataGridView1.Columns.Count > 3)
            {
                dataGridView1.Columns.RemoveAt(0);
            }
            dataGridView1.RowCount = /*8*/ serverDataManager.listServer.Count;
            dataGridView1.AutoSize = true;
            dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            for (int i = 0; i < serverDataManager.listServer.Count; i++)
            {
                ModbusServerData data = serverDataManager.listServer.ElementAt(i);
                //ModbusServer.DataManager datamanage = serverDataManager;
                //ModbusServerData data = serverDataManager.listServer[i];
                dataGridView1.Rows[i].Cells["ID"].Value = data.ID;
                
                //dataGridView1.Rows[i].Cells["配置"].Value = "..."/* + i.ToString()*/;
            }
        }

        bool init = false;
        public void initForm()
        {
            if (init == false)
            {
                return;
            }
            //if (dataGridView1.RowCount < 1)
            //{
            //    return;
            //}
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView1.Rows.RemoveAt(i);
            }
            if (serverDataManager.listServer.Count > 0)
            dataGridView1.RowCount += serverDataManager.listServer.Count;
            int a = serverDataManager.listServer.Count;
            for (int i = 0; i < serverDataManager.listServer.Count; i++)
            {
                ModbusServerData data = serverDataManager.listServer.ElementAt(i);
                
                dataGridView1.Rows[i].Cells[0].Value = data.ID;
                //dataGridView1.Rows[i].Cells["跳转"].Value = "..."/* + i.ToString()*/;
            }
            if (dataGridView1.Columns.Count > 3)
            {
                dataGridView1.Columns.RemoveAt(0);
            }
        }

        //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
        private void button1_Click(object sender, EventArgs e)
        {
            //int rowcount = dataGridView1.RowCount;
            //this.dataGridView1.Rows.Add(1);
            //btn.Name = "Goto";
            //btn.HeaderText = "跳转";
            //btn.DefaultCellStyle.NullValue = ". . .";
            //if (dataGridView1.RowCount == 1)
            //{
            //    dataGridView1.Columns.Add(btn);
            //}

            int row = dataGridView1.RowCount;
            dataGridView1.RowCount += 1;

            // Set the text for each button.
            int i = row;


            ModbusServerData data = new ModbusServerData();

            // for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells["ID"].Value = row;
                data.ID = row;
                dataGridView1.Rows[i].Cells["配置"].Value = "..."/* + i.ToString()*/;
                //data.device = new DeviceData();
                if (row > 1)
                {
                    data.dataDevice_.shmrange = serverDataManager.listServer[row - 1].dataDevice_.shmrange + serverDataManager.listServer[row - 1].dataDevice_.shmlength;
                }
                if(data.dataDevice_.transformMode == 0)
                {
                    serverDataManager.Rtunum++;
                }
                else if (data.dataDevice_.transformMode == 1)
                {
                    serverDataManager.TCPnum++;
                }
                serverDataManager.listServer.Add(data);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("Infos").AddEntry("Hello world! (from C#)", AdeOutputWindowMessageType.adeOwMsgInfo, "", "", 0, "");
                // show the output window and activate the "Infos" tab
                LocalPLC.UserControl1.multiprogApp.OutputWindows.Item("Infos").Activate();

                return;
            }

            for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
            {
                int index = dataGridView1.SelectedRows[i].Index;

                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[i]);
                serverDataManager.listServer.RemoveAt(index);
            }
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if      (e.RowIndex == -1)
            {
                return;
            }

        }

        public void saveXml(ref System.Xml.XmlElement elem, ref System.Xml.XmlDocument doc)
        {
            XmlElement elem1 = doc.CreateElement("modbusserver");
            elem.SetAttribute("Rtunum", serverDataManager.Rtunum.ToString());
            elem.SetAttribute("Tcpnum", serverDataManager.TCPnum.ToString());
            elem.AppendChild(elem1);
            //elem1.SetAttribute("start");
            for (int i = 0; i < serverDataManager.listServer.Count; i++)
            {
                ModbusServerData data = serverDataManager.listServer.ElementAt(i);
                XmlElement elem1_s = doc.CreateElement("s");
                elem1_s.SetAttribute("ID", data.ID.ToString());

                //server详细信息
                //data.dataDevice_;
                XmlElement elem1_s_data = doc.CreateElement("data");
                elem1_s.SetAttribute("coilcount", data.dataDevice_.coilCount.ToString());
                elem1_s.SetAttribute("holdingcount", data.dataDevice_.holdingCount.ToString());
                elem1_s.SetAttribute("decretecount", data.dataDevice_.decreteCount.ToString());
                elem1_s.SetAttribute("statuscount", data.dataDevice_.statusCount.ToString());
                elem1_s.SetAttribute("transform", data.dataDevice_.transform.ToString());
                elem1_s.SetAttribute("transformport", data.dataDevice_.transformport.ToString());
                elem1_s.SetAttribute("transformmode", data.dataDevice_.transformMode.ToString());
                elem1_s.SetAttribute("deviceaddr", data.dataDevice_.deviceAddr.ToString());
                elem1_s.SetAttribute("port", data.dataDevice_.port.ToString());
                elem1_s.SetAttribute("maxconnect", data.dataDevice_.maxconnectnumber.ToString());
                elem1_s.SetAttribute("ipfixed", data.dataDevice_.ipfixed.ToString());
                elem1_s.SetAttribute("ip0", data.dataDevice_.ip0.ToString());
                elem1_s.SetAttribute("ip1", data.dataDevice_.ip1.ToString());
                elem1_s.SetAttribute("ip2", data.dataDevice_.ip2.ToString());
                elem1_s.SetAttribute("ip3", data.dataDevice_.ip3.ToString());
                elem1_s.SetAttribute("ip10", data.dataDevice_.ip10.ToString());
                elem1_s.SetAttribute("ip11", data.dataDevice_.ip11.ToString());
                elem1_s.SetAttribute("ip12", data.dataDevice_.ip12.ToString());
                elem1_s.SetAttribute("ip13", data.dataDevice_.ip13.ToString());
                elem1_s.SetAttribute("ip20", data.dataDevice_.ip20.ToString());
                elem1_s.SetAttribute("ip21", data.dataDevice_.ip21.ToString());
                elem1_s.SetAttribute("ip22", data.dataDevice_.ip22.ToString());
                elem1_s.SetAttribute("ip23", data.dataDevice_.ip23.ToString());
                elem1_s.SetAttribute("ip30", data.dataDevice_.ip30.ToString());
                elem1_s.SetAttribute("ip31", data.dataDevice_.ip31.ToString());
                elem1_s.SetAttribute("ip32", data.dataDevice_.ip32.ToString());
                elem1_s.SetAttribute("ip33", data.dataDevice_.ip33.ToString());
                elem1_s.SetAttribute("IOAddrRange", data.serverstartaddr.ToString());
                elem1_s.SetAttribute("IOAddrLength", data.dataDevice_.IOAddrLength.ToString());
                elem1_s.SetAttribute("SHMRange", data.dataDevice_.shmrange.ToString());
                elem1_s.SetAttribute("SHMLength", data.dataDevice_.shmlength.ToString());
                elem1_s.SetAttribute("coilstart", data.dataDevice_.coilIoAddrStart);
                elem1_s.SetAttribute("holdingstart", data.dataDevice_.holdingIoAddrStart);
                elem1_s.SetAttribute("decretestart", data.dataDevice_.decreteIoAddrStart);
                elem1_s.SetAttribute("statusstart", data.dataDevice_.statusIoAddrStart);

                elem1_s.SetAttribute("transformmode", data.dataDevice_.transformMode.ToString());
                elem1_s.SetAttribute("deviceaddr", data.dataDevice_.deviceAddr.ToString());

                //elem1_s.AppendChild(elem1_s_data);

                elem1.AppendChild(elem1_s);
            }
        }

        public void saveJson(JsonTextWriter writer)
        {
            //添加modbusserver节点
            DataManager server = serverDataManager;
            if (server.listServer.Count > 0)
            {
                writer.WritePropertyName("mb_slave");
                writer.WriteStartObject();//添加{  server节点
                                          //writer.WritePropertyName("number");
                                          //writer.WriteValue(serverDataManager.listServer.Count);//number
                                          //writer.WritePropertyName("conf");
                                          //writer.WriteStartArray();//[ server节点下conf数组
                                          //for (int i = 0; i < serverDataManager.listServer.Count; i++)
                {
                    ModbusServerData data = serverDataManager.listServer[0];
                    writer.WritePropertyName("io_range");
                    writer.WriteStartObject();//{ server节点io_range
                    writer.WritePropertyName("start");
                    writer.WriteValue(Convert.ToInt32(data.dataDevice_.IOAddrRange));
                    writer.WritePropertyName("bytes");
                    writer.WriteValue(data.dataDevice_.IOAddrLength);
                    writer.WriteEndObject();//} server节点下io_range
                    writer.WritePropertyName("shm_range");
                    writer.WriteStartObject();//{ server节点shm_range
                    writer.WritePropertyName("start");
                    writer.WriteValue(data.dataDevice_.shmrange);
                    writer.WritePropertyName("bytes");
                    writer.WriteValue(data.dataDevice_.shmlength);
                    writer.WriteEndObject();//} server节点下shm_range
                                            //离散
                    writer.WritePropertyName("discrete");
                    writer.WriteStartObject();
                    writer.WritePropertyName("addr_type");
                    writer.WriteValue("IO_INPUT");
                    writer.WritePropertyName("start");
                    writer.WriteValue(Convert.ToInt32(data.dataDevice_.decreteIoAddrStart));
                    writer.WritePropertyName("num");
                    writer.WriteValue(data.dataDevice_.decreteCount);
                    writer.WriteEndObject();
                    //线圈
                    writer.WritePropertyName("coils");
                    writer.WriteStartObject();
                    writer.WritePropertyName("addr_type");
                    writer.WriteValue("IO_INOUT");
                    writer.WritePropertyName("start");
                    writer.WriteValue(Convert.ToInt32(data.dataDevice_.shmrange));
                    writer.WritePropertyName("num");
                    writer.WriteValue(data.dataDevice_.coilCount);
                    writer.WriteEndObject();
                    //状态
                    writer.WritePropertyName("regs");
                    writer.WriteStartObject();
                    writer.WritePropertyName("addr_type");
                    writer.WriteValue("IO_INPUT");
                    writer.WritePropertyName("start");
                    writer.WriteValue(Convert.ToInt32(data.dataDevice_.statusIoAddrStart));
                    writer.WritePropertyName("num");
                    writer.WriteValue(data.dataDevice_.statusCount);
                    writer.WriteEndObject();
                    //保持
                    writer.WritePropertyName("holding");
                    writer.WriteStartObject();
                    writer.WritePropertyName("addr_type");
                    writer.WriteValue("IO_INOUT");
                    writer.WritePropertyName("start");
                    writer.WriteValue(Convert.ToInt32(data.dataDevice_.holdingIoAddrStart));
                    writer.WritePropertyName("num");
                    writer.WriteValue(data.dataDevice_.holdingCount);
                    writer.WriteEndObject();//}
                    //串口
                    int rtunumber = 0;
                    for (int i = 0; i < serverDataManager.listServer.Count; i++)
                    {
                        if (serverDataManager.listServer[i].dataDevice_.transform == 0)
                        {
                            rtunumber++;
                        }
                    }
                    if (rtunumber > 0)
                    {
                        writer.WritePropertyName("mb_slave_serial");
                        writer.WriteStartObject();//{ 串口
                        writer.WritePropertyName("number");
                        writer.WriteValue(rtunumber);
                        writer.WritePropertyName("conf");
                        writer.WriteStartArray();//[ 串口节点下conf数组              
                        int j = 0;
                        for (int i = 0; i < serverDataManager.listServer.Count; i++)
                        {
                            ModbusServerData data_ = serverDataManager.listServer[i];
                            if (serverDataManager.listServer[i].dataDevice_.transform == 0)
                            {
                                writer.WriteStartObject(); // { 串口数组下设备左括号
                                writer.WritePropertyName("port");
                                writer.WriteValue("ser_port" + (data_.dataDevice_.transformport));
                                writer.WritePropertyName("mode");
                                writer.WriteValue("rtu");
                                writer.WritePropertyName("time_unit");
                                writer.WriteValue("ms");
                                writer.WritePropertyName("dev_id");
                                writer.WriteValue(data_.ID);
                                writer.WritePropertyName("dev_namestr");
                                writer.WriteValue("mbrtu_slave" + j.ToString());
                                writer.WriteEndObject(); // } 串口数组下设备右括号
                                j++;
                            }
                        }
                        writer.WriteEndArray();//] 串口节点下conf数组  
                        writer.WriteEndObject();//} 串口
                    }
                    //网口
                    int tcpnumber = 0;
                    for (int i = 0; i < serverDataManager.listServer.Count; i++)
                    {
                        if (serverDataManager.listServer[i].dataDevice_.transform == 1)
                        {
                            tcpnumber++;
                        }
                    }
                    if (tcpnumber > 0)
                    {
                        int k = 0;
                        writer.WritePropertyName("mb_slave_tcp");
                        writer.WriteStartObject();//{ 网口
                        writer.WritePropertyName("number");
                        writer.WriteValue(tcpnumber);
                        writer.WritePropertyName("conf");
                        writer.WriteStartArray();//[ 网口节点下conf数组   
                        int n = 0;
                        for (int i = 0; i < serverDataManager.listServer.Count; i++)
                        {
                            ModbusServerData data_ = serverDataManager.listServer[i];
                            bool ipfixed = false;
                            if (data_.dataDevice_.ipfixed == false)
                            {
                                ipfixed = false;
                            }
                            else if (data_.dataDevice_.ipfixed == true)
                            {
                                ipfixed = true;
                            }
                            if (serverDataManager.listServer[i].dataDevice_.transform == 1)
                            {
                                writer.WriteStartObject(); // { 网口数组下设备左括号
                                writer.WritePropertyName("port");
                                writer.WriteValue("ethif" + (data_.dataDevice_.transformport - 2));
                                writer.WritePropertyName("remote_ip_fixed");
                                writer.WriteValue(ipfixed);
                                writer.WritePropertyName("max_connection");
                                writer.WriteValue(4);
                                writer.WritePropertyName("remote_ip_num");
                                writer.WriteValue(data_.dataDevice_.maxconnectnumber);
                                writer.WritePropertyName("remote_ip");
                                writer.WriteStartArray();//[ 指定ip的数组
                                string ip0 = data_.dataDevice_.ip0.ToString() + "." + data_.dataDevice_.ip1.ToString() + "." + data_.dataDevice_.ip2.ToString() + "." + data_.dataDevice_.ip3.ToString();
                                string ip1 = data_.dataDevice_.ip10.ToString() + "." + data_.dataDevice_.ip11.ToString() + "." + data_.dataDevice_.ip12.ToString() + "." + data_.dataDevice_.ip13.ToString();
                                string ip2 = data_.dataDevice_.ip20.ToString() + "." + data_.dataDevice_.ip21.ToString() + "." + data_.dataDevice_.ip22.ToString() + "." + data_.dataDevice_.ip23.ToString();
                                string ip3 = data_.dataDevice_.ip30.ToString() + "." + data_.dataDevice_.ip31.ToString() + "." + data_.dataDevice_.ip32.ToString() + "." + data_.dataDevice_.ip33.ToString();
                                string[] ip = { ip0, ip1, ip2, ip3 };
                                for (int m = 0; m < data_.dataDevice_.maxconnectnumber; m++)
                                {
                                    writer.WriteValue(ip[m]);
                                }
                                writer.WriteEndArray();//] 指定ip的数组
                                writer.WritePropertyName("ip_port");
                                writer.WriteValue(502);
                                writer.WritePropertyName("time_unit");
                                writer.WriteValue("ms");
                                writer.WritePropertyName("dev_id");
                                writer.WriteValue(data_.ID);
                                writer.WritePropertyName("dev_namstr");
                                writer.WriteValue("mbtcp_client" + n);
                                n++;
                            }
                        }
                        writer.WriteEndArray();//] 网口节点下conf数组   
                        writer.WriteEndObject();//} 网口
                    }
                    //writer.WritePropertyName("max_connection");
                    //writer.WriteValue(data.dataDevice_.maxconnectnumber);
                    //writer.WritePropertyName("remote_ip_num");
                    //writer.WriteValue(1);
                    //writer.WritePropertyName("remote_ip");
                    //writer.WriteStartArray();//[ remote_ip节点下数组
                    //writer.WriteValue(data.dataDevice_.ip0+"."+data.dataDevice_.ip1+"."+data.dataDevice_.ip2+"."+data.dataDevice_.ip3);
                    //writer.WriteValue("192.168.1.11");
                    //writer.WriteEndArray();//] remote_ip节点下数组
                    //writer.WritePropertyName("ip_port");
                    //writer.WriteValue(data.dataDevice_.port);
                    //writer.WritePropertyName("time_uint");
                    //writer.WriteValue("ms");
                    //writer.WritePropertyName("dev_id");
                    //writer.WriteValue(data.ID);
                    //writer.WritePropertyName("io_range");
                    //writer.WriteStartObject();
                    //writer.WritePropertyName("start");
                    //writer.WriteValue(Convert.ToInt32(data.serverstartaddr));
                    //writer.WritePropertyName("bytes");
                    //writer.WriteValue(data.dataDevice_.IOAddrLength);
                    //writer.WriteEndObject();



                }
                //writer.WriteEndArray();//] server节点下conf数组
                writer.WriteEndObject();// 添加}  server节点
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 | e.RowIndex < 0)
            {
                return;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "配置")
            {
                DataGridViewDisableButtonCell buttonCell =
                    (DataGridViewDisableButtonCell)dataGridView1.
                    Rows[e.RowIndex].Cells["配置"];
                int a = e.RowIndex;
                if (buttonCell.Enabled)
                {
                    ModbusServer.modbusserver mss = new modbusserver(e.RowIndex);
                    ModbusServerData data = serverDataManager.listServer.ElementAt(e.RowIndex);
                    data.serverstartaddr = utility.modbusAddr + UserControl1.mci.clientManage.modbusClientList.Count * utility.modbusMudule;
                    mss.getServerData(ref data);
                    mss.StartPosition = FormStartPosition.CenterScreen;
                   
                    mss.ShowDialog();
                }
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "info")
            {
                int index = dataGridView1.SelectedRows[0].Index;
                ModbusServerData data = serverDataManager.listServer[index];
                dataGridView2.RowCount = 4;
                dataGridView2.Rows[0].Cells[0].Value = "线圈寄存器";
                dataGridView2.Rows[0].Cells[1].Value = data.dataDevice_.coilCount;
                dataGridView2.Rows[0].Cells[2].Value = data.dataDevice_.coilIoAddrStart;
                dataGridView2.Rows[1].Cells[0].Value = "保持寄存器";
                dataGridView2.Rows[1].Cells[1].Value = data.dataDevice_.holdingCount;
                dataGridView2.Rows[1].Cells[2].Value = data.dataDevice_.holdingIoAddrStart;
                dataGridView2.Rows[2].Cells[0].Value = "离散输入寄存器";
                dataGridView2.Rows[2].Cells[1].Value = data.dataDevice_.decreteCount;
                dataGridView2.Rows[2].Cells[2].Value = data.dataDevice_.decreteIoAddrStart;
                dataGridView2.Rows[3].Cells[0].Value = "状态寄存器";
                dataGridView2.Rows[3].Cells[1].Value = data.dataDevice_.statusCount;
                dataGridView2.Rows[3].Cells[2].Value = data.dataDevice_.statusIoAddrStart;
            }
        }

        public void loadXml(XmlNode xn)
        {
            init = true;

            XmlNodeList nodeList = xn.ChildNodes;
            
            foreach (XmlNode childNode in nodeList)
            {
                XmlElement e = (XmlElement)childNode;
                string name = e.Name;

                ModbusServerData data = new ModbusServerData();

                int.TryParse(e.GetAttribute("ID"), out data.ID);
                //线圈个数
                int.TryParse(e.GetAttribute("coilcount"), out data.dataDevice_.coilCount);
                //保持
                int.TryParse(e.GetAttribute("holdingcount"), out data.dataDevice_.holdingCount);
                //离散输入
                int.TryParse(e.GetAttribute("decretecount"), out data.dataDevice_.decreteCount);
                //状态
                int.TryParse(e.GetAttribute("statuscount"), out data.dataDevice_.statusCount);
                //串口或者网口
                int.TryParse(e.GetAttribute("transform"), out data.dataDevice_.transform);
                //传输方式
                int.TryParse(e.GetAttribute("transformmode"), out data.dataDevice_.transformMode);
                //传输端口
                int.TryParse(e.GetAttribute("transformport"), out data.dataDevice_.transformport);
                //设备地址
                int.TryParse(e.GetAttribute("deviceaddr"), out data.dataDevice_.deviceAddr);
                //端口号
                int.TryParse(e.GetAttribute("port"), out data.dataDevice_.port);
                //最大连接数量
                int.TryParse(e.GetAttribute("maxconnect"), out data.dataDevice_.maxconnectnumber);
                //指定ip
                bool.TryParse(e.GetAttribute("ipfixed"), out data.dataDevice_.ipfixed);
                int.TryParse(e.GetAttribute("ip0"), out data.dataDevice_.ip0);
                int.TryParse(e.GetAttribute("ip1"), out data.dataDevice_.ip1);
                int.TryParse(e.GetAttribute("ip2"), out data.dataDevice_.ip2);
                int.TryParse(e.GetAttribute("ip3"), out data.dataDevice_.ip3);
                int.TryParse(e.GetAttribute("ip10"), out data.dataDevice_.ip10);
                int.TryParse(e.GetAttribute("ip11"), out data.dataDevice_.ip11);
                int.TryParse(e.GetAttribute("ip12"), out data.dataDevice_.ip12);
                int.TryParse(e.GetAttribute("ip13"), out data.dataDevice_.ip13);
                int.TryParse(e.GetAttribute("ip20"), out data.dataDevice_.ip20);
                int.TryParse(e.GetAttribute("ip21"), out data.dataDevice_.ip21);
                int.TryParse(e.GetAttribute("ip22"), out data.dataDevice_.ip22);
                int.TryParse(e.GetAttribute("ip23"), out data.dataDevice_.ip23);
                int.TryParse(e.GetAttribute("ip30"), out data.dataDevice_.ip30);
                int.TryParse(e.GetAttribute("ip31"), out data.dataDevice_.ip31);
                int.TryParse(e.GetAttribute("ip32"), out data.dataDevice_.ip32);
                int.TryParse(e.GetAttribute("ip33"), out data.dataDevice_.ip33);
                data.dataDevice_.IOAddrRange = e.GetAttribute("IOAddrRange");
                int.TryParse(e.GetAttribute("IOAddrRange"), out data.serverstartaddr);
                int.TryParse(e.GetAttribute("IOAddrLength"), out data.dataDevice_.IOAddrLength);
                int.TryParse(e.GetAttribute("SHMRange"), out data.dataDevice_.shmrange);
                int.TryParse(e.GetAttribute("SHMLength"), out data.dataDevice_.shmlength);
                data.dataDevice_.coilIoAddrStart = e.GetAttribute("coilstart");
                data.dataDevice_.holdingIoAddrStart = e.GetAttribute("holdingstart");
                data.dataDevice_.decreteIoAddrStart = e.GetAttribute("decretestart");
                data.dataDevice_.statusIoAddrStart = e.GetAttribute("statusstart");
                bool.TryParse(e.GetAttribute("ipfixed"), out data.dataDevice_.ipfixed);
                
                serverDataManager.listServer.Add(data);
            }

        }

       
    }
    
}
