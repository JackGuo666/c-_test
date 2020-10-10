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

        public modbusmasterchannel()
        {
            InitializeComponent();
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
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.RowCount;
            dataGridView1.RowCount += 1;

            // Set the text for each button.
            int i = row;

            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.ID].Value = row;
            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NAME].Value = "设备" + i.ToString();
            //
            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.MSGTYPE].Value = "";
            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.POLLINGTIME].Value = "1000";    //ms
            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READOFFSET].Value = "";
            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.READLENGTH].Value = "";
            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITEOFFSET].Value = "";
            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.WRITELENGTH].Value = "";
            dataGridView1.Rows[i].Cells[(int)COLUMNNAME_CHANNLE.NOTE].Value = "";
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
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[i]);
            }
        }
    }
}
