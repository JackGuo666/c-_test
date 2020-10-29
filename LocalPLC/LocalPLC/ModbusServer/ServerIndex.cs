using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LocalPLC.ModbusServer
{
    public partial class ServerIndex : UserControl
    {
        public ServerIndex()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ServerIndex_Load(object sender, EventArgs e)
        {

        }
        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
        private void button1_Click(object sender, EventArgs e)
        {
            int rowcount = dataGridView1.RowCount;
            this.dataGridView1.Rows.Add(1);
            btn.Name = "Goto";
            btn.HeaderText = "跳转";
            btn.DefaultCellStyle.NullValue = ". . .";
            if (dataGridView1.RowCount == 1)
            {
                dataGridView1.Columns.Add(btn);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
        }
        ModbusServer.modbusserver mss = new modbusserver();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           mss.ShowDialog();
        }
    }
}
