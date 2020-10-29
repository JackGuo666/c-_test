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
    public partial class modbusclient : UserControl
    {
        private Button channel = new Button();
        ModbusClient.ClientChannel CCl;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        public modbusclient()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //this.Controls.Add(channel);
            channel.Text = ". . .";
        }

        private void modbusclient_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = "1000";
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("年龄", typeof(int));
            ds.Tables.Add(dt);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            string Item = this.comboBox1.SelectedItem.ToString();
            if (Item == "网口1")
            {

            }
        }

        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
        private void button1_Click(object sender, EventArgs e)
        {
            int rowcount = dataGridView1.RowCount;
            this.dataGridView1.Rows.Add(rowcount, "设备" + rowcount.ToString(), "", "", "", "", "", "");
            btn.Name = "Beginbtn";
            btn.HeaderText = "通道";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //说明点击的列是DataGridViewButtonColumn列
            DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
            int currentrow = dataGridView1.CurrentRow.Index;
            //CCl = new ModbusClient.ClientChannel(currentrow);
            CCl.ShowDialog();
        }

        private void ClientSettings_Enter(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataRow row = dt.NewRow();

            dt.Rows.Add(row);
            this.dataGridView1.DataSource = ds.Tables[0];

        }
    }
}
