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
    public partial class modbusmasterform : Form
    {
        public modbusmasterform()
        {
            InitializeComponent();
        }

        public enum COLUMNNAME :  int
            { ID, NAME, SLAVE_ADDR, REPONSE_TIMEOUT, PERMIT_TIMEOUT_COUNT, RECONNECT_INTERVAL
                                        , RESET_VARIABLE, CHANNEL};
        private string[] columnName = {"ID", "name"};
        private void modbusmasterform_Load(object sender, EventArgs e)
        { 
            DataGridViewTextBoxColumn cellColumnID = new DataGridViewTextBoxColumn();
            cellColumnID.Name = "ID";
            DataGridViewTextBoxColumn cellColumnName = new DataGridViewTextBoxColumn();
            cellColumnName.Name = "名称";
            DataGridViewTextBoxColumn cellColumnSlaveAddr = new DataGridViewTextBoxColumn();
            cellColumnSlaveAddr.Name = "从站地址";
            DataGridViewTextBoxColumn cellColumnTimeout = new DataGridViewTextBoxColumn();
            cellColumnTimeout.Name = "响应超时(ms)";
            DataGridViewTextBoxColumn cellColumnTimeoutCount = new DataGridViewTextBoxColumn();
            cellColumnTimeoutCount.Name = "允许的超时次数";
            DataGridViewTextBoxColumn cellColumnReconnectInvertal = new DataGridViewTextBoxColumn();
            cellColumnReconnectInvertal.Name = "重连间隔";
            DataGridViewTextBoxColumn cellColumnResetVariable = new DataGridViewTextBoxColumn();
            cellColumnResetVariable.Name = "复位变量";
            DataGridViewDisableButtonColumn buttonColumn = new DataGridViewDisableButtonColumn();
            buttonColumn.Name = "通道";
            //列标题自适应
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;


            dataGridView1.Columns.Add(cellColumnID);
            dataGridView1.Columns.Add(cellColumnName);
            dataGridView1.Columns.Add(cellColumnSlaveAddr);
            dataGridView1.Columns.Add(cellColumnTimeout);
            dataGridView1.Columns.Add(cellColumnTimeoutCount);
            dataGridView1.Columns.Add(cellColumnReconnectInvertal);
            dataGridView1.Columns.Add(cellColumnResetVariable);
            dataGridView1.Columns.Add(buttonColumn);
            dataGridView1.RowCount = /*8*/ 1;
            dataGridView1.AutoSize = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            this.comboBox_transform_channel.Items.Add("COM1");
            this.textBox_reponse_timeout.Text = "1000";   //ms
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.RowCount;
            dataGridView1.RowCount += 1;

            // Set the text for each button.
            int i = row;

            // for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                //dataGridView1.Rows[i].Cells["ID"].Value = row;
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.ID].Value = row;
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.NAME].Value = "设备" + i.ToString();
                //
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.SLAVE_ADDR].Value = "";
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.REPONSE_TIMEOUT].Value = 1000;
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.PERMIT_TIMEOUT_COUNT].Value = 5;
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.RECONNECT_INTERVAL].Value = 1000;
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.RESET_VARIABLE].Value = "";
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.CHANNEL].Value = "Button " + i.ToString();

                //dataGridView1.Cell.Value = "Button " + i.ToString();
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            // int row = dataGridView1.SelectedRows[0];
            if(dataGridView1.SelectedRows.Count <= 0)
            {
                return;
            }

            for(int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
            {
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[i]);
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                return;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Index == (int)COLUMNNAME.CHANNEL)
            {
                DataGridViewDisableButtonCell buttonCell =
                    (DataGridViewDisableButtonCell)dataGridView1.
                    Rows[e.RowIndex].Cells[(int)COLUMNNAME.CHANNEL];

                if (buttonCell.Enabled)
                {
                    //MessageBox.Show(dataGridView1.Rows[e.RowIndex].
                    //    Cells[e.ColumnIndex].Value.ToString() +
                    //    " is enabled");

                    modbusmasterchannel form = new modbusmasterchannel();
                    form.ShowDialog();
                }
            }
        }
    }
}
