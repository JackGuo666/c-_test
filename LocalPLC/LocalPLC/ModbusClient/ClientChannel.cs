using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LocalPLC.ModbusClient
{
    public partial class ClientChannel : Form
    {
        private ComboBox functioncode1 = new ComboBox();
        modbusclient mct;
        int rownumber;
        ClientChannel CC2;
        public struct channeldate
        {
            public int CID;
            public int CName;
            public int CFunction;
            public int CCycletime;
            public int CWriteoffset;
            public int CWritelength;
            public int CReadoffset;
            public int CReadlength;
            public string Cannotation;
        };
        public channeldate[] CD = new channeldate[16];

        public int a;

        public ClientChannel(int datagrid1cr)
        {
            InitializeComponent();
            dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.rownumber = datagrid1cr;
            label3.Text = rownumber.ToString();
            label5.Text = rownumber.ToString();

            for (int i = 0; i < a; i++)
            {
                
                dataGridView2[0, i].Value = CD[i].CID;
            }
        }

        private void ClientChannel_Load(object sender, EventArgs e)
        {
            
            
        }

        private void add_Click(object sender, EventArgs e)
        {
            int rowcount = dataGridView2.RowCount;
            this.dataGridView2.Rows.Add(rowcount, "设备" + rowcount.ToString(), functioncode1, "", "", "", "", "");
        }

        private void groupchannel_Enter(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {
            this.dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[0].Index);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rowcount = this.dataGridView2.RowCount;
            a = rowcount;
            for (int row = 1; row < rowcount; row++)
            {
                CD[row].CID = Convert.ToInt32(this.dataGridView2[0, row].Value.ToString());
                CD[row].CName = Convert.ToInt32(this.dataGridView2[1, row].Value.ToString());
                CD[row].CFunction = Convert.ToInt32(this.dataGridView2[2, row].Value.ToString());
                CD[row].CCycletime = Convert.ToInt32(this.dataGridView2[3, row].Value.ToString());
                CD[row].CReadoffset = Convert.ToInt32(this.dataGridView2[4, row].Value.ToString());
                CD[row].CReadlength = Convert.ToInt32(this.dataGridView2[5, row].Value.ToString());
                CD[row].CWriteoffset = Convert.ToInt32(this.dataGridView2[6, row].Value.ToString());
                CD[row].CWritelength = Convert.ToInt32(this.dataGridView2[7, row].Value.ToString());
                CD[row].Cannotation = this.dataGridView2[8, row].Value.ToString();
                
            }
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRowView row1 = (DataRowView)dataGridView2.Rows[e.RowIndex].DataBoundItem;
            if (row1.Row.RowState == DataRowState.Unchanged)
                return;
            row1.Row.AcceptChanges();
        }
    }
}
