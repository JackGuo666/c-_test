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

namespace LocalPLC.ModbusSlave
{
    public partial class modbusslavemain : UserControl
    {

        private DataManager dataManager = null;

        

        public modbusslavemain()
        {
            InitializeComponent();

            dataManager = DataManager.GetInstance();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void modbusslavemain_Load(object sender, EventArgs e)
        {
            DataGridViewDisableButtonColumn buttonColumn = new DataGridViewDisableButtonColumn();
            buttonColumn.Name = "配置";

            DataGridViewTextBoxColumn cellColumn = new DataGridViewTextBoxColumn();
            cellColumn.Name = "ID";

            dataGridView1.Columns.Add(cellColumn);
            dataGridView1.Columns.Add(buttonColumn);
            dataGridView1.RowCount = /*8*/ 1;
            dataGridView1.AutoSize = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
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

                dataManager.listSlave.Add(data);
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

                    modbusslaveform form = new modbusslaveform(e.RowIndex);
                    ModbusSlaveData data = dataManager.listSlave.ElementAt(e.RowIndex);
                    form.getSlaveData(ref data);
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
                dataManager.listSlave.RemoveAt(index);
            }
        }
    }
}
