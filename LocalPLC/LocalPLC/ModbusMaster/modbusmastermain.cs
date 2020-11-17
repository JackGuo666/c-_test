using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ADELib;
using System.Data.Common;
using System.Xml;
using LocalPLC;
using Newtonsoft.Json;
using System.IO;

namespace LocalPLC.ModbusMaster
{
    public partial class modbusmastermain : UserControl
    {
        

        private string columnConfig = "配置";
        public ModbusMasterManage masterManage = new ModbusMasterManage();
        public modbusmastermain()
        {
            InitializeComponent();

           
        }

        public void deleteTableRow()
        {
            for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
            {
                //int index = dataGridView1.SelectedRows[i].Index;

                dataGridView1.Rows.RemoveAt(i);
                masterManage.modbusMastrList.RemoveAt(i);
            }
        }

        public void loadXml(XmlNode xn)
        {
            XmlNodeList nodeList = xn.ChildNodes;//创建xn的所有子节点的集合
            foreach (XmlNode childNode in nodeList)//遍历集合中所有的节点
            {
                XmlElement e = (XmlElement)childNode;
                string name = e.Name;
                string test = e.GetAttribute("name");//获取该节点中所有name属性的值
                Console.WriteLine(name);

                ModbusMasterData data = new ModbusMasterData();

                int.TryParse(e.GetAttribute("id"), out data.ID);//将所有id属性的值（字符串）,转换成int32类型，输出变量为data.ID
                data.transformChannel = e.GetAttribute("transformchannel");
                int.TryParse(e.GetAttribute("transformmode"), out data.transformMode);
                int.TryParse(e.GetAttribute("responsetimeout"), out data.responseTimeout);

                //data.transformChannel = int.TryParse(eChild.GetAttribute("transformchannel"));
                //读取device数据
                XmlNodeList nodeDeviceList = childNode.ChildNodes;//创建当前子设备节点下的所有子节点集合
                foreach(XmlNode childDeviceNode in nodeDeviceList)
                {
                    e = (XmlElement)childDeviceNode;//子设备节点
                    DeviceData deviceData = new DeviceData();
                    int.TryParse(e.GetAttribute("ID"), out deviceData.ID);//为各子节点赋值
                    deviceData.nameDev = e.GetAttribute("namedev");
                    deviceData.slaveAddr = e.GetAttribute("slaveaddr");
                    int.TryParse(e.GetAttribute("responsetimeout"), out deviceData.reponseTimeout);
                    int.TryParse(e.GetAttribute("permittimeoutcount"), out deviceData.permitTimeoutCount);
                    int.TryParse(e.GetAttribute("reconnectinterval"), out deviceData.reconnectInterval);
                    deviceData.resetVaraible = e.GetAttribute("resetvaraible");

                    //读取channel数据
                    XmlNodeList nodeChannelList = childDeviceNode.ChildNodes;
                    foreach(XmlNode childChannelNode in nodeChannelList)
                    {
                        e = (XmlElement)childChannelNode;
                        ChannelData channelData = new ChannelData();

                        int.TryParse(e.GetAttribute("ID"), out channelData.ID);
                        channelData.nameChannel = e.GetAttribute("namechannel");
                        int.TryParse(e.GetAttribute("msgtype"), out channelData.msgType);
                        int.TryParse(e.GetAttribute("pollingtime"), out channelData.pollingTime);
                        int.TryParse(e.GetAttribute("readoffset"), out channelData.readOffset);
                        int.TryParse(e.GetAttribute("readlength"), out channelData.readLength);
                        int.TryParse(e.GetAttribute("writeoffset"), out channelData.writeOffset);
                        int.TryParse(e.GetAttribute("writelength"), out channelData.writeLength);
                        channelData.note = e.GetAttribute("note");

                        deviceData.modbusChannelList.Add(channelData);
                    }


                    data.modbusDeviceList.Add(deviceData);
                }

                masterManage.add(data);//添加进xml



                

            }
        }
        modbusmasterDeviceform mmdf = new modbusmasterDeviceform();
        public void saveXml(ref XmlElement elem, ref XmlDocument doc)
        {
            XmlElement elem1 = doc.CreateElement("modbusmaster");
            
            elem1.SetAttribute("number", dataGridView1.RowCount.ToString());
            elem1.SetAttribute("time_unit", "ms");
            
            elem.AppendChild(elem1);
            //XmlElement elemc = doc.CreateElement("conf");
            //elemc.SetAttribute("mode",);
            //master项
            for(int i = 0; i < masterManage.modbusMastrList.Count; i++)//遍历所有的modbusmaster集合
            {
                ModbusMasterData data = masterManage.modbusMastrList.ElementAt(i);
                XmlElement elem1_m = doc.CreateElement("m");//创建m节点
                elem1_m.SetAttribute("id", data.ID.ToString());//给id节点赋值，值为data.ID
                //transformChannel com1 com2 com3
                elem1_m.SetAttribute("transformchannel", data.transformChannel);
                //0 RTU    1 ASCII
                elem1_m.SetAttribute("transformmode", data.transformMode.ToString());
                elem1_m.SetAttribute("responsetimeout", data.responseTimeout.ToString());

                //create devices
                for(int j = 0; j < data.modbusDeviceList.Count; j ++)//循环添加每个设备的各参数值至xml
                {
                    DeviceData dataDev = data.modbusDeviceList.ElementAt(j);
                    XmlElement elem1_m_d = doc.CreateElement("device");
                    elem1_m_d.SetAttribute("ID", dataDev.ID.ToString());
                    elem1_m_d.SetAttribute("namedev", dataDev.nameDev.ToString());
                    elem1_m_d.SetAttribute("slaveaddr", dataDev.slaveAddr.ToString());
                    elem1_m_d.SetAttribute("responsetimeout", dataDev.reponseTimeout.ToString());
                    elem1_m_d.SetAttribute("permittimeoutcount", dataDev.permitTimeoutCount.ToString());
                    elem1_m_d.SetAttribute("reconnectinterval", dataDev.reconnectInterval.ToString());
                    elem1_m_d.SetAttribute("resetvaraible", dataDev.resetVaraible);

                    //通道
                    for(int k = 0; k < dataDev.modbusChannelList.Count; k++)//循环添加通道至子设备节点下
                    {
                        ChannelData dataChannel = dataDev.modbusChannelList.ElementAt(k);
                        XmlElement elem1_m_d_c = doc.CreateElement("channel");
                        elem1_m_d_c.SetAttribute("ID", dataChannel.ID.ToString());
                        elem1_m_d_c.SetAttribute("namechannel", dataChannel.nameChannel);
                        elem1_m_d_c.SetAttribute("msgtype", dataChannel.msgType.ToString());
                        elem1_m_d_c.SetAttribute("pollingtime", dataChannel.pollingTime.ToString());
                        elem1_m_d_c.SetAttribute("readoffset", dataChannel.readOffset.ToString());
                        elem1_m_d_c.SetAttribute("readlength", dataChannel.readLength.ToString());
                        elem1_m_d_c.SetAttribute("writeoffset", dataChannel.writeOffset.ToString());
                        elem1_m_d_c.SetAttribute("writelength", dataChannel.writeLength.ToString());
                        elem1_m_d_c.SetAttribute("note", dataChannel.note.ToString());

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
            writer.WritePropertyName("mbserial_master");
            writer.WriteStartObject();//添加{  client节点
            writer.WritePropertyName("number");
            writer.WriteValue(masterManage.modbusMastrList.Count);//number
            writer.WritePropertyName("time_uint");
            writer.WriteValue("ms");//时间单位
            int index = 0;
            writer.WritePropertyName("conf");
            writer.WriteStartArray();//[ client节点下conf数组
            for (int i = 0; i < masterManage.modbusMastrList.Count; i++)//遍历所有Client的集合
            {
                ModbusMasterData data = masterManage.modbusMastrList.ElementAt(i);

                writer.WriteStartObject();//{  client节点下device
                writer.WritePropertyName("port");
                writer.WriteValue("ser_port" + data.ID.ToString());
                string mode = null;
                if (data.transformMode == 0)
                {
                    mode = "rtu";
                }
                else if (data.transformMode == 1)
                {
                    mode = "ascii";
                }
                writer.WritePropertyName("mode");
                writer.WriteValue(mode);
                writer.WritePropertyName("dev_namestr");
                writer.WriteValue("mbserial" + "_master" + data.ID);
                writer.WritePropertyName("slave");
                writer.WriteStartObject();//{  slave节点 从设备信息
                writer.WritePropertyName("num");
                writer.WriteValue(data.modbusDeviceList.Count);
                if (i > 0)
                {
                    index = i * masterManage.modbusMastrList[i - 1].modbusDeviceList.Count;
                }
                writer.WritePropertyName("conf");
                writer.WriteStartArray();//[  slave节点conf
                for (int j = 0; j < data.modbusDeviceList.Count; j++)//循环添加每个设备的各参数至
                {
                    DeviceData dataDev = data.modbusDeviceList.ElementAt(j);

                    writer.WriteStartObject();//{  conf数组下节点，从设备信息
                    writer.WritePropertyName("slave_id");
                    writer.WriteValue(dataDev.ID);
                    writer.WritePropertyName("response_timeout");
                    writer.WriteValue(dataDev.reponseTimeout);
                    
                    writer.WritePropertyName("timeout_cnt_max");
                    writer.WriteValue(dataDev.permitTimeoutCount);
                    writer.WritePropertyName("retry_interval");
                    writer.WriteValue(dataDev.reconnectInterval);
                    writer.WritePropertyName("io_range");
                    writer.WriteStartObject();//{  conf数组下 iorange                
                    writer.WritePropertyName("start");
                    writer.WriteValue(3000 + 1024 * (index+j));//io范围这块上位机设计方案中在master和client中并没有提到，需要确认
                    writer.WritePropertyName("bytes");
                    writer.WriteValue(1024);
                    writer.WriteEndObject();//}    conf数组下 iorange    
                    writer.WritePropertyName("channel_cfg");
                    writer.WriteStartObject();//{  channel_cfg节点
                    writer.WritePropertyName("num");
                    writer.WriteValue(data.modbusDeviceList.Count);
                    writer.WritePropertyName("conf");
                    writer.WriteStartArray();//[  channel_cfg节点下conf数组

                    for (int k = 0; k < dataDev.modbusChannelList.Count; k++)//循环添加通道至子设备节点下
                    {
                        ChannelData dataChannel = dataDev.modbusChannelList.ElementAt(k);
                        writer.WriteStartObject();//{  channel_cfg节点下conf数组中channel信息
                        writer.WritePropertyName("channel_id");
                        writer.WriteValue(dataChannel.ID);
                        writer.WritePropertyName("channel"+dataChannel.ID);
                        writer.WriteValue(dataChannel.nameChannel);
                        writer.WritePropertyName("msg_type");
                        writer.WriteValue(dataChannel.msgType);
                        writer.WritePropertyName("trig_mode");//trig_mode的含义？？
                        writer.WriteValue(0);
                        writer.WritePropertyName("polling_time");
                        writer.WriteValue(dataChannel.pollingTime);
                        writer.WritePropertyName("offset");//偏移这块和我们上位机草图设计有一些出入，需要确认
                        writer.WriteValue(dataChannel.readOffset);
                        writer.WritePropertyName("quantity");
                        writer.WriteValue(dataChannel.readLength);
                        writer.WritePropertyName("io_offset");
                        writer.WriteValue(dataChannel.writeOffset);
                        writer.WritePropertyName("io_bytes");
                        writer.WriteValue(dataChannel.writeLength);
                        writer.WritePropertyName("trigger_offset");
                        writer.WriteValue(101);
                        writer.WritePropertyName("error_offset");
                        writer.WriteValue(102);
                        writer.WritePropertyName("direction");//direction参数的含义？？
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
        //ttttttttttttttttttttttttttttttt
        //ttest
        private void button_add_Click(object sender, EventArgs e)
        {
            if(dataGridView1.RowCount >= utility.masterCount)
            {
                string err = string.Format("master最大个数是{0}", utility.masterCount);
                utility.PrintError(err);
                return;
            }


            int row = dataGridView1.RowCount;
            dataGridView1.RowCount += 1;
            
            // Set the text for each button.
            int i = row;

            ModbusMasterData data = new ModbusMasterData();

            // for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells["ID"].Value = row;
                data.ID = row;
                dataGridView1.Rows[i].Cells[columnConfig].Value = "..."/* + i.ToString()*/;
                //data.device = new DeviceData();

                masterManage.modbusMastrList.Add(data);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            //load 事件只加载一次，工程重新加载需清空之前函数
            //init是load加载后，initForm才可以加载
            if(init == false)
            {
                return;
            }

            //for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            //{
            //    dataGridView1.Rows.RemoveAt(i);
            //}

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            createColumn();


            //DataGridViewDisableButtonColumn buttonColumn = new DataGridViewDisableButtonColumn();
            //buttonColumn.Name = columnConfig;

            //DataGridViewTextBoxColumn cellColumn = new DataGridViewTextBoxColumn();
            //cellColumn.Name = "ID";

            //dataGridView1.Columns.Add(cellColumn);
            //dataGridView1.Columns.Add(buttonColumn);

            dataGridView1.RowCount += /*8*/  masterManage.modbusMastrList.Count;
            //dataGridView1.AutoSize = true;
            //dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
            //    DataGridViewContentAlignment.MiddleCenter;

            for (int i = 0; i < masterManage.modbusMastrList.Count; i++)
            {
                ModbusMasterData data = masterManage.modbusMastrList.ElementAt(i);
                dataGridView1.Rows[i].Cells["ID"].Value = data.ID;
                dataGridView1.Rows[i].Cells[columnConfig].Value = "..."/* + i.ToString()*/;
            }
        }

        public void modbusmastermain_Load(object sender, EventArgs e)
        {
            init = true;

            //for(int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    dataGridView1.Rows.RemoveAt(i);
            //}
            dataGridView1.Rows.Clear();

            //for (int i = 0; i < dataGridView1.Columns.Count; i++)
            //{
            //    dataGridView1.Columns.RemoveAt(i);
            //}



            createColumn();
            dataGridView1.RowCount = /*8*/ 1 + masterManage.modbusMastrList.Count;
            dataGridView1.AutoSize = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            for (int i = 0; i < masterManage.modbusMastrList.Count; i++)
            {
                ModbusMasterData data = masterManage.modbusMastrList.ElementAt(i);
                dataGridView1.Rows[i].Cells["ID"].Value = data.ID;
                dataGridView1.Rows[i].Cells[columnConfig].Value = "..."/* + i.ToString()*/;
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 | e.RowIndex < 0)
            {
                return;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == columnConfig)
            {
                DataGridViewDisableButtonCell buttonCell =
                    (DataGridViewDisableButtonCell)dataGridView1.
                    Rows[e.RowIndex].Cells[columnConfig];

                if (buttonCell.Enabled)
                {
                    //MessageBox.Show(dataGridView1.Rows[e.RowIndex].
                    //    Cells[e.ColumnIndex].Value.ToString() +
                    //    " is enabled");

                    modbusmasterDeviceform form = new modbusmasterDeviceform();
                    ModbusMasterData data = masterManage.modbusMastrList.ElementAt(e.RowIndex);
                    form.getMasterData(ref data);
                    form.ShowDialog();
                }
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
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
                masterManage.modbusMastrList.RemoveAt(index);
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

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
                masterManage.modbusMastrList.ElementAt(e.RowIndex).ID = int.Parse(str);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }



    public class ChannelData
    {
        public int ID;
        public string nameChannel;
        public int msgType;
        public int pollingTime;
        public int readOffset;
        public int readLength;
        public int writeOffset;
        public int writeLength;
        public string note;
    }

    public class DeviceData
    {
        public int ID;
        public string nameDev;
        public string slaveAddr;
        public int reponseTimeout;
        public int permitTimeoutCount;
        public int reconnectInterval;
        public string resetVaraible;
        public string channel;
        public List<ChannelData> modbusChannelList/* { get; set; }*/ = new List<ChannelData>();
    }
    public class ModbusMasterData
    {

        public int ID;
        //public DeviceData device { get; set; }

        public string transformChannel;
        public int responseTimeout = 1000;  //ms
        public int transformMode;
        public List<DeviceData> modbusDeviceList { get; set; } = new List<DeviceData>();
        public ModbusMasterData()
        {
            //0 RTU    1 ASCII
            transformMode = 0;
        }
    }

    public class ModbusMasterManage
    {
        public List<ModbusMasterData> modbusMastrList { get; set; } = new List<ModbusMasterData>();

        public ModbusMasterManage()
        {

        }

        public void add(ModbusMasterData data)
        {
            modbusMastrList.Add(data);
        }
    }
    

}

public class DataGridViewTextColumn : DataGridViewColumn
{
    public DataGridViewTextColumn()
    {
        //this.CellTemplate = new DataGridViewDisableButtonCell();
    }
}


public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
{
    public DataGridViewDisableButtonColumn()
    {
        this.CellTemplate = new DataGridViewDisableButtonCell();
    }
}

public class DataGridViewDisableButtonCell : DataGridViewButtonCell
{
    private bool enabledValue;
    public bool Enabled
    {
        get
        {
            return enabledValue;
        }
        set
        {
            enabledValue = value;
        }
    }

    // Override the Clone method so that the Enabled property is copied.
    public override object Clone()
    {
        DataGridViewDisableButtonCell cell =
            (DataGridViewDisableButtonCell)base.Clone();
        cell.Enabled = this.Enabled;
        return cell;
    }

    // By default, enable the button cell.
    public DataGridViewDisableButtonCell()
    {
        this.enabledValue = true;
    }

    protected override void Paint(Graphics graphics,
        Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
        DataGridViewElementStates elementState, object value,
        object formattedValue, string errorText,
        DataGridViewCellStyle cellStyle,
        DataGridViewAdvancedBorderStyle advancedBorderStyle,
        DataGridViewPaintParts paintParts)
    {
        // The button cell is disabled, so paint the border,  
        // background, and disabled button for the cell.
        if (!this.enabledValue)
        {
            // Draw the cell background, if specified.
            if ((paintParts & DataGridViewPaintParts.Background) ==
                DataGridViewPaintParts.Background)
            {
                SolidBrush cellBackground =
                    new SolidBrush(cellStyle.BackColor);
                graphics.FillRectangle(cellBackground, cellBounds);
                cellBackground.Dispose();
            }

            // Draw the cell borders, if specified.
            if ((paintParts & DataGridViewPaintParts.Border) ==
                DataGridViewPaintParts.Border)
            {
                PaintBorder(graphics, clipBounds, cellBounds, cellStyle,
                    advancedBorderStyle);
            }

            // Calculate the area in which to draw the button.
            Rectangle buttonArea = cellBounds;
            Rectangle buttonAdjustment =
                this.BorderWidths(advancedBorderStyle);
            buttonArea.X += buttonAdjustment.X;
            buttonArea.Y += buttonAdjustment.Y;
            buttonArea.Height -= buttonAdjustment.Height;
            buttonArea.Width -= buttonAdjustment.Width;

            // Draw the disabled button.                
            ButtonRenderer.DrawButton(graphics, buttonArea,
                PushButtonState.Disabled);

            // Draw the disabled button text. 
            if (this.FormattedValue is String)
            {
                TextRenderer.DrawText(graphics,
                    (string)this.FormattedValue,
                    this.DataGridView.Font,
                    buttonArea, SystemColors.GrayText);
            }
        }
        else
        {
            // The button cell is enabled, so let the base class 
            // handle the painting.
            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                elementState, value, formattedValue, errorText,
                cellStyle, advancedBorderStyle, paintParts);
        }
    }
}