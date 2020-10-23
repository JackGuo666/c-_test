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
            XmlNodeList nodeList = xn.ChildNodes;
            foreach (XmlNode childNode in nodeList)
            {
                XmlElement e = (XmlElement)childNode;
                string name = e.Name;
                string test = e.GetAttribute("name");
                Console.WriteLine(name);

                ModbusMasterData data = new ModbusMasterData();

                int.TryParse(e.GetAttribute("id"), out data.ID);
                data.transformChannel = e.GetAttribute("transformchannel");
                int.TryParse(e.GetAttribute("transformmode"), out data.transformMode);
                int.TryParse(e.GetAttribute("responsetimeout"), out data.responseTimeout);

                //data.transformChannel = int.TryParse(eChild.GetAttribute("transformchannel"));

                masterManage.add(data);



                

            }
        }

        public void saveXml(ref XmlElement elem, ref XmlDocument doc)
        {
            XmlElement elem1 = doc.CreateElement("modbusmaster");
            elem1.SetAttribute("name", "张三");
            elem1.SetAttribute("class", "三年一班");
            elem1.SetAttribute("sex", "性别");
            elem.AppendChild(elem1);

            //master项
            for(int i = 0; i < masterManage.modbusMastrList.Count; i++)
            {
                ModbusMasterData data = masterManage.modbusMastrList.ElementAt(i);
                XmlElement elem1_m = doc.CreateElement("m");
                elem1_m.SetAttribute("id", data.ID.ToString());
                //transformChannel com1 com2 com3
                elem1_m.SetAttribute("transformchannel", data.transformChannel);
                //0 RTU    1 ASCII
                elem1_m.SetAttribute("transformmode", data.transformMode.ToString());
                elem1_m.SetAttribute("responsetimeout", data.responseTimeout.ToString());

                //create devices
                for(int j = 0; j < data.modbusDeviceList.Count; j ++)
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
                    for(int k = 0; k < dataDev.modbusChannelList.Count; k++)
                    {
                        ChannelData dataChannel = dataDev.modbusChannelList.ElementAt(i);
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

                        elem1_m_d.AppendChild(elem1_m_d_c);
                    }

                    elem1_m.AppendChild(elem1_m_d);
                }
                

                //XmlElement elem1_d = doc.CreateElement("device");
                //elem1_m.SetAttribute("id", data.ID.ToString());

                elem1.AppendChild(elem1_m);
            }

        }

        private enum COLUMNNAME : int
        {
            ID
        };
        //ttttttttttttttttttttttttttttttt
        //ttest
        private void button_add_Click(object sender, EventArgs e)
        {
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

        bool init = false;
        public void initForm()
        {
            if(init == false)
            {
                return;
            }
            
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows.RemoveAt(i);
            }



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
            
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows.RemoveAt(i);
            }

            //for (int i = 0; i < dataGridView1.Columns.Count; i++)
            //{
            //    dataGridView1.Columns.RemoveAt(i);
            //}



            DataGridViewDisableButtonColumn buttonColumn = new DataGridViewDisableButtonColumn();
            buttonColumn.Name = columnConfig;

            DataGridViewTextBoxColumn cellColumn = new DataGridViewTextBoxColumn();
            cellColumn.Name = "ID";

            dataGridView1.Columns.Add(cellColumn);
            dataGridView1.Columns.Add(buttonColumn);
            dataGridView1.RowCount = /*8*/ 1 + masterManage.modbusMastrList.Count;
            dataGridView1.AutoSize = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            //for (int i = 0; i < masterManage.modbusMastrList.Count; i++)
            //{
            //    ModbusMasterData data = masterManage.modbusMastrList.ElementAt(i);
            //    dataGridView1.Rows[i].Cells["ID"].Value = data.ID;
            //    dataGridView1.Rows[i].Cells[columnConfig].Value = "..."/* + i.ToString()*/;
            //}

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