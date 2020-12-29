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
using System.IO;

namespace LocalPLC.ModbusClient
{
    public partial class Clientindex : UserControl
    {
        public ModbusClientManage clientManage = new ModbusClientManage();
        public Clientindex()
        {
            InitializeComponent();
            //dataGridView1.AllowUserToAddRows = false;
            //this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void deleteTableRow()
        {
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                //int index = dataGridView1.SelectedRows[i].Index;

                dataGridView1.Rows.RemoveAt(i);
                clientManage.modbusClientList.RemoveAt(i);
            }
        }

        private void Clientindex_Load(object sender, EventArgs e)
        {
            init = true;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows.RemoveAt(i);
            }
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            DataGridViewTextBoxColumn cellColumn = new DataGridViewTextBoxColumn();
            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            btn.Name = "config";
            btn.HeaderText = "配置";
            btn.DefaultCellStyle.NullValue = ". . .";
            cellColumn.Name = "Client编号";
            btn1.Name = "info";
            btn1.HeaderText = "详细信息";
            btn1.DefaultCellStyle.NullValue = ". . .";
            dataGridView1.Columns.Add(cellColumn);
            dataGridView1.Columns.Add(btn);
            dataGridView1.Columns.Add(btn1);
            dataGridView1.RowCount = /*8*/ 1 + clientManage.modbusClientList.Count;
            dataGridView1.AutoSize = true;
            dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < clientManage.modbusClientList.Count; i++)
            {
                ModbusClientData data = clientManage.modbusClientList.ElementAt(i);
                dataGridView1.Rows[i].Cells["Client编号"].Value = data.ID;
                //dataGridView1.Rows[i].Cells["config"].Value = "..."/* + i.ToString()*/;

            }
            dataGridView2.AllowUserToAddRows = false;

        }
       

        //public int clientnumber;

        public void loadXml(XmlNode xn)
        {
            XmlNodeList nodeList = xn.ChildNodes;//创建xn的所有子节点的集合
            foreach (XmlNode childNode in nodeList)//遍历集合中所有的节点
            {
                XmlElement e = (XmlElement)childNode;
                string name = e.Name;
                string test = e.GetAttribute("name");//获取该节点中所有name属性的值
                Console.WriteLine(name);

                ModbusClientData data = new ModbusClientData();

                int.TryParse(e.GetAttribute("id"), out data.ID);//将所有id属性的值（字符串）,转换成int32类型，输出变量为data.ID
                int.TryParse(e.GetAttribute("clientstartaddr"), out data.clientstartaddr);
                data.transformChannel = e.GetAttribute("transformchannel");
                int.TryParse(e.GetAttribute("transformmode"), out data.transformMode);
                int.TryParse(e.GetAttribute("responsetimeout"), out data.responseTimeout);
                int.TryParse(e.GetAttribute("clientstartaddr"), out data.clientstartaddr);
                //data.transformChannel = int.TryParse(eChild.GetAttribute("transformchannel"));
                //读取device数据
                XmlNodeList nodeDeviceList = childNode.ChildNodes;//创建当前子设备节点下的所有子节点集合
                foreach (XmlNode childDeviceNode in nodeDeviceList)
                {
                    e = (XmlElement)childDeviceNode;//子设备节点
                    DeviceData deviceData = new DeviceData();
                    int.TryParse(e.GetAttribute("ID"), out deviceData.ID);//为各子节点赋值
                    deviceData.nameDev = e.GetAttribute("namedev");
                    deviceData.ipaddr = e.GetAttribute("ipaddr");
                    deviceData.serverAddr = e.GetAttribute("serveraddr");
                    int.TryParse(e.GetAttribute("responsetimeout"), out deviceData.reponseTimeout);
                    int.TryParse(e.GetAttribute("permittimeoutcount"), out deviceData.permitTimeoutCount);
                    int.TryParse(e.GetAttribute("reconnectinterval"), out deviceData.reconnectInterval);
                    //int.TryParse(e.GetAttribute("resetVaraible"), out deviceData.resetVaraible);
                    deviceData.resetVaraible = e.GetAttribute("resetvaraible");
                    int.TryParse(e.GetAttribute("devstartaddr"), out deviceData.devstartaddr);
                    int.TryParse(e.GetAttribute("devlength"), out deviceData.devlength);
                    //读取channel数据
                    XmlNodeList nodeChannelList = childDeviceNode.ChildNodes;
                    foreach (XmlNode childChannelNode in nodeChannelList)
                    {
                        e = (XmlElement)childChannelNode;
                        ChannelData channelData = new ChannelData();

                        int.TryParse(e.GetAttribute("ID"), out channelData.ID);
                        channelData.nameChannel = e.GetAttribute("namechannel");
                        int.TryParse(e.GetAttribute("msgtype"), out channelData.msgType);
                        channelData.msgdiscrib = e.GetAttribute("msgdiscrib");
                        int.TryParse(e.GetAttribute("pollingtime"), out channelData.pollingTime);
                        int.TryParse(e.GetAttribute("offset"), out channelData.Offset);
                        int.TryParse(e.GetAttribute("length"), out channelData.Length);
                        //int.TryParse(e.GetAttribute("writeoffset"), out channelData.writeOffset);
                        channelData.trigger_offset = e.GetAttribute("trigger_offset");
                        //int.TryParse(e.GetAttribute("writelength"), out channelData.writeLength);
                        channelData.error_offset = e.GetAttribute("error_offset");
                        int.TryParse(e.GetAttribute("channelstartaddr"), out channelData.channelstartaddr);
                        int.TryParse(e.GetAttribute("Channellength"), out channelData.Channellength);
                        int.TryParse(e.GetAttribute("type"), out channelData.type);
                        channelData.note = e.GetAttribute("note");
                        
                        deviceData.modbusChannelList.Add(channelData);
                    }


                    data.modbusDeviceList.Add(deviceData);
                }

                clientManage.add(data);//添加进xml





            }
        }

        public void saveXml(ref XmlElement elem, ref XmlDocument doc)
        {
            XmlElement elem1 = doc.CreateElement("modbusclient");
            elem1.SetAttribute("number", dataGridView1.RowCount.ToString());
            elem1.SetAttribute("time_unit", "ms");
            elem.AppendChild(elem1);
            for (int i = 0; i < clientManage.modbusClientList.Count; i++)//遍历所有的modbusclient集合
            {
                ModbusClientData data = clientManage.modbusClientList.ElementAt(i);
                XmlElement elem1_m = doc.CreateElement("modbusclient");//创建m节点
                elem1_m.SetAttribute("id", data.ID.ToString());//给id节点赋值，值为data.ID
                elem1_m.SetAttribute("clientstartaddr", data.clientstartaddr.ToString());
                //transformChannel com1 com2 com3
                elem1_m.SetAttribute("transformchannel", data.transformChannel);
                //0 TCP    1 UDP
                elem1_m.SetAttribute("transformmode", data.transformMode.ToString());
                elem1_m.SetAttribute("responsetimeout", data.responseTimeout.ToString());
                elem1_m.SetAttribute("clientstartaddr", data.clientstartaddr.ToString());
                //create devices
                for (int j = 0; j < data.modbusDeviceList.Count; j++)//循环添加每个设备的各参数值至xml
                {
                    DeviceData dataDev = data.modbusDeviceList.ElementAt(j);
                    XmlElement elem1_m_d = doc.CreateElement("device");
                    elem1_m_d.SetAttribute("ID", dataDev.ID.ToString());
                    elem1_m_d.SetAttribute("namedev", dataDev.nameDev.ToString());
                    elem1_m_d.SetAttribute("ipaddr", dataDev.ipaddr.ToString());
                    elem1_m_d.SetAttribute("serveraddr", dataDev.serverAddr.ToString());
                    elem1_m_d.SetAttribute("responsetimeout", dataDev.reponseTimeout.ToString());
                    elem1_m_d.SetAttribute("permittimeoutcount", dataDev.permitTimeoutCount.ToString());
                    elem1_m_d.SetAttribute("reconnectinterval", dataDev.reconnectInterval.ToString());
                    elem1_m_d.SetAttribute("resetvaraible", dataDev.resetVaraible.ToString());
                    elem1_m_d.SetAttribute("devstartaddr", dataDev.devstartaddr.ToString());
                    elem1_m_d.SetAttribute("devlength", dataDev.devlength.ToString());

                    //通道
                    for (int k = 0; k < dataDev.modbusChannelList.Count; k++)//循环添加通道至子设备节点下
                    {
                        ChannelData dataChannel = dataDev.modbusChannelList.ElementAt(k);
                        XmlElement elem1_m_d_c = doc.CreateElement("channel");
                        elem1_m_d_c.SetAttribute("ID", dataChannel.ID.ToString());
                        elem1_m_d_c.SetAttribute("namechannel", dataChannel.nameChannel);
                        elem1_m_d_c.SetAttribute("msgtype", dataChannel.msgType.ToString());
                        elem1_m_d_c.SetAttribute("msgdiscrib", dataChannel.msgdiscrib);
                        elem1_m_d_c.SetAttribute("pollingtime", dataChannel.pollingTime.ToString());
                        elem1_m_d_c.SetAttribute("offset", dataChannel.Offset.ToString());
                        elem1_m_d_c.SetAttribute("length", dataChannel.Length.ToString());
                        elem1_m_d_c.SetAttribute("trigger_offset", dataChannel.trigger_offset);
                        elem1_m_d_c.SetAttribute("error_offset", dataChannel.error_offset);
                        elem1_m_d_c.SetAttribute("channelstartaddr", dataChannel.channelstartaddr.ToString());
                        elem1_m_d_c.SetAttribute("Channellength", dataChannel.Channellength.ToString());
                        elem1_m_d_c.SetAttribute("type", dataChannel.type.ToString());
                        elem1_m_d_c.SetAttribute("note", dataChannel.note);

                        elem1_m_d.AppendChild(elem1_m_d_c);//将通道节点作为子节点加入设备节点
                    }
                    elem1_m.AppendChild(elem1_m_d);
                }
                //XmlElement elem1_d = doc.CreateElement("device");
                //elem1_m.SetAttribute("id", data.ID.ToString());

                elem1.AppendChild(elem1_m);
            }
        }
        public void saveJson(JsonTextWriter writer)
        {
            //添加modbusslave节点
            writer.WritePropertyName("mbtcp_client");
            writer.WriteStartObject();//添加{  client节点
            writer.WritePropertyName("number");
            writer.WriteValue(clientManage.modbusClientList.Count);//number
            writer.WritePropertyName("time_uint");
            writer.WriteValue("ms");//时间单位
            int index = 0;
                writer.WritePropertyName("conf");
                writer.WriteStartArray();//[ client节点下conf数组
            for (int i = 0; i < clientManage.modbusClientList.Count; i++)//遍历所有Client的集合
            {
                ModbusClientData data = clientManage.modbusClientList.ElementAt(i);
                
                writer.WriteStartObject();//{  client节点下device
                writer.WritePropertyName("port");
                writer.WriteValue("ethif_" + data.ID.ToString());
                string mode = null;
                if (data.transformMode == 0)
                {
                    mode = "tcp";
                }
                else if (data.transformMode == 1)
                {
                    mode = "udp";
                }
                writer.WritePropertyName("mode");
                writer.WriteValue(mode);
                writer.WritePropertyName("dev_namestr");
                writer.WriteValue("mb"+mode+"_client"+data.ID);
                writer.WritePropertyName("slave");
                writer.WriteStartObject();//{  slave节点 从设备信息
                writer.WritePropertyName("num");
                writer.WriteValue(data.modbusDeviceList.Count);
                if (i > 0)
                {
                    index = i * clientManage.modbusClientList[i - 1].modbusDeviceList.Count;
                }
                    writer.WritePropertyName("conf");
                    writer.WriteStartArray();//[  slave节点conf
                for (int j = 0; j < data.modbusDeviceList.Count; j++)//循环添加每个设备的各参数至
                {
                    DeviceData dataDev = data.modbusDeviceList.ElementAt(j);
                    
                    writer.WriteStartObject();//{  conf数组下节点，从设备信息
                    writer.WritePropertyName("slave_ip");
                    writer.WriteValue(dataDev.serverAddr);
                    writer.WritePropertyName("slave_port");
                    writer.WriteValue(502);
                    writer.WritePropertyName("response_timeout");
                    writer.WriteValue(dataDev.reponseTimeout);
                    writer.WritePropertyName("retry_interval");
                    writer.WriteValue(dataDev.reconnectInterval);
                    writer.WritePropertyName("timeout_cnt_max");
                    writer.WriteValue(dataDev.permitTimeoutCount);
                    writer.WritePropertyName("io_range");
                    writer.WriteStartObject();//{  conf数组下 iorange                
                    writer.WritePropertyName("start");
                    writer.WriteValue(dataDev.devstartaddr);
                    writer.WritePropertyName("bytes");
                    writer.WriteValue(dataDev.devlength);
                    writer.WriteEndObject();//}    conf数组下 iorange    
                    writer.WritePropertyName("restart_offset");
                    writer.WriteValue(0);
                    writer.WritePropertyName("channel_cfg");
                    writer.WriteStartObject();//{  channel_cfg节点
                    writer.WritePropertyName("num");
                    writer.WriteValue(dataDev.modbusChannelList.Count);
                        writer.WritePropertyName("conf");
                        writer.WriteStartArray();//[  channel_cfg节点下conf数组

                    for (int k = 0; k < dataDev.modbusChannelList.Count; k++)//循环添加通道至子设备节点下
                    {
                        ChannelData dataChannel = dataDev.modbusChannelList.ElementAt(k);
                        writer.WriteStartObject();//{  channel_cfg节点下conf数组中channel信息
                        writer.WritePropertyName("channel_id");
                        writer.WriteValue(dataChannel.ID);
                        writer.WritePropertyName("channel_name");
                        writer.WriteValue("channel" + dataChannel.ID);
                        writer.WritePropertyName("msg_type");
                        writer.WriteValue(dataChannel.msgType);
                        writer.WritePropertyName("polling_time");
                        writer.WriteValue(dataChannel.pollingTime);
                        writer.WritePropertyName("offset");
                        writer.WriteValue(dataChannel.Offset);
                        writer.WritePropertyName("quantity");
                        writer.WriteValue(dataChannel.Length);
                        writer.WritePropertyName("io_offset");
                        writer.WriteValue(dataChannel.channelstartaddr + 2 - data.clientstartaddr);
                        writer.WritePropertyName("io_bytes");
                        writer.WriteValue(dataChannel.Channellength - 2);
                        writer.WritePropertyName("trigger_offset");
                        writer.WriteValue(dataChannel.channelstartaddr-data.clientstartaddr);
                        writer.WritePropertyName("error_offset");
                        writer.WriteValue(dataChannel.channelstartaddr + 1 - data.clientstartaddr);
                        writer.WritePropertyName("direction");
                        writer.WriteValue("in");
                        writer.WriteEndObject();//} channel_cfg节点下conf数组中channel信息

                    }
                    writer.WriteEndArray();//] channel_cfg节点下conf数组
                    writer.WriteEndObject();//}  channel_cfg节点
                    writer.WriteEndObject();//}  conf数组下节点，从设备信息
                }
                writer.WriteEndArray();//]  slave节点conf
                writer.WriteEndObject();//} slave节点 从设备信息
                writer.WriteEndObject();//} client节点下device
            }
            writer.WriteEndArray();//] client节点下conf数组
            writer.WriteEndObject();//添加}  client节点
        }

        private enum COLUMNNAME : int
        {
            ID
        };
        private void button1_Click(object sender, EventArgs e)
        {
            int rowcount = dataGridView1.RowCount;
            if (dataGridView1.RowCount >= utility.clientCount)
            {
                string err = string.Format("client最大个数是{0}", utility.clientCount);
                utility.PrintError(err);
                return;
            }
            dataGridView1.RowCount += 1;
            int i = rowcount;
            ModbusClientData data = new ModbusClientData();
            {
                dataGridView1.Rows[i].Cells["Client编号"].Value = rowcount;
                data.ID = rowcount;
                //dataGridView1.Rows[i].Cells[columnConfig].Value = "..."/* + i.ToString()*/;
                //data.device = new DeviceData();
                data.clientstartaddr = 1000 + 1000 * i;
                clientManage.modbusClientList.Add(data);
            }
            
            
        }
        public void refreshID()
        {
            for (int i = 0; i < clientManage.modbusClientList.Count; i++)
            {
                clientManage.modbusClientList[i].ID = i;
                clientManage.modbusClientList[i].clientstartaddr = 1000 + 1000 * i;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // int row = dataGridView1.SelectedRows[0];
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
                clientManage.modbusClientList.RemoveAt(index);
            }
            refreshID();
        }

        UserControl1 user1 = new UserControl1();
        
        public int a;
        public string clientnumber
        {
            
            set { clientnumber = value; }
            get { return this.label1.Text; }
        }
        //public void cilentnumber1(int cn)
        //{
        //    this.label1.Text = 
        //}
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            
            //a = dataGridView1.SelectedRows[0].Index;
        }
        bool init = false;
        public void initForm()
        {
            //load 事件只加载一次，工程重新加载需清空之前函数
            //init是load加载后，initForm才可以加载
            if (init == false)
            {
                return;
            }

            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView1.Rows.RemoveAt(i);
            }



            //DataGridViewDisableButtonColumn buttonColumn = new DataGridViewDisableButtonColumn();
            //buttonColumn.Name = columnConfig;

            //DataGridViewTextBoxColumn cellColumn = new DataGridViewTextBoxColumn();
            //cellColumn.Name = "ID";

            //dataGridView1.Columns.Add(cellColumn);
            //dataGridView1.Columns.Add(buttonColumn);
            if (clientManage.modbusClientList.Count > 0)
            dataGridView1.RowCount += /*8*/  clientManage.modbusClientList.Count;
            //dataGridView1.AutoSize = true;
            //dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
            //    DataGridViewContentAlignment.MiddleCenter;

            for (int i = 0; i < clientManage.modbusClientList.Count; i++)
            {
                ModbusClientData data = clientManage.modbusClientList.ElementAt(i);
                dataGridView1.Rows[i].Cells[0].Value = data.ID;
                //dataGridView1.Rows[i].Cells[""].Value = "..."/* + i.ToString()*/;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex < 0 | e.RowIndex < 0)
            {
                return;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "config")
            {
                //DataGridViewDisableButtonCell buttonCell =
                //    (DataGridViewDisableButtonCell)dataGridView1.
                //    Rows[e.RowIndex].Cells["config"];

                //if (buttonCell.Enabled)
                {
                    //MessageBox.Show(dataGridView1.Rows[e.RowIndex].
                    //    Cells[e.ColumnIndex].Value.ToString() +
                    //    " is enabled");

                    ModbusClient.modbusclient1 mct1 = new modbusclient1();
                    ModbusClient.ClientChannel cc1 = new ClientChannel();
                    //modbusclientDeviceform form = new modbusmasterDeviceform();
                    ModbusClientData data = clientManage.modbusClientList.ElementAt(e.RowIndex);
                    this.label1.Text = this.dataGridView1.SelectedRows[0].Index.ToString();
                    
                    mct1.ClientNumber(this.label1.Text);
                    
                    mct1.getClientData(ref data);
                    //cc1.ClientNumber(this.label1.Text);
                    // cc1.getClientData(ref data);
                    mct1.StartPosition = FormStartPosition.CenterScreen;
                    mct1.ShowDialog();
                }
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "info")
            {
                int index = dataGridView1.SelectedRows[0].Index;
                ModbusClientData data = clientManage.modbusClientList[index];
                int devcount = data.modbusDeviceList.Count;
                int channelcount = 0;
                int [] devchannelnum = new int[8];
                for (int i = 0; i<devcount;i++)
                {
                    channelcount += data.modbusDeviceList[i].modbusChannelList.Count;
                    devchannelnum[i] = data.modbusDeviceList[i].modbusChannelList.Count;
                }
                int a = 0;
                if(channelcount > 0)
                dataGridView2.RowCount = channelcount;
                for (int m = 0; m < devcount; m++)
                {
                    if(m>0)
                    {
                        a  = a+devchannelnum[m-1];
                    }
 
                    for (int n =0;n< data.modbusDeviceList[m].modbusChannelList.Count;n++)
                    {
                        
                        dataGridView2.Rows[a + n].Cells[0].Value = data.modbusDeviceList[m].nameDev;
                        dataGridView2.Rows[a + n].Cells[1].Value = data.modbusDeviceList[m].modbusChannelList[n].nameChannel;
                        dataGridView2.Rows[a + n].Cells[2].Value = data.modbusDeviceList[m].modbusChannelList[n].msgdiscrib;
                        dataGridView2.Rows[a + n].Cells[3].Value = data.modbusDeviceList[m].modbusChannelList[n].channelstartaddr+2;
                        dataGridView2.Rows[a + n].Cells[4].Value = data.modbusDeviceList[m].modbusChannelList[n].Length;
                        dataGridView2.Rows[a + n].Cells[5].Value = data.modbusDeviceList[m].modbusChannelList[n].channelstartaddr;
                        dataGridView2.Rows[a + n].Cells[6].Value = data.modbusDeviceList[m].modbusChannelList[n].channelstartaddr+1;

                    }
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Object obj = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
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
                clientManage.modbusClientList.ElementAt(e.RowIndex).ID = int.Parse(str);
            }
        }
    }

    
    
 }
    

