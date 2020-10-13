using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LocalPLC.ModbusSlave
{
    public partial class modbusslavemain : UserControl
    {
        public modbusslavemain()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void modbusslavemain_Load(object sender, EventArgs e)
        {
            DataGridViewDisableButtonColumn buttonColumn = new DataGridViewDisableButtonColumn();
            buttonColumn.Name = "test";

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

            // for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells["ID"].Value = row;
                //data.ID = row;
                dataGridView1.Rows[i].Cells["test"].Value = "Button " + i.ToString();
                //data.device = new DeviceData();
            }
        }
    }
}
