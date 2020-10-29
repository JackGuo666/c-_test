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
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataSet dc = new DataSet();
        int rownumber;
        //ClientChannel CC2;
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

        //public ClientChannel(int datagrid1cr)//int datagrid1cr)
        public ClientChannel()
        {
            InitializeComponent();
            dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //this.rownumber = datagrid1cr;
            

            for (int i = 0; i < 16; i++)
            {
                ds.Tables.Add(new DataTable());
                ds.Tables[i].Columns.Add("ID", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("名称", Type.GetType("System.String"));
                ds.Tables[i].Columns.Add("IP地址", Type.GetType("System.String"));
                ds.Tables[i].Columns.Add("消息类型（功能码）", Type.GetType("System.String"));
                ds.Tables[i].Columns.Add("循环触发时间", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("读偏移", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("读长度", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("写偏移", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("写长度", Type.GetType("System.Int32"));
                ds.Tables[i].Columns.Add("注释", Type.GetType("System.String"));
            }
            dc.Tables.Add(new DataTable());
            dc.Tables[0].Columns.Add("functioncode");
            dc.Tables[0].Columns.Add("displayvalue");
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "读多个位(线圈)-0x01", dc.Tables[0].NewRow()[1] = 1);
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "读多个位(离散输入)-0x02", dc.Tables[0].NewRow()[1] = 2);
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "读多个字（保持寄存器）-0x03", dc.Tables[0].NewRow()[1] = 3);
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "读多个字（输入寄存器）-0x04", dc.Tables[0].NewRow()[1] = 4);
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "写单个位（线圈）-0x05", dc.Tables[0].NewRow()[1] = 5);
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "写单个字（寄存器）-0x06", dc.Tables[0].NewRow()[1] = 6);
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "写多个位（线圈）-0x0F", dc.Tables[0].NewRow()[1] = 7);
            dc.Tables[0].Rows.Add(dc.Tables[0].NewRow()[0] = "写多个字（寄存器）-0x01", dc.Tables[0].NewRow()[1] = 8);
            //for (int i = 0; i < a; i++)
            //{

            //    dataGridView2[0, i].Value = CD[i].CID;
            //}
        }

        //DataGridViewComboBoxColumn dc = new DataGridViewComboBoxColumn();
        private void ClientChannel_Load(object sender, EventArgs e)
        {
            ModbusClient.modbusclient1 mc1 = (modbusclient1)this.Owner;
            this.label3.Text = mc1.channelnumber.ToString();
            this.label5.Text = mc1.channelnumber.ToString();

            this.dataGridView2.BeginInvoke(new Action(() => { this.dataGridView2.DataSource = ds.Tables[mc1.channelnumber]; }));
            

        }

        private void add_Click(object sender, EventArgs e)
        {
            //int rowcount = dataGridView2.RowCount;
            //this.dataGridView2.Rows.Add(rowcount, "设备" + rowcount.ToString(), functioncode1, "", "", "", "", "");


            DataRow dr = ds.Tables[0].NewRow();
            
            int n = Convert.ToInt32(this.label3.Text);
            dr[0] = dataGridView2.RowCount;
            dr[1] = "设备" + dataGridView2.RowCount;
            
            ds.Tables[n].Rows.Add(dr.ItemArray);
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
                cell.DataSource = dc.Tables[0];
                cell.DisplayMember = "functioncode";
                cell.ValueMember = "displayvalue";
                cell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                dataGridView2.Rows[i].Cells["消息类型（功能码）"] = cell;
            }
        }

        private void groupchannel_Enter(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {
            this.dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[0].Index);
            int n = Convert.ToInt32(dataGridView2.SelectedRows[0].Index);
            ds.Tables[n].Clear();
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

        private void ClientChannel_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
                cell.DataSource = dc.Tables[0];
                cell.DisplayMember = "functioncode" ;
                cell.ValueMember = "displayvalue";
                cell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                dataGridView2.Rows[i].Cells["消息类型（功能码）"] = cell;
            }
        }
    }
}
