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
    public partial class modbusclient1 : Form
    {
        private Button channel = new Button();
       
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        public modbusclient1()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //this.Controls.Add(channel);
            channel.Text = ". . .";
            
            DataColumn dc = null;
            //dt.Columns.Add("ID", Type.GetType("System.Int32"));
            //dt.Columns.Add("名称", Type.GetType("System.String"));
            //dt.Columns.Add("IP地址", Type.GetType("System.String"));
            //dt.Columns.Add("响应超时（ms）", Type.GetType("System.Int32"));
            //dt.Columns.Add("允许超时的次数", Type.GetType("System.Int32"));
            //dt.Columns.Add("重连间隔", Type.GetType("System.Int32"));
            //dt.Columns.Add("复位变量", Type.GetType("System.Int32"));
            //dt.Columns.Add(new DataColumn ("通道",typeof(bool)));
            for (int i = 0; i < 16; i++)
            {
                ds.Tables.Add(new DataTable());
                ds.Tables[i].Columns.Add("ID", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("名称", Type.GetType("System.String"));
                ds.Tables[i].Columns.Add("IP地址", Type.GetType("System.String"));
                ds.Tables[i].Columns.Add("响应超时（ms）", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("重连间隔", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("允许超时的次数", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("复位变量", Type.GetType("System.Int32"));

            }

        }

        public string cn = null;
        public void ClientNumber(string a)
        {
            cn = a;
            
        }
        public string get()
        {
            return cn;
        }
        DataGridViewButtonColumn mcg = new DataGridViewButtonColumn();
        public int channelnumber
        {
            get { return this.dataGridView1.SelectedRows[0].Index; }
        }
        private void modbusclient1_Load(object sender, EventArgs e)
        {
            Clientindex mci = new Clientindex();
            
            this.label5.Text = cn;
            // this.dataGridView1.DataSource = dt;
            this.dataGridView1.BeginInvoke(new Action(() => { this.dataGridView1.DataSource = ds.Tables[Convert.ToInt32(cn)]; }));
            //this.dataGridView1.DataSource = ds.Tables[form1.clientnumber];
            mcg.Name = "Goto";
            mcg.HeaderText = "跳转";
            mcg.DefaultCellStyle.NullValue = ". . .";
            //dataGridView1.Columns.Add(btn);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

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
            //this.dataGridView1.Rows.Add(rowcount, "设备" + rowcount.ToString(), "", "", "", "", "", "");
            //btn.Name = "Beginbtn";
            //btn.HeaderText = "通道";
            //btn.DefaultCellStyle.NullValue = ". . .";
            //if (dataGridView1.RowCount == 1)
            //{
            //    dataGridView1.Columns.Add(btn);
            //}
            DataRow dr = ds.Tables[0].NewRow();
            //Button add = new Button();
            //add.Text = ". . .";

            //dt.Rows.Add(dr);
            ////ds.Tables.Add(dt);
            ////dataGridView1.DataSource = ds.Tables[0];
            //btn.Name = "Beginbtn";
            //btn.HeaderText = "通道";
            //btn.DefaultCellStyle.NullValue = ". . .";
            int n = Convert.ToInt32(this.label5.Text);

            //dt.Rows.Add(row);
            ds.Tables[n].Rows.Add(dr.ItemArray);
            int a = ds.Tables[n].Rows.Count;
            if (dataGridView1.ColumnCount == 7)
            {
                dataGridView1.Columns.Add(mcg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            int n = Convert.ToInt32(dataGridView1.SelectedRows[0].Index);
            //ds.Tables[n].Clear();
        }
        ModbusClient.ClientChannel CCl = new ClientChannel();
        //ClientChannel CC ;
        //ClientChannel1 CC1 = new ClientChannel1();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //说明点击的列是DataGridViewButtonColumn列
            //DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
            //int currentrow = dataGridView1.SelectedRows[0].Index;
            //CCl = new ModbusClient.ClientChannel();
            //CC = new ClientChannel();
            CCl.ShowDialog(this);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
