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

            DataGridViewTextBoxColumn cellColumn = new DataGridViewTextBoxColumn();
            cellColumn.Name = "ID";

            buttonColumn.DefaultCellStyle.NullValue = ". . .";
            

            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Columns.Add(cellColumn);
            dataGridView1.Columns.Add(buttonColumn);
            if (dataGridView1.Columns.Count > 2)
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
            if (dataGridView1.Columns.Count > 2)
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
           

        }

        public void saveXml(ref System.Xml.XmlElement elem, ref System.Xml.XmlDocument doc)
        {
            XmlElement elem1 = doc.CreateElement("modbusserver");
            elem.AppendChild(elem1);
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

                elem1_s.SetAttribute("transformmode", data.dataDevice_.transformMode.ToString());
                elem1_s.SetAttribute("deviceaddr", data.dataDevice_.deviceAddr.ToString());
                elem1_s.SetAttribute("port", data.dataDevice_.port.ToString());
                elem1_s.SetAttribute("maxconnect", data.dataDevice_.maxconnectnumber.ToString());
                elem1_s.SetAttribute("ip0", data.dataDevice_.ip0.ToString());
                elem1_s.SetAttribute("ip1", data.dataDevice_.ip1.ToString());
                elem1_s.SetAttribute("ip2", data.dataDevice_.ip2.ToString());
                elem1_s.SetAttribute("ip3", data.dataDevice_.ip3.ToString());
                elem1_s.SetAttribute("IOAddrRange", data.dataDevice_.IOAddrRange);
                elem1_s.SetAttribute("IOAddrLength", data.dataDevice_.IOAddrLength.ToString());
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
            //添加modbusslave节点
            writer.WritePropertyName("mbtcp_server");
            writer.WriteStartObject();//添加{  server节点
            writer.WritePropertyName("number");
            writer.WriteValue(serverDataManager.listServer.Count);//number
                writer.WritePropertyName("conf");
                writer.WriteStartArray();//[ server节点下conf数组
            for (int i = 0; i < serverDataManager.listServer.Count; i++)
            {
                ModbusServerData data = serverDataManager.listServer.ElementAt(i);
                
                writer.WriteStartObject();//{ server节点下conf数组中配置信息
                writer.WritePropertyName("port");
                writer.WriteValue("ethif_"+ data.ID.ToString());
                string ipfixed = null;
                if (data.dataDevice_.ipconnect == 0)
                {
                    ipfixed = "false";
                }
                else if (data.dataDevice_.ipconnect == 1)
                {
                    ipfixed = "true";
                }
                writer.WritePropertyName("remote_ip_fixed");
                writer.WriteValue(ipfixed);
                writer.WritePropertyName("max_connection");
                writer.WriteValue(data.dataDevice_.maxconnectnumber);
                writer.WritePropertyName("remote_ip_num");
                writer.WriteValue(1);
                writer.WritePropertyName("remote_ip");
                writer.WriteValue(data.dataDevice_.ip0+"."+data.dataDevice_.ip1+"."+data.dataDevice_.ip2+"."+data.dataDevice_.ip3);
                writer.WritePropertyName("ip_port");
                writer.WriteValue(data.dataDevice_.port);
                writer.WritePropertyName("time_uint");
                writer.WriteValue("ms");
                writer.WritePropertyName("dev_id");
                writer.WriteValue(data.ID);
                writer.WritePropertyName("io_range");
                writer.WriteStartObject();
                writer.WritePropertyName("start");
                writer.WriteValue(data.dataDevice_.IOAddrRange);
                writer.WritePropertyName("bytes");
                writer.WriteValue(data.dataDevice_.IOAddrLength);
                writer.WriteEndObject();
                //离散
                writer.WritePropertyName("discrete");
                writer.WriteStartObject();
                writer.WritePropertyName("addr_type");
                writer.WriteValue("IO_INPUT");
                writer.WritePropertyName("start");
                writer.WriteValue(data.dataDevice_.decreteIoAddrStart);
                writer.WritePropertyName("num");
                writer.WriteValue(data.dataDevice_.decreteCount);
                writer.WriteEndObject();
                //线圈
                writer.WritePropertyName("coils");
                writer.WriteStartObject();
                writer.WritePropertyName("addr_type");
                writer.WriteValue("IO_INOUT");
                writer.WritePropertyName("start");
                writer.WriteValue(data.dataDevice_.coilIoAddrStart);
                writer.WritePropertyName("num");
                writer.WriteValue(data.dataDevice_.coilCount);
                writer.WriteEndObject();
                //状态
                writer.WritePropertyName("regs");
                writer.WriteStartObject();
                writer.WritePropertyName("addr_type");
                writer.WriteValue("IO_INPUT");
                writer.WritePropertyName("start");
                writer.WriteValue(data.dataDevice_.statusIoAddrStart);
                writer.WritePropertyName("num");
                writer.WriteValue(data.dataDevice_.statusCount);
                writer.WriteEndObject();
                //保持
                writer.WritePropertyName("holding");
                writer.WriteStartObject();
                writer.WritePropertyName("addr_type");
                writer.WriteValue("IO_INOUT");
                writer.WritePropertyName("start");
                writer.WriteValue(data.dataDevice_.holdingIoAddrStart);
                writer.WritePropertyName("num");
                writer.WriteValue(data.dataDevice_.holdingCount);
                writer.WriteEndObject();//}
                writer.WriteEndObject();//} server节点下conf数组中配置信息
                
            }
            writer.WriteEndArray();//] server节点下conf数组
            writer.WriteEndObject();// 添加{  server节点
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
                    mss.getServerData(ref data);
                    mss.ShowDialog();
                }
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
                //传输方式
                int.TryParse(e.GetAttribute("transformmode"), out data.dataDevice_.transformMode);
                //设备地址
                int.TryParse(e.GetAttribute("deviceaddr"), out data.dataDevice_.deviceAddr);
                //端口号
                int.TryParse(e.GetAttribute("port"), out data.dataDevice_.port);
                //最大连接数量
                int.TryParse(e.GetAttribute("maxconnect"), out data.dataDevice_.maxconnectnumber);
                //指定ip
                int.TryParse(e.GetAttribute("ip0"), out data.dataDevice_.ip0);
                int.TryParse(e.GetAttribute("ip1"), out data.dataDevice_.ip1);
                int.TryParse(e.GetAttribute("ip2"), out data.dataDevice_.ip2);
                int.TryParse(e.GetAttribute("ip3"), out data.dataDevice_.ip3);
                data.dataDevice_.IOAddrRange = e.GetAttribute("IOAddrRange");
                int.TryParse(e.GetAttribute("IOAddrLength"), out data.dataDevice_.IOAddrLength);
                data.dataDevice_.coilIoAddrStart = e.GetAttribute("coilstart");
                data.dataDevice_.holdingIoAddrStart = e.GetAttribute("holdingstart");
                data.dataDevice_.decreteIoAddrStart = e.GetAttribute("decretestart");
                data.dataDevice_.statusIoAddrStart = e.GetAttribute("statusstart");
                int.TryParse(e.GetAttribute("ipconnect"), out data.dataDevice_.ipconnect);
                
                serverDataManager.listServer.Add(data);
            }

        }
    }
    
}
