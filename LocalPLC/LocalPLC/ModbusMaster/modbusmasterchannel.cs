using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LocalPLC.ModbusMaster
{


    public partial class modbusmasterchannel : Form
    {
        public enum COLUMNNAME_CHANNLE : int
        {
            ID, NAME, MSGTYPE, POLLINGTIME, READOFFSET, READLENGTH,
            WRITEOFFSET, WRITELENGTH, NOTE
        };

        private DeviceData data_;
        public modbusmasterchannel()
        {
            InitializeComponent();
        }

        public void getDeviceData(ref DeviceData data)
        {
            data_ = data;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex < 0 | e.RowIndex < 0)
            {
                return;
            }
        }

        private void modbusmasterchannel_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn cellColumnID = new DataGridViewTextBoxColumn();
            cellColumnID.Name = "ID";
            DataGridViewTextBoxColumn cellColumnName = new DataGridViewTextBoxColumn();
            cellColumnName.Name = "名称";



            DataGridViewComboBoxColumn columnMsgType = new DataGridViewComboBoxColumn();
            columnMsgType.Name = "消息类型";
            columnMsgType.Items.Add("读多个位(线圈) - 0x01");
            columnMsgType.Items.Add("读多个位(离散输入) - 0x02");
            columnMsgType.Items.Add("读多个字(保持寄存器) - 0x03");
            columnMsgType.Items.Add("读多个字(输入寄存器) - 0x04");
            columnMsgType.Items.Add("写单个位(线圈) - 0x05");
            columnMsgType.Items.Add("写单个字(寄存器) - 0x06");
            columnMsgType.Items.Add("写多个位(线圈) - 0x0F");
            columnMsgType.Items.Add("写多个字(寄存器) - 0x10");


            DataGridViewTextBoxColumn cellColumnPolling = new DataGridViewTextBoxColumn();
            cellColumnPolling.Name = "循环触发事件";
            DataGridViewTextBoxColumn cellColumnReadOffset = new DataGridViewTextBoxColumn();
            cellColumnReadOffset.Name = "读偏移";
            DataGridViewTextBoxColumn cellColumnReadLength = new DataGridViewTextBoxColumn();
            cellColumnReadLength.Name = "读长度";
            DataGridViewTextBoxColumn cellColumnWriteOffset = new DataGridViewTextBoxColumn();
            cellColumnWriteOffset.Name = "写偏移";
            DataGridViewTextBoxColumn cellColumnWriteLength = new DataGridViewTextBoxColumn();
            cellColumnWriteLength.Name = "写长度";
            DataGridViewTextBoxColumn cellColumnNote = new DataGridViewTextBoxColumn();
            cellColumnNote.Name = "注释";
            //列标题自适应
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;


            dataGridView1.Columns.Add(cellColumnID);
            dataGridView1.Columns.Add(cellColumnName);
            dataGridView1.Columns.Add(columnMsgType);
            dataGridView1.Columns.Add(cellColumnPolling);
            dataGridView1.Columns.Add(cellColumnReadOffset);
            dataGridView1.Columns.Add(cellColumnReadLength);
            dataGridView1.Columns.Add(cellColumnWriteOffset);
            dataGridView1.Columns.Add(cellColumnWriteLength);
            dataGridView1.Columns.Add(cellColumnNote);
            dataGridView1.RowCount = /*8*/ 1;
            dataGridView1.AutoSize = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;


            dataGridView1.Columns[(int)COLUMNNAME_CHANNLE.MSGTYPE].Width = 280;
            //dataGridView1.AllowUserToResizeColumns = true;

            textBox1.Text =  data_.nameDev.ToString();
            textBox3.Text = data_.slaveAddr.ToString();

        }

        private void button_add_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.RowCount;
            dataGridView1.RowCount += 1;

            // Set the text for each button.
            int i = row;

            ChannelData data = new ChannelData();

            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.ID].Value = row;
            data.ID = row;


            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NAME].Value = "设备" + i.ToString();
            data.nameChannel = dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NAME].Value.ToString();

            //
            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.MSGTYPE].Value = "";
            if(dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.MSGTYPE].Value.ToString() == "")
            {
                data.msgType = -1;
            }
            else 
            {
                data.msgType = int.Parse(dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.MSGTYPE].Value.ToString());
            }

            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.POLLINGTIME].Value = "1000";    //ms
            data.pollingTime = int.Parse(dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.POLLINGTIME].Value.ToString());

            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READOFFSET].Value = "0";
            string str = dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READOFFSET].Value.ToString();
            data.readOffset = int.Parse(str);

            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READLENGTH].Value = "1";
            str = dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READLENGTH].Value.ToString();
            data.readLength = int.Parse(str);

            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITEOFFSET].Value = "0";
            str = dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITEOFFSET].Value.ToString();
            data.writeOffset = int.Parse(str);

            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITELENGTH].Value = "1";
            str = dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITELENGTH].Value.ToString();
            data.writeLength = int.Parse(str);

            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NOTE].Value = "";
            data.note = dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NOTE].Value.ToString();

            data_.modbusChannelList.Add(data);
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            // int row = dataGridView1.SelectedRows[0];
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                return;
            }

            for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
            {
                int index = dataGridView1.SelectedRows[i].Index;

                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[i]);
                data_.modbusChannelList.RemoveAt(index);
            }
        }
    }
}
