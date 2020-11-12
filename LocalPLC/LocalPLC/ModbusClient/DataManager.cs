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

namespace LocalPLC.ModbusClient
{
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
        public string ipaddr;
        public string serverAddr;
        public int reponseTimeout;
        public int permitTimeoutCount;
        public int reconnectInterval;
        public string resetVaraible;
        public string channel;
        public List<ChannelData> modbusChannelList/* { get; set; }*/ = new List<ChannelData>();
    }
    public class ModbusClientData
    {

        public int ID;
        //public DeviceData device { get; set; }

        public string transformChannel;
        public int responseTimeout = 1000;  //ms
        public int transformMode;
        public List<DeviceData> modbusDeviceList { get; set; } = new List<DeviceData>();
        public ModbusClientData()
        {
            //0 TCP    1 UDP
            transformMode = 0;
        }
    }
}
namespace LocalPLC.ModbusClient
{
    public class ModbusClientManage
    {
        public List<ModbusClientData> modbusClientList { get; set; } = new List<ModbusClientData>();

        public ModbusClientManage()
        {

        }

        public void add(ModbusClientData data)
        {
            modbusClientList.Add(data);
        }
    }
}
namespace LocalPLC.ModbusClient
{
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
}