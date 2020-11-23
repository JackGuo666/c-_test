using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LocalPLC.ModbusMaster;
using LocalPLC.ModbusSlave;
using ADELib;
using System.Xml;
using Newtonsoft.Json;
using System.IO;

namespace LocalPLC.ModbusSlave
{
    public partial class modbusslavemain : UserControl
    {

        public DataManager slaveDataManager = null;

        public modbusslavemain()
        {
            InitializeComponent();

            slaveDataManager = DataManager.GetInstance();

            init = false;
        }

        public void deleteTableRow()
        {
            for(int i = dataGridView1.Rows.Count - 1; i>= 0; i--)
            {
                dataGridView1.Rows.RemoveAt(i);
            }
        }


        private void createColumn()
        {
            DataGridViewDisableButtonColumn buttonColumn = new DataGridViewDisableButtonColumn();
            buttonColumn.Name = "配置";

            DataGridViewTextBoxColumn cellColumn = new DataGridViewTextBoxColumn();
            cellColumn.Name = "ID";

            dataGridView1.Columns.Add(cellColumn);
            dataGridView1.Columns.Add(buttonColumn);
        }

        bool init = false;
        public void initForm()
        {
            if(init == false)
            {
                return;
            }



            //if(dataGridView1.RowCount > 0)
            //{
            //    for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            //    {
            //        dataGridView1.Rows.RemoveAt(i);
            //    }
            //}
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Clear();


            createColumn();


            //if (dataGridView1.RowCount < 1)
            //{
            //    return;
            //}

            dataGridView1.RowCount += slaveDataManager.listSlave.Count;
            //DataGridViewDisableButtonColumn buttonColumn = new DataGridViewDisableButtonColumn();
            //buttonColumn.Name = "配置";

            //DataGridViewTextBoxColumn cellColumn = new DataGridViewTextBoxColumn();
            //cellColumn.Name = "ID";

            //dataGridView1.Columns.Add(cellColumn);
            //dataGridView1.Columns.Add(buttonColumn);



            for (int k = 0; k < slaveDataManager.listSlave.Count; k++)
            {
                ModbusSlaveData data = slaveDataManager.listSlave.ElementAt(k);
                dataGridView1.Rows[k].Cells["ID"].Value = data.ID;
                dataGridView1.Rows[k].Cells["配置"].Value = "..."/* + i.ToString()*/;
            }
            //if (dataGridView1.Columns.Count > 2)
            //{
            //    dataGridView1.Columns.RemoveAt(0);
            //}
        }



