using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LocalPLC.ModbusClient
{
    public partial class Clientindex : UserControl
    {
        public Clientindex()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        
        private void Clientindex_Load(object sender, EventArgs e)
        {

        }
        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();

        //public int clientnumber;

       
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
            dataGridView1.Rows[rowcount].Cells[0].Value = rowcount;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
        }

        UserControl1 user1 = new UserControl1();
        ModbusClient.modbusclient1 mct1 = new modbusclient1();
        public int a;
        public string clientnumber
        {
            
            set { clientnumber = value; }
            get { return this.label1.Text; }
        }
        //public void cilentnumber1(int cn)
        //{
        //    this.label1.Text = 
        //}
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.label1.Text = this.dataGridView1.SelectedRows[0].Index.ToString();
            mct1.ClientNumber(this.label1.Text);
            mct1.ShowDialog(this);
            //a = dataGridView1.SelectedRows[0].Index;
        }
    }
}