        public void saveXml(ref System.Xml.XmlElement elem, ref System.Xml.XmlDocument doc)
        {
            XmlElement elem1 = doc.CreateElement("modbusslave");
            //elem1.SetAttribute("tt", "张三");
            //elem1.SetAttribute("ttt", "三年一班");
            //elem1.SetAttribute("tttt", "性别");
            elem.AppendChild(elem1);

            
            for(int i = 0; i < slaveDataManager.listSlave.Count; i++)
            {
                ModbusSlaveData data = slaveDataManager.listSlave.ElementAt(i);
                XmlElement elem1_s = doc.CreateElement("s");
                elem1_s.SetAttribute("ID", data.ID.ToString());

                //slave详细信息
                //data.dataDevice_;
                XmlElement elem1_s_data = doc.CreateElement("data");
                elem1_s.SetAttribute("coilcount", data.dataDevice_.coilCount.ToString());
                elem1_s.SetAttribute("holdingcount", data.dataDevice_.holdingCount.ToString());
                elem1_s.SetAttribute("decretecount", data.dataDevice_.decreteCount.ToString());
                elem1_s.SetAttribute("statuscount", data.dataDevice_.statusCount.ToString());
                elem1_s.SetAttribute("IOAddrRange", data.dataDevice_.IOAddrRange);
                elem1_s.SetAttribute("IOAddrLength", data.dataDevice_.IOAddrLength.ToString ());
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
            writer.WritePropertyName("mbserial_slave");
            writer.WriteStartObject();//添加{  slave节点
            writer.WritePropertyName("number");
            writer.WriteValue(slaveDataManager.listSlave.Count);//number
                writer.WritePropertyName("conf");
                writer.WriteStartArray();//[  slave节点下conf数组
            for (int i = 0; i < slaveDataManager.listSlave.Count; i++)
            {
                ModbusSlaveData data = slaveDataManager.listSlave.ElementAt(i);
                
                writer.WriteStartObject();//{ slave下conf数组的配置信息
                writer.WritePropertyName("port");
                writer.WriteValue("ser_port"+ data.ID.ToString());
                string mode =null;
                if(data.dataDevice_.transformMode == 0)
                {
                    mode = "rtu";
                }
                else if (data.dataDevice_.transformMode == 1)
                {
                    mode = "ASCII";
                }
                writer.WritePropertyName("mode");
                writer.WriteValue(mode);
                writer.WritePropertyName("dev_id");
                writer.WriteValue(data.ID);
                writer.WritePropertyName("dev_namestr");
                writer.WriteValue("mb"+mode+"_slave"+data.ID.ToString());
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
                writer.WriteEndObject();//}   slave下conf数组的配置信息
                
            }
            writer.WriteEndArray();//]    slave节点下conf数组
            writer.WriteEndObject();//} slave节点
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void modbusslavemain_Load(object sender, EventArgs e)
        {
            init = true;

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            createColumn();
    
            //if (dataGridView1.Columns.Count > 2)
            //{
            //    dataGridView1.Columns.RemoveAt(0);
            //}
            dataGridView1.RowCount = /*8*/ 1 + slaveDataManager.listSlave.Count; ;
            dataGridView1.AutoSize = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            for (int i = 0; i < slaveDataManager.listSlave.Count; i++)
            {
                ModbusSlaveData data = slaveDataManager.listSlave.ElementAt(i);
                dataGridView1.Rows[i].Cells["ID"].Value = data.ID;
                dataGridView1.Rows[i].Cells["配置"].Value = "..."/* + i.ToString()*/;
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.RowCount;
            dataGridView1.RowCount += 1;

            // Set the text for each button.
            int i = row;


            ModbusSlaveData data = new ModbusSlaveData();

            // for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells["ID"].Value = row;
                data.ID = row;
                dataGridView1.Rows[i].Cells["配置"].Value = "..."/* + i.ToString()*/;
                //data.device = new DeviceData();

                slaveDataManager.listSlave.Add(data);
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

                if (buttonCell.Enabled)
                {
                    //MessageBox.Show(dataGridView1.Rows[e.RowIndex].
                    //    Cells[e.ColumnIndex].Value.ToString() +
                    //    " is enabled");
                    int a = e.RowIndex;
                    modbusslaveform form = new modbusslaveform(e.RowIndex);
                    ModbusSlaveData data = slaveDataManager.listSlave.ElementAt(e.RowIndex);
                    form.getSlaveData(ref data);
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
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
                slaveDataManager.listSlave.RemoveAt(index);
            }
        }

        public void loadXml(XmlNode xn)
        {
            init = true;

            XmlNodeList nodeList = xn.ChildNodes;
            foreach(XmlNode childNode in nodeList)
            {
                XmlElement e = (XmlElement)childNode;
                string name = e.Name;

                ModbusSlaveData data = new ModbusSlaveData();
                
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
                data.dataDevice_.IOAddrRange = e.GetAttribute("IOAddrRange");
                int.TryParse(e.GetAttribute("IOAddrLength"), out data.dataDevice_.IOAddrLength);
                data.dataDevice_.coilIoAddrStart = e.GetAttribute("coilstart");
                data.dataDevice_.holdingIoAddrStart = e.GetAttribute("holdingstart");
                data.dataDevice_.decreteIoAddrStart = e.GetAttribute("decretestart");
                data.dataDevice_.statusIoAddrStart = e.GetAttribute("statusstart");

                slaveDataManager.listSlave.Add(data);
            }

        }
    }
}
